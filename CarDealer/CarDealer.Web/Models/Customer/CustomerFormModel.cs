namespace CarDealer.Web.Models.Customer
{
    using CarDealer.Data;
    using System;
    using System.ComponentModel.DataAnnotations;

    public class CustomerFormModel
    {
        [Required]
        [MaxLength(DbConstants.StringMaxLength)]
        public string Name { get; set; }

        [DataType(DataType.Date)]
        public DateTime BirthDate { get; set; }

        [Display(Name = "Young driver")]
        public bool IsYoungDriver { get; set; }
    }
}
