namespace CarDealer.Web.Controllers
{
    using CarDealer.Services;
    using CarDealer.Web.Models.Cars;
    using Microsoft.AspNetCore.Mvc;

    [Route("car")]
    public class CarController : Controller
    {
        private readonly ICarService carService;

        public CarController(ICarService carService)
        {
            this.carService = carService;
        }

        [Route(nameof(Add))]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        [Route(nameof(Add))]
        public IActionResult Add(CarFormModel carFormModel)
        {
            if (!ModelState.IsValid)
            {
                return View(carFormModel);
            }

            this.carService.Add(
                carFormModel.Make,
                carFormModel.Model,
                carFormModel.TravelledDistance);

            return RedirectToAction(nameof(CarsByMake), "opel");

        }

        [Route(nameof(CarsByMake) + "/{make}")]
        public IActionResult CarsByMake(string make)
        {
            var cars = this.carService.GetCarsByMake(make);

            return View(cars);
        }

        [Route(nameof(CarsWithParts))]
        public IActionResult CarsWithParts()
        {
            return View(this.carService.GetCarsWithParts());
        }
    }
}
