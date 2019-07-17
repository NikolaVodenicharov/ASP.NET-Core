using AutoMapper;
using AutoMapper.QueryableExtensions;
using LearningSystem.Data;
using LearningSystem.Data.Enums;
using LearningSystem.Services.Constants;
using LearningSystem.Services.Interfaces;
using LearningSystem.Services.Models;
using LearningSystem.Services.Models.Users;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LearningSystem.Services.Implementations
{
    public class UserService : AbstractService, IUserService
    {
        private readonly IPdfGenerator pdfGenerator;

        public UserService(LearningSystemDbContext db, IMapper mapper, IPdfGenerator pdfGenerator) 
            : base(db, mapper)
        {
            this.pdfGenerator = pdfGenerator;
        }

        public List<UserListingServiceModel> AllByPages(int page = PageConstants.DefaultPage)
        {
            return base.db
                .Users
                .OrderBy(u => u.UserName)
                .Skip(PageConstants.PageSize * (page - 1))
                .Take(PageConstants.PageSize)
                .ProjectTo<UserListingServiceModel>(base.mapper.ConfigurationProvider)
                .ToList();
        }

        public List<string> FindIdsByRole(string roleName)
        {
            var role = base.db
                .Roles
                .FirstOrDefault(r => r.Name == roleName);

            if (role == null)
            {
                return null;
            }

            return base.db
                .UserRoles
                .Where(ur => ur.RoleId == role.Id)
                .Select(ur => ur.UserId)
                .ToList();
        }
        public bool HasStudentCertificate(string studentId, int courseId)
        {
            var grade = this.db
                .CourseUsers
                .Find(courseId, studentId)
                .StudentGrade;

            if (grade == StudentGrade.A ||
                grade == StudentGrade.B ||
                grade == StudentGrade.C)
            {
                return true;
            }

            return false;
        }

        public byte[] CreateCertificate(string studentId, int courseId)
        {
            if (!this.HasStudentCertificate(studentId, courseId))
            {
                return null;
            }

            var data = this.db
                .CourseUsers
                .Where(cu => cu.CourseId == courseId && cu.UserId == studentId)
                .Select(cu => new
                {
                    CourseName = cu.Course.Name,
                    CourseStartDate = cu.Course.StartDate,
                    CourseEndDate = cu.Course.EndDate,
                    StudentName = cu.User.Name,
                    StudentGrade = cu.StudentGrade ?? StudentGrade.F,
                    TrainerName = cu.Course.Trainer.Name,
                    CertificateDate = DateTime.UtcNow
                })
                .FirstOrDefault();

            var pdfModel = $"<h1>Certificate</h1>" +
                $"<br/>" +
                $"<h2>{data.CourseName}</h2>" +
                $"<br/>" +
                $"<h3>{data.CourseStartDate} - {data.CourseEndDate}" +
                $"<h3><To {data.StudentName} - Grade {data.StudentGrade} </h3>" +
                $"<br/>" +
                $"<h3>Trainer: {data.TrainerName} publishing date:{data.CertificateDate}";

            return this.pdfGenerator.GeneratePdfFromHtml(pdfModel);
        }
    }
}
