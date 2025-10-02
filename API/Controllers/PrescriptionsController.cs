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
        private IPrescriptionsService _prescriptionsService;

        public PrescriptionsController(
            AppDbContext context,
            IPrescriptionsService prescriptionsService)
        {
            _context = context;
            _prescriptionsService = prescriptionsService;
        }

        [HttpPut]
        public async Task<IActionResult> UpdatePrescriptionDetails([FromBody] UpdatePrescriptionsDTO updateDto)
        {

            var result = await _prescriptionsService.UpdatePrescriptionDetails(updateDto);

            return Ok(true);
        }


        [HttpGet("sendEmail/appointmentId/{id}")]
        public async Task<IActionResult> SendAppointmentEmail(int id)
        {
            var result = await _prescriptionsService.SendAppointmentEmail(id);
            return Ok(new { result });
        }

    }
}
