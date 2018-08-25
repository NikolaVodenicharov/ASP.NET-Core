namespace CarDealer.Web.Controllers
{
    using CarDealer.Services;
    using CarDealer.Web.Models.Parts;
    using Microsoft.AspNetCore.Mvc;
    using System;

    [Route("Part")]
    public class PartController : Controller
    {
        private readonly IPartService partService;

        public PartController(IPartService partService)
        {
            this.partService = partService;
        }

        [Route(nameof(Add) + "/{id}")]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        [Route(nameof(Add) +"/{id}")]
        public IActionResult Add(int id, PartFormModel model)
        {
            this.partService.Add(
                model.Name,
                model.Price,
                id,
                model.Quantity);

            return Redirect("/home");
        }
    }
}
