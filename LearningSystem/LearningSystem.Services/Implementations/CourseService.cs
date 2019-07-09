using AutoMapper;
using AutoMapper.QueryableExtensions;
using LearningSystem.Data;
using LearningSystem.Data.Models;
using LearningSystem.Services.Constants;
using LearningSystem.Services.Interfaces;
using LearningSystem.Services.Models;
using LearningSystem.Services.Models.Courses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LearningSystem.Services.Implementations
{
    public class CourseService : AbstractService, ICourseService
    {
        public CourseService(LearningSystemDbContext db, IMapper mapper) 
            : base(db, mapper)
        {
        }

        public void Create(Course course)
        {
            base.db.Courses.Add(course);
            base.db.SaveChanges();
        }

        public List<CourseSummaryServiceModel> AllByPages(int page = PageConstants.DefaultPage)
        {
            return base.db
                .Courses
                .OrderBy(c => c.StartDate)
                .Skip(PageConstants.PageSize * (page - 1))
                .Take(PageConstants.PageSize)
                .ProjectTo<CourseSummaryServiceModel>(base.mapper.ConfigurationProvider)
                .ToList();
        }

        public List<CourseSummaryServiceModel> AllByStudentByPages(string studentId, int page = PageConstants.DefaultPage)
        {
            var result = base.db
                .CourseUsers
                .Where(cu => cu.UserId == studentId)
                .Select(cu => cu.Course)
                .OrderBy(c => c.StartDate)
                .Skip(PageConstants.PageSize * (page - 1))
                .Take(PageConstants.PageSize)
                .ProjectTo<CourseSummaryServiceModel>(base.mapper.ConfigurationProvider)
                .ToList();

            return result;
        }

        public CourseServiceModel GetById(int id)
        {
            return this.db
                .Courses
                .Where(c => c.Id == id)
                .ProjectTo<CourseServiceModel>(base.mapper.ConfigurationProvider)
                .FirstOrDefault();
        }

        public bool IsUserSingIn (int courseId, string userId)
        {
            return this.db
                .CourseUsers
                .Any(cu => cu.CourseId == courseId && cu.UserId == userId);
        }

        public void SingInUser(CourseUser courseUser)
        {
            base.db.CourseUsers.Add(courseUser);
            base.db.SaveChanges();
        }

        public void SingOutUser(CourseUser courseUser)
        {
            base.db.CourseUsers.Remove(courseUser);
            base.db.SaveChanges();
        }
    }
}
