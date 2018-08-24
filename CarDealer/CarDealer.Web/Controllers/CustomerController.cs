namespace CarDealer.Web.Controllers
{
    using CarDealer.Services;
    using CarDealer.Services.Enums;
    using CarDealer.Web.Models.Customer;
    using Microsoft.AspNetCore.Mvc;
    using System;

    [Route("customer")]
    public class CustomerController : Controller
    {
        private readonly ICustomerService customerService;

        public CustomerController(ICustomerService customerService)
        {
            this.customerService = customerService;
        }

        [Route(nameof(Add))]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        [Route(nameof(Add))]
        public IActionResult Add(CustomerFormModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            this.customerService.Add(
                model.Name,
                model.BirthDate,
                model.IsYoungDriver);

            return RedirectToAction(nameof(All));
        }

        [Route(nameof(Edit) + "/{id}")]
        public IActionResult Edit(int id)
        {
            var customerModel = this.customerService.GetById(id);

            if (customerModel == null)
            {
                return NotFound();
            }

            var customerFormModel = new CustomerFormModel
            {
                Name = customerModel.Name,
                BirthDate = customerModel.BirthDate,
                IsYoungDriver = customerModel.IsYoungDriver
            };

            return View(customerFormModel);
        }

        [HttpPost]
        [Route(nameof(Edit) + "/{id}")]
        public IActionResult Edit(int id, CustomerFormModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            if (!this.customerService.IsExisting(id))
            {
                return NotFound();
            }

            this.customerService.Edit(
                id,
                model.Name,
                model.BirthDate,
                model.IsYoungDriver);

            return RedirectToAction(nameof(All));
        }

        [Route("all")]
        public IActionResult All(string order)
        {
            var orderDirection = OrderDirection.Ascending;

            if (order != null)
            {
                Enum.TryParse(order, true, out orderDirection);
            }

            var customers = this.customerService.OrderedCustomers(orderDirection);

            return View(customers);
        }

        [Route("{id}")]
        public IActionResult CustomerCarPurchases (int id)
        {
            return View(this.customerService.CustomerCarPurchases(id));
        }
    }
}
