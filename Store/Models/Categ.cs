using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Store.Models
{
    public partial class Categ
    {
        public int CategId { get; set; }
        [Required(ErrorMessage = "Введите категорию")]
        [StringLength(200)]
        public string Category { get; set; }
        public List<Tovar> Tovars { get; set; }
    }
}