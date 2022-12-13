using HourlyRate.Core.Models.GeneralCost;

namespace HourlyRate.Core.Contracts
{
    public interface IGeneralCostService
    {
        Task<bool> Exists(int id);
        Task<IEnumerable<GeneralCostTypeViewModel>> AllCostCategoryTypes();
        Task<IEnumerable<GeneralCostCenterViewModel>> AllCostCentersTypes();

        Task<IEnumerable<CostViewModel>> AllGeneralCost(Guid companyId);
        Task<bool> CostCategoryTypeExist(int? departmentId);
        Task<int> CreateCost(AddCostViewModel model, Guid companyId);
        Task<int> CreateCostCategory(AddCostCategoryViewModel model, Guid companyId);
        Task<AddCostViewModel> GeneralCostDetailsById(int id, Guid companyId);
        Task Edit(AddCostViewModel model, Guid companyId);
        Task Delete(int generalCostId);


    }
}
