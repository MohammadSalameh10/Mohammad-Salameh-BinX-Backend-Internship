using CardiacPatientMonitoringSystem.API.DTOs.Requests;
using CardiacPatientMonitoringSystem.API.Models;
using CardiacPatientMonitoringSystem.API.Repositories.Interfaces;
using CardiacPatientMonitoringSystem.API.Services.Classes;
using Moq;

namespace CardiacPatientMonitoringSystem.Tests.Services
{
    public class MedicationServiceTests
    {
        private readonly Mock<IMedicationRepository> _mockRepository;
        private readonly MedicationService _service;

        public MedicationServiceTests()
        {
            _mockRepository = new Mock<IMedicationRepository>();
            _service = new MedicationService(_mockRepository.Object);
        }

        [Fact]
        public async Task CreateAsync_WhenPatientExists_CreatesMedicationAndReturnsResponse()
        {
            // Arrange
            var userId = "user-123";

            var patient = new Patient
            {
                Id = 1
            };

            var request = new CreateMedicationRequest
            {
                Name = "Aspirin",
                Dosage = "81 mg",
                Frequency = "Once daily",
                StartDate = new DateTime(2026, 8, 1),
                EndDate = new DateTime(2026, 9, 1)
            };

            _mockRepository
                .Setup(r => r.GetPatientByUserIdAsync(userId))
                .ReturnsAsync(patient);

            _mockRepository
                .Setup(r => r.AddAsync(It.IsAny<Medication>()))
                .Returns(Task.CompletedTask);

            _mockRepository
                .Setup(r => r.SaveChangesAsync())
                .Returns(Task.CompletedTask);

            // Act
            var result = await _service.CreateAsync(userId, request);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(1, result.PatientId);
            Assert.Equal("Aspirin", result.Name);
            Assert.Equal("81 mg", result.Dosage);
            Assert.Equal("Once daily", result.Frequency);

            _mockRepository.Verify(
                r => r.AddAsync(It.Is<Medication>(m =>
                    m.PatientId == 1 &&
                    m.Name == "Aspirin" &&
                    m.Dosage == "81 mg" &&
                    m.Frequency == "Once daily")),
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

            var request = new CreateMedicationRequest
            {
                Name = "Aspirin",
                Dosage = "81 mg",
                Frequency = "Once daily",
                StartDate = new DateTime(2026, 8, 1),
                EndDate = new DateTime(2026, 9, 1)
            };

            _mockRepository
                .Setup(r => r.GetPatientByUserIdAsync(userId))
                .ReturnsAsync((Patient?)null);

            // Act
            var result = await _service.CreateAsync(userId, request);

            // Assert
            Assert.Null(result);

            _mockRepository.Verify(
                r => r.AddAsync(It.IsAny<Medication>()),
                Times.Never);

            _mockRepository.Verify(
                r => r.SaveChangesAsync(),
                Times.Never);
        }

        [Fact]
        public async Task UpdateAsync_WhenMedicationExists_UpdatesMedicationAndReturnsTrue()
        {
            // Arrange
            var medication = new Medication
            {
                Id = 1,
                PatientId = 1,
                Name = "Old Medication",
                Dosage = "50 mg",
                Frequency = "Twice daily",
                StartDate = new DateTime(2026, 7, 1),
                EndDate = new DateTime(2026, 8, 1)
            };

            var request = new UpdateMedicationRequest
            {
                Name = "Aspirin",
                Dosage = "81 mg",
                Frequency = "Once daily",
                StartDate = new DateTime(2026, 8, 1),
                EndDate = new DateTime(2026, 9, 1)
            };

            _mockRepository
                .Setup(r => r.GetByIdAsync(1))
                .ReturnsAsync(medication);

            _mockRepository
                .Setup(r => r.SaveChangesAsync())
                .Returns(Task.CompletedTask);

            // Act
            var result = await _service.UpdateAsync(1, request);

            // Assert
            Assert.True(result);

            Assert.Equal("Aspirin", medication.Name);
            Assert.Equal("81 mg", medication.Dosage);
            Assert.Equal("Once daily", medication.Frequency);
            Assert.Equal(new DateTime(2026, 8, 1), medication.StartDate);
            Assert.Equal(new DateTime(2026, 9, 1), medication.EndDate);

            _mockRepository.Verify(
                r => r.GetByIdAsync(1),
                Times.Once);

            _mockRepository.Verify(
                r => r.SaveChangesAsync(),
                Times.Once);
        }

        [Fact]
        public async Task UpdateAsync_WhenMedicationDoesNotExist_ReturnsFalse()
        {
            // Arrange
            var request = new UpdateMedicationRequest
            {
                Name = "Aspirin",
                Dosage = "81 mg",
                Frequency = "Once daily",
                StartDate = new DateTime(2026, 8, 1),
                EndDate = new DateTime(2026, 9, 1)
            };

            _mockRepository
                .Setup(r => r.GetByIdAsync(999))
                .ReturnsAsync((Medication?)null);

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
        public async Task DeleteAsync_WhenMedicationExists_RemovesMedicationAndReturnsTrue()
        {
            // Arrange
            var medication = new Medication
            {
                Id = 1,
                PatientId = 1,
                Name = "Aspirin",
                Dosage = "81 mg",
                Frequency = "Once daily",
                StartDate = new DateTime(2026, 8, 1),
                EndDate = new DateTime(2026, 9, 1)
            };

            _mockRepository
                .Setup(r => r.GetByIdAsync(1))
                .ReturnsAsync(medication);

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
                r => r.Remove(medication),
                Times.Once);

            _mockRepository.Verify(
                r => r.SaveChangesAsync(),
                Times.Once);
        }

        [Fact]
        public async Task DeleteAsync_WhenMedicationDoesNotExist_ReturnsFalse()
        {
            // Arrange
            _mockRepository
                .Setup(r => r.GetByIdAsync(999))
                .ReturnsAsync((Medication?)null);

            // Act
            var result = await _service.DeleteAsync(999);

            // Assert
            Assert.False(result);

            _mockRepository.Verify(
                r => r.GetByIdAsync(999),
                Times.Once);

            _mockRepository.Verify(
                r => r.Remove(It.IsAny<Medication>()),
                Times.Never);

            _mockRepository.Verify(
                r => r.SaveChangesAsync(),
                Times.Never);
        }
    }
}