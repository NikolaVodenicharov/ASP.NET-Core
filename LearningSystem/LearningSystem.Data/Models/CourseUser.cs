using LearningSystem.Data.Constants;
using LearningSystem.Data.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace LearningSystem.Data.Models
{
    public class CourseUser
    {
        public Course Course { get; set; }
        public int CourseId { get; set; }

        public User User { get; set; }
        public string UserId { get; set; }

        public StudentGrade? StudentGrade { get; set; }

        [MaxLength(CourseUserConstants.ExamSubmissionMaxSize)]
        public byte[] ExamSubmission { get; set; }
    }
}
