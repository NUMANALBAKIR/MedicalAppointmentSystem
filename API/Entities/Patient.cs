namespace API.Entities;

public class Patient
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public List<Appointment> Appointments { get; set; } = new();
}


public class Doctor
{
    public int Id { get; set; }
    public string Name { get; set; }
    public List<Appointment> Appointments { get; set; } = new();
}

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

public class Medicine
{
    public int Id { get; set; }
    public string Name { get; set; }
    public List<PrescriptionDetail> Prescriptions { get; set; } = new();
}

public class PrescriptionDetail
{
    public int Id { get; set; }

    public int AppointmentId { get; set; }
    public Appointment Appointment { get; set; }

    public int MedicineId { get; set; }
    public Medicine Medicine { get; set; }

    public string Dosage { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public string Notes { get; set; }
}