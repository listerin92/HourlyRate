using HourlyRate.Core.Contracts;
using HourlyRate.Core.Models.CostCenter;
using HourlyRate.Infrastructure.Data.Common;
using HourlyRate.Infrastructure.Data.Models;
using HourlyRate.Infrastructure.Data.Models.Employee;
using Microsoft.EntityFrameworkCore;

namespace HourlyRate.Core.Services
{
    public class CostCenterService : ICostCenterService
    {
        private readonly IRepository _repo;

        public CostCenterService(
            IRepository repo
        )
        {
            _repo = repo;
        }

        /// <summary>
        /// Same for all services, add it to a general service
        /// </summary>
        /// <returns></returns>
        private int ActiveFinancialYear()
        {
            return _repo.AllReadonly<FinancialYear>()
                .First(y => y.IsActive).Year;
        }
        public async Task<IEnumerable<CostCenterViewModel>> AllCostCenters(Guid companyId)
        {
            var currentYear = ActiveFinancialYear();

            return await _repo.AllReadonly<Expenses>()
                .Where(c => c.FinancialYear.Year == currentYear &&
                            c.CompanyId == companyId)
                .Select(c => new CostCenterViewModel()
                {
                    Id = c.Id,
                    
                }).ToListAsync();


        }
    }
}
