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

        public Tovar GetById(int id)
        {
            return context.Set<Tovar>().FirstOrDefault(i => i.TovarId == id);
        }
    }
}
