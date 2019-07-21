using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BookShop.Web.Models;
using BookShop.Services.Interfaces;
using BookShop.Data.Models;

namespace BookShop.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IAuthorService authorService;

        public HomeController(IAuthorService authorService)
        {
            this.authorService = authorService;
        }


        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
