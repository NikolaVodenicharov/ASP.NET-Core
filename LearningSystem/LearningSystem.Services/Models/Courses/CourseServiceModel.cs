using LearningSystem.Data.Models;
using LearningSystem.Helpers.Mappings;
using System;
using System.Collections.Generic;
using System.Text;

namespace LearningSystem.Services.Models.Courses
{
    public class CourseServiceModel : IMapFrom<Course>
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }
    }
}
