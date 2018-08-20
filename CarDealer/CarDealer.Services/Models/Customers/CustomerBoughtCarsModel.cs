namespace CarDealer.Services.Models.Customers
{
    using System.Collections.Generic;

    public class CustomerBoughtCarsModel
    {
        public string Name { get; set; }
        public int CarsCount { get; set; }
        public decimal TotalSpendMoneyOnCars { get; set; }
    }
}
