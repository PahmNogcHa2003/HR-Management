namespace ProjectPRN232_HRM.DTOs
{
    public class EmployeeDetailDTO
    {
        public int Id { get; set; }
        public string EmployeeCode { get; set; } = null!;
        public string FullName { get; set; } = null!;
        public string? Gender { get; set; }
        public DateOnly? DOB { get; set; }
        public DateOnly JoinDate { get; set; }
        public string Email { get; set; } = null!;
        public string? PhoneNumber { get; set; }

        public string? Address { get; set; }

        public string? DepartmentName { get; set; }
        public string? PositionTitle { get; set; }

        public List<EmployeeSkillDTO> Skills { get; set; } = new();
        public List<EmployeeTrainingDTO> Trainings { get; set; } = new();
    }

}
