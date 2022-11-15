using HourlyRate.Infrastructure.Models.Employee;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HourlyRate.Infrastructure.Configuration
{
    internal class SalaryConfiguration : IEntityTypeConfiguration<Salary>
    {
        public void Configure(EntityTypeBuilder<Salary> builder)
        {
            builder.HasData(CreateSalary());
        }

        private List<Salary> CreateSalary()
        {
            var salary = new List<Salary>()
            {
                new Salary()
                {
                    Id = 1,
                    EmployeeId = 1,
                    Amount = 5000,
                    FinancialYearId = 8

                },
                new Salary()
                {
                    Id = 2,
                    EmployeeId = 2,
                    Amount = 2322,
                    FinancialYearId = 8

                },
                new Salary()
                {
                    Id = 3,
                    EmployeeId = 3,
                    Amount = 1211,
                    FinancialYearId = 8

                },
                new Salary()
                {
                    Id = 4,
                    EmployeeId = 4,
                    Amount = 855,
                    FinancialYearId = 8

                },
            };

            return salary;
        }
    }
}
