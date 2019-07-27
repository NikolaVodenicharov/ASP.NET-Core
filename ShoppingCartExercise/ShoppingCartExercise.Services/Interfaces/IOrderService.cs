using ShoppingCartExercise.Services.Models.ShoppingCartModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShoppingCartExercise.Services.Interfaces
{
    public interface IOrderService
    {
        void Create(string UserId, IEnumerable<CartItemSummaryServiceModel> items);
    }
}
