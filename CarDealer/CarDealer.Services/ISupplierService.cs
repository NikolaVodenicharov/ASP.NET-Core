namespace CarDealer.Services
{
    using CarDealer.Services.Enums;
    using CarDealer.Services.Models;
    using System.Collections.Generic;

    public interface ISupplierService
    {
        IEnumerable<SupplierModel> GetSuppliers(SupplierType supplierType);
    }
}
