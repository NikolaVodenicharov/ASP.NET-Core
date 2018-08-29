﻿namespace CarDealer.Services.Implementations
{
    using CarDealer.Data;
    using CarDealer.Data.Models;
    using CarDealer.Services.Models.Parts;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class PartService : IPartService
    {
        private readonly CarDealerDbContext db;

        public PartService(CarDealerDbContext db)
        {
            this.db = db;
        }

        public void Add(string name, decimal price, int supplierId, int quantity = 1)
        {
            Part part = new Part
            {
                Name = name,
                Price = price,
                Quantity = quantity,
                SupplierId = supplierId
            };

            db.Parts.Add(part);
            db.SaveChanges();
        }
        public void Edit(int id, string name, decimal price, int quantity)
        {
            Part part = this.GetPart(id);

            part.Name = name;
            part.Price = price;
            part.Quantity = quantity;

            db.SaveChanges();
        }
        public void Delete(int id)
        {
            Part part = this.GetPart(id);
            if (part == null)
            {
                // not exist msg ?
                return;
            }

            List<PartCar> partCars = db
                .PartCars
                .Where(pc => pc.PartId == id)
                .ToList();
            // if partCars is empty ?

            db.PartCars.RemoveRange(partCars);
            db.Parts.Remove(part);
            db.SaveChanges();
        }
        private Part GetPart(int id)
        {
            return db
                .Parts
                .Where(p => p.Id == id)
                .FirstOrDefault();
        }

        public IEnumerable<PartModel> GetAllBySupplier(int supplierId)
        {
            return db
                .Parts
                .Where(p => p.SupplierId == supplierId)
                .Select(p => new PartModel
                {
                    Id = p.Id,
                    Name = p.Name,
                    Price = p.Price,
                    Quantity = p.Quantity
                })
                .ToList();
        }
        public IEnumerable<PartModel> GetAll()
        {
            return this
                .PartsQuery()
                .ToList();
        }
        public PartModel GetById(int id)
        {
            return this
                .PartsQuery()
                .Where(p => p.Id == id)
                .FirstOrDefault();
        }
        private IQueryable<PartModel> PartsQuery()
        {
            return db
                .Parts
                .Select(p => new PartModel
                {
                    Id = p.Id,
                    Name = p.Name,
                    Price = p.Price,
                    Quantity = p.Quantity
                });
        }
    }
}