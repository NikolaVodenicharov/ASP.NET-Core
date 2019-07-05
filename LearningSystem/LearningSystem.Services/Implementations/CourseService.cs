using AutoMapper;
using AutoMapper.QueryableExtensions;
using LearningSystem.Data;
using LearningSystem.Data.Models;
using LearningSystem.Services.Constants;
using LearningSystem.Services.Interfaces;
using LearningSystem.Services.Models;
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

        public void SingInUser(CourseUser courseUser)
        {
            base.db.CourseUsers.Add(courseUser);
            base.db.SaveChanges();
        }
    }
}
