import { Prescription } from "./prescription";

export interface Appointment {
  id: number;
  patient: string;
  doctor: string;
  date: string;
  visitType: 'First' | 'Follow-up';
  diagnosis: string;
  notes: string;
  prescriptions: Prescription[];
}