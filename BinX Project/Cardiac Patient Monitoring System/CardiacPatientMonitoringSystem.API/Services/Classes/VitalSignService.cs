using CardiacPatientMonitoringSystem.API.Data;
using CardiacPatientMonitoringSystem.API.DTOs.Requests;
using CardiacPatientMonitoringSystem.API.DTOs.Responses;
using CardiacPatientMonitoringSystem.API.Models;
using CardiacPatientMonitoringSystem.API.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CardiacPatientMonitoringSystem.API.Services.Classes
{
    public class VitalSignService : IVitalSignService
    {
        private readonly ApplicationDbContext _context;

        public VitalSignService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<VitalSignResponse>> GetAllAsync()
        {
            return await _context.VitalSigns
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
                .ToListAsync();
        }

        public async Task<VitalSignResponse?> GetByIdAsync(int id)
        {
            var vitalSign = await _context.VitalSigns
                .FirstOrDefaultAsync(v => v.Id == id);

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

        public async Task<VitalSignResponse> CreateAsync(CreateVitalSignRequest request)
        {
            var vitalSign = new VitalSign
            {
                PatientId = request.PatientId,
                HeartRate = request.HeartRate,
                SystolicBloodPressure = request.SystolicBloodPressure,
                DiastolicBloodPressure = request.DiastolicBloodPressure,
                OxygenSaturation = request.OxygenSaturation,
                RecordedAt = request.RecordedAt
            };

            _context.VitalSigns.Add(vitalSign);
            await _context.SaveChangesAsync();

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
            var vitalSign = await _context.VitalSigns
                .FirstOrDefaultAsync(v => v.Id == id);

            if (vitalSign == null)
                return false;

            vitalSign.HeartRate = request.HeartRate;
            vitalSign.SystolicBloodPressure = request.SystolicBloodPressure;
            vitalSign.DiastolicBloodPressure = request.DiastolicBloodPressure;
            vitalSign.OxygenSaturation = request.OxygenSaturation;
            vitalSign.RecordedAt = request.RecordedAt;

            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var vitalSign = await _context.VitalSigns
                .FirstOrDefaultAsync(v => v.Id == id);

            if (vitalSign == null)
                return false;

            _context.VitalSigns.Remove(vitalSign);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}