using CardiacPatientMonitoringSystem.API.DTOs.Requests;
using CardiacPatientMonitoringSystem.API.Models;
using CardiacPatientMonitoringSystem.API.Repositories.Interfaces;
using CardiacPatientMonitoringSystem.API.Services.Classes;
using Moq;

namespace CardiacPatientMonitoringSystem.Tests.Services
{
    public class VitalSignServiceTests
    {
        private readonly Mock<IVitalSignRepository> _mockRepository;
        private readonly VitalSignService _service;

        public VitalSignServiceTests()
        {
            _mockRepository = new Mock<IVitalSignRepository>();
            _service = new VitalSignService(_mockRepository.Object);
        }
        [Fact]
        public void GetHeartRateStatus_ShouldReturnLow_WhenHeartRateIsBelow60()
        {
            // Arrange
            int heartRate = 50;

            // Act
            var result = _service.GetHeartRateStatus(heartRate);

            // Assert
            Assert.Equal("Low", result);
        }

        [Fact]
        public void GetHeartRateStatus_ShouldReturnNormal_WhenHeartRateIsBetween60And100()
        {
            // Arrange
            int heartRate = 75;

            // Act
            var result = _service.GetHeartRateStatus(heartRate);

            // Assert
            Assert.Equal("Normal", result);
        }

        [Fact]
        public void GetHeartRateStatus_ShouldReturnHigh_WhenHeartRateIsAbove100()
        {
            // Arrange
            int heartRate = 120;

            // Act
            var result = _service.GetHeartRateStatus(heartRate);

            // Assert
            Assert.Equal("High", result);
        }

        [Theory]
        [InlineData(40, "Low")]
        [InlineData(80, "Normal")]
        [InlineData(150, "High")]
        public void GetHeartRateStatus_ShouldReturnExpectedStatus(int heartRate, string expectedStatus)
        {
            // Act
            var result = _service.GetHeartRateStatus(heartRate);

            // Assert
            Assert.Equal(expectedStatus, result);
        }

        [Fact]
        public async Task GetByIdAsync_WhenVitalSignExists_ReturnsVitalSignResponse()
        {
            // Arrange
            var vitalSign = new VitalSign
            {
                Id = 1,
                PatientId = 1,
                HeartRate = 75,
                SystolicBloodPressure = 120,
                DiastolicBloodPressure = 80,
                OxygenSaturation = 98,
                RecordedAt = DateTime.Now
            };

            _mockRepository
                .Setup(r => r.GetByIdAsync(1))
                .ReturnsAsync(vitalSign);

            // Act
            var result = await _service.GetByIdAsync(1);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(1, result.Id);
            Assert.Equal(75, result.HeartRate);

            _mockRepository.Verify(
                r => r.GetByIdAsync(1),
                Times.Once);
        }

        [Fact]
        public async Task GetByIdAsync_WhenRepositoryThrowsException_ThrowsInvalidOperationException()
        {
            // Arrange
            _mockRepository
                .Setup(r => r.GetByIdAsync(1))
                .ThrowsAsync(new InvalidOperationException("Database error"));

            // Act & Assert
            await Assert.ThrowsAsync<InvalidOperationException>(
                () => _service.GetByIdAsync(1));

            _mockRepository.Verify(
                r => r.GetByIdAsync(1),
                Times.Once);
        }

