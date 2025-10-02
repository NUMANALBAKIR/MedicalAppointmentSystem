using API.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace API.Interfaces
{
    public interface IAppointmentsService
    {
        Task<bool> CreateAppointment(UpsertAppointmentDTO createDto);
        Task<bool> DeleteAppointment(int id);
        Task<AppointmentDTO> GetAppointment(int id);
        Task<object> GetAppointments(int page, int pageSize, string? search, string? doctorFilter, string? visitTypeFilter);
        Task<bool> UpdateAppointment(int id, UpsertAppointmentDTO updateDto);
    }
}