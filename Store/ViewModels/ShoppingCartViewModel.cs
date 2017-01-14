using System.Collections.Generic;
using Store.Models;

namespace Store.ViewModels
{
    public class ShoppingCartViewModel
    {
        public List<Cart> CartItems { get; set; }
        public int CartTotal { get; set; }
    }
}