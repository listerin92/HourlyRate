using HourlyRate.Infrastructure.Data.Models.CostCategories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HourlyRate.Infrastructure.Configuration
{
    internal class CostCategoryConfiguration : IEntityTypeConfiguration<CostCategory>
    {
        public void Configure(EntityTypeBuilder<CostCategory> builder)
        {
            builder.HasData(CreateCostCategory());
        }

        private List<CostCategory> CreateCostCategory()
        {
            var costCategories = new List<CostCategory>()
            {
                new CostCategory()
                {
                    Id = 1,
                    CompanyId = Guid.Parse("4B609DF0-6F9C-4226-1BCE-08DAD4A028BB"),
                    Name = "Water",
                },
                new CostCategory()
                {
                    Id = 2,
                    CompanyId = Guid.Parse("4B609DF0-6F9C-4226-1BCE-08DAD4A028BB"),
                    Name = "Power",
                },
                new CostCategory()
                {   
                    Id = 3,
                    CompanyId = Guid.Parse("4B609DF0-6F9C-4226-1BCE-08DAD4A028BB"),
                    Name = "Phones",
                },
                new CostCategory()
                {
                    Id = 4,
                    CompanyId = Guid.Parse("4B609DF0-6F9C-4226-1BCE-08DAD4A028BB"),
                    Name = "Other",
                },
                new CostCategory()
                {
                    Id = 5,
                    CompanyId = Guid.Parse("4B609DF0-6F9C-4226-1BCE-08DAD4A028BB"),
                    Name = "Administration",
                },
                new CostCategory()
                {
                    Id = 6,
                    CompanyId = Guid.Parse("4B609DF0-6F9C-4226-1BCE-08DAD4A028BB"),
                    Name = "Rent",
                },
                new CostCategory()
                {
                    Id = 7,
                    CompanyId = Guid.Parse("4B609DF0-6F9C-4226-1BCE-08DAD4A028BB"),
                    Name = "Direct Repairs",
                },
                new CostCategory()
                {
                    Id = 8,
                    CompanyId = Guid.Parse("4B609DF0-6F9C-4226-1BCE-08DAD4A028BB"),
                    Name = "Direct Depreciation",
                }
            };
            return costCategories;
        }
    }
}
