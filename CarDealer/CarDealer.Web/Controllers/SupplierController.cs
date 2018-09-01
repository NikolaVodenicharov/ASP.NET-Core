namespace CarDealer.Web.Controllers
{
    using CarDealer.Services;
    using CarDealer.Services.Enums;
    using CarDealer.Web.Models.Suppliers;
    using Microsoft.AspNetCore.Mvc;
    using System;

    public class SupplierController : Controller
    {
        private readonly ISupplierService supplierService;

        public SupplierController(ISupplierService supplierService)
        {
            this.supplierService = supplierService;
        }

        public IActionResult GetSuppliers(string supplierType)
        {
            var supplierTypeEnum = SupplierType.Local;

            if (supplierType != null)
            {
                Enum.TryParse(supplierType, true, out supplierTypeEnum);
            }

            var supplierViewModel = new SuppliersViewModel
            {
                SupplierType = supplierTypeEnum,
                Suppliers = this.supplierService.GetSuppliersByType(supplierTypeEnum)
            };

            return View(supplierViewModel);
        }
    }
}
