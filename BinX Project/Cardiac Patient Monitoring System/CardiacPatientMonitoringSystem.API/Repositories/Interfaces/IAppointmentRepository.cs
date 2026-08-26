using CardiacPatientMonitoringSystem.API.Models;

namespace CardiacPatientMonitoringSystem.API.Repositories.Interfaces
{
    public interface IAppointmentRepository
    {
        Task<List<Appointment>> GetAllAsync(string? reason);
        Task<Appointment?> GetByIdAsync(int id);
        Task<Patient?> GetPatientByUserIdAsync(string userId);
        Task AddAsync(Appointment appointment);
        void Remove(Appointment appointment);
        Task SaveChangesAsync();
    }
}