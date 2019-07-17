using AutoMapper;
using LearningSystem.Services.Interfaces;
using LearningSystem.Web.Infrastructure.Attributes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LearningSystem.Web.Controllers
{
    [RouteController(nameof(ArticleController))]
    public class ArticleController : AbstractController
    {
        private readonly IArticleService articleService;

        public ArticleController(IMapper mapper, IArticleService articleService)
            : base(mapper)
        {
            this.articleService = articleService;
        }

        [Route(nameof(AllByPages))]
        public IActionResult AllByPages()
        {
            return View(
                this.articleService.AllByPages());
        }

        [Authorize]
        [Route(nameof(Details) + "/{id}")]
        public IActionResult Details(int id)
        {
            return View(
                this.articleService.GetById(id));
        }
    }
}
