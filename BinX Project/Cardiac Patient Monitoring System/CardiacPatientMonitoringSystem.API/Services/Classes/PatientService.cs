using CardiacPatientMonitoringSystem.API.Data;
using CardiacPatientMonitoringSystem.API.DTOs.Requests;
using CardiacPatientMonitoringSystem.API.DTOs.Responses;
using CardiacPatientMonitoringSystem.API.Models;
using CardiacPatientMonitoringSystem.API.Repositories.Interfaces;
using CardiacPatientMonitoringSystem.API.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CardiacPatientMonitoringSystem.API.Services.Classes
{
    public class PatientService : IPatientService
    {
        private readonly IPatientRepository _patientRepository;

        public PatientService(IPatientRepository patientRepository)
        {
            _patientRepository = patientRepository;
        }

        public async Task<List<PatientResponse>> GetAllAsync()
        {
            var patients = await _patientRepository.GetAllAsync();

            return patients
                .Select(p => new PatientResponse
                {
                    Id = p.Id,
                    UserId = p.UserId,
                    FullName = p.FullName,
                    DateOfBirth = p.DateOfBirth,
                    Gender = p.Gender,
                    PhoneNumber = p.PhoneNumber,
                    BloodType = p.BloodType
                })
                .ToList();
        }

        public async Task<PatientResponse?> GetByIdAsync(int id)
        {
            var patient = await _patientRepository.GetByIdAsync(id);

            if (patient == null)
                return null;

            return new PatientResponse
            {
                Id = patient.Id,
                UserId = patient.UserId,
                FullName = patient.FullName,
                DateOfBirth = patient.DateOfBirth,
                Gender = patient.Gender,
                PhoneNumber = patient.PhoneNumber,
                BloodType = patient.BloodType
            };
        }

        public async Task<PatientResponse?> CreateAsync(
            string userId,
            CreatePatientRequest request)
        {
            var existingPatient =
                await _patientRepository.GetByUserIdAsync(userId);

            if (existingPatient != null)
                return null;

            var patient = new Patient
            {
                UserId = userId,
                FullName = request.FullName,
                DateOfBirth = request.DateOfBirth,
                Gender = request.Gender,
                PhoneNumber = request.PhoneNumber,
                BloodType = request.BloodType
            };

            await _patientRepository.AddAsync(patient);
            await _patientRepository.SaveChangesAsync();

            return new PatientResponse
            {
                Id = patient.Id,
                UserId = patient.UserId,
                FullName = patient.FullName,
                DateOfBirth = patient.DateOfBirth,
                Gender = patient.Gender,
                PhoneNumber = patient.PhoneNumber,
                BloodType = patient.BloodType
            };
        }

        public async Task<bool> UpdateAsync(int id, UpdatePatientRequest request)
        {
            var patient = await _patientRepository.GetByIdAsync(id);

            if (patient == null)
                return false;

            patient.FullName = request.FullName;
            patient.DateOfBirth = request.DateOfBirth;
            patient.Gender = request.Gender;
            patient.PhoneNumber = request.PhoneNumber;
            patient.BloodType = request.BloodType;

            await _patientRepository.SaveChangesAsync();

            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var patient = await _patientRepository.GetByIdAsync(id);

            if (patient == null)
                return false;

            _patientRepository.Remove(patient);
            await _patientRepository.SaveChangesAsync();

            return true;
        }
    }
}