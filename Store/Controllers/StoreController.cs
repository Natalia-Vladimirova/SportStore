using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using DAL;
using DAL.Interfaces.Repositories;
using DAL.Repositories;
using Store.Mappers;

namespace Store.Controllers
{
    public class StoreController : Controller
    {
        private static List<int> compare = new List<int>();

        private readonly IProductRepository productRepository = new ProductRepository(new StoreDbContext());
        private readonly ICategoryRepository categoryRepository = new CategoryRepository(new StoreDbContext());

        public ActionResult Index()
        {
            ViewBag.listCompare = compare;
            return View(productRepository.GetAll().Select(i => i.ToMvc()));
        }

        public ActionResult CompareCount()
        {
            return PartialView("_CompareProducts", compare.Count);
        }

        public ActionResult Search(string see)
        {
            var model = productRepository.GetAll().Where(x => x.Title.ToUpper().Contains(see.ToUpper())).Select(i => i.ToMvc()).ToList();
            if (Request.IsAjaxRequest())
            {
                return PartialView("_ProductList", model);
            }
            return View(model);        
        }

        public ActionResult AddCompare(int id)
        {
            if (!compare.Contains(id))
            {
                compare.Add(id);
            }
           
            if (Request.IsAjaxRequest())
            {
                return PartialView("_CompareProducts", compare.Count);
            }
            
            return RedirectToAction("Index");
        }

        public ActionResult DeleteCompare(int id)
        {
            compare.Remove(id);
            if (compare.Count == 0)
            {
                return JavaScript("location.reload(true)");
            }

            if (Request.IsAjaxRequest())
            {
                return new EmptyResult();
            }

            return RedirectToAction("ShowCompare");
        }

        public ActionResult DeleteAllCompare()
        {
            compare.Clear();
            return RedirectToAction("ShowCompare");
        }

        public ActionResult ShowCompare()
        {
            return View(compare.Select(x => productRepository.GetById(x).ToMvc()).ToList());
        }

        public ActionResult Browse(int param)
        {
            return View(productRepository.GetAll().Where(x => x.CategoryId == param).Select(i => i.ToMvc()).ToList());
        }

        public ActionResult Details(int id)
        {
            var product = productRepository.GetById(id);
            return View(product.ToMvc());
        }

        [ChildActionOnly]
        public ActionResult CategMenu()
        {
            var categories = categoryRepository.GetAll();
            return PartialView(categories.Select(i => i.ToMvc()));
        }
    }
}
