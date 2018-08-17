namespace CarDealer.Web.Controllers
{
    using CarDealer.Services;
    using CarDealer.Services.Models;
    using Microsoft.AspNetCore.Mvc;
    using System;

    public class CustomerController : Controller
    {
        private readonly ICustomerService customerService;

        public CustomerController(ICustomerService customerService)
        {
            this.customerService = customerService;
        }

        public IActionResult All(string order)
        {
            var orderDirection = OrderDirection.Ascending;
            Enum.TryParse(order, true, out orderDirection);

            var customers = this.customerService.OrderedCustomers(orderDirection);

            return View(customers);
        }
    }
}
