﻿namespace CarDealer.Services
{
    using CarDealer.Services.Enums;
    using Models;
    using System.Collections.Generic;

    public interface ICustomerService
    {
        IEnumerable<CustomerModel> OrderedCustomers(OrderDirection order);
    }
}
