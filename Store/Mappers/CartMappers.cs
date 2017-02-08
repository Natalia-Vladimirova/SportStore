using Store.Models;
using Dal = DAL.Interfaces.Entities;

namespace Store.Mappers
{
    public static class CartMappers
    {
        public static Dal.Cart ToDal(this Cart item)
        {
            if (item == null) return null;

            return new Dal.Cart
            {
                CartId = item.CartId,
                UserName = item.UserName,
                ProductId = item.ProductId,
                Count = item.Count,
                DateCreated = item.DateCreated
            };
        }

        public static Cart ToMvc(this Dal.Cart item)
        {
            if (item == null) return null;

            return new Cart
            {
                CartId = item.CartId,
                UserName = item.UserName,
                ProductId = item.ProductId,
                Count = item.Count,
                DateCreated = item.DateCreated,
                Product = item.Product.ToMvc()
            };
        }
    }
}