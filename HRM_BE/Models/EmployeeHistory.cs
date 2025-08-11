using System;
using System.Collections.Generic;

namespace ProjectPRN232_HRM.Models;

public partial class EmployeeHistory
{
    public int Id { get; set; }

    public int EmployeeId { get; set; }

    public string ChangeType { get; set; } = null!;

    public string? OldValue { get; set; }

    public string? NewValue { get; set; }

    public DateOnly ChangeDate { get; set; }

    public string? Reason { get; set; }

    public int? ChangedBy { get; set; }

    public DateTime CreatedDate { get; set; }

    public virtual Employee? ChangedByNavigation { get; set; }

    public virtual Employee Employee { get; set; } = null!;
}
