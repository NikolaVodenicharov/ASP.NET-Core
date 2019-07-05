using LearningSystem.Data.Models;
using LearningSystem.Services.Constants;
using LearningSystem.Services.Models.Articles;
using System.Collections.Generic;

namespace LearningSystem.Services.Interfaces
{
    public interface IArticleService
    {
        void CreateArticle(Article article);

        List<ArticleSummaryServiceModel> AllByPages(int page = PageConstants.DefaultPage);

        ArticleServiceModel GetById(int id);
    }
}
