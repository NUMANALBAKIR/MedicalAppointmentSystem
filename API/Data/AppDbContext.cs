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
            new Patient { Id = 2, Name = "Jane Smith", Email = "jane@email.com" }
        );

        modelBuilder.Entity<Doctor>().HasData(
            new Doctor { Id = 1, Name = "Dr. Smith" },
            new Doctor { Id = 2, Name = "Dr. Brown" }
        );

        modelBuilder.Entity<Medicine>().HasData(
            new Medicine { Id = 1, Name = "Paracetamol" },
            new Medicine { Id = 2, Name = "Metformin" },
            new Medicine { Id = 3, Name = "Amoxicillin" }
        );

        modelBuilder.Entity<Appointment>().HasData(
            new Appointment
            {
                Id = 1,
                PatientId = 1,
                DoctorId = 1,
                AppointmentDate = new DateTime(2024, 1, 15, 10, 0, 0),
                VisitType = "First Visit",
                Notes = "Patient complains of headache",
                Diagnosis = "Tension headache"
            },
            new Appointment
            {
                Id = 2,
                PatientId = 2,
                DoctorId = 2,
                AppointmentDate = new DateTime(2024, 1, 20, 14, 30, 0),
                VisitType = "Follow-up",
                Notes = "Blood pressure check",
                Diagnosis = "Hypertension - stable"
            },
            new Appointment
            {
                Id = 3,
                PatientId = 1,
                DoctorId = 1,
                AppointmentDate = new DateTime(2024, 2, 1, 9, 0, 0),
                VisitType = "Follow-up",
                Notes = "Follow-up for headache treatment",
                Diagnosis = "Improved condition"
            }
        );

        modelBuilder.Entity<PrescriptionDetail>().HasData(
            new PrescriptionDetail
            {
                Id = 1,
                AppointmentId = 1,
                MedicineId = 1,
                Dosage = "500mg twice daily",
                StartDate = new DateTime(2024, 1, 15),
                EndDate = new DateTime(2024, 1, 22),
                Notes = "Take with food"
            },
            new PrescriptionDetail
            {
                Id = 2,
                AppointmentId = 2,
                MedicineId = 2,
                Dosage = "500mg once daily",
                StartDate = new DateTime(2024, 1, 20),
                EndDate = new DateTime(2024, 4, 20),
                Notes = "Take before breakfast"
            },
            new PrescriptionDetail
            {
                Id = 3,
                AppointmentId = 1,
                MedicineId = 3,
                Dosage = "250mg three times daily",
                StartDate = new DateTime(2024, 1, 15),
                EndDate = new DateTime(2024, 1, 25),
                Notes = "Complete full course"
            }
        );
    }

}
