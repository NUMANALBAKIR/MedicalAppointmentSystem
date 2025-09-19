namespace API.Entities;

public class Doctor
{
    public int Id { get; set; }
    public string Name { get; set; }
    public List<Appointment> Appointments { get; set; } = new();
}
