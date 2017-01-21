using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Store.Models
{
    public class Category
    {
        public int CategoryId { get; set; }

        [Required(ErrorMessage = "Enter a category name")]
        [StringLength(200)]
        public string CategoryName { get; set; }

        public IEnumerable<Product> Products { get; set; }
    }
}