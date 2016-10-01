using Microsoft.AspNetCore.Mvc;

namespace DevelopedUsingDotNet.Controllers
{
	public class HomeController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}

		public IActionResult Error(int code)
		{
			ViewData.Add("Code", code);

			return View();
		}
	}
}
