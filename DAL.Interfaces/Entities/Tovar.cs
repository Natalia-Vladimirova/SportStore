using System.ComponentModel.DataAnnotations;

namespace DAL.Interfaces.Entities
{
    public class Tovar
    {
        public int TovarId { get; set; }

        [Required]
        [StringLength(160)]
        public string Title { get; set; }

        [Required]
        [Range(1, 500000)]
        public int Price { get; set; }

        [Range(1, 1000)]
        public int Amount { get; set; }

        [StringLength(200)]
        public string Description { get; set; }

        [StringLength(200)]
        public string Image { get; set; }

        public int CategId { get; set; }
        
        public virtual Categ Categ { get; set; }
    }
}