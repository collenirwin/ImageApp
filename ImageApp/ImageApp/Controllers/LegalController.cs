using Microsoft.AspNetCore.Mvc;

namespace ImageApp.Controllers
{
    public class LegalController : Controller
    {
        public IActionResult Licenses()
        {
            return View();
        }

        public IActionResult Tos()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }
    }
}
