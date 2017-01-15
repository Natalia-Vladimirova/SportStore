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
    public class StoreManagerController : Controller
    {
        private readonly IProductRepository productRepository = new ProductRepository(new StoreDbContext());
        private readonly ICategoryRepository categoryRepository = new CategoryRepository(new StoreDbContext());

        public ViewResult Index()
        {
            var products = productRepository.GetAll().Select(i => i.ToMvc());
            return View(products);
        }
        
        public ViewResult Details(int id)
        {
            var product = productRepository.GetById(id).ToMvc();
            return View(product);
        }
        
        public ActionResult Create()
        {
            var categories = categoryRepository.GetAll().Select(i => i.ToMvc());
            ViewBag.CategId = new SelectList(categories, "CategId", "Category");
            return View();
        }
        
        [HttpPost]
        public ActionResult Create(Tovar tovar)
        {
            if (ModelState.IsValid)
            {
                productRepository.Create(tovar.ToDal());
                return RedirectToAction("Index");  
            }

            var categories = categoryRepository.GetAll().Select(i => i.ToMvc());
            ViewBag.CategId = new SelectList(categories, "CategId", "Category", tovar.CategId);
            return View(tovar);
        }
        
        public ActionResult Edit(int id)
        {
            var product = productRepository.GetById(id).ToMvc();
            var categories = categoryRepository.GetAll().Select(i => i.ToMvc());
            ViewBag.CategId = new SelectList(categories, "CategId", "Category", product.CategId);
            return View(product);
        }
        
        [HttpPost]
        public ActionResult Edit(Tovar tovar)
        {
            if (ModelState.IsValid)
            {
                productRepository.Update(tovar.ToDal());
                return RedirectToAction("Index");
            }

            var categories = categoryRepository.GetAll().Select(i => i.ToMvc());
            ViewBag.CategId = new SelectList(categories, "CategId", "Category", tovar.CategId);
            return View(tovar);
        }
        
        public ActionResult Delete(int id)
        {
            var product = productRepository.GetById(id).ToMvc();
            return View(product);
        }
        
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {            
            productRepository.Delete(id);
            return RedirectToAction("Index");
        }
    }
}