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
                CategId = item.CategId,
                Title = item.Title,
                Price = item.Price,
                Amount = item.Amount,
                Description = item.Description,
                Image = item.Image
            };
        }

        public static Tovar ToMvc(this Dal.Tovar item)
        {
            if (item == null) return null;

            return new Tovar
            {
                TovarId = item.TovarId,
                CategId = item.CategId,
                Title = item.Title,
                Price = item.Price,
                Amount = item.Amount,
                Description = item.Description,
                Image = item.Image,
                Categ = item.Categ.ToMvc()
            };
        }
    }
}