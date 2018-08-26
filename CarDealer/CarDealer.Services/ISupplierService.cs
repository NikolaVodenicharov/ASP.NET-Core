namespace CarDealer.Services
{
    using CarDealer.Services.Enums;
    using CarDealer.Services.Models;
    using CarDealer.Services.Models.Suppliers;
    using System.Collections.Generic;

    public interface ISupplierService
    {
        IEnumerable<SupplierPartsCountModel> GetSuppliers(SupplierType supplierType);
        SupplierModel GetById(int id);
    }
}
