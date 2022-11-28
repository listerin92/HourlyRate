using HourlyRate.Core.Contracts;
using HourlyRate.Core.Models.CostCenter;
using HourlyRate.Infrastructure.Data.Models.Account;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace HourlyRate.Controllers
{
    public class CostCentersController : Controller
    {
        private readonly ILogger<CostCentersController> _logger;
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
            _logger = logger;
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

        [HttpGet]
        public async Task<IActionResult> Add()
        {
            var model = new AddCostCenterViewModel()
            {
                EmployeeDepartments = await _costCenterService.AllDepartments()
            };
            return View(model);
        }
    }
}
