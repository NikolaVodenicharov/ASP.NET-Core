using LearningSystem.Data.Enums;
using LearningSystem.Services.Constants;
using LearningSystem.Services.Models;
using LearningSystem.Services.Models.Trainers;
using System.Collections.Generic;

namespace LearningSystem.Services.Interfaces
{
    public interface ITrainerService
    {
        List<CourseSummaryServiceModel> AllCoursesByPages(string trainerId, int page = PageConstants.DefaultPage);

        CourseWithStudentsSummaryServiceModel CourseDetails(int courseId);

        bool SetGrade(int courseId, string studentId, StudentGrade grade);
    }
}
