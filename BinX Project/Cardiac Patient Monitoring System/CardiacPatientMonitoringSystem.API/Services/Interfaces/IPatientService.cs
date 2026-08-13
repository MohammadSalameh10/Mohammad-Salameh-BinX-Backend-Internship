using CardiacPatientMonitoringSystem.API.DTOs.Requests;
using CardiacPatientMonitoringSystem.API.DTOs.Responses;

namespace CardiacPatientMonitoringSystem.API.Services.Interfaces
{
    public interface IPatientService
    {
        Task<List<PatientResponse>> GetAllAsync();

        Task<PatientResponse?> GetByIdAsync(int id);

        Task<PatientResponse> CreateAsync(string userId, CreatePatientRequest request);

        Task<bool> UpdateAsync(int id, UpdatePatientRequest request);

        Task<bool> DeleteAsync(int id);
    }
}