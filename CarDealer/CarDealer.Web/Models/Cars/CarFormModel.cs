namespace CarDealer.Web.Models.Cars
{
    using CarDealer.Data;
    using CarDealer.Services.Models.Parts;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class CarFormModel
    {
        [Required]
        [MaxLength(DbConstants.StringMaxLength)]
        public string Make { get; set; }

        [Required]
        [MaxLength(DbConstants.StringMaxLength)]
        public string Model { get; set; }

        [Required]
        [Range(DbConstants.CarConstants.MinTravelledDistance, DbConstants.CarConstants.MaxTravelledDistance)]
        public long TravelledDistance { get; set; }

        [Display(Name = "Parts")]
        public IEnumerable<int> SelectedPartsIds { get; set; }
        public IEnumerable<SelectListItem> OptionalParts { get; set; }
    }
}
