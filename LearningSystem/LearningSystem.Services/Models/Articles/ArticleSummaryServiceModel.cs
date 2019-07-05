using LearningSystem.Data.Models;
using LearningSystem.Helpers.Mappings;
using System;
using System.Collections.Generic;
using System.Text;

namespace LearningSystem.Services.Models.Articles
{
    public class ArticleSummaryServiceModel : IMapFrom<Article>
    {
        public int Id { get; set; }
        public string Title { get; set; }
    }
}
