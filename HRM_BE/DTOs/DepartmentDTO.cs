namespace ProjectPRN232_HRM.DTOs
{
    public class DepartmentDTO
    {
        public int Id { get; set; }
        public string DepartmentCode { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public int? ParentDepartmentId { get; set; }
    }

}
