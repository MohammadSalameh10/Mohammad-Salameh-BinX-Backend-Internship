using CardiacPatientMonitoringSystem.API.Data;
using CardiacPatientMonitoringSystem.API.DTOs.Responses;
using CardiacPatientMonitoringSystem.API.Models;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Security.Claims;
using System.Text;

namespace CardiacPatientMonitoringSystem.Tests.Integration
{
    public class VitalSignsApiTests
        : IClassFixture<CustomWebApplicationFactory>
    {
        private readonly HttpClient _client;
        private readonly CustomWebApplicationFactory _factory;

        public VitalSignsApiTests(CustomWebApplicationFactory factory)
        {
            _factory = factory;
            _client = factory.CreateClient();

            SeedTestData();
        }

        private void SeedTestData()
        {
            using var scope = _factory.Services.CreateScope();

            var context = scope.ServiceProvider
                .GetRequiredService<ApplicationDbContext>();

            context.VitalSigns.RemoveRange(context.VitalSigns);

            context.VitalSigns.Add(new VitalSign
            {
                Id = 1,
                PatientId = 1,
                HeartRate = 75,
                SystolicBloodPressure = 120,
                DiastolicBloodPressure = 80,
                OxygenSaturation = 98,
                RecordedAt = new DateTime(2026, 8, 18, 10, 0, 0)
            });

            context.SaveChanges();
        }

        private string GenerateAdminToken()
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, "test-admin-id"),
                new Claim(ClaimTypes.Email, "admin@test.com"),
                new Claim(ClaimTypes.Role, "Admin")
            };

            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(
                    "CardiacPatientMonitoringSystemSuperSecretKey2026"));

            var credentials = new SigningCredentials(
                key,
                SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: "CardiacPatientMonitoringSystemAPI",
                audience: "CardiacPatientMonitoringSystemClient",
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(30),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler()
                .WriteToken(token);
        }

        [Fact]
        public async Task GetById_WhenVitalSignExists_ReturnsOkWithVitalSign()
        {
            // Arrange
            var token = GenerateAdminToken();

            _client.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", token);

            // Act
            var response = await _client.GetAsync("/api/VitalSigns/1");

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            var vitalSign = await response.Content
                .ReadFromJsonAsync<VitalSignResponse>();

            Assert.NotNull(vitalSign);
            Assert.Equal(1, vitalSign.Id);
            Assert.Equal(1, vitalSign.PatientId);
            Assert.Equal(75, vitalSign.HeartRate);
            Assert.Equal(120, vitalSign.SystolicBloodPressure);
            Assert.Equal(80, vitalSign.DiastolicBloodPressure);
            Assert.Equal(98, vitalSign.OxygenSaturation);
            Assert.Equal(
                new DateTime(2026, 8, 18, 10, 0, 0),
                vitalSign.RecordedAt);
        }

        [Fact]
        public async Task GetById_WhenVitalSignDoesNotExist_ReturnsNotFound()
        {
            // Arrange
            var token = GenerateAdminToken();

            _client.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", token);

            // Act
            var response = await _client.GetAsync("/api/VitalSigns/99999");

            // Assert
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Fact]
        public async Task GetById_WithoutToken_ReturnsUnauthorized()
        {
            // Arrange
            _client.DefaultRequestHeaders.Authorization = null;

            // Act
            var response = await _client.GetAsync("/api/VitalSigns/1");

            // Assert
            Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
        }
    }
}