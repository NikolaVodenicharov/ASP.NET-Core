using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LearningSystem.Data.Models;
using LearningSystem.Services.Interfaces;
using LearningSystem.Web.Infrastructure.Attributes;
using LearningSystem.Web.Models.Courses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace LearningSystem.Web.Controllers
{
    [Authorize]
    [RouteController(nameof(UserController))]
    public class UserController : Controller
    {
        private readonly UserManager<User> userManager;
        private readonly ICourseService courseService;

        public UserController(UserManager<User> userManager, ICourseService courseService)
        {
            this.userManager = userManager;
            this.courseService = courseService;
        }

        [Route(nameof(SignedCourses))]
        public IActionResult SignedCourses()
        {
            var studentId = this.userManager.GetUserId(base.User);

            var model = new CoursesSummaryUserIdViewModel
            {
                Courses = this.courseService.AllByStudentByPages(studentId),
                UserId = studentId
            };

            return View(model);
        }
    }
}