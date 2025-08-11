using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using ProjectPRN232_HRM.DTOs;
using ProjectPRN232_HRM.Services.Interface;

namespace ProjectPRN232_HRM.Controllers
{
    [Route("odata/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;

        public EmployeeController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }
        [HttpGet]
        [EnableQuery]
        public async Task<ActionResult<IQueryable<EmployeeDetailDTO>>> GetEmployees()
        {
            var employees = await _employeeService.GetAllAsync();
            return Ok(employees.AsQueryable()); 
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<EmployeeDetailDTO>> GetEmployee(int id)
        {
            var employee = await _employeeService.GetByIdAsync(id);
            if (employee == null) return NotFound();

            return Ok(employee);
        }
        [HttpGet("search")]
        public async Task<IActionResult> SearchByName([FromQuery] string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                return BadRequest("Vui lòng nhập tên để tìm kiếm.");

            var result = await _employeeService.SearchByNameAsync(name);

            if (result == null || result.Count == 0)
                return NoContent();

            return Ok(result);
        }

    }
}
