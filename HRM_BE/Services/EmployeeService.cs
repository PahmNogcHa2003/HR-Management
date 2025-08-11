using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using ProjectPRN232_HRM.DTOs;
using ProjectPRN232_HRM.Models;
using ProjectPRN232_HRM.Services.Interface;
using static ProjectPRN232_HRM.Services.EmployeeService;

namespace ProjectPRN232_HRM.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly ProjectPrn232HrmanagementContext _context;
        private readonly IMapper _mapper;

        public EmployeeService(ProjectPrn232HrmanagementContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<EmployeeDetailDTO>> GetAllAsync()
        {
            var employees = await _context.Employees
                .Include(e => e.Department)
                .Include(e => e.Position)
                .Include(e => e.EmployeeSkills)
                    .ThenInclude(es => es.Skill)
                .Include(e => e.EmployeeTrainings)
                    .ThenInclude(et => et.TrainingProgram)
                .ProjectTo<EmployeeDetailDTO>(_mapper.ConfigurationProvider)
                .ToListAsync();

            return employees;
        }

        public async Task<EmployeeDetailDTO?> GetByIdAsync(int id)
        {
            var employee = await _context.Employees
                .Include(e => e.Department)
                .Include(e => e.Position)
                .Include(e => e.EmployeeSkills)
                    .ThenInclude(es => es.Skill)
                .Include(e => e.EmployeeTrainings)
                    .ThenInclude(et => et.TrainingProgram)
                .FirstOrDefaultAsync(e => e.Id == id);

            return employee != null ? _mapper.Map<EmployeeDetailDTO>(employee) : null;
        }

        public async Task<bool> ExistsAsync(int id)
        {
            return await _context.Employees.AnyAsync(e => e.Id == id);
        }

        public async Task AddAsync(EmployeeDetailDTO employeeDto)
        {
            var employee = _mapper.Map<Employee>(employeeDto);
            _context.Employees.Add(employee);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(int id, EmployeeDetailDTO employeeDto)
        {
            var employee = await _context.Employees.FindAsync(id);
            if (employee == null) throw new Exception("Employee not found.");

            _mapper.Map(employeeDto, employee);
            _context.Employees.Update(employee);
            await _context.SaveChangesAsync();
        }
        public async Task<List<EmployeeDetailDTO>> SearchByNameAsync(string name)
        {
            var employees = await _context.Employees
                .Include(e => e.Department)
                .Include(e => e.Position)
                .Include(e => e.EmployeeSkills)
                    .ThenInclude(es => es.Skill)
                .Include(e => e.EmployeeTrainings)
                    .ThenInclude(et => et.TrainingProgram)
                .Where(e => e.FullName.Contains(name)) 
                .ProjectTo<EmployeeDetailDTO>(_mapper.ConfigurationProvider)
                .ToListAsync();

            return employees;
        }

     

         
        }

    }
