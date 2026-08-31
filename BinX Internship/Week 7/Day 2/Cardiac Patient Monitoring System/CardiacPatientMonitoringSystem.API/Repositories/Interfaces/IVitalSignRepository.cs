using CardiacPatientMonitoringSystem.API.Models;

namespace CardiacPatientMonitoringSystem.API.Repositories.Interfaces
{
    public interface IVitalSignRepository
    {
        Task<List<VitalSign>> GetAllAsync();
        Task<VitalSign?> GetByIdAsync(int id);
        Task<Patient?> GetPatientByUserIdAsync(string userId);
        Task AddAsync(VitalSign vitalSign);
        void Remove(VitalSign vitalSign);
        Task SaveChangesAsync();
    }
}