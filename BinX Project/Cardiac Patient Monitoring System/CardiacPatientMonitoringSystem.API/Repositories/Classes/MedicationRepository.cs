using CardiacPatientMonitoringSystem.API.Data;
using CardiacPatientMonitoringSystem.API.Models;
using CardiacPatientMonitoringSystem.API.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CardiacPatientMonitoringSystem.API.Repositories.Classes
{
    public class MedicationRepository : IMedicationRepository
    {
        private readonly ApplicationDbContext _context;

        public MedicationRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Medication>> GetAllAsync(string? name)
        {
            var query = _context.Medications.AsQueryable();

            if (!string.IsNullOrWhiteSpace(name))
            {
                query = query.Where(m => m.Name.Contains(name));
            }

            return await query.ToListAsync();
        }

        public async Task<Medication?> GetByIdAsync(int id)
        {
            return await _context.Medications
                .FirstOrDefaultAsync(m => m.Id == id);
        }

        public async Task<Patient?> GetPatientByUserIdAsync(string userId)
        {
            return await _context.Patients
                .FirstOrDefaultAsync(p => p.UserId == userId);
        }

        public async Task AddAsync(Medication medication)
        {
            await _context.Medications.AddAsync(medication);
        }

        public void Remove(Medication medication)
        {
            _context.Medications.Remove(medication);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}