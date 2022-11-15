using HourlyRate.Core.Models.Employee;
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
                    Name = "Prepress"
                },
                new Department()
                {
                    Id = 2,
                    Name = "Press"
                },
                new Department()
                {
                    Id = 3,
                    Name = "WebPress"
                },
                new Department()
                {
                    Id = 4,
                    Name = "ManualLabor"
                },
                new Department()
                {
                    Id = 5,
                    Name = "Cutters"
                },
                new Department()
                {
                    Id = 6,
                    Name = "Stitchers"
                },
                new Department()
                {
                    Id = 7,
                    Name = "Binders"
                },
                new Department()
                {
                    Id = 8,
                    Name = "HardCover"
                },
                new Department()
                {
                    Id = 9,
                    Name = "FrontCutter"
                },
                new Department()
                {
                    Id = 10,
                    Name = "RotaryCutter"
                }

            };

            return departments;
        }
    }
}
