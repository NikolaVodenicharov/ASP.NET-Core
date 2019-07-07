using LearningSystem.Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LearningSystem.Web.Models.Courses
{
    public class CoursesSummaryUserIdViewModel
    {
        public IEnumerable<CourseSummaryServiceModel> Courses { get; set; }
        public string UserId { get; set; }
    }
}
