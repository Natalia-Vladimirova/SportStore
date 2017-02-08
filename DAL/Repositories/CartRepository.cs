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
                .FirstOrDefault(i => i.CartId == id);
        }

        public Cart GetCart(string shoppingCartId)
        {
            return context.Set<Cart>()
                .FirstOrDefault(i => i.UserName == shoppingCartId);
        }

        public void AddToCart(Product product, string shoppingCartId)
        {
            var cartItem = context.Set<Cart>()
                .SingleOrDefault(c => c.UserName == shoppingCartId && c.ProductId == product.ProductId);

            if (cartItem == null)
            {
                cartItem = new Cart
                {
                    ProductId = product.ProductId,
                    UserName = shoppingCartId,
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
                .SingleOrDefault(cart => cart.UserName == shoppingCartId && cart.CartId == id);

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
                .Where(i => i.UserName == shoppingCartId);

            foreach (var cartItem in cartItems)
            {
                context.Set<Cart>().Remove(cartItem);
            }

            context.SaveChanges();
        }

        public IEnumerable<Cart> GetCartItems(string shoppingCartId)
        {
            return context.Set<Cart>()
                .Where(cart => cart.UserName == shoppingCartId)
                .ToList();
        }

        public int GetCount(string shoppingCartId)
        {
            return context.Set<Cart>()
                .Where(i => i.UserName == shoppingCartId)
                .ToList()
                .Sum(i => i.Count);
        }

        public int GetTotal(string shoppingCartId)
        {
            return context.Set<Cart>()
                .Where(i => i.UserName == shoppingCartId)
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

        public void MigrateCart(string userName, string shoppingCartId)
        {
            var shoppingCart = context.Set<Cart>().Where(c => c.UserName == shoppingCartId);
            var existingUserCart = context.Set<Cart>().Where(c => c.UserName == userName).ToList();

            foreach (Cart item in shoppingCart)
            {
                var existingCartProduct = existingUserCart.FirstOrDefault(i => i.ProductId == item.ProductId);

                if (existingCartProduct == null)
                {
                    item.UserName = userName;
                }
                else
                {
                    existingCartProduct.Count += item.Count;
                    context.Set<Cart>().Remove(item);
                }
            }

            context.SaveChanges();
        }
    }
}