        [Fact]
        public async Task CreateAsync_WhenPatientExists_CreatesVitalSignAndReturnsResponse()
        {
            // Arrange
            var userId = "user-123";

            var patient = new Patient
            {
                Id = 1
            };

            var request = new CreateVitalSignRequest
            {
                HeartRate = 75,
                SystolicBloodPressure = 120,
                DiastolicBloodPressure = 80,
                OxygenSaturation = 98,
                RecordedAt = DateTime.Now
            };

            _mockRepository
                .Setup(r => r.GetPatientByUserIdAsync(userId))
                .ReturnsAsync(patient);

            _mockRepository
                .Setup(r => r.AddAsync(It.IsAny<VitalSign>()))
                .Returns(Task.CompletedTask);

            _mockRepository
                .Setup(r => r.SaveChangesAsync())
                .Returns(Task.CompletedTask);

            // Act
            var result = await _service.CreateAsync(userId, request);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(1, result.PatientId);
            Assert.Equal(75, result.HeartRate);
            Assert.Equal(120, result.SystolicBloodPressure);
            Assert.Equal(80, result.DiastolicBloodPressure);
            Assert.Equal(98, result.OxygenSaturation);

            _mockRepository.Verify(
                r => r.AddAsync(It.Is<VitalSign>(v =>
                    v.PatientId == 1 &&
                    v.HeartRate == 75 &&
                    v.SystolicBloodPressure == 120 &&
                    v.DiastolicBloodPressure == 80 &&
                    v.OxygenSaturation == 98)),
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

            var request = new CreateVitalSignRequest
            {
                HeartRate = 75,
                SystolicBloodPressure = 120,
                DiastolicBloodPressure = 80,
                OxygenSaturation = 98,
                RecordedAt = DateTime.Now
            };

            _mockRepository
                .Setup(r => r.GetPatientByUserIdAsync(userId))
                .ReturnsAsync((Patient?)null);

            // Act
            var result = await _service.CreateAsync(userId, request);

            // Assert
            Assert.Null(result);

            _mockRepository.Verify(
                r => r.AddAsync(It.IsAny<VitalSign>()),
                Times.Never);

            _mockRepository.Verify(
                r => r.SaveChangesAsync(),
                Times.Never);
        }

        [Fact]
        public async Task UpdateAsync_WhenVitalSignExists_UpdatesVitalSignAndReturnsTrue()
        {
            // Arrange
            var vitalSign = new VitalSign
            {
                Id = 1,
                PatientId = 1,
                HeartRate = 70,
                SystolicBloodPressure = 110,
                DiastolicBloodPressure = 70,
                OxygenSaturation = 95,
                RecordedAt = DateTime.Now.AddMinutes(-10)
            };

            var request = new UpdateVitalSignRequest
            {
                HeartRate = 80,
                SystolicBloodPressure = 120,
                DiastolicBloodPressure = 80,
                OxygenSaturation = 98,
                RecordedAt = DateTime.Now
            };

            _mockRepository
                .Setup(r => r.GetByIdAsync(1))
                .ReturnsAsync(vitalSign);

            _mockRepository
                .Setup(r => r.SaveChangesAsync())
                .Returns(Task.CompletedTask);

            // Act
            var result = await _service.UpdateAsync(1, request);

            // Assert
            Assert.True(result);

            Assert.Equal(80, vitalSign.HeartRate);
            Assert.Equal(120, vitalSign.SystolicBloodPressure);
            Assert.Equal(80, vitalSign.DiastolicBloodPressure);
            Assert.Equal(98, vitalSign.OxygenSaturation);

            _mockRepository.Verify(
                r => r.GetByIdAsync(1),
                Times.Once);

            _mockRepository.Verify(
                r => r.SaveChangesAsync(),
                Times.Once);
        }

        [Fact]
        public async Task UpdateAsync_WhenVitalSignDoesNotExist_ReturnsFalse()
        {
            // Arrange
            var request = new UpdateVitalSignRequest
            {
                HeartRate = 80,
                SystolicBloodPressure = 120,
                DiastolicBloodPressure = 80,
                OxygenSaturation = 98,
                RecordedAt = DateTime.Now
            };

            _mockRepository
                .Setup(r => r.GetByIdAsync(999))
                .ReturnsAsync((VitalSign?)null);

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
        public async Task DeleteAsync_WhenVitalSignExists_RemovesVitalSignAndReturnsTrue()
        {
            // Arrange
            var vitalSign = new VitalSign
            {
                Id = 1,
                PatientId = 1,
                HeartRate = 75,
                SystolicBloodPressure = 120,
                DiastolicBloodPressure = 80,
                OxygenSaturation = 98,
                RecordedAt = DateTime.Now
            };

            _mockRepository
                .Setup(r => r.GetByIdAsync(1))
                .ReturnsAsync(vitalSign);

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
                r => r.Remove(vitalSign),
                Times.Once);

            _mockRepository.Verify(
                r => r.SaveChangesAsync(),
                Times.Once);
        }

        [Fact]
        public async Task DeleteAsync_WhenVitalSignDoesNotExist_ReturnsFalse()
        {
            // Arrange
            _mockRepository
                .Setup(r => r.GetByIdAsync(999))
                .ReturnsAsync((VitalSign?)null);

            // Act
            var result = await _service.DeleteAsync(999);

            // Assert
            Assert.False(result);

            _mockRepository.Verify(
                r => r.GetByIdAsync(999),
                Times.Once);

            _mockRepository.Verify(
                r => r.Remove(It.IsAny<VitalSign>()),
                Times.Never);

            _mockRepository.Verify(
                r => r.SaveChangesAsync(),
                Times.Never);
        }
    }
}