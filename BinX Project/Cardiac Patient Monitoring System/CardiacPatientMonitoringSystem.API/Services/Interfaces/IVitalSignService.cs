using CardiacPatientMonitoringSystem.API.DTOs.Requests;
using CardiacPatientMonitoringSystem.API.DTOs.Responses;

namespace CardiacPatientMonitoringSystem.API.Services.Interfaces
{
    public interface IVitalSignService
    {
        Task<List<VitalSignResponse>> GetAllAsync();
        Task<VitalSignResponse?> GetByIdAsync(int id);
        Task<List<VitalSignResponse>?> GetPatientVitalSignsForDoctorAsync(int doctorId, int patientId);
        Task<VitalSignResponse?> CreateAsync(string userId, CreateVitalSignRequest request);
        Task<bool> UpdateAsync(int id, UpdateVitalSignRequest request);
        Task<bool> DeleteAsync(int id);
    }
}