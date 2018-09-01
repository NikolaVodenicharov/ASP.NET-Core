namespace CarDealer.Services.Implementations
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using CarDealer.Data;
    using CarDealer.Services.Enums;
    using CarDealer.Services.Models;
    using CarDealer.Services.Models.Suppliers;
    using Microsoft.AspNetCore.Mvc.Rendering;

    public class SupplierService : ISupplierService
    {
        private readonly CarDealerDbContext db;

        public SupplierService (CarDealerDbContext db)
        {
            this.db = db;
        }

        public SupplierModel GetById(int id)
        {
            return this
                .QueryAll()
                .Where(s => s.Id == id)
                .FirstOrDefault();
        }

        public IEnumerable<SupplierModel> GetAllSuppliers()
        {
            return this
                .QueryAll()
                .ToList();
        }

        public IEnumerable<SelectListItem> GetSelectedList()
        {
            return this
                .QueryAll()
                .Select(s => new SelectListItem
                {
                    Text = s.Name,
                    Value = s.Id.ToString()
                });
        }

        public IEnumerable<SupplierPartsCountModel> GetSuppliersByType(SupplierType supplierType)
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

        private IQueryable<SupplierModel> QueryAll()
        {
            return db
                .Suppliers
                .Select(s => new SupplierModel
                {
                    Id = s.Id,
                    Name = s.Name,
                    SupplierType = s.IsImporter ? SupplierType.Importer : SupplierType.Local
                })
                .OrderBy(sm => sm.Id);
        }
    }
}
