namespace API.Entities;

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