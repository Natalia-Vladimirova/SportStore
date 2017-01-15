using System.Data.Entity;
using System.Linq;
using DAL.Interfaces.Entities;
using DAL.Interfaces.Repositories;

namespace DAL.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly DbContext context;

        public OrderRepository(DbContext context)
        {
            this.context = context;
        }

        public Order GetById(int id)
        {
            return context.Set<Order>().FirstOrDefault(i => i.OrderId == id);
        }

        public void Create(Order entity)
        {
            context.Set<Order>().Add(entity);
            context.SaveChanges();
        }

        public void Delete(int id)
        {
            var order = context.Set<Order>().FirstOrDefault(i => i.OrderId == id);

            if (order == null) return;

            context.Set<Order>().Remove(order);
            context.SaveChanges();
        }

        public bool OrderIsValid(int orderId, string username)
        {
            return context.Set<Order>().Any(i => i.OrderId == orderId && i.Username == username);
        }
    }
}
