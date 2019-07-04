using LearningSystem.Data.Models;
using LearningSystem.Services.Constants;
using LearningSystem.Services.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace LearningSystem.Services.Interfaces
{
    public interface ICourseService
    {
        void Create(Course course);

        List<CourseSummaryServiceModel> AllByPages(int page = PageConstants.DefaultPage);

        void SingInUser(CourseUser courseUser);
    }
}
