using Microsoft.AspNetCore.Mvc;

namespace API.Interfaces
{
    public interface IEmailService
    {
        Task<bool> SendEmailAsync(int appointmentId);
    }
}
