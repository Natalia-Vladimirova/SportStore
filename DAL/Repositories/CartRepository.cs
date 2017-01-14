using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using DAL.Interfaces.Entities;
using DAL.Interfaces.Repositories;

namespace DAL.Repositories
{
    public class CartRepository : ICartRepository
    {
        private readonly DbContext context;

        public CartRepository(DbContext context)
        {
            this.context = context;
        }

        public Cart GetById(int id)
        {
            return context.Set<Cart>()
                .FirstOrDefault(i => i.RecordId == id);
        }

        public Cart GetCart(string shoppingCartId)
        {
            return context.Set<Cart>()
                .FirstOrDefault(i => i.CartId == shoppingCartId);

            /*var cart = new ShoppingCart();
            cart.ShoppingCartId = cart.GetCartId(context);
            return cart;*/
        }

        public void AddToCart(Tovar tovar, string shoppingCartId)
        {
            var cartItem = context.Set<Cart>()
                .SingleOrDefault(c => c.CartId == shoppingCartId && c.TovarId == tovar.TovarId);

            if (cartItem == null)
            {
                // Create a new cart item if no cart item exists
                cartItem = new Cart
                {
                    TovarId = tovar.TovarId,
                    CartId = shoppingCartId,
                    Count = 1,
                    DateCreated = DateTime.Now
                };

                context.Set<Cart>().Add(cartItem);
            }
            else
            {
                // If the item does exist in the cart, 
                // then add one to the quantity
                cartItem.Count++;
            }

            context.SaveChanges();
        }

        public int RemoveFromCart(int id, string shoppingCartId)
        {
            // Get the cart
            var cartItem = context.Set<Cart>()
                .SingleOrDefault(cart => cart.CartId == shoppingCartId && cart.RecordId == id);

            int itemCount = 0;

            if (cartItem != null)
            {
                if (cartItem.Count > 1)
                {
                    cartItem.Count--;
                    itemCount = cartItem.Count;
                }
                else
                {
                    context.Set<Cart>().Remove(cartItem);
                }

                context.SaveChanges();
            }

            return itemCount;
        }

        public void EmptyCart(string shoppingCartId)
        {
            var cartItems = context.Set<Cart>()
                .Where(i => i.CartId == shoppingCartId);

            foreach (var cartItem in cartItems)
            {
                context.Set<Cart>().Remove(cartItem);
            }

            context.SaveChanges();
        }

        public List<Cart> GetCartItems(string shoppingCartId)
        {
            return context.Set<Cart>()
                .Where(cart => cart.CartId == shoppingCartId)
                .ToList();
        }

        public int GetCount(string shoppingCartId)
        {
            // Get the count of each item in the cart and sum them up
            return context.Set<Cart>()
                .Where(i => i.CartId == shoppingCartId)
                .ToList()
                .Sum(i => i.Count);

            //int? count = (from cartItems in storeDB.Carts
            //              where cartItems.CartId == ShoppingCartId
            //              select (int?)cartItems.Count).Sum();
            //// Return 0 if all entries are null
            //return count ?? 0;
        }

        public int GetTotal(string shoppingCartId)
        {
            return context.Set<Cart>()
                .Where(i => i.CartId == shoppingCartId)
                .ToList()
                .Sum(i => i.Count * i.Tovar.Price);

            /*int? total = (from cartItems in storeDB.Carts
                          where cartItems.CartId == ShoppingCartId
                          select cartItems.Count *
                          cartItems.Tovar.Price).Sum();

            return total;*/
        }

        public int CreateOrder(Order order, string shoppingCartId)
        {
            int orderTotal = 0;

            var cartItems = GetCartItems(shoppingCartId);
            // Iterate over the items in the cart, 
            // adding the order details for each
            foreach (var item in cartItems)
            {
                var orderDetail = new OrderDetail
                {
                    TovarId = item.TovarId,
                    OrderId = order.OrderId,
                    UnitPrice = item.Tovar.Price,
                    Quantity = item.Count
                };
                // Set the order total of the shopping cart
                orderTotal += (item.Count * item.Tovar.Price);

                context.Set<OrderDetail>().Add(orderDetail);

            }
            // Set the order's total to the orderTotal count
            order.Total = orderTotal;

            context.SaveChanges();

            // Empty the shopping cart
            EmptyCart(shoppingCartId);

            // Return the OrderId as the confirmation number
            return order.OrderId;
        }

        // When a user has logged in, migrate their shopping cart to be associated with their username
        public void MigrateCart(string userName, string shoppingCartId)
        {
            var shoppingCart = context.Set<Cart>().Where(c => c.CartId == shoppingCartId);

            foreach (Cart item in shoppingCart)
            {
                item.CartId = userName;
            }

            context.SaveChanges();
        }
    }
}
