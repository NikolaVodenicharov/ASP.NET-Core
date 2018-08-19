namespace CarDealer.Web.Controllers
{
    using CarDealer.Services;
    using Microsoft.AspNetCore.Mvc;
    using System;

    [Route("car")]
    public class CarController : Controller
    {
        private readonly ICarService carService;

        public CarController(ICarService carService)
        {
            this.carService = carService;
        }

        [Route("{make}", Order = 2)]
        public IActionResult GetCarsByMake(string make)
        {
            var cars = this.carService.GetCarsByMake(make);

            return View(cars);
        }

        [Route("parts", Order = 1)]
        public IActionResult GetCarsWithParts()
        {
            return View(this.carService.GetCarsWithParts());
        }
    }
}
