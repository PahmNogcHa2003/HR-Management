using ProjectPRN232_HRM.DTOs;

namespace ProjectPRN232_HRM.Services.Interface;

public interface IAuthService
{
    Task<AuthResponseDTO> LoginAsync(LoginDTO loginDTO);
    Task<ForgotPasswordResponseDTO> ForgotPasswordAsync(ForgotPasswordDTO forgotPasswordDTO);
    Task<AuthResponseDTO> ResetPasswordAsync(ResetPasswordDTO resetPasswordDTO);
    Task<AuthResponseDTO> ChangePasswordAsync(ChangePasswordDTO changePasswordDTO, int userId);
    Task<bool> LogoutAsync(string userId);
} 