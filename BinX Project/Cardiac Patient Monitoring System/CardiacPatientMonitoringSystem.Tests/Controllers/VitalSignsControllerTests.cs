using CardiacPatientMonitoringSystem.API.Controllers;
using CardiacPatientMonitoringSystem.API.DTOs.Requests;
using CardiacPatientMonitoringSystem.API.DTOs.Responses;
using CardiacPatientMonitoringSystem.API.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Security.Claims;

namespace CardiacPatientMonitoringSystem.Tests.Controllers
{
    public class VitalSignsControllerTests
    {
        private readonly Mock<IVitalSignService> _mockService;
        private readonly VitalSignsController _controller;

        public VitalSignsControllerTests()
        {
            _mockService = new Mock<IVitalSignService>();
            _controller = new VitalSignsController(_mockService.Object);
        }

        [Fact]
        public async Task GetById_WhenVitalSignExists_ReturnsOkWithVitalSign()
        {
            // Arrange
            var vitalSign = new VitalSignResponse
            {
                Id = 1,
                PatientId = 1,
                HeartRate = 75,
                SystolicBloodPressure = 120,
                DiastolicBloodPressure = 80,
                OxygenSaturation = 98,
                RecordedAt = new DateTime(2026, 8, 18, 10, 0, 0)
            };

            _mockService
                .Setup(s => s.GetByIdAsync(1))
                .ReturnsAsync(vitalSign);

            // Act
            var result = await _controller.GetById(1);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);

            Assert.Equal(200, okResult.StatusCode);
            Assert.Equal(vitalSign, okResult.Value);

            _mockService.Verify(
                s => s.GetByIdAsync(1),
                Times.Once);
        }

        [Fact]
        public async Task GetById_WhenVitalSignDoesNotExist_ReturnsNotFound()
        {
            // Arrange
            _mockService
                .Setup(s => s.GetByIdAsync(999))
                .ReturnsAsync((VitalSignResponse?)null);

            // Act
            var result = await _controller.GetById(999);

            // Assert
            Assert.IsType<NotFoundResult>(result);

            _mockService.Verify(
                s => s.GetByIdAsync(999),
                Times.Once);
        }

        [Fact]
        public async Task Create_WhenPatientProfileExists_ReturnsCreatedAtAction()
        {
            // Arrange
            var userId = "user-123";

            var request = new CreateVitalSignRequest
            {
                HeartRate = 75,
                SystolicBloodPressure = 120,
                DiastolicBloodPressure = 80,
                OxygenSaturation = 98,
                RecordedAt = new DateTime(2026, 8, 18, 10, 0, 0)
            };

            var response = new VitalSignResponse
            {
                Id = 1,
                PatientId = 1,
                HeartRate = 75,
                SystolicBloodPressure = 120,
                DiastolicBloodPressure = 80,
                OxygenSaturation = 98,
                RecordedAt = new DateTime(2026, 8, 18, 10, 0, 0)
            };

            var user = new ClaimsPrincipal(
                new ClaimsIdentity(
                    new[]
                    {
                new Claim(ClaimTypes.NameIdentifier, userId)
                    },
                    "TestAuth"));

            _controller.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext
                {
                    User = user
                }
            };

            _mockService
                .Setup(s => s.CreateAsync(userId, request))
                .ReturnsAsync(response);

            // Act
            var result = await _controller.Create(request);

            // Assert
            var createdResult = Assert.IsType<CreatedAtActionResult>(result);

            Assert.Equal(nameof(VitalSignsController.GetById), createdResult.ActionName);
            Assert.Equal(1, createdResult.RouteValues!["id"]);
            Assert.Equal(response, createdResult.Value);

