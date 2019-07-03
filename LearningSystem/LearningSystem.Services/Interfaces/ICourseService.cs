using LearningSystem.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace LearningSystem.Services.Interfaces
{
    public interface ICourseService
    {
        void CreateCourse(Course course);
    }
}
