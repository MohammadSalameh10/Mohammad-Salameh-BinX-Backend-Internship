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
    public class AppointmentsApiTests
        : IClassFixture<CustomWebApplicationFactory>
    {
        private readonly HttpClient _client;
        private readonly CustomWebApplicationFactory _factory;

        public AppointmentsApiTests(CustomWebApplicationFactory factory)
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

            context.Appointments.RemoveRange(context.Appointments);
            context.Patients.RemoveRange(context.Patients);

            var patient = new Patient
            {
                Id = 1,
                UserId = "patient-user-1",
                FullName = "Patient One",
                DateOfBirth = new DateTime(1995, 1, 1),
                Gender = "Male",
                PhoneNumber = "0599000001",
                BloodType = "O+"
            };

            context.Patients.Add(patient);

            context.Appointments.Add(new Appointment
            {
                Id = 1,
                PatientId = 1,
                AppointmentDate = new DateTime(2026, 9, 20, 10, 0, 0),
                Reason = "Cardiac follow-up",
                Notes = "Integration test appointment"
            });

            context.SaveChanges();
        }

        private string GeneratePatientToken(int patientId)
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, "patient-user-1"),
                new Claim(ClaimTypes.Email, "patient@test.com"),
                new Claim(ClaimTypes.Role, "Patient"),
                new Claim("PatientId", patientId.ToString())
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
        public async Task GetById_WhenPatientOwnsAppointment_ReturnsOk()
        {
            // Arrange
            var token = GeneratePatientToken(1);

            _client.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", token);

            // Act
            var response = await _client.GetAsync(
                "/api/Appointments/1");

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            var appointment = await response.Content
                .ReadFromJsonAsync<AppointmentResponse>();

            Assert.NotNull(appointment);
            Assert.Equal(1, appointment.Id);
            Assert.Equal(1, appointment.PatientId);
        }

        [Fact]
        public async Task GetById_WhenPatientDoesNotOwnAppointment_ReturnsNotFound()
        {
            // Arrange
            var token = GeneratePatientToken(2);

            _client.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", token);

            // Act
            var response = await _client.GetAsync(
                "/api/Appointments/1");

            // Assert
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }
    }
}