using LearningSystem.Data;
using LearningSystem.Data.Models;
using LearningSystem.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace LearningSystem.Services.Implementations
{
    public class CourseService : ICourseService
    {
        private LearningSystemDbContext db;

        public CourseService(LearningSystemDbContext db)
        {
            this.db = db;
        }

        public void CreateCourse(Course course)
        {
            this.db.Courses.Add(course);
            this.db.SaveChanges();
        }
    }
}
