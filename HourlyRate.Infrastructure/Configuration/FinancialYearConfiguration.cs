using HourlyRate.Infrastructure.Data.Models.Employee;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HourlyRate.Infrastructure.Configuration
{
    internal class FinancialYearConfiguration : IEntityTypeConfiguration<FinancialYear>
    {
        public void Configure(EntityTypeBuilder<FinancialYear> builder)
        {
            builder.HasData(CreateYears());

        }

        private List<FinancialYear> CreateYears()
        {
            int yearItr = 2015;
            var years = new List<FinancialYear>();
            for (int id = 1; id < 20; id++, yearItr++)
            {
                var year = new FinancialYear()
                {
                    Id = id,
                    Year = yearItr,
                };
                years.Add(year);
            }

            return years;   
        }
    }
}
