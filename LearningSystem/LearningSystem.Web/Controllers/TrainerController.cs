using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LearningSystem.Data.Enums;
using LearningSystem.Data.Models;
using LearningSystem.Services.Interfaces;
using LearningSystem.Web.Infrastructure.Attributes;
using LearningSystem.Web.Infrastructure.Constants;
using LearningSystem.Web.Infrastructure.Extensions;
using LearningSystem.Web.Models.Courses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace LearningSystem.Web.Controllers
{
    [Authorize(Roles = RoleConstants.Administrator + ", " + RoleConstants.Trainer)]
    [RouteController(nameof(TrainerController))]
    public class TrainerController : Controller
    {
        public const string ZipContentType = "application/zip";

        private UserManager<User> userManager;
        private readonly ITrainerService trainerService;
        private readonly ICourseService courseService;

        public TrainerController(UserManager<User> userManager, ITrainerService trainerService, ICourseService courseService)
        {
            this.userManager = userManager;
            this.trainerService = trainerService;
            this.courseService = courseService;
        }

        [Route(nameof(AllCourses))]
        public IActionResult AllCourses(string searchString = null, int page = 1)
        {
            var trainerId = this.userManager.GetUserId(User);

            var model = new CoursesSummaryViewModel
            {
                Courses = this.courseService.AllByTrainer(trainerId, searchString, page),
                LoggedUserId = trainerId,
                SearchString = searchString
            };

            return View(model);
        }

        [Route(nameof(CourseDetails) + "/{id}")]
        public IActionResult CourseDetails(int id)
        {
            var model = this.trainerService.CourseDetails(id);

            return View(model);
        }

        public IActionResult DownloadExamSubmission(int courseId, string studentId)
        {
            if (courseId < 1 || studentId == null)
            {
                return BadRequest();
            }

            var trainerId = this.userManager.GetUserId(User);
            var isCourseTrainer = this.trainerService.IsCourseTrainer(trainerId, courseId);
            if (!isCourseTrainer)
            {
                return BadRequest();
            }

            var examSubmission = this.trainerService.GetStudentExamSubmission(studentId, courseId);
            if (examSubmission == null)
            {
                return BadRequest();
            }

            return File(examSubmission, ZipContentType);
        }

        [HttpPost]
        [Route(nameof(SetGrade))]
        public IActionResult SetGrade(int courseId, string studentId, StudentGrade grade)
        {
            var areParametersValid = 
                courseId > 0 && 
                !string.IsNullOrEmpty(studentId) && 
                grade != 0;
            if (!areParametersValid)
            {
                return BadRequest();
            }

            var isSetGradeSuccessful = this.trainerService.SetGrade(courseId, studentId, grade);
            if (!isSetGradeSuccessful)
            {
                return BadRequest();
            }

            TempData.AddSuccessMessage("Student grade was successful saved.");
            return RedirectToAction(nameof(CourseDetails), new { id = courseId });
        }
    }
}