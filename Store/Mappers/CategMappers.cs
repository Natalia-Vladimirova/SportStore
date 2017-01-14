using Store.Models;
using Dal = DAL.Interfaces.Entities;

namespace Store.Mappers
{
    public static class CategMappers
    {
        public static Dal.Categ ToDal(this Categ item)
        {
            if (item == null) return null;

            return new Dal.Categ
            {
                CategId = item.CategId,
                Category = item.Category
            };
        }

        public static Categ ToMvc(this Dal.Categ item)
        {
            if (item == null) return null;

            return new Categ
            {
                CategId = item.CategId,
                Category = item.Category
            };
        }
    }
}