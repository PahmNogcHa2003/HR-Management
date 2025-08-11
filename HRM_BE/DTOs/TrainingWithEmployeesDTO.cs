namespace ProjectPRN232_HRM.DTOs
{
    public class TrainingWithEmployeesDTO
    {

            public string ProgramName { get; set; } = null!;
            public string Provider { get; set; } = null!;
            public DateTime? StartDate { get; set; }
            public DateTime? EndDate { get; set; }

            public List<EmployeeDTO> Employees { get; set; } = new();
        }
    }

