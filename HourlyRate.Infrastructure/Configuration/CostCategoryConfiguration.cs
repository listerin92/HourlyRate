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
                    CompanyId = Guid.Parse("457FC37B-B204-4019-9E5D-08DACF799BB2"),
                    Name = "FloorSpace m2",
                },
                new CostCategory()
                {
                    Id = 2,
                    CompanyId = Guid.Parse("457FC37B-B204-4019-9E5D-08DACF799BB2"),
                    Name = "Power Consumption kWh",
                },
                new CostCategory()
                {   
                    Id = 3,
                    CompanyId = Guid.Parse("457FC37B-B204-4019-9E5D-08DACF799BB2"),
                    Name = "Heating",
                },
                new CostCategory()
                {
                    Id = 4,
                    CompanyId = Guid.Parse("457FC37B-B204-4019-9E5D-08DACF799BB2"),
                    Name = "Cooling",
                },
                new CostCategory()
                {
                    Id = 5,
                    CompanyId = Guid.Parse("457FC37B-B204-4019-9E5D-08DACF799BB2"),
                    Name = "General Taxes",
                },
                new CostCategory()
                {
                    Id = 6,
                    CompanyId = Guid.Parse("457FC37B-B204-4019-9E5D-08DACF799BB2"),
                    Name = "Direct Repairs",
                },
                new CostCategory()
                {
                    Id = 7,
                    CompanyId = Guid.Parse("457FC37B-B204-4019-9E5D-08DACF799BB2"),
                    Name = "Available hours",
                },
                new CostCategory()
                {
                    Id = 8,
                    CompanyId = Guid.Parse("457FC37B-B204-4019-9E5D-08DACF799BB2"),
                    Name = "Salable hours",
                }
            };
            return costCategories;
        }
    }
}
