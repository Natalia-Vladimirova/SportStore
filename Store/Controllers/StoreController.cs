using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Store.Models;
using System.Xml;
using DAL;
using Store.Mappers;

namespace Store.Controllers
{
    public class StoreController : Controller
    {
        StoreDbContext storeDB = new StoreDbContext();
        static List<int> compare = new List<int>();
        //
        // GET: /Store/
        public ActionResult Index()
        {
            ViewBag.listCompare = compare;
            return View(storeDB.Tovars.ToList().Select(i => i.ToMvc()));
        }

        // Поиск
        public ActionResult Search(string see)
        {
            ViewBag.param = see;
            return View(storeDB.Tovars.ToList().Select(i => i.ToMvc()));
        }

        // Добавить товар в сравнение
        public ActionResult AddCompare(int id)
        {
            compare.Add(id);
            return Redirect("Index");
        }

        // Удалить товар из сравнения
        public ActionResult DeleteCompare(int id)
        {
            compare.Remove(id);
            return Redirect("ShowCompare");
        }

        // Удалить все товары из сравнения
        public ActionResult DeleteAllCompare()
        {
            compare.Clear();
            return Redirect("ShowCompare");
        }
        // Сравнить товары
        public ActionResult ShowCompare()
        {
            ViewBag.listCompare = compare;
            return View(storeDB.Tovars.ToList().Select(i => i.ToMvc()));
        }

        public ActionResult Browse(int param)
        {
            ViewBag.param = param;
            return View(storeDB.Tovars.ToList().Select(i => i.ToMvc()));
        }

        //
        // GET: /Store/Browse
        public ActionResult BrowseOld(string categ)
        {
            var categModel = storeDB.Categs.Include("Tovars").Single(g => g.Category == categ);
            return View(categModel.ToMvc());
        }
        //
        // GET: /Store/Details
        public ActionResult Details(int id)
        {
            var tovar = storeDB.Tovars.Find(id);
            return View(tovar.ToMvc());
        }
        //
        // GET: /Store/CategMenu
        [ChildActionOnly]
        public ActionResult CategMenu()
        {
            var categs = storeDB.Categs.ToList();
            return PartialView(categs.Select(i => i.ToMvc()));
        }

        public ActionResult Myxml()
        {
            var cat = storeDB.Categs.ToList();

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
