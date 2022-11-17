using HourlyRate.Infrastructure.Models.Employee;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HourlyRate.Infrastructure.Configuration
{
    internal class EmployeeConfiguration : IEntityTypeConfiguration<Employee>
    {
        public void Configure(EntityTypeBuilder<Employee> builder)
        {
            builder.HasData(CreateEmployees());
        }

        private List<Employee> CreateEmployees()
        {
            var employees = new List<Employee>()
            {
                new Employee()
                {
                    Id = 1,
                    FirstName = "Ivan",
                    LastName = "Ivanov",
                    JobTitle = "asdf",
                    DepartmentId = 1,
                    IsEmployee = true,
                },
                new Employee()
                {
                    Id = 2,
                    FirstName = "Petar",
                    LastName = "Petrov",
                    JobTitle = "asdf",
                    DepartmentId = 2,
                    IsEmployee = true,
                },
                new Employee()
                {
                    Id = 3,
                    FirstName = "Stefan",
                    LastName = "Todorov",
                    JobTitle = "bbb",
                    DepartmentId = 1,
                    IsEmployee = true,
                },
                new Employee()
                {
                    Id = 4,
                    FirstName = "Georgi",
                    LastName = "Antonov",
                    JobTitle = "asdf",
                    DepartmentId = 1,
                    IsEmployee = true,
                },
            };

            return employees;
        }
    }
}
