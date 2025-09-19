namespace API.DTOs;

public class AppointmentDTO
{
    public int Id { get; set; }
    public int PatientId { get; set; }
    public string PatientName { get; set; }
    public int DoctorId { get; set; }
    public string DoctorName { get; set; }
    public DateTime AppointmentDate { get; set; }
    public string VisitType { get; set; }
    public string Notes { get; set; }
    public string Diagnosis { get; set; }
    public List<PrescriptionDetailDTO> Prescriptions { get; set; } = new();
}
