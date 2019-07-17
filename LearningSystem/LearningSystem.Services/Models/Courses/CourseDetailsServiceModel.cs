using AutoMapper;
using LearningSystem.Data.Models;
using LearningSystem.Helpers.Mappings;
using System;
using System.Collections.Generic;
using System.Text;

namespace LearningSystem.Services.Models.Courses
{
    public class CourseDetailsServiceModel : CourseServiceModel, IHaveCustomMapping
    {
        public string TrainerUserName { get; set; }

        public void ConfigureMapping(Profile profile)
        {
            profile
                .CreateMap<Course, CourseDetailsServiceModel>()
                .ForMember(csm =>
                    csm.TrainerUserName,
                    configure => configure.MapFrom(c => c.Trainer.UserName));
        }
    }
}
