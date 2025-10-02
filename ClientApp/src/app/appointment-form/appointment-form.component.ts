import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, FormArray, Validators } from '@angular/forms';
import { Router, ActivatedRoute } from '@angular/router';
import { AppointmentService } from '../_services/appointment.service';
import { Observable, Subscription } from 'rxjs';
import { PatientDTO } from '../_models/patientDTO';
import { DoctorDTO } from '../_models/doctorDTO';
import { DataService } from '../_services/data.service';
import { AppointmentDTO } from '../_models/appointmentDTO';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-appointment-form',
  templateUrl: './appointment-form.component.html',
  styleUrls: ['./appointment-form.component.css']
})
export class AppointmentFormComponent implements OnInit {
  appointmentForm!: FormGroup;
  isEditMode = false;
  appointmentId: number | null = null;
  allPatients!: Observable<PatientDTO[]>;
  allDoctors!: Observable<DoctorDTO[]>;

  constructor(
    private fb: FormBuilder,
    public appointmentService: AppointmentService,
    public dataService: DataService,
    private router: Router,
    private route: ActivatedRoute,
    private toastr: ToastrService
  ) { }

  ngOnInit(): void {
    this.appointmentForm = this.createForm();

    this.allDoctors = this.dataService.getDoctors();
    this.allPatients = this.dataService.getPatients();

    this.route.params.subscribe(params => {
      if (params['id']) {
        this.isEditMode = true;
        this.appointmentId = Number(params['id']);
        this.loadAppointment(this.appointmentId);
      }
    });
  }

  createForm(): FormGroup {
    return this.fb.group({
      id: [''],
      patientId: ['', Validators.required],
      doctorId: ['', Validators.required],
      appointmentDate: ['', Validators.required],
      visitType: ['', Validators.required],
      notes: ['', Validators.required],
      diagnosis: ['', Validators.required]
    });
  }

  loadAppointment(id: number): void {
    this.appointmentService.getAppointmentById(id).subscribe({
      next: (appointment: AppointmentDTO) => {
        if (appointment) {
          this.appointmentForm.patchValue({
            id: appointment.id,
            patientId: appointment.patientId,
            patientName: appointment.patientName,
            doctorId: appointment.doctorId,
            doctorName: appointment.doctorName,
            appointmentDate: appointment.appointmentDate.split('T')[0],
            visitType: appointment.visitType,
            notes: appointment.notes,
            diagnosis: appointment.diagnosis
          });
        }
      },
      error: (e) => {
        console.log('Error: ' + e);
      }
    });
  }

  onSubmit(): void {
    if (this.appointmentForm.valid) {
      const formValue: AppointmentDTO = this.appointmentForm.value as AppointmentDTO;

      formValue.appointmentDate = new Date(formValue.appointmentDate).toISOString();

      if (this.isEditMode && this.appointmentId) {

        this.appointmentService.updateAppointment(this.appointmentId, formValue)
          .subscribe({
            next: (res) => {
              this.toastr.success('Appointment updated!', 'Success');
              this.router.navigate(['/appointment-list']);
            },
            error: (err) => {
              console.log(err);
              this.toastr.error('Failed', 'Error');
            }
          });
      }
      else {
        this.appointmentService.createAppointment(formValue)
          .subscribe({
            next: (res) => {
              this.toastr.success('Appointment created!', 'Success');
              this.router.navigate(['/appointment-list']);
            },
            error: (err) => {
              console.log(err);
              this.toastr.error('Failed', 'Error');
            }
          });


      }

    }
  }

  cancel(): void {
    this.router.navigate(['/appointment-list']);
  }

  isFieldInvalid(fieldName: string): boolean {
    const field = this.appointmentForm.get(fieldName);
    return !!(field && field.invalid && (field.dirty || field.touched));
  }
}
