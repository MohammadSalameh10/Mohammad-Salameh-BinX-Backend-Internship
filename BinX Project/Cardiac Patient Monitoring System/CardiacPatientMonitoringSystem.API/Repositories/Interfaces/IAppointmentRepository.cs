using CardiacPatientMonitoringSystem.API.DTOs.Responses;
using CardiacPatientMonitoringSystem.API.Models;

namespace CardiacPatientMonitoringSystem.API.Repositories.Interfaces
{
    public interface IAppointmentRepository
    {
        Task<PaginatedResponse<AppointmentResponse>> GetAllAsync(
            string? reason,
            int? patientId,
            string? sort,
            int page,
            int pageSize);
        Task<List<Appointment>> GetAllAsync(string? reason);
        Task<Appointment?> GetByIdAsync(int id);
        Task<List<Appointment>> GetByDoctorIdAsync(int doctorId);
        Task<Patient?> GetPatientByUserIdAsync(string userId);
        Task<Doctor?> GetDoctorByIdAsync(int doctorId);
        Task AddAsync(Appointment appointment);
        void Remove(Appointment appointment);
        Task SaveChangesAsync();
    }
}