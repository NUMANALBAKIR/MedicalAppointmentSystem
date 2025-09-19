import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { MedicineDTO } from '../_models/medicineDTO';
import { PatientDTO } from '../_models/patientDTO';
import { DoctorDTO } from '../_models/doctorDTO';

@Injectable({
  providedIn: 'root'
})
export class DataService {

  private url!: string;

  constructor(private httpClient: HttpClient) {
    this.url = `${environment.apiUrl}/data`;
  }

  getMedicines(): Observable<MedicineDTO[]> {
    let fullUrl = this.url + '/medicines';
    return this.httpClient.get<MedicineDTO[]>(fullUrl, { responseType: 'json' });
  }

  getDoctors(): Observable<DoctorDTO[]> {
    let fullUrl = this.url + '/doctors';
    return this.httpClient.get<DoctorDTO[]>(fullUrl, { responseType: 'json' });
  }

  getPatients(): Observable<PatientDTO[]> {
    let fullUrl = this.url + '/patients';
    return this.httpClient.get<PatientDTO[]>(fullUrl, { responseType: 'json' });
  }


}
