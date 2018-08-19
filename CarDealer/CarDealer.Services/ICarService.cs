namespace CarDealer.Services
{
    using CarDealer.Services.Models;
    using System;
    using System.Collections.Generic;

    public interface ICarService
    {
        IEnumerable<CarModel> GetCarsByMake(string make);
    }
}
