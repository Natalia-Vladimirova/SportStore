using System.Linq;
using System.Web.Mvc;
using DAL.Interfaces.Repositories;
using Store.Mappers;
using Store.Models;

namespace Store.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class OrderController : Controller
    {
        private readonly IOrderRepository orderRepository;
        private readonly IOrderDetailRepository orderDetailRepository;

        public OrderController(IOrderRepository orderRepository, IOrderDetailRepository orderDetailRepository)
        {
            this.orderRepository = orderRepository;
            this.orderDetailRepository = orderDetailRepository;
        }

        public ActionResult Index()
        {
            return View(orderRepository.GetAll().Select(i => i.Username).Distinct());
        }

        public ActionResult UserOrders(string username)
        {
            ViewBag.Username = username;
            return View(orderRepository.GetUserOrders(username).Select(i => i.ToMvc()));
        }

        public ActionResult UserOrderDetails(int id)
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
