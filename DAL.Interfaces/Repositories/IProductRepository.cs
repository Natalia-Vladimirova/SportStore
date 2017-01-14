using DAL.Interfaces.Entities;

namespace DAL.Interfaces.Repositories
{
    public interface IProductRepository //: IRepository<Cart>
    {
        Tovar GetById(int id);
    }
}