using CardiacPatientMonitoringSystem.API.Services.Classes;

namespace CardiacPatientMonitoringSystem.Tests
{
    public class VitalSignServiceTests
    {
        private readonly VitalSignService _service;

        public VitalSignServiceTests()
        {
            _service = new VitalSignService(null!);
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
        public void GetHeartRateStatus_ShouldReturnExpectedStatus(int heartRate,string expectedStatus)
        {
            // Act
            var result = _service.GetHeartRateStatus(heartRate);

            // Assert
            Assert.Equal(expectedStatus, result);
        }

    }
}