using ProjectPRN232_HRM.DTOs;

namespace ProjectPRN232_HRM.Services.Interface
{
    public interface IEmployeeService
    {
        Task<List<EmployeeDetailDTO>> GetAllAsync();
        Task<EmployeeDetailDTO?> GetByIdAsync(int id);
        Task<bool> ExistsAsync(int id);
        Task AddAsync(EmployeeDetailDTO employeeDto);
        Task UpdateAsync(int id, EmployeeDetailDTO employeeDto);
        Task<List<EmployeeDetailDTO>> SearchByNameAsync(string name);

    }
}
