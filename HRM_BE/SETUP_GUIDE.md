# Hướng dẫn Setup Hệ thống Xác thực HRM

## Bước 1: Cấu hình Database

1. **Kiểm tra Connection String** trong `appsettings.json`:
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=YOUR_SERVER;Database=YOUR_DB;uid=YOUR_USER;pwd=YOUR_PASSWORD;TrustServerCertificate=True"
  }
}
```

2. **Chạy Migration** (nếu chưa có):
```bash
dotnet ef database update
```

## Bước 2: Cấu hình Email (Tùy chọn)

Để sử dụng chức năng quên mật khẩu, cấu hình email trong `appsettings.json`:

```json
{
  "Email": {
    "SmtpServer": "smtp.gmail.com",
    "SmtpPort": 587,
    "Username": "your-email@gmail.com",
    "Password": "your-app-password",
    "FromEmail": "your-email@gmail.com"
  }
}
```

**Lưu ý cho Gmail:**
- Bật 2-Factor Authentication
- Tạo App Password
- Sử dụng App Password thay vì mật khẩu thường

## Bước 3: Chạy ứng dụng

```bash
dotnet run
```

Khi chạy lần đầu, hệ thống sẽ tự động:
- Tạo các roles mặc định: Admin, HR, Manager, Employee
- Tạo tài khoản admin mặc định

## Bước 4: Tài khoản Admin mặc định

Sau khi chạy ứng dụng, bạn sẽ thấy thông tin tài khoản admin trong console:

```
Admin account created successfully!
Email: admin@company.com
Password: Admin123!
```

## Bước 5: Test API

### 1. Đăng nhập Admin
```bash
POST https://localhost:7000/api/auth/login
Content-Type: application/json

{
  "emailOrEmployeeCode": "admin@company.com",
  "password": "Admin123!",
  "rememberMe": false
}
```

### 2. Tạo tài khoản cho nhân viên
Sử dụng token admin để tạo tài khoản cho nhân viên:

```bash
POST https://localhost:7000/api/employeeaccount/create-account
Content-Type: application/json
Authorization: Bearer YOUR_ADMIN_TOKEN

{
  "employeeId": 1,
  "password": "Employee123!",
  "role": "Employee"
}
```

## Bước 6: Cấu hình Production

### 1. Thay đổi JWT Key
```json
{
  "Jwt": {
    "Key": "YOUR_VERY_SECURE_KEY_AT_LEAST_32_CHARACTERS_LONG",
    "Issuer": "YOUR_COMPANY_HRM",
    "Audience": "YOUR_COMPANY_HRM_USERS"
  }
}
```

### 2. Cấu hình HTTPS
Đảm bảo sử dụng HTTPS trong production.

### 3. Cấu hình Email Production
Sử dụng email service production thay vì Gmail.

## Cấu trúc API

### Authentication Endpoints:
- `POST /api/auth/login` - Đăng nhập
- `POST /api/auth/forgot-password` - Quên mật khẩu
- `POST /api/auth/reset-password` - Đặt lại mật khẩu
- `POST /api/auth/change-password` - Đổi mật khẩu
- `GET /api/auth/me` - Thông tin user hiện tại
- `POST /api/auth/logout` - Đăng xuất

### Employee Account Management (Admin/HR):
- `POST /api/employeeaccount/create-account` - Tạo tài khoản
- `POST /api/employeeaccount/assign-role` - Gán role
- `GET /api/employeeaccount/employees-without-account` - Nhân viên chưa có tài khoản
- `GET /api/employeeaccount/employees-with-account` - Nhân viên đã có tài khoản

## Roles và Permissions

- **Admin**: Quyền quản trị cao nhất
- **HR**: Quyền quản lý nhân sự
- **Manager**: Quyền quản lý nhóm
- **Employee**: Nhân viên thường

## Troubleshooting

### Lỗi thường gặp:

1. **"Database connection failed"**
   - Kiểm tra connection string
   - Đảm bảo SQL Server đang chạy

2. **"Admin account creation failed"**
   - Kiểm tra logs trong console
   - Đảm bảo database có quyền write

3. **"Email sending failed"**
   - Kiểm tra cấu hình SMTP
   - Đảm bảo App Password đúng (Gmail)

4. **"JWT token invalid"**
   - Kiểm tra JWT key trong appsettings.json
   - Đảm bảo token chưa hết hạn

### Debug:

1. Kiểm tra logs trong console
2. Sử dụng Swagger UI: `https://localhost:7000/swagger`
3. Test API với file `AuthTest.http`

## Security Best Practices

1. **JWT Key**: Sử dụng key đủ mạnh (ít nhất 32 ký tự)
2. **HTTPS**: Luôn sử dụng HTTPS trong production
3. **Password Policy**: Áp dụng chính sách mật khẩu mạnh
4. **Rate Limiting**: Giới hạn số lần đăng nhập thất bại
5. **Token Storage**: Lưu token an toàn ở client side
6. **Email Verification**: Xác thực email khi tạo tài khoản

## Next Steps

1. Tích hợp với frontend application
2. Thêm logging và monitoring
3. Implement rate limiting
4. Thêm audit trail
5. Cấu hình backup và recovery

