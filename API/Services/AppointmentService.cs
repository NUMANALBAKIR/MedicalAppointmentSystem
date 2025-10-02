using API.Data;
using API.DTOs;
using API.Entities;
using API.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace API.Services;

public class AppointmentService : IAppointmentsService
{
    private readonly AppDbContext _context;
    public AppointmentService(AppDbContext context)
    {
        _context = context;

    }

    public async Task<object> GetAppointments(
        int page = 1,
        int pageSize = 2,
        string? search = null,
        string? doctorFilter = null,
        string? visitTypeFilter = null)
    {
        var query = _context.Appointments
            .Include(a => a.Patient)
            .Include(a => a.Doctor)
            .AsQueryable();

        // Apply filters
        if (!string.IsNullOrEmpty(search))
        {
            query = query.Where(a => a.Patient.Name.Contains(search) || a.Doctor.Name.Contains(search));
        }

        if (!string.IsNullOrEmpty(doctorFilter))
        {
            query = query.Where(a => a.Doctor.Name.Contains(doctorFilter));
        }

        if (!string.IsNullOrEmpty(visitTypeFilter))
        {
            query = query.Where(a => a.VisitType == visitTypeFilter);
        }

        var totalItems = await query.CountAsync();
        var totalPages = (int)Math.Ceiling(totalItems / (double)pageSize);

        var appointments = await query
            .OrderByDescending(a => a.AppointmentDate)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .Select(a => new AppointmentDTO
            {
                Id = a.Id,
                PatientId = a.PatientId,
                PatientName = a.Patient.Name,
                DoctorId = a.DoctorId,
                DoctorName = a.Doctor.Name,
                AppointmentDate = a.AppointmentDate,
                VisitType = a.VisitType,
                Notes = a.Notes,
                Diagnosis = a.Diagnosis
            })
            .ToListAsync();

        return new
        {
            Data = appointments,
            TotalItems = totalItems,
            TotalPages = totalPages,
            CurrentPage = page,
            PageSize = pageSize
        };
    }


    public async Task<AppointmentDTO> GetAppointment(int id)
    {
        var appointment = await _context.Appointments
            .Include(a => a.Patient)
            .Include(a => a.Doctor)
            .Include(a => a.Prescriptions)
            .ThenInclude(p => p.Medicine)
            .FirstOrDefaultAsync(a => a.Id == id);

        if (appointment == null)
            return null;

        var appointmentDto = new AppointmentDTO
        {
            Id = appointment.Id,
            PatientId = appointment.PatientId,
            PatientName = appointment.Patient.Name,
            DoctorId = appointment.DoctorId,
            DoctorName = appointment.Doctor.Name,
            AppointmentDate = appointment.AppointmentDate,
            VisitType = appointment.VisitType,
            Notes = appointment.Notes,
            Diagnosis = appointment.Diagnosis,
            Prescriptions = appointment.Prescriptions.Select(p => new PrescriptionDetailDTO
            {
                Id = p.Id,
                AppointmentId = p.AppointmentId,
                MedicineId = p.MedicineId,
                MedicineName = p.Medicine.Name,
                Dosage = p.Dosage,
                StartDate = p.StartDate,
                EndDate = p.EndDate,
                Notes = p.Notes
            }).ToList()
        };

        return appointmentDto;
    }


    public async Task<bool> CreateAppointment(UpsertAppointmentDTO createDto)
    {
        var appointment = new Appointment
        {
            PatientId = createDto.PatientId,
            DoctorId = createDto.DoctorId,
            AppointmentDate = createDto.AppointmentDate,
            VisitType = createDto.VisitType,
            Notes = createDto.Notes,
            Diagnosis = createDto.Diagnosis
        };

        _context.Appointments.Add(appointment);
        await _context.SaveChangesAsync();
        return true;
    }


    public async Task<bool> UpdateAppointment(int id, UpsertAppointmentDTO updateDto)
    {
        var appointment = await _context.Appointments.FindAsync(id);
        if (appointment == null)
            return false;

        appointment.PatientId = updateDto.PatientId;
        appointment.DoctorId = updateDto.DoctorId;
        appointment.AppointmentDate = updateDto.AppointmentDate;
        appointment.VisitType = updateDto.VisitType;
        appointment.Notes = updateDto.Notes;
        appointment.Diagnosis = updateDto.Diagnosis;

        await _context.SaveChangesAsync();
        return true;
    }


    public async Task<bool> DeleteAppointment(int id)
    {
        var appointment = await _context.Appointments.FindAsync(id);
        if (appointment == null)
            return false;

        _context.Appointments.Remove(appointment);
        await _context.SaveChangesAsync();
        return true;
    }

}
