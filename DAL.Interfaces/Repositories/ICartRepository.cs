using System.Collections.Generic;
using DAL.Interfaces.Entities;

namespace DAL.Interfaces.Repositories
{
    public interface ICartRepository
    {
        Cart GetById(int id);

        Cart GetCart(string shoppingCartId);

        void AddToCart(Product product, string shoppingCartId);

        int RemoveFromCart(int id, string shoppingCartId);
        
        IEnumerable<Cart> GetCartItems(string shoppingCartId);

        int GetCount(string shoppingCartId);

        int GetTotal(string shoppingCartId);

        int CreateOrder(Order order, string shoppingCartId);

        void MigrateCart(string userName, string shoppingCartId);
    }
}