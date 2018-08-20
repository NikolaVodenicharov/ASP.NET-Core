namespace CarDealer.Services.Models.Sales
{
    using CarDealer.Data.Models;
    using System.Collections.Generic;
    using System.Linq;

    public class CarSalePrice
    {
        public bool IsYoungDriver { get; set; }

        public decimal DiscountPercentage { get; set; }

        public IEnumerable<Part> Parts { get; set; }
        private decimal TotalPartsPrice => this.Parts.Sum(p => p.Price);

        public decimal CarPrice => 
            this.TotalPartsPrice * (1 - this.DiscountPercentage - (this.IsYoungDriver ? 0.05m : 0m));
    }
}
