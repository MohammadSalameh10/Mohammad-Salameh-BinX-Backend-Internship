using CardiacPatientMonitoringSystem.API.Models;

namespace CardiacPatientMonitoringSystem.API.Repositories.Interfaces
{
    public interface IPatientRepository
    {
        Task<List<Patient>> GetAllAsync();
        Task<Patient?> GetByIdAsync(int id);
        Task<Patient?> GetByUserIdAsync(string userId);
        Task AddAsync(Patient patient);
        void Remove(Patient patient);
        Task SaveChangesAsync();
    }
}