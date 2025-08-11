# Hướng dẫn sử dụng API Xác thực HRM

## Tổng quan

Hệ thống xác thực HRM được thiết kế với các đặc điểm sau:

- **Không có chức năng đăng ký**: Tài khoản được tạo thủ công bởi HR/Admin
- **Đăng nhập bằng Email hoặc Mã nhân viên**
- **JWT Token với thời hạn linh hoạt** (Remember Me)
- **Chức năng quên mật khẩu** với email reset
- **Đổi mật khẩu** cho người dùng đã đăng nhập
- **Phân quyền bằng Role-based Authorization**

## Cấu hình

### 1. Cấu hình JWT trong appsettings.json

```json
{
  "Jwt": {
    "Key": "YourSuperSecretKey12345678901234567890",
    "Issuer": "HRM_Issuer",
    "Audience": "HRM_Audience"
  },
  "AppUrl": "https://localhost:7000",
  "Email": {
    "SmtpServer": "smtp.gmail.com",
    "SmtpPort": 587,
    "Username": "your-email@gmail.com",
    "Password": "your-app-password",
    "FromEmail": "your-email@gmail.com"
  }
}
```

### 2. Cấu hình Email (Gmail)

Để sử dụng Gmail SMTP:
1. Bật 2-Factor Authentication
2. Tạo App Password
3. Sử dụng App Password thay vì mật khẩu thường

## API Endpoints

### 1. Đăng nhập

**POST** `/api/auth/login`

```json
{
  "emailOrEmployeeCode": "employee@company.com",
  "password": "password123",
  "rememberMe": false
}
```

**Response thành công:**
```json
{
  "success": true,
  "message": "Đăng nhập thành công",
  "token": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9...",
  "expiresAt": "2024-01-15T10:30:00Z",
  "user": {
    "id": 1,
    "employeeCode": "EMP001",
    "fullName": "Nguyễn Văn A",
    "email": "employee@company.com",
    "roles": ["Employee"]
  }
}
```

### 2. Quên mật khẩu

**POST** `/api/auth/forgot-password`

```json
{
  "email": "employee@company.com"
}
```

**Response:**
```json
{
  "success": true,
  "message": "Email đặt lại mật khẩu đã được gửi. Vui lòng kiểm tra hộp thư của bạn."
}
```

### 3. Đặt lại mật khẩu

**POST** `/api/auth/reset-password`

```json
{
  "email": "employee@company.com",
  "token": "reset-token-from-email",
  "newPassword": "newpassword123",
  "confirmPassword": "newpassword123"
}
```

### 4. Đổi mật khẩu (yêu cầu đăng nhập)

**POST** `/api/auth/change-password`

**Headers:** `Authorization: Bearer {token}`

```json
{
  "currentPassword": "oldpassword123",
  "newPassword": "newpassword123",
  "confirmPassword": "newpassword123"
}
```

### 5. Lấy thông tin user hiện tại

**GET** `/api/auth/me`

**Headers:** `Authorization: Bearer {token}`

### 6. Đăng xuất

**POST** `/api/auth/logout`

```json
"userId"
```

## Quản lý tài khoản (HR/Admin)

### 1. Tạo tài khoản cho nhân viên

**POST** `/api/employeeaccount/create-account`

**Headers:** `Authorization: Bearer {admin-token}`

```json
{
  "employeeId": 1,
  "password": "initialpassword123",
  "role": "Employee"
}
```

### 2. Gán role cho nhân viên

**POST** `/api/employeeaccount/assign-role`

**Headers:** `Authorization: Bearer {admin-token}`

```json
{
  "employeeId": 1,
  "role": "Manager"
}
```

### 3. Lấy danh sách nhân viên chưa có tài khoản

**GET** `/api/employeeaccount/employees-without-account`

**Headers:** `Authorization: Bearer {admin-token}`

### 4. Lấy danh sách nhân viên đã có tài khoản

**GET** `/api/employeeaccount/employees-with-account`

**Headers:** `Authorization: Bearer {admin-token}`

## Phân quyền

### Roles mặc định:
- **Admin**: Quyền quản trị cao nhất
- **HR**: Quyền quản lý nhân sự
- **Manager**: Quyền quản lý nhóm
- **Employee**: Nhân viên thường

### Sử dụng Authorization:

```csharp
[Authorize] // Yêu cầu đăng nhập
[Authorize(Roles = "Admin,HR")] // Yêu cầu role Admin hoặc HR
[Authorize(Roles = "Admin")] // Chỉ Admin
```

## JWT Token

### Claims trong JWT:
- `UserId`: ID của ApplicationUser
- `EmployeeId`: ID của Employee
- `Email`: Email nhân viên
- `FullName`: Họ tên nhân viên
- `EmployeeCode`: Mã nhân viên
- `Role`: Vai trò (có thể có nhiều)

### Thời hạn token:
- **Không Remember Me**: 8 giờ
- **Có Remember Me**: 30 ngày

## Xử lý lỗi

### Lỗi đăng nhập:
```json
{
  "success": false,
  "message": "Email hoặc mã nhân viên không tồn tại"
}
```

### Lỗi mật khẩu:
```json
{
  "success": false,
  "message": "Mật khẩu không đúng"
}
```

### Lỗi validation:
```json
{
  "success": false,
  "message": "Dữ liệu không hợp lệ"
}
```

## Ví dụ sử dụng

### 1. Đăng nhập và lấy token:

```javascript
const loginResponse = await fetch('/api/auth/login', {
  method: 'POST',
  headers: {
    'Content-Type': 'application/json'
  },
  body: JSON.stringify({
    emailOrEmployeeCode: 'employee@company.com',
    password: 'password123',
    rememberMe: false
  })
});

const loginData = await loginResponse.json();
const token = loginData.token;
```

### 2. Sử dụng token cho API khác:

```javascript
const response = await fetch('/api/auth/me', {
  headers: {
    'Authorization': `Bearer ${token}`
  }
});
```

### 3. Quên mật khẩu:

```javascript
const forgotResponse = await fetch('/api/auth/forgot-password', {
  method: 'POST',
  headers: {
    'Content-Type': 'application/json'
  },
  body: JSON.stringify({
    email: 'employee@company.com'
  })
});
```

## Lưu ý bảo mật

1. **JWT Key**: Sử dụng key đủ mạnh và bảo mật
2. **HTTPS**: Luôn sử dụng HTTPS trong production
3. **Token Storage**: Lưu token an toàn (HttpOnly cookies, secure storage)
4. **Password Policy**: Áp dụng chính sách mật khẩu mạnh
5. **Rate Limiting**: Giới hạn số lần đăng nhập thất bại
6. **Email Verification**: Xác thực email khi tạo tài khoản

## Troubleshooting

### Lỗi thường gặp:

1. **"Email không tồn tại"**: Kiểm tra email đã được đăng ký chưa
2. **"Không thể gửi email"**: Kiểm tra cấu hình SMTP
3. **"Token không hợp lệ"**: Token đã hết hạn hoặc không đúng format
4. **"Không có quyền truy cập"**: Kiểm tra role của user

### Debug:

1. Kiểm tra logs trong console
2. Verify JWT token tại jwt.io
3. Kiểm tra cấu hình database connection
4. Verify email settings 