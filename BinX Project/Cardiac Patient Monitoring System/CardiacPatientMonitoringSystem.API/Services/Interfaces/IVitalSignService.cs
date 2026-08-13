using CardiacPatientMonitoringSystem.API.DTOs.Requests;
using CardiacPatientMonitoringSystem.API.DTOs.Responses;

namespace CardiacPatientMonitoringSystem.API.Services.Interfaces
{
    public interface IVitalSignService
    {
        Task<List<VitalSignResponse>> GetAllAsync();
        Task<VitalSignResponse?> GetByIdAsync(int id);
        Task<VitalSignResponse> CreateAsync(CreateVitalSignRequest request);
        Task<bool> UpdateAsync(int id, UpdateVitalSignRequest request);
        Task<bool> DeleteAsync(int id);
    }
}