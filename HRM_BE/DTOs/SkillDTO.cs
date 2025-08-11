namespace ProjectPRN232_HRM.DTOs
{
    public class SkillDTO
    {
        public int Id { get; set; }
        public string SkillCode { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public string? Category { get; set; }
    }

}
