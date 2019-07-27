using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShoppingCartExercise.Data.Models
{
    public class User : IdentityUser
    {
        public List<Order> Orders { get; set; } = new List<Order>();
    }
}
