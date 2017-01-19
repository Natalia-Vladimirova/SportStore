using Store.Models;
using Dal = DAL.Interfaces.Entities;

namespace Store.Mappers
{
    public static class CategoryMappers
    {
        public static Dal.Category ToDal(this Category item)
        {
            if (item == null) return null;

            return new Dal.Category
            {
                CategoryId = item.CategoryId,
                CategoryName = item.CategoryName
            };
        }

        public static Category ToMvc(this Dal.Category item)
        {
            if (item == null) return null;

            return new Category
            {
                CategoryId = item.CategoryId,
                CategoryName = item.CategoryName
            };
        }
    }
}