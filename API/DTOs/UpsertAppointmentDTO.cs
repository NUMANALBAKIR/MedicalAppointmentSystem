namespace API.DTOs;

public class UpsertAppointmentDTO
{
    public int PatientId { get; set; }
    public int DoctorId { get; set; }
    public string VisitType { get; set; }
    public string Notes { get; set; }
    public string Diagnosis { get; set; }
    public DateTime AppointmentDate { get; set; }
}
