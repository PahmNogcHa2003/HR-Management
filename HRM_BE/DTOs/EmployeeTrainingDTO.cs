namespace ProjectPRN232_HRM.DTOs
{
    public class EmployeeTrainingDTO
    {
        public int TrainingProgramId { get; set; }
        public DateOnly RegistrationDate { get; set; }
        public string? CompletionStatus { get; set; }
        public string? Result { get; set; }
        public string? CertificateUrl { get; set; }
        public string? Notes { get; set; }

        public TrainingProgramDTO TrainingProgram { get; set; } = null!;
    }

}
