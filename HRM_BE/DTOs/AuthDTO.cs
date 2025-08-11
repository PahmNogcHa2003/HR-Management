using System.ComponentModel.DataAnnotations;

namespace ProjectPRN232_HRM.DTOs;

public class LoginDTO
{
    [Required(ErrorMessage = "Email hoặc mã nhân viên là bắt buộc")]
    public string EmailOrEmployeeCode { get; set; } = null!;

    [Required(ErrorMessage = "Mật khẩu là bắt buộc")]
    public string Password { get; set; } = null!;

    public bool RememberMe { get; set; } = false;
}

public class ForgotPasswordDTO
{
    [Required(ErrorMessage = "Email là bắt buộc")]
    [EmailAddress(ErrorMessage = "Email không hợp lệ")]
    public string Email { get; set; } = null!;
}

public class ResetPasswordDTO
{
    [Required(ErrorMessage = "Email là bắt buộc")]
    [EmailAddress(ErrorMessage = "Email không hợp lệ")]
    public string Email { get; set; } = null!;

    [Required(ErrorMessage = "Token là bắt buộc")]
    public string Token { get; set; } = null!;

    [Required(ErrorMessage = "Mật khẩu mới là bắt buộc")]
    [StringLength(100, MinimumLength = 6, ErrorMessage = "Mật khẩu phải có ít nhất 6 ký tự")]
    public string NewPassword { get; set; } = null!;

    [Required(ErrorMessage = "Xác nhận mật khẩu là bắt buộc")]
    [Compare("NewPassword", ErrorMessage = "Mật khẩu xác nhận không khớp")]
    public string ConfirmPassword { get; set; } = null!;
}

public class ChangePasswordDTO
{
    [Required(ErrorMessage = "Mật khẩu hiện tại là bắt buộc")]
    public string CurrentPassword { get; set; } = null!;

    [Required(ErrorMessage = "Mật khẩu mới là bắt buộc")]
    [StringLength(100, MinimumLength = 6, ErrorMessage = "Mật khẩu phải có ít nhất 6 ký tự")]
    public string NewPassword { get; set; } = null!;

    [Required(ErrorMessage = "Xác nhận mật khẩu là bắt buộc")]
    [Compare("NewPassword", ErrorMessage = "Mật khẩu xác nhận không khớp")]
    public string ConfirmPassword { get; set; } = null!;
}

public class AuthResponseDTO
{
    public bool Success { get; set; }
    public string Message { get; set; } = null!;
    public string? Token { get; set; }
    public DateTime? ExpiresAt { get; set; }
    public UserInfoDTO? User { get; set; }
}

public class UserInfoDTO
{
    public int Id { get; set; }
    public string EmployeeCode { get; set; } = null!;
    public string FullName { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string? PhoneNumber { get; set; }
    public string? Address { get; set; }
    public string? Gender { get; set; }
    public DateOnly? Dob { get; set; }
    public DateOnly JoinDate { get; set; }
    public string Status { get; set; } = null!;
    public string? DepartmentName { get; set; }
    public string? PositionTitle { get; set; }
    public string? AvatarUrl { get; set; }
    public List<string> Roles { get; set; } = new List<string>();
}

public class ForgotPasswordResponseDTO
{
    public bool Success { get; set; }
    public string Message { get; set; } = null!;
} 