using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DAL.Interfaces.Entities
{
    public class Order
    {
        public int OrderId { get; set; }

        public DateTime OrderDate { get; set; }

        public string Username { get; set; }

        [Required]
        [StringLength(160)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(160)]
        public string LastName { get; set; }

        [Required]
        [StringLength(100)]
        public string Address { get; set; }

        [Required]
        [StringLength(50)]
        public string City { get; set; }

        [Required]
        [StringLength(50)]
        public string State { get; set; }

        [Required]
        [StringLength(6)]
        public string PostalCode { get; set; }

        [Required]
        [StringLength(50)]
        public string Country { get; set; }

        [Required]
        [StringLength(13)]
        public string Phone { get; set; }

        [Required]
        public string Email { get; set; }

        public int Total { get; set; }

        public virtual IEnumerable<OrderDetail> OrderDetails { get; set; }
    }
}