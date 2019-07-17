using LearningSystem.Data.Models;
using LearningSystem.Helpers.Mappings;
using LearningSystem.Services.Models.Users;
using System.Collections.Generic;

namespace LearningSystem.Services.Models.Courses
{
    public class CourseWithStudentsSummaryServiceModel : CourseServiceModel, IMapFrom<Course>
    {
        public IEnumerable<UserExaminationServiceModel> Students { get; set; }
    }
}
