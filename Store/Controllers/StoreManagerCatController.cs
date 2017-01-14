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
    public class StoreManagerCatController : Controller
    {
        private StoreDBContext db = new StoreDBContext();

        //
        // GET: /StoreManagerCat/

        public ViewResult Index()
        {
            return View(db.Categs.ToList());
        }

        //
        // GET: /StoreManagerCat/Details/5

        public ViewResult Details(int id)
        {
            Categ categ = db.Categs.Find(id);
            return View(categ);
        }

        //
        // GET: /StoreManagerCat/Create

        public ActionResult Create()
        {
            return View();
        } 

        //
        // POST: /StoreManagerCat/Create

        [HttpPost]
        public ActionResult Create(Categ categ)
        {
            if (ModelState.IsValid)
            {
                db.Categs.Add(categ);
                db.SaveChanges();
                return RedirectToAction("Index");  
            }

            return View(categ);
        }
        
        //
        // GET: /StoreManagerCat/Edit/5
 
        public ActionResult Edit(int id)
        {
            Categ categ = db.Categs.Find(id);
            return View(categ);
        }

        //
        // POST: /StoreManagerCat/Edit/5

        [HttpPost]
        public ActionResult Edit(Categ categ)
        {
            if (ModelState.IsValid)
            {
                db.Entry(categ).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(categ);
        }

        //
        // GET: /StoreManagerCat/Delete/5
 
        public ActionResult Delete(int id)
        {
            Categ categ = db.Categs.Find(id);
            return View(categ);
        }

        //
        // POST: /StoreManagerCat/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {            
            Categ categ = db.Categs.Find(id);
            db.Categs.Remove(categ);
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