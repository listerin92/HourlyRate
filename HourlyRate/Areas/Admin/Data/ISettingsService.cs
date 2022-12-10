using System.Collections;
using HourlyRate.Areas.Admin.Models;
using HourlyRate.Infrastructure.Data.Models.Employee;

namespace HourlyRate.Areas.Admin.Data
{
    public interface ISettingsService
    {
        Task<IEnumerable<FinancialYearsViewModel>> GetAllYears();
        FinancialYear ActiveFinancialYear();
        Task Edit(int id, FinancialYearsViewModel model);
    }
}
