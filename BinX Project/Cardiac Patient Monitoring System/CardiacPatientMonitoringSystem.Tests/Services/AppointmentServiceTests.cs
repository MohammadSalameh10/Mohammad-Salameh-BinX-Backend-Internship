using CardiacPatientMonitoringSystem.API.DTOs.Requests;
using CardiacPatientMonitoringSystem.API.Models;
using CardiacPatientMonitoringSystem.API.Repositories.Interfaces;
using CardiacPatientMonitoringSystem.API.Services.Classes;
using Moq;

namespace CardiacPatientMonitoringSystem.Tests.Services
{
    public class AppointmentServiceTests
    {
        private readonly Mock<IAppointmentRepository> _mockRepository;
        private readonly AppointmentService _service;

        public AppointmentServiceTests()
        {
            _mockRepository = new Mock<IAppointmentRepository>();
            _service = new AppointmentService(_mockRepository.Object);
        }

        [Fact]
        public async Task CreateAsync_WhenPatientExists_CreatesAppointmentAndReturnsResponse()
        {
            // Arrange
            var userId = "user-123";

            var patient = new Patient
            {
                Id = 1
            };

            var request = new CreateAppointmentRequest
            {
                AppointmentDate = new DateTime(2026, 9, 1, 10, 0, 0),
                Reason = "Routine cardiac follow-up",
                Notes = "Routine check-up"
            };

            _mockRepository
                .Setup(r => r.GetPatientByUserIdAsync(userId))
                .ReturnsAsync(patient);

            _mockRepository
                .Setup(r => r.AddAsync(It.IsAny<Appointment>()))
                .Returns(Task.CompletedTask);

            _mockRepository
                .Setup(r => r.SaveChangesAsync())
                .Returns(Task.CompletedTask);

            // Act
            var result = await _service.CreateAsync(userId, request);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(1, result.PatientId);
            Assert.Equal("Routine cardiac follow-up", result.Reason);
            Assert.Equal("Routine check-up", result.Notes);

            _mockRepository.Verify(
                r => r.AddAsync(It.Is<Appointment>(a =>
                    a.PatientId == 1 &&
                    a.Reason == "Routine cardiac follow-up" &&
                    a.Notes == "Routine check-up")),
                Times.Once);

            _mockRepository.Verify(
                r => r.SaveChangesAsync(),
                Times.Once);
        }

        [Fact]
        public async Task CreateAsync_WhenPatientDoesNotExist_ReturnsNull()
        {
            // Arrange
            var userId = "user-123";

            var request = new CreateAppointmentRequest
            {
                AppointmentDate = new DateTime(2026, 9, 1, 10, 0, 0),
                Reason = "Routine cardiac follow-up",
                Notes = "Routine check-up"
            };

            _mockRepository
                .Setup(r => r.GetPatientByUserIdAsync(userId))
                .ReturnsAsync((Patient?)null);

            // Act
            var result = await _service.CreateAsync(userId, request);

            // Assert
            Assert.Null(result);

            _mockRepository.Verify(
                r => r.AddAsync(It.IsAny<Appointment>()),
                Times.Never);

            _mockRepository.Verify(
                r => r.SaveChangesAsync(),
                Times.Never);
        }

        [Fact]
        public async Task UpdateAsync_WhenAppointmentExists_UpdatesAppointmentAndReturnsTrue()
        {
            // Arrange
            var appointment = new Appointment
            {
                Id = 1,
                PatientId = 1,
                AppointmentDate = new DateTime(2026, 9, 1, 10, 0, 0),
                Reason = "Old reason",
                Notes = "Old notes"
            };

            var request = new UpdateAppointmentRequest
            {
                AppointmentDate = new DateTime(2026, 9, 10, 11, 0, 0),
                Reason = "Cardiac follow-up",
                Notes = "Updated notes"
            };

            _mockRepository
                .Setup(r => r.GetByIdAsync(1))
                .ReturnsAsync(appointment);

            _mockRepository
                .Setup(r => r.SaveChangesAsync())
                .Returns(Task.CompletedTask);

            // Act
            var result = await _service.UpdateAsync(1, request);

            // Assert
            Assert.True(result);

            Assert.Equal(
                new DateTime(2026, 9, 10, 11, 0, 0),
                appointment.AppointmentDate);

            Assert.Equal("Cardiac follow-up", appointment.Reason);
            Assert.Equal("Updated notes", appointment.Notes);

            _mockRepository.Verify(
                r => r.GetByIdAsync(1),
                Times.Once);

            _mockRepository.Verify(
                r => r.SaveChangesAsync(),
                Times.Once);
        }

        [Fact]
        public async Task UpdateAsync_WhenAppointmentDoesNotExist_ReturnsFalse()
        {
            // Arrange
            var request = new UpdateAppointmentRequest
            {
                AppointmentDate = new DateTime(2026, 9, 10, 11, 0, 0),
                Reason = "Cardiac follow-up",
                Notes = "Updated notes"
            };

            _mockRepository
                .Setup(r => r.GetByIdAsync(999))
                .ReturnsAsync((Appointment?)null);

            // Act
            var result = await _service.UpdateAsync(999, request);

            // Assert
            Assert.False(result);

            _mockRepository.Verify(
                r => r.GetByIdAsync(999),
                Times.Once);

            _mockRepository.Verify(
                r => r.SaveChangesAsync(),
                Times.Never);
        }

        [Fact]
        public async Task DeleteAsync_WhenAppointmentExists_RemovesAppointmentAndReturnsTrue()
        {
            // Arrange
            var appointment = new Appointment
            {
                Id = 1,
                PatientId = 1,
                AppointmentDate = new DateTime(2026, 9, 10, 11, 0, 0),
                Reason = "Cardiac follow-up",
                Notes = "Routine check-up"
            };

            _mockRepository
                .Setup(r => r.GetByIdAsync(1))
                .ReturnsAsync(appointment);

            _mockRepository
                .Setup(r => r.SaveChangesAsync())
                .Returns(Task.CompletedTask);

            // Act
            var result = await _service.DeleteAsync(1);

            // Assert
            Assert.True(result);

            _mockRepository.Verify(
                r => r.GetByIdAsync(1),
                Times.Once);

            _mockRepository.Verify(
                r => r.Remove(appointment),
                Times.Once);

            _mockRepository.Verify(
                r => r.SaveChangesAsync(),
                Times.Once);
        }

        [Fact]
        public async Task DeleteAsync_WhenAppointmentDoesNotExist_ReturnsFalse()
        {
            // Arrange
            _mockRepository
                .Setup(r => r.GetByIdAsync(999))
                .ReturnsAsync((Appointment?)null);

            // Act
            var result = await _service.DeleteAsync(999);

            // Assert
            Assert.False(result);

            _mockRepository.Verify(
                r => r.GetByIdAsync(999),
                Times.Once);

            _mockRepository.Verify(
                r => r.Remove(It.IsAny<Appointment>()),
                Times.Never);

            _mockRepository.Verify(
                r => r.SaveChangesAsync(),
                Times.Never);
        }
    }
}