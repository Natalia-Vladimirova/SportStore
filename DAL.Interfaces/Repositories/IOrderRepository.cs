using DAL.Interfaces.Entities;

namespace DAL.Interfaces.Repositories
{
    public interface IOrderRepository
    {
        Order GetById(int id);

        int Create(Order entity);

        void Delete(int id);

        bool OrderIsValid(int orderId, string username);
    }
}