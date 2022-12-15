using HourlyRate.Areas.Admin.Data;
using HourlyRate.Areas.Admin.Models;
using HourlyRate.Infrastructure.Data.Models.Account;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace HourlyRate.Areas.Admin.Controllers
{
    [Area(nameof(Admin))]
    public class SettingsController : Controller
    {
        private readonly ISettingsService _settingsService;
        private readonly UserManager<UserIdentityExt> _userManager;


        public SettingsController(
            ISettingsService settingsService
            , UserManager<UserIdentityExt> userManager

            )
        {
            _settingsService = settingsService;
            _userManager = userManager;

        }
        public async Task<IActionResult> Index()
        {

            var isActiveYear = _settingsService.ActiveFinancialYear();

            var model = new FinancialYearsViewModel()
            {
                Id = isActiveYear.Id,
                FinancialYears = await _settingsService.GetAllYears()
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, FinancialYearsViewModel model)
        {
           await _settingsService.Edit(model.Id, model);

            return RedirectToAction(nameof(Index), "Home", new { area = "" });
        }
    }
}
