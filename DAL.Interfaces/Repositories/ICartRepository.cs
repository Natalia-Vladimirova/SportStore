using System.Collections.Generic;
using DAL.Interfaces.Entities;

namespace DAL.Interfaces.Repositories
{
    public interface ICartRepository //: IRepository<Cart>
    {
        Cart GetById(int id);

        Cart GetCart(string shoppingCartId);

        void AddToCart(Tovar tovar, string shoppingCartId);

        int RemoveFromCart(int id, string shoppingCartId);

        void EmptyCart(string shoppingCartId);

        List<Cart> GetCartItems(string shoppingCartId);

        int GetCount(string shoppingCartId);

        int GetTotal(string shoppingCartId);

        int CreateOrder(Order order, string shoppingCartId);

        void MigrateCart(string userName, string shoppingCartId);
    }
}