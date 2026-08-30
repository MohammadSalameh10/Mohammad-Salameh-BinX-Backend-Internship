using CardiacPatientMonitoringSystem.API.Data;
using CardiacPatientMonitoringSystem.API.DTOs.Responses;
using CardiacPatientMonitoringSystem.API.Models;
using CardiacPatientMonitoringSystem.API.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CardiacPatientMonitoringSystem.API.Repositories.Classes
{
    public class AppointmentRepository : IAppointmentRepository
    {
        private readonly ApplicationDbContext _context;

        public AppointmentRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<PaginatedResponse<AppointmentResponse>> GetAllAsync(
           string? reason,
           int? patientId,
           string? sort,
           int page,
           int pageSize)
        {
            var query = _context.Appointments.AsQueryable();

            if (!string.IsNullOrWhiteSpace(reason))
            {
                query = query.Where(a => a.Reason.Contains(reason));
            }

            if (patientId.HasValue)
            {
                query = query.Where(a => a.PatientId == patientId.Value);
            }

            query = sort switch
            {
                "date_desc" => query.OrderByDescending(a => a.AppointmentDate),
                "date_asc" => query.OrderBy(a => a.AppointmentDate),
                _ => query.OrderBy(a => a.AppointmentDate)
            };

            var totalCount = await query.CountAsync();

            var items = await query
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .Select(a => new AppointmentResponse
                {
                    Id = a.Id,
                    PatientId = a.PatientId,
                    AppointmentDate = a.AppointmentDate,
                    Reason = a.Reason,
                    Notes = a.Notes
                })
                .ToListAsync();

            return new PaginatedResponse<AppointmentResponse>
            {
                Page = page,
                PageSize = pageSize,
                TotalCount = totalCount,
                Items = items
            };
        }

        public async Task<List<Appointment>> GetAllAsync(string? reason)
        {
            var query = _context.Appointments.AsQueryable();

            if (!string.IsNullOrWhiteSpace(reason))
            {
                query = query.Where(a => a.Reason.Contains(reason));
            }

            return await query.ToListAsync();
        }

        public async Task<Appointment?> GetByIdAsync(int id)
        {
            return await _context.Appointments
                .FirstOrDefaultAsync(a => a.Id == id);
        }

        public async Task<Patient?> GetPatientByUserIdAsync(string userId)
        {
            return await _context.Patients
                .FirstOrDefaultAsync(p => p.UserId == userId);
        }

        public async Task AddAsync(Appointment appointment)
        {
            await _context.Appointments.AddAsync(appointment);
        }

        public void Remove(Appointment appointment)
        {
            _context.Appointments.Remove(appointment);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}