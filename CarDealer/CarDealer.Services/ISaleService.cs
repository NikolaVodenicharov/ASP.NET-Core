namespace CarDealer.Services
{
    using CarDealer.Services.Models.Sales;
    using System.Collections.Generic;

    public interface ISaleService
    {
        IEnumerable<SaleDetailsModel> All();

        SaleCarDetailsModel Details(int id);
    }
}
