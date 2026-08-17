using CardiacPatientMonitoringSystem.API.Models;
using CardiacPatientMonitoringSystem.API.Repositories.Interfaces;
using CardiacPatientMonitoringSystem.API.Services.Classes;
using Moq;

namespace CardiacPatientMonitoringSystem.Tests
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
    }
}