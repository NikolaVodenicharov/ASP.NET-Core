using LearningSystem.Services.Models;
using System.Collections.Generic;

namespace LearningSystem.Web.Models.Courses
{
    /// <summary>
    /// Have properties for controller and action redirection for more details of the course. 
    /// In that way we can generalize the layout for courses summary.
    /// Example:
    /// ControllerName = nameof(TrainerController).Replace("Controller", string.Empty),
    /// ActionName = nameof(CourseDetails)             
    /// </summary>
    public class CoursesSummaryViewModel
    {
        public IEnumerable<CourseSummaryServiceModel> Courses { get; set; }
        public string LoggedUserId { get; set; }
        public string SearchString { get; set; }

        public string ControllerName { get; set; }

        public string ActionName { get; set; }
    }
}
