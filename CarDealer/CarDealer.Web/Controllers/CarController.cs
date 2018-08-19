namespace CarDealer.Web.Controllers
{
    using CarDealer.Services;
    using Microsoft.AspNetCore.Mvc;
    using System;

    public class CarController : Controller
    {
        private readonly ICarService carService;

        public CarController(ICarService carService)
        {
            this.carService = carService;
        }

        // [Route("car/{make}")] another option to register route
        public IActionResult GetCarsByMake(string make)
        {
            var cars = this.carService.GetCarsByMake(make);

            return View(cars);
        }
    }
}
