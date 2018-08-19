namespace CarDealer.Web.Models.Suppliers
{
    using CarDealer.Services.Enums;
    using CarDealer.Services.Models;
    using System.Collections;
    using System.Collections.Generic;

    public class SuppliersViewModel
    {
        public SupplierType SupplierType { get; set; }
        public IEnumerable<SupplierModel> Suppliers { get; set; }

    }
}
