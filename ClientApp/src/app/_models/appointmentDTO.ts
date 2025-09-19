import { PrescriptionDetailDTO } from "./prescriptionDetailDTO";

export interface AppointmentDTO {
  id: number;

  patientId: number;
  patientName: string;

  doctorId: number;
  doctorName: string;

  appointmentDate: string;
  visitType: string;
  notes: string;
  diagnosis: string;

  prescriptions: PrescriptionDetailDTO[];
}
