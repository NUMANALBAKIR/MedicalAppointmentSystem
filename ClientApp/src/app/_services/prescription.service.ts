import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { PrescriptionDetailDTO } from '../_models/prescriptionDetailDTO';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class PrescriptionService {
  private url!: string;

  constructor(private httpClient: HttpClient) {
    this.url = `${environment.apiUrl}/prescriptions`;
  }

  updatePrescriptions(prescriptionDetails: PrescriptionDetailDTO[]) {
    return this.httpClient.put(this.url, prescriptionDetails, { responseType: 'json' });
  }

  // updatePrescriptions(prescriptions: PrescriptionDetailDTO[]): Observable<any> {
  //     let apiUrl = 'https://localhost:5500/api/prescriptions'; // adjust
  //   return this.httpClient.put(`${apiUrl}/update`, prescriptions);
  // }

}
