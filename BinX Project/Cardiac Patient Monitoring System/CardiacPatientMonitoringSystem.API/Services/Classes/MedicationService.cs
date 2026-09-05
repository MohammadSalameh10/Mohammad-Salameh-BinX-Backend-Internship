using CardiacPatientMonitoringSystem.API.Data;
using CardiacPatientMonitoringSystem.API.DTOs.Requests;
using CardiacPatientMonitoringSystem.API.DTOs.Responses;
using CardiacPatientMonitoringSystem.API.Models;
using CardiacPatientMonitoringSystem.API.Repositories.Interfaces;
using CardiacPatientMonitoringSystem.API.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CardiacPatientMonitoringSystem.API.Services.Classes
{
    public class MedicationService : IMedicationService
    {
        private readonly IMedicationRepository _medicationRepository;

        public MedicationService(IMedicationRepository medicationRepository)
        {
            _medicationRepository = medicationRepository;
        }

        public async Task<List<MedicationResponse>> GetAllAsync(string? name)
        {
            var medications = await _medicationRepository.GetAllAsync(name);

            return medications
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
                .ToList();
        }

        public async Task<MedicationResponse?> GetByIdAsync(int id)
        {
            var medication = await _medicationRepository.GetByIdAsync(id);

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

        public async Task<List<MedicationResponse>?> GetPatientMedicationsForDoctorAsync(int doctorId, int patientId)
        {
            var hasAccess = await _medicationRepository
                .DoctorHasPatientAsync(doctorId, patientId);

            if (!hasAccess)
                return null;

            var medications = await _medicationRepository
                .GetByPatientIdAsync(patientId);

            return medications
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
                .ToList();
        }

        public async Task<MedicationResponse?> CreateAsync(
    string userId,
    CreateMedicationRequest request)
        {
            var patient = await _medicationRepository.GetPatientByUserIdAsync(userId);

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

            await _medicationRepository.AddAsync(medication);
            await _medicationRepository.SaveChangesAsync();

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
            var medication = await _medicationRepository.GetByIdAsync(id);

            if (medication == null)
                return false;

            medication.Name = request.Name;
            medication.Dosage = request.Dosage;
            medication.Frequency = request.Frequency;
            medication.StartDate = request.StartDate;
            medication.EndDate = request.EndDate;

            await _medicationRepository.SaveChangesAsync();

            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var medication = await _medicationRepository.GetByIdAsync(id);

            if (medication == null)
                return false;

            _medicationRepository.Remove(medication);
            await _medicationRepository.SaveChangesAsync();

            return true;
        }
    }
}