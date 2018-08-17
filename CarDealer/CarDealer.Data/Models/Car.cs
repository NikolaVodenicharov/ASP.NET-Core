namespace CarDealer.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Car
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(ModelConstants.StringMaxLength)]
        public string Make { get; set; }

        [Required]
        [MaxLength(ModelConstants.StringMaxLength)]
        public string Model { get; set; }

        [Required]
        [Range(0, long.MaxValue)]
        public long TravelledDistance { get; set; }

        public List<Sale> Sales { get; set; } = new List<Sale>();
        public List<PartCar> PartCars { get; set; } = new List<PartCar>();
    }
}
