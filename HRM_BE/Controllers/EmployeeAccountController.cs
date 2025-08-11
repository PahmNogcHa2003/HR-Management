using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ProjectPRN232_HRM.DTOs;
using ProjectPRN232_HRM.Models;
using ProjectPRN232_HRM.Services.Interface;
using System.Security.Claims;

namespace ProjectPRN232_HRM.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize(Roles = "Admin,HR")]
public class EmployeeAccountController : ControllerBase
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly ProjectPrn232HrmanagementContext _context;
    private readonly IConfiguration _configuration;

    public EmployeeAccountController(
        UserManager<ApplicationUser> userManager,
        ProjectPrn232HrmanagementContext context,
        IConfiguration configuration)
    {
        _userManager = userManager;
        _context = context;
        _configuration = configuration;
    }

    /// <summary>
    /// Tạo tài khoản cho nhân viên (chỉ Admin/HR)
    /// </summary>
    /// <param name="createAccountDTO">Thông tin tạo tài khoản</param>
    /// <returns>Kết quả tạo tài khoản</returns>
    [HttpPost("create-account")]
    public async Task<ActionResult<AuthResponseDTO>> CreateEmployeeAccount([FromBody] CreateEmployeeAccountDTO createAccountDTO)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(new AuthResponseDTO
            {
                Success = false,
                Message = "Dữ liệu không hợp lệ"
            });
        }

        try
        {
            // Kiểm tra employee có tồn tại không
            var employee = await _context.Employees
                .FirstOrDefaultAsync(e => e.Id == createAccountDTO.EmployeeId);

            if (employee == null)
            {
                return BadRequest(new AuthResponseDTO
                {
                    Success = false,
                    Message = "Không tìm thấy nhân viên"
                });
            }

            // Kiểm tra employee đã có tài khoản chưa
            if (employee.UserId.HasValue)
            {
                return BadRequest(new AuthResponseDTO
                {
                    Success = false,
                    Message = "Nhân viên đã có tài khoản"
                });
            }

            // Kiểm tra email đã được sử dụng chưa
            var existingUser = await _userManager.FindByEmailAsync(employee.Email);
            if (existingUser != null)
            {
                return BadRequest(new AuthResponseDTO
                {
                    Success = false,
                    Message = "Email đã được sử dụng bởi tài khoản khác"
                });
            }

            // Tạo ApplicationUser
            var user = new ApplicationUser
            {
                UserName = employee.Email,
                Email = employee.Email,
                EmailConfirmed = true,
                CreatedDate = DateTime.Now
            };

            var result = await _userManager.CreateAsync(user, createAccountDTO.Password);
            if (!result.Succeeded)
            {
                var errors = string.Join(", ", result.Errors.Select(e => e.Description));
                return BadRequest(new AuthResponseDTO
                {
                    Success = false,
                    Message = $"Không thể tạo tài khoản: {errors}"
                });
            }

            // Gán role cho user
            if (!string.IsNullOrEmpty(createAccountDTO.Role))
            {
                var roleResult = await _userManager.AddToRoleAsync(user, createAccountDTO.Role);
                if (!roleResult.Succeeded)
                {
                    var errors = string.Join(", ", roleResult.Errors.Select(e => e.Description));
                    return BadRequest(new AuthResponseDTO
                    {
                        Success = false,
                        Message = $"Không thể gán role: {errors}"
                    });
                }
            }

            // Cập nhật UserId cho Employee
            employee.UserId = user.Id;
            employee.ModifiedDate = DateTime.Now;
            await _context.SaveChangesAsync();

            return Ok(new AuthResponseDTO
            {
                Success = true,
                Message = "Tạo tài khoản thành công",
                User = new UserInfoDTO
                {
                    Id = employee.Id,
                    EmployeeCode = employee.EmployeeCode,
                    FullName = employee.FullName,
                    Email = employee.Email,
                    Roles = new List<string> { createAccountDTO.Role ?? "Employee" }
                }
            });
        }
        catch (Exception ex)
        {
            return BadRequest(new AuthResponseDTO
            {
                Success = false,
                Message = $"Lỗi: {ex.Message}"
            });
        }
    }

    /// <summary>
    /// Gán role cho nhân viên (chỉ Admin/HR)
    /// </summary>
    /// <param name="assignRoleDTO">Thông tin gán role</param>
    /// <returns>Kết quả gán role</returns>
    [HttpPost("assign-role")]
    public async Task<ActionResult<AuthResponseDTO>> AssignRole([FromBody] AssignRoleDTO assignRoleDTO)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(new AuthResponseDTO
            {
                Success = false,
                Message = "Dữ liệu không hợp lệ"
            });
        }

        try
        {
            var employee = await _context.Employees
                .FirstOrDefaultAsync(e => e.Id == assignRoleDTO.EmployeeId);

            if (employee?.UserId == null)
            {
                return BadRequest(new AuthResponseDTO
                {
                    Success = false,
                    Message = "Nhân viên không có tài khoản"
                });
            }

            var user = await _userManager.FindByIdAsync(employee.UserId.ToString());
            if (user == null)
            {
                return BadRequest(new AuthResponseDTO
                {
                    Success = false,
                    Message = "Không tìm thấy tài khoản người dùng"
                });
            }

            // Xóa tất cả roles hiện tại
            var currentRoles = await _userManager.GetRolesAsync(user);
            if (currentRoles.Any())
            {
                await _userManager.RemoveFromRolesAsync(user, currentRoles);
            }

            // Gán role mới
            var result = await _userManager.AddToRoleAsync(user, assignRoleDTO.Role);
            if (!result.Succeeded)
            {
                var errors = string.Join(", ", result.Errors.Select(e => e.Description));
                return BadRequest(new AuthResponseDTO
                {
                    Success = false,
                    Message = $"Không thể gán role: {errors}"
                });
            }

            return Ok(new AuthResponseDTO
            {
                Success = true,
                Message = "Gán role thành công"
            });
        }
        catch (Exception ex)
        {
            return BadRequest(new AuthResponseDTO
            {
                Success = false,
                Message = $"Lỗi: {ex.Message}"
            });
        }
    }

    /// <summary>
    /// Lấy danh sách nhân viên chưa có tài khoản
    /// </summary>
    /// <returns>Danh sách nhân viên</returns>
    [HttpGet("employees-without-account")]
    public async Task<ActionResult<List<EmployeeDTO>>> GetEmployeesWithoutAccount()
    {
        var employees = await _context.Employees
            .Where(e => e.UserId == null)
            .Include(e => e.Department)
            .Include(e => e.Position)
            .Select(e => new EmployeeDTO
            {
                Id = e.Id,
                EmployeeCode = e.EmployeeCode,
                FullName = e.FullName,
                Email = e.Email,
                DepartmentName = e.Department.Name,
                PositionTitle = e.Position.Title,
                Status = e.Status
            })
            .ToListAsync();

        return Ok(employees);
    }

    /// <summary>
    /// Lấy danh sách nhân viên đã có tài khoản
    /// </summary>
    /// <returns>Danh sách nhân viên với thông tin tài khoản</returns>
    [HttpGet("employees-with-account")]
    public async Task<ActionResult<List<EmployeeAccountDTO>>> GetEmployeesWithAccount()
    {
        var employees = await _context.Employees
            .Where(e => e.UserId != null)
            .Include(e => e.Department)
            .Include(e => e.Position)
            .Select(e => new EmployeeAccountDTO
            {
                Id = e.Id,
                EmployeeCode = e.EmployeeCode,
                FullName = e.FullName,
                Email = e.Email,
                DepartmentName = e.Department.Name,
                PositionTitle = e.Position.Title,
                Status = e.Status,
                UserId = e.UserId.Value
            })
            .ToListAsync();

        // Lấy roles cho từng user
        foreach (var employee in employees)
        {
            var user = await _userManager.FindByIdAsync(employee.UserId.ToString());
            if (user != null)
            {
                var roles = await _userManager.GetRolesAsync(user);
                employee.Roles = roles.ToList();
            }
        }

        return Ok(employees);
    }
}

// DTOs cho EmployeeAccountController
public class CreateEmployeeAccountDTO
{
    public int EmployeeId { get; set; }
    public string Password { get; set; } = null!;
    public string Role { get; set; } = "Employee";
}

public class AssignRoleDTO
{
    public int EmployeeId { get; set; }
    public string Role { get; set; } = null!;
}

public class EmployeeAccountDTO : EmployeeDTO
{
    public int UserId { get; set; }
    public List<string> Roles { get; set; } = new List<string>();
}

