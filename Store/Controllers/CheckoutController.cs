using System;
using System.Linq;
using System.Web.Mvc;
using DAL;
using Store.Mappers;
using Store.Models;

namespace Store.Controllers
{
    [Authorize]
    public class CheckoutController : Controller
    {
        StoreDbContext storeDB = new StoreDbContext();
        const string PromoCode = "FREE";
        //
        // GET: /Checkout/AddressAndPayment
        public ActionResult AddressAndPayment()
        {
            return View();
        }
        //
        // POST: /Checkout/AddressAndPayment
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
                else
                {
                    order.Username = User.Identity.Name;
                    order.OrderDate = DateTime.Now;

                    //Save Order
                    storeDB.Orders.Add(order.ToDal());
                    storeDB.SaveChanges();
                    //Process the order
                    var cart = ShoppingCart.GetCart(this.HttpContext);
                    cart.CreateOrder(order);

                    return RedirectToAction("Complete", new { id = order.OrderId });
                }
            }
            catch
            {
                //Invalid - redisplay with errors
                return View(order);
            }
        }
        //
        // GET: /Checkout/Complete
        public ActionResult Complete(int id)
        {
            // Validate customer owns this order
            bool isValid = storeDB.Orders.Any(
                o => o.OrderId == id &&
                o.Username == User.Identity.Name);

            if (isValid)
            {
                return View(id);
            }
            else
            {
                return View("Error");
            }
        }
        public ActionResult UserCart(string nameparam)
        {
            ViewBag.param = nameparam;
            return View(storeDB.OrderDetails.ToList().Select(i => i.ToMvc()));
        }

        public ActionResult UserCartDetails(int idparam)
        {
            ViewBag.param = idparam;
            return View(storeDB.OrderDetails.ToList().Select(i => i.ToMvc()));
        }
    }
}