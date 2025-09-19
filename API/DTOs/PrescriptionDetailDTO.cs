namespace API.DTOs;

public class PrescriptionDetailDTO
{
    public int Id { get; set; }
    public int AppointmentId { get; set; }
    public int MedicineId { get; set; }
    public string MedicineName { get; set; }
    public string Dosage { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public string Notes { get; set; }
}
