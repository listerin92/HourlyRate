using Microsoft.AspNetCore.Mvc;

namespace HourlyRate.Areas.Admin.Controllers
{
	[Area(nameof(Admin))]
	public class SettingsController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}
	}
}
