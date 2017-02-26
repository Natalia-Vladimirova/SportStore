using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Store.Models
{
    [Bind(Exclude = "OrderId")]
    public class Order
    {
        [ScaffoldColumn(false)]
        public int OrderId { get; set; }

        [ScaffoldColumn(false)]
        public DateTime OrderDate { get; set; }

        [ScaffoldColumn(false)]
        public string Username { get; set; }

        [Required(ErrorMessage = "First name is required")]
        [DisplayName("First name")]
        [StringLength(160)]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last name is required")]
        [DisplayName("Last name")]
        [StringLength(160)]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Address is required")]
        [StringLength(100)]
        [DisplayName("Address")]
        public string Address { get; set; }

        [Required(ErrorMessage = "City is required")]
        [StringLength(50)]
        [DisplayName("City")]
        public string City { get; set; }

        [Required(ErrorMessage = "District is required")]
        [StringLength(50)]
        [DisplayName("District")]
        public string State { get; set; }

        [Required(ErrorMessage = "Post-code is required")]
        [DisplayName("Post-code")]
        [StringLength(6)]
        public string PostalCode { get; set; }

        [Required(ErrorMessage = "Country is required")]
        [StringLength(50)]
        [DisplayName("Country")]
        public string Country { get; set; }

        [Required(ErrorMessage = "Phone is required")]
        [StringLength(13)]
        [DisplayName("Phone")]
        public string Phone { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [DisplayName("Email")]
        [RegularExpression(@"^([a-zA-Z0-9_.-])+@([a-zA-Z0-9_.-])+\.[a-zA-Z]+$", ErrorMessage = "Entered email is incorrect")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [ScaffoldColumn(false)]
        public int Total { get; set; }

        public IEnumerable<OrderDetail> OrderDetails { get; set; }
    }
}