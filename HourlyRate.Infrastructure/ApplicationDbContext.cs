using HourlyRate.Core.Models.Account;
using HourlyRate.Core.Models.Employee;
using HourlyRate.Infrastructure.Configuration;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace HourlyRate.Infrastructure
{
    public class ApplicationDbContext : IdentityDbContext<User>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Department> Departments { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<FinancialYear> FinancialYears { get; set; }
        public DbSet<Salary> Salaries { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new DepartmentConfiguration());
            modelBuilder.ApplyConfiguration(new FinancialYearConfiguration());

            base.OnModelCreating(modelBuilder);
        }
    }
}
