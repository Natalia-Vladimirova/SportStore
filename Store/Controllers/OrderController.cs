using System.Linq;
using System.Web.Mvc;
using DAL;
using DAL.Interfaces.Repositories;
using DAL.Repositories;
using Store.Mappers;
using Store.Models;

namespace Store.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class OrderController : Controller
    {
        private readonly IOrderRepository orderRepository = new OrderRepository(new StoreDbContext());
        private readonly IOrderDetailRepository orderDetailRepository = new OrderDetailRepository(new StoreDbContext());

        public ActionResult Index()
        {
            return View(orderDetailRepository.GetAll().Select(i => i.ToMvc()));
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
        
        public ActionResult Delete(int id)
        {
            Order order = orderRepository.GetById(id).ToMvc();
            return View(order);
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            orderRepository.Delete(id);
            return RedirectToAction("Index");
        }
    }
}
