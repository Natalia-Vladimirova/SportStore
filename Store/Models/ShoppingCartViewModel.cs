using System.Collections.Generic;

namespace Store.Models
{
    public class ShoppingCartViewModel
    {
        public IEnumerable<Cart> CartItems { get; set; }

        public int CartTotal { get; set; }
    }
}