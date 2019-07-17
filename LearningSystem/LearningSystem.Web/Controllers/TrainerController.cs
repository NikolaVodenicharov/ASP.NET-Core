using AutoMapper;
using LearningSystem.Data.Enums;
using LearningSystem.Data.Models;
using LearningSystem.Services.Interfaces;
using LearningSystem.Web.Infrastructure.Attributes;
using LearningSystem.Web.Infrastructure.Constants;
using LearningSystem.Web.Infrastructure.Extensions;
using LearningSystem.Web.Models.Courses;
using LearningSystem.Web.Models.Trainers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace LearningSystem.Web.Controllers
{
    /// <summary>
    /// All methods must check is the user authorized. There is private method for checking that, named RedirectUnauthorized.
    /// </summary>
    [Authorize(Roles = RoleConstants.Administrator + ", " + RoleConstants.Trainer)]
    [RouteController(nameof(TrainerController))]
    public class TrainerController : AbstractController
    {
        public const string ZipContentType = "application/zip";

        private UserManager<User> userManager;
        private readonly ITrainerService trainerService;
        private readonly ICourseService courseService;

        public TrainerController(IMapper mapper, UserManager<User> userManager, ITrainerService trainerService, ICourseService courseService)
            : base(mapper)
        {
            this.userManager = userManager;
            this.trainerService = trainerService;
            this.courseService = courseService;
        }

        [Route(nameof(AllCourses))]
        public IActionResult AllCourses(string searchString = null, int page = 1)
        {
            this.RedirectUnauthorized();

            var model = new CoursesSummaryViewModel
            {
                Courses = this.courseService.All(searchString, page),
                SearchString = searchString,
                ControllerName = nameof(TrainerController).Replace("Controller", string.Empty),
                ActionName = nameof(CourseDetails)
            };

            return View(model);
        }

        [Route(nameof(CourseDetails) + "/{id}")]
        public IActionResult CourseDetails(int Id)
        {
            var courseDetailsServiceModel = this.courseService.GetById(Id);
            // var model = this.trainerService.CourseDetails(courseId);

            var model = new CourseUserExaminationViewModel
            {
                CourseDetails = this.mapper.Map<CourseDetailsViewModel>(courseDetailsServiceModel),
                Students = this.courseService.AllStudentsInCourse(Id)
            };

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

        /// <summary>
        /// Check if the user is authorized to use trainer functionallity. If it is not, he is redirected to some base home page.
        /// </summary>
        private void RedirectUnauthorized()
        {
            if (base.User.IsInRole(RoleConstants.Trainer) || base.User.IsInRole(RoleConstants.Administrator))
            {
                return;
            }

            this.RedirectToAllCourses();
        }
        private IActionResult RedirectToAllCourses()
        {
            base.TempData.AddErrrorMessage($"You are not {RoleConstants.Administrator} or {RoleConstants.Trainer}, but you try to access trainers functionallity.");
            return RedirectToRoute($"{nameof(CourseController).Replace("Controller", string.Empty) }/ All");
        }
    }
}