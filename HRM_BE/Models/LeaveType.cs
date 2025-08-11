using System;
using System.Collections.Generic;

namespace ProjectPRN232_HRM.Models;

public partial class LeaveType
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public bool? IsPaid { get; set; }

    public int? MaxDaysPerYear { get; set; }

    public DateTime CreatedDate { get; set; }

    public DateTime? ModifiedDate { get; set; }

    public int? CreatedBy { get; set; }

    public int? ModifiedBy { get; set; }

    public virtual Employee? CreatedByNavigation { get; set; }

    public virtual ICollection<LeaveRequest> LeaveRequests { get; set; } = new List<LeaveRequest>();

    public virtual Employee? ModifiedByNavigation { get; set; }
}
