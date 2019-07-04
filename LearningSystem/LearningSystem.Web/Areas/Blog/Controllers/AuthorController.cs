using LearningSystem.Data.Models;
using LearningSystem.Services.Interfaces;
using LearningSystem.Web.Areas.Blog.Models;
using LearningSystem.Web.Infrastructure.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Linq;

namespace LearningSystem.Web.Areas.Blog.Controllers
{
    [Area("Blog")]
    [Authorize(Roles = RoleConstants.BlogAuthor + ", " + RoleConstants.Administrator)]
    [Route("Author")]
    public class AuthorController : Controller
    {
        private readonly UserManager<User> userManager;
        private readonly IUserService userService;
        private readonly IArticleService articleService;

        public AuthorController(UserManager<User> userManager, IUserService userService, IArticleService articleService)
        {
            this.userManager = userManager;
            this.userService = userService;
            this.articleService = articleService;
        }

        [Route(nameof(CreateArticle))]
        public IActionResult CreateArticle()
        {
            var model = new CreateArticleViewModel
            {
                Authors = this.CreateAuthorsSelectListItem()
            };

            return View(model);
        }
        private List<SelectListItem> CreateAuthorsSelectListItem()
        {
            var authorsIds = this.userService.FindIdsByRole(RoleConstants.BlogAuthor);
            if (authorsIds.Count == 0)
            {
                // do something ?
            }

            return this.userManager
                    .Users
                    .Where(u => authorsIds.Contains(u.Id))
                    .Select(u => new SelectListItem
                    {
                        Text = u.UserName,
                        Value = u.Id
                    })
                    .ToList();
        }

        [HttpPost]
        [Route(nameof(CreateArticle))]
        public IActionResult CreateArticle(CreateArticleViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.Authors = this.CreateAuthorsSelectListItem();

                return View(model);
            }

            var article = new Article
            {
                Title = model.Title,
                Content = model.Content,
                PublishDate = model.PublishDate,
                AuthorId = model.AuthorId
            };

            this.articleService.CreateArticle(article);

            return Redirect("/Home/Index");
        }
    }
}