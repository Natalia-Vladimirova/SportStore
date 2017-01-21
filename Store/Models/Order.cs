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

        [Required(ErrorMessage = "Введите имя")]
        [DisplayName("Имя")]
        [StringLength(160)]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Введите фамилию")]
        [DisplayName("Фамилия")]
        [StringLength(160)]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Введите адрес")]
        [StringLength(100)]
        [DisplayName("Улица")]
        public string Address { get; set; }

        [Required(ErrorMessage = "Введите город")]
        [StringLength(50)]
        [DisplayName("Город")]
        public string City { get; set; }

        [Required(ErrorMessage = "Введите район")]
        [StringLength(50)]
        [DisplayName("Район")]
        public string State { get; set; }

        [Required(ErrorMessage = "Требуется почтовый индекс")]
        [DisplayName("Почтовый индекс")]
        [StringLength(6)]
        public string PostalCode { get; set; }

        [Required(ErrorMessage = "Введите страну")]
        [StringLength(50)]
        [DisplayName("Страна")]
        public string Country { get; set; }

        [Required(ErrorMessage = "Введите номер телефона")]
        [StringLength(13)]
        [DisplayName("Телефон")]
        public string Phone { get; set; }

        [Required(ErrorMessage = "Введите Email")]
        [DisplayName("Email")]
        [RegularExpression(@"[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}",ErrorMessage = "Неправильный Email")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [ScaffoldColumn(false)]
        public int Total { get; set; }

        public IEnumerable<OrderDetail> OrderDetails { get; set; }
    }
}