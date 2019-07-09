using AutoMapper;
using AutoMapper.QueryableExtensions;
using LearningSystem.Data;
using LearningSystem.Data.Enums;
using LearningSystem.Services.Constants;
using LearningSystem.Services.Interfaces;
using LearningSystem.Services.Models;
using LearningSystem.Services.Models.Trainers;
using LearningSystem.Services.Models.Users;
using System.Collections.Generic;
using System.Linq;

namespace LearningSystem.Services.Implementations
{
    public class TrainerService : AbstractService, ITrainerService
    {
        public TrainerService(LearningSystemDbContext db, IMapper mapper) 
            : base(db, mapper)
        {
        }

        public List<CourseSummaryServiceModel> AllCoursesByPages(string trainerId, int page = PageConstants.DefaultPage)
        {
            return base.db
                .Courses
                .Where(c => c.TrainerId == trainerId)
                .OrderBy(c => c.StartDate)
                .Skip(PageConstants.PageSize * (page - 1))
                .Take(PageConstants.PageSize)
                .ProjectTo<CourseSummaryServiceModel>(base.mapper.ConfigurationProvider)
                .ToList();
        }

        public CourseWithStudentsSummaryServiceModel CourseDetails(int courseId)
        {
            var course = this.db
                .Courses
                .Find(courseId);

            if (course == null)
            {
                return null;
            }

            var model = base.mapper
                .Map<CourseWithStudentsSummaryServiceModel>(course);

            model.Students = this.db
                .Courses
                .Where(c => c.Id == courseId)
                .SelectMany(c => c
                    .CourseUsers
                    .Select(cu => new UserWithGradeServiceModel
                    {
                        Id = cu.User.Id,
                        UserName = cu.User.UserName,
                        StudentGrade = cu.StudentGrade
                    }))
                .OrderBy(u => u.UserName)
                .ToList();

            return model;
        }

        public bool SetGrade(int courseId, string studentId, StudentGrade grade)
        {
            var courseUser = this.db
                .CourseUsers
                .Where(cu => cu.CourseId == courseId && cu.UserId == studentId)
                .FirstOrDefault();

            if (courseUser == null)
            {
                return false;
            }

            courseUser.StudentGrade = grade;
            this.db.SaveChanges();
            return true;
        }
    }
}
