using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace ImageApp.Controllers
{
    public class UserController : Controller
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
