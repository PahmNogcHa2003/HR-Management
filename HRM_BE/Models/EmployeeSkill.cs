using System;
using System.Collections.Generic;

namespace ProjectPRN232_HRM.Models;

public partial class EmployeeSkill
{
    public int EmployeeId { get; set; }

    public int SkillId { get; set; }

    public string? ProficiencyLevel { get; set; }

    public string? Certification { get; set; }

    public DateOnly? CertifiedDate { get; set; }

    public DateOnly? ExpiryDate { get; set; }

    public string? Notes { get; set; }

    public virtual Employee Employee { get; set; } = null!;

    public virtual Skill Skill { get; set; } = null!;
}
