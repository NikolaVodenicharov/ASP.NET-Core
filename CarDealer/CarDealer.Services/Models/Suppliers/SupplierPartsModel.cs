namespace CarDealer.Services.Models.Suppliers
{
    using CarDealer.Services.Models.Parts;
    using System.Collections.Generic;

    public class SupplierPartsModel
    {
        public SupplierModel Suppler { get; set; }

        public IEnumerable<PartModel> Parts { get; set; }
    }
}
