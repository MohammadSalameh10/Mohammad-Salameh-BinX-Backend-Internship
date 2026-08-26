using CardiacPatientMonitoringSystem.API.DTOs.Requests;
using CardiacPatientMonitoringSystem.API.Models;
using CardiacPatientMonitoringSystem.API.Repositories.Interfaces;
using CardiacPatientMonitoringSystem.API.Services.Classes;
using Moq;

namespace CardiacPatientMonitoringSystem.Tests.Services
{
    public class PatientServiceTests
    {
        private readonly Mock<IPatientRepository> _mockRepository;
        private readonly PatientService _service;

        public PatientServiceTests()
        {
            _mockRepository = new Mock<IPatientRepository>();
            _service = new PatientService(_mockRepository.Object);
        }

        [Fact]
        public async Task CreateAsync_WhenPatientDoesNotExist_CreatesPatientAndReturnsResponse()
        {
            // Arrange
            var userId = "user-123";

            var request = new CreatePatientRequest
            {
                FullName = "Ahmad Khalil",
                DateOfBirth = new DateTime(1998, 4, 10),
                Gender = "Male",
                PhoneNumber = "0599222333",
                BloodType = "A+"
            };

            _mockRepository
                .Setup(r => r.GetByUserIdAsync(userId))
                .ReturnsAsync((Patient?)null);

            _mockRepository
                .Setup(r => r.AddAsync(It.IsAny<Patient>()))
                .Returns(Task.CompletedTask);

            _mockRepository
                .Setup(r => r.SaveChangesAsync())
                .Returns(Task.CompletedTask);

            // Act
            var result = await _service.CreateAsync(userId, request);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(userId, result.UserId);
            Assert.Equal("Ahmad Khalil", result.FullName);
            Assert.Equal("A+", result.BloodType);

            _mockRepository.Verify(
                r => r.AddAsync(It.Is<Patient>(p =>
                    p.UserId == userId &&
                    p.FullName == "Ahmad Khalil" &&
                    p.BloodType == "A+")),
                Times.Once);

            _mockRepository.Verify(
                r => r.SaveChangesAsync(),
                Times.Once);
        }

        [Fact]
        public async Task CreateAsync_WhenPatientAlreadyExists_ReturnsNull()
        {
            // Arrange
            var userId = "user-123";

            var existingPatient = new Patient
            {
                Id = 1,
                UserId = userId
            };

            var request = new CreatePatientRequest
            {
                FullName = "Ahmad Khalil",
                DateOfBirth = new DateTime(1998, 4, 10),
                Gender = "Male",
                PhoneNumber = "0599222333",
                BloodType = "A+"
            };

            _mockRepository
                .Setup(r => r.GetByUserIdAsync(userId))
                .ReturnsAsync(existingPatient);

            // Act
            var result = await _service.CreateAsync(userId, request);

            // Assert
            Assert.Null(result);

            _mockRepository.Verify(
                r => r.AddAsync(It.IsAny<Patient>()),
                Times.Never);

            _mockRepository.Verify(
                r => r.SaveChangesAsync(),
                Times.Never);
        }

        [Fact]
        public async Task UpdateAsync_WhenPatientExists_UpdatesPatientAndReturnsTrue()
        {
            // Arrange
            var patient = new Patient
            {
                Id = 1,
                UserId = "user-123",
                FullName = "Old Name",
                DateOfBirth = new DateTime(1998, 4, 10),
                Gender = "Male",
                PhoneNumber = "0599000000",
                BloodType = "A+"
            };

            var request = new UpdatePatientRequest
            {
                FullName = "Ahmad Khalil",
                DateOfBirth = new DateTime(1997, 5, 15),
                Gender = "Male",
                PhoneNumber = "0599222333",
                BloodType = "O+"
            };

            _mockRepository
                .Setup(r => r.GetByIdAsync(1))
                .ReturnsAsync(patient);

            _mockRepository
                .Setup(r => r.SaveChangesAsync())
                .Returns(Task.CompletedTask);

            // Act
            var result = await _service.UpdateAsync(1, request);

            // Assert
            Assert.True(result);

            Assert.Equal("Ahmad Khalil", patient.FullName);
            Assert.Equal(new DateTime(1997, 5, 15), patient.DateOfBirth);
            Assert.Equal("Male", patient.Gender);
            Assert.Equal("0599222333", patient.PhoneNumber);
            Assert.Equal("O+", patient.BloodType);

            _mockRepository.Verify(
                r => r.GetByIdAsync(1),
                Times.Once);

            _mockRepository.Verify(
                r => r.SaveChangesAsync(),
                Times.Once);
        }

        [Fact]
        public async Task UpdateAsync_WhenPatientDoesNotExist_ReturnsFalse()
        {
            // Arrange
            var request = new UpdatePatientRequest
            {
                FullName = "Ahmad Khalil",
                DateOfBirth = new DateTime(1997, 5, 15),
                Gender = "Male",
                PhoneNumber = "0599222333",
                BloodType = "O+"
            };

            _mockRepository
                .Setup(r => r.GetByIdAsync(999))
                .ReturnsAsync((Patient?)null);

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
        public async Task DeleteAsync_WhenPatientExists_RemovesPatientAndReturnsTrue()
        {
            // Arrange
            var patient = new Patient
            {
                Id = 1,
                UserId = "user-123",
                FullName = "Ahmad Khalil",
                DateOfBirth = new DateTime(1998, 4, 10),
                Gender = "Male",
                PhoneNumber = "0599222333",
                BloodType = "A+"
            };

            _mockRepository
                .Setup(r => r.GetByIdAsync(1))
                .ReturnsAsync(patient);

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
                r => r.Remove(patient),
                Times.Once);

            _mockRepository.Verify(
                r => r.SaveChangesAsync(),
                Times.Once);
        }

        [Fact]
        public async Task DeleteAsync_WhenPatientDoesNotExist_ReturnsFalse()
        {
            // Arrange
            _mockRepository
                .Setup(r => r.GetByIdAsync(999))
                .ReturnsAsync((Patient?)null);

            // Act
            var result = await _service.DeleteAsync(999);

            // Assert
            Assert.False(result);

            _mockRepository.Verify(
                r => r.GetByIdAsync(999),
                Times.Once);

            _mockRepository.Verify(
                r => r.Remove(It.IsAny<Patient>()),
                Times.Never);

            _mockRepository.Verify(
                r => r.SaveChangesAsync(),
                Times.Never);
        }
    }
}