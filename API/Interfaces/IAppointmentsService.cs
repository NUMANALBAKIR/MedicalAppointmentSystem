using API.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace API.Interfaces
{
    public interface IAppointmentsService
    {
        Task<bool> CreateAppointment(UpsertAppointmentDTO createDto);
        Task<bool> DeleteAppointment(int id);
        Task<AppointmentDTO> GetAppointment(int id);
        Task<object> GetAppointments(int page = 1, int pageSize = 2, string? search = null, string? doctorFilter = null, string? visitTypeFilter = null);
        Task<bool> UpdateAppointment(int id, UpsertAppointmentDTO updateDto);
    }
}