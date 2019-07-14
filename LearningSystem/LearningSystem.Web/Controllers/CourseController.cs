using AutoMapper;
using LearningSystem.Data.Constants;
using LearningSystem.Data.Models;
using LearningSystem.Services.Interfaces;
using LearningSystem.Web.Infrastructure.Attributes;
using LearningSystem.Web.Infrastructure.Constants;
using LearningSystem.Web.Infrastructure.Extensions;
using LearningSystem.Web.Models.Courses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.IO;

namespace LearningSystem.Web.Controllers
{
    [RouteController(nameof(CourseController))]
    public class CourseController : Controller
    {
        public const string ZipFileExtension = ".zip";

        private readonly ICourseService courseService;
        private readonly UserManager<User> userManager;
        private readonly IMapper mapper;

        public CourseController(ICourseService courseService, UserManager<User> userManager, IMapper mapper)
        {
            this.courseService = courseService;
            this.userManager = userManager;
            this.mapper = mapper;
        }

        [Route(nameof(All))]
        public IActionResult All(string searchString = null, int page = 1)
        {
            var model = new CoursesSummaryViewModel
            {
                Courses = this.courseService.All(searchString, page),
                SearchString = searchString
            };

            var isAuthenticated = User.Identity.IsAuthenticated;
            if (isAuthenticated)
            {
                model.LoggedUserId = this.userManager.GetUserId(User);
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
            var isAuthenticated = base.User.Identity.IsAuthenticated;
            if (!isAuthenticated)
            {
                return;
            }

            var userId = this.userManager.GetUserId(User);
            model.LoggedUserid = userId;
        }
        private void AddSingInStatus(CourseDetailsViewModel model)
        {
            var userId = model.LoggedUserid;
            if (userId == null)
            {
                return;
            }

            var isUserSingInCourse = this.courseService.IsUserSingIn(model.Id, userId);
            model.IsUserSingIn = isUserSingInCourse;
        }

        [HttpPost]
        [Authorize]
        [Route(nameof(SubmitExam))]
        public IActionResult SubmitExam(int courseId, string studentId, IFormFile examSubmission)
        {
            if (courseId < 1 || studentId == null)
            {
                return BadRequest();
            }

            if (examSubmission.Length > CourseUserConstants.ExamSubmissionMaxSize || !examSubmission.FileName.EndsWith(ZipFileExtension))
            {
                base.TempData.AddSuccessMessage($"The file must be less than {CourseUserConstants.ExamSubmissionMaxSize} and must be of type {ZipFileExtension}.");

                return RedirectToAction(nameof(Details), new { courseId });
            }

            using (var memoryStream = new MemoryStream())
            {
                examSubmission.CopyToAsync(memoryStream);
                var examBytes = memoryStream.ToArray();

                var isSuccessful = this.courseService.SaveSubmitedExam(courseId, studentId, examBytes);

                if (!isSuccessful)
                {
                    return BadRequest();
                }

            };

            return RedirectToAction(nameof(All));
        }

        [HttpPost]
        [Route(nameof(SingInUser))]
        public IActionResult SingInUser(CourseUser model)
        {
            if (model.CourseId == 0 || model.UserId == null)
            {
                return RedirectToAction(nameof(All));
            }

            this.courseService.SingInUser(model);
            this.TempData.AddSuccessMessage("Sing in is successful.");

            return RedirectToAction(nameof(All));
        }

        [HttpPost]
        [Route(nameof(SingOutUser))]
        public IActionResult SingOutUser(CourseUser model)
        {
            if (model.CourseId == 0 || model.UserId == null)
            {
                return RedirectToAction(nameof(All));
            }

            this.courseService.SingOutUser(model);
            this.TempData.AddSuccessMessage("Sing out is successful.");

            return RedirectToAction(nameof(All));
        }
    }
}