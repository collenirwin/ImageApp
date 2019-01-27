using Microsoft.AspNetCore.Mvc;

namespace ImageApp.Controllers
{
    public class UserController : BaseController
    {
        public IActionResult Index(string username)
        {
            if (string.IsNullOrEmpty(username))
            {
                return RedirectToAction(nameof(HomeController.Index), "Home");
            }

            return Content($"{username}'s user page");
        }
    }
}
