using CardiacPatientMonitoringSystem.API.Data;
using CardiacPatientMonitoringSystem.API.DTOs.Requests;
using CardiacPatientMonitoringSystem.API.DTOs.Responses;
using CardiacPatientMonitoringSystem.API.Models;
using CardiacPatientMonitoringSystem.API.Repositories.Interfaces;
using CardiacPatientMonitoringSystem.API.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CardiacPatientMonitoringSystem.API.Services.Classes
{
    public class VitalSignService : IVitalSignService
    {
        private readonly IVitalSignRepository _repository;

        public VitalSignService(IVitalSignRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<VitalSignResponse>> GetAllAsync()
        {
            var vitalSigns = await _repository.GetAllAsync();

            return vitalSigns
                .Select(v => new VitalSignResponse
                {
                    Id = v.Id,
                    PatientId = v.PatientId,
                    HeartRate = v.HeartRate,
                    SystolicBloodPressure = v.SystolicBloodPressure,
                    DiastolicBloodPressure = v.DiastolicBloodPressure,
                    OxygenSaturation = v.OxygenSaturation,
                    RecordedAt = v.RecordedAt
                })
                .ToList();
        }

        public async Task<VitalSignResponse?> GetByIdAsync(int id)
        {
            var vitalSign = await _repository.GetByIdAsync(id);

            if (vitalSign == null)
                return null;

            return new VitalSignResponse
            {
                Id = vitalSign.Id,
                PatientId = vitalSign.PatientId,
                HeartRate = vitalSign.HeartRate,
                SystolicBloodPressure = vitalSign.SystolicBloodPressure,
                DiastolicBloodPressure = vitalSign.DiastolicBloodPressure,
                OxygenSaturation = vitalSign.OxygenSaturation,
                RecordedAt = vitalSign.RecordedAt
            };
        }

        public async Task<VitalSignResponse?> CreateAsync(string userId, CreateVitalSignRequest request)
        {
            var patient = await _repository.GetPatientByUserIdAsync(userId);

            if (patient == null)
                return null;

            var vitalSign = new VitalSign
            {
                PatientId = patient.Id,
                HeartRate = request.HeartRate,
                SystolicBloodPressure = request.SystolicBloodPressure,
                DiastolicBloodPressure = request.DiastolicBloodPressure,
                OxygenSaturation = request.OxygenSaturation,
                RecordedAt = request.RecordedAt
            };

            await _repository.AddAsync(vitalSign);
            await _repository.SaveChangesAsync();

            return new VitalSignResponse
            {
                Id = vitalSign.Id,
                PatientId = vitalSign.PatientId,
                HeartRate = vitalSign.HeartRate,
                SystolicBloodPressure = vitalSign.SystolicBloodPressure,
                DiastolicBloodPressure = vitalSign.DiastolicBloodPressure,
                OxygenSaturation = vitalSign.OxygenSaturation,
                RecordedAt = vitalSign.RecordedAt
            };
        }

        public async Task<bool> UpdateAsync(int id, UpdateVitalSignRequest request)
        {
            var vitalSign = await _repository.GetByIdAsync(id);

            if (vitalSign == null)
                return false;

            vitalSign.HeartRate = request.HeartRate;
            vitalSign.SystolicBloodPressure = request.SystolicBloodPressure;
            vitalSign.DiastolicBloodPressure = request.DiastolicBloodPressure;
            vitalSign.OxygenSaturation = request.OxygenSaturation;
            vitalSign.RecordedAt = request.RecordedAt;

            await _repository.SaveChangesAsync();

            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var vitalSign = await _repository.GetByIdAsync(id);

            if (vitalSign == null)
                return false;

            _repository.Remove(vitalSign);
            await _repository.SaveChangesAsync();

            return true;
        }

        public string GetHeartRateStatus(int heartRate)
        {
            if (heartRate < 60)
                return "Low";

            if (heartRate > 100)
                return "High";

            return "Normal";
        }
    }
}