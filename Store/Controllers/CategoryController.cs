using System.Linq;
using System.Web.Mvc;
using DAL.Interfaces.Repositories;
using Store.Mappers;
using Store.Models;

namespace Store.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class CategoryController : Controller
    {
        private readonly ICategoryRepository categoryRepository;

        public CategoryController(ICategoryRepository categoryRepository)
        {
            this.categoryRepository = categoryRepository;
        }

        public ViewResult Index()
        {
            return View(categoryRepository.GetAll().Select(i => i.ToMvc()));
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