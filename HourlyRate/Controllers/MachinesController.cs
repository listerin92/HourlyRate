using Microsoft.AspNetCore.Mvc;

namespace HourlyRate.Controllers
{
    public class MachinesController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
