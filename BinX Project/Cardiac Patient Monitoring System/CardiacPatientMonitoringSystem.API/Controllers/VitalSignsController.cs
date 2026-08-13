using CardiacPatientMonitoringSystem.API.DTOs.Requests;
using CardiacPatientMonitoringSystem.API.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CardiacPatientMonitoringSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VitalSignsController : ControllerBase
    {
        private readonly IVitalSignService _vitalSignService;

        public VitalSignsController(IVitalSignService vitalSignService)
        {
            _vitalSignService = vitalSignService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var vitalSigns = await _vitalSignService.GetAllAsync();

            return Ok(vitalSigns);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var vitalSign = await _vitalSignService.GetByIdAsync(id);

            if (vitalSign == null)
                return NotFound();

            return Ok(vitalSign);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateVitalSignRequest request)
        {
            var vitalSign = await _vitalSignService.CreateAsync(request);

            return CreatedAtAction(
                nameof(GetById),
                new { id = vitalSign.Id },
                vitalSign);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, UpdateVitalSignRequest request)
        {
            var updated = await _vitalSignService.UpdateAsync(id, request);

            if (!updated)
                return NotFound();

            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _vitalSignService.DeleteAsync(id);

            if (!deleted)
                return NotFound();

            return NoContent();
        }
    }
}