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
                    CompanyId = Guid.Parse("4B609DF0-6F9C-4226-1BCE-08DAD4A028BB")
                },
                new Department()
                {
                    Id = 2,
                    Name = "2 CTP",
                    CompanyId = Guid.Parse("4B609DF0-6F9C-4226-1BCE-08DAD4A028BB")
                },
                new Department()
                {
                    Id = 3,
                    Name = "SM102-8P",
                    CompanyId = Guid.Parse("4B609DF0-6F9C-4226-1BCE-08DAD4A028BB")
                },
                new Department()
                {
                    Id = 4,
                    Name = "SM74-5",
                    CompanyId = Guid.Parse("4B609DF0-6F9C-4226-1BCE-08DAD4A028BB")
                },
                new Department()
                {
                    Id = 5,
                    Name = "SM74-8P",
                    CompanyId = Guid.Parse("4B609DF0-6F9C-4226-1BCE-08DAD4A028BB")
                },
                new Department()
                {
                    Id = 6,
                    Name = "GTO",
                    CompanyId = Guid.Parse("4B609DF0-6F9C-4226-1BCE-08DAD4A028BB")
                },
                new Department()
                {
                    Id = 7,
                    Name = "2 Cutters",
                    CompanyId = Guid.Parse("4B609DF0-6F9C-4226-1BCE-08DAD4A028BB")
                },
                new Department()
                {
                    Id = 8,
                    Name = "5 Folders",
                    CompanyId = Guid.Parse("4B609DF0-6F9C-4226-1BCE-08DAD4A028BB")
                },
                new Department()
                {
                    Id = 9,
                    Name = "Lamination",
                    CompanyId = Guid.Parse("4B609DF0-6F9C-4226-1BCE-08DAD4A028BB")
                },
                new Department()
                {
                    Id = 10,
                    Name = "Binding",
                    CompanyId = Guid.Parse("4B609DF0-6F9C-4226-1BCE-08DAD4A028BB")
                },
                new Department()
                {
                    Id = 11,
                    Name = "Stitcher",
                    CompanyId = Guid.Parse("4B609DF0-6F9C-4226-1BCE-08DAD4A028BB")
                },
                new Department()
                {
                    Id = 12,
                    Name = "Sewing",
                    CompanyId = Guid.Parse("4B609DF0-6F9C-4226-1BCE-08DAD4A028BB")
                },
                new Department()
                {
                    Id = 13,
                    Name = "EndPaper",
                    CompanyId = Guid.Parse("4B609DF0-6F9C-4226-1BCE-08DAD4A028BB")
                },
                new Department()
                {
                    Id = 14,
                    Name = "CaseMaker",
                    CompanyId = Guid.Parse("4B609DF0-6F9C-4226-1BCE-08DAD4A028BB")
                },
                new Department()
                {
                    Id = 15,
                    Name = "CasingIN",
                    CompanyId = Guid.Parse("4B609DF0-6F9C-4226-1BCE-08DAD4A028BB")
                },
                new Department()
                {
                    Id = 16,
                    Name = "FrontKnife",
                    CompanyId = Guid.Parse("4B609DF0-6F9C-4226-1BCE-08DAD4A028BB")
                },
                new Department()
                {
                    Id = 17,
                    Name = "Rotary Cutting",
                    CompanyId = Guid.Parse("4B609DF0-6F9C-4226-1BCE-08DAD4A028BB")
                },
                new Department()
                {
                    Id = 18,
                    Name = "Creaser",
                    CompanyId = Guid.Parse("4B609DF0-6F9C-4226-1BCE-08DAD4A028BB")
                },
                new Department()
                {
                    Id = 19,
                    Name = "Manual/Assistants/Packing",
                    CompanyId = Guid.Parse("4B609DF0-6F9C-4226-1BCE-08DAD4A028BB")
                },
                new Department()
                {
                    Id = 20,
                    Name = "General",
                    CompanyId = Guid.Parse("4B609DF0-6F9C-4226-1BCE-08DAD4A028BB")
                },
                new Department()
                {
                    Id = 21,
                    Name = "CTP Web",
                    CompanyId = Guid.Parse("4B609DF0-6F9C-4226-1BCE-08DAD4A028BB")
                },
                new Department()
                {
                    Id = 22,
                    Name = "Man Uniman",
                    CompanyId = Guid.Parse("4B609DF0-6F9C-4226-1BCE-08DAD4A028BB")
                }

            };

            return departments;
        }
    }
}
