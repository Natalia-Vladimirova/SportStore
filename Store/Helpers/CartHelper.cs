using System;
using System.Web;
using DAL;
using DAL.Interfaces.Repositories;
using DAL.Repositories;

namespace Store.Helpers
{
    public class CartHelper
    {
        private const string CartSessionKey = "CartId";
        private static readonly ICartRepository cartRepository = new CartRepository(new StoreDbContext());

        public static string GetCartId(HttpContextBase context)
        {
            if (context.Session[CartSessionKey] == null)
            {
                if (!string.IsNullOrWhiteSpace(context.User.Identity.Name))
                {
                    context.Session[CartSessionKey] = context.User.Identity.Name;
                }
                else
                {
                    // Generate a new random GUID using System.Guid class
                    Guid tempCartId = Guid.NewGuid();
                    // Send tempCartId back to client as a cookie
                    context.Session[CartSessionKey] = tempCartId.ToString();
                }
            }
            return context.Session[CartSessionKey].ToString();
        }

        public static void MigrateCartWhenAuthorizing(HttpContextBase context, string username)
        {
            if (string.IsNullOrWhiteSpace(username)) return;

            var cartId = GetCartId(context);

            if (cartId != username)
            {
                cartRepository.MigrateCart(username, cartId);
                context.Session[CartSessionKey] = username;
            }
        }

        public static void RemoveSessionCartIdOnLogout(HttpContextBase context)
        {
            context.Session[CartSessionKey] = null;
        }
    }
}