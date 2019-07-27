using ShoppingCartExercise.Data;
using ShoppingCartExercise.Data.Models;
using ShoppingCartExercise.Services.Interfaces;
using ShoppingCartExercise.Services.Models.ShoppingCartModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ShoppingCartExercise.Services.Implementations
{
    public class OrderService : AbstractService ,IOrderService
    {
        public OrderService(ShoppingCartExerciseDbContext db) 
            : base(db)
        {
        }

        public void Create(string userId, IEnumerable<CartItemSummaryServiceModel> items)
        {
            if (String.IsNullOrEmpty(userId) || items.Count() == 0)
            {
                return;     // Exception ?
            }


            var keyValuePairs = items?
                .ToDictionary(i => i.Id, i => i.Quantity);

            var orderItems = this.db
                .Shoes
                .Where(s => keyValuePairs.ContainsKey(s.Id))
                .Select(s => new OrderItem
                {
                    ShoeId = s.Id,
                    ShoePrice = s.Price,
                    Quantity = keyValuePairs[s.Id]
                })
                .ToList();

            var order = new Order
            {
                UserId = userId,
                Items = orderItems
            };

            this.db
                .Orders
                .Add(order);

            this.db
                .SaveChanges();
        }
    }
}
