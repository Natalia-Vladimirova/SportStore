using System.Collections.Generic;
using DAL.Interfaces.Entities;

namespace DAL.Interfaces.Repositories
{
    public interface IOrderRepository
    {
        IEnumerable<Order> GetAll();

        Order GetById(int id);

        IEnumerable<Order> GetUserOrders(string username);

        int Create(Order entity);

        void Delete(int id);

        bool OrderIsValid(int orderId, string username);
    }
}