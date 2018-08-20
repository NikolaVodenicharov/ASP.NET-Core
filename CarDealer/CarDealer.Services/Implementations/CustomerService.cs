namespace CarDealer.Services.Implementations
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using CarDealer.Data;
    using CarDealer.Services.Enums;
    using CarDealer.Services.Models;
    using CarDealer.Services.Models.Cars;
    using CarDealer.Services.Models.Customers;

    public class CustomerService : ICustomerService
    {
        private readonly CarDealerDbContext db;

        public CustomerService(CarDealerDbContext db)
        {
            this.db = db;
        }

        public IEnumerable<CustomerModel> OrderedCustomers(OrderDirection order)
        {
            var customersQuery = this.db.Customers.AsQueryable();

            switch (order)
            {
                case OrderDirection.Ascending:
                    customersQuery = customersQuery
                        .OrderBy(c => c.BirthDate)
                        .ThenBy(c => c.IsYoungDriver);
                    break;
                case OrderDirection.Descending:
                    customersQuery = customersQuery
                        .OrderByDescending(c => c.BirthDate)
                        .ThenBy(c => c.IsYoungDriver);                
                        break;
                default:
                    throw new InvalidOperationException($"Invalid order direction - {order}.");
            }

            return customersQuery
                .Select(c => new CustomerModel
                {
                    Name = c.Name,
                    BirthDate = c.BirthDate,
                    IsYoungDriver = c.IsYoungDriver
                });
        }


        public CustomerBoughtCarsModel CustomerCarPurchases(int id)
        {
            var customerModel = db
                .Customers
                .Where(c => c.Id == id)
                .Select(c => new CustomerModel
                {
                    Name = c.Name,
                    IsYoungDriver = c.IsYoungDriver,
                    BirthDate = c.BirthDate
                })
                .FirstOrDefault();
                
            var carsPartsPriceAndDiscounts = db
                .Customers
                .Where(c => c.Id == id)
                .Select(cu => cu
                    .Sales
                    .Select(s => new CarPriceAndDiscount
                    {
                        CarPrice = s.Car.PartCars.Select(ca => ca.Part.Price).Sum(),
                        DiscountPercentage = s.Discount
                    }))
                .FirstOrDefault();

            var carsCount = carsPartsPriceAndDiscounts.Count();

            decimal carsTotalPrice = 0;
            foreach (var car in carsPartsPriceAndDiscounts)
            {
                var carPrice = car.CarPrice * (1 - (decimal)car.DiscountPercentage - (customerModel.IsYoungDriver ? 0.05m : 0));
                carsTotalPrice += carPrice;
            }

            var result = new CustomerBoughtCarsModel
            {
                Name = customerModel.Name,
                CarsCount = carsCount,
                TotalSpendMoneyOnCars = carsTotalPrice
            };

            return result;
        }
    }
}
