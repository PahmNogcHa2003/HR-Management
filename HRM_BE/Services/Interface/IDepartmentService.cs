using ProjectPRN232_HRM.DTOs;

namespace ProjectPRN232_HRM.Services.Interface
{
    public interface IDepartmentService
    {
        
            Task<IEnumerable<DepartmentDTO>> GetAllAsync();
            Task<DepartmentDTO?> GetByIdAsync(int id);
            Task<DepartmentDTO> CreateAsync(DepartmentDTO dto);
            Task<bool> UpdateAsync(int id, DepartmentDTO dto);

    }
}
