using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShoppingCartExercise.Services.Models.ShoppingCartModels
{
    public class CartServiceModel
    {
        public string Id { get; set; }
        private IList<CartItemSummaryServiceModel> Items;

        public CartServiceModel()
        {
            this.Items =new List<CartItemSummaryServiceModel>();
        }

        public void AddItem(int productId)
        {
            var item = this.Items
                .FirstOrDefault(i => i.Id == productId);

            if (item == null)
            {
                item = new CartItemSummaryServiceModel
                {
                    Id = productId,
                    Quantity = 1
                };

                this.Items.Add(item);
            }
            else
            {
                item.Quantity++;
            }
        }

        public void RemoveItem(int productId)
        {
            var productExist = this.Items.Any(i => i.Id == productId);
            if (!productExist)
            {
                return;
            }

            var item = this.Items
                .Where(i => i.Id == productId)
                .First();

            this.Items.Remove(item);
        }

        public void Clear()
        {
            this.Items.Clear();
        }

        public IEnumerable<CartItemSummaryServiceModel> AllItems() => new List<CartItemSummaryServiceModel>(this.Items);
    }
}
