using System;
using System.Collections.Generic;

namespace ProjectPRN232_HRM.Models;

public partial class RewardDiscipline
{
    public int Id { get; set; }

    public int EmployeeId { get; set; }

    public string Type { get; set; } = null!;

    public string Title { get; set; } = null!;

    public string? Description { get; set; }

    public DateOnly EffectiveDate { get; set; }

    public decimal? Amount { get; set; }

    public string? DecisionNumber { get; set; }

    public string? DocumentUrl { get; set; }

    public string? Status { get; set; }

    public DateTime CreatedDate { get; set; }

    public DateTime? ModifiedDate { get; set; }

    public int? CreatedBy { get; set; }

    public int? ApprovedBy { get; set; }

    public virtual Employee? ApprovedByNavigation { get; set; }

    public virtual Employee? CreatedByNavigation { get; set; }

    public virtual Employee Employee { get; set; } = null!;
}
