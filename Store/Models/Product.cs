using System.ComponentModel.DataAnnotations;

namespace Store.Models
{
    public class Product
    {
        public int ProductId { get; set; }

        [Required(ErrorMessage = "Требуется название товара")]
        [StringLength(160)]
        public string Title { get; set; }

        [Required(ErrorMessage = "Введите цену")]
        [Range(1, 500000, ErrorMessage = "Цена должна быть от 1 до 500000 руб.")]
        public int Price { get; set; }

        [Required(ErrorMessage = "Введите количество")]
        [Range(1, 1000, ErrorMessage = "Количество должно быть от 1 до 1000 штук(метров)")]
        public int Amount { get; set; }

        [StringLength(200)]
        public string Description { get; set; }

        [StringLength(200)]
        public string Image { get; set; }

        public int CategoryId { get; set; }

        public virtual Category Category { get; set; }
    }
}