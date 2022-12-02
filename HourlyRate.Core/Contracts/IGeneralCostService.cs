using HourlyRate.Core.Models.GeneralCost;

namespace HourlyRate.Core.Contracts
{
    public interface IGeneralCostService
    {
        Task<IEnumerable<GeneralCostTypeViewModel>> AllCostTypes();
        Task<IEnumerable<GeneralCostCenterViewModel>> AllCostCenters();

        Task<IEnumerable<CostViewModel>> AllGeneralCost(Guid companyId);
        Task<bool> CostCategoryTypeExist(int? departmentId);
        Task<int> CreateCost(AddCostViewModel model, Guid companyId);
        Task<int> CreateCostCategory(AddCostCategoryViewModel model, Guid companyId);


    }
}
