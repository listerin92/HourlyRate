using HourlyRate.Core.Contracts;
using HourlyRate.Core.Models.GeneralCost;
using HourlyRate.Core.Services;
using HourlyRate.Infrastructure.Data.Models.Account;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;

namespace HourlyRate.Controllers
{
    public class GeneralController : Controller
    {

        private readonly IGeneralCostService _generalCostService;
        private readonly UserManager<UserIdentityExt> _userManager;

        public GeneralController(
            IGeneralCostService generalCostService
        , UserManager<UserIdentityExt> userManager
        )

        {
            _userManager = userManager;
            _generalCostService = generalCostService;
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
            var model = new CostViewModel()
            {
                GeneralCostType = await _generalCostService.AllCostTypes()
            };

            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Add(CostViewModel cost)
        {
            if ((await _generalCostService.CostCategoryTypeExist(cost.CostCategoryId)) == false)
            {
                ModelState.AddModelError(nameof(cost.CostCategoryId), "Category does not exists");
            }

            if (!ModelState.IsValid)
            {
                cost.GeneralCostType = await _generalCostService.AllCostTypes();

                return View(cost);
            }
            var companyId = GetCompanyId();

            await _generalCostService.CreateCost(cost, companyId);

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> AddCostCategory()
        {
            var model = new CostCategoryViewModel()
            {
                GeneralCostType = await _generalCostService.AllCostTypes()
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> AddCostCategory(CostCategoryViewModel model)
        {
            if ((await _generalCostService.CostCategoryTypeExist(model.CostCategoryId)) == false)
            {
                ModelState.AddModelError(nameof(model.CostCategoryId), "Category does not exists");
            }
            if (!ModelState.IsValid)
            {
                model.GeneralCostType = await _generalCostService.AllCostTypes();

                return View(model);
            }
            var companyId = GetCompanyId();

            var result = await _generalCostService.CreateCostCategory(model, companyId);
            
            if (result != -1) return RedirectToAction(nameof(Index));
            
            ModelState.AddModelError("", "CategoryAlreadyExist");
            return View(model);


        }

        private Guid GetCompanyId()
        {
            var companyId = _userManager.GetUserAsync(User).Result.CompanyId;
            return companyId;
        }
    }
}
