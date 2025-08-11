using System;
using System.Collections.Generic;

namespace ProjectPRN232_HRM.Models;

public partial class Department
{
    public int Id { get; set; }

    public string DepartmentCode { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public int? ParentDepartmentId { get; set; }

    public DateTime CreatedDate { get; set; }

    public DateTime? ModifiedDate { get; set; }

    public int? CreatedBy { get; set; }

    public int? ModifiedBy { get; set; }

    public virtual Employee? CreatedByNavigation { get; set; }

    public virtual ICollection<Employee> Employees { get; set; } = new List<Employee>();

    public virtual ICollection<Department> InverseParentDepartment { get; set; } = new List<Department>();

    public virtual Employee? ModifiedByNavigation { get; set; }

    public virtual Department? ParentDepartment { get; set; }
}
