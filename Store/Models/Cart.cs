using System;
using System.ComponentModel.DataAnnotations;

namespace Store.Models
{
    public class Cart
    {
        [Key]
        public int RecordId { get; set; }

        public string CartId { get; set; }

        public int Count { get; set; }

        public DateTime DateCreated { get; set; }

        public int TovarId { get; set; }

        public virtual Tovar Tovar { get; set; }
    }
}