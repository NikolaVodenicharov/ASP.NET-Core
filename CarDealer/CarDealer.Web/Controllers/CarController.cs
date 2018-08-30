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

            return RedirectToAction(nameof(CarsWithParts));

        }

        [Route(nameof(PagedCars) + "/{currentPage}")]
        public IActionResult PagedCars(int currentPage = 1)
        {
            return View(this.carService.PagedCars(currentPage));
        }

        [Route(nameof(CarsByMake) + "/{make}")]
        public IActionResult CarsByMake(string make)
        {
            var cars = this.carService.GetCarsByMake(make);

            return View(cars);
        }

        [Route(nameof(CarDetails) +"/{id}")]
        public IActionResult CarDetails(int id)
        {
            return View(this.carService.CarDetails(id));
        }

        [Route(nameof(CarsWithParts))]
        public IActionResult CarsWithParts()
        {
            return View(this.carService.CarsDetails());
        }
    }
}
