using LearningSystem.Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LearningSystem.Web.Models.Courses
{
    public class CoursesSummaryViewModel
    {
        public IEnumerable<CourseSummaryServiceModel> Courses { get; set; }
        public string LoggedUserId { get; set; }
        public string SearchString { get; set; }
    }
}
