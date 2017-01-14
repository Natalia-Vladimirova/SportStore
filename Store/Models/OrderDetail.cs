﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Store.Models
{
    public class OrderDetail
    {
        public int OrderDetailId { get; set; }
        public int OrderId { get; set; }
        public int TovarId { get; set; }
        public int Quantity { get; set; }
        public int UnitPrice { get; set; }
        public virtual Tovar Tovar { get; set; }
        public virtual Order Order { get; set; }
    }
}