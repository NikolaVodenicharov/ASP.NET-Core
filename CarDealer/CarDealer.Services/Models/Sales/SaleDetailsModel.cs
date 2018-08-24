namespace CarDealer.Services.Models.Sales
{
    using CarDealer.Services.Models.Cars;

    public class SaleDetailsModel : SaleModel
    {
        public int Id { get; set; }

        public string CustomerName { get; set; }

        public bool IsYoungDriver { get; set; }

        public double TotalDiscountPercentage 
            => this.DiscountPercentage + (this.IsYoungDriver ? 0.05 : 0);

        public decimal DiscountedCarPrice
            => this.CarPrice * (1 - (decimal)this.TotalDiscountPercentage);
    }
}
