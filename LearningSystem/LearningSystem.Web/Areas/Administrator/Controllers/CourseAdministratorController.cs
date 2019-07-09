using LearningSystem.Data.Models;
using LearningSystem.Services.Interfaces;
using LearningSystem.Web.Areas.Administrator.Models.Courses;
using LearningSystem.Web.Infrastructure;
using LearningSystem.Web.Infrastructure.Attributes;
using LearningSystem.Web.Infrastructure.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Linq;

namespace LearningSystem.Web.Areas.Administrator.Controllers
{
    [Area("Administrator")]
    [Authorize(Roles = RoleConstants.Administrator)]
    [RouteController(nameof(CourseAdministratorController))]
    public class CourseAdministratorController : Controller
    {
        private readonly UserManager<User> userManager;
        private readonly IUserService userService;
        private readonly ICourseService courseService;

        public CourseAdministratorController(UserManager<User> userManager, IUserService userService, ICourseService courseService)
        {
            this.userManager = userManager;
            this.userService = userService;
            this.courseService = courseService;
        }

        [Route(nameof(CreateCourse))]
        public IActionResult CreateCourse()
        {
            var model = new CreateCourseViewModel
            {
                Trainers = this.CreateTrainersSelectListItem()
            };

            return View(model);
        }
        private List<SelectListItem> CreateTrainersSelectListItem()
        {
            var trainersIds = this.userService.FindIdsByRole(RoleConstants.Trainer);
            if (trainersIds.Count == 0)
            {
                // do something ?
            }

            return this.userManager
                    .Users
                    .Where(u => trainersIds.Contains(u.Id))
                    .Select(u => new SelectListItem
                    {
                        Text = u.UserName,
                        Value = u.Id
                    })
                    .ToList();
        }

        [HttpPost]
        [Route(nameof(CreateCourse))]
        public IActionResult CreateCourse(CreateCourseViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.Trainers = this.CreateTrainersSelectListItem();

                return View(model);
            }

            var course = new Course
            {
                Name = model.Name,
                Description = model.Description,
                StartDate = model.StartDate,
                EndDate = model.EndDate,
                TrainerId = model.TrainerId
            };

            this.courseService.Create(course);

            return Redirect("/Course/AllByPages");
        }
    }
}