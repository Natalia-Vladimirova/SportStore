using Store.Models;
using Dal = DAL.Interfaces.Entities;

namespace Store.Mappers
{
    public static class ProductMappers
    {
        public static Dal.Product ToDal(this Product item)
        {
            if (item == null) return null;

            return new Dal.Product
            {
                ProductId = item.ProductId,
                Title = item.Title,
                Price = item.Price,
                Description = item.Description,
                Image = item.Image,
                CategoryId = item.CategoryId
            };
        }

        public static Product ToMvc(this Dal.Product item)
        {
            if (item == null) return null;

            return new Product
            {
                ProductId = item.ProductId,
                Title = item.Title,
                Price = item.Price,
                Description = item.Description,
                Image = item.Image,
                CategoryId = item.CategoryId,
                Category = item.Category.ToMvc()
            };
        }
    }
}