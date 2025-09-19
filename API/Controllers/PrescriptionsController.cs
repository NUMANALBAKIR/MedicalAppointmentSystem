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
    public class PrescriptionsController : ControllerBase
    {
        private readonly AppDbContext _context;
        private IEmailService _emailService;
        public PrescriptionsController(AppDbContext context, IEmailService emailService)
        {
            _context = context;
            _emailService = emailService;
        }


        [HttpPut]
        public async Task<IActionResult> UpdatePrescriptionDetails([FromBody] List<PrescriptionDetailDTO> prescriptionDetailDTOs)
        {

            var itemsToDelete = await _context.PrescriptionDetails
                .ToListAsync();

            _context.PrescriptionDetails.RemoveRange(itemsToDelete);
            await _context.SaveChangesAsync();

            foreach (var dto in prescriptionDetailDTOs)
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
            await _context.SaveChangesAsync();
            return Ok(true);
        }


        [HttpGet("sendEmail/appointmentId/{id}")]
        public async Task<IActionResult> SendAppointmentEmail(int id)
        {
            var result = await _emailService.SendEmailAsync(id);
            return Ok(new { success = result });
        }

    }
}
