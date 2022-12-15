using HourlyRate.Core.Contracts;
using HourlyRate.Core.Models.CostCenter;
using HourlyRate.Core.Services;
using HourlyRate.Infrastructure.Data.Models.Account;
using HourlyRate.Infrastructure.Data.Models.Employee;
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
                var companyId = GetCompanyId();
                var model = await _costCenterService.AllCostCenters(companyId);
                await _costCenterService.UpdateAllCostCenters(companyId);


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
                model.EmployeeDepartments = await _costCenterService.AllDepartments();

                return View(model);

            }
            var companyId = GetCompanyId();

            try
            {
                await _costCenterService.AddCostCenter(model, companyId);
            }
            catch (ArgumentException)
            {
                ModelState.AddModelError(nameof(model.Name), "Name already exists");

                model.EmployeeDepartments = await _costCenterService.AllDepartments();

                return View(model);
            }

            await _costCenterService.AddCostCenterToEmployee(model);
            await _costCenterService.UpdateAllCostCenters(companyId);

            return RedirectToAction(nameof(Index));

        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            if ((await _costCenterService.Exists(id)) == false)
            {
                return RedirectToAction(nameof(Index));
            }
            var companyId = GetCompanyId();
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

            var companyId = GetCompanyId();

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


            var companyId = GetCompanyId();

            await _costCenterService.Delete(id, companyId);

            await _costCenterService.UpdateAllCostCenters(companyId);


            return RedirectToAction(nameof(Index));
        }

        private Guid GetCompanyId()
        {
            var companyId = _userManager.GetUserAsync(User).Result.CompanyId;
            return companyId;
        }
    }
}
