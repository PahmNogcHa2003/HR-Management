namespace ProjectPRN232_HRM.DTOs
{
    public class EmployeeSkillDTO
    {
        public int SkillId { get; set; }
        public string? ProficiencyLevel { get; set; }
        public string? Certification { get; set; }
        public DateOnly? CertifiedDate { get; set; }
        public DateOnly? ExpiryDate { get; set; }
        public string? Notes { get; set; }

        public SkillDTO Skill { get; set; } = null!;
    }

}
