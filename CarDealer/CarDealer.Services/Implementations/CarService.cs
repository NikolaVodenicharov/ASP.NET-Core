namespace CarDealer.Services.Implementations
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
                    Make = c.Make,
                    Model = c.Model,
                    TravelledDistance = c.TravelledDistance
                });
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
