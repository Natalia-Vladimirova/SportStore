using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using DAL.Interfaces.Entities;
using DAL.Interfaces.Repositories;

namespace DAL.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly DbContext context;

        public ProductRepository(DbContext context)
        {
            this.context = context;
        }

        public IEnumerable<Product> GetAll()
        {
            return context.Set<Product>().ToList();
        }

        public Product GetById(int id)
        {
            return context.Set<Product>().FirstOrDefault(i => i.ProductId == id);
        }

        public void Create(Product entity)
        {
            context.Set<Product>().Add(entity);
            context.SaveChanges();
        }

        public void Delete(int id)
        {
            var product = context.Set<Product>().FirstOrDefault(i => i.ProductId == id);

            if (product == null) return;

            context.Set<Product>().Remove(product);
            context.SaveChanges();
        }

        public void Update(Product entity)
        {
            var product = context.Set<Product>().FirstOrDefault(i => i.ProductId == entity.ProductId);

            if (product == null) return;

            product.Title = entity.Title;
            product.Price = entity.Price;
            product.Amount = entity.Amount;
            product.Description = entity.Description;
            product.Image = entity.Image;
            product.CategoryId = entity.CategoryId;

            context.SaveChanges();
        }
    }
}
