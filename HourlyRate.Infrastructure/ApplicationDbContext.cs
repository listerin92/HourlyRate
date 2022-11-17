using HourlyRate.Infrastructure.Configuration;
using HourlyRate.Infrastructure.Models;
using HourlyRate.Infrastructure.Models.Account;
using HourlyRate.Infrastructure.Models.Costs;
using HourlyRate.Infrastructure.Models.Employee;
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
        public DbSet<Expenses> Expenses { get; set; }
        public DbSet<Rent> Rents { get; set; }
        public DbSet<Tax> Taxes { get; set; }
        public DbSet<Consumable> Consumables { get; set; }
        public DbSet<Machine> Machines { get; set; }
        public DbSet<Company> Companies { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.ApplyConfiguration(new DepartmentConfiguration());
            modelBuilder.ApplyConfiguration(new FinancialYearConfiguration());
            modelBuilder.ApplyConfiguration(new EmployeeConfiguration());
            modelBuilder.ApplyConfiguration(new SalaryConfiguration());

            base.OnModelCreating(modelBuilder);
        }
    }
}
