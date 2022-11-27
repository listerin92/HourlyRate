using HourlyRate.Core.Contracts;
using HourlyRate.Core.Models;
using HourlyRate.Infrastructure.Data.Common;
using HourlyRate.Infrastructure.Data.Models;
using HourlyRate.Infrastructure.Data.Models.CostCategories;
using HourlyRate.Infrastructure.Data.Models.Employee;
using Microsoft.EntityFrameworkCore;

namespace HourlyRate.Core.Services
{
    public class GeneralCostService : IGeneralCostService
    {
        private readonly IRepository _repo;

        public GeneralCostService(IRepository repo)
        {
            _repo = repo;
        }
        public async Task<IEnumerable<GeneralCostTypeViewModel>> AllCostTypes()
        {

            return await _repo.AllReadonly<CostCategory>()
                .OrderBy(c => c.Name)
                .Select(c => new GeneralCostTypeViewModel()
                {
                    Id = c.Id,
                    Name = c.Name
                })
                .ToListAsync();
        }

        public async Task<IEnumerable<CostViewModel>> AllGeneralCost(Guid companyId)
        {
            var all = await _repo.AllReadonly<Expenses>()
               .Where(y => y.FinancialYear.Year == 2022 && y.CompanyId == companyId && y.CostCategoryId != null)
               .Select(c => new CostViewModel()
               {
                   Id = c.Id,
                   Amount = c.Amount,
                   CostCategoryId = c.CostCategoryId,
                   Description = c.CostCenter!.Name

               })
               .ToListAsync();

            return all;
        }

        public async Task<bool> CostCategoryTypeExist(int? costcategoryId)
        {
            return await _repo.AllReadonly<CostCategory>()
                .AnyAsync(c => c.Id == costcategoryId);

        }

        public async Task<int> CreateCost(CostViewModel model, Guid companyId)
        {
            var cost = new Expenses()
            {
                CompanyId = companyId,
                Amount = model.Amount,
                CostCategoryId = model.CostCategoryId,
                FinancialYearId = 8
            };

            await _repo.AddAsync(cost);
            await _repo.SaveChangesAsync();

            return cost.Id;
        }
    }
}
