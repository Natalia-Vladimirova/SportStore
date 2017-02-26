using System.Collections.Generic;
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

        public IEnumerable<Order> GetAll()
        {
            return context.Set<Order>().ToList();
        }

        public Order GetById(int id)
        {
            return context.Set<Order>().FirstOrDefault(i => i.OrderId == id);
        }

        public IEnumerable<Order> GetUserOrders(string username)
        {
            return context.Set<Order>().Where(i => i.Username == username).ToList();
        }

        public int Create(Order entity)
        {
            context.Set<Order>().Add(entity);
            context.SaveChanges();
            return entity.OrderId;
        }

        public void Delete(int id)
        {
            var order = context.Set<Order>().FirstOrDefault(i => i.OrderId == id);

            if (order == null) return;

            var orderDetails = context.Set<OrderDetail>().Where(i => i.OrderId == id);

            foreach (var orderDetail in orderDetails)
            {
                context.Set<OrderDetail>().Remove(orderDetail);
            }

            context.Set<Order>().Remove(order);
            
            context.SaveChanges();
        }

        public bool OrderIsValid(int orderId, string username)
        {
            return context.Set<Order>().Any(i => i.OrderId == orderId && i.Username == username);
        }
    }
}
