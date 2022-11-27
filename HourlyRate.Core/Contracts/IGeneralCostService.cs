using HourlyRate.Core.Models;

namespace HourlyRate.Core.Contracts
{
    public interface IGeneralCostService
    {
        Task<IEnumerable<GeneralCostTypeViewModel>> AllCostTypes();

        Task<IEnumerable<CostViewModel>> AllGeneralCost(Guid companyId);
        Task<bool> CostCategoryTypeExist(int? departmentId);
        Task<int> CreateCost(CostViewModel model, Guid companyId);
        Task<int> CreateCostCategory(CostCategoryViewModel model, Guid companyId);

    }
}
