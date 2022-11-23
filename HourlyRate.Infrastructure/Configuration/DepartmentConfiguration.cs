using HourlyRate.Infrastructure.Data.Models.Employee;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HourlyRate.Infrastructure.Configuration
{
    internal class DepartmentConfiguration : IEntityTypeConfiguration<Department>
    {
        public void Configure(EntityTypeBuilder<Department> builder)
        {
            builder.HasData(CreateDepartments());
        }

        private List<Department> CreateDepartments()
        {
            var departments = new List<Department>()
            {
                new Department()
                {
                    Id = 1,
                    Name = "Prepress",
                    CompanyId = Guid.Parse("17E6E89D-C613-47D0-D580-08DAC9989BD1")
                },
                new Department()
                {
                    Id = 2,
                    Name = "Press",
                    CompanyId = Guid.Parse("17E6E89D-C613-47D0-D580-08DAC9989BD1")
                },
                new Department()
                {
                    Id = 3,
                    Name = "WebPress",
                    CompanyId = Guid.Parse("17E6E89D-C613-47D0-D580-08DAC9989BD1")
                },
                new Department()
                {
                    Id = 4,
                    Name = "ManualLabor",
                    CompanyId = Guid.Parse("17E6E89D-C613-47D0-D580-08DAC9989BD1")
                },
                new Department()
                {
                    Id = 5,
                    Name = "Cutters",
                    CompanyId = Guid.Parse("17E6E89D-C613-47D0-D580-08DAC9989BD1")
                },
                new Department()
                {
                    Id = 6,
                    Name = "Stitchers",
                    CompanyId = Guid.Parse("17E6E89D-C613-47D0-D580-08DAC9989BD1")
                },
                new Department()
                {
                    Id = 7,
                    Name = "Binders",
                    CompanyId = Guid.Parse("17E6E89D-C613-47D0-D580-08DAC9989BD1")
                },
                new Department()
                {
                    Id = 8,
                    Name = "HardCover",
                    CompanyId = Guid.Parse("17E6E89D-C613-47D0-D580-08DAC9989BD1")
                },
                new Department()
                {
                    Id = 9,
                    Name = "FrontCutter",
                    CompanyId = Guid.Parse("17E6E89D-C613-47D0-D580-08DAC9989BD1")
                },
                new Department()
                {
                    Id = 10,
                    Name = "RotaryCutter",
                    CompanyId = Guid.Parse("17E6E89D-C613-47D0-D580-08DAC9989BD1")
                }

            };

            return departments;
        }
    }
}
