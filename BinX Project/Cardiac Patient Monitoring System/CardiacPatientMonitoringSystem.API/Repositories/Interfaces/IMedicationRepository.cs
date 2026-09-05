using CardiacPatientMonitoringSystem.API.Models;

namespace CardiacPatientMonitoringSystem.API.Repositories.Interfaces
{
    public interface IMedicationRepository
    {
        Task<List<Medication>> GetAllAsync(string? name);

        Task<Medication?> GetByIdAsync(int id);

        Task<List<Medication>> GetByPatientIdAsync(int patientId);

        Task<bool> DoctorHasPatientAsync(int doctorId, int patientId);

        Task<Patient?> GetPatientByUserIdAsync(string userId);

        Task AddAsync(Medication medication);

        void Remove(Medication medication);

        Task SaveChangesAsync();
    }
}