﻿using System.Linq;
using System.Web.Mvc;
using DAL;
using DAL.Interfaces.Repositories;
using DAL.Repositories;
using Store.ViewModels;
using Store.Helpers;
using Store.Mappers;

namespace Store.Controllers
{
    public class ShoppingCartController : Controller
    {
        private readonly ICartRepository cartRepository = new CartRepository(new StoreDbContext());
        private readonly IProductRepository productRepository = new ProductRepository(new StoreDbContext());

        //StoreDBContext storeDB = new StoreDBContext();

        //
        // GET: /ShoppingCart/
        public ActionResult Index()
        {
            //var cart = ShoppingCart.GetCart(this.HttpContext);
            string cartId = CartHelper.GetCartId(this.HttpContext);

            // Set up our ViewModel
            var viewModel = new ShoppingCartViewModel
            {
                CartItems = cartRepository.GetCartItems(cartId).Select(i => i.ToMvc()).ToList(),//cart.GetCartItems(),
                CartTotal = cartRepository.GetTotal(cartId)//cart.GetTotal()
            };

            return View(viewModel);
        }

        //
        // GET: /Store/AddToCart/5
        public ActionResult AddToCart(int id)
        {
            // Retrieve the album from the database
            var product = productRepository.GetById(id);

            // Add it to the shopping cart
            string cartId = CartHelper.GetCartId(this.HttpContext);

            cartRepository.AddToCart(product, cartId);

            // Go back to the main store page for more shopping
            return RedirectToAction("Index");
        }

        //
        // AJAX: /ShoppingCart/RemoveFromCart/5
        [HttpPost]
        public ActionResult RemoveFromCart(int id)
        {
            // Remove the item from the cart
            string cartId = CartHelper.GetCartId(this.HttpContext);

            // Get the name of the album to display confirmation
            string tovarName = cartRepository.GetById(id).Tovar.Title;

            // Remove from cart
            int itemCount = cartRepository.RemoveFromCart(id, cartId);

            // Display the confirmation message
            var results = new ShoppingCartRemoveViewModel
            {
                Message = "Товар <<" + Server.HtmlEncode(tovarName) +
                    ">>был удален из корзины.",
                CartTotal = cartRepository.GetTotal(cartId),
                CartCount = cartRepository.GetCount(cartId),
                ItemCount = itemCount,
                DeleteId = id
            };

            return Json(results);
        }

        //
        // GET: /ShoppingCart/CartSummary
        [ChildActionOnly]
        public ActionResult CartSummary()
        {
            string cartId = CartHelper.GetCartId(this.HttpContext);
            ViewData["CartCount"] = cartRepository.GetCount(cartId);
            return PartialView("CartSummary");
        }
    }
}