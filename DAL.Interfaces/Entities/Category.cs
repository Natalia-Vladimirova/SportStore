using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DAL.Interfaces.Entities
{
    public class Category
    {
        public int CategoryId { get; set; }

        [Required]
        [StringLength(200)]
        public string CategoryName { get; set; }

        public virtual IEnumerable<Product> Products { get; set; }
    }
}