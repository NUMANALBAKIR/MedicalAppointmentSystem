using API.Data;
using API.DTOs;
using API.Entities;
using API.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.Net;
using System.Net.Mail;

namespace API.Services;

public class EmailService : IEmailService
{
    private readonly EmailSettings _emailSettings;
    private readonly AppDbContext _context;

    public EmailService(IOptions<EmailSettings> emailSettings, AppDbContext context)
    {
        _emailSettings = emailSettings.Value;
        _context = context;
    }

    public async Task<bool> SendEmailAsync(int appointmentId)
    {
        try
        {
            var appointment = await _context.Appointments
                .Include(a => a.Patient)
                .Include(a => a.Doctor)
                .Include(a => a.Prescriptions)
                .ThenInclude(p => p.Medicine)
                .FirstOrDefaultAsync(a => a.Id == appointmentId);

            if (appointment == null) return false;

            using var client = new SmtpClient(_emailSettings.SmtpHost, _emailSettings.SmtpPort);
            client.UseDefaultCredentials = false;
            client.Credentials = new NetworkCredential(_emailSettings.Username, _emailSettings.Password);
            client.EnableSsl = true;

            var mailMessage = new MailMessage
            {
                From = new MailAddress(_emailSettings.FromEmail, _emailSettings.FromName),
                Subject = $"Appointment Details - {appointment.AppointmentDate:MMM dd, yyyy}",
                Body = GenerateEmailBody(appointment),
                IsBodyHtml = true
            };

            mailMessage.To.Add(appointment.Patient.Email);
            await client.SendMailAsync(mailMessage);
            return true;
        }
        catch
        {
            return false;
        }
    }

    private string GenerateEmailBody(Appointment appointment)
    {
        var prescriptionsHtml = string.Join("", appointment.Prescriptions.Select(p =>
            $@"<tr>
                <td style='padding: 8px; border-bottom: 1px solid #ddd;'>{p.Medicine.Name}</td>
                <td style='padding: 8px; border-bottom: 1px solid #ddd;'>{p.Dosage}</td>
                <td style='padding: 8px; border-bottom: 1px solid #ddd;'>{p.StartDate:dd-MMM-yyyy}</td>
            <td style='padding: 8px; border-bottom: 1px solid #ddd;'>{p.EndDate:dd-MMM-yyyy}</td>
            </tr>"));

        return $@"
        <div style='font-family: Arial, sans-serif; max-width: 600px; margin: 0 auto;'>
            <h2 style='color: #333; border-bottom: 2px solid #007bff; padding-bottom: 10px;'>Prescription Report</h2>
            
            <div style='background: #f8f9fa; padding: 15px; margin: 20px 0; border-radius: 5px;'>
                <p><strong>Patient:</strong> {appointment.Patient.Name}</p>
                <p><strong>Doctor:</strong> {appointment.Doctor.Name}</p>
                <p><strong>Date:</strong> {appointment.AppointmentDate:dd-MMM-yyyy}</p>
                <p><strong>Visit Type:</strong> {appointment.VisitType}</p>
            </div>

            <h4 style='color: #333; margin-top: 30px;'>Prescriptions</h4>
            <table style='width: 100%; border-collapse: collapse; margin-top: 10px;'>
                <thead>
                    <tr style='background: #36454F; color: white;'>
                        <th style='padding: 10px; text-align: left;'>Medicine</th>
                        <th style='padding: 10px; text-align: left;'>Dosage</th>
                        <th style='padding: 10px; text-align: left;'>Start Date</th>
                        <th style='padding: 10px; text-align: left;'>End Date</th>
                    </tr>
                </thead>
                <tbody>
                    {prescriptionsHtml}
                </tbody>
            </table>
        </div>";
    }
}