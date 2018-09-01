namespace CarDealer.Web.Models.Parts
{
    using CarDealer.Data;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class AddPartFormModel
    {
        [Required]
        [MinLength(2)]
        [MaxLength(DbConstants.StringMaxLength)]
        public string Name { get; set; }

        [Range(0.00001, double.MaxValue, ErrorMessage = "Price must be positive number")]
        public decimal Price { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Quantity must be atleast 1")]
        public int Quantity { get; set; }

        [Range(1, int.MaxValue)]
        [Display(Name = "Supplier")]
        public int SupplierId { get; set; }

        public IEnumerable<SelectListItem> Suppliers { get; set; }
    }
}
