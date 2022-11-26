using Microsoft.AspNetCore.Mvc;

namespace HourlyRate.Controllers
{
    public class CostCentersController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
