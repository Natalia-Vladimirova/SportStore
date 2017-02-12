using System;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DAL;
using DAL.Interfaces.Repositories;
using DAL.Repositories;
using Store.Mappers;
using Store.Models;

namespace Store.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class ProductController : Controller
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
            ViewBag.CategoryId = new SelectList(categories, "CategoryId", "CategoryName");
            return View();
        }
        
        [HttpPost]
        public ActionResult Create(Product product, HttpPostedFileBase uploadImage)
        {
            if (uploadImage == null)
            {
                ModelState.AddModelError("uploadImage", "Choose an image");
            }

            if (ModelState.IsValid)
            {
                string filename = DateTime.Now.ToString("yyyyMMddHHmmss") + "_" + uploadImage.FileName;
                string destPath = Server.MapPath("~/Content/Images/") + filename;
                
                using (var fileStream = new FileStream(destPath, FileMode.Create, FileAccess.Write))
                {
                    uploadImage.InputStream.CopyTo(fileStream);
                }

                product.Image = filename;
                productRepository.Create(product.ToDal());
                return RedirectToAction("Index");  
            }

            var categories = categoryRepository.GetAll().Select(i => i.ToMvc());
            ViewBag.CategoryId = new SelectList(categories, "CategoryId", "CategoryName", product.CategoryId);
            return View(product);
        }
        
        public ActionResult Edit(int id)
        {
            var product = productRepository.GetById(id).ToMvc();
            var categories = categoryRepository.GetAll().Select(i => i.ToMvc());
            ViewBag.CategoryId = new SelectList(categories, "CategoryId", "CategoryName", product.CategoryId);
            return View(product);
        }
        
        [HttpPost]
        public ActionResult Edit(Product product, HttpPostedFileBase uploadImage)
        {
            if (ModelState.IsValid)
            {
                if (uploadImage != null)
                {
                    string filename = DateTime.Now.ToString("yyyyMMddHHmmss") + "_" + uploadImage.FileName;
                    string destPath = Server.MapPath("~/Content/Images/") + filename;
                    string oldFilePath = Server.MapPath("~/Content/Images/") + product.Image;

                    using (var fileStream = new FileStream(destPath, FileMode.Create, FileAccess.Write))
                    {
                        uploadImage.InputStream.CopyTo(fileStream);
                    }

                    product.Image = filename;

                    if (System.IO.File.Exists(oldFilePath))
                    {
                        System.IO.File.Delete(oldFilePath);
                    }
                }

                productRepository.Update(product.ToDal());
                return RedirectToAction("Index");
            }

            var categories = categoryRepository.GetAll().Select(i => i.ToMvc());
            ViewBag.CategoryId = new SelectList(categories, "CategoryId", "CategoryName", product.CategoryId);
            return View(product);
        }
        
        public ActionResult Delete(int id)
        {
            var product = productRepository.GetById(id).ToMvc();
            return View(product);
        }
        
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id, string image)
        {
            string imagePath = Server.MapPath("~/Content/Images/") + image;
            if (System.IO.File.Exists(imagePath))
            {
                System.IO.File.Delete(imagePath);
            }
            productRepository.Delete(id);
            return RedirectToAction("Index");
        }
    }
}