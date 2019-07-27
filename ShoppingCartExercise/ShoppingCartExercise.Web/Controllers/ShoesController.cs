using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ShoppingCartExercise.Services.Interfaces;

namespace ShoppingCartExercise.Web.Controllers
{
    public class ShoesController : Controller
    {
        private readonly IShoeService shoeService;

        public ShoesController(IShoeService shoeService)
        {
            this.shoeService = shoeService;
        }

        public IActionResult All()
        {
            var model = this.shoeService.All();

            return View(model);
        }


        private void Seeder()
        {
            for (int i = 1; i < 10; i++)
            {
                this.shoeService.Add($"Shoe{i}", i * 10, 5);
            }
        }
    }
}