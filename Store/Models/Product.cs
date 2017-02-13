using System.ComponentModel.DataAnnotations;

namespace Store.Models
{
    public class Product
    {
        public int ProductId { get; set; }

        [Required(ErrorMessage = "Product title is required")]
        [StringLength(160)]
        public string Title { get; set; }

        [Required(ErrorMessage = "Price is required")]
        [Range(1, 500000, ErrorMessage = "Price must be between 1 and 500000")]
        public int Price { get; set; }
        
        [StringLength(200)]
        public string Description { get; set; }

        [StringLength(200)]
        public string Image { get; set; }

        public int CategoryId { get; set; }

        public Category Category { get; set; }
    }
}