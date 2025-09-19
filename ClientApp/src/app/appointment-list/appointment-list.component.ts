import { AppointmentDTO } from '../_models/appointmentDTO';
import { Component, OnInit, OnDestroy } from '@angular/core';
import { Router } from '@angular/router';
import { Observable, Subject, takeUntil } from 'rxjs';
import { AppointmentService } from '../_services/appointment.service';
import { DoctorDTO } from '../_models/doctorDTO';
import { DataService } from '../_services/data.service';

@Component({
  selector: 'app-appointment-list',
  templateUrl: './appointment-list.component.html',
  styleUrls: ['./appointment-list.component.css']
})
export class AppointmentListComponent implements OnInit {

  allDoctor!: Observable<DoctorDTO[]>;
  appointments: AppointmentDTO[] = [];
  filteredAppointments: AppointmentDTO[] = [];
  paginatedAppointments: AppointmentDTO[] = [];

  searchTerm = '';
  doctorFilter = '';
  visitTypeFilter = '';

  currentPage = 1;
  itemsPerPage = 10;
  totalPages = 0;

  private destroy$ = new Subject<void>();

  constructor(
    public appointmentService: AppointmentService,
    public dataService: DataService,
    private router: Router
  ) {

  }

  ngOnInit(): void {
    this.allDoctor = this.dataService.getDoctors();

    this.appointmentService.getAppointments()
      .pipe(takeUntil(this.destroy$))
      .subscribe(appointments => {
        this.appointments = appointments;
        this.applyFilters();
      });
  }

  ngOnDestroy(): void {
    this.destroy$.next();
    this.destroy$.complete();
  }

  navigateToAdd(): void {
    this.router.navigate(['/add-appointment']);
  }

  editAppointment(id: number): void {
    this.router.navigate(['/edit-appointment', id]);
  }

  deleteAppointment(id: number): void {
    if (confirm('Are you sure you want to delete this appointment?')) {
      this.appointmentService.deleteAppointment(id);
    }
  }

  downloadPrescription(appointment: AppointmentDTO): void {
    const prescriptionModal = document.getElementById('prescriptionModal') as any;
    if (prescriptionModal && prescriptionModal.show) {
      prescriptionModal.show(appointment);
    }
  }

  applyFilters(): void {

    this.filteredAppointments = this.appointments.filter(appointment => {
      const matchesSearch = !this.searchTerm ||
        appointment.patientName.toLowerCase().includes(this.searchTerm.toLowerCase()) ||
        appointment.doctorName.toLowerCase().includes(this.searchTerm.toLowerCase());

      const matchesDoctor = !this.doctorFilter || appointment.doctorName === this.doctorFilter;
      const matchesVisitType = !this.visitTypeFilter || appointment.visitType === this.visitTypeFilter;

      return matchesSearch && matchesDoctor && matchesVisitType;
    }
    );

    this.totalPages = Math.ceil(this.filteredAppointments.length / this.itemsPerPage);
    this.currentPage = 1;
    this.updatePagination();
  }

  changePage(event: Event, page: number): void {
    event.preventDefault();
    this.currentPage = page;
    this.updatePagination();
  }

  updatePagination(): void {
    const start = (this.currentPage - 1) * this.itemsPerPage;
    const end = start + this.itemsPerPage;
    this.paginatedAppointments = this.filteredAppointments.slice(start, end);
  }

  getPageNumbers(): number[] {
    return Array.from({ length: this.totalPages }, (_, i) => i + 1);
  }

  prescriptionDetails(id: number): void {
    this.router.navigate(['/prescription-details', id]);
  }

}