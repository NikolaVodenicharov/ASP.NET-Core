namespace CarDealer.Services
{
    using Models;
    using System.Collections.Generic;

    public interface ICustomerService
    {
        IEnumerable<CustomerModel> OrderedCustomers(OrderDirection order);
    }
}
