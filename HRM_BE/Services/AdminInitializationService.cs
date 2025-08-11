using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ProjectPRN232_HRM.Models;

namespace ProjectPRN232_HRM.Services;

public class AdminInitializationService
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly ProjectPrn232HrmanagementContext _context;
    private readonly RoleService _roleService;

    public AdminInitializationService(
        UserManager<ApplicationUser> userManager,
        ProjectPrn232HrmanagementContext context,
        RoleService roleService)
    {
        _userManager = userManager;
        _context = context;
        _roleService = roleService;
    }

    public async Task InitializeAdminAsync()
    {
        try
        {
            // Kiểm tra xem đã có admin chưa
            var adminUser = await _userManager.FindByEmailAsync("admin@company.com");
            if (adminUser != null)
            {
                Console.WriteLine("Admin account already exists.");
                return;
            }

            // Tạo employee cho admin
            var adminEmployee = new Employee
            {
                EmployeeCode = "ADMIN001",
                FullName = "System Administrator",
                Email = "admin@company.com",
                PhoneNumber = "0123456789",
                Address = "Company Address",
                Gender = "Nam",
                JoinDate = DateOnly.FromDateTime(DateTime.Now),
                Status = "Active",
                CreatedDate = DateTime.Now
            };

            _context.Employees.Add(adminEmployee);
            await _context.SaveChangesAsync();

            // Tạo ApplicationUser cho admin
            var adminUserEntity = new ApplicationUser
            {
                UserName = "admin@company.com",
                Email = "admin@company.com",
                EmailConfirmed = true,
                CreatedDate = DateTime.Now
            };

            var result = await _userManager.CreateAsync(adminUserEntity, "Admin123!");
            if (!result.Succeeded)
            {
                var errors = string.Join(", ", result.Errors.Select(e => e.Description));
                Console.WriteLine($"Failed to create admin user: {errors}");
                return;
            }

            // Gán role Admin
            await _userManager.AddToRoleAsync(adminUserEntity, "Admin");

            // Cập nhật UserId cho Employee
            adminEmployee.UserId = adminUserEntity.Id;
            await _context.SaveChangesAsync();

            Console.WriteLine("Admin account created successfully!");
            Console.WriteLine("Email: admin@company.com");
            Console.WriteLine("Password: Admin123!");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error creating admin account: {ex.Message}");
        }
    }
}

