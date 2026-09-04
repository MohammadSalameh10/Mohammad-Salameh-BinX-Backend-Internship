using CardiacPatientMonitoringSystem.API.Models;

namespace CardiacPatientMonitoringSystem.API.Repositories.Interfaces
{
    public interface IAuthRepository
    {
        Task BeginTransactionAsync();

        Task CommitTransactionAsync();

        Task RollbackTransactionAsync();

        Task AddPatientAsync(Patient patient);

        Task<Patient?> GetPatientByUserIdAsync(string userId);
    }
}