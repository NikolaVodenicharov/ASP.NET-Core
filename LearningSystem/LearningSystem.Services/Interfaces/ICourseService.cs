﻿using LearningSystem.Data.Models;
using LearningSystem.Services.Constants;
using LearningSystem.Services.Models;
using LearningSystem.Services.Models.Courses;
using LearningSystem.Services.Models.Users;
using System;
using System.Collections.Generic;
using System.Text;

namespace LearningSystem.Services.Interfaces
{
    public interface ICourseService
    {
        void Create(Course course);


        List<CourseSummaryServiceModel> All(string namePart = null, int page = PageConstants.DefaultPage);

        List<CourseSummaryServiceModel> AllByStudent(string studentId, string searchString = null, int page = PageConstants.DefaultPage);

        List<CourseSummaryServiceModel> AllByTrainer(string trainerId, string searchString = null, int page = PageConstants.DefaultPage);


        CourseDetailsServiceModel GetById(int id);

        bool SaveSubmitedExam(int courseId, string studentId, byte[] exam);


        bool IsUserSingIn(int courseId, string userId);

        void SingInUser(CourseUser courseUser);

        void SingOutUser(CourseUser courseUser);

        List<UserExaminationServiceModel> AllStudentsInCourse(int courseId);
    }
}
