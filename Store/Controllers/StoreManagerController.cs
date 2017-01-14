using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Store.Models;

namespace Store.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class StoreManagerController : Controller
    {
        private StoreDBContext db = new StoreDBContext();

        //
        // GET: /StoreManager/

        public ViewResult Index()
        {
            var tovars = db.Tovars.Include(t => t.Categ);
            return View(tovars.ToList());
        }

        //
        // GET: /StoreManager/Details/5

        public ViewResult Details(int id)
        {
            Tovar tovar = db.Tovars.Find(id);
            return View(tovar);
        }

        //
        // GET: /StoreManager/Create

        public ActionResult Create()
        {
            ViewBag.CategId = new SelectList(db.Categs, "CategId", "Category");
            return View();
        } 

        //
        // POST: /StoreManager/Create

        [HttpPost]
        public ActionResult Create(Tovar tovar)
        {
            if (ModelState.IsValid)
            {
                db.Tovars.Add(tovar);
                db.SaveChanges();
                return RedirectToAction("Index");  
            }

            ViewBag.CategId = new SelectList(db.Categs, "CategId", "Category", tovar.CategId);
            return View(tovar);
        }
        
        //
        // GET: /StoreManager/Edit/5
 
        public ActionResult Edit(int id)
        {
            Tovar tovar = db.Tovars.Find(id);
            ViewBag.CategId = new SelectList(db.Categs, "CategId", "Category", tovar.CategId);
            return View(tovar);
        }

        //
        // POST: /StoreManager/Edit/5

        [HttpPost]
        public ActionResult Edit(Tovar tovar)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tovar).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CategId = new SelectList(db.Categs, "CategId", "Category", tovar.CategId);
            return View(tovar);
        }

        //
        // GET: /StoreManager/Delete/5
 
        public ActionResult Delete(int id)
        {
            Tovar tovar = db.Tovars.Find(id);
            return View(tovar);
        }

        //
        // POST: /StoreManager/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {            
            Tovar tovar = db.Tovars.Find(id);
            db.Tovars.Remove(tovar);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}