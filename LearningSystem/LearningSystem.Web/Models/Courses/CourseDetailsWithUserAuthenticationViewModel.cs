using LearningSystem.Helpers.Mappings;
using LearningSystem.Services.Models.Courses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LearningSystem.Web.Models.Courses
{
    public class CourseDetailsWithUserAuthenticationViewModel
    {
        public CourseDetailsViewModel CourseDetails { get; set; }

        public string LoggedUserid { get; set; }

        public bool IsUserSingIn { get; set; }
    }
}
