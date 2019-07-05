using LearningSystem.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LearningSystem.Web.Controllers
{
    [Route("Article")]
    public class ArticleController : Controller
    {
        private readonly IArticleService articleService;

        public ArticleController(IArticleService articleService)
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
