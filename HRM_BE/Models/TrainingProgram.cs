using System;
using System.Collections.Generic;

namespace ProjectPRN232_HRM.Models;

public partial class TrainingProgram
{
    public int Id { get; set; }

    public string ProgramCode { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public DateOnly? StartDate { get; set; }

    public DateOnly? EndDate { get; set; }

    public decimal? Cost { get; set; }

    public string? Provider { get; set; }

    public string? Location { get; set; }

    public string? Status { get; set; }

    public DateTime CreatedDate { get; set; }

    public DateTime? ModifiedDate { get; set; }

    public int? CreatedBy { get; set; }

    public int? ModifiedBy { get; set; }

    public virtual Employee? CreatedByNavigation { get; set; }

    public virtual ICollection<EmployeeTraining> EmployeeTrainings { get; set; } = new List<EmployeeTraining>();

    public virtual Employee? ModifiedByNavigation { get; set; }
}
