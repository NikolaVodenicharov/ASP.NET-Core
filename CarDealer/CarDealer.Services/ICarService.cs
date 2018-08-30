namespace CarDealer.Services
{
    using CarDealer.Services.Models.Cars;
    using System;
    using System.Collections.Generic;

    public interface ICarService
    {
        void Add(string make, string model, long travelledDistance);

        PagedCarsModel PagedCars(
            int currentPage = ServicesConstants.DefaultCurrentPage, 
            int pageSize = ServicesConstants.DefaultPageSize);

        IEnumerable<CarModel> GetCarsByMake(string make);

        CarWithPartsModel CarDetails(int id);

        IEnumerable<CarWithPartsModel> CarsDetails();
    }
}
