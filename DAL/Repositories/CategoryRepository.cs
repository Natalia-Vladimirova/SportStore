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

        public IEnumerable<Category> GetAll()
        {
            return context.Set<Category>().ToList();
        }

        public Category GetById(int id)
        {
            return context.Set<Category>().FirstOrDefault(i => i.CategoryId == id);
        }

        public void Create(Category entity)
        {
            context.Set<Category>().Add(entity);
            context.SaveChanges();
        }

        public void Delete(int id)
        {
            var category = context.Set<Category>().FirstOrDefault(i => i.CategoryId == id);

            if (category == null) return;

            context.Set<Category>().Remove(category);
            context.SaveChanges();
        }

        public void Update(Category entity)
        {
            var category = context.Set<Category>().FirstOrDefault(i => i.CategoryId == entity.CategoryId);

            if (category == null) return;

            category.CategoryName = entity.CategoryName;
            context.SaveChanges();
        }
    }
}
