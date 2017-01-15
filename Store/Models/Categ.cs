using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Store.Models
{
    public class Categ
    {
        public int CategId { get; set; }

        [Required(ErrorMessage = "Введите категорию")]
        [StringLength(200)]
        public string Category { get; set; }

        public virtual List<Tovar> Tovars { get; set; }
    }
}