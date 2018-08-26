namespace CarDealer.Services
{
    using System.Collections.Generic;
    using CarDealer.Services.Models.Parts;

    public interface IPartService
    {
        void Add(string name, decimal price, int supplierId, int quantity);
        void Edit(int id, string name, decimal price, int quantity);
        void Delete(int id);

        IEnumerable<PartModel> GetAllBySupplier(int supplierId);
        IEnumerable<PartModel> GetAll();
        PartModel GetById(int id);
    }
}
