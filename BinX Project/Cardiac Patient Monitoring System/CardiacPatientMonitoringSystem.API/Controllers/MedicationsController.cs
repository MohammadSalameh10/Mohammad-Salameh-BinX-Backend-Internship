using CardiacPatientMonitoringSystem.API.DTOs.Requests;
using CardiacPatientMonitoringSystem.API.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

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
        public async Task<IActionResult> GetAll()
        {
            var medications = await _medicationService.GetAllAsync();

            return Ok(medications);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var medication = await _medicationService.GetByIdAsync(id);

            if (medication == null)
                return NotFound();

            return Ok(medication);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateMedicationRequest request)
        {
            var medication = await _medicationService.CreateAsync(request);

            return CreatedAtAction(
                nameof(GetById),
                new { id = medication.Id },
                medication);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, UpdateMedicationRequest request)
        {
            var updated = await _medicationService.UpdateAsync(id, request);

            if (!updated)
                return NotFound();

            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _medicationService.DeleteAsync(id);

            if (!deleted)
                return NotFound();

            return NoContent();
        }
    }
}