import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { Observable } from 'rxjs';
import { UpdatePrescriptionsDTO } from '../_models/UpdatePrescriptionsDTO';

@Injectable({
  providedIn: 'root'
})
export class PrescriptionService {
  private url: string;

  constructor(private httpClient: HttpClient) {
    this.url = `${environment.apiUrl}/prescriptions`;
  }

  updatePrescriptions(updatePrescriptions: UpdatePrescriptionsDTO) {
    return this.httpClient.put(this.url, updatePrescriptions, { responseType: 'json' });
  }

  sendEmail(id: number): Observable<boolean> {
    const fullUrl = `${this.url}/sendEmail/appointmentId/${id}`;
    return this.httpClient.get<boolean>(fullUrl);
  }
}
