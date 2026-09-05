using CardiacPatientMonitoringSystem.API.Controllers;
using CardiacPatientMonitoringSystem.API.DTOs.Requests;
using CardiacPatientMonitoringSystem.API.DTOs.Responses;
using CardiacPatientMonitoringSystem.API.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace CardiacPatientMonitoringSystem.Tests.Controllers
{
    public class AuthsControllerTests
    {
        private readonly Mock<IAuthService> _mockService;
        private readonly AuthController _controller;

        public AuthsControllerTests()
        {
            _mockService = new Mock<IAuthService>();
            _controller = new AuthController(_mockService.Object);
        }

        [Fact]
        public async Task Register_WhenRegistrationSucceeds_ReturnsCreated()
        {
            // Arrange
            var request = new RegisterRequest
            {
                Email = "patient@test.com",
                Password = "Patient@123"
            };

            _mockService
                .Setup(s => s.RegisterAsync(request))
                .ReturnsAsync(IdentityResult.Success);

            // Act
            var result = await _controller.Register(request);

            // Assert
            var statusCodeResult = Assert.IsType<StatusCodeResult>(result);

            Assert.Equal(201, statusCodeResult.StatusCode);

            _mockService.Verify(
                s => s.RegisterAsync(request),
                Times.Once);
        }

        [Fact]
        public async Task Register_WhenRegistrationFails_ReturnsBadRequest()
        {
            // Arrange
            var request = new RegisterRequest
            {
                Email = "patient@test.com",
                Password = "Patient@123"
            };

            var identityError = new IdentityError
            {
                Code = "DuplicateEmail",
                Description = "Email is already registered."
            };

            var identityResult = IdentityResult.Failed(identityError);

            _mockService
                .Setup(s => s.RegisterAsync(request))
                .ReturnsAsync(identityResult);

            // Act
            var result = await _controller.Register(request);

            // Assert
            var badRequestResult =
                Assert.IsType<BadRequestObjectResult>(result);

            Assert.Equal(identityResult.Errors, badRequestResult.Value);

            _mockService.Verify(
                s => s.RegisterAsync(request),
                Times.Once);
        }

        [Fact]
        public async Task Login_WhenCredentialsAreValid_ReturnsOkWithToken()
        {
            // Arrange
            var request = new LoginRequest
            {
                Email = "admin@test.com",
                Password = "Admin@123"
            };

            var response = new LoginResponse
            {
                Token = "test-jwt-token"
            };

            _mockService
                .Setup(s => s.LoginAsync(request))
                .ReturnsAsync(response);

            // Act
            var result = await _controller.Login(request);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);

            Assert.Equal(200, okResult.StatusCode);
            Assert.Equal(response, okResult.Value);

            _mockService.Verify(
                s => s.LoginAsync(request),
                Times.Once);
        }

        [Fact]
        public async Task Login_WhenCredentialsAreInvalid_ReturnsUnauthorized()
        {
            // Arrange
            var request = new LoginRequest
            {
                Email = "admin@test.com",
                Password = "WrongPassword"
            };

            _mockService
                .Setup(s => s.LoginAsync(request))
                .ReturnsAsync((LoginResponse?)null);

            // Act
            var result = await _controller.Login(request);

            // Assert
            Assert.IsType<UnauthorizedResult>(result);

            _mockService.Verify(
                s => s.LoginAsync(request),
                Times.Once);
        }
    }
}