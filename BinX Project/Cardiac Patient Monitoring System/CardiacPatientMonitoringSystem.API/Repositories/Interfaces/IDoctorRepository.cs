using CardiacPatientMonitoringSystem.API.Models;

namespace CardiacPatientMonitoringSystem.API.Repositories.Interfaces
{
    public interface IDoctorRepository
    {
        Task<List<Doctor>> GetAllAsync();

        Task<Doctor?> GetByIdAsync(int id);

        Task<Doctor?> GetByUserIdAsync(string userId);

        Task<bool> HasAppointmentsAsync(int doctorId);

        Task AddAsync(Doctor doctor);

        void Remove(Doctor doctor);

        Task SaveChangesAsync();

        Task BeginTransactionAsync();

        Task CommitTransactionAsync();

        Task RollbackTransactionAsync();
    }
}