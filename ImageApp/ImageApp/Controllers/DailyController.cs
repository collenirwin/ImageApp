using ImageApp.Models;
using ImageApp.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ImageApp.Controllers
{
    /// <summary>
    /// Handles serving the daily images
    /// </summary>
    public class DailyController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly CommentService _commentService;

        public DailyController(UserManager<ApplicationUser> userManager, CommentService commentService)
        {
            _userManager = userManager;
            _commentService = commentService;
        }

        [HttpGet]
        public IActionResult Index(int id = 1)
        {
            // only accept ids between 1 and 10
            if (id < 1 || id > 10)
            {
                return RedirectToAction(nameof(Done));
            }

            var model = new DailyViewModel
            {
                Comment = new Comment(),
                ImageHalf = new ImageHalf() // TODO: Write a service for ImageHalfs and a method to get one of
                                            // today's ImageHalfs by a number
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Index(DailyViewModel model)
        {
            if (ModelState.IsValid)
            {
                // get the currently logged in user (or null if there isn't one)
                var user = await _userManager.GetUserAsync(HttpContext.User);

                model.Comment.UserId = user?.Id;

                // TODO: Check for the ImageHalf in the db by its id before doing this?
                // to prevent this comment from being tied to a different or nonexistant image
                model.Comment.ImageHalfId = model.ImageHalf.Id;

                // attempt to add the comment to the database
                bool commentAdded = await _commentService.AddComment(model.Comment);
            }
            else
            {
                ModelState.AddModelError("", "Invalid input");
            }

            return View(model);
        }

        [HttpGet]
        public IActionResult Done()
        {
            return View();
        }
    }
}
