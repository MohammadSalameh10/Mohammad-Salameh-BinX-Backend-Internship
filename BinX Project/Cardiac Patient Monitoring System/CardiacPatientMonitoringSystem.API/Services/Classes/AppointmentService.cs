using CardiacPatientMonitoringSystem.API.Data;
using CardiacPatientMonitoringSystem.API.DTOs.Requests;
using CardiacPatientMonitoringSystem.API.DTOs.Responses;
using CardiacPatientMonitoringSystem.API.Models;
using CardiacPatientMonitoringSystem.API.Repositories.Interfaces;
using CardiacPatientMonitoringSystem.API.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CardiacPatientMonitoringSystem.API.Services.Classes
{
    public class AppointmentService : IAppointmentService
    {
        private readonly IAppointmentRepository _appointmentRepository;

        public AppointmentService(IAppointmentRepository appointmentRepository)
        {
            _appointmentRepository = appointmentRepository;
        }

        public async Task<PaginatedResponse<AppointmentResponse>> GetAllAsync(string? reason, int? patientId, string? sort, int page, int pageSize)
        {
            return await _appointmentRepository.GetAllAsync(reason, patientId, sort, page, pageSize);
        }

        public async Task<AppointmentResponse?> GetByIdAsync(int id)
        {
            var appointment = await _appointmentRepository.GetByIdAsync(id);

            if (appointment == null)
                return null;

            return new AppointmentResponse
            {
                Id = appointment.Id,
                PatientId = appointment.PatientId,
                DoctorId = appointment.DoctorId,
                AppointmentDate = appointment.AppointmentDate,
                Reason = appointment.Reason,
                Notes = appointment.Notes
            };
        }

        public async Task<List<AppointmentResponse>> GetByDoctorIdAsync(int doctorId)
        {
            var appointments = await _appointmentRepository
                .GetByDoctorIdAsync(doctorId);

            return appointments.Select(a => new AppointmentResponse
            {
                Id = a.Id,
                PatientId = a.PatientId,
                DoctorId = a.DoctorId,
                AppointmentDate = a.AppointmentDate,
                Reason = a.Reason,
                Notes = a.Notes
            }).ToList();
        }

        public async Task<AppointmentResponse?> CreateAsync(
     string userId,
     CreateAppointmentRequest request)
        {
            var patient = await _appointmentRepository.GetPatientByUserIdAsync(userId);

            if (patient == null)
                return null;

            var doctor = await _appointmentRepository.GetDoctorByIdAsync(request.DoctorId);

            if (doctor == null)
                return null;

            var appointment = new Appointment
            {
                PatientId = patient.Id,
                DoctorId = request.DoctorId,
                AppointmentDate = request.AppointmentDate,
                Reason = request.Reason,
                Notes = request.Notes
            };

            await _appointmentRepository.AddAsync(appointment);
            await _appointmentRepository.SaveChangesAsync();

            return new AppointmentResponse
            {
                Id = appointment.Id,
                PatientId = appointment.PatientId,
                DoctorId = appointment.DoctorId,
                AppointmentDate = appointment.AppointmentDate,
                Reason = appointment.Reason,
                Notes = appointment.Notes
            };
        }

        public async Task<bool> UpdateAsync(int id, UpdateAppointmentRequest request)
        {
            var appointment = await _appointmentRepository.GetByIdAsync(id);

            if (appointment == null)
                return false;

            var doctor = await _appointmentRepository.GetDoctorByIdAsync(request.DoctorId);

            if (doctor == null)
                return false;

            appointment.DoctorId = request.DoctorId;
            appointment.AppointmentDate = request.AppointmentDate;
            appointment.Reason = request.Reason;
            appointment.Notes = request.Notes;

            await _appointmentRepository.SaveChangesAsync();

            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var appointment = await _appointmentRepository.GetByIdAsync(id);

            if (appointment == null)
                return false;

            _appointmentRepository.Remove(appointment);
            await _appointmentRepository.SaveChangesAsync();

            return true;
        }
    }
}