using HourlyRate.Infrastructure.Data.Models.Employee;
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
                    ImageUrl = "https://www.loudegg.com/wp-content/uploads/2020/10/Mickey-Mouse.jpg",
                    DepartmentId = 1,
                    IsEmployee = true,
                    CompanyId = Guid.Parse("457FC37B-B204-4019-9E5D-08DACF799BB2") //TODO: Change to your first Company ID if you start from scratch
                },
                new Employee()
                {
                    Id = 2,
                    FirstName = "Petar",
                    LastName = "Petrov",
                    JobTitle = "asdf",
                    ImageUrl = "https://www.loudegg.com/wp-content/uploads/2020/10/Bugs-Bunny.jpg",
                    DepartmentId = 2,
                    IsEmployee = true,
                    CompanyId = Guid.Parse("457FC37B-B204-4019-9E5D-08DACF799BB2")
                },
                new Employee()
                {
                    Id = 3,
                    FirstName = "Stefan",
                    LastName = "Todorov",
                    JobTitle = "bbb",
                    ImageUrl = "https://www.loudegg.com/wp-content/uploads/2020/10/Fred-Flintstone.jpg",
                    DepartmentId = 3,
                    IsEmployee = true,
                    CompanyId = Guid.Parse("457FC37B-B204-4019-9E5D-08DACF799BB2")
                },
                new Employee()
                {
                    Id = 4,
                    FirstName = "Georgi",
                    LastName = "Antonov",
                    JobTitle = "asdf",
                    ImageUrl = "https://www.loudegg.com/wp-content/uploads/2020/10/SpongeBob-SqaurePants.jpg",
                    DepartmentId = 4,
                    IsEmployee = true,
                    CompanyId = Guid.Parse("457FC37B-B204-4019-9E5D-08DACF799BB2")
                },
            };

            return employees;
        }
    }
}
