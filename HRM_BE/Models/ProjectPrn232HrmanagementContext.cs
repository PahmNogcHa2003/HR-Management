using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ProjectPRN232_HRM.Models;

public partial class ProjectPrn232HrmanagementContext : IdentityDbContext<ApplicationUser, IdentityRole<int>, int>
{
    public ProjectPrn232HrmanagementContext()
    {
    }

    public ProjectPrn232HrmanagementContext(DbContextOptions<ProjectPrn232HrmanagementContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Attendance> Attendances { get; set; }

    public virtual DbSet<Contract> Contracts { get; set; }

    public virtual DbSet<Department> Departments { get; set; }

    public virtual DbSet<EmergencyContact> EmergencyContacts { get; set; }

    public virtual DbSet<Employee> Employees { get; set; }

    public virtual DbSet<EmployeeDocument> EmployeeDocuments { get; set; }

    public virtual DbSet<EmployeeHistory> EmployeeHistories { get; set; }

    public virtual DbSet<EmployeeSkill> EmployeeSkills { get; set; }

    public virtual DbSet<EmployeeTraining> EmployeeTrainings { get; set; }

    public virtual DbSet<LeaveRequest> LeaveRequests { get; set; }

    public virtual DbSet<LeaveType> LeaveTypes { get; set; }

    public virtual DbSet<PerformanceReview> PerformanceReviews { get; set; }

    public virtual DbSet<Position> Positions { get; set; }

    public virtual DbSet<RewardDiscipline> RewardDisciplines { get; set; }

    public virtual DbSet<Salary> Salaries { get; set; }

    public virtual DbSet<Skill> Skills { get; set; }

    public virtual DbSet<TrainingProgram> TrainingPrograms { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Attendance>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Attendan__3214EC077941A050");

            entity.ToTable("Attendance");

            entity.Property(e => e.CreatedDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.ModifiedDate).HasColumnType("datetime");
            entity.Property(e => e.Notes).HasMaxLength(255);
            entity.Property(e => e.Status)
                .HasMaxLength(20)
                .HasDefaultValue("Pending");
            entity.Property(e => e.Type)
                .HasMaxLength(20)
                .HasDefaultValue("Normal");
            entity.Property(e => e.WorkHours).HasColumnType("decimal(4, 2)");

            entity.HasOne(d => d.ApprovedByNavigation).WithMany(p => p.AttendanceApprovedByNavigations)
                .HasForeignKey(d => d.ApprovedBy)
                .HasConstraintName("FK__Attendanc__Appro__5FB337D6");

            entity.HasOne(d => d.Employee).WithMany(p => p.AttendanceEmployees)
                .HasForeignKey(d => d.EmployeeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Attendanc__Emplo__5EBF139D");
        });

        modelBuilder.Entity<Contract>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Contract__3214EC07D13357EB");

            entity.ToTable("Contract");

            entity.HasIndex(e => e.ContractNumber, "UQ__Contract__C51D43DAC7FAEC34").IsUnique();

            entity.Property(e => e.BaseSalary).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.ContractNumber).HasMaxLength(50);
            entity.Property(e => e.ContractType).HasMaxLength(50);
            entity.Property(e => e.CreatedDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.DocumentUrl).HasMaxLength(255);
            entity.Property(e => e.ModifiedDate).HasColumnType("datetime");
            entity.Property(e => e.ProbationSalary).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.SigningPlace).HasMaxLength(100);
            entity.Property(e => e.Status).HasMaxLength(20);
            entity.Property(e => e.WorkingHours).HasMaxLength(100);

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.ContractCreatedByNavigations)
                .HasForeignKey(d => d.CreatedBy)
                .HasConstraintName("FK__Contract__Create__71D1E811");

            entity.HasOne(d => d.Employee).WithMany(p => p.ContractEmployees)
                .HasForeignKey(d => d.EmployeeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Contract__Employ__70DDC3D8");

            entity.HasOne(d => d.ModifiedByNavigation).WithMany(p => p.ContractModifiedByNavigations)
                .HasForeignKey(d => d.ModifiedBy)
                .HasConstraintName("FK__Contract__Modifi__72C60C4A");
        });

        modelBuilder.Entity<Department>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Departme__3214EC07E1D39C43");

            entity.ToTable("Department");

            entity.HasIndex(e => e.DepartmentCode, "UQ__Departme__6EA8896D2AEA9098").IsUnique();

            entity.Property(e => e.CreatedDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.DepartmentCode).HasMaxLength(20);
            entity.Property(e => e.Description).HasMaxLength(255);
            entity.Property(e => e.ModifiedDate).HasColumnType("datetime");
            entity.Property(e => e.Name).HasMaxLength(100);

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.DepartmentCreatedByNavigations)
                .HasForeignKey(d => d.CreatedBy)
                .HasConstraintName("FK_Department_CreatedBy_Employee_Id");

            entity.HasOne(d => d.ModifiedByNavigation).WithMany(p => p.DepartmentModifiedByNavigations)
                .HasForeignKey(d => d.ModifiedBy)
                .HasConstraintName("FK_Department_ModifiedBy_Employee_Id");

            entity.HasOne(d => d.ParentDepartment).WithMany(p => p.InverseParentDepartment)
                .HasForeignKey(d => d.ParentDepartmentId)
                .HasConstraintName("FK__Departmen__Paren__4AB81AF0");
        });

        modelBuilder.Entity<EmergencyContact>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Emergenc__3214EC079A5E9DFB");

            entity.ToTable("EmergencyContact");

            entity.Property(e => e.Address).HasMaxLength(255);
            entity.Property(e => e.CreatedDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Email).HasMaxLength(100);
            entity.Property(e => e.FullName).HasMaxLength(100);
            entity.Property(e => e.IsPrimary).HasDefaultValue(false);
            entity.Property(e => e.ModifiedDate).HasColumnType("datetime");
            entity.Property(e => e.Notes).HasMaxLength(255);
            entity.Property(e => e.Phone).HasMaxLength(20);
            entity.Property(e => e.Relationship).HasMaxLength(50);

            entity.HasOne(d => d.Employee).WithMany(p => p.EmergencyContacts)
                .HasForeignKey(d => d.EmployeeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Emergency__Emplo__1CBC4616");
        });

        modelBuilder.Entity<Employee>(entity =>
        {
            // 1. Cấu hình bảng và khóa chính
            entity.ToTable("Employee");
            entity.HasKey(e => e.Id).HasName("PK__Employee__3214EC07DAF4A017");

            // Đảm bảo Id tự tăng
            entity.Property(e => e.Id)
                  .ValueGeneratedOnAdd()
                  .UseIdentityColumn();

            // 2. Cấu hình các trường nghiệp vụ
            entity.Property(e => e.EmployeeCode)
                  .HasMaxLength(20)
                  .IsRequired();

            entity.Property(e => e.FullName)
                  .HasMaxLength(100)
                  .IsRequired();

            entity.Property(e => e.Email)
                  .HasMaxLength(100)
                  .IsRequired();

            entity.Property(e => e.Address)
                  .HasMaxLength(255);

            entity.Property(e => e.AvatarUrl)
                  .HasMaxLength(255);

            entity.Property(e => e.BankAccount)
                  .HasMaxLength(50);

            entity.Property(e => e.BankName)
                  .HasMaxLength(100);

            entity.Property(e => e.CreatedDate)
                  .HasDefaultValueSql("(getdate())")
                  .HasColumnType("datetime");

            entity.Property(e => e.Dob)
                  .HasColumnName("DOB");

            entity.Property(e => e.Gender)
                  .HasMaxLength(10);

            entity.Property(e => e.ModifiedDate)
                  .HasColumnType("datetime");

            entity.Property(e => e.NationalId)
                  .HasMaxLength(20)
                  .HasColumnName("NationalID");

            entity.Property(e => e.PhoneNumber)
                  .HasMaxLength(20);

            entity.Property(e => e.Status)
                  .HasMaxLength(20)
                  .HasDefaultValue("Active");

            entity.Property(e => e.TaxCode)
                  .HasMaxLength(20);

            // 3. Cấu hình index
            entity.HasIndex(e => e.Email)
                  .IsUnique()
                  .HasDatabaseName("IX_Employee_Email");

            entity.HasIndex(e => e.EmployeeCode)
                  .IsUnique()
                  .HasDatabaseName("UQ__Employee__1F642548318A6A1A");

            // 4. Cấu hình các quan hệ
            entity.HasOne(d => d.CreatedByNavigation)
                  .WithMany(p => p.InverseCreatedByNavigation)
                  .HasForeignKey(d => d.CreatedBy)
                  .HasConstraintName("FK__Employee__Create__5441852A");

            entity.HasOne(d => d.Department)
                  .WithMany(p => p.Employees)
                  .HasForeignKey(d => d.DepartmentId)
                  .HasConstraintName("FK__Employee__Depart__5165187F");

            entity.HasOne(d => d.Manager)
                  .WithMany(p => p.InverseManager)
                  .HasForeignKey(d => d.ManagerId)
                  .HasConstraintName("FK__Employee__Manage__534D60F1");

            entity.HasOne(d => d.ModifiedByNavigation)
                  .WithMany(p => p.InverseModifiedByNavigation)
                  .HasForeignKey(d => d.ModifiedBy)
                  .HasConstraintName("FK__Employee__Modifi__5535A963");

            entity.HasOne(d => d.Position)
                  .WithMany(p => p.Employees)
                  .HasForeignKey(d => d.PositionId)
                  .HasConstraintName("FK__Employee__Positi__52593CB8");

            // 5. Cấu hình quan hệ 1-1 với ApplicationUser
            entity.HasOne(d => d.User)
                  .WithOne(u => u.Employee)
                  .HasForeignKey<Employee>(d => d.UserId)
                  .HasConstraintName("FK_Employee_ApplicationUser_UserId");
        });

        // Cấu hình ApplicationUser
        modelBuilder.Entity<ApplicationUser>(entity =>
        {
            entity.ToTable("AspNetUsers");
            
        });

        modelBuilder.Entity<IdentityRole<int>>(b =>
        {
            b.Property(r => r.Name).HasMaxLength(256);
            b.Property(r => r.NormalizedName).HasMaxLength(256);
        });

        // CẤU HÌNH CÁC BẢNG IDENTITY NGOÀI PHẠM VI Employee
        modelBuilder.Entity<IdentityUserLogin<int>>(b =>
        {
            b.ToTable("AspNetUserLogins");  // Đặt tên bảng rõ ràng
            b.HasKey(l => new { l.LoginProvider, l.ProviderKey });
            b.Property(l => l.LoginProvider).HasMaxLength(128);
            b.Property(l => l.ProviderKey).HasMaxLength(128);
        });

        modelBuilder.Entity<IdentityUserRole<int>>(b =>
        {
            b.ToTable("AspNetUserRoles");
            b.HasKey(r => new { r.UserId, r.RoleId });
        });

        modelBuilder.Entity<IdentityUserToken<int>>(b =>
        {
            b.ToTable("AspNetUserTokens");
            b.HasKey(t => new { t.UserId, t.LoginProvider, t.Name });
            b.Property(t => t.LoginProvider).HasMaxLength(128);
            b.Property(t => t.Name).HasMaxLength(128);
        });

        modelBuilder.Entity<IdentityUserClaim<int>>(b =>
        {
            b.ToTable("AspNetUserClaims");
            b.HasKey(c => c.Id);
        });

        modelBuilder.Entity<IdentityRole<int>>(b =>
        {
            b.ToTable("AspNetRoles");
            b.Property(r => r.Name).HasMaxLength(256);
            b.Property(r => r.NormalizedName).HasMaxLength(256);
        });

        modelBuilder.Entity<IdentityRoleClaim<int>>(b =>
        {
            b.ToTable("AspNetRoleClaims");
            b.HasKey(rc => rc.Id);
        });

        modelBuilder.Entity<EmployeeDocument>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Employee__3214EC07AA36C12A");

            entity.ToTable("EmployeeDocument");

            entity.Property(e => e.CreatedDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Description).HasMaxLength(255);
            entity.Property(e => e.DocumentType).HasMaxLength(50);
            entity.Property(e => e.DocumentUrl).HasMaxLength(255);
            entity.Property(e => e.ModifiedDate).HasColumnType("datetime");
            entity.Property(e => e.Status).HasMaxLength(20);
            entity.Property(e => e.Title).HasMaxLength(100);

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.EmployeeDocumentCreatedByNavigations)
                .HasForeignKey(d => d.CreatedBy)
                .HasConstraintName("FK__EmployeeD__Creat__2180FB33");

            entity.HasOne(d => d.Employee).WithMany(p => p.EmployeeDocumentEmployees)
                .HasForeignKey(d => d.EmployeeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__EmployeeD__Emplo__208CD6FA");

            entity.HasOne(d => d.ModifiedByNavigation).WithMany(p => p.EmployeeDocumentModifiedByNavigations)
                .HasForeignKey(d => d.ModifiedBy)
                .HasConstraintName("FK__EmployeeD__Modif__22751F6C");
        });

        modelBuilder.Entity<EmployeeHistory>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Employee__3214EC0786FE778B");

            entity.ToTable("EmployeeHistory");

            entity.Property(e => e.ChangeType).HasMaxLength(50);
            entity.Property(e => e.CreatedDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.NewValue).HasMaxLength(255);
            entity.Property(e => e.OldValue).HasMaxLength(255);
            entity.Property(e => e.Reason).HasMaxLength(255);

            entity.HasOne(d => d.ChangedByNavigation).WithMany(p => p.EmployeeHistoryChangedByNavigations)
                .HasForeignKey(d => d.ChangedBy)
                .HasConstraintName("FK__EmployeeH__Chang__04E4BC85");

            entity.HasOne(d => d.Employee).WithMany(p => p.EmployeeHistoryEmployees)
                .HasForeignKey(d => d.EmployeeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__EmployeeH__Emplo__03F0984C");
        });

        modelBuilder.Entity<EmployeeSkill>(entity =>
        {
            entity.HasKey(e => new { e.EmployeeId, e.SkillId }).HasName("PK__Employee__172A4609FB330E1D");

            entity.ToTable("EmployeeSkill");

            entity.Property(e => e.Certification).HasMaxLength(100);
            entity.Property(e => e.Notes).HasMaxLength(255);
            entity.Property(e => e.ProficiencyLevel).HasMaxLength(20);

            entity.HasOne(d => d.Employee).WithMany(p => p.EmployeeSkills)
                .HasForeignKey(d => d.EmployeeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__EmployeeS__Emplo__0D7A0286");

            entity.HasOne(d => d.Skill).WithMany(p => p.EmployeeSkills)
                .HasForeignKey(d => d.SkillId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__EmployeeS__Skill__0E6E26BF");
        });

        modelBuilder.Entity<EmployeeTraining>(entity =>
        {
            entity.HasKey(e => new { e.EmployeeId, e.TrainingProgramId }).HasName("PK__Employee__BE28D8B49CBA82C8");

            entity.ToTable("EmployeeTraining");

            entity.Property(e => e.CertificateUrl).HasMaxLength(255);
            entity.Property(e => e.CompletionStatus).HasMaxLength(20);
            entity.Property(e => e.Notes).HasMaxLength(255);
            entity.Property(e => e.Result).HasMaxLength(50);

            entity.HasOne(d => d.Employee).WithMany(p => p.EmployeeTrainings)
                .HasForeignKey(d => d.EmployeeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__EmployeeT__Emplo__17036CC0");

            entity.HasOne(d => d.TrainingProgram).WithMany(p => p.EmployeeTrainings)
                .HasForeignKey(d => d.TrainingProgramId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__EmployeeT__Train__17F790F9");
        });

        modelBuilder.Entity<LeaveRequest>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__LeaveReq__3214EC071E003BB3");

            entity.ToTable("LeaveRequest");

            entity.Property(e => e.CreatedDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.ModifiedDate).HasColumnType("datetime");
            entity.Property(e => e.Reason).HasMaxLength(255);
            entity.Property(e => e.Status)
                .HasMaxLength(20)
                .HasDefaultValue("Pending");

            entity.HasOne(d => d.ApprovedByNavigation).WithMany(p => p.LeaveRequestApprovedByNavigations)
                .HasForeignKey(d => d.ApprovedBy)
                .HasConstraintName("FK__LeaveRequ__Appro__6C190EBB");

            entity.HasOne(d => d.Employee).WithMany(p => p.LeaveRequestEmployees)
                .HasForeignKey(d => d.EmployeeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__LeaveRequ__Emplo__6A30C649");

            entity.HasOne(d => d.LeaveType).WithMany(p => p.LeaveRequests)
                .HasForeignKey(d => d.LeaveTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__LeaveRequ__Leave__6B24EA82");
        });

        modelBuilder.Entity<LeaveType>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__LeaveTyp__3214EC07494E307E");

            entity.ToTable("LeaveType");

            entity.Property(e => e.CreatedDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Description).HasMaxLength(255);
            entity.Property(e => e.IsPaid).HasDefaultValue(true);
            entity.Property(e => e.ModifiedDate).HasColumnType("datetime");
            entity.Property(e => e.Name).HasMaxLength(50);

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.LeaveTypeCreatedByNavigations)
                .HasForeignKey(d => d.CreatedBy)
                .HasConstraintName("FK__LeaveType__Creat__6477ECF3");

            entity.HasOne(d => d.ModifiedByNavigation).WithMany(p => p.LeaveTypeModifiedByNavigations)
                .HasForeignKey(d => d.ModifiedBy)
                .HasConstraintName("FK__LeaveType__Modif__656C112C");
        });

        modelBuilder.Entity<PerformanceReview>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Performa__3214EC0798519EAF");

            entity.ToTable("PerformanceReview");

            entity.Property(e => e.BehaviorScore).HasColumnType("decimal(5, 2)");
            entity.Property(e => e.CompetencyScore).HasColumnType("decimal(5, 2)");
            entity.Property(e => e.CreatedDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.ModifiedDate).HasColumnType("datetime");
            entity.Property(e => e.OverallScore).HasColumnType("decimal(5, 2)");
            entity.Property(e => e.PerformanceScore).HasColumnType("decimal(5, 2)");
            entity.Property(e => e.ReviewPeriod).HasMaxLength(50);
            entity.Property(e => e.Status)
                .HasMaxLength(20)
                .HasDefaultValue("Draft");

            entity.HasOne(d => d.ApprovedByNavigation).WithMany(p => p.PerformanceReviewApprovedByNavigations)
                .HasForeignKey(d => d.ApprovedBy)
                .HasConstraintName("FK__Performan__Appro__29221CFB");

            entity.HasOne(d => d.Employee).WithMany(p => p.PerformanceReviewEmployees)
                .HasForeignKey(d => d.EmployeeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Performan__Emplo__2739D489");

            entity.HasOne(d => d.Reviewer).WithMany(p => p.PerformanceReviewReviewers)
                .HasForeignKey(d => d.ReviewerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Performan__Revie__282DF8C2");
        });

        modelBuilder.Entity<Position>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Position__3214EC0734B88FC7");

            entity.ToTable("Position");

            entity.HasIndex(e => e.PositionCode, "UQ__Position__83745B02F53B89C5").IsUnique();

            entity.Property(e => e.CreatedDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Description).HasMaxLength(255);
            entity.Property(e => e.ModifiedDate).HasColumnType("datetime");
            entity.Property(e => e.PositionCode).HasMaxLength(20);
            entity.Property(e => e.SalaryFactor)
                .HasDefaultValue(10m)
                .HasColumnType("decimal(5, 2)");
            entity.Property(e => e.Title).HasMaxLength(100);

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.PositionCreatedByNavigations)
                .HasForeignKey(d => d.CreatedBy)
                .HasConstraintName("FK_Position_CreatedBy_Employee_Id");

            entity.HasOne(d => d.ModifiedByNavigation).WithMany(p => p.PositionModifiedByNavigations)
                .HasForeignKey(d => d.ModifiedBy)
                .HasConstraintName("FK_Position_ModifiedBy_Employee_Id");
        });

        modelBuilder.Entity<RewardDiscipline>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__RewardDi__3214EC0733A3EF5E");

            entity.ToTable("RewardDiscipline");

            entity.Property(e => e.Amount).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.CreatedDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.DecisionNumber).HasMaxLength(50);
            entity.Property(e => e.DocumentUrl).HasMaxLength(255);
            entity.Property(e => e.ModifiedDate).HasColumnType("datetime");
            entity.Property(e => e.Status)
                .HasMaxLength(20)
                .HasDefaultValue("Active");
            entity.Property(e => e.Title).HasMaxLength(100);
            entity.Property(e => e.Type).HasMaxLength(20);

            entity.HasOne(d => d.ApprovedByNavigation).WithMany(p => p.RewardDisciplineApprovedByNavigations)
                .HasForeignKey(d => d.ApprovedBy)
                .HasConstraintName("FK__RewardDis__Appro__2FCF1A8A");

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.RewardDisciplineCreatedByNavigations)
                .HasForeignKey(d => d.CreatedBy)
                .HasConstraintName("FK__RewardDis__Creat__2EDAF651");

            entity.HasOne(d => d.Employee).WithMany(p => p.RewardDisciplineEmployees)
                .HasForeignKey(d => d.EmployeeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__RewardDis__Emplo__2DE6D218");
        });

        modelBuilder.Entity<Salary>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Salary__3214EC07EA4CDAAE");

            entity.ToTable("Salary");

            entity.Property(e => e.Allowance)
                .HasDefaultValue(0m)
                .HasColumnType("decimal(18, 2)");
            entity.Property(e => e.BasicSalary).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.Bonus)
                .HasDefaultValue(0m)
                .HasColumnType("decimal(18, 2)");
            entity.Property(e => e.CreatedDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Deduction)
                .HasDefaultValue(0m)
                .HasColumnType("decimal(18, 2)");
            entity.Property(e => e.Insurance)
                .HasDefaultValue(0m)
                .HasColumnType("decimal(18, 2)");
            entity.Property(e => e.ModifiedDate).HasColumnType("datetime");
            entity.Property(e => e.NetSalary)
                .HasComputedColumnSql("(((((([BasicSalary]+[Allowance])+[Bonus])+[OvertimePay])-[Deduction])-[Tax])-[Insurance])", true)
                .HasColumnType("decimal(24, 2)");
            entity.Property(e => e.OvertimePay)
                .HasDefaultValue(0m)
                .HasColumnType("decimal(18, 2)");
            entity.Property(e => e.Status)
                .HasMaxLength(20)
                .HasDefaultValue("Draft");
            entity.Property(e => e.Tax)
                .HasDefaultValue(0m)
                .HasColumnType("decimal(18, 2)");

            entity.HasOne(d => d.ApprovedByNavigation).WithMany(p => p.SalaryApprovedByNavigations)
                .HasForeignKey(d => d.ApprovedBy)
                .HasConstraintName("FK__Salary__Approved__00200768");

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.SalaryCreatedByNavigations)
                .HasForeignKey(d => d.CreatedBy)
                .HasConstraintName("FK__Salary__CreatedB__7E37BEF6");

            entity.HasOne(d => d.Employee).WithMany(p => p.SalaryEmployees)
                .HasForeignKey(d => d.EmployeeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Salary__Employee__7D439ABD");

            entity.HasOne(d => d.ModifiedByNavigation).WithMany(p => p.SalaryModifiedByNavigations)
                .HasForeignKey(d => d.ModifiedBy)
                .HasConstraintName("FK__Salary__Modified__7F2BE32F");
        });

        modelBuilder.Entity<Skill>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Skill__3214EC07E30DAA9C");

            entity.ToTable("Skill");

            entity.HasIndex(e => e.SkillCode, "UQ__Skill__59F57E0930860F63").IsUnique();

            entity.Property(e => e.Category).HasMaxLength(50);
            entity.Property(e => e.CreatedDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Description).HasMaxLength(255);
            entity.Property(e => e.ModifiedDate).HasColumnType("datetime");
            entity.Property(e => e.Name).HasMaxLength(100);
            entity.Property(e => e.SkillCode).HasMaxLength(20);

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.SkillCreatedByNavigations)
                .HasForeignKey(d => d.CreatedBy)
                .HasConstraintName("FK__Skill__CreatedBy__09A971A2");

            entity.HasOne(d => d.ModifiedByNavigation).WithMany(p => p.SkillModifiedByNavigations)
                .HasForeignKey(d => d.ModifiedBy)
                .HasConstraintName("FK__Skill__ModifiedB__0A9D95DB");
        });

        modelBuilder.Entity<TrainingProgram>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Training__3214EC07F6563DAC");

            entity.ToTable("TrainingProgram");

            entity.HasIndex(e => e.ProgramCode, "UQ__Training__7658A987CCDBC5FF").IsUnique();

            entity.Property(e => e.Cost).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.CreatedDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Description).HasMaxLength(255);
            entity.Property(e => e.Location).HasMaxLength(100);
            entity.Property(e => e.ModifiedDate).HasColumnType("datetime");
            entity.Property(e => e.Name).HasMaxLength(100);
            entity.Property(e => e.ProgramCode).HasMaxLength(20);
            entity.Property(e => e.Provider).HasMaxLength(100);
            entity.Property(e => e.Status).HasMaxLength(20);

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.TrainingProgramCreatedByNavigations)
                .HasForeignKey(d => d.CreatedBy)
                .HasConstraintName("FK__TrainingP__Creat__1332DBDC");

            entity.HasOne(d => d.ModifiedByNavigation).WithMany(p => p.TrainingProgramModifiedByNavigations)
                .HasForeignKey(d => d.ModifiedBy)
                .HasConstraintName("FK__TrainingP__Modif__14270015");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
