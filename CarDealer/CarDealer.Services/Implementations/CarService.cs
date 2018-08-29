﻿namespace CarDealer.Services.Implementations
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using CarDealer.Data;
    using CarDealer.Data.Models;
    using CarDealer.Services.Models.Cars;
    using CarDealer.Services.Models.Parts;

    public class CarService : ICarService
    {
        private readonly CarDealerDbContext db;

        public CarService(CarDealerDbContext db)
        {
            this.db = db;
        }

        public void Add(string make, string model, long travelledDistance)
        {
            Car car = new Car
            {
                Make = make,
                Model = model,
                TravelledDistance = travelledDistance
            };

            db.Cars.Add(car);
            db.SaveChanges();
        }

        public IEnumerable<CarModel> GetCarsByMake(string make)
        {
            return db
                .Cars
                .Where(c => c.Make.Equals(make, StringComparison.InvariantCultureIgnoreCase))
                .OrderBy(c => c.Model)
                .ThenByDescending(c => c.TravelledDistance)
                .Select(c => new CarModel
                {
                    Id = c.Id,
                    Make = c.Make,
                    Model = c.Model,
                    TravelledDistance = c.TravelledDistance
                });
        }

        public PagedCarsModel PagedCars(
            int currentPage = ServicesConstants.CarServiceConstants.DefaultCurrentPage, 
            int pageSize = ServicesConstants.CarServiceConstants.DefaultPageSize)
        {
            if (currentPage < ServicesConstants.CarServiceConstants.DefaultMinCurrentPage)
            {
                currentPage = ServicesConstants.CarServiceConstants.DefaultCurrentPage;
            }
            if (pageSize < ServicesConstants.CarServiceConstants.DefaultMinPageSize)
            {
                pageSize = ServicesConstants.CarServiceConstants.DefaultPageSize;
            }

            int skipCarsNumber = pageSize * (currentPage - 1);

            var model = new PagedCarsModel
            {
                CurrentPage = currentPage,
                TotalPage = (int)Math.Ceiling(this.CarsCount() / (double)pageSize),
                Cars = this
                    .CarsQuery()
                    .Skip(skipCarsNumber)
                    .Take(pageSize)
                    .ToList()
            };

            return model;
        }
        private int CarsCount()
        {
            return db
                .Cars
                .Count();
        }
        private IQueryable<CarModel> CarsQuery()
        {
            return db
                .Cars
                .Select(c => new CarModel
                {
                    Id = c.Id,
                    Make = c.Make,
                    Model = c.Model,
                    TravelledDistance = c.TravelledDistance
                })
                .OrderBy(c => c.Make)
                .ThenBy(c => c.Model)
                .ThenBy(c => c.TravelledDistance);
        }

        public IEnumerable<CarWithPartsModel> GetCarsWithParts()
        {
            return db
                .Cars
                .Select(c => new CarWithPartsModel
                {
                    Make = c.Make,
                    Model = c.Model,
                    TravelledDistance = c.TravelledDistance,
                    Parts = c.PartCars.Select(p => new PartPriceModel
                    {
                        Name = p.Part.Name,
                        Price = p.Part.Price
                    })
                    .ToList()
                })
                .ToList();
        }
    }
}
