using CardiacPatientMonitoringSystem.API.DTOs.Requests;
using CardiacPatientMonitoringSystem.API.DTOs.Responses;

namespace CardiacPatientMonitoringSystem.API.Services.Interfaces
{
    public interface IMedicationService
    {
        Task<List<MedicationResponse>> GetAllAsync();
        Task<MedicationResponse?> GetByIdAsync(int id);
        Task<MedicationResponse> CreateAsync(CreateMedicationRequest request);
        Task<bool> UpdateAsync(int id, UpdateMedicationRequest request);
        Task<bool> DeleteAsync(int id);
    }
}