namespace CarDealer.Services
{
    using CarDealer.Services.Enums;
    using CarDealer.Services.Models;
    using CarDealer.Services.Models.Suppliers;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using System.Collections.Generic;

    public interface ISupplierService
    {
        IEnumerable<SupplierModel> GetAllSuppliers();

        IEnumerable<SelectListItem> GetSelectedList();

        IEnumerable<SupplierPartsCountModel> GetSuppliersByType(SupplierType supplierType);

        SupplierModel GetById(int id);
    }
}
