using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ProjectPRN232_HRM.DTOs;
using ProjectPRN232_HRM.Models;
using ProjectPRN232_HRM.Services.Interface;

namespace ProjectPRN232_HRM.Services
{
    public class DepartmentService : IDepartmentService
    {
        private readonly ProjectPrn232HrmanagementContext _context;
        private readonly IMapper _mapper;

        public DepartmentService(ProjectPrn232HrmanagementContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<IEnumerable<DepartmentDTO>> GetAllAsync()
        {
            var data = await _context.Departments.ToListAsync();
            return _mapper.Map<List<DepartmentDTO>>(data);
        }

        public async Task<DepartmentDTO?> GetByIdAsync(int id)
        {
            var dept = await _context.Departments.FindAsync(id);
            return dept == null ? null : _mapper.Map<DepartmentDTO>(dept);
        }

        public async Task<DepartmentDTO> CreateAsync(DepartmentDTO dto)
        {
            var dept = _mapper.Map<Department>(dto);
            _context.Departments.Add(dept);
            await _context.SaveChangesAsync();
            return _mapper.Map<DepartmentDTO>(dept);
        }

        public async Task<bool> UpdateAsync(int id, DepartmentDTO dto)
        {
            var dept = await _context.Departments.FindAsync(id);
            if (dept == null) return false;

            _mapper.Map(dto, dept);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
