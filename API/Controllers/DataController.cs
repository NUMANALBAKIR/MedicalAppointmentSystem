using API.Data;
using API.DTOs;
using API.Entities;
using API.Interfaces;
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
        private IDataService _dataService;

        public DataController(AppDbContext context, IDataService dataService)
        {
            _context = context;
            _dataService = dataService;
        }

        [HttpGet("patients")]
        public async Task<ActionResult<List<PatientDTO>>> GetPatients()
        {
            var patients = await _dataService.GetPatients();

            if (patients == null)
            {
                return BadRequest("No patients found.");
            }

            return Ok(patients);
        }

        [HttpGet("doctors")]
        public async Task<ActionResult<List<DoctorDTO>>> GetDoctors()
        {
            var doctors = await _dataService.GetDoctors();

            if (doctors == null)
            {
                return BadRequest("No doctors found.");
            }

            return Ok(doctors);
        }

        [HttpGet("medicines")]
        public async Task<ActionResult<List<MedicineDTO>>> GetMedicines()
        {
            var medicines = await _dataService.GetMedicines();

            if (medicines == null)
            {
                return BadRequest("No medicines found.");
            }

            return Ok(medicines);
        }

    }
}
