﻿using LearningSystem.Data.Models;
using LearningSystem.Helpers.Mappings;
using System;
using System.Collections.Generic;
using System.Text;

namespace LearningSystem.Services.Models
{
    public class CourseSummaryServiceModel : IMapFrom<Course>
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public DateTime StartDate { get; set; }
    }
}
