using System.Linq;
using System.Web.Mvc;
using DAL;
using DAL.Interfaces.Repositories;
using DAL.Repositories;
using Store.Helpers;
using Store.Mappers;
using Store.Models;

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

            if (Request.IsAjaxRequest())
            {
                return PartialView("_CartCount", cartRepository.GetCount(cartId));
            }          
            
            return RedirectToAction("Index", "Store");
        }

        [HttpPost]
        public ActionResult RemoveFromCart(int id)
        {
            var cartId = CartHelper.GetCartId(HttpContext);
            var productName = cartRepository.GetById(id).Product.Title;
            var itemCount = cartRepository.RemoveFromCart(id, cartId);

            var results = new ShoppingCartRemoveViewModel
            {
                Message = "Product <<" + Server.HtmlEncode(productName) +
                    ">>was removed.",
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
            return PartialView("_CartCount", cartRepository.GetCount(cartId));
        }
    }
}