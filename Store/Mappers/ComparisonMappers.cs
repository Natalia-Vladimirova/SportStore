using Store.Models;
using Dal = DAL.Interfaces.Entities;

namespace Store.Mappers
{
    public static class ComparisonMappers
    {
        public static Dal.Comparison ToDal(this Comparison item)
        {
            if (item == null) return null;

            return new Dal.Comparison
            {
                ComparisonId = item.ComparisonId,
                UserName = item.UserName,
                ProductId = item.ProductId
            };
        }

        public static Comparison ToMvc(this Dal.Comparison item)
        {
            if (item == null) return null;

            return new Comparison
            {
                ComparisonId = item.ComparisonId,
                UserName = item.UserName,
                ProductId = item.ProductId,
                Product = item.Product.ToMvc()
            };
        }
    }
}