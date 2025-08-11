namespace ProjectPRN232_HRM.DTOs
{
    public class TrainingProgramDTO
    {
        public int Id { get; set; }
        public string ProgramCode { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public DateOnly? StartDate { get; set; }
        public DateOnly? EndDate { get; set; }
        public decimal? Cost { get; set; }
        public string? Provider { get; set; }
        public string? Location { get; set; }
        public string? Status { get; set; }
    }

}
