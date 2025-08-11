using System;
using System.Collections.Generic;

namespace ProjectPRN232_HRM.Models;

public partial class PerformanceReview
{
    public int Id { get; set; }

    public int EmployeeId { get; set; }

    public string ReviewPeriod { get; set; } = null!;

    public DateOnly StartDate { get; set; }

    public DateOnly EndDate { get; set; }

    public int ReviewerId { get; set; }

    public decimal? PerformanceScore { get; set; }

    public decimal? CompetencyScore { get; set; }

    public decimal? BehaviorScore { get; set; }

    public decimal? OverallScore { get; set; }

    public string? Strengths { get; set; }

    public string? AreasForImprovement { get; set; }

    public string? DevelopmentPlan { get; set; }

    public string? Status { get; set; }

    public DateTime CreatedDate { get; set; }

    public DateTime? ModifiedDate { get; set; }

    public int? ApprovedBy { get; set; }

    public virtual Employee? ApprovedByNavigation { get; set; }

    public virtual Employee Employee { get; set; } = null!;

    public virtual Employee Reviewer { get; set; } = null!;
}
