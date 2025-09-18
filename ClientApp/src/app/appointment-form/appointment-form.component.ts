import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, FormArray, Validators } from '@angular/forms';
import { Router, ActivatedRoute } from '@angular/router';
import { AppointmentService } from '../_services/appointment.service';

@Component({
  selector: 'app-appointment-form',
  templateUrl: './appointment-form.component.html',
  styleUrls: ['./appointment-form.component.css']
})
export class AppointmentFormComponent implements OnInit {
  appointmentForm: FormGroup;
  isEditMode = false;
  appointmentId: number | null = null;

  constructor(
    private fb: FormBuilder,
    public appointmentService: AppointmentService,
    private router: Router,
    private route: ActivatedRoute
  ) {
    this.appointmentForm = this.createForm();
  }

  ngOnInit(): void {
    this.route.params.subscribe(params => {
      if (params['id']) {
        this.isEditMode = true;
        this.appointmentId = +params['id'];
        this.loadAppointment(this.appointmentId);
      }
    });
  }

  createForm(): FormGroup {
    return this.fb.group({
      patient: ['', Validators.required],
      doctor: ['', Validators.required],
      date: ['', Validators.required],
      visitType: ['', Validators.required],
      notes: [''],
      diagnosis: [''],
    });
  }

  // get prescriptionsArray(): FormArray {
  //   return this.appointmentForm.get('prescriptions') as FormArray;
  // }

  // createPrescriptionForm(prescription?: Prescription): FormGroup {
  //   return this.fb.group({
  //     medicine: [prescription?.medicine || ''],
  //     dosage: [prescription?.dosage || ''],
  //     startDate: [prescription?.startDate || ''],
  //     endDate: [prescription?.endDate || ''],
  //     notes: [prescription?.notes || '']
  //   });
  // }

  // addPrescriptionRow(): void {
  //   this.prescriptionsArray.push(this.createPrescriptionForm());
  // }

  // removePrescriptionRow(index: number): void {
  //   this.prescriptionsArray.removeAt(index);
  // }

  loadAppointment(id: number): void {
    const appointment = this.appointmentService.getAppointmentById(id);
    if (appointment) {
      this.appointmentForm.patchValue({
        patient: appointment.patient,
        doctor: appointment.doctor,
        date: appointment.date,
        visitType: appointment.visitType,
        notes: appointment.notes,
        diagnosis: appointment.diagnosis
      });

      // Load prescriptions
      // appointment.prescriptions.forEach(prescription => {
      //   this.prescriptionsArray.push(this.createPrescriptionForm(prescription));
      // });
    }
  }

  onSubmit(): void {
    if (this.appointmentForm.valid) {
      const formValue = this.appointmentForm.value;

      if (this.isEditMode && this.appointmentId) {
        this.appointmentService.updateAppointment(this.appointmentId, formValue);
      } else {
        this.appointmentService.createAppointment(formValue);
      }

      this.router.navigate(['/appointment-list']);
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
