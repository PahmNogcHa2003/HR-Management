using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ProjectPRN232_HRM.DTOs;
using ProjectPRN232_HRM.Models;
using ProjectPRN232_HRM.Services.Interface;

namespace ProjectPRN232_HRM.Services
{
    public class TrainingProgramSerivce : ITrainingProgramService
    {
        private readonly ProjectPrn232HrmanagementContext _context;
        private readonly IMapper _mapper;

        public TrainingProgramSerivce(ProjectPrn232HrmanagementContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<List<TrainingWithEmployeesDTO>> SearchProgramsByProviderAsync(string provider)
        {
            return await _context.TrainingPrograms
                .Where(p => p.Provider.Contains(provider))
                .Include(p => p.EmployeeTrainings)
                    .ThenInclude(et => et.Employee)
                .Select(p => new TrainingWithEmployeesDTO
                {
                    ProgramName = p.Name,
                    Provider = p.Provider,
                    Employees = p.EmployeeTrainings.Select(et => new EmployeeDTO
                    {
                        Id = et.Employee.Id,
                        FullName = et.Employee.FullName,
                        Email = et.Employee.Email
                    }).ToList()
                })
                .ToListAsync();
        }
    }
}
