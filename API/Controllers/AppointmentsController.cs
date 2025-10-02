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
    public class AppointmentsController : ControllerBase
    {
        private readonly AppDbContext _context;
        private IAppointmentsService _appointmentsService;

        public AppointmentsController(AppDbContext context, IAppointmentsService appointmentsService)
        {
            _context = context;
            _appointmentsService = appointmentsService;
        }


        [HttpGet]
        public async Task<ActionResult<object>> GetAppointments(
            [FromQuery] int page = 1,
            [FromQuery] int pageSize = 2,
            [FromQuery] string? search = null,
            [FromQuery] string? doctorFilter = null,
            [FromQuery] string? visitTypeFilter = null)
            {
            var result = await _appointmentsService.GetAppointments(page, pageSize, search, doctorFilter, visitTypeFilter);
            return Ok(result);
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<AppointmentDTO>> GetAppointment(int id)
        {
            var result = await _appointmentsService.GetAppointment(id);
            return Ok(result);
        }


        [HttpPost]
        public async Task<IActionResult> CreateAppointment([FromBody] UpsertAppointmentDTO createDto)
        {
            var result = await _appointmentsService.CreateAppointment(createDto);
            return Ok(result);
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAppointment(int id, [FromBody] UpsertAppointmentDTO updateDto)
        {
            var result = await _appointmentsService.UpdateAppointment(id, updateDto);
            return Ok(result);
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAppointment(int id)
        {
            var result = await _appointmentsService.DeleteAppointment(id);
            return Ok(result);
        }


    }
}
