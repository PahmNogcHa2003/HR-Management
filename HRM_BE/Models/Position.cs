using System;
using System.Collections.Generic;

namespace ProjectPRN232_HRM.Models;

public partial class Position
{
    public int Id { get; set; }

    public string PositionCode { get; set; } = null!;

    public string Title { get; set; } = null!;

    public decimal? SalaryFactor { get; set; }

    public int? Level { get; set; }

    public string? Description { get; set; }

    public DateTime CreatedDate { get; set; }

    public DateTime? ModifiedDate { get; set; }

    public int? CreatedBy { get; set; }

    public int? ModifiedBy { get; set; }

    public virtual Employee? CreatedByNavigation { get; set; }

    public virtual ICollection<Employee> Employees { get; set; } = new List<Employee>();

    public virtual Employee? ModifiedByNavigation { get; set; }
}
