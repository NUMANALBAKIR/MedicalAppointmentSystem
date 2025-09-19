namespace API.Entities;

public class Medicine
{
    public int Id { get; set; }
    public string Name { get; set; }
    public List<PrescriptionDetail> Prescriptions { get; set; } = new();
}
