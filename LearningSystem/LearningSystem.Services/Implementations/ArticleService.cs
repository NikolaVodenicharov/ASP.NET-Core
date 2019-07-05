using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using LearningSystem.Data;
using LearningSystem.Data.Models;
using LearningSystem.Services.Constants;
using LearningSystem.Services.Interfaces;
using LearningSystem.Services.Models.Articles;

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

        public List<ArticleSummaryServiceModel> AllByPages(int page = PageConstants.DefaultPage)
        {
            var articles = base.db
                .Articles
                .OrderByDescending(a => a.PublishDate)
                .Skip(PageConstants.PageSize * (page - 1))
                .Take(PageConstants.PageSize)
                .ProjectTo<ArticleSummaryServiceModel>(base.mapper.ConfigurationProvider)
                .ToList();

            return articles;
        }

        public ArticleServiceModel GetById(int id)
        {
            return base.db
                .Articles
                .Where(a => a.Id == id)
                .ProjectTo<ArticleServiceModel>(base.mapper.ConfigurationProvider)
                .FirstOrDefault();
        }
    }
}
