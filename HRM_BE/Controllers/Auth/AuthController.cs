using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using ProjectPRN232_HRM.DTOs;
using ProjectPRN232_HRM.Services.Interface;
using System.Security.Claims;

namespace ProjectPRN232_HRM.Controllers.Auth;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    /// <summary>
    /// Đăng nhập bằng email hoặc mã nhân viên
    /// </summary>
    /// <param name="loginDTO">Thông tin đăng nhập</param>
    /// <returns>Token và thông tin user</returns>
    [HttpPost("login")]
    public async Task<ActionResult<AuthResponseDTO>> Login([FromBody] LoginDTO loginDTO)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(new AuthResponseDTO
            {
                Success = false,
                Message = "Dữ liệu không hợp lệ",
            });
        }

        var result = await _authService.LoginAsync(loginDTO);
        
        if (!result.Success)
        {
            return BadRequest(result);
        }

        return Ok(result);
    }

    /// <summary>
    /// Quên mật khẩu - gửi email đặt lại mật khẩu
    /// </summary>
    /// <param name="forgotPasswordDTO">Email cần đặt lại mật khẩu</param>
    /// <returns>Kết quả gửi email</returns>
    [HttpPost("forgot-password")]
    public async Task<ActionResult<ForgotPasswordResponseDTO>> ForgotPassword([FromBody] ForgotPasswordDTO forgotPasswordDTO)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(new ForgotPasswordResponseDTO
            {
                Success = false,
                Message = "Dữ liệu không hợp lệ",
            });
        }

        var result = await _authService.ForgotPasswordAsync(forgotPasswordDTO);
        
        if (!result.Success)
        {
            return BadRequest(result);
        }

        return Ok(result);
    }

    /// <summary>
    /// Đặt lại mật khẩu bằng token
    /// </summary>
    /// <param name="resetPasswordDTO">Thông tin đặt lại mật khẩu</param>
    /// <returns>Kết quả đặt lại mật khẩu</returns>
    [HttpPost("reset-password")]
    public async Task<ActionResult<AuthResponseDTO>> ResetPassword([FromBody] ResetPasswordDTO resetPasswordDTO)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(new AuthResponseDTO
            {
                Success = false,
                Message = "Dữ liệu không hợp lệ",
            });
        }

        var result = await _authService.ResetPasswordAsync(resetPasswordDTO);
        
        if (!result.Success)
        {
            return BadRequest(result);
        }

        return Ok(result);
    }

    /// <summary>
    /// Đổi mật khẩu (yêu cầu đăng nhập)
    /// </summary>
    /// <param name="changePasswordDTO">Thông tin đổi mật khẩu</param>
    /// <returns>Kết quả đổi mật khẩu</returns>
    [HttpPost("change-password")]
    [Authorize]
    public async Task<ActionResult<AuthResponseDTO>> ChangePassword([FromBody] ChangePasswordDTO changePasswordDTO)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(new AuthResponseDTO
            {
                Success = false,
                Message = "Dữ liệu không hợp lệ",
            });
        }

        // Lấy userId từ JWT token
        var userIdClaim = User.FindFirst("UserId")?.Value;
        if (string.IsNullOrEmpty(userIdClaim) || !int.TryParse(userIdClaim, out int userId))
        {
            return Unauthorized(new AuthResponseDTO
            {
                Success = false,
                Message = "Không tìm thấy thông tin người dùng"
            });
        }

        var result = await _authService.ChangePasswordAsync(changePasswordDTO, userId);
        
        if (!result.Success)
        {
            return BadRequest(result);
        }

        return Ok(result);
    }

    /// <summary>
    /// Đăng xuất
    /// </summary>
    /// <param name="userId">ID của user</param>
    /// <returns>Kết quả đăng xuất</returns>
    [HttpPost("logout")]
    public async Task<ActionResult<bool>> Logout([FromBody] string userId)
    {
        if (string.IsNullOrEmpty(userId))
        {
            return BadRequest(false);
        }

        var result = await _authService.LogoutAsync(userId);
        return Ok(result);
    }

    /// <summary>
    /// Kiểm tra trạng thái đăng nhập
    /// </summary>
    /// <returns>Thông tin user hiện tại</returns>
    [HttpGet("me")]
    [Authorize]
    public async Task<ActionResult<UserInfoDTO>> GetCurrentUser()
    {
        // Lấy thông tin user từ JWT token
        var userId = User.FindFirst("UserId")?.Value;
        var employeeId = User.FindFirst("EmployeeId")?.Value;
        var email = User.FindFirst(ClaimTypes.Email)?.Value;
        var fullName = User.FindFirst("FullName")?.Value;
        var employeeCode = User.FindFirst("EmployeeCode")?.Value;
        var roles = User.FindAll(ClaimTypes.Role).Select(c => c.Value).ToList();

        if (string.IsNullOrEmpty(userId))
        {
            return Unauthorized(new AuthResponseDTO
            {
                Success = false,
                Message = "Không tìm thấy thông tin user"
            });
        }

        var userInfo = new UserInfoDTO
        {
            Id = int.Parse(employeeId ?? "0"),
            EmployeeCode = employeeCode ?? "",
            FullName = fullName ?? "",
            Email = email ?? "",
            Roles = roles
        };

        return Ok(userInfo);
    }
} 