using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ProjectPRN232_HRM.Models;

public partial class Employee
{
    public int Id { get; set; }

    [Required]
    [StringLength(20)]
    public string EmployeeCode { get; set; } = null!;

    [Required]
    [StringLength(100)]
    public string FullName { get; set; } = null!;

    public string? Gender { get; set; }

    public DateOnly? Dob { get; set; }

    public DateOnly JoinDate { get; set; }

    [Required]
    [EmailAddress]
    [StringLength(100)]
    public string Email { get; set; } = null!;

    public string? PhoneNumber { get; set; }

    public string? Address { get; set; }

    public string? NationalId { get; set; }

    public string? TaxCode { get; set; }

    public string? BankAccount { get; set; }

    public string? BankName { get; set; }

    public int? DepartmentId { get; set; }

    public int? PositionId { get; set; }

    public int? ManagerId { get; set; }

    public string? AvatarUrl { get; set; }

    [Required]
    [StringLength(20)]
    public string Status { get; set; } = "Active";

    public DateTime CreatedDate { get; set; } = DateTime.Now;

    public DateTime? ModifiedDate { get; set; }

    public int? CreatedBy { get; set; }

    public int? ModifiedBy { get; set; }

    // Foreign key to ApplicationUser
    public int? UserId { get; set; }

    // Navigation property to ApplicationUser
    public virtual ApplicationUser? User { get; set; }

    public virtual ICollection<Attendance> AttendanceApprovedByNavigations { get; set; } = new List<Attendance>();

    public virtual ICollection<Attendance> AttendanceEmployees { get; set; } = new List<Attendance>();

    public virtual ICollection<Contract> ContractCreatedByNavigations { get; set; } = new List<Contract>();

    public virtual ICollection<Contract> ContractEmployees { get; set; } = new List<Contract>();

    public virtual ICollection<Contract> ContractModifiedByNavigations { get; set; } = new List<Contract>();

    public virtual Employee? CreatedByNavigation { get; set; }

    public virtual Department? Department { get; set; }

    public virtual ICollection<Department> DepartmentCreatedByNavigations { get; set; } = new List<Department>();

    public virtual ICollection<Department> DepartmentModifiedByNavigations { get; set; } = new List<Department>();

    public virtual ICollection<EmergencyContact> EmergencyContacts { get; set; } = new List<EmergencyContact>();

    public virtual ICollection<EmployeeDocument> EmployeeDocumentCreatedByNavigations { get; set; } = new List<EmployeeDocument>();

    public virtual ICollection<EmployeeDocument> EmployeeDocumentEmployees { get; set; } = new List<EmployeeDocument>();

    public virtual ICollection<EmployeeDocument> EmployeeDocumentModifiedByNavigations { get; set; } = new List<EmployeeDocument>();

    public virtual ICollection<EmployeeHistory> EmployeeHistoryChangedByNavigations { get; set; } = new List<EmployeeHistory>();

    public virtual ICollection<EmployeeHistory> EmployeeHistoryEmployees { get; set; } = new List<EmployeeHistory>();

    public virtual ICollection<EmployeeSkill> EmployeeSkills { get; set; } = new List<EmployeeSkill>();

    public virtual ICollection<EmployeeTraining> EmployeeTrainings { get; set; } = new List<EmployeeTraining>();

    public virtual ICollection<Employee> InverseCreatedByNavigation { get; set; } = new List<Employee>();

    public virtual ICollection<Employee> InverseManager { get; set; } = new List<Employee>();

    public virtual ICollection<Employee> InverseModifiedByNavigation { get; set; } = new List<Employee>();

    public virtual ICollection<LeaveRequest> LeaveRequestApprovedByNavigations { get; set; } = new List<LeaveRequest>();

    public virtual ICollection<LeaveRequest> LeaveRequestEmployees { get; set; } = new List<LeaveRequest>();

    public virtual ICollection<LeaveType> LeaveTypeCreatedByNavigations { get; set; } = new List<LeaveType>();

    public virtual ICollection<LeaveType> LeaveTypeModifiedByNavigations { get; set; } = new List<LeaveType>();

    public virtual Employee? Manager { get; set; }

    public virtual Employee? ModifiedByNavigation { get; set; }

    public virtual ICollection<PerformanceReview> PerformanceReviewApprovedByNavigations { get; set; } = new List<PerformanceReview>();

    public virtual ICollection<PerformanceReview> PerformanceReviewEmployees { get; set; } = new List<PerformanceReview>();

    public virtual ICollection<PerformanceReview> PerformanceReviewReviewers { get; set; } = new List<PerformanceReview>();

    public virtual Position? Position { get; set; }

    public virtual ICollection<Position> PositionCreatedByNavigations { get; set; } = new List<Position>();

    public virtual ICollection<Position> PositionModifiedByNavigations { get; set; } = new List<Position>();

    public virtual ICollection<RewardDiscipline> RewardDisciplineApprovedByNavigations { get; set; } = new List<RewardDiscipline>();

    public virtual ICollection<RewardDiscipline> RewardDisciplineCreatedByNavigations { get; set; } = new List<RewardDiscipline>();

    public virtual ICollection<RewardDiscipline> RewardDisciplineEmployees { get; set; } = new List<RewardDiscipline>();

    public virtual ICollection<Salary> SalaryApprovedByNavigations { get; set; } = new List<Salary>();

    public virtual ICollection<Salary> SalaryCreatedByNavigations { get; set; } = new List<Salary>();

    public virtual ICollection<Salary> SalaryEmployees { get; set; } = new List<Salary>();

    public virtual ICollection<Salary> SalaryModifiedByNavigations { get; set; } = new List<Salary>();

    public virtual ICollection<Skill> SkillCreatedByNavigations { get; set; } = new List<Skill>();

    public virtual ICollection<Skill> SkillModifiedByNavigations { get; set; } = new List<Skill>();

    public virtual ICollection<TrainingProgram> TrainingProgramCreatedByNavigations { get; set; } = new List<TrainingProgram>();

    public virtual ICollection<TrainingProgram> TrainingProgramModifiedByNavigations { get; set; } = new List<TrainingProgram>();
}
