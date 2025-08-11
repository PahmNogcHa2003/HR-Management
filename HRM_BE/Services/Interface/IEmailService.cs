namespace ProjectPRN232_HRM.Services.Interface;

public interface IEmailService
{
    Task<bool> SendPasswordResetEmailAsync(string email, string resetLink);
}

