using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ProjectPRN232_HRM.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize] // Yêu cầu authentication
public class TestAuthController : ControllerBase
{
    [HttpGet("protected")]
    public IActionResult GetProtectedData()
    {
        var userId = User.FindFirst("UserId")?.Value;
        var employeeId = User.FindFirst("EmployeeId")?.Value;
        var email = User.FindFirst(ClaimTypes.Email)?.Value;
        var fullName = User.FindFirst("FullName")?.Value;
        var employeeCode = User.FindFirst("EmployeeCode")?.Value;
        var roles = User.FindAll(ClaimTypes.Role).Select(c => c.Value).ToList();

        return Ok(new
        {
            Message = "Bạn đã truy cập thành công vào API được bảo vệ!",
            UserId = userId,
            EmployeeId = employeeId,
            Email = email,
            FullName = fullName,
            EmployeeCode = employeeCode,
            Roles = roles
        });
    }

    [HttpGet("admin")]
    [Authorize(Roles = "Admin")]
    public IActionResult GetAdminData()
    {
        return Ok(new
        {
            Message = "Bạn có quyền Admin!",
            Data = "Dữ liệu chỉ dành cho Admin"
        });
    }

    [HttpGet("employee")]
    [Authorize(Roles = "Employee")]
    public IActionResult GetEmployeeData()
    {
        return Ok(new
        {
            Message = "Bạn có quyền Employee!",
            Data = "Dữ liệu dành cho Employee"
        });
    }
} 