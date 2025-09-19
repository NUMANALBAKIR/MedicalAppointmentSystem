namespace API.DTOs;

public class UpdatePrescriptionsDTO
{
    public int AppointmentId { get; set; }
    public List<PrescriptionDetailDTO> Prescriptions { get; set; }
}
