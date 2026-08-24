using CardiacPatientMonitoringSystem.API.DTOs.Requests;
using CardiacPatientMonitoringSystem.API.Models;
using CardiacPatientMonitoringSystem.API.Repositories.Interfaces;
using CardiacPatientMonitoringSystem.API.Services.Classes;
using Moq;

namespace CardiacPatientMonitoringSystem.Tests.Services
{
    public class VitalSignServiceTests
    {
        [Fact]
        public async Task CreateAsync_ReturnsVitalSign_WhenPatientExists()
        {
            // Arrange
            var mockRepository = new Mock<IVitalSignRepository>();

            var patient = new Patient
            {
                Id = 1,
                UserId = "user-1"
            };

            var request = new CreateVitalSignRequest
            {
                HeartRate = 72,
                SystolicBloodPressure = 120,
                DiastolicBloodPressure = 80,
                OxygenSaturation = 98,
                RecordedAt = new DateTime(2026, 8, 10, 9, 0, 0)
            };

            mockRepository
                .Setup(r => r.GetPatientByUserIdAsync("user-1"))
                .ReturnsAsync(patient);

            var service = new VitalSignService(mockRepository.Object);

            // Act
            var result = await service.CreateAsync("user-1", request);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(1, result.PatientId);
            Assert.Equal(72, result.HeartRate);

            mockRepository.Verify(
                r => r.AddAsync(It.IsAny<VitalSign>()),
                Times.Once);

            mockRepository.Verify(
                r => r.SaveChangesAsync(),
                Times.Once);
        }

        [Fact]
        public async Task CreateAsync_ReturnsNull_WhenPatientDoesNotExist()
        {
            // Arrange
            var mockRepository = new Mock<IVitalSignRepository>();

            var request = new CreateVitalSignRequest
            {
                HeartRate = 72,
                SystolicBloodPressure = 120,
                DiastolicBloodPressure = 80,
                OxygenSaturation = 98,
                RecordedAt = new DateTime(2026, 8, 10, 9, 0, 0)
            };

            mockRepository
                .Setup(r => r.GetPatientByUserIdAsync("user-1"))
                .ReturnsAsync((Patient?)null);

            var service = new VitalSignService(mockRepository.Object);

            // Act
            var result = await service.CreateAsync("user-1", request);

            // Assert
            Assert.Null(result);

            mockRepository.Verify(
                r => r.AddAsync(It.IsAny<VitalSign>()),
                Times.Never);

            mockRepository.Verify(
                r => r.SaveChangesAsync(),
                Times.Never);
        }

        [Fact]
        public async Task UpdateAsync_ReturnsTrue_WhenVitalSignExists()
        {
            // Arrange
            var mockRepository = new Mock<IVitalSignRepository>();

            var vitalSign = new VitalSign
            {
                Id = 1,
                PatientId = 1,
                HeartRate = 72,
                SystolicBloodPressure = 120,
                DiastolicBloodPressure = 80,
                OxygenSaturation = 98,
                RecordedAt = new DateTime(2026, 8, 10, 9, 0, 0)
            };

            var request = new UpdateVitalSignRequest
            {
                HeartRate = 80,
                SystolicBloodPressure = 125,
                DiastolicBloodPressure = 82,
                OxygenSaturation = 97,
                RecordedAt = new DateTime(2026, 8, 11, 9, 0, 0)
            };

            mockRepository
                .Setup(r => r.GetByIdAsync(1))
                .ReturnsAsync(vitalSign);

            var service = new VitalSignService(mockRepository.Object);

            // Act
            var result = await service.UpdateAsync(1, request);

            // Assert
            Assert.True(result);

            Assert.Equal(80, vitalSign.HeartRate);
            Assert.Equal(125, vitalSign.SystolicBloodPressure);
            Assert.Equal(82, vitalSign.DiastolicBloodPressure);
            Assert.Equal(97, vitalSign.OxygenSaturation);

            mockRepository.Verify(
                r => r.SaveChangesAsync(),
                Times.Once);
        }

        [Fact]
        public async Task UpdateAsync_ReturnsFalse_WhenVitalSignDoesNotExist()
        {
            // Arrange
            var mockRepository = new Mock<IVitalSignRepository>();

            var request = new UpdateVitalSignRequest
            {
                HeartRate = 80,
                SystolicBloodPressure = 125,
                DiastolicBloodPressure = 82,
                OxygenSaturation = 97,
                RecordedAt = new DateTime(2026, 8, 11, 9, 0, 0)
            };

            mockRepository
                .Setup(r => r.GetByIdAsync(999))
                .ReturnsAsync((VitalSign?)null);

            var service = new VitalSignService(mockRepository.Object);

            // Act
            var result = await service.UpdateAsync(999, request);

            // Assert
            Assert.False(result);

            mockRepository.Verify(
                r => r.SaveChangesAsync(),
                Times.Never);
        }

        [Fact]
        public async Task DeleteAsync_ReturnsTrue_WhenVitalSignExists()
        {
            // Arrange
            var mockRepository = new Mock<IVitalSignRepository>();

            var vitalSign = new VitalSign
            {
                Id = 1,
                PatientId = 1,
                HeartRate = 72,
                SystolicBloodPressure = 120,
                DiastolicBloodPressure = 80,
                OxygenSaturation = 98,
                RecordedAt = new DateTime(2026, 8, 10, 9, 0, 0)
            };

            mockRepository
                .Setup(r => r.GetByIdAsync(1))
                .ReturnsAsync(vitalSign);

            var service = new VitalSignService(mockRepository.Object);

            // Act
            var result = await service.DeleteAsync(1);

            // Assert
            Assert.True(result);

            mockRepository.Verify(
                r => r.Remove(vitalSign),
                Times.Once);

            mockRepository.Verify(
                r => r.SaveChangesAsync(),
                Times.Once);
        }

        [Fact]
        public async Task DeleteAsync_ReturnsFalse_WhenVitalSignDoesNotExist()
        {
            // Arrange
            var mockRepository = new Mock<IVitalSignRepository>();

            mockRepository
                .Setup(r => r.GetByIdAsync(999))
                .ReturnsAsync((VitalSign?)null);

            var service = new VitalSignService(mockRepository.Object);

            // Act
            var result = await service.DeleteAsync(999);

            // Assert
            Assert.False(result);

            mockRepository.Verify(
                r => r.Remove(It.IsAny<VitalSign>()),
                Times.Never);

            mockRepository.Verify(
                r => r.SaveChangesAsync(),
                Times.Never);
        }
    }
}