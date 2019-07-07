using AutoMapper;
using LearningSystem.Data.Models;
using LearningSystem.Helpers.Mappings;
using System;
using System.Collections.Generic;
using System.Text;

namespace LearningSystem.Services.Models.Courses
{
    public class CourseServiceModel : IMapFrom<Course>, IHaveCustomMapping
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public string Description { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public string TrainerUserName { get; set; }

        public void ConfigureMapping(Profile profile)
        {
            profile
                .CreateMap<Course, CourseServiceModel>()
                .ForMember(csm =>
                    csm.TrainerUserName,
                    configure => configure.MapFrom(c => c.Trainer.UserName));
        }
    }
}
