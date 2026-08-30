using CardiacPatientMonitoringSystem.API.DTOs.Requests;
using CardiacPatientMonitoringSystem.API.DTOs.Responses;

namespace CardiacPatientMonitoringSystem.API.Services.Interfaces
{
    public interface IAppointmentService
    {
        Task<PaginatedResponse<AppointmentResponse>> GetAllAsync(
          string? reason,
          int? patientId,
          string? sort,
          int page,
          int pageSize);
        Task<AppointmentResponse?> GetByIdAsync(int id);
        Task<AppointmentResponse?> CreateAsync(string userId, CreateAppointmentRequest request);
        Task<bool> UpdateAsync(int id, UpdateAppointmentRequest request);
        Task<bool> DeleteAsync(int id);
    }
}