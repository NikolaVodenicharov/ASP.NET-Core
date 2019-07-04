using LearningSystem.Data.Models;
using LearningSystem.Services.Interfaces;
using LearningSystem.Services.Models;
using LearningSystem.Web.Models;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace LearningSystem.Web.Controllers
{
    [Route("Course")]
    public class CourseController : Controller
    {
        private readonly ICourseService courseService;

        public CourseController(ICourseService courseService)
        {
            this.courseService = courseService;
        }

        [Route(nameof(AllByPages))]
        public IActionResult AllByPages()
        {
            var model = new CoursesSummaryUserIdViewModel
            {
                Courses = this.courseService.AllByPages()
            };

            var isAuthenticated = User.Identity.IsAuthenticated;
            if (isAuthenticated)
            {
                model.UserId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            }

            return View(model);
        }

        [HttpPost]
        [Route(nameof(SingInUser))]
        public IActionResult SingInUser(CourseUser model)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction(nameof(AllByPages));
            }

            this.courseService.SingInUser(model);
        
            return RedirectToAction(nameof(AllByPages));
        }
    }
}