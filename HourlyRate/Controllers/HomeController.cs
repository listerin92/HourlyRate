using System.Diagnostics;
using HourlyRate.Core.Contracts;
using HourlyRate.Core.Models;
using Microsoft.AspNetCore.Mvc;

namespace HourlyRate.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly IEmployeeService _employeeService;

    public HomeController(
        IEmployeeService employeeService,
        ILogger<HomeController> logger)
    {
        _employeeService = employeeService;
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


    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}

