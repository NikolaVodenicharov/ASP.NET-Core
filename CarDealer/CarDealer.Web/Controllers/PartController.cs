namespace CarDealer.Web.Controllers
{
    using CarDealer.Services;
    using CarDealer.Services.Models.Parts;
    using CarDealer.Services.Models.Suppliers;
    using CarDealer.Web.Models.Parts;
    using Microsoft.AspNetCore.Mvc;
    using System;
    using System.Collections;

    [Route("Part")]
    public class PartController : Controller
    {
        private readonly IPartService partService;
        private readonly ISupplierService supplierService;

        public PartController(IPartService partService, ISupplierService supplierService)
        {
            this.partService = partService;
            this.supplierService = supplierService;
        }

        [Route(nameof(Add) + "/{supplierId}")]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        [Route(nameof(Add) + "/{supplierId}")]
        public IActionResult Add(int supplierId, PartFormModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            this.partService.Add(
                model.Name,
                model.Price,
                supplierId,
                model.Quantity);

            return RedirectToAction(nameof(GetAllBySupplier), supplierId);
        }

        [Route(nameof(Edit) + "/{supplierId}/{id}")]
        public IActionResult Edit(int supplierId, int id)
        {
            PartModel partModel = this.partService.GetById(id);

            PartFormModel partFormModel = new PartFormModel
            {
                Name = partModel.Name,
                Price = partModel.Price,
                Quantity = partModel.Quantity
            };

            return View(partFormModel);
        }

        [HttpPost]
        [Route(nameof(Edit) + "/{supplierId}/{id}")]
        public IActionResult Edit(int supplierId, int id, PartFormModel model)
        {
            this.partService.Edit(
                id,
                model.Name,
                model.Price,
                model.Quantity);

            return RedirectToAction(nameof(GetAllBySupplier), supplierId);
        }

        [Route(nameof(Delete) + "/{supplierId}/{id}")]
        public IActionResult Delete(int id, int supplierId)
        {
            this.partService.Delete(id);

            return RedirectToAction(nameof(GetAllBySupplier), supplierId);
        }

        [Route(nameof(GetAllBySupplier) + "/{supplierId}")] 
        public IActionResult GetAllBySupplier(int supplierId)
        {
            var supplierPartsModel = new SupplierPartsModel
            {
                Suppler = this.supplierService.GetById(supplierId),
                Parts = this.partService.GetAllBySupplier(supplierId)
            };

            return View(supplierPartsModel);
        }

        [Route(nameof(GetAll))]
        public IActionResult GetAll()
        {
            return View(this.partService.GetAll());
        }
    }
}
