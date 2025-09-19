import { AppointmentDTO } from '../_models/appointmentDTO';
import { Component, OnInit, OnDestroy } from '@angular/core';
import { Router } from '@angular/router';
import { debounceTime, distinctUntilChanged, Observable, Subject, takeUntil } from 'rxjs';
import { AppointmentService } from '../_services/appointment.service';
import { DoctorDTO } from '../_models/doctorDTO';
import { DataService } from '../_services/data.service';
import { PrescriptionPrintService } from '../_services/prescription-print.service';
import { ToastrService } from 'ngx-toastr';
import { PrescriptionService } from '../_services/prescription.service';

@Component({
  selector: 'app-appointment-list',
  templateUrl: './appointment-list.component.html',
  styleUrls: ['./appointment-list.component.css']
})
export class AppointmentListComponent implements OnInit {

  allDoctor!: Observable<DoctorDTO[]>;
  paginatedAppointments: AppointmentDTO[] = [];
  searchTerm = '';
  doctorFilter = '';
  visitTypeFilter = '';

  currentPage = 1;
  pageSize = 2;
  totalItems = 0;
  totalPages = 0;
  loading = false;

  constructor(
    public appointmentService: AppointmentService,
    public dataService: DataService,
    private router: Router,
    private prescriptionPrintService: PrescriptionPrintService,
    private toastr: ToastrService,
    private prescriptionService: PrescriptionService
  ) { }

  ngOnInit(): void {
    this.allDoctor = this.dataService.getDoctors();
    this.loadAppointments();
  }

  loadAppointments(): void {
    this.loading = true;

    this.appointmentService.getAppointments(
      this.currentPage,
      this.pageSize,
      this.searchTerm,
      this.doctorFilter,
      this.visitTypeFilter
    ).subscribe({
      next: (response: any) => {
        this.paginatedAppointments = response.data;
        this.totalItems = response.totalItems;
        this.totalPages = response.totalPages;
        this.loading = false;
      },
      error: (err) => {
        console.error('Error loading appointments:', err);
        this.toastr.error('Failed to load appointments', 'Error');
        this.loading = false;
      }
    });
  }

  applyFilters(): void {
    this.currentPage = 1;
    this.loadAppointments();
  }

  onPageChanged(event: any): void {
    this.currentPage = event.page;
    this.loadAppointments();
  }

  navigateToAdd(): void {
    this.router.navigate(['/add-appointment']);
  }

  editAppointment(id: number): void {
    this.router.navigate(['/edit-appointment', id]);
  }

  deleteAppointment(id: number): void {
    if (confirm('Are you sure you want to delete this appointment?')) {

      this.appointmentService.deleteAppointment(id)
        .subscribe({
          next: (res) => {
            if (res && typeof res === 'number') {
              this.toastr.success('Dleted successfully!', 'Success');
              this.paginatedAppointments = this.paginatedAppointments.filter(x => x.id !== res);
            }
          },
          error: (err) => {
            console.log(err);
            this.toastr.error('Failed', 'Error');
          }
        });

    }
  }

  prescriptionDetails(id: number): void {
    this.router.navigate(['/prescription-details', id]);
  }

  downloadPrescription(appointmentId: number) {
    this.prescriptionPrintService.downloadPrescriptionById(appointmentId);
  }

  sendEmail(id: number) {
    this.prescriptionService.sendEmail(id)
      .subscribe({
        next: (res) => {
          if (res) {
            this.toastr.success('Email Sent!', 'Success');
          }
        },
        error: (err) => {
          console.log(err);
          this.toastr.error('Failed', 'Error');
        }
      });
  }

}
