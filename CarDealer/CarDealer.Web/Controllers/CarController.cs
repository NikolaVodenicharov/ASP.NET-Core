namespace CarDealer.Web.Controllers
{
    using CarDealer.Services;
    using CarDealer.Web.Models.Cars;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using System.Collections.Generic;
    using System.Linq;

    [Route("car")]
    public class CarController : Controller
    {
        private readonly ICarService carService;
        private readonly IPartService partService;

        public CarController(ICarService carService, IPartService partService)
        {
            this.carService = carService;
            this.partService = partService;
        }

        [Route(nameof(Add))]
        public IActionResult Add()
        {
            var model = new CarFormModel
            {
                OptionalParts = this.PartsIdNames()
            };

            return View(model);
        }

        [HttpPost]
        [Route(nameof(Add))]
        public IActionResult Add(CarFormModel carFormModel)
        {
            if (!ModelState.IsValid)
            {
                carFormModel.OptionalParts = this.PartsIdNames();
                return View(carFormModel);
            }

            this.carService.Add(
                carFormModel.Make,
                carFormModel.Model,
                carFormModel.TravelledDistance,
                carFormModel.SelectedPartsIds);

            return RedirectToAction(nameof(CarsWithParts));

        }
        private IEnumerable<SelectListItem> PartsIdNames()
        {
            return this
                    .partService
                    .GetAllIdNames()
                    .Select(p => new SelectListItem
                    {
                        Text = p.Name,
                        Value = p.Id.ToString()
                    });
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
