using HourlyRate.Areas.Admin.Models;
using HourlyRate.Core.Models.Employee;
using HourlyRate.Infrastructure.Data;
using HourlyRate.Infrastructure.Data.Models;
using HourlyRate.Infrastructure.Data.Models.Employee;
using Microsoft.EntityFrameworkCore;

namespace HourlyRate.Areas.Admin.Data
{
    public class SettingsService : ISettingsService
    {
        private readonly ApplicationDbContext _context;

        public SettingsService(
            ApplicationDbContext context

        )
        {
            _context = context;

        }
        public async Task<IEnumerable<FinancialYearsViewModel>> GetAllYears()
        {
            return await _context.FinancialYears
                .OrderBy(c => c.Year)
                .Select(c => new FinancialYearsViewModel()
                {
                    Id = c.Id,
                    Year = c.Year,
                    IsActive = c.IsActive,
                })
                .ToListAsync();

        }

        public FinancialYear ActiveFinancialYear()
        {
            return _context.FinancialYears
                .First(y => y.IsActive);
        }

        public async Task Edit(int id, FinancialYearsViewModel model)
        {
            var oldFinancialYear =  _context.FinancialYears.First(f => f.IsActive);
            oldFinancialYear.IsActive = false;

            var newFinancialYear = _context.FinancialYears.First(f => f.Id == id);
            newFinancialYear.IsActive = true;

            await _context.SaveChangesAsync();

        }
    }
}
