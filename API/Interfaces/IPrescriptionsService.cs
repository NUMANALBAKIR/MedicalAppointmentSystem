using API.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace API.Interfaces
{
    public interface IPrescriptionsService
    {
        Task<bool> SendAppointmentEmail(int id);
        Task<bool> UpdatePrescriptionDetails(UpdatePrescriptionsDTO updateDto);
    }
}