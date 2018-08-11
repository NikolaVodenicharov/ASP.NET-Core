namespace AspNetCoreIntro.Data.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class Cat
    {
        private const int StringMaxLength = 50;

        public int Id { get; private set; }

        [Required]
        [MaxLength(StringMaxLength)]
        public string Name { get; set; }

        [Required]
        [Range(1, 30)]
        public int Age { get; set; }

        [Required]
        [MaxLength(StringMaxLength)]
        public string Breed { get; set; }

        [Required]
        [MaxLength(2000)]
        public string ImageUrl { get; set; }
    }
}
