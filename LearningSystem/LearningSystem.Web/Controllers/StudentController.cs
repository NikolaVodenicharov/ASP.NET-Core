using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using iTextSharp.text;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text.pdf;
using LearningSystem.Data.Models;
using LearningSystem.Services.Interfaces;
using LearningSystem.Web.Infrastructure.Attributes;
using LearningSystem.Web.Infrastructure.Extensions;
using LearningSystem.Web.Models.Courses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace LearningSystem.Web.Controllers
{
    /// <summary>
    /// All methods must check is the user is authenticated. There is private method for checking that, named RedirectUnauthenticated.
    /// </summary>
    [Authorize]
    [RouteController(nameof(StudentController))]
    public class StudentController : AbstractController
    {
        private readonly UserManager<User> userManager;
        private readonly ICourseService courseService;
        private readonly IUserService userService;

        public StudentController(UserManager<User> userManager, ICourseService courseService, IMapper mapper, IUserService userService)
            : base(mapper)
        {
            this.userManager = userManager;
            this.courseService = courseService;
            this.userService = userService;
        }

        [Route(nameof(AllCourses))]
        public IActionResult AllCourses(string searchString = null, int page = 1)
        {
            this.RedirectUnauthenticated();

            var studentId = this.userManager.GetUserId(base.User);

            var model = new CoursesSummaryViewModel
            {
                Courses = this.courseService.AllByStudent(studentId, searchString, page),
                LoggedUserId = studentId,
                SearchString = searchString,
                ControllerName = nameof(StudentController).Replace("Controller", string.Empty),
                ActionName = nameof(CourseDetails)
            };

            return View(model);
        }

        [Route(nameof(CourseDetails) + "/{id}")]
        public IActionResult CourseDetails(int id)
        {
            this.RedirectUnauthenticated();

            var courseServiceModel = this.courseService.GetById(id);

            var studentId = this.userManager.GetUserId(User);

            var model = new CourseDetailsWithStudentViewModel
            {
                CourseDetails = this.mapper.Map<CourseDetailsViewModel>(courseServiceModel),
                Userid = studentId,
                HaveCertificate = this.userService.HasStudentCertificate(studentId, id)
            };

            return View(model);
        }

        public IActionResult DownloadCertificate(int courseId, string studentId)
        {
            this.RedirectUnauthenticated();

            if (courseId < 1 || studentId == null)
            {
                return BadRequest();
            }

            var haveCertificate = this.userService.HasStudentCertificate(studentId, courseId);
            if (!haveCertificate)
            {
                return BadRequest();
            }

            var certificate = this.userService.CreateCertificate(studentId, courseId);
            if (certificate == null)
            {
                return BadRequest();
            }

            return File(certificate, "application/pdf");
        }


        /// <summary>
        /// Check if the user is authenticated, If it is not, he is redirected to some base home page.
        /// </summary>
        private void RedirectUnauthenticated()
        {
            if (base.User.Identity.IsAuthenticated)
            {
                return;
            }

            this.RedirectToAllCourses();
        }
        private IActionResult RedirectToAllCourses()
        {
            base.TempData.AddErrrorMessage($"You are not authenticated, you cannot use this functionallity.");
            return RedirectToRoute($"{nameof(CourseController).Replace("Controller", string.Empty) }/ All");
        }
    }
}