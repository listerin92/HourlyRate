using HourlyRate.Core.Contracts;
using HourlyRate.Infrastructure.Data.Models.Account;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace HourlyRate.Controllers
{
    public class CostCentersController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly UserManager<UserIdentityExt> _userManager;
        private readonly IEmployeeService _employeeService;
        public CostCentersController(
             IEmployeeService employeeService
            , ILogger<HomeController> logger
            , UserManager<UserIdentityExt> userManager
            )
        {
            _employeeService = employeeService;
            _userManager = userManager;
            _logger = logger;
        }
        public IActionResult Index()
        {
            if (User.Identity?.IsAuthenticated ?? false)
            {
                var companyId = _userManager.GetUserAsync(User).Result.CompanyId;

                var model = await _employeeService.AllCostCenters(companyId);
                return View(model);
            }
            else return View();
        }

        public IActionResult AddCostCenter()
        {
            return RedirectToAction(nameof(Index));
        }
    }
}
