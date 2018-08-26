namespace CarDealer.Services.Models.Suppliers
{
    using CarDealer.Services.Enums;

    public class SupplierModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public SupplierType SupplierType { get; set; }
    }
}
