using System;
using System.Collections.Generic;

namespace ProjectPRN232_HRM.Models;

public partial class Attendance
{
    public int Id { get; set; }

    public int EmployeeId { get; set; }

    public DateOnly Date { get; set; }

    public TimeOnly? CheckIn { get; set; }

    public TimeOnly? CheckOut { get; set; }

    public decimal? WorkHours { get; set; }

    public string Status { get; set; } = null!;

    public string Type { get; set; } = null!;

    public string? Notes { get; set; }

    public int? ApprovedBy { get; set; }

    public DateTime CreatedDate { get; set; }

    public DateTime? ModifiedDate { get; set; }

    public virtual Employee? ApprovedByNavigation { get; set; }

    public virtual Employee Employee { get; set; } = null!;
}
