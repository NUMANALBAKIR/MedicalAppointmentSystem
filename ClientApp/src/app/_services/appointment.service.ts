import { Injectable } from '@angular/core';
import { AppointmentDTO } from '../_models/appointmentDTO';
import { BehaviorSubject, map, Observable } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { DoctorDTO } from '../_models/doctorDTO';
import { PatientDTO } from '../_models/patientDTO';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class AppointmentService {

  private url!: string;
  // private selectedAppointment!: AppointmentDTO;

  constructor(private httpClient: HttpClient) {
    this.url = `${environment.apiUrl}/appointments`;
  }

  // setAppointment(appointment: AppointmentDTO) {
  //   this.selectedAppointment = appointment;
  // }

  // getAppointment(): AppointmentDTO {
  //   return this.selectedAppointment;
  // }


  createAppointment(appointmentDTO: AppointmentDTO) {
    return this.httpClient.post(this.url, appointmentDTO, { responseType: 'json' });
  }

  updateAppointment(id: number, appointmentDTO: AppointmentDTO) {
    let fullUrl = this.url + `/${id}`;
    return this.httpClient.put(fullUrl, appointmentDTO, { responseType: 'json' });
  }

  deleteAppointment(id: number) {
    let fullUrl = this.url + `/${id}`;
    return this.httpClient.delete(fullUrl, { responseType: 'json' });
  }

  //------------

  private appointments: AppointmentDTO[] = [
    {
      id: 1,
      patientName: 'John Doe',
      doctorName: 'Dr. Smith',
      appointmentDate: '2025-08-20',
      visitType: 'First',
      diagnosis: 'Fever',
      notes: 'Patient complains of high fever',
      doctorId: 1,
      patientId: 1,
      prescriptions: []
    },
    {
      id: 2,
      patientName: 'Jane Smith',
      doctorName: 'Dr. Brown',
      appointmentDate: '2025-08-21',
      visitType: 'Follow-up',
      diagnosis: 'Diabetes',
      notes: 'Regular checkup',
      doctorId: 1,
      patientId: 1,
      prescriptions: []
    }
  ];

  private appointmentsSubject = new BehaviorSubject<AppointmentDTO[]>(this.appointments);
  public appointments$ = this.appointmentsSubject.asObservable();

  patients = ['John Doe', 'Jane Smith', 'Bob Johnson'];
  doctors = ['Dr. Smith', 'Dr. Brown', 'Dr. Johnson'];
  medicines = ['Paracetamol', 'Amoxicillin', 'Metformin', 'Aspirin', 'Ibuprofen', 'Omeprazole'];


  getAppointments(): Observable<AppointmentDTO[]> {
    return this.appointments$;
  }

  // getAppointmentById(id: number) {
  //   let fullUrl = `${this.url}/${id}}`;
  //   return this.httpClient.get<AppointmentDTO>(fullUrl, { responseType: 'json' })
  //     .pipe(map(
  //       (data) => {
  //         return data;
  //       }
  //     ));
  // }

  getAppointmentById(id: number): Observable<AppointmentDTO> {
    const fullUrl = `${this.url}/${id}`;
    return this.httpClient.get<AppointmentDTO>(fullUrl);
  }

  //--


  private getNextId(): number {
    return this.appointments.length > 0 ? Math.max(...this.appointments.map(a => a.id)) + 1 : 1;
  }
}