using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ShoppingCartExercise.Data.Models;
using ShoppingCartExercise.Services.Interfaces;
using System;

namespace ShoppingCartExercise.Web.Controllers
{
    public class CartController : Controller
    {
        private const string CartIdKey = "CartId";

        private readonly ICartManager cartManager;
        private readonly IShoeService shoeService;
        private readonly IOrderService orderService;
        private readonly UserManager<User> userManager;

        public CartController(ICartManager cartManager, IShoeService shoeService, IOrderService orderService, UserManager<User> userManager)
        {
            this.cartManager = cartManager;
            this.shoeService = shoeService;
            this.orderService = orderService;
            this.userManager = userManager;
        }

        [HttpPost]
        public IActionResult Add(int productId)
        {
            var cartId = this.GetCartId();

            this.cartManager.AddToCart(cartId, productId);
            
            return RedirectToAction(nameof(All));
        }

        public IActionResult All()
        {
            var cartId = this.GetCartId();
            var cart = this.cartManager.GetCart(cartId);

            var items = this.shoeService.CartItemsDetails(cart);

            return View(items);
        }

        [Authorize]
        public IActionResult FinishOrder()
        {
            var cartId = this.GetCartId();
            var cart = this.cartManager.GetCart(cartId);
            var items = cart.AllItems();

            var userId = this.userManager.GetUserId(User);

            this.orderService.Create(userId, items);
            cart.Clear();

            return RedirectToAction(nameof(All));
        }

        private string GetCartId()
        {
            var cartId = this.HttpContext.Session.GetString(CartIdKey);

            if (cartId == null)
            {
                cartId = Guid.NewGuid().ToString();
                this.HttpContext.Session.SetString(CartIdKey, cartId);
            }

            return cartId;
        }
    }
}