using System;
using System.Collections.Generic;

namespace ProjectPRN232_HRM.Models;

public partial class Skill
{
    public int Id { get; set; }

    public string SkillCode { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public string? Category { get; set; }

    public DateTime CreatedDate { get; set; }

    public DateTime? ModifiedDate { get; set; }

    public int? CreatedBy { get; set; }

    public int? ModifiedBy { get; set; }

    public virtual Employee? CreatedByNavigation { get; set; }

    public virtual ICollection<EmployeeSkill> EmployeeSkills { get; set; } = new List<EmployeeSkill>();

    public virtual Employee? ModifiedByNavigation { get; set; }
}
