namespace CarDealer.Services
{
    public interface IPartService
    {
        void Add(string name, decimal price, int supplierId, int quantity);
    }
}
