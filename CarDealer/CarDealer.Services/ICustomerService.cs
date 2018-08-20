namespace CarDealer.Services
{
    using CarDealer.Services.Enums;
    using Models;
    using Models.Customers;
    using System.Collections.Generic;

    public interface ICustomerService
    {
        IEnumerable<CustomerModel> OrderedCustomers(OrderDirection order);

        CustomerBoughtCarsModel CustomerCarPurchases(int id);
    }
}
