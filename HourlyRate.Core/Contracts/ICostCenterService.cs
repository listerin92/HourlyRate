using HourlyRate.Core.Models.CostCenter;
using HourlyRate.Core.Models.Employee;

namespace HourlyRate.Core.Contracts
{
    public interface ICostCenterService
    {
        Task<IEnumerable<CostCenterViewModel>> AllCostCenters(Guid companyId);

    }
}
