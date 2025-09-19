namespace API.Entities;

public class Appointment
{
    public int Id { get; set; }
    public int PatientId { get; set; }
    public Patient Patient { get; set; }

    public int DoctorId { get; set; }
    public Doctor Doctor { get; set; }

    public DateTime AppointmentDate { get; set; }
    public string VisitType { get; set; }
    public string Notes { get; set; }
    public string Diagnosis { get; set; }

    public List<PrescriptionDetail> Prescriptions { get; set; } = new();
}
