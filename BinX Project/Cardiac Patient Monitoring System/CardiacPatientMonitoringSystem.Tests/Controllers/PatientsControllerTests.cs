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
    public class PatientsControllerTests
    {
        private readonly Mock<IPatientService> _mockService;
        private readonly PatientsController _controller;

        public PatientsControllerTests()
        {
            _mockService = new Mock<IPatientService>();
            _controller = new PatientsController(_mockService.Object);
        }

        [Fact]
        public async Task GetById_WhenPatientExists_ReturnsOkWithPatient()
        {
            // Arrange
            var patient = new PatientResponse
            {
                Id = 1,
                UserId = "user-123",
                FullName = "Ahmad Khalil",
                DateOfBirth = new DateTime(1998, 4, 10),
                Gender = "Male",
                PhoneNumber = "0599222333",
                BloodType = "A+"
            };

            _mockService
                .Setup(s => s.GetByIdAsync(1))
                .ReturnsAsync(patient);

            // Act
            var result = await _controller.GetById(1);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);

            Assert.Equal(200, okResult.StatusCode);
            Assert.Equal(patient, okResult.Value);

            _mockService.Verify(
                s => s.GetByIdAsync(1),
                Times.Once);
        }

        [Fact]
        public async Task GetById_WhenPatientDoesNotExist_ReturnsNotFound()
        {
            // Arrange
            _mockService
                .Setup(s => s.GetByIdAsync(999))
                .ReturnsAsync((PatientResponse?)null);

            // Act
            var result = await _controller.GetById(999);

            // Assert
            Assert.IsType<NotFoundResult>(result);

            _mockService.Verify(
                s => s.GetByIdAsync(999),
                Times.Once);
        }

        [Fact]
        public async Task Create_WhenPatientDoesNotExist_ReturnsCreatedAtAction()
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

            var response = new PatientResponse
            {
                Id = 1,
                UserId = userId,
                FullName = "Ahmad Khalil",
                DateOfBirth = new DateTime(1998, 4, 10),
                Gender = "Male",
                PhoneNumber = "0599222333",
                BloodType = "A+"
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

            Assert.Equal(nameof(PatientsController.GetById), createdResult.ActionName);
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
            var request = new CreatePatientRequest
            {
                FullName = "Ahmad Khalil",
                DateOfBirth = new DateTime(1998, 4, 10),
                Gender = "Male",
                PhoneNumber = "0599222333",
                BloodType = "A+"
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
                    It.IsAny<CreatePatientRequest>()),
                Times.Never);
        }

        [Fact]
        public async Task Create_WhenPatientProfileAlreadyExists_ReturnsBadRequest()
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
                .ReturnsAsync((PatientResponse?)null);

            // Act
            var result = await _controller.Create(request);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);

            Assert.Equal(
                "Patient profile already exists.",
                badRequestResult.Value);

            _mockService.Verify(
                s => s.CreateAsync(userId, request),
                Times.Once);
        }

        [Fact]
        public async Task Update_WhenPatientExists_ReturnsOk()
        {
            // Arrange
            var request = new UpdatePatientRequest
            {
                FullName = "Ahmad Khalil",
                DateOfBirth = new DateTime(1998, 4, 10),
                Gender = "Male",
                PhoneNumber = "0599222333",
                BloodType = "A+"
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
        public async Task Update_WhenPatientDoesNotExist_ReturnsNotFound()
        {
            // Arrange
            var request = new UpdatePatientRequest
            {
                FullName = "Ahmad Khalil",
                DateOfBirth = new DateTime(1998, 4, 10),
                Gender = "Male",
                PhoneNumber = "0599222333",
                BloodType = "A+"
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
        public async Task Delete_WhenPatientExists_ReturnsNoContent()
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
        public async Task Delete_WhenPatientDoesNotExist_ReturnsNotFound()
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