using System;
using System.Collections.Generic;
using System.Text;

namespace LearningSystem.Data.Models
{
    public class CourseUser
    {
        public int id { get; set; }

        public Course Course { get; set; }
        public int CourseId { get; set; }

        public User User { get; set; }
        public string UserId { get; set; }
    }
}
