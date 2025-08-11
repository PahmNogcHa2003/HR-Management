using System;
using System.Collections.Generic;

namespace ProjectPRN232_HRM.Models;

public partial class Salary
{
    public int Id { get; set; }

    public int EmployeeId { get; set; }  

    public int Month { get; set; }

    public int Year { get; set; }

    public decimal BasicSalary { get; set; }

    public decimal? Allowance { get; set; }

    public decimal? Bonus { get; set; }

    public decimal? OvertimePay { get; set; }

    public decimal? Deduction { get; set; }

    public decimal? Tax { get; set; }

    public decimal? Insurance { get; set; }

    public decimal? NetSalary { get; set; }

    public string? Status { get; set; }

    public DateTime CreatedDate { get; set; }

    public DateTime? ModifiedDate { get; set; }

    public int? CreatedBy { get; set; }

    public int? ModifiedBy { get; set; }

    public int? ApprovedBy { get; set; }

    public virtual Employee? ApprovedByNavigation { get; set; }

    public virtual Employee? CreatedByNavigation { get; set; }

    public virtual Employee Employee { get; set; } = null!;

    public virtual Employee? ModifiedByNavigation { get; set; }
}
