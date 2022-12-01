using HourlyRate.Core.Contracts;
using HourlyRate.Core.Models;
using HourlyRate.Core.Models.Employee;
using HourlyRate.Core.Services;
using HourlyRate.Infrastructure.Data.Models.Account;
using HourlyRate.Infrastructure.Data.Models.Employee;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace HourlyRate.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly UserManager<UserIdentityExt> _userManager;
    private readonly IEmployeeService _employeeService;

    public HomeController(
        IEmployeeService employeeService
        , ILogger<HomeController> logger
        , UserManager<UserIdentityExt> userManager
        )

    {
        _employeeService = employeeService;
        _userManager = userManager;
        _logger = logger;
    }

    public async Task<IActionResult> Index()
    {
        if (User.Identity?.IsAuthenticated ?? false)
        {
            var companyId = _userManager.GetUserAsync(User).Result.CompanyId;

            var model = await _employeeService.AllEmployeesWithSalary(companyId);
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
        var companyId = _userManager.GetUserAsync(User).Result.CompanyId;

        int employeeId = await _employeeService.CreateEmployee(employee, companyId);
        var amount = employee.Salary;

        await _employeeService.CreateExpensesByEmployee(employeeId, amount, companyId);

        return RedirectToAction(nameof(Index), new { id = employeeId });

    }

    [HttpGet]
    public async Task<IActionResult> AddDepartment()
    {
        var model = new AddEmployeeDepartmentViewModel()
        {
            EmployeeDepartments = await _employeeService.AllDepartments()
        };
        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> AddDepartment(AddEmployeeDepartmentViewModel model)
    {
        if ((await _employeeService.DepartmentExists(model.DepartmentId)) == false)
        {
            ModelState.AddModelError(nameof(model.DepartmentId), "Department does not exists");
        }
        if (!ModelState.IsValid)
        {
            model.EmployeeDepartments = await _employeeService.AllDepartments();

            return View(model);
        }
        var companyId = GetCompanyId();

        var result = await _employeeService.CreateDepartment(model, companyId);

        if (result != -1) return RedirectToAction(nameof(Index));

        ModelState.AddModelError("", "Department Already Exist");
        return View(model);
    }


    [HttpGet]
    public async Task<IActionResult> Edit(int id)
    {
        if ((await _employeeService.Exists(id)) == false)
        {
            return RedirectToAction(nameof(Index));
        }
        var user = _userManager.GetUserAsync(User);
        var companyId = user.Result.CompanyId;

        var employee = await _employeeService.EmployeeDetailsById(id, companyId);

        var salary = _employeeService.GetEmployeeSalary(employee.Id).Result.Amount;
        var departmentId = await _employeeService.GetEmployeeDepartmentId(id);


        var model = new EmployeeViewModel()
        {
            Id = id,
            FirstName = employee.FirstName,
            LastName = employee.LastName,
            JobTitle = employee.JobTitle,
            ImageUrl = employee.ImageUrl,
            Salary = salary,
            DepartmentId = departmentId,
            EmployeeDepartments = await _employeeService.AllDepartments()
        };

        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(int id, EmployeeViewModel model)
    {
        if (id != model.Id)
        {
            return RedirectToPage("/Account/AccessDenied", new { area = "Identity" });
        }

        if ((await _employeeService.Exists(model.Id)) == false)
        {
            ModelState.AddModelError("", "Employee does not exist");
            model.EmployeeDepartments = await _employeeService.AllDepartments();

            return View(model);
        }


        if (ModelState.IsValid == false)
        {
            model.EmployeeDepartments = await _employeeService.AllDepartments();

            return View(model);
        }
        var user = _userManager.GetUserAsync(User);
        var companyId = user.Result.CompanyId;
        await _employeeService.Edit(model.Id, model, companyId);

        return RedirectToAction(nameof(Index), new { model.Id });
    }

    [HttpGet]
    public async Task<IActionResult> Delete(int id)
    {
        if ((await _employeeService.Exists(id)) == false)
        {
            return RedirectToAction(nameof(Index));
        }
        var user = _userManager.GetUserAsync(User);
        var companyId = user.Result.CompanyId;

        var employee = await _employeeService.EmployeeDetailsById(id, companyId);
        var model = new EmployeeViewModel()
        {
            FirstName = employee.FirstName,
            LastName = employee.LastName,
            ImageUrl = employee.ImageUrl
        };

        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> Delete(int id, EmployeeViewModel model)
    {
        if ((await _employeeService.Exists(id)) == false)
        {
            return RedirectToAction(nameof(Index));
        }


        await _employeeService.Delete(id);

        return RedirectToAction(nameof(Index));
    }

    private Guid GetCompanyId()
    {
        var companyId = _userManager.GetUserAsync(User).Result.CompanyId;
        return companyId;
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}

