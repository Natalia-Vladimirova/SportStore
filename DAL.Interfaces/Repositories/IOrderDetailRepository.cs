using System.Collections.Generic;
using DAL.Interfaces.Entities;

namespace DAL.Interfaces.Repositories
{
    public interface IOrderDetailRepository
    {
        IEnumerable<OrderDetail> GetAll();

        OrderDetail GetById(int id);

        IEnumerable<OrderDetail> GetUserOrders(string username);

        IEnumerable<OrderDetail> GetByOrderId(int orderId);
    }
}