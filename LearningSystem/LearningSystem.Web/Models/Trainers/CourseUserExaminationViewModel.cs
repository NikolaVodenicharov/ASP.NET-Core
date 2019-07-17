using LearningSystem.Services.Models.Users;
using LearningSystem.Web.Models.Courses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LearningSystem.Web.Models.Trainers
{
    public class CourseUserExaminationViewModel
    {
        public CourseDetailsViewModel CourseDetails { get; set; }
        public IEnumerable<UserExaminationServiceModel> Students { get; set; }
    }
}
