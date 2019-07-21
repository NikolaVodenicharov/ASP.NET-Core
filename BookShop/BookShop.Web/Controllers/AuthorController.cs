using BookShop.Data.Models;
using BookShop.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace BookShop.Web.Controllers
{
    public class AuthorController : ApiAbstractController
    {
        private readonly IAuthorService authorService;

        public AuthorController(IAuthorService authorService)
        {
            this.authorService = authorService;
        }

        [HttpGet]
        [Route("{id}")]
        public Author Get (int id)
        {
            return this.authorService.Get(id);
        }

        [HttpPost]
        public ActionResult<Author> Post(Author author)
        {
            var isSuccessful = this.authorService.Create(author);

            if (!isSuccessful)
            {
                return NotFound();
            }

            return author;
        }
    }
}