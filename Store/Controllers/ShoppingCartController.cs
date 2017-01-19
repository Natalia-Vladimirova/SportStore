using System.Linq;
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

        public ActionResult Index()
        {
            string cartId = CartHelper.GetCartId(HttpContext);

            var viewModel = new ShoppingCartViewModel
            {
                CartItems = cartRepository.GetCartItems(cartId).Select(i => i.ToMvc()),
                CartTotal = cartRepository.GetTotal(cartId)
            };

            return View(viewModel);
        }

        public ActionResult AddToCart(int id)
        {
            var product = productRepository.GetById(id);

            string cartId = CartHelper.GetCartId(HttpContext);
            cartRepository.AddToCart(product, cartId);

            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult RemoveFromCart(int id)
        {
            string cartId = CartHelper.GetCartId(HttpContext);

            string productName = cartRepository.GetById(id).Product.Title;

            int itemCount = cartRepository.RemoveFromCart(id, cartId);

            // Display the confirmation message
            var results = new ShoppingCartRemoveViewModel
            {
                Message = "Товар <<" + Server.HtmlEncode(productName) +
                    ">>был удален из корзины.",
                CartTotal = cartRepository.GetTotal(cartId),
                CartCount = cartRepository.GetCount(cartId),
                ItemCount = itemCount,
                DeleteId = id
            };

            return Json(results);
        }

        [ChildActionOnly]
        public ActionResult CartSummary()
        {
            string cartId = CartHelper.GetCartId(HttpContext);
            return PartialView("CartSummary", cartRepository.GetCount(cartId));
        }
    }
}