using System;
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
    [Authorize]
    public class CheckoutController : Controller
    {
        private readonly ICartRepository cartRepository = new CartRepository(new StoreDbContext());
        private readonly IOrderRepository orderRepository = new OrderRepository(new StoreDbContext());
        private readonly IOrderDetailRepository orderDetailRepository = new OrderDetailRepository(new StoreDbContext());

        private const string PromoCode = "FREE";

        public ActionResult AddressAndPayment()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddressAndPayment(Order order, string promoCode)
        {
            if (!ModelState.IsValid)
            {
                return View(order);
            }

            if (!string.Equals(promoCode, PromoCode, StringComparison.InvariantCultureIgnoreCase))
            {
                ModelState.AddModelError("PromoCode", "Incorrect promocode");
                return View(order);
            }

            try
            {
                order.Username = User.Identity.Name;
                order.OrderDate = DateTime.Now;

                int orderId = orderRepository.Create(order.ToDal());
                order.OrderId = orderId;

                string cartId = CartHelper.GetCartId(HttpContext);
                cartRepository.CreateOrder(order.ToDal(), cartId);

                return RedirectToAction("Complete", new { id = order.OrderId });
            }
            catch
            {
                ModelState.AddModelError("", "An error occurred while placing your order...");
                return View(order);
            }
        }
        
        public ActionResult Complete(int id)
        {
            // Validate if customer owns this order
            bool isValid = orderRepository.OrderIsValid(id, User.Identity.Name);
            return isValid ? View(id) : View("Error");
        }
        
        public ActionResult UserCart(string username)
        {
            ViewBag.Username = username;
            return View(orderDetailRepository.GetUserOrders(username).Select(i => i.ToMvc()));
        }
        
        public ActionResult UserCartDetails(int id)
        {
            var order = orderRepository.GetById(id).ToMvc();
            order.OrderDetails = orderDetailRepository.GetByOrderId(id).Select(i => i.ToMvc());
            return View(order);
        }
    }
}