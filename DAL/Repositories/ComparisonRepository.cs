using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using DAL.Interfaces.Entities;
using DAL.Interfaces.Repositories;

namespace DAL.Repositories
{
    public class ComparisonRepository : IComparisonRepository
    {
        private readonly DbContext context;

        public ComparisonRepository(DbContext context)
        {
            this.context = context;
        }

        public IEnumerable<Comparison> GetUserComparisons(string userName)
        {
            return context.Set<Comparison>()
                .Where(i => i.UserName == userName)
                .ToList();
        }

        public void MigrateComparisons(string userName, string oldUserName)
        {
            var shoppingComparisons = context.Set<Comparison>().Where(c => c.UserName == oldUserName);
            var existingUserComparisons = context.Set<Comparison>().Where(c => c.UserName == userName).ToList();

            foreach (Comparison item in shoppingComparisons)
            {
                var existingCartProduct = existingUserComparisons.FirstOrDefault(i => i.ProductId == item.ProductId);

                if (existingCartProduct == null)
                {
                    item.UserName = userName;
                }
                else
                {
                    context.Set<Comparison>().Remove(item);
                }
            }

            context.SaveChanges();
        }

        public void Create(Comparison comparison)
        {
            var existingComparison = context.Set<Comparison>()
                .FirstOrDefault(i => i.ProductId == comparison.ProductId && i.UserName == comparison.UserName);

            if (existingComparison == null)
            {
                context.Set<Comparison>().Add(comparison);
                context.SaveChanges();
            }
        }

        public void Delete(int productId, string userName)
        {
            var item = context.Set<Comparison>().FirstOrDefault(i => i.ProductId == productId && i.UserName == userName);

            if (item == null) return;

            context.Set<Comparison>().Remove(item);
            context.SaveChanges();
        }

        public void DeleteAll(string userName)
        {
            var items = context.Set<Comparison>().Where(i => i.UserName == userName).ToList();

            if (!items.Any()) return;

            foreach (var item in items)
            {
                context.Set<Comparison>().Remove(item);
            }
            
            context.SaveChanges();
        }
    }
}
