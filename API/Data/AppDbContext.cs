using API.Entities;
using Microsoft.EntityFrameworkCore;

namespace API.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Patient> Patients { get; set; }
    public DbSet<Doctor> Doctors { get; set; }
    public DbSet<Appointment> Appointments { get; set; }
    public DbSet<Medicine> Medicines { get; set; }
    public DbSet<PrescriptionDetail> PrescriptionDetails { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Patient>().HasData(
            new Patient { Id = 1, Name = "John Doe", Email = "john@email.com" },
            new Patient { Id = 2, Name = "Jane Smith", Email = "jane@email.com" },
            new Patient { Id = 3, Name = "Alice Johnson", Email = "alice@email.com" },
            new Patient { Id = 4, Name = "Bob Lee", Email = "bob@email.com" },
            new Patient { Id = 5, Name = "Clara Oswald", Email = "clara@email.com" },
            new Patient { Id = 6, Name = "Numan", Email = "david@email.com" }
        );

        modelBuilder.Entity<Doctor>().HasData(
            new Doctor { Id = 1, Name = "Dr. Smith" },
            new Doctor { Id = 2, Name = "Dr. Brown" },
            new Doctor { Id = 3, Name = "Dr. Taylor" },
            new Doctor { Id = 4, Name = "Dr. Wilson" },
            new Doctor { Id = 5, Name = "Dr. Carter" },
            new Doctor { Id = 6, Name = "Dr. Adams" }
        );

        modelBuilder.Entity<Medicine>().HasData(
            new Medicine { Id = 1, Name = "Paracetamol" },
            new Medicine { Id = 2, Name = "Metformin" },
            new Medicine { Id = 3, Name = "Amoxicillin" },
            new Medicine { Id = 4, Name = "Ibuprofen" },
            new Medicine { Id = 5, Name = "Atorvastatin" },
            new Medicine { Id = 6, Name = "Omeprazole" },
            new Medicine { Id = 7, Name = "Losartan" },
            new Medicine { Id = 8, Name = "Levothyroxine" },
            new Medicine { Id = 9, Name = "Azithromycin" }
        );

        modelBuilder.Entity<Appointment>().HasData(
            new Appointment { Id = 1, PatientId = 1, DoctorId = 1, AppointmentDate = new DateTime(2024, 1, 15, 10, 0, 0), VisitType = "First Visit", Notes = "Headache", Diagnosis = "Tension headache" },
            new Appointment { Id = 2, PatientId = 2, DoctorId = 2, AppointmentDate = new DateTime(2024, 1, 20, 14, 30, 0), VisitType = "Follow-up", Notes = "BP check", Diagnosis = "Hypertension" },
            new Appointment { Id = 3, PatientId = 1, DoctorId = 1, AppointmentDate = new DateTime(2024, 2, 1, 9, 0, 0), VisitType = "Follow-up", Notes = "Headache follow-up", Diagnosis = "Improved" },
            new Appointment { Id = 4, PatientId = 3, DoctorId = 3, AppointmentDate = new DateTime(2024, 2, 10, 11, 0, 0), VisitType = "First Visit", Notes = "Cough", Diagnosis = "Bronchitis" },
            new Appointment { Id = 5, PatientId = 4, DoctorId = 4, AppointmentDate = new DateTime(2024, 2, 15, 13, 0, 0), VisitType = "Follow-up", Notes = "Annual checkup", Diagnosis = "Healthy" },
            new Appointment { Id = 6, PatientId = 5, DoctorId = 5, AppointmentDate = new DateTime(2024, 3, 1, 10, 30, 0), VisitType = "First Visit", Notes = "Stomach pain", Diagnosis = "Gastritis" },
            new Appointment { Id = 7, PatientId = 6, DoctorId = 6, AppointmentDate = new DateTime(2024, 3, 5, 15, 0, 0), VisitType = "Follow-up", Notes = "Thyroid check", Diagnosis = "Hypothyroidism" },
            new Appointment { Id = 8, PatientId = 3, DoctorId = 2, AppointmentDate = new DateTime(2024, 3, 10, 9, 0, 0), VisitType = "Follow-up", Notes = "Cough persists", Diagnosis = "Improving" },
            new Appointment { Id = 9, PatientId = 4, DoctorId = 1, AppointmentDate = new DateTime(2024, 3, 15, 14, 0, 0), VisitType = "First Visit", Notes = "Cholesterol review", Diagnosis = "High cholesterol" }
        );

        modelBuilder.Entity<PrescriptionDetail>().HasData(
            new PrescriptionDetail { Id = 1, AppointmentId = 1, MedicineId = 1, Dosage = "500mg twice daily", StartDate = new DateTime(2024, 1, 15), EndDate = new DateTime(2024, 1, 22), Notes = "With food" },
            new PrescriptionDetail { Id = 2, AppointmentId = 2, MedicineId = 2, Dosage = "500mg once daily", StartDate = new DateTime(2024, 1, 20), EndDate = new DateTime(2024, 4, 20), Notes = "Before breakfast" },
            new PrescriptionDetail { Id = 3, AppointmentId = 1, MedicineId = 3, Dosage = "250mg three times daily", StartDate = new DateTime(2024, 1, 15), EndDate = new DateTime(2024, 1, 25), Notes = "Full course" },
            new PrescriptionDetail { Id = 4, AppointmentId = 4, MedicineId = 4, Dosage = "200mg twice daily", StartDate = new DateTime(2024, 2, 10), EndDate = new DateTime(2024, 2, 17), Notes = "After meals" },
            new PrescriptionDetail { Id = 5, AppointmentId = 5, MedicineId = 5, Dosage = "10mg daily", StartDate = new DateTime(2024, 2, 15), EndDate = new DateTime(2024, 8, 15), Notes = "Monitor cholesterol" },
            new PrescriptionDetail { Id = 6, AppointmentId = 6, MedicineId = 6, Dosage = "20mg daily", StartDate = new DateTime(2024, 3, 1), EndDate = new DateTime(2024, 3, 10), Notes = "Before meals" },
            new PrescriptionDetail { Id = 7, AppointmentId = 7, MedicineId = 8, Dosage = "100mcg daily", StartDate = new DateTime(2024, 3, 5), EndDate = new DateTime(2024, 6, 5), Notes = "Morning dose" },
            new PrescriptionDetail { Id = 8, AppointmentId = 8, MedicineId = 9, Dosage = "500mg once daily", StartDate = new DateTime(2024, 3, 10), EndDate = new DateTime(2024, 3, 17), Notes = "With water" },
            new PrescriptionDetail { Id = 9, AppointmentId = 9, MedicineId = 5, Dosage = "10mg daily", StartDate = new DateTime(2024, 3, 15), EndDate = new DateTime(2024, 9, 15), Notes = "Monitor lipid profile" },
            new PrescriptionDetail { Id = 10, AppointmentId = 4, MedicineId = 3, Dosage = "250mg twice daily", StartDate = new DateTime(2024, 2, 10), EndDate = new DateTime(2024, 2, 20), Notes = "Complete course" },
            new PrescriptionDetail { Id = 11, AppointmentId = 6, MedicineId = 6, Dosage = "20mg daily", StartDate = new DateTime(2024, 3, 1), EndDate = new DateTime(2024, 3, 10), Notes = "Before meals" },
            new PrescriptionDetail { Id = 12, AppointmentId = 7, MedicineId = 8, Dosage = "100mcg daily", StartDate = new DateTime(2024, 3, 5), EndDate = new DateTime(2024, 6, 5), Notes = "Morning dose" }
        );
    }


}
