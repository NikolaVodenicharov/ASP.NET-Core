using ShoppingCartExercise.Data;
using ShoppingCartExercise.Services.Interfaces;
using ShoppingCartExercise.Services.Models.ShoppingCartModels;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;

namespace ShoppingCartExercise.Services.Implementations
{
    public class CartManager : ICartManager
    {
        private readonly ConcurrentDictionary<string, CartServiceModel> carts;

        public CartManager()
        {
            this.carts = new ConcurrentDictionary<string, CartServiceModel>();
        }

        public void AddToCart(string cartId, int productId)
        {
            var cart = this.GetCart(cartId);
            cart.AddItem(productId);
        }

        public IEnumerable<CartItemSummaryServiceModel> AllItemsSummary(string cartId)
        {
            return this.carts[cartId]?.AllItems();
        }

        public void Remove(string cartId, int productId)
        {
            var cart = this.GetCart(cartId);

            if (cart == null)
            {
                return;
            }

            cart.RemoveItem(productId);
        }

        public CartServiceModel GetCart(string cartId)
        {   
            return this.carts.GetOrAdd(cartId, new CartServiceModel());
        }
    }
}
