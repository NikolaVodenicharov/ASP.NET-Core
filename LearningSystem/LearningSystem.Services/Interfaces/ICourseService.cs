using LearningSystem.Data.Models;
using LearningSystem.Services.Constants;
using LearningSystem.Services.Models;
using LearningSystem.Services.Models.Courses;
using System;
using System.Collections.Generic;
using System.Text;

namespace LearningSystem.Services.Interfaces
{
    public interface ICourseService
    {
        void Create(Course course);

        List<CourseSummaryServiceModel> AllByPages(int page = PageConstants.DefaultPage);

        CourseServiceModel GetById(int id);

        bool IsUserSingIn(int courseId, string userId);

        void SingInUser(CourseUser courseUser);

        void SingOutUser(CourseUser courseUser);
    }
}
