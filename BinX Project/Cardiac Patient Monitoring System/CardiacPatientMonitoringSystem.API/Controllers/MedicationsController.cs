using CardiacPatientMonitoringSystem.API.DTOs.Requests;
using CardiacPatientMonitoringSystem.API.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace CardiacPatientMonitoringSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MedicationsController : ControllerBase
    {
        private readonly IMedicationService _medicationService;

        public MedicationsController(IMedicationService medicationService)
        {
            _medicationService = medicationService;
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAll([FromQuery] string? name)
        {
            var medications = await _medicationService.GetAllAsync(name);

            return Ok(medications);
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetById(int id)
        {
            var medication = await _medicationService.GetByIdAsync(id);

            if (medication == null)
                return NotFound();

            return Ok(medication);
        }

        [HttpGet("patient/{patientId}/doctor")]
        [Authorize(Roles = "Doctor")]
        public async Task<IActionResult> GetPatientMedicationsForDoctor(int patientId)
        {
            var doctorIdClaim = User.FindFirstValue("DoctorId");

            if (!int.TryParse(doctorIdClaim, out var doctorId))
                return Forbid();

            var medications = await _medicationService
                .GetPatientMedicationsForDoctorAsync(
                    doctorId,
                    patientId);

            if (medications == null)
                return Forbid();

            return Ok(medications);
        }

        [HttpPost]
        [Authorize(Roles = "Patient")]
        public async Task<IActionResult> Create(CreateMedicationRequest request)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (userId == null)
                return Unauthorized();

            var medication = await _medicationService.CreateAsync(
                userId,
                request);

            if (medication == null)
                return BadRequest("Patient profile not found. Create a patient profile first.");

            return CreatedAtAction(
                nameof(GetById),
                new { id = medication.Id },
                medication);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Update(int id, UpdateMedicationRequest request)
        {
            var updated = await _medicationService.UpdateAsync(id, request);

            if (!updated)
                return NotFound();

            return Ok();
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _medicationService.DeleteAsync(id);

            if (!deleted)
                return NotFound();

            return NoContent();
        }
    }
}