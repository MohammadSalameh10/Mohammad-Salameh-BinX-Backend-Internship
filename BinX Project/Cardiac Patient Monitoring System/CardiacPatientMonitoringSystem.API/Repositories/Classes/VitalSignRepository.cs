using CardiacPatientMonitoringSystem.API.Data;
using CardiacPatientMonitoringSystem.API.Models;
using CardiacPatientMonitoringSystem.API.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CardiacPatientMonitoringSystem.API.Repositories.Classes
{
    public class VitalSignRepository : IVitalSignRepository
    {
        private readonly ApplicationDbContext _context;

        public VitalSignRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<VitalSign>> GetAllAsync()
        {
            return await _context.VitalSigns.ToListAsync();
        }

        public async Task<VitalSign?> GetByIdAsync(int id)
        {
            return await _context.VitalSigns
                .FirstOrDefaultAsync(v => v.Id == id);
        }

        public async Task<List<VitalSign>> GetByPatientIdAsync(int patientId)
        {
            return await _context.VitalSigns
                .Where(v => v.PatientId == patientId)
                .OrderByDescending(v => v.RecordedAt)
                .ToListAsync();
        }

        public async Task<bool> DoctorHasPatientAsync(int doctorId, int patientId)
        {
            return await _context.Appointments
                .AnyAsync(a =>
                    a.DoctorId == doctorId &&
                    a.PatientId == patientId);
        }

        public async Task<Patient?> GetPatientByUserIdAsync(string userId)
        {
            return await _context.Patients
                .FirstOrDefaultAsync(p => p.UserId == userId);
        }

        public async Task AddAsync(VitalSign vitalSign)
        {
            await _context.VitalSigns.AddAsync(vitalSign);
        }

        public void Remove(VitalSign vitalSign)
        {
            _context.VitalSigns.Remove(vitalSign);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}