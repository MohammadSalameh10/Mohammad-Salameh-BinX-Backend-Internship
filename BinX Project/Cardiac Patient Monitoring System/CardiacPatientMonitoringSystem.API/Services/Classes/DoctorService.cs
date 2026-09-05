using CardiacPatientMonitoringSystem.API.DTOs.Requests;
using CardiacPatientMonitoringSystem.API.DTOs.Responses;
using CardiacPatientMonitoringSystem.API.Models;
using CardiacPatientMonitoringSystem.API.Repositories.Interfaces;
using CardiacPatientMonitoringSystem.API.Services.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace CardiacPatientMonitoringSystem.API.Services.Classes
{
    public enum DeleteDoctorResult
    {
        Deleted,
        NotFound,
        HasAppointments
    }
    public class DoctorService : IDoctorService
    {
        private readonly IDoctorRepository _doctorRepository;
        private readonly UserManager<IdentityUser> _userManager;

        public DoctorService(IDoctorRepository doctorRepository, UserManager<IdentityUser> userManager)
        {
            _doctorRepository = doctorRepository;
            _userManager = userManager;
        }

        public async Task<List<DoctorResponse>> GetAllAsync()
        {
            var doctors = await _doctorRepository.GetAllAsync();

            return doctors.Select(d => new DoctorResponse
            {
                Id = d.Id,
                UserId = d.UserId,
                FullName = d.FullName,
                PhoneNumber = d.PhoneNumber
            }).ToList();
        }

        public async Task<DoctorResponse?> GetByIdAsync(int id)
        {
            var doctor = await _doctorRepository.GetByIdAsync(id);

            if (doctor == null)
                return null;

            return new DoctorResponse
            {
                Id = doctor.Id,
                UserId = doctor.UserId,
                FullName = doctor.FullName,
                PhoneNumber = doctor.PhoneNumber
            };
        }

        public async Task<DoctorResponse?> CreateAsync(
     CreateDoctorRequest request)
        {
            await _doctorRepository.BeginTransactionAsync();

            try
            {
                var user = new IdentityUser
                {
                    UserName = request.Email,
                    Email = request.Email
                };

                var userResult = await _userManager.CreateAsync(
                    user,
                    request.Password);

                if (!userResult.Succeeded)
                {
                    await _doctorRepository.RollbackTransactionAsync();
                    return null;
                }

                var roleResult = await _userManager.AddToRoleAsync(
                    user,
                    "Doctor");

                if (!roleResult.Succeeded)
                {
                    await _doctorRepository.RollbackTransactionAsync();
                    return null;
                }

                var doctor = new Doctor
                {
                    UserId = user.Id,
                    FullName = request.FullName,
                    PhoneNumber = request.PhoneNumber
                };

                await _doctorRepository.AddAsync(doctor);
                await _doctorRepository.SaveChangesAsync();

                await _doctorRepository.CommitTransactionAsync();

                return new DoctorResponse
                {
                    Id = doctor.Id,
                    UserId = doctor.UserId,
                    FullName = doctor.FullName,
                    PhoneNumber = doctor.PhoneNumber
                };
            }
            catch
            {
                await _doctorRepository.RollbackTransactionAsync();
                throw;
            }
        }

        public async Task<bool> UpdateAsync(
            int id,
            UpdateDoctorRequest request)
        {
            var doctor = await _doctorRepository.GetByIdAsync(id);

            if (doctor == null)
                return false;

            doctor.FullName = request.FullName;
            doctor.PhoneNumber = request.PhoneNumber;

            await _doctorRepository.SaveChangesAsync();

            return true;
        }

        public async Task<DeleteDoctorResult> DeleteAsync(int id)
        {
            var doctor = await _doctorRepository.GetByIdAsync(id);

            if (doctor == null)
                return DeleteDoctorResult.NotFound;

            var hasAppointments = await _doctorRepository
                .HasAppointmentsAsync(id);

            if (hasAppointments)
                return DeleteDoctorResult.HasAppointments;

            var user = await _userManager.FindByIdAsync(doctor.UserId);

            await _doctorRepository.BeginTransactionAsync();

            try
            {
                _doctorRepository.Remove(doctor);
                await _doctorRepository.SaveChangesAsync();

                if (user != null)
                {
                    var userResult = await _userManager.DeleteAsync(user);

                    if (!userResult.Succeeded)
                    {
                        await _doctorRepository.RollbackTransactionAsync();
                        return DeleteDoctorResult.NotFound;
                    }
                }

                await _doctorRepository.CommitTransactionAsync();

                return DeleteDoctorResult.Deleted;
            }
            catch
            {
                await _doctorRepository.RollbackTransactionAsync();
                throw;
            }
        }
    }
}