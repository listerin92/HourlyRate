using HourlyRate.Core.Contracts;
using HourlyRate.Core.Models.Employee;
using HourlyRate.Core.Models.GeneralCost;
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
        public async Task<IEnumerable<GeneralCostTypeViewModel>> AllCostCategoryTypes()
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

        public async Task<bool> Exists(int id)
        {
            return await _repo.AllReadonly<Expenses>()
                .AnyAsync(h => h.Id == id);
        }

        public async Task<IEnumerable<GeneralCostCenterViewModel>> AllCostCentersTypes()
        {
            var activeFinancialYearId = ActiveFinancialYearId();


            return await _repo.AllReadonly<CostCenter>()
                .Where(c => c.IsActive == true
                         && c.FinancialYearId == activeFinancialYearId || c.FinancialYearId == 20)
                
                .OrderBy(c => c.Name)
                .Select(c => new GeneralCostCenterViewModel()
                {
                    Id = c.Id,
                    Name = c.Name
                })
                .ToListAsync();
        }

        public async Task<IEnumerable<CostViewModel>> AllGeneralCost(Guid companyId)
        {
            var currentYear = ActiveFinancialYear();

            var allGeneralCost = await _repo.AllReadonly<Expenses>()
               .Where(y => y.FinancialYear.Year == currentYear && y.CompanyId == companyId && y.CostCategoryId != null && y.IsDeleted == false)
               .Select(c => new CostViewModel()
               {
                   Id = c.Id,
                   Amount = c.Amount,
                   CostCategoryId = c.CostCategoryId,
                   Description = c.Description!,
                   DefaultCurrency = c.Company.DefaultCurrency,
                   CostCategoryName = c.CostCategories!.Name ?? "None",
                   CostCenterName = c.CostCenter!.Name ?? "None",
               })
               .ToListAsync();

            return allGeneralCost;
        }

        public async Task<bool> CostCategoryTypeExist(int? costCategoryId)
        {
            return await _repo.AllReadonly<CostCategory>()
                .AnyAsync(c => c.Id == costCategoryId);

        }

        public async Task<int> CreateCost(AddCostViewModel model, Guid companyId)
        {
            var activeFinancialYearId = ActiveFinancialYearId();
            var cost = new Expenses()
            {
                CompanyId = companyId,
                Amount = model.Amount,
                CostCategoryId = model.CostCategoryId,
                CostCenterId = model.CostCenterId,
                Description = model.Description,
                FinancialYearId = activeFinancialYearId
            };

            await _repo.AddAsync(cost);
            await _repo.SaveChangesAsync();

            return cost.Id;

        }

        public async Task Edit(AddCostViewModel model, Guid companyId)
        {
            var activeFinancialYearId = ActiveFinancialYearId();
            var generalCost = await _repo.GetByIdAsync<Expenses>(model.Id);

            generalCost.Description = model.Description;
            generalCost.Amount = model.Amount;
            generalCost.CostCategoryId = model.CostCategoryId;
            generalCost.CostCenterId = model.CostCenterId;
            generalCost.CompanyId = companyId;
            generalCost.FinancialYearId = activeFinancialYearId;

            await _repo.SaveChangesAsync();
        }

        public async Task<int> CreateCostCategory(AddCostCategoryViewModel model, Guid companyId)
        {
            var checkExist = _repo.AllReadonly<CostCategory>()
                .FirstOrDefault(c => c.Name == model.Description)
                ?.Name;
            if (checkExist != null)
            {
                return -1;
            }

            var costCategory = new CostCategory()
            {
                Name = model.Description,
                CompanyId = companyId
            };

            await _repo.AddAsync(costCategory);
            await _repo.SaveChangesAsync();
            return costCategory.Id;
        }
        public async Task<AddCostViewModel> GeneralCostDetailsById(int id, Guid companyId)
        {
            return await _repo.AllReadonly<Expenses>()
                .Where(e => e.CompanyId == companyId && e.Id == id)
                .Select(e => new AddCostViewModel()
                {
                    Id = id,
                    Description = e.Description!,
                    CostCategoryId = e.CostCategoryId,
                    CostCenterId = e.CostCenterId,
                    Amount = e.Amount,
                })
                .FirstAsync();
        }

        public async Task Delete(int generalCostId)
        {
            var generalCost = await _repo.GetByIdAsync<Expenses>(generalCostId);
            generalCost.IsDeleted = true;
            await _repo.SaveChangesAsync();
        }

        private int ActiveFinancialYear()
        {
            return _repo.AllReadonly<FinancialYear>()
                .First(y => y.IsActive).Year;
        }

        private int ActiveFinancialYearId()
        {
            return _repo.AllReadonly<FinancialYear>()
                .First(y => y.IsActive).Id;
        }

        //TODO: Not Implemented Consumables
    }

}
