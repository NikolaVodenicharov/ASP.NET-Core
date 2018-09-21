namespace CameraBazaar.Web.Models.Users
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class UserFormModel
    {
        [Required]
        [MinLength(4)]
        [MaxLength(20)]
        public string Username { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [MinLength(3)]
        [RegularExpression("[a-z0-9]+")]
        public string Password { get; set; }

        [Required]
        [RegularExpression("([+][0-9]{10,12})")]
        public string PhoneNumber { get; set; }
    }
}
