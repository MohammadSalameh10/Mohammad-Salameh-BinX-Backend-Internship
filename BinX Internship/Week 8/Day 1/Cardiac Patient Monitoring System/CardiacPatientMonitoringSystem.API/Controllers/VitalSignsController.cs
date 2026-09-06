using CardiacPatientMonitoringSystem.API.Data;
using CardiacPatientMonitoringSystem.API.DTOs.Requests;
using CardiacPatientMonitoringSystem.API.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace CardiacPatientMonitoringSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VitalSignsController : ControllerBase
    {
        private readonly IVitalSignService _vitalSignService;
        private readonly ApplicationDbContext _context;

        public VitalSignsController(IVitalSignService vitalSignService, ApplicationDbContext context)
        {
            _vitalSignService = vitalSignService;
            _context = context;
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAll()
        {
            var vitalSigns = await _vitalSignService.GetAllAsync();

            return Ok(vitalSigns);
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetById(int id)
        {
            var vitalSign = await _vitalSignService.GetByIdAsync(id);

            if (vitalSign == null)
                return NotFound();

            return Ok(vitalSign);
        }

        [HttpPost]
        [Authorize(Roles = "Patient")]
        public async Task<IActionResult> Create(CreateVitalSignRequest request)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (userId == null)
                return Unauthorized();

            var vitalSign = await _vitalSignService.CreateAsync(
                userId,
                request);

            if (vitalSign == null)
                return BadRequest("Patient profile not found. Create a patient profile first.");

            return CreatedAtAction(
                nameof(GetById),
                new { id = vitalSign.Id },
                vitalSign);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Update(int id, UpdateVitalSignRequest request)
        {
            var updated = await _vitalSignService.UpdateAsync(id, request);

            if (!updated)
                return NotFound();

            return Ok();
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _vitalSignService.DeleteAsync(id);

            if (!deleted)
                return NotFound();

            return NoContent();
        }

        [HttpGet("diagnostic-n-plus-one")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DiagnoseNPlusOne()
        {
            var vitalSigns = await _context.VitalSigns
                .AsNoTracking()
                .ToListAsync();

            var result = new List<object>();

            foreach (var vitalSign in vitalSigns)
            {
                var patient = await _context.Patients
                    .AsNoTracking()
                    .FirstOrDefaultAsync(p => p.Id == vitalSign.PatientId);

                result.Add(new
                {
                    vitalSign.Id,
                    vitalSign.PatientId,
                    vitalSign.HeartRate,
                    vitalSign.RecordedAt,
                    PatientName = patient?.FullName
                });
            }

            return Ok(result);
        }
    }
}