namespace CarDealer.Services.Models.Sales
{
    using CarDealer.Services.Models.Cars;

    public class SaleCarDetailsModel : SaleDetailsModel
    {
        public CarModel CarModel { get; set; }
    }
}
