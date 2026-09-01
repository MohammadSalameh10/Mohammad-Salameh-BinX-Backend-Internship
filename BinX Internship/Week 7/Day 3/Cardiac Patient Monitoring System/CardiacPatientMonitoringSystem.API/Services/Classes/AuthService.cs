using CardiacPatientMonitoringSystem.API.Data;
using CardiacPatientMonitoringSystem.API.DTOs.Requests;
using CardiacPatientMonitoringSystem.API.DTOs.Responses;
using CardiacPatientMonitoringSystem.API.Models;
using CardiacPatientMonitoringSystem.API.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace CardiacPatientMonitoringSystem.API.Services.Classes
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IConfiguration _configuration;
        private readonly ApplicationDbContext _context;

        public AuthService(
            UserManager<IdentityUser> userManager,
            IConfiguration configuration,
            ApplicationDbContext context)
        {
            _userManager = userManager;
            _configuration = configuration;
            _context = context;
        }

        public async Task<IdentityResult> RegisterAsync(RegisterRequest request)
        {
            await using var transaction =
                await _context.Database.BeginTransactionAsync();

            try
            {
                var user = new IdentityUser
                {
                    UserName = request.Email,
                    Email = request.Email
                };

                var result = await _userManager.CreateAsync(
                    user,
                    request.Password);

                if (!result.Succeeded)
                {
                    await transaction.RollbackAsync();
                    return result;
                }

                var roleResult = await _userManager.AddToRoleAsync(
                    user,
                    "Patient");

                if (!roleResult.Succeeded)
                {
                    await transaction.RollbackAsync();
                    return roleResult;
                }

                var patient = new Patient
                {
                    UserId = user.Id,
                    FullName = request.FullName,
                    DateOfBirth = request.DateOfBirth,
                    Gender = request.Gender,
                    PhoneNumber = request.PhoneNumber,
                    BloodType = request.BloodType
                };

                await _context.Patients.AddAsync(patient);
                await _context.SaveChangesAsync();

                await transaction.CommitAsync();

                return result;
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }

        public async Task<LoginResponse?> LoginAsync(LoginRequest request)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);

            if (user == null)
                return null;

            var isPasswordValid = await _userManager.CheckPasswordAsync(
                user,
                request.Password);

            if (!isPasswordValid)
                return null;

            var roles = await _userManager.GetRolesAsync(user);

            var patient = await _context.Patients
                .AsNoTracking()
                .FirstOrDefaultAsync(p => p.UserId == user.Id);

            var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.NameIdentifier, user.Id),
                    new Claim(ClaimTypes.Email, user.Email!)
                };

            if (patient != null)
            {
                claims.Add(new Claim("PatientId", patient.Id.ToString()));
            }

            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(
                    _configuration["Jwt:Key"]!));

            var credentials = new SigningCredentials(
                key,
                SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(
                    double.Parse(
                        _configuration["Jwt:DurationInMinutes"]!)),
                signingCredentials: credentials);

            return new LoginResponse
            {
                Token = new JwtSecurityTokenHandler()
                    .WriteToken(token)
            };
        }
    }
}