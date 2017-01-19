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
        }

        public void AddToCart(Product product, string shoppingCartId)
        {
            var cartItem = context.Set<Cart>()
                .SingleOrDefault(c => c.CartId == shoppingCartId && c.ProductId == product.ProductId);

            if (cartItem == null)
            {
                cartItem = new Cart
                {
                    ProductId = product.ProductId,
                    CartId = shoppingCartId,
                    Count = 1,
                    DateCreated = DateTime.Now
                };

                context.Set<Cart>().Add(cartItem);
            }
            else
            {
                cartItem.Count++;
            }

            context.SaveChanges();
        }

        public int RemoveFromCart(int id, string shoppingCartId)
        {
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

        public IEnumerable<Cart> GetCartItems(string shoppingCartId)
        {
            return context.Set<Cart>()
                .Where(cart => cart.CartId == shoppingCartId)
                .ToList();
        }

        public int GetCount(string shoppingCartId)
        {
            return context.Set<Cart>()
                .Where(i => i.CartId == shoppingCartId)
                .ToList()
                .Sum(i => i.Count);
        }

        public int GetTotal(string shoppingCartId)
        {
            return context.Set<Cart>()
                .Where(i => i.CartId == shoppingCartId)
                .ToList()
                .Sum(i => i.Count * i.Product.Price);
        }

        public int CreateOrder(Order order, string shoppingCartId)
        {
            int orderTotal = 0;

            var cartItems = GetCartItems(shoppingCartId);

            // Iterate over the items in the cart, adding the order details for each
            foreach (var item in cartItems)
            {
                var orderDetail = new OrderDetail
                {
                    ProductId = item.ProductId,
                    OrderId = order.OrderId,
                    UnitPrice = item.Product.Price,
                    Quantity = item.Count
                };

                // Set the order total of the shopping cart
                orderTotal += item.Count * item.Product.Price;

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
