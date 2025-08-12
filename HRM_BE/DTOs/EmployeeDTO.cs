namespace ProjectPRN232_HRM.DTOs
{
    public class EmployeeDTO
    {
        public int Id { get; set; }
        public string EmployeeCode { get; set; } = string.Empty;
        public string FullName { get; set; } = string.Empty;
        public string? Gender { get; set; }
        public DateOnly? DOB { get; set; }
        public DateOnly JoinDate { get; set; }
        public string Email { get; set; } = string.Empty;
        public string? PhoneNumber { get; set; }
        public string? Address { get; set; }
        public string? NationalID { get; set; }
        public string? TaxCode { get; set; }
        public string? BankAccount { get; set; }
        public string? BankName { get; set; }
        public int? DepartmentId { get; set; }
        public string? DepartmentName { get; set; }
        public string? PositionTitle { get; set; }
        public int? PositionId { get; set; }
        public int? ManagerId { get; set; }
        public string? AvatarUrl { get; set; }
        public string Status { get; set; } = "Active";
    }

}
