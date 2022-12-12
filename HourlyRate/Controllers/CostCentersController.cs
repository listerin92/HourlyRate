using HourlyRate.Core.Contracts;
using HourlyRate.Core.Models.CostCenter;
using HourlyRate.Core.Services;
using HourlyRate.Infrastructure.Data.Models.Account;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace HourlyRate.Controllers
{
    public class CostCentersController : Controller
    {
        private readonly UserManager<UserIdentityExt> _userManager;
        private readonly ICostCenterService _costCenterService;
        public CostCentersController(
             ICostCenterService costCenterService
            , ILogger<CostCentersController> logger
            , UserManager<UserIdentityExt> userManager
            )
        {
            _costCenterService = costCenterService;
            _userManager = userManager;
        }
        public async Task<IActionResult> Index()
        {
            if (User.Identity?.IsAuthenticated ?? false)
            {
                var companyId = CompanyId();

                await _costCenterService.UpdateAllCostCenters(companyId);

                var model = await _costCenterService.AllCostCenters(companyId);
                return View(model);
            }
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Add()
        {
            var model = new AddCostCenterViewModel()
            {
                EmployeeDepartments = await _costCenterService.AllDepartments()
            };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddCostCenterViewModel model)
        {

            if (!ModelState.IsValid)
            {
                return View(model);

            }
            var companyId = CompanyId();

            await _costCenterService.AddCostCenter(model, companyId);
            await _costCenterService.AddCostCenterToEmployee(model);

            return RedirectToAction(nameof(Index));

        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            if ((await _costCenterService.Exists(id)) == false)
            {
                return RedirectToAction(nameof(Index));
            }
            var companyId = CompanyId();
            var cc = await _costCenterService.GetCostCenterDetailsById(id, companyId);

            var model = new AddCostCenterViewModel()
            {
                Id = id,
                Name = cc.Name,
                AnnualHours = cc.AnnualHours,
                AnnualChargeableHours = cc.AnnualChargeableHours,
                AvgPowerConsumptionKwh = cc.AvgPowerConsumptionKwh,
                FloorSpace = cc.FloorSpace,
                IsUsingWater = cc.IsUsingWater,
                DepartmentId = cc.DepartmentId,
                EmployeeDepartments = await _costCenterService.AllDepartments()
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, AddCostCenterViewModel model)
        {
            if (id != model.Id)
            {
                return RedirectToPage("/Account/AccessDenied", new { area = "Identity" });

            }

            if (await _costCenterService.Exists(id) == false)
            {
                ModelState.AddModelError("", "Cost Center does not exists");
                model.EmployeeDepartments = await _costCenterService.AllDepartments();
                return View(model);
            }

            var companyId = CompanyId();

            await _costCenterService.Edit(id, model, companyId);
            await _costCenterService.UpdateAllCostCenters(companyId);

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            if ((await _costCenterService.Exists(id)) == false)
            {
                return RedirectToAction(nameof(Index));
            }

            
            var companyId = CompanyId();

            await _costCenterService.Delete(id, companyId);


            await _costCenterService.UpdateAllCostCenters(companyId);
            

            return RedirectToAction(nameof(Index));
        }

        private Guid CompanyId()
        {
            var companyId = _userManager.GetUserAsync(User).Result.CompanyId;
            return companyId;
        }
    }
}
