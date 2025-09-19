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

  sendEmail(id: number): Observable<boolean> {
    const fullUrl = `${this.url}/sendEmail/appointmentId/${id}`;
    return this.httpClient.get<boolean>(fullUrl);
  }
}
