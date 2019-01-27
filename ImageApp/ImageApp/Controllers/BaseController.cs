using Microsoft.AspNetCore.Mvc;

namespace ImageApp.Controllers
{
    /// <summary>
    /// Base class for this project's controllers; contains some helper methods
    /// </summary>
    public abstract class BaseController : Controller
    {
        /// <summary>
        /// If the given url is a local url, redirect there, otherwise redirect home
        /// </summary>
        /// <param name="url">(should be) local url</param>
        /// <returns>a redirection</returns>
        protected IActionResult RedirectToLocal(string url)
        {
            if (Url.IsLocalUrl(url))
            {
                return Redirect(url);
            }

            return RedirectHome();
        }

        /// <summary>
        /// Redirects to <see cref="HomeController.Index"/>
        /// </summary>
        /// <returns>a redirection</returns>
        protected IActionResult RedirectHome()
        {
            return RedirectToAction(nameof(HomeController.Index), "Home");
        }
    }
}