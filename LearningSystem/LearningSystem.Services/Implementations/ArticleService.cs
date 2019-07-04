using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using LearningSystem.Data;
using LearningSystem.Data.Models;
using LearningSystem.Services.Interfaces;

namespace LearningSystem.Services.Implementations
{
    public class ArticleService : AbstractService, IArticleService
    {
        public ArticleService(LearningSystemDbContext db, IMapper mapper) 
            : base(db, mapper)
        {
        }

        public void CreateArticle(Article article)
        {
            base.db.Articles.Add(article);
            base.db.SaveChanges();
        }
    }
}
