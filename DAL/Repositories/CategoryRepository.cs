using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using DAL.Interfaces.Entities;
using DAL.Interfaces.Repositories;

namespace DAL.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly DbContext context;

        public CategoryRepository(DbContext context)
        {
            this.context = context;
        }

        public IEnumerable<Categ> GetAll()
        {
            return context.Set<Categ>().ToList();
        }

        public Categ GetById(int id)
        {
            return context.Set<Categ>().FirstOrDefault(i => i.CategId == id);
        }

        public void Create(Categ entity)
        {
            context.Set<Categ>().Add(entity);
            context.SaveChanges();
        }

        public void Delete(int id)
        {
            var category = context.Set<Categ>().FirstOrDefault(i => i.CategId == id);

            if (category == null) return;

            context.Set<Categ>().Remove(category);
            context.SaveChanges();
        }

        public void Update(Categ entity)
        {
            var category = context.Set<Categ>().FirstOrDefault(i => i.CategId == entity.CategId);

            if (category == null) return;

            category.Category = entity.Category;
            context.SaveChanges();
        }
    }
}
