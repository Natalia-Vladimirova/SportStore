using Store.Models;
using Dal = DAL.Interfaces.Entities;

namespace Store.Mappers
{
    public static class TovarMappers
    {
        public static Dal.Tovar ToDal(this Tovar item)
        {
            if (item == null) return null;

            return new Dal.Tovar
            {
                TovarId = item.TovarId,
                Title = item.Title,
                Price = item.Price,
                Amount = item.Amount,
                Description = item.Description,
                Image = item.Image,
                CategId = item.CategId
            };
        }

        public static Tovar ToMvc(this Dal.Tovar item)
        {
            if (item == null) return null;

            return new Tovar
            {
                TovarId = item.TovarId,
                Title = item.Title,
                Price = item.Price,
                Amount = item.Amount,
                Description = item.Description,
                Image = item.Image,
                CategId = item.CategId,
                Categ = item.Categ.ToMvc()
            };
        }
    }
}