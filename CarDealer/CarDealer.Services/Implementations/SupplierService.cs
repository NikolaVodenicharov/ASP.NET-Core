namespace CarDealer.Services.Implementations
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using CarDealer.Data;
    using CarDealer.Services.Enums;
    using CarDealer.Services.Models;
    using CarDealer.Services.Models.Suppliers;

    public class SupplierService : ISupplierService
    {
        private readonly CarDealerDbContext db;

        public SupplierService (CarDealerDbContext db)
        {
            this.db = db;
        }

        public SupplierModel GetById(int id)
        {
            return db
                .Suppliers
                .Where(s => s.Id == id)
                .Select(s => new SupplierModel
                {
                    Id = s.Id,
                    Name = s.Name,
                    SupplierType = (s.IsImporter ? SupplierType.Importer : SupplierType.Local)
                })
                .FirstOrDefault();
        }

        public IEnumerable<SupplierPartsCountModel> GetSuppliers(SupplierType supplierType)
        {
            var isImporter = supplierType == SupplierType.Importer;

            return db
                .Suppliers
                .Where(s => s.IsImporter == isImporter)
                .Select(s => new SupplierPartsCountModel
                {
                    Id = s.Id,
                    Name = s.Name,
                    PartsCount = s.Parts.Count()
                })
                .ToList();
        }
    }
}
