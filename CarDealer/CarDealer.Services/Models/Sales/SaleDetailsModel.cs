namespace CarDealer.Services.Models.Sales
{
    using CarDealer.Services.Models.Cars;

    public class SaleDetailsModel
    {
        public double Discount { get; set; }

        public string CustomerName { get; set; }

        public bool IsYoungDriver { get; set; }

        public CarModel CarModel { get; set; }

        public decimal CarPrice { get; set; }

        public decimal DiscountedCarPrice
            => this.CarPrice * (1 - (decimal)this.Discount - (this.IsYoungDriver ? 0.05m : 0));
    }
}
