using AutoMapper;
using LearningSystem.Data.Models;
using LearningSystem.Services.Interfaces;
using LearningSystem.Web.Infrastructure.Extensions;
using LearningSystem.Web.Models.Courses;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace LearningSystem.Web.Controllers
{
    [Route("Course")]
    public class CourseController : Controller
    {
        private readonly ICourseService courseService;
        private readonly UserManager<User> userManager;
        private readonly IMapper mapper;

        public CourseController(ICourseService courseService, UserManager<User> userManager, IMapper mapper)
        {
            this.courseService = courseService;
            this.userManager = userManager;
            this.mapper = mapper;
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
                model.UserId = this.userManager.GetUserId(User);
            }

            return View(model);
        }

        [Route(nameof(Details) + "/{id}")]
        public IActionResult Details(int id)
        {
            var courseServiceModel = this.courseService.GetById(id);

            var model = this.mapper.Map<CourseDetailsViewModel>(courseServiceModel);
            this.AddUserId(model);
            this.AddSingInStatus(model);

            return View(model);
        }
        private void AddUserId(CourseDetailsViewModel model)
        {
            var isAuthenticated = User.Identity.IsAuthenticated;
            if (!isAuthenticated)
            {
                return;
            }

            var userId = this.userManager.GetUserId(User);
            model.Userid = userId;
        }
        private void AddSingInStatus(CourseDetailsViewModel model)
        {
            var userId = model.Userid;
            if (userId == null)
            {
                return;
            }

            var isUserSingInCourse = this.courseService.IsUserSingIn(model.Id, userId);
            model.IsUserSingIn = isUserSingInCourse;
        }

        [HttpPost]
        [Route(nameof(SingInUser))]
        public IActionResult SingInUser(CourseUser model)
        {
            if (model.CourseId == 0 || model.UserId == null)
            {
                return RedirectToAction(nameof(AllByPages));
            }

            this.courseService.SingInUser(model);
            this.TempData.AddSuccessMessage("Sing in is successful.");

            return RedirectToAction(nameof(AllByPages));
        }

        [HttpPost]
        [Route(nameof(SingOutUser))]
        public IActionResult SingOutUser(CourseUser model)
        {
            if (model.CourseId == 0 || model.UserId == null)
            {
                return RedirectToAction(nameof(AllByPages));
            }

            this.courseService.SingOutUser(model);
            this.TempData.AddSuccessMessage("Sing out is successful.");

            return RedirectToAction(nameof(AllByPages));
        }
    }
}