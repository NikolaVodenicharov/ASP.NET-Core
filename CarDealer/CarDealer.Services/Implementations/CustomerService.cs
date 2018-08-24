namespace CarDealer.Services.Implementations
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using CarDealer.Data;
    using CarDealer.Services.Enums;
    using CarDealer.Services.Models;
    using CarDealer.Services.Models.Sales;
    using CarDealer.Services.Models.Customers;
    using CarDealer.Data.Models;

    public class CustomerService : ICustomerService
    {
        private readonly CarDealerDbContext db;

        public CustomerService(CarDealerDbContext db)
        {
            this.db = db;
        }


        public void Add(string name, DateTime birthDate, bool isYoungDriver)
        {
            var customer = new Customer
            {
                Name = name,
                BirthDate = birthDate,
                IsYoungDriver = isYoungDriver
            };

            db.Customers.Add(customer);
            db.SaveChanges();
        }

        public CustomerModel GetById(int id)
        {
            return db
                .Customers
                .Where(c => c.Id == id)
                .Select(c => new CustomerModel
                {
                    Id = c.Id,
                    Name = c.Name,
                    BirthDate = c.BirthDate,
                    IsYoungDriver = c.IsYoungDriver
                })
                .FirstOrDefault();
        }

        public void Edit(int id, string name, DateTime birthDate, bool isYoungDriver)
        {
            var customer = db
                .Customers
                .Where(c => c.Id == id)
                .FirstOrDefault();

            if (customer == null)
            {
                return;
            }

            customer.Name = name;
            customer.BirthDate = birthDate;
            customer.IsYoungDriver = isYoungDriver;

            db.SaveChanges();
        }

        public bool IsExisting(int id)
        {
            return db
                .Customers
                .Any(c => c.Id == id);
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
                    Id = c.Id,
                    Name = c.Name,
                    BirthDate = c.BirthDate,
                    IsYoungDriver = c.IsYoungDriver
                });
        }

        public CustomerBoughtCarsModel CustomerCarPurchases(int id)
        {
            CustomerModel customerModel = db
                .Customers
                .Where(c => c.Id == id)
                .Select(c => new CustomerModel
                {
                    Id = c.Id,
                    Name = c.Name,
                    IsYoungDriver = c.IsYoungDriver,
                    BirthDate = c.BirthDate
                })
                .FirstOrDefault();

            IEnumerable<SaleModel> sales = db
                .Customers
                .Where(c => c.Id == id)
                .Select(cu => cu
                    .Sales
                    .Select(s => new SaleModel
                    {
                        CarPrice = s.Car.PartCars.Select(ca => ca.Part.Price).Sum(),
                        DiscountPercentage = s.Discount
                    }))
                .FirstOrDefault();

            var carsCount = sales.Count();

            decimal carsTotalPrice = 0;
            foreach (SaleModel sale in sales)
            {
                var carPrice = sale.CarPrice * (1 - (decimal)sale.DiscountPercentage - (customerModel.IsYoungDriver ? 0.05m : 0));
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
