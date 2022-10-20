using Microsoft.AspNetCore.Mvc;

namespace HourlyRate.Controllers
{
    public class TestController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
