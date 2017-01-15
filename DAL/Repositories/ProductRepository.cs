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

        public IEnumerable<Tovar> GetAll()
        {
            return context.Set<Tovar>().ToList();
        }

        public Tovar GetById(int id)
        {
            return context.Set<Tovar>().FirstOrDefault(i => i.TovarId == id);
        }

        public void Create(Tovar entity)
        {
            context.Set<Tovar>().Add(entity);
            context.SaveChanges();
        }

        public void Delete(int id)
        {
            var product = context.Set<Tovar>().FirstOrDefault(i => i.TovarId == id);

            if (product == null) return;

            context.Set<Tovar>().Remove(product);
            context.SaveChanges();
        }

        public void Update(Tovar entity)
        {
            var product = context.Set<Tovar>().FirstOrDefault(i => i.TovarId == entity.TovarId);

            if (product == null) return;

            product.Title = entity.Title;
            product.Price = entity.Price;
            product.Amount = entity.Amount;
            product.Description = entity.Description;
            product.Image = entity.Image;
            product.CategId = entity.CategId;

            context.SaveChanges();
        }
    }
}
