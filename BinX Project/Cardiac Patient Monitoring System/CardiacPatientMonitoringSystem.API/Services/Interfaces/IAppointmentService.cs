using CardiacPatientMonitoringSystem.API.DTOs.Requests;
using CardiacPatientMonitoringSystem.API.DTOs.Responses;

namespace CardiacPatientMonitoringSystem.API.Services.Interfaces
{
    public interface IAppointmentService
    {
        Task<List<AppointmentResponse>> GetAllAsync();
        Task<AppointmentResponse?> GetByIdAsync(int id);
        Task<AppointmentResponse> CreateAsync(CreateAppointmentRequest request);
        Task<bool> UpdateAsync(int id, UpdateAppointmentRequest request);
        Task<bool> DeleteAsync(int id);
    }
}