using System;
using System.Collections.Generic;

namespace ProjectPRN232_HRM.Models;

public partial class Contract
{
    public int Id { get; set; }

    public int EmployeeId { get; set; }

    public string ContractType { get; set; } = null!;

    public string ContractNumber { get; set; } = null!;

    public DateOnly StartDate { get; set; }

    public DateOnly? EndDate { get; set; }

    public decimal BaseSalary { get; set; }

    public DateOnly? SignedDate { get; set; }

    public string Status { get; set; } = null!;

    public string? DocumentUrl { get; set; }

    public string? JobDescription { get; set; }

    public string? WorkingHours { get; set; }

    public int? ProbationPeriod { get; set; }

    public decimal? ProbationSalary { get; set; }

    public string? Benefits { get; set; }

    public string? TermsAndConditions { get; set; }

    public string? SigningPlace { get; set; }

    public DateTime CreatedDate { get; set; }

    public DateTime? ModifiedDate { get; set; }

    public int? CreatedBy { get; set; }

    public int? ModifiedBy { get; set; }

    public virtual Employee? CreatedByNavigation { get; set; }

    public virtual Employee Employee { get; set; } = null!;

    public virtual Employee? ModifiedByNavigation { get; set; }
}
