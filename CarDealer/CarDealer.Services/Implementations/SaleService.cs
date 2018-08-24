namespace CarDealer.Services.Implementations
{
    using CarDealer.Data;
    using CarDealer.Services.Models.Cars;
    using CarDealer.Services.Models.Customers;
    using CarDealer.Services.Models.Sales;
    using System.Collections.Generic;
    using System.Linq;

    public class SaleService : ISaleService
    {
        private readonly CarDealerDbContext db;

        public SaleService(CarDealerDbContext db)
        {
            this.db = db;
        }

        public IEnumerable<SaleDetailsModel> All ()
        {
            return db
                .Sales
                .Select(s => new SaleDetailsModel
                {
                    Id = s.Id,
                    DiscountPercentage = s.Discount,
                    CustomerName = s.Customer.Name,
                    IsYoungDriver = s.Customer.IsYoungDriver,
                    CarPrice = s.Car.PartCars.Select(pc => pc.Part.Price).Sum()
                });
        }

        public SaleCarDetailsModel Details (int id)
        {
            return db
                .Sales
                .Where(s => s.Id == id)
                .Select(s => new SaleCarDetailsModel
                {
                    DiscountPercentage = s.Discount,
                    CustomerName = s.Customer.Name,
                    IsYoungDriver = s.Customer.IsYoungDriver,
                    CarPrice = s.Car.PartCars.Select(pc => pc.Part.Price).Sum(),
                    CarModel = new CarModel
                    {
                        Make = s.Car.Make,
                        Model = s.Car.Model,
                        TravelledDistance = s.Car.TravelledDistance
                    }
                })
                .FirstOrDefault();
        }
    }
}
