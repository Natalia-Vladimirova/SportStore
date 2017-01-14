using Store.Models;
using Dal = DAL.Interfaces.Entities;

namespace Store.Mappers
{
    public static class OrderDetailMappers
    {
        public static Dal.OrderDetail ToDal(this OrderDetail item)
        {
            if (item == null) return null;

            return new Dal.OrderDetail
            {
                OrderDetailId = item.OrderDetailId,
                Quantity = item.Quantity,
                UnitPrice = item.UnitPrice,
                TovarId = item.TovarId,
                OrderId = item.OrderId
            };
        }

        public static OrderDetail ToMvc(this Dal.OrderDetail item)
        {
            if (item == null) return null;

            return new OrderDetail
            {
                OrderDetailId = item.OrderDetailId,
                Quantity = item.Quantity,
                UnitPrice = item.UnitPrice,
                TovarId = item.TovarId,
                OrderId = item.OrderId,
                Tovar = item.Tovar.ToMvc(),
                Order = item.Order.ToMvc()
            };
        }
    }
}