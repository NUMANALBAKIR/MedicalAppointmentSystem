using API.Data;
using API.DTOs;
using API.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PrescriptionsController : ControllerBase
    {
        private readonly AppDbContext _context;
        public PrescriptionsController(AppDbContext context)
        {
            _context = context;
        }


        [HttpPut]
        public async Task<IActionResult> UpdatePrescriptionDetails([FromBody] List<PrescriptionDetailDTO> prescriptionDetailDTOs)
        {
            foreach (var dto in prescriptionDetailDTOs)
            {
                // Add new
                if (dto.Id == 0)
                {
                    var detail = new PrescriptionDetail
                    {
                        AppointmentId = dto.AppointmentId,
                        MedicineId = dto.MedicineId,
                        Dosage = dto.Dosage,
                        StartDate = dto.StartDate,
                        EndDate = dto.EndDate,
                        Notes = dto.Notes
                    };

                    _context.PrescriptionDetails.Add(detail);
                }
                // Update existing
                else
                {
                    var existing = _context.PrescriptionDetails.Find(dto.Id);
                    if (existing != null)
                    {
                        existing.AppointmentId = dto.AppointmentId;
                        existing.MedicineId = dto.MedicineId;
                        existing.Dosage = dto.Dosage;
                        existing.StartDate = dto.StartDate;
                        existing.EndDate = dto.EndDate;
                        existing.Notes = dto.Notes;
                    }
                }
            }
            await _context.SaveChangesAsync();
            return Ok(true);
        }
    }
}
