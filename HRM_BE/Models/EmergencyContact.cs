using System;
using System.Collections.Generic;

namespace ProjectPRN232_HRM.Models;

public partial class EmergencyContact
{
    public int Id { get; set; }

    public int EmployeeId { get; set; }

    public string FullName { get; set; } = null!;

    public string? Relationship { get; set; }

    public string Phone { get; set; } = null!;

    public string? Email { get; set; }

    public string? Address { get; set; }

    public bool? IsPrimary { get; set; }

    public string? Notes { get; set; }

    public DateTime CreatedDate { get; set; }

    public DateTime? ModifiedDate { get; set; }

    public virtual Employee Employee { get; set; } = null!;
}
