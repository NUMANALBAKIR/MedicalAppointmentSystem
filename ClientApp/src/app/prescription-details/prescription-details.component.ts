import { Component, OnInit } from '@angular/core';
import { FormArray, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { AppointmentService } from '../_services/appointment.service';
import { MedicineDTO } from '../_models/medicineDTO';
import { Observable } from 'rxjs';
import { DataService } from '../_services/data.service';
import { PrescriptionDetailDTO } from '../_models/prescriptionDetailDTO';
import { AppointmentDTO } from '../_models/appointmentDTO';
import { PrescriptionService } from '../_services/prescription.service';

@Component({
  selector: 'app-prescription-details',
  templateUrl: './prescription-details.component.html',
  styleUrls: ['./prescription-details.component.css']
})
export class PrescriptionDetailsComponent implements OnInit {

  prescriptionFormGroup!: FormGroup;
  public allMedicine!: Observable<MedicineDTO[]>;
  public appointment!: AppointmentDTO;

  constructor(
    private router: Router,
    private route: ActivatedRoute,
    private fb: FormBuilder,
    public appointmentService: AppointmentService,
    public dataService: DataService,
    public prescriptionService: PrescriptionService

  ) {
    this.prescriptionFormGroup = this.createPrescriptionFormGroup();
  }

  ngOnInit() {
    this.allMedicine = this.dataService.getMedicines();

    this.route.params.subscribe(params => {
      if (params['id']) {
        let appointmentId = Number(params['id']);
        this.loadAppointWithPrescriptions(appointmentId);
      }
    });
  }

  createPrescriptionFormGroup(): FormGroup {
    return this.fb.group({
      prescriptions: this.fb.array([])
    });
  }

  createPrescriptionRow(prescription?: PrescriptionDetailDTO): FormGroup {
    return this.fb.group({
      id: [prescription?.id || 0],
      appointmentId: [this.appointment?.id],
      medicineId: [prescription?.medicineId || ''],
      medicineName: ['med name'],
      dosage: [prescription?.dosage || ''],
      startDate: [prescription?.startDate.split('T')[0] || ''],
      endDate: [prescription?.endDate.split('T')[0] || ''],
      notes: [prescription?.notes || ''],
    });
  }

  get getPrescriptionArray(): FormArray {
    return this.prescriptionFormGroup.get('prescriptions') as FormArray;
  }

  addPrescriptionRow(): void {
    this.getPrescriptionArray.push(this.createPrescriptionRow());
  }

  removePrescriptionRow(index: number): void {
    this.getPrescriptionArray.removeAt(index);
  }

  loadAppointWithPrescriptions(id: number): void {
    this.appointmentService.getAppointmentById(id).subscribe(
      (appointment: AppointmentDTO) => {
        if (appointment) {
          this.appointment = appointment;

          if (appointment?.prescriptions?.length > 0) {
            appointment.prescriptions.forEach(prescription => {
              this.getPrescriptionArray.push(this.createPrescriptionRow(prescription));
            });
          }
          else {
            this.addPrescriptionRow();
          }
        }
      },
      (e) => {
        console.log('Error: ' + e);
      }
    );
  }

  onSubmit() {
    if (this.prescriptionFormGroup.valid) {
      const formValue = this.getPrescriptionArray.value as PrescriptionDetailDTO[];

      formValue.forEach(e => {
        e.startDate = new Date(e.startDate).toISOString();
        e.endDate = new Date(e.endDate).toISOString();
      });

      console.log('Prescriptions:', formValue);

      this.prescriptionService.updatePrescriptions(formValue)
        .subscribe({
          next: (res) => {
            this.router.navigate(['/appointment-list']);
          },
          error: (err) => {
            console.log(err);
          }
        });

    }
  }

  cancel(): void {
    this.router.navigate(['/appointment-list']);
  }

}
