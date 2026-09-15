using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;

namespace CardiacPatientMonitoringSystem.Tests.Integration
{
    public class PatientsApiTests
        : IClassFixture<CustomWebApplicationFactory>
    {
        private readonly HttpClient _client;

        public PatientsApiTests(CustomWebApplicationFactory factory)
        {
            _client = factory.CreateClient();
        }

        private string GenerateToken(string role)
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, "test-user-id"),
                new Claim(ClaimTypes.Email, "test@cardiac.com"),
                new Claim(ClaimTypes.Role, role)
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
        public async Task GetAll_WhenUserIsAdmin_ReturnsOk()
        {
            // Arrange
            var token = GenerateToken("Admin");

            _client.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", token);

            // Act
            var response = await _client.GetAsync(
                "/api/Patients");

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task GetAll_WhenUserIsPatient_ReturnsForbidden()
        {
            // Arrange
            var token = GenerateToken("Patient");

            _client.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", token);

            // Act
            var response = await _client.GetAsync(
                "/api/Patients");

            // Assert
            Assert.Equal(HttpStatusCode.Forbidden, response.StatusCode);
        }
    }
}