namespace CarDealer.Services.Models.Cars
{
    using CarDealer.Services.Models.Parts;
    using System.Collections.Generic;

    public class CarWithPartsModel : CarModel
    {
        public IEnumerable<PartPriceModel> Parts { get; set; }
    }
}
