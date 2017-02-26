using System.Linq;
using System.Web.Mvc;
using DAL.Interfaces.Repositories;
using Store.Helpers;
using Store.Mappers;
using Store.Models;

namespace Store.Controllers
{
    public class StoreController : Controller
    {
        private readonly IProductRepository productRepository;
        private readonly ICategoryRepository categoryRepository;
        private readonly IComparisonRepository comparisonRepository;

        public StoreController(IProductRepository productRepository, ICategoryRepository categoryRepository, IComparisonRepository comparisonRepository)
        {
            this.productRepository = productRepository;
            this.categoryRepository = categoryRepository;
            this.comparisonRepository = comparisonRepository;
        }

        public ActionResult Index()
        {
            return View(productRepository.GetAll().Select(i => i.ToMvc()));
        }

        public ActionResult CompareCount()
        {
            string userName = CartHelper.GetCartId(HttpContext);
            int compareCount = comparisonRepository.GetUserComparisons(userName).Count();
            return PartialView("_CompareProducts", compareCount);
        }

        public ActionResult Search(string see)
        {
            var model = productRepository.GetAll()
                .Where(x => x.Title.ToUpper().Contains(see.ToUpper()))
                .Select(i => i.ToMvc())
                .ToList();

            if (Request.IsAjaxRequest())
            {
                return PartialView("_ProductList", model);
            }
            return View(model);        
        }

        public ActionResult AddCompare(int id)
        {
            string userName = CartHelper.GetCartId(HttpContext);
            var comparison = new Comparison
            {
                UserName = userName,
                ProductId = id
            };

            comparisonRepository.Create(comparison.ToDal());
            
            int compareCount = comparisonRepository.GetUserComparisons(userName).Count();

            if (Request.IsAjaxRequest())
            {
                return PartialView("_CompareProducts", compareCount);
            }
            
            return RedirectToAction("Index");
        }

        public ActionResult DeleteCompare(int id)
        {
            string userName = CartHelper.GetCartId(HttpContext);
            comparisonRepository.Delete(id, userName);

            int compareCount = comparisonRepository.GetUserComparisons(userName).Count();

            if (compareCount == 0)
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
            string userName = CartHelper.GetCartId(HttpContext);
            comparisonRepository.DeleteAll(userName);
            return RedirectToAction("ShowCompare");
        }

        public ActionResult ShowCompare()
        {
            string userName = CartHelper.GetCartId(HttpContext);
            var items = comparisonRepository.GetUserComparisons(userName).Select(i => i.Product.ToMvc());
            return View(items);
        }

        public ActionResult Browse(int category)
        {
            return View(productRepository.GetAll().Where(x => x.CategoryId == category).Select(i => i.ToMvc()).ToList());
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
