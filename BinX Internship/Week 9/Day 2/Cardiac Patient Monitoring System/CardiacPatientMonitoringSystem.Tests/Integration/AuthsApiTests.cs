using CardiacPatientMonitoringSystem.API.DTOs.Requests;
using CardiacPatientMonitoringSystem.API.DTOs.Responses;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System.Net;
using System.Net.Http.Json;

namespace CardiacPatientMonitoringSystem.Tests.Integration
{
    public class AuthsApiTests
        : IClassFixture<CustomWebApplicationFactory>
    {
        private readonly HttpClient _client;
        private readonly CustomWebApplicationFactory _factory;

        public AuthsApiTests(CustomWebApplicationFactory factory)
        {
            _factory = factory;
            _client = factory.CreateClient();
        }

        private async Task SeedLoginUserAsync()
        {
            using var scope = _factory.Services.CreateScope();

            var userManager = scope.ServiceProvider
                .GetRequiredService<UserManager<IdentityUser>>();

            const string email = "login-test@cardiac.com";
            const string password = "LoginTest@123";

            var existingUser = await userManager.FindByEmailAsync(email);

            if (existingUser == null)
            {
                var user = new IdentityUser
                {
                    UserName = email,
                    Email = email
                };

                var result = await userManager.CreateAsync(user, password);

                Assert.True(result.Succeeded);
            }
        }

        private async Task SeedPatientRoleAsync()
        {
            using var scope = _factory.Services.CreateScope();

            var roleManager = scope.ServiceProvider
                .GetRequiredService<RoleManager<IdentityRole>>();

            if (!await roleManager.RoleExistsAsync("Patient"))
            {
                var result = await roleManager.CreateAsync(
                    new IdentityRole("Patient"));

                Assert.True(result.Succeeded);
            }
        }

        [Fact]
        public async Task Login_WhenCredentialsAreValid_ReturnsOkWithToken()
        {
            // Arrange
            await SeedLoginUserAsync();

            var request = new LoginRequest
            {
                Email = "login-test@cardiac.com",
                Password = "LoginTest@123"
            };

            // Act
            var response = await _client.PostAsJsonAsync(
                "/api/Auths/login",
                request);

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            var loginResponse = await response.Content
                .ReadFromJsonAsync<LoginResponse>();

            Assert.NotNull(loginResponse);
            Assert.False(string.IsNullOrWhiteSpace(loginResponse.Token));
        }

        [Fact]
        public async Task Login_WhenCredentialsAreInvalid_ReturnsUnauthorized()
        {
            // Arrange
            await SeedLoginUserAsync();

            var request = new LoginRequest
            {
                Email = "login-test@cardiac.com",
                Password = "WrongPassword@123"
            };

            // Act
            var response = await _client.PostAsJsonAsync(
                "/api/Auths/login",
                request);

            // Assert
            Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
        }

        [Fact]
        public async Task Register_WhenRequestIsValid_ReturnsCreated()
        {
            // Arrange
            await SeedPatientRoleAsync();

            var request = new RegisterRequest
            {
                Email = $"register-{Guid.NewGuid()}@cardiac.com",
                Password = "Patient@123",
                FullName = "Integration Test Patient",
                DateOfBirth = new DateTime(1998, 5, 10),
                Gender = "Male",
                PhoneNumber = "0599000001",
                BloodType = "O+"
            };

            // Act
            var response = await _client.PostAsJsonAsync(
                "/api/Auths/register",
                request);

            // Assert
            Assert.Equal(HttpStatusCode.Created, response.StatusCode);
        }

        [Fact]
        public async Task Register_WhenEmailAlreadyExists_ReturnsBadRequest()
        {
            // Arrange
            var email = $"duplicate-{Guid.NewGuid()}@cardiac.com";

            var firstRequest = new RegisterRequest
            {
                Email = email,
                Password = "Patient@123",
                FullName = "First Patient",
                DateOfBirth = new DateTime(1998, 5, 10),
                Gender = "Male",
                PhoneNumber = "0599000002",
                BloodType = "A+"
            };

            var secondRequest = new RegisterRequest
            {
                Email = email,
                Password = "Patient@123",
                FullName = "Second Patient",
                DateOfBirth = new DateTime(1997, 4, 15),
                Gender = "Male",
                PhoneNumber = "0599000003",
                BloodType = "B+"
            };

            await _client.PostAsJsonAsync(
                "/api/Auths/register",
                firstRequest);

            // Act
            var response = await _client.PostAsJsonAsync(
                "/api/Auths/register",
                secondRequest);

            // Assert
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }
    }
}