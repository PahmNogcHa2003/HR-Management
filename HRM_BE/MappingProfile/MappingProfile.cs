using AutoMapper;
using ProjectPRN232_HRM.DTOs;
using ProjectPRN232_HRM.Models;

namespace ProjectPRN232_HRM.MappingProfile
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Department, DepartmentDTO>().ReverseMap();
            CreateMap<Position, PositionDTO>().ReverseMap();
            CreateMap<Employee, EmployeeDTO>().ReverseMap();
            CreateMap<Skill, SkillDTO>().ReverseMap();
            CreateMap<TrainingProgram, TrainingProgramDTO>().ReverseMap();
            CreateMap<EmployeeSkill, EmployeeSkillDTO>()
                .ForMember(dest => dest.Skill, opt => opt.MapFrom(src => src.Skill));

            CreateMap<EmployeeTraining, EmployeeTrainingDTO>()
                .ForMember(dest => dest.TrainingProgram, opt => opt.MapFrom(src => src.TrainingProgram));
            CreateMap<Employee, EmployeeDetailDTO>()
                .ForMember(dest => dest.DepartmentName,
             opt => opt.MapFrom(src => src.Department != null ? src.Department.Name : null))
                .ForMember(dest => dest.PositionTitle,
             opt => opt.MapFrom(src => src.Position != null ? src.Position.Title : null))
                .ForMember(dest => dest.Skills,
             opt => opt.MapFrom(src => src.EmployeeSkills))
                .ForMember(dest => dest.Trainings,
             opt => opt.MapFrom(src => src.EmployeeTrainings));

        }
    }
}
