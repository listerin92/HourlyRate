using HourlyRate.Core.Contracts;
using HourlyRate.Infrastructure.Data.Models.Account;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace HourlyRate.Controllers;

public class HomeController : Controller
{
    private readonly UserManager<UserIdentityExt> _userManager;
    private readonly ICostCenterService _costCenterService;

    public HomeController(
        UserManager<UserIdentityExt> userManager
        , ICostCenterService costCenterService
    )

    {
        _userManager = userManager;
        _costCenterService = costCenterService;
    }

    public async Task<IActionResult> Index()
    {
        if (User.Identity?.IsAuthenticated ?? false)
        {
            var companyId = _userManager.GetUserAsync(User).Result.CompanyId;

            var model = await _costCenterService.AllCostCenters(companyId);
            return View(model);
        }
        return View();
    }

    public async Task<IActionResult> Update()
    {
        if (User.Identity?.IsAuthenticated ?? false)
        {
            var companyId = _userManager.GetUserAsync(User).Result.CompanyId;

            await _costCenterService.UpdateAllCostCenters(companyId);

            return RedirectToAction(nameof(Index));
        }
        return RedirectToAction(nameof(Index));

    }

    [Route("/Home/HandleError/{code:int}")]
    public IActionResult HandleError(int code)
    {
        ViewData["ErrorMessage"] = $"{code}";
        return View("~/Views/Shared/ErrorPage.cshtml");
    }
}

