using System.Diagnostics;
using System.Security.Claims;
using HourlyRate.Core.Contracts;
using HourlyRate.Core.Models;
using HourlyRate.Extensions;
using HourlyRate.Infrastructure.Data.Models;
using HourlyRate.Infrastructure.Data.Models.Account;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace HourlyRate.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly UserManager<UserIdentityExt> _userManager;
    private readonly IEmployeeService _employeeService;

    public HomeController(
        IEmployeeService employeeService,
        ILogger<HomeController> logger,
        UserManager<UserIdentityExt> userManager)

    {
        _employeeService = employeeService;
        _userManager = userManager;
        _logger = logger;
    }

    public async Task<IActionResult> Index()
    {
        if (User.Identity?.IsAuthenticated ?? false)
        {
            var model = await _employeeService.AllEmployees();
            return View(model);
        }
        else return View();
    }

    [HttpGet]
    public async Task<IActionResult> Add()
    {

        var model = new EmployeeViewModel()
        {
            EmployeeDepartments = await _employeeService.AllDepartments()
        };

        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> Add(EmployeeViewModel employee)
    {
        if ((await _employeeService.DepartmentExists(employee.DepartmentId)) == false)
        {
            ModelState.AddModelError(nameof(employee.DepartmentId), "Category does not exists");
        }

        if (!ModelState.IsValid)
        {
            employee.EmployeeDepartments = await _employeeService.AllDepartments();

            return View(employee);
        }
        var user = _userManager.GetUserAsync(User);
        
        int employeeId = await _employeeService.CreateEmployee(employee, user.Result.CompanyId);
        await _employeeService.CreateExpensesByEmployee(employeeId, employee, user.Result.CompanyId);

        return RedirectToAction(nameof(Index), new { id = employeeId });
    }



    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}

