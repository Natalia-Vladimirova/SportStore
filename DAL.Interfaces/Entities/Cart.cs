using System;
using System.ComponentModel.DataAnnotations;

namespace DAL.Interfaces.Entities
{
    public class Cart
    {
        public int CartId { get; set; }

        public string UserName { get; set; }

        public int Count { get; set; }

        public DateTime DateCreated { get; set; }

        public int ProductId { get; set; }

        public virtual Product Product { get; set; }
    }
}