using CardiacPatientMonitoringSystem.API.DTOs.Requests;
using CardiacPatientMonitoringSystem.API.DTOs.Responses;
using CardiacPatientMonitoringSystem.API.Services.Classes;
namespace CardiacPatientMonitoringSystem.API.Services.Interfaces
{
    public interface IDoctorService
    {
        Task<List<DoctorResponse>> GetAllAsync();

        Task<DoctorResponse?> GetByIdAsync(int id);

        Task<DoctorResponse?> CreateAsync(CreateDoctorRequest request);

        Task<bool> UpdateAsync(int id, UpdateDoctorRequest request);

        Task<DeleteDoctorResult> DeleteAsync(int id);
    }
}