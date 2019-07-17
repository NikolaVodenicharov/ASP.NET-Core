using LearningSystem.Helpers.Mappings;
using LearningSystem.Services.Models.Courses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LearningSystem.Web.Models.Courses
{
    public class CourseDetailsViewModel : IMapFrom<CourseDetailsServiceModel>
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public string TrainerUserName { get; set; }

    }
}
