using Microsoft.AspNetCore.Mvc;

namespace CardiacPatientMonitoringSystem.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TestController : ControllerBase
    {
        [HttpGet("exception")]
        public IActionResult ThrowException()
        {
            throw new Exception("This is a test exception.");
        }
    }
}