using ProjectPRN232_HRM.DTOs;

namespace ProjectPRN232_HRM.Services.Interface
{
    public interface ITrainingProgramService
    {
        Task<List<TrainingWithEmployeesDTO>> SearchProgramsByProviderAsync(string provider);
    }
}
