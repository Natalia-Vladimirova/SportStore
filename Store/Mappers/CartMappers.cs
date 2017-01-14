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
                RecordId = item.RecordId,
                CartId = item.CartId,
                TovarId = item.TovarId,
                Count = item.Count,
                DateCreated = item.DateCreated
            };
        }

        public static Cart ToMvc(this Dal.Cart item)
        {
            if (item == null) return null;

            return new Cart
            {
                RecordId = item.RecordId,
                CartId = item.CartId,
                TovarId = item.TovarId,
                Count = item.Count,
                DateCreated = item.DateCreated,
                Tovar = item.Tovar.ToMvc()
            };
        }
    }
}