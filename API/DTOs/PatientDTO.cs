namespace API.DTOs;

public class PatientDTO
{
    public int Id { get; set; }
    public string Name { get; set; }
}

public class DoctorDTO
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Specialty { get; set; }
}

public class MedicineDTO
{
    public int Id { get; set; }
    public string Name { get; set; }
}

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

public class UpsertAppointmentDTO
{
    public int PatientId { get; set; }
    public int DoctorId { get; set; }
    public string VisitType { get; set; }
    public string Notes { get; set; }
    public string Diagnosis { get; set; }
    public DateTime AppointmentDate { get; set; }
}

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
