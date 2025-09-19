import { PrescriptionDetailDTO } from "./prescriptionDetailDTO";


export interface UpdatePrescriptionsDTO {
    appointmentId: number;
    prescriptions: PrescriptionDetailDTO[];
}