            _mockService.Verify(
                s => s.CreateAsync(userId, request),
                Times.Once);
        }

        [Fact]
        public async Task Create_WhenUserIdIsMissing_ReturnsUnauthorized()
        {
            // Arrange
            var request = new CreateVitalSignRequest
            {
                HeartRate = 75,
                SystolicBloodPressure = 120,
                DiastolicBloodPressure = 80,
                OxygenSaturation = 98,
                RecordedAt = new DateTime(2026, 8, 18, 10, 0, 0)
            };

            _controller.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext
                {
                    User = new ClaimsPrincipal(new ClaimsIdentity())
                }
            };

            // Act
            var result = await _controller.Create(request);

            // Assert
            Assert.IsType<UnauthorizedResult>(result);

            _mockService.Verify(
                s => s.CreateAsync(
                    It.IsAny<string>(),
                    It.IsAny<CreateVitalSignRequest>()),
                Times.Never);
        }

        [Fact]
        public async Task Create_WhenPatientProfileDoesNotExist_ReturnsBadRequest()
        {
            // Arrange
            var userId = "user-123";

            var request = new CreateVitalSignRequest
            {
                HeartRate = 75,
                SystolicBloodPressure = 120,
                DiastolicBloodPressure = 80,
                OxygenSaturation = 98,
                RecordedAt = new DateTime(2026, 8, 18, 10, 0, 0)
            };

            var user = new ClaimsPrincipal(
                new ClaimsIdentity(
                    new[]
                    {
                new Claim(ClaimTypes.NameIdentifier, userId)
                    },
                    "TestAuth"));

            _controller.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext
                {
                    User = user
                }
            };

            _mockService
                .Setup(s => s.CreateAsync(userId, request))
                .ReturnsAsync((VitalSignResponse?)null);

            // Act
            var result = await _controller.Create(request);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);

            Assert.Equal(
                "Patient profile not found. Create a patient profile first.",
                badRequestResult.Value);

            _mockService.Verify(
                s => s.CreateAsync(userId, request),
                Times.Once);
        }

        [Fact]
        public async Task Update_WhenVitalSignExists_ReturnsOk()
        {
            // Arrange
            var request = new UpdateVitalSignRequest
            {
                HeartRate = 80,
                SystolicBloodPressure = 125,
                DiastolicBloodPressure = 82,
                OxygenSaturation = 97,
                RecordedAt = new DateTime(2026, 8, 18, 11, 0, 0)
            };

            _mockService
                .Setup(s => s.UpdateAsync(1, request))
                .ReturnsAsync(true);

            // Act
            var result = await _controller.Update(1, request);

            // Assert
            Assert.IsType<OkResult>(result);

            _mockService.Verify(
                s => s.UpdateAsync(1, request),
                Times.Once);
        }

        [Fact]
        public async Task Update_WhenVitalSignDoesNotExist_ReturnsNotFound()
        {
            // Arrange
            var request = new UpdateVitalSignRequest
            {
                HeartRate = 80,
                SystolicBloodPressure = 125,
                DiastolicBloodPressure = 82,
                OxygenSaturation = 97,
                RecordedAt = new DateTime(2026, 8, 18, 11, 0, 0)
            };

            _mockService
                .Setup(s => s.UpdateAsync(999, request))
                .ReturnsAsync(false);

            // Act
            var result = await _controller.Update(999, request);

            // Assert
            Assert.IsType<NotFoundResult>(result);

            _mockService.Verify(
                s => s.UpdateAsync(999, request),
                Times.Once);
        }

        [Fact]
        public async Task Delete_WhenVitalSignExists_ReturnsNoContent()
        {
            // Arrange
            _mockService
                .Setup(s => s.DeleteAsync(1))
                .ReturnsAsync(true);

            // Act
            var result = await _controller.Delete(1);

            // Assert
            Assert.IsType<NoContentResult>(result);

            _mockService.Verify(
                s => s.DeleteAsync(1),
                Times.Once);
        }

        [Fact]
        public async Task Delete_WhenVitalSignDoesNotExist_ReturnsNotFound()
        {
            // Arrange
            _mockService
                .Setup(s => s.DeleteAsync(999))
                .ReturnsAsync(false);

            // Act
            var result = await _controller.Delete(999);

            // Assert
            Assert.IsType<NotFoundResult>(result);

            _mockService.Verify(
                s => s.DeleteAsync(999),
                Times.Once);
        }
    }
}