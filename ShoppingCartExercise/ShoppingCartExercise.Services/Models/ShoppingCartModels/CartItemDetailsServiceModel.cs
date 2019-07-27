using System;
using System.Collections.Generic;
using System.Text;

namespace ShoppingCartExercise.Services.Models.ShoppingCartModels
{
    public class CartItemDetailsServiceModel : CartItemSummaryServiceModel
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
    }
}
