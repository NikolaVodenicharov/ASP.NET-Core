using System;
using System.Collections.Generic;
using System.Text;

namespace ShoppingCartExercise.Data.Models
{
    public class Order
    {
        public int Id { get; set; }

        public string UserId { get; set; }
        public User User { get; set; }

        public IList<OrderItem> Items { get; set; } = new List<OrderItem>();

    }
}
