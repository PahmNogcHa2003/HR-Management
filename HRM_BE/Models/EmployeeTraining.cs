using System;
using System.Collections.Generic;

namespace ProjectPRN232_HRM.Models;

public partial class EmployeeTraining
{
    public int EmployeeId { get; set; }

    public int TrainingProgramId { get; set; }

    public DateOnly RegistrationDate { get; set; }

    public string? CompletionStatus { get; set; }

    public string? Result { get; set; }

    public string? CertificateUrl { get; set; }

    public string? Notes { get; set; }

    public virtual Employee Employee { get; set; } = null!;

    public virtual TrainingProgram TrainingProgram { get; set; } = null!;
}
