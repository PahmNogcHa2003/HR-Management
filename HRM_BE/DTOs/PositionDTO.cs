namespace ProjectPRN232_HRM.DTOs
{
    public class PositionDTO
    {
        public int Id { get; set; }
        public string PositionCode { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        public decimal SalaryFactor { get; set; }
        public int? Level { get; set; }
        public string? Description { get; set; }
    }

}
