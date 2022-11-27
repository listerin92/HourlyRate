using HourlyRate.Core.Contracts;
using HourlyRate.Core.Models;
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
                var companyId = _userManager.GetUserAsync(User).Result.CompanyId;

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
            var user = _userManager.GetUserAsync(User);

            int employeeId = await _generalCostService.CreateCost(cost, user.Result.CompanyId);

            return RedirectToAction(nameof(Index), new { id = employeeId });
        }
    }
}
