﻿namespace CarDealer.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Car
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(DbConstants.StringMaxLength)]
        public string Make { get; set; }

        [Required]
        [MaxLength(DbConstants.StringMaxLength)]
        public string Model { get; set; }

        [Required]
        [Range(DbConstants.CarConstants.MinTravelledDistance, DbConstants.CarConstants.MaxTravelledDistance)]
        public long TravelledDistance { get; set; }

        public List<Sale> Sales { get; set; } = new List<Sale>();
        public List<PartCar> PartCars { get; set; } = new List<PartCar>();
    }
}
