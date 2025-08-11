using Microsoft.AspNetCore.Mvc;
using ProjectPRN232_HRM.Services;
using ProjectPRN232_HRM.Services.Interface;

namespace ProjectPRN232_HRM.Controllers
{

    [Route("odata/[controller]")]
    [ApiController]
    public class TrainingProgramController : ControllerBase
    {
        private readonly ITrainingProgramService _trainingProgamService;

        public TrainingProgramController(ITrainingProgramService trainingProgramService)
        {
            _trainingProgamService = trainingProgramService;
        }
 
        [HttpGet("search")]
        public async Task<IActionResult> SearchByName([FromQuery] string provider)
        {
            if (string.IsNullOrWhiteSpace(provider))
                return BadRequest("Vui lòng nhập tên để tìm kiếm.");

            var result = await _trainingProgamService.SearchProgramsByProviderAsync(provider);

            if (result == null || result.Count == 0)
                return NoContent();

            return Ok(result);
        }
    }
}
