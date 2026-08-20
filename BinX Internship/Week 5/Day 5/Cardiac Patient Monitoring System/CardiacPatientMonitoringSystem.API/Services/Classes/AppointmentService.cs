using CardiacPatientMonitoringSystem.API.Data;
using CardiacPatientMonitoringSystem.API.DTOs.Requests;
using CardiacPatientMonitoringSystem.API.DTOs.Responses;
using CardiacPatientMonitoringSystem.API.Models;
using CardiacPatientMonitoringSystem.API.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CardiacPatientMonitoringSystem.API.Services.Classes
{
    public class AppointmentService : IAppointmentService
    {
        private readonly ApplicationDbContext _context;

        public AppointmentService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<AppointmentResponse>> GetAllAsync(string? reason)
        {
            var query = _context.Appointments.AsQueryable();

            if (!string.IsNullOrWhiteSpace(reason))
            {
                query = query.Where(a => a.Reason.Contains(reason));
            }

            return await query
                .Select(a => new AppointmentResponse
                {
                    Id = a.Id,
                    PatientId = a.PatientId,
                    AppointmentDate = a.AppointmentDate,
                    Reason = a.Reason,
                    Notes = a.Notes
                })
                .ToListAsync();
        }

        public async Task<AppointmentResponse?> GetByIdAsync(int id)
        {
            var appointment = await _context.Appointments
                .FirstOrDefaultAsync(a => a.Id == id);

            if (appointment == null)
                return null;

            return new AppointmentResponse
            {
                Id = appointment.Id,
                PatientId = appointment.PatientId,
                AppointmentDate = appointment.AppointmentDate,
                Reason = appointment.Reason,
                Notes = appointment.Notes
            };
        }

        public async Task<AppointmentResponse?> CreateAsync(string userId, CreateAppointmentRequest request)
        {
            var patient = await _context.Patients
                .FirstOrDefaultAsync(p => p.UserId == userId);

            if (patient == null)
                return null;

            var appointment = new Appointment
            {
                PatientId = patient.Id,
                AppointmentDate = request.AppointmentDate,
                Reason = request.Reason,
                Notes = request.Notes
            };

            _context.Appointments.Add(appointment);
            await _context.SaveChangesAsync();

            return new AppointmentResponse
            {
                Id = appointment.Id,
                PatientId = appointment.PatientId,
                AppointmentDate = appointment.AppointmentDate,
                Reason = appointment.Reason,
                Notes = appointment.Notes
            };
        }

        public async Task<bool> UpdateAsync(int id, UpdateAppointmentRequest request)
        {
            var appointment = await _context.Appointments
                .FirstOrDefaultAsync(a => a.Id == id);

            if (appointment == null)
                return false;

            appointment.AppointmentDate = request.AppointmentDate;
            appointment.Reason = request.Reason;
            appointment.Notes = request.Notes;

            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var appointment = await _context.Appointments
                .FirstOrDefaultAsync(a => a.Id == id);

            if (appointment == null)
                return false;

            _context.Appointments.Remove(appointment);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}