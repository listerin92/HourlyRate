using HourlyRate.Core.Contracts;
using HourlyRate.Core.Models.Employee;
using HourlyRate.Core.Models.GeneralCost;
using HourlyRate.Core.Services;
using HourlyRate.Infrastructure.Data.Models.Account;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace HourlyRate.Controllers
{
    public class GeneralController : Controller
    {

        private readonly IGeneralCostService _generalCostService;
        private readonly UserManager<UserIdentityExt> _userManager;
        private readonly ICostCenterService _costCenterService;


        public GeneralController(
            IGeneralCostService generalCostService
        , UserManager<UserIdentityExt> userManager
            , ICostCenterService costCenterService

        )

        {
            _userManager = userManager;
            _generalCostService = generalCostService;
            _costCenterService = costCenterService;

        }
        public async Task<IActionResult> Index()
        {
            if (User.Identity?.IsAuthenticated ?? false)
            {
                var companyId = GetCompanyId();

                var model = await _generalCostService.AllGeneralCost(companyId);
                return View(model);
            }
            else return View();
        }


        [HttpGet]
        public async Task<IActionResult> Add()
        {

            var model = new AddCostViewModel()
            {
                GeneralCostType = await _generalCostService.AllCostCategoryTypes(),
                GeneralCostCenter = await _generalCostService.AllCostCentersTypes()
            };

            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Add(AddCostViewModel cost)
        {
            if ((await _generalCostService.CostCategoryTypeExist(cost.CostCategoryId)) == false)
            {
                ModelState.AddModelError(nameof(cost.CostCategoryId), "Category does not exists");
            }
            var companyDefaultCurrency = _userManager.GetUserAsync(User).Result.DefaultCurrency;

            if (!ModelState.IsValid)
            {
                cost.GeneralCostType = await _generalCostService.AllCostCategoryTypes();

                return View(cost);
            }
            var companyId = GetCompanyId();

            await _generalCostService.CreateCost(cost, companyId);

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult AddCostCategory()
        {
            var model = new AddCostCategoryViewModel();

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> AddCostCategory(AddCostCategoryViewModel model)
        {

            if (!ModelState.IsValid)
            {
                ModelState.AddModelError(nameof(model.Description), "Add Category Name");

                return View(model);
            }
            var companyId = GetCompanyId();

            var result = await _generalCostService.CreateCostCategory(model, companyId);

            if (result != -1) return RedirectToAction(nameof(Index));

            ModelState.AddModelError("", "CategoryAlreadyExist");
            return View(model);


        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            if (await _generalCostService.Exists(id) == false)
            {
                return RedirectToAction(nameof(Index));

            }

            var companyId = GetCompanyId();
            var generalCost = await _generalCostService.GeneralCostDetailsById(id, companyId);
            var model = new AddCostViewModel()
            {
                Id = id,
                Description = generalCost.Description,
                Amount = generalCost.Amount,
                CostCategoryId = generalCost.CostCategoryId,
                CostCenterId = generalCost.CostCenterId,
                GeneralCostType = await _generalCostService.AllCostCategoryTypes(),
                GeneralCostCenter = await _generalCostService.AllCostCentersTypes()
            };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, AddCostViewModel model)
        {
            if (id != model.Id)
            {
                return RedirectToPage("/Account/AccessDenied", new { area = "Identity" });
            }
            if ((await _generalCostService.Exists(model.Id)) == false)
            {
                ModelState.AddModelError("", "Employee does not exist");
                model.GeneralCostType = await _generalCostService.AllCostCategoryTypes();
                model.GeneralCostCenter = await _generalCostService.AllCostCentersTypes();

                return View(model);
            }

            if (ModelState.IsValid == false)
            {
                model.GeneralCostType = await _generalCostService.AllCostCategoryTypes();
                model.GeneralCostCenter = await _generalCostService.AllCostCentersTypes();
                return View(model);
            }

            var companyId = GetCompanyId();
            await _generalCostService.Edit(model, companyId);

            await _costCenterService.UpdateAllCostCenters(companyId);
            return RedirectToAction(nameof(Index), new { model.Id });
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id, AddCostViewModel model)
        {
            if ((await _generalCostService.Exists(id)) == false)
            {
                return RedirectToAction(nameof(Index));
            }


            await _generalCostService.Delete(id);
            var companyId = GetCompanyId();

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
