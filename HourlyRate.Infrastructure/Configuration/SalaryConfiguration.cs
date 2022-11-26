using HourlyRate.Infrastructure.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HourlyRate.Infrastructure.Configuration
{
    internal class SalaryConfiguration : IEntityTypeConfiguration<Expenses>
    {
        public void Configure(EntityTypeBuilder<Expenses> builder)
        {
            builder.HasData(CreateSalary());
        }

        private List<Expenses> CreateSalary()
        {
            var salary = new List<Expenses>()
            {
                new Expenses()
                {
                    Id = 1,
                    EmployeeId = 1,
                    Amount = 5000,
                    FinancialYearId = 8,
                    CompanyId = Guid.Parse("457FC37B-B204-4019-9E5D-08DACF799BB2")

                },
                new Expenses()
                {
                    Id = 2,
                    EmployeeId = 2,
                    Amount = 2322,
                    FinancialYearId = 8,
                    CompanyId = Guid.Parse("457FC37B-B204-4019-9E5D-08DACF799BB2")

                },
                new Expenses()
                {
                    Id = 3,
                    EmployeeId = 3,
                    Amount = 1211,
                    FinancialYearId = 8,
                    CompanyId = Guid.Parse("457FC37B-B204-4019-9E5D-08DACF799BB2")

                },
                new Expenses()
                {
                    Id = 4,
                    EmployeeId = 4,
                    Amount = 855,
                    FinancialYearId = 8,
                    CompanyId = Guid.Parse("457FC37B-B204-4019-9E5D-08DACF799BB2")

                },
            };

            return salary;
        }
    }
}
