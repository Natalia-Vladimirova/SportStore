using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Store.Models
{
    public class Category
    {
        public int CategoryId { get; set; }

        [Required(ErrorMessage = "Введите категорию")]
        [StringLength(200)]
        public string CategoryName { get; set; }

        public virtual List<Product> Products { get; set; }
    }
}