using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DAL;
using Store.Mappers;
using Store.Models;

namespace Store.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class StoreManagerUserController : Controller
    {
        StoreDbContext storeDB = new StoreDbContext();
        //
        // GET: /StoreManagerUser/

        public ActionResult Index()
        {
            return View(storeDB.OrderDetails.ToList().Select(i => i.ToMvc()));
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

        //
        // GET: //StoreManagerUser/Delete/5

        public ActionResult Delete(int id)
        {
            Order order = storeDB.Orders.Find(id).ToMvc();
            return View(order);
        }

        //
        // POST: /StoreManagerUser/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Order order = storeDB.Orders.Find(id).ToMvc();
            storeDB.Orders.Remove(order.ToDal());
            storeDB.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            storeDB.Dispose();
            base.Dispose(disposing);
        }

    }
}
