using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ShoppingCartExercise.Data.Models
{
    public class OrderItem
    {
        public int Id { get; set; }

        public int ShoeId { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal ShoePrice { get; set; }

        public int Quantity { get; set; }

        public int OrderId { get; set; }
        public Order Order { get; set; }
    }
}
