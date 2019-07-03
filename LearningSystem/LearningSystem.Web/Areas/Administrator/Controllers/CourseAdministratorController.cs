using LearningSystem.Data.Models;
using LearningSystem.Services.Interfaces;
using LearningSystem.Web.Areas.Administrator.Models.Courses;
using LearningSystem.Web.Infrastructure;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LearningSystem.Web.Areas.Administrator.Controllers
{
    [Area("Administrator")]
    [Route("CourseAdministrator")]
    public class CourseAdministratorController : Controller
    {
        private UserManager<User> userManager;
        private IUserService userService;
        private ICourseService courseService;

        public CourseAdministratorController(UserManager<User> userManager, IUserService userService, ICourseService courseService)
        {
            this.userManager = userManager;
            this.userService = userService;
            this.courseService = courseService;
        }

        [Route(nameof(CreateCourse))]
        public async Task<IActionResult> CreateCourse()
        {
            var model = new CreateCourseViewModel
            {
                Trainers = this.CreateTrainersSelectListItem()
            };

            return Redirect("/home/index");

            return View(model);
        }
        private List<SelectListItem> CreateTrainersSelectListItem()
        {
            var trainersIds = this.userService.FindUsersByRole(Constants.Roles.Trainer);
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

            this.courseService.CreateCourse(course);

            return Redirect("/home/index");
        }
    }
}