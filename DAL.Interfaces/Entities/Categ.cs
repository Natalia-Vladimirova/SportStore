using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DAL.Interfaces.Entities
{
    public class Categ
    {
        public int CategId { get; set; }

        [Required]
        public string Category { get; set; }

        public List<Tovar> Tovars { get; set; }
    }
}