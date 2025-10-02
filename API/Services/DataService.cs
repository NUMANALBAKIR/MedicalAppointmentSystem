using API.Data;
using API.DTOs;
using API.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace API.Services;

public class DataService : IDataService
{
    private readonly AppDbContext _context;
    public DataService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<PatientDTO>> GetPatients()
    {
        var patients = await _context.Patients
            .Select(p => new PatientDTO { Id = p.Id, Name = p.Name })
            .ToListAsync();

        return patients;
    }

    public async Task<List<DoctorDTO>> GetDoctors()
    {
        var doctors = await _context.Doctors
            .Select(d => new DoctorDTO { Id = d.Id, Name = d.Name })
            .ToListAsync();

        return doctors;
    }

    public async Task<List<MedicineDTO>> GetMedicines()
    {
        var medicines = await _context.Medicines
            .Select(m => new MedicineDTO { Id = m.Id, Name = m.Name })
            .ToListAsync();

        return medicines;
    }

}
