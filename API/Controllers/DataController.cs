using API.Data;
using API.DTOs;
using API.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DataController : ControllerBase
    {
        private readonly AppDbContext _context;

        public DataController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet("patients")]
        public async Task<ActionResult<List<PatientDTO>>> GetPatients()
        {
            var patients = await _context.Patients
                .Select(p => new PatientDTO { Id = p.Id, Name = p.Name })
                .ToListAsync();
            return Ok(patients);
        }

        [HttpGet("doctors")]
        public async Task<ActionResult<List<DoctorDTO>>> GetDoctors()
        {
            var doctors = await _context.Doctors
                .Select(d => new DoctorDTO { Id = d.Id, Name = d.Name })
                .ToListAsync();
            return Ok(doctors);
        }

        [HttpGet("medicines")]
        public async Task<ActionResult<List<MedicineDTO>>> GetMedicines()
        {
            var medicines = await _context.Medicines
                .Select(m => new MedicineDTO { Id = m.Id, Name = m.Name })
                .ToListAsync();
            return Ok(medicines);
        }

    }
}
