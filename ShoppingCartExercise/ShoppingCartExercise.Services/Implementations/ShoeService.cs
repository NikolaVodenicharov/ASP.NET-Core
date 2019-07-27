using ShoppingCartExercise.Data;
using ShoppingCartExercise.Data.Models;
using ShoppingCartExercise.Services.Interfaces;
using ShoppingCartExercise.Services.Models;
using ShoppingCartExercise.Services.Models.ShoppingCartModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ShoppingCartExercise.Services.Implementations
{
    public class ShoeService : AbstractService, IShoeService
    {
        public ShoeService(ShoppingCartExerciseDbContext db) 
            : base(db)
        {
        }

        public void Add(string name, decimal price, int quantity)
        {
            this.db.Shoes.Add(new Shoe
            {
                Name = name,
                Price = price,
                Quantity = quantity
            });

            this.db.SaveChanges();
        }

        public IEnumerable<ShoeServiceModel> All()
        {
            return this.db
                .Shoes
                .Select(s => new ShoeServiceModel
                {
                    Id = s.Id,
                    Name = s.Name,
                    Price = s.Price,
                    Quantity = s.Quantity
                })
                .ToList();
        }

        public ShoeServiceModel ById(int id)
        {
            return this.db
                .Shoes
                .Where(s => s.Id == id)
                .Select(s => new ShoeServiceModel
                {
                    Id = s.Id,
                    Name = s.Name,
                    Price = s.Price,
                    Quantity = s.Quantity
                })
                .FirstOrDefault();
        }


        public IEnumerable<CartItemDetailsServiceModel> CartItemsDetails(CartServiceModel cart)
        {
            var items = cart?
                .AllItems()
                .ToDictionary(i => i.Id, i => i.Quantity);

            if (items == null)
            {
                return null;
            }

            return this.db
                .Shoes
                .Where(s => items.ContainsKey(s.Id))
                .Select(s => new CartItemDetailsServiceModel
                {
                    Id = s.Id,
                    Name = s.Name,
                    Price = s.Price,
                    Quantity = items[s.Id]
                })
                .ToList();
        }
    }
}
