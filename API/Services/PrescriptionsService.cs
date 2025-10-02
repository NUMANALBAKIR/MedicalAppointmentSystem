using API.Data;
using API.DTOs;
using API.Entities;
using API.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace API.Services;

public class PrescriptionsService : IPrescriptionsService
{
    private readonly AppDbContext _context;
    private IEmailService _emailService;

    public PrescriptionsService(AppDbContext context, IEmailService emailService)
    {
        _context = context;
        _emailService = emailService;
    }

    public async Task<bool> UpdatePrescriptionDetails(UpdatePrescriptionsDTO updateDto)
    {
        var appointmentId = updateDto.AppointmentId;

        var itemsToDelete = await _context.PrescriptionDetails
            .Where(x => x.AppointmentId == appointmentId)
            .ToListAsync();

        _context.PrescriptionDetails.RemoveRange(itemsToDelete);
        await _context.SaveChangesAsync();

        foreach (var dto in updateDto.Prescriptions)
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

        return true;
    }


    public async Task<bool> SendAppointmentEmail(int id)
    {
        var result = await _emailService.SendEmailAsync(id);
        return result;
    }

}
