using ShoppingCartExercise.Services.Models.ShoppingCartModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShoppingCartExercise.Services.Interfaces
{
    public interface ICartManager
    {
        void AddToCart(string cartId, int productId);

        void Remove(string cartId, int productId);

        CartServiceModel GetCart(string cartId);

        IEnumerable<CartItemSummaryServiceModel> AllItemsSummary(string cartId);
    }
}
