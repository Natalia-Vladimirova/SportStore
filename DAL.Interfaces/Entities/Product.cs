using System.ComponentModel.DataAnnotations;

namespace DAL.Interfaces.Entities
{
    public class Product
    {
        public int ProductId { get; set; }

        [Required]
        [StringLength(160)]
        public string Title { get; set; }

        [Required]
        [Range(1, 500000)]
        public int Price { get; set; }
        
        [StringLength(200)]
        public string Description { get; set; }

        [StringLength(200)]
        public string Image { get; set; }

        public int CategoryId { get; set; }
        
        public virtual Category Category { get; set; }
    }
}