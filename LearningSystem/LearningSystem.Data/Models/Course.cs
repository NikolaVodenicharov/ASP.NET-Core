using LearningSystem.Data.Constants;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace LearningSystem.Data.Models
{
    public class Course
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(CourseConstants.NameMaxLength)]
        public string Name { get; set; }

        [Required]
        [MaxLength(CourseConstants.DescriptionMaxlenght)]
        public string Description { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        [Required]
        public User Trainer { get; set; }
        public string TrainerId { get; set; }

        public List<CourseUser> CourseUsers { get; set; }

    }
}