using AutoMapper;
using AutoMapper.QueryableExtensions;
using LearningSystem.Data;
using LearningSystem.Data.Models;
using LearningSystem.Services.Constants;
using LearningSystem.Services.Interfaces;
using LearningSystem.Services.Models;
using LearningSystem.Services.Models.Courses;
using Microsoft.AspNetCore.Http;
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

        /// <summary>
        /// Return summary of all courses with options for for searching by title.
        /// </summary>
        /// <param name="searchString">This is optional parameter to search in courses by part of the title.</param>
        /// <param name="page">This is parameter to split the data in to a pages.</param>
        /// <returns></returns>
        public List<CourseSummaryServiceModel> All(string searchString = null, int page = PageConstants.DefaultPage)
        {
            var courses = this.db
                .Courses
                as IQueryable<Course>;

            var result = this.ToCourseSummaryPages(courses, searchString, page);

            return result;
        }
        private List<CourseSummaryServiceModel> ToCourseSummaryPages(IQueryable<Course> courses, string searchString, int page)
        {
            if (!string.IsNullOrEmpty(searchString))
            {
                courses = courses.Where(c => c.Name.Contains(searchString));
            }

            var result = courses
                .OrderBy(c => c.StartDate)
                .Skip(PageConstants.PageSize * (page - 1))
                .Take(PageConstants.PageSize)
                .ProjectTo<CourseSummaryServiceModel>(base.mapper.ConfigurationProvider)
                .ToList();

            return result;
        }

        public List<CourseSummaryServiceModel> AllByStudent(string studentId, string searchString = null, int page = PageConstants.DefaultPage)
        {
            var courses = base.db
                .CourseUsers
                .Where(cu => cu.UserId == studentId)
                .Select(cu => cu.Course);

            var result = this.ToCourseSummaryPages(courses, searchString, page);

            return result;
        }

        public List<CourseSummaryServiceModel> AllByTrainer(string trainerId, string searchString = null, int page = PageConstants.DefaultPage)
        {
            var courses = base.db
                .Courses
                .Where(c => c.TrainerId == trainerId);

            var result = this.ToCourseSummaryPages(courses, searchString, page);

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


        public bool SaveSubmitedExam(int courseId, string studentId, byte[] examSubmission)
        {
            var courseUser = this.db
                .CourseUsers
                .Find(courseId, studentId);

            if (courseUser == null)
            {
                return false;
            }

            courseUser.ExamSubmission = examSubmission;
            db.SaveChanges();

            return true;
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
