using System.Linq;
using Store.Models;
using Dal = DAL.Interfaces.Entities;

namespace Store.Mappers
{
    public static class OrderMappers
    {
        public static Dal.Order ToDal(this Order item)
        {
            if (item == null) return null;

            return new Dal.Order
            {
                OrderId = item.OrderId,
                OrderDate = item.OrderDate,
                Username = item.Username,
                FirstName = item.FirstName,
                LastName = item.LastName,
                Address = item.Address,
                City = item.City,
                State = item.State,
                PostalCode = item.PostalCode,
                Country = item.Country,
                Phone = item.Phone,
                Email = item.Email,
                Total = item.Total
            };
        }

        public static Order ToMvc(this Dal.Order item)
        {
            if (item == null) return null;

            return new Order
            {
                OrderId = item.OrderId,
                OrderDate = item.OrderDate,
                Username = item.Username,
                FirstName = item.FirstName,
                LastName = item.LastName,
                Address = item.Address,
                City = item.City,
                State = item.State,
                PostalCode = item.PostalCode,
                Country = item.Country,
                Phone = item.Phone,
                Email = item.Email,
                Total = item.Total
            };
        }
    }
}