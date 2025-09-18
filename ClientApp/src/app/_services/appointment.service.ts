import { Injectable } from '@angular/core';
import { Appointment } from '../_models/appointment';
import { BehaviorSubject, Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class AppointmentService {
  private appointments: Appointment[] = [
    {
      id: 1,
      patient: 'John Doe',
      doctor: 'Dr. Smith',
      date: '2025-08-20',
      visitType: 'First',
      diagnosis: 'Fever',
      notes: 'Patient complains of high fever',
      prescriptions: [
        { medicine: 'Paracetamol', dosage: '500mg 2x/day', startDate: '2025-08-20', endDate: '2025-08-25', notes: 'Take after meals' },
        { medicine: 'Amoxicillin', dosage: '250mg 3x/day', startDate: '2025-08-20', endDate: '2025-08-27', notes: 'Before meal' }
      ]
    },
    {
      id: 2,
      patient: 'Jane Smith',
      doctor: 'Dr. Brown',
      date: '2025-08-21',
      visitType: 'Follow-up',
      diagnosis: 'Diabetes',
      notes: 'Regular checkup',
      prescriptions: [
        { medicine: 'Metformin', dosage: '850mg 1x/day', startDate: '2025-08-21', endDate: '2025-09-21', notes: 'Morning dose' }
      ]
    }
  ];

  private appointmentsSubject = new BehaviorSubject<Appointment[]>(this.appointments);
  public appointments$ = this.appointmentsSubject.asObservable();

  patients = ['John Doe', 'Jane Smith', 'Bob Johnson'];
  doctors = ['Dr. Smith', 'Dr. Brown', 'Dr. Johnson'];
  medicines = ['Paracetamol', 'Amoxicillin', 'Metformin', 'Aspirin', 'Ibuprofen', 'Omeprazole'];

  constructor() { }

  getAppointments(): Observable<Appointment[]> {
    return this.appointments$;
  }

  getAppointmentById(id: number): Appointment | undefined {
    return this.appointments.find(a => a.id === id);
  }

  createAppointment(appointment: Omit<Appointment, 'id'>): void {
    const newAppointment: Appointment = {
      ...appointment,
      id: this.getNextId()
    };
    this.appointments.push(newAppointment);
    this.appointmentsSubject.next([...this.appointments]);
  }

  updateAppointment(id: number, appointment: Omit<Appointment, 'id'>): void {
    const index = this.appointments.findIndex(a => a.id === id);
    if (index !== -1) {
      this.appointments[index] = { ...appointment, id };
      this.appointmentsSubject.next([...this.appointments]);
    }
  }

  deleteAppointment(id: number): void {
    this.appointments = this.appointments.filter(a => a.id !== id);
    this.appointmentsSubject.next([...this.appointments]);
  }

  private getNextId(): number {
    return this.appointments.length > 0 ? Math.max(...this.appointments.map(a => a.id)) + 1 : 1;
  }
}