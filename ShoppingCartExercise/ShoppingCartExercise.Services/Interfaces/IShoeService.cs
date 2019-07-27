using ShoppingCartExercise.Services.Models;
using ShoppingCartExercise.Services.Models.ShoppingCartModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShoppingCartExercise.Services.Interfaces
{
    public interface IShoeService
    {
        void Add(string name, decimal price, int quantity);

        ShoeServiceModel ById(int id);

        IEnumerable<ShoeServiceModel> All();

        IEnumerable<CartItemDetailsServiceModel> CartItemsDetails(CartServiceModel cart);
    }
}
