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
    public class AppointmentsControllerTests
    {
        private readonly Mock<IAppointmentService> _mockService;
        private readonly AppointmentsController _controller;

        public AppointmentsControllerTests()
        {
            _mockService = new Mock<IAppointmentService>();
            _controller = new AppointmentsController(_mockService.Object);
        }

        [Fact]
        public async Task GetById_WhenAppointmentExists_ReturnsOkWithAppointment()
        {
            // Arrange
            var appointment = new AppointmentResponse
            {
                Id = 1,
                PatientId = 1,
                DoctorId = 1,
                AppointmentDate = new DateTime(2026, 9, 1, 10, 0, 0),
                Reason = "Routine cardiac follow-up",
                Notes = "Routine check-up"
            };

            var user = new ClaimsPrincipal(
                new ClaimsIdentity(
                    new[]
                    {
                new Claim(ClaimTypes.Role, "Admin")
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
                .Setup(s => s.GetByIdAsync(1))
                .ReturnsAsync(appointment);

            // Act
            var result = await _controller.GetById(1);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);

            Assert.Equal(200, okResult.StatusCode);
            Assert.Equal(appointment, okResult.Value);

            _mockService.Verify(
                s => s.GetByIdAsync(1),
                Times.Once);
        }

        [Fact]
        public async Task GetById_WhenAppointmentDoesNotExist_ReturnsNotFound()
        {
            // Arrange
            _mockService
                .Setup(s => s.GetByIdAsync(999))
                .ReturnsAsync((AppointmentResponse?)null);

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

            var request = new CreateAppointmentRequest
            {
                AppointmentDate = new DateTime(2026, 9, 1, 10, 0, 0),
                Reason = "Routine cardiac follow-up",
                Notes = "Routine check-up"
            };

            var response = new AppointmentResponse
            {
                Id = 1,
                PatientId = 1,
                AppointmentDate = new DateTime(2026, 9, 1, 10, 0, 0),
                Reason = "Routine cardiac follow-up",
                Notes = "Routine check-up"
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

            Assert.Equal(
                nameof(AppointmentsController.GetById),
                createdResult.ActionName);

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
            var request = new CreateAppointmentRequest
            {
                AppointmentDate = new DateTime(2026, 9, 1, 10, 0, 0),
                Reason = "Routine cardiac follow-up",
                Notes = "Routine check-up"
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
                    It.IsAny<CreateAppointmentRequest>()),
                Times.Never);
        }

        [Fact]
        public async Task Create_WhenPatientProfileDoesNotExist_ReturnsBadRequest()
        {
            // Arrange
            var userId = "user-123";

            var request = new CreateAppointmentRequest
            {
                DoctorId = 1,
                AppointmentDate = new DateTime(2026, 9, 1, 10, 0, 0),
                Reason = "Routine cardiac follow-up",
                Notes = "Routine check-up"
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
                .ReturnsAsync((AppointmentResponse?)null);

            // Act
            var result = await _controller.Create(request);

            // Assert
            var badRequestResult =
                Assert.IsType<BadRequestObjectResult>(result);

            Assert.Equal(
                "Patient profile or doctor not found.",
                badRequestResult.Value);

            _mockService.Verify(
                s => s.CreateAsync(userId, request),
                Times.Once);
        }

        [Fact]
        public async Task Update_WhenAppointmentExists_ReturnsOk()
        {
            // Arrange
            var request = new UpdateAppointmentRequest
            {
                AppointmentDate = new DateTime(2026, 9, 10, 11, 0, 0),
                Reason = "Cardiac follow-up",
                Notes = "Updated appointment notes"
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
        public async Task Update_WhenAppointmentDoesNotExist_ReturnsNotFound()
        {
            // Arrange
            var request = new UpdateAppointmentRequest
            {
                AppointmentDate = new DateTime(2026, 9, 10, 11, 0, 0),
                Reason = "Cardiac follow-up",
                Notes = "Updated appointment notes"
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
        public async Task Delete_WhenAppointmentExists_ReturnsNoContent()
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
        public async Task Delete_WhenAppointmentDoesNotExist_ReturnsNotFound()
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