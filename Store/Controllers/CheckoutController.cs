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
        public ActionResult AddressAndPayment(FormCollection values)
        {
            var order = new Order();
            TryUpdateModel(order);

            try
            {
                if (string.Equals(values["PromoCode"], PromoCode,
                        StringComparison.OrdinalIgnoreCase) == false)
                {
                    return View(order);
                }

                order.Username = User.Identity.Name;
                order.OrderDate = DateTime.Now;

                orderRepository.Create(order.ToDal());

                //Process the order
                string cartId = CartHelper.GetCartId(HttpContext);
                cartRepository.CreateOrder(order.ToDal(), cartId);

                return RedirectToAction("Complete", new {id = order.OrderId});
            }
            catch
            {
                //Invalid - redisplay with errors
                return View(order);
            }
        }

        public ActionResult Complete(int id)
        {
            // Validate if customer owns this order
            bool isValid = orderRepository.OrderIsValid(id, User.Identity.Name);
            return isValid ? View(id) : View("Error");
        }

        public ActionResult UserCart(string nameparam)
        {
            ViewBag.param = nameparam;
            var orderDetails = orderDetailRepository.GetAll().Select(i => i.ToMvc());
            return View(orderDetails);
        }

        public ActionResult UserCartDetails(int idparam)
        {
            ViewBag.param = idparam;
            var orderDetails = orderDetailRepository.GetAll().Select(i => i.ToMvc());
            return View(orderDetails);
        }
    }
}