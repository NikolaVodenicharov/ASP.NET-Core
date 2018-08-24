namespace CarDealer.Services
{
    using CarDealer.Services.Enums;
    using Models;
    using Models.Customers;
    using System;
    using System.Collections.Generic;

    public interface ICustomerService
    {
        void Add(string name, DateTime birthDate, bool isYoungDriver);

        void Edit(int id, string name, DateTime birthDate, bool isYoungDriver);

        CustomerModel GetById(int id);

        bool IsExisting(int id);

        IEnumerable<CustomerModel> OrderedCustomers(OrderDirection order);

        CustomerBoughtCarsModel CustomerCarPurchases(int id);

    }
}
