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
    public class StoreManagerUserController : Controller
    {
        private readonly IOrderRepository orderRepository = new OrderRepository(new StoreDbContext());
        private readonly IOrderDetailRepository orderDetailRepository = new OrderDetailRepository(new StoreDbContext());

        public ActionResult Index()
        {
            return View(orderDetailRepository.GetAll().Select(i => i.ToMvc()));
        }

        public ActionResult UserCart(string nameparam)
        {
            ViewBag.param = nameparam;
            return View(orderDetailRepository.GetAll().Select(i => i.ToMvc()));
        }

        public ActionResult UserCartDetails(int idparam)
        {
            ViewBag.param = idparam;
            return View(orderDetailRepository.GetAll().Select(i => i.ToMvc()));
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
