using Microsoft.AspNetCore.Identity;
using ProjectPRN232_HRM.Models;

namespace ProjectPRN232_HRM.Services;

public class RoleService
{
    private readonly RoleManager<IdentityRole<int>> _roleManager;
    private readonly UserManager<ApplicationUser> _userManager;

    public RoleService(RoleManager<IdentityRole<int>> roleManager, UserManager<ApplicationUser> userManager)
    {
        _roleManager = roleManager;
        _userManager = userManager;
    }

    public async Task InitializeRolesAsync()
    {
        // Tạo các role mặc định nếu chưa tồn tại
        var roles = new[] { "Admin", "Manager", "Employee", "HR" };

        foreach (var role in roles)
        {
            if (!await _roleManager.RoleExistsAsync(role))
            {
                await _roleManager.CreateAsync(new IdentityRole<int>(role));
            }
        }
    }

    public async Task AssignDefaultRoleAsync(ApplicationUser user, string roleName = "Employee")
    {
        if (!await _roleManager.RoleExistsAsync(roleName))
        {
            await _roleManager.CreateAsync(new IdentityRole<int>(roleName));
        }

        if (!await _userManager.IsInRoleAsync(user, roleName))
        {
            await _userManager.AddToRoleAsync(user, roleName);
        }
    }
} 