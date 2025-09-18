import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AppointmentListComponent } from './appointment-list/appointment-list.component';
import { AppointmentFormComponent } from './appointment-form/appointment-form.component';
import { PrescriptionDetailsComponent } from './prescription-details/prescription-details.component';

const routes: Routes = [
  { path: '', redirectTo: '/appointment-list', pathMatch: 'full' },
  { path: 'appointment-list', component: AppointmentListComponent },
  { path: 'add-appointment', component: AppointmentFormComponent },
  { path: 'edit-appointment/:id', component: AppointmentFormComponent },
  { path: 'prescription-details/:id', component: PrescriptionDetailsComponent }
];


@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
