using AutoMapper;
using LearningSystem.Data.Models;
using LearningSystem.Helpers.Mappings;
using System;
using System.Collections.Generic;
using System.Text;

namespace LearningSystem.Services.Models.Articles
{
    public class ArticleServiceModel : IMapFrom<Article>, IHaveCustomMapping
    {
        public string Title { get; set; }

        public string Content { get; set; }

        public DateTime PublishDate { get; set; }

        public string AuthorUserName { get; set; }

        public void ConfigureMapping(Profile profile)
        {
            profile
                .CreateMap<Article, ArticleServiceModel>()
                .ForMember(
                    asm => asm.AuthorUserName, 
                    configure => configure.MapFrom(a => a.Author.UserName));
        }
    }
}

