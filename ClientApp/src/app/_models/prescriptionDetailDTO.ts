export interface PrescriptionDetailDTO {
  id: number;

  appointmentId: number;
  medicineId: number;
  medicineName: string;

  dosage: string;
  startDate: string;
  endDate: string;
  notes: string;
}
