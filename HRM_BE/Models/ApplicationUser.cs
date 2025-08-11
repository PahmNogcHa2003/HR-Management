using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace ProjectPRN232_HRM.Models;

public class ApplicationUser : IdentityUser<int>
{
    // Navigation property to Employee
    public virtual Employee? Employee { get; set; }
    
    // Additional properties if needed
    public DateTime CreatedDate { get; set; } = DateTime.Now;
    public DateTime? ModifiedDate { get; set; }
} 