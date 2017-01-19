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
    public class StoreManagerCatController : Controller
    {
        private readonly ICategoryRepository categoryRepository = new CategoryRepository(new StoreDbContext());

        public ViewResult Index()
        {
            return View(categoryRepository.GetAll().Select(i => i.ToMvc()));
        }
        
        public ViewResult Details(int id)
        {
            return View(categoryRepository.GetById(id).ToMvc());
        }
        
        public ActionResult Create()
        {
            return View();
        } 
        
        [HttpPost]
        public ActionResult Create(Category category)
        {
            if (ModelState.IsValid)
            {
                categoryRepository.Create(category.ToDal());
                return RedirectToAction("Index");  
            }

            return View(category);
        }
        
        public ActionResult Edit(int id)
        {
            return View(categoryRepository.GetById(id).ToMvc());
        }
        
        [HttpPost]
        public ActionResult Edit(Category category)
        {
            if (ModelState.IsValid)
            {
                categoryRepository.Update(category.ToDal());
                return RedirectToAction("Index");
            }
            return View(category);
        }
        
        public ActionResult Delete(int id)
        {
            return View(categoryRepository.GetById(id).ToMvc());
        }
        
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            categoryRepository.Delete(id);
            return RedirectToAction("Index");
        }
    }
}