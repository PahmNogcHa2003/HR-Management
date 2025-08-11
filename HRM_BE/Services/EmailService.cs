using System.Net.Mail;
using System.Net;
using ProjectPRN232_HRM.Services.Interface;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace ProjectPRN232_HRM.Services;

public class EmailService : IEmailService
{
    private readonly IConfiguration _configuration;
    private readonly ILogger<EmailService> _logger;

    public EmailService(IConfiguration configuration, ILogger<EmailService> logger)
    {
        _configuration = configuration;
        _logger = logger;
    }

    public async Task<bool> SendPasswordResetEmailAsync(string email, string resetLink)
    {
        try
        {
            var smtpServer = _configuration["Email:SmtpServer"] ?? "smtp.gmail.com";
            var smtpPort = int.Parse(_configuration["Email:SmtpPort"] ?? "587");
            var smtpUsername = _configuration["Email:Username"];
            var smtpPassword = _configuration["Email:Password"];
            var fromEmail = _configuration["Email:FromEmail"] ?? smtpUsername;

            if (string.IsNullOrEmpty(smtpUsername) || string.IsNullOrEmpty(smtpPassword))
            {
                _logger.LogWarning("Email credentials not configured. Skipping send.");
                return true;
            }

            using var client = new SmtpClient(smtpServer, smtpPort)
            {
                EnableSsl = true,
                Credentials = new NetworkCredential(smtpUsername, smtpPassword),
                Timeout = 10000 // 10 giây
            };

            var mailMessage = new MailMessage
            {
                From = new MailAddress(fromEmail, "HRM System"),
                Subject = "Đặt lại mật khẩu - HRM System",
                Body = GeneratePasswordResetEmailBody(resetLink),
                IsBodyHtml = true,
                BodyEncoding = System.Text.Encoding.UTF8,
                SubjectEncoding = System.Text.Encoding.UTF8
            };

            mailMessage.To.Add(email);

            await client.SendMailAsync(mailMessage);
            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Lỗi gửi email tới {Email}", email);
            return false;
        }
    }


    private string GeneratePasswordResetEmailBody(string resetLink)
    {
        return $@"
            <html>
            <head>
                <style>
                    body {{ font-family: Arial, sans-serif; line-height: 1.6; color: #333; }}
                    .container {{ max-width: 600px; margin: 0 auto; padding: 20px; }}
                    .header {{ background-color: #007bff; color: white; padding: 20px; text-align: center; }}
                    .content {{ padding: 20px; background-color: #f8f9fa; }}
                    .button {{ display: inline-block; padding: 12px 24px; background-color: #007bff; color: white; text-decoration: none; border-radius: 5px; margin: 20px 0; }}
                    .footer {{ text-align: center; padding: 20px; color: #666; font-size: 12px; }}
                </style>
            </head>
            <body>
                <div class='container'>
                    <div class='header'>
                        <h1>HRM System</h1>
                        <h2>Đặt lại mật khẩu</h2>
                    </div>
                    <div class='content'>
                        <p>Xin chào,</p>
                        <p>Bạn đã yêu cầu đặt lại mật khẩu cho tài khoản HRM của mình.</p>
                        <p>Vui lòng nhấp vào nút bên dưới để đặt lại mật khẩu:</p>
                        <p style='text-align: center;'>
                            <a href='{resetLink}' class='button'>Đặt lại mật khẩu</a>
                        </p>
                        <p>Hoặc copy link sau vào trình duyệt:</p>
                        <p style='word-break: break-all; background-color: #e9ecef; padding: 10px; border-radius: 5px;'>
                            {resetLink}
                        </p>
                        <p><strong>Lưu ý:</strong></p>
                        <ul>
                            <li>Link này có hiệu lực trong 24 giờ</li>
                            <li>Nếu bạn không yêu cầu đặt lại mật khẩu, vui lòng bỏ qua email này</li>
                            <li>Để bảo mật, vui lòng không chia sẻ link này với người khác</li>
                        </ul>
                    </div>
                    <div class='footer'>
                        <p>Email này được gửi tự động từ hệ thống HRM. Vui lòng không trả lời email này.</p>
                        <p>Nếu có vấn đề, vui lòng liên hệ với quản trị viên hệ thống.</p>
                    </div>
                </div>
            </body>
            </html>";
    }
}

