using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using ProjectPRN232_HRM.DTOs;
using ProjectPRN232_HRM.Models;
using ProjectPRN232_HRM.Services.Interface;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ProjectPRN232_HRM.Services;

public class AuthService : IAuthService
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly ProjectPrn232HrmanagementContext _context;
    private readonly IConfiguration _configuration;
    private readonly IEmailService _emailService;

    public AuthService(
        UserManager<ApplicationUser> userManager,
        ProjectPrn232HrmanagementContext context,
        IConfiguration configuration,
        IEmailService emailService)
    {
        _userManager = userManager;
        _context = context;
        _configuration = configuration;
        _emailService = emailService;
    }

    public async Task<AuthResponseDTO> LoginAsync(LoginDTO loginDTO)
    {
        try
        {
            // Tìm user bằng email hoặc employee code
            var user = await _userManager.FindByEmailAsync(loginDTO.EmailOrEmployeeCode);
            
            if (user == null)
            {
                // Nếu không tìm thấy bằng email, tìm bằng employee code
                var employees = await _context.Employees
                    .FirstOrDefaultAsync(e => e.EmployeeCode == loginDTO.EmailOrEmployeeCode);
                
                if (employees?.UserId != null)
                {
                    user = await _userManager.FindByIdAsync(employees.UserId.ToString());
                }
            }

            if (user == null)
            {
                return new AuthResponseDTO
                {
                    Success = false,
                    Message = "Email hoặc mã nhân viên không tồn tại"
                };
            }

            // Kiểm tra mật khẩu
            var isPasswordValid = await _userManager.CheckPasswordAsync(user, loginDTO.Password);
            if (!isPasswordValid)
            {
                return new AuthResponseDTO
                {
                    Success = false,
                    Message = "Mật khẩu không đúng"
                };
            }

            // Lấy thông tin employee
            var employee = await _context.Employees
                .Include(e => e.Department)
                .Include(e => e.Position)
                .FirstOrDefaultAsync(e => e.UserId == user.Id);

            if (employee == null)
            {
                return new AuthResponseDTO
                {
                    Success = false,
                    Message = "Không tìm thấy thông tin nhân viên"
                };
            }

            // Lấy roles của user
            var roles = await _userManager.GetRolesAsync(user);

            // Tạo JWT token
            var token = await GenerateJwtTokenAsync(user, employee, roles, loginDTO.RememberMe);

            var userInfo = new UserInfoDTO
            {
                Id = employee.Id,
                EmployeeCode = employee.EmployeeCode,
                FullName = employee.FullName,
                Email = employee.Email,
                PhoneNumber = employee.PhoneNumber,
                Address = employee.Address,
                Gender = employee.Gender,
                Dob = employee.Dob,
                JoinDate = employee.JoinDate,
                Status = employee.Status,
                DepartmentName = employee.Department?.Name,
                PositionTitle = employee.Position?.Title,
                AvatarUrl = employee.AvatarUrl,
                Roles = roles.ToList()
            };

            return new AuthResponseDTO
            {
                Success = true,
                Message = "Đăng nhập thành công",
                Token = token,
                ExpiresAt = loginDTO.RememberMe ? DateTime.UtcNow.AddDays(30) : DateTime.UtcNow.AddHours(8),
                User = userInfo
            };
        }
        catch (Exception ex)
        {
            return new AuthResponseDTO
            {
                Success = false,
                Message = $"Lỗi đăng nhập: {ex.Message}"
            };
        }
    }

    public async Task<ForgotPasswordResponseDTO> ForgotPasswordAsync(ForgotPasswordDTO forgotPasswordDTO)
    {
        try
        {
            var user = await _userManager.FindByEmailAsync(forgotPasswordDTO.Email);
            if (user == null)
            {
                return new ForgotPasswordResponseDTO
                {
                    Success = false,
                    Message = "Email không tồn tại trong hệ thống"
                };
            }

            // Tạo token reset password
            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            
            // Tạo link reset password
            var resetLink = $"{_configuration["AppUrl"]}/reset-password?email={Uri.EscapeDataString(forgotPasswordDTO.Email)}&token={Uri.EscapeDataString(token)}";

            // Gửi email
            var emailSent = await _emailService.SendPasswordResetEmailAsync(forgotPasswordDTO.Email, resetLink);

            if (emailSent)
            {
                return new ForgotPasswordResponseDTO
                {
                    Success = true,
                    Message = "Email đặt lại mật khẩu đã được gửi. Vui lòng kiểm tra hộp thư của bạn."
                };
            }
            else
            {
                return new ForgotPasswordResponseDTO
                {
                    Success = false,
                    Message = "Không thể gửi email. Vui lòng thử lại sau."
                };
            }
        }
        catch (Exception ex)
        {
            return new ForgotPasswordResponseDTO
            {
                Success = false,
                Message = $"Lỗi: {ex.Message}"
            };
        }
    }

    public async Task<AuthResponseDTO> ResetPasswordAsync(ResetPasswordDTO resetPasswordDTO)
    {
        try
        {
            var user = await _userManager.FindByEmailAsync(resetPasswordDTO.Email);
            if (user == null)
            {
                return new AuthResponseDTO
                {
                    Success = false,
                    Message = "Email không tồn tại trong hệ thống"
                };
            }

            // Giải mã token từ URL
            var decodedToken = Uri.UnescapeDataString(resetPasswordDTO.Token);

            // Reset password
            var result = await _userManager.ResetPasswordAsync(user, decodedToken, resetPasswordDTO.NewPassword);

            if (!result.Succeeded)
            {
                var errors = string.Join(", ", result.Errors.Select(e => e.Description));
                return new AuthResponseDTO
                {
                    Success = false,
                    Message = $"Không thể đặt lại mật khẩu: {errors}"
                };
            }

            return new AuthResponseDTO
            {
                Success = true,
                Message = "Đặt lại mật khẩu thành công. Vui lòng đăng nhập lại."
            };
        }
        catch (Exception ex)
        {
            return new AuthResponseDTO
            {
                Success = false,
                Message = $"Lỗi: {ex.Message}"
            };
        }
    }


    public async Task<AuthResponseDTO> ChangePasswordAsync(ChangePasswordDTO changePasswordDTO, int userId)
    {
        try
        {
            var user = await _userManager.FindByIdAsync(userId.ToString());
            if (user == null)
            {
                return new AuthResponseDTO
                {
                    Success = false,
                    Message = "Không tìm thấy người dùng"
                };
            }

            // Kiểm tra mật khẩu hiện tại
            var isCurrentPasswordValid = await _userManager.CheckPasswordAsync(user, changePasswordDTO.CurrentPassword);
            if (!isCurrentPasswordValid)
            {
                return new AuthResponseDTO
                {
                    Success = false,
                    Message = "Mật khẩu hiện tại không đúng"
                };
            }

            // Thay đổi mật khẩu
            var result = await _userManager.ChangePasswordAsync(user, changePasswordDTO.CurrentPassword, changePasswordDTO.NewPassword);
            
            if (!result.Succeeded)
            {
                var errors = string.Join(", ", result.Errors.Select(e => e.Description));
                return new AuthResponseDTO
                {
                    Success = false,
                    Message = $"Không thể thay đổi mật khẩu: {errors}"
                };
            }

            return new AuthResponseDTO
            {
                Success = true,
                Message = "Thay đổi mật khẩu thành công"
            };
        }
        catch (Exception ex)
        {
            return new AuthResponseDTO
            {
                Success = false,
                Message = $"Lỗi: {ex.Message}"
            };
        }
    }

    public async Task<bool> LogoutAsync(string userId)
    {
        try
        {
            // Có thể thêm logic để blacklist token hoặc clear session
            return true;
        }
        catch
        {
            return false;
        }
    }

    private async Task<string> GenerateJwtTokenAsync(ApplicationUser user, Employee employee, IList<string> roles, bool rememberMe)
    {
        var jwtKey = _configuration["Jwt:Key"] ?? "YourSuperSecretKey12345678901234567890";
        var jwtIssuer = _configuration["Jwt:Issuer"] ?? "HRM_Issuer";
        var jwtAudience = _configuration["Jwt:Audience"] ?? "HRM_Audience";

        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim("UserId", user.Id.ToString()),
            new Claim("EmployeeId", employee.Id.ToString()),
            new Claim(ClaimTypes.Email, employee.Email),
            new Claim("FullName", employee.FullName),
            new Claim("EmployeeCode", employee.EmployeeCode),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
        };

        // Thêm roles vào claims
        foreach (var role in roles)
        {
            claims.Add(new Claim(ClaimTypes.Role, role));
        }

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        // Thời hạn token dựa trên RememberMe
        var expires = rememberMe ? DateTime.UtcNow.AddDays(30) : DateTime.UtcNow.AddHours(8);

        var token = new JwtSecurityToken(
            issuer: jwtIssuer,
            audience: jwtAudience,
            claims: claims,
            expires: expires,
            signingCredentials: creds
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}

