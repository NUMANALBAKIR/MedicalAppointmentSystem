import { Injectable } from '@angular/core';
import { AppointmentDTO } from '../_models/appointmentDTO';
import { BehaviorSubject, map, Observable } from 'rxjs';
import { HttpClient, HttpParams } from '@angular/common/http';
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

  getAppointmentById(id: number): Observable<AppointmentDTO> {
    const fullUrl = `${this.url}/${id}`;
    return this.httpClient.get<AppointmentDTO>(fullUrl);
  }

  getAppointments(
    page: number = 1,
    pageSize: number = 2,
    search?: string,
    doctorFilter?: string,
    visitTypeFilter?: string
  ): Observable<any> {
    let params = new HttpParams()
      .set('page', page.toString())
      .set('pageSize', pageSize.toString());

    if (search && search.trim()) {
      params = params.set('search', search.trim());
    }
    if (doctorFilter && doctorFilter.trim()) {
      params = params.set('doctorFilter', doctorFilter.trim());
    }
    if (visitTypeFilter && visitTypeFilter.trim()) {
      params = params.set('visitTypeFilter', visitTypeFilter.trim());
    }

    return this.httpClient.get(this.url, { params });
  }

}