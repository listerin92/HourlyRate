using HourlyRate.Infrastructure.Configuration;
using HourlyRate.Infrastructure.Data.Models;
using HourlyRate.Infrastructure.Data.Models.Account;
using HourlyRate.Infrastructure.Data.Models.CostCategories;
using HourlyRate.Infrastructure.Data.Models.Costs;
using HourlyRate.Infrastructure.Data.Models.Employee;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace HourlyRate.Infrastructure.Data
{
    public class ApplicationDbContext : IdentityDbContext<UserIdentityExt>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
            
        public DbSet<Department> Departments { get; set; } = null!;
        public DbSet<Employee> Employees { get; set; } = null!;
        public DbSet<FinancialYear> FinancialYears { get; set; } = null!;
        public DbSet<Expenses> Expenses { get; set; } = null!;
        public DbSet<Consumable> Consumables { get; set; } = null!;
        public DbSet<Machine> Machines { get; set; } = null!;
        public DbSet<Company> Companies { get; set; } = null!;
        public DbSet<CostCategory> CostCategories { get; set; } = null!;


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.ApplyConfiguration(new FinancialYearConfiguration());
            modelBuilder.ApplyConfiguration(new DepartmentConfiguration());
            modelBuilder.ApplyConfiguration(new EmployeeConfiguration());
            modelBuilder.ApplyConfiguration(new SalaryConfiguration());
            modelBuilder.ApplyConfiguration(new CostCategoryConfiguration());
            modelBuilder.Entity<CostCenter>()
                .HasOne(e => e.Department)
                .WithMany()
                .OnDelete(DeleteBehavior.Restrict);

            base.OnModelCreating(modelBuilder);
        }
    }
}
