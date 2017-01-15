using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Xml;
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

        public ActionResult Search(string see)
        {
            ViewBag.param = see;
            return View(productRepository.GetAll().Select(i => i.ToMvc()));
        }

        public ActionResult AddCompare(int id)
        {
            compare.Add(id);
            return Redirect("Index");
        }

        public ActionResult DeleteCompare(int id)
        {
            compare.Remove(id);
            return Redirect("ShowCompare");
        }

        public ActionResult DeleteAllCompare()
        {
            compare.Clear();
            return Redirect("ShowCompare");
        }

        public ActionResult ShowCompare()
        {
            ViewBag.listCompare = compare;
            return View(productRepository.GetAll().Select(i => i.ToMvc()));
        }

        public ActionResult Browse(int param)
        {
            ViewBag.param = param;
            return View(productRepository.GetAll().Select(i => i.ToMvc()));
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

        public ActionResult Myxml()
        {
            var cat = categoryRepository.GetAll();

            XmlTextWriter textWriter = new XmlTextWriter("C:\\Предметы\\_STORE_\\Store\\myxml.xml", null);
            textWriter.WriteStartDocument();
            textWriter.WriteStartElement("Table");

            foreach (var r in cat)
            {

                {
                    textWriter.WriteStartElement("Element");
                    textWriter.WriteStartElement("CategId", "");
                    textWriter.WriteString(Convert.ToString(r.CategId));
                    textWriter.WriteEndElement();
                    textWriter.WriteStartElement("Category", "");
                    textWriter.WriteString(Convert.ToString(r.Category));
                    textWriter.WriteEndElement();
                    textWriter.WriteEndElement();
                }
            }
            textWriter.WriteEndElement();
            textWriter.WriteEndDocument();
            textWriter.Close();

            return Redirect("Index");
        }
    }
}
