using System;
using System.Collections.Generic;

namespace ProjectPRN232_HRM.Models;

public partial class EmployeeDocument
{
    public int Id { get; set; }

    public int EmployeeId { get; set; }

    public string DocumentType { get; set; } = null!;

    public string Title { get; set; } = null!;

    public DateOnly? IssueDate { get; set; }

    public DateOnly? ExpiryDate { get; set; }

    public string DocumentUrl { get; set; } = null!;

    public string? Description { get; set; }

    public string? Status { get; set; }

    public DateTime CreatedDate { get; set; }

    public DateTime? ModifiedDate { get; set; }

    public int? CreatedBy { get; set; }

    public int? ModifiedBy { get; set; }

    public virtual Employee? CreatedByNavigation { get; set; }

    public virtual Employee Employee { get; set; } = null!;

    public virtual Employee? ModifiedByNavigation { get; set; }
}
