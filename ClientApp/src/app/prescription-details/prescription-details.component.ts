import { Component, OnInit } from '@angular/core';
import { Appointment } from '../_models/appointment';
import { FormArray, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Prescription } from '../_models/prescription';
import { ActivatedRoute, Router } from '@angular/router';
import { AppointmentService } from '../_services/appointment.service';

@Component({
  selector: 'app-prescription-details',
  templateUrl: './prescription-details.component.html',
  styleUrls: ['./prescription-details.component.css']
})
export class PrescriptionDetailsComponent implements OnInit {

  prescriptionForm!: FormGroup;

  constructor(
    private router: Router,
    private route: ActivatedRoute,
    private fb: FormBuilder,
    public appointmentService: AppointmentService
  ) {
    this.prescriptionForm = this.createPrescriptionForm();
  }

  ngOnInit() {
  }


  get prescriptions(): FormArray {
    return this.prescriptionForm.get('prescriptions') as FormArray;
  }

  createPrescriptionForm(): FormGroup {
    return this.fb.group({
      medicine: ['', Validators.required],
      dosage: ['', Validators.required],
      startDate: ['', Validators.required],
      endDate: ['', Validators.required],
      notes: ['']
    });
  }


  addPrescription() {
    this.prescriptions.push(this.createPrescriptionForm());
  }

  removePrescription(index: number) {
    if (this.prescriptions.length > 1) {
      this.prescriptions.removeAt(index);
    }
  }


  onSubmit() {
    if (this.prescriptionForm.valid) {
      const formValue = this.prescriptionForm.value;
      console.log('Prescriptions:', formValue.prescriptions);
      // Process the prescription data here
    }
  }

  cancel(): void {
    this.router.navigate(['/appointment-list']);
  }

  // isFieldInvalid(fieldName: string): boolean {
  //   const field = this.prescriptionForm.get(fieldName);
  //   return !!(field && field.invalid && (field.dirty || field.touched));
  // }

}
