using AutoMapper;
using LearningSystem.Data.Enums;
using LearningSystem.Data.Models;
using LearningSystem.Helpers.Mappings;
using System;
using System.Collections.Generic;
using System.Text;

namespace LearningSystem.Services.Models.Users
{
    public class UserWithGradeServiceModel : UserListingServiceModel
    {
        public StudentGrade? StudentGrade { get; set; }
    }
}
