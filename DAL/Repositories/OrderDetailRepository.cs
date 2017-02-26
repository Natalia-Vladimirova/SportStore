using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using DAL.Interfaces.Entities;
using DAL.Interfaces.Repositories;

namespace DAL.Repositories
{
    public class OrderDetailRepository : IOrderDetailRepository
    {
        private readonly DbContext context;

        public OrderDetailRepository(DbContext context)
        {
            this.context = context;
        }

        public IEnumerable<OrderDetail> GetAll()
        {
            return context.Set<OrderDetail>().ToList();
        }

        public OrderDetail GetById(int id)
        {
            return context.Set<OrderDetail>().FirstOrDefault(i => i.OrderDetailId == id);
        }

        public IEnumerable<OrderDetail> GetByOrderId(int orderId)
        {
            return context.Set<OrderDetail>().Where(i => i.OrderId == orderId).ToList();
        }
    }
}
