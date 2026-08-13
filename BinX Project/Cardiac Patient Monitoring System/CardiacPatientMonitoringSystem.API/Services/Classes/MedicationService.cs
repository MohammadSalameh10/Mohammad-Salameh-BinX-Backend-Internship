using CardiacPatientMonitoringSystem.API.Data;
using CardiacPatientMonitoringSystem.API.DTOs.Requests;
using CardiacPatientMonitoringSystem.API.DTOs.Responses;
using CardiacPatientMonitoringSystem.API.Models;
using CardiacPatientMonitoringSystem.API.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CardiacPatientMonitoringSystem.API.Services.Classes
{
    public class MedicationService : IMedicationService
    {
        private readonly ApplicationDbContext _context;

        public MedicationService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<MedicationResponse>> GetAllAsync()
        {
            return await _context.Medications
                .Select(m => new MedicationResponse
                {
                    Id = m.Id,
                    PatientId = m.PatientId,
                    Name = m.Name,
                    Dosage = m.Dosage,
                    Frequency = m.Frequency,
                    StartDate = m.StartDate,
                    EndDate = m.EndDate
                })
                .ToListAsync();
        }

        public async Task<MedicationResponse?> GetByIdAsync(int id)
        {
            var medication = await _context.Medications
                .FirstOrDefaultAsync(m => m.Id == id);

            if (medication == null)
                return null;

            return new MedicationResponse
            {
                Id = medication.Id,
                PatientId = medication.PatientId,
                Name = medication.Name,
                Dosage = medication.Dosage,
                Frequency = medication.Frequency,
                StartDate = medication.StartDate,
                EndDate = medication.EndDate
            };
        }

        public async Task<MedicationResponse?> CreateAsync(string userId, CreateMedicationRequest request)
        {
            var patient = await _context.Patients
                .FirstOrDefaultAsync(p => p.UserId == userId);

            if (patient == null)
                return null;

            var medication = new Medication
            {
                PatientId = patient.Id,
                Name = request.Name,
                Dosage = request.Dosage,
                Frequency = request.Frequency,
                StartDate = request.StartDate,
                EndDate = request.EndDate
            };

            _context.Medications.Add(medication);
            await _context.SaveChangesAsync();

            return new MedicationResponse
            {
                Id = medication.Id,
                PatientId = medication.PatientId,
                Name = medication.Name,
                Dosage = medication.Dosage,
                Frequency = medication.Frequency,
                StartDate = medication.StartDate,
                EndDate = medication.EndDate
            };
        }

        public async Task<bool> UpdateAsync(int id, UpdateMedicationRequest request)
        {
            var medication = await _context.Medications
                .FirstOrDefaultAsync(m => m.Id == id);

            if (medication == null)
                return false;

            medication.Name = request.Name;
            medication.Dosage = request.Dosage;
            medication.Frequency = request.Frequency;
            medication.StartDate = request.StartDate;
            medication.EndDate = request.EndDate;

            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var medication = await _context.Medications
                .FirstOrDefaultAsync(m => m.Id == id);

            if (medication == null)
                return false;

            _context.Medications.Remove(medication);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}