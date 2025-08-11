using Microsoft.AspNetCore.OData;
using Microsoft.EntityFrameworkCore;
using ProjectPRN232_HRM.MappingProfile;
using ProjectPRN232_HRM.Models;
using ProjectPRN232_HRM.OData;
using ProjectPRN232_HRM.Services.Interface;
using ProjectPRN232_HRM.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers().AddOData(opt => opt.Select().Filter().OrderBy().Count().SetMaxTop(100)
        .AddRouteComponents("odata", ODataConfig.GetEdmModel()));
// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddDbContext<ProjectPrn232HrmanagementContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Cấu hình Identity với kiểu int
builder.Services.AddIdentity<ApplicationUser, IdentityRole<int>>()
    .AddEntityFrameworkStores<ProjectPrn232HrmanagementContext>()
    .AddDefaultTokenProviders();

// Cấu hình JWT Authentication
var jwtKey = builder.Configuration["Jwt:Key"] ?? "YourSuperSecretKey12345678901234567890";
var jwtIssuer = builder.Configuration["Jwt:Issuer"] ?? "HRM_Issuer";
var jwtAudience = builder.Configuration["Jwt:Audience"] ?? "HRM_Audience";

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = jwtIssuer,
        ValidAudience = jwtAudience,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey)),
        ClockSkew = TimeSpan.Zero
    };
});

builder.Services.AddScoped<IDepartmentService, DepartmentService>();
builder.Services.AddScoped<IPositionService, PositionService>();
builder.Services.AddScoped<IEmployeeService, EmployeeService>();
builder.Services.AddScoped<ITrainingProgramService, TrainingProgramSerivce>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IEmailService, EmailService>();
builder.Services.AddScoped<RoleService>();
builder.Services.AddScoped<AdminInitializationService>();

builder.Services.AddAutoMapper(typeof(MappingProfile));

var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

// Khởi tạo roles và admin mặc định
using (var scope = app.Services.CreateScope())
{
    var roleService = scope.ServiceProvider.GetRequiredService<RoleService>();
    await roleService.InitializeRolesAsync();

    var adminService = scope.ServiceProvider.GetRequiredService<AdminInitializationService>();
    await adminService.InitializeAdminAsync();
}

app.MapControllers();

app.Run();
