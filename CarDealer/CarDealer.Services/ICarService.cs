namespace CarDealer.Services
{
    using CarDealer.Services.Models.Cars;
    using System;
    using System.Collections.Generic;

    public interface ICarService
    {
        IEnumerable<CarModel> GetCarsByMake(string make);
        IEnumerable<CarWithPartsModel> GetCarsWithParts();
    }
}
