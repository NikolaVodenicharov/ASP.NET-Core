using LearningSystem.Data.Constants;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace LearningSystem.Data.Models
{
    public class User : IdentityUser
    {
        [Required]
        [MinLength(UserConstants.NameMinLength)]
        [MaxLength(UserConstants.NameMaxLength)]
        public string Name { get; set; }

        public DateTime Birthdate { get; set; }

        public List<CourseUser> CourseUsers { get; set; } = new List<CourseUser>();
        public List<Course> Trainings { get; set; } = new List<Course>();
        public List<Article> Articles { get; set; } = new List<Article>();
    }
}
