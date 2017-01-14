using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DAL;
using Store.Mappers;
using Store.Models;

namespace Store.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class StoreManagerCatController : Controller
    {
        private StoreDbContext db = new StoreDbContext();

        //
        // GET: /StoreManagerCat/

        public ViewResult Index()
        {
            return View(db.Categs.ToList().Select(i => i.ToMvc()));
        }

        //
        // GET: /StoreManagerCat/Details/5

        public ViewResult Details(int id)
        {
            Categ categ = db.Categs.Find(id).ToMvc();
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
                db.Categs.Add(categ.ToDal());
                db.SaveChanges();
                return RedirectToAction("Index");  
            }

            return View(categ);
        }
        
        //
        // GET: /StoreManagerCat/Edit/5
 
        public ActionResult Edit(int id)
        {
            Categ categ = db.Categs.Find(id).ToMvc();
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
            Categ categ = db.Categs.Find(id).ToMvc();
            return View(categ);
        }

        //
        // POST: /StoreManagerCat/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {            
            Categ categ = db.Categs.Find(id).ToMvc();
            db.Categs.Remove(categ.ToDal());
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