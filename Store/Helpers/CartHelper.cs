using System;
using System.Web;
using DAL.Interfaces.Repositories;

namespace Store.Helpers
{
    public class CartHelper
    {
        private const string CartSessionKey = "CartId";
        private readonly ICartRepository cartRepository;
        private readonly IComparisonRepository comparisonRepository;

        public CartHelper(ICartRepository cartRepository, IComparisonRepository comparisonRepository)
        {
            this.cartRepository = cartRepository;
            this.comparisonRepository = comparisonRepository;
        }

        public static string GetCartId(HttpContextBase context)
        {
            if (context?.Session == null) return null;

            if (context.Session[CartSessionKey] == null)
            {
                if (!string.IsNullOrWhiteSpace(context.User.Identity.Name))
                {
                    context.Session[CartSessionKey] = context.User.Identity.Name;
                }
                else
                {
                    Guid tempCartId = Guid.NewGuid();
                    context.Session[CartSessionKey] = tempCartId.ToString();
                }
            }
            return context.Session[CartSessionKey].ToString();
        }

        public void MigrateCartWhenAuthorizing(HttpContextBase context, string username)
        {
            if (context?.Session == null) return;
            if (string.IsNullOrWhiteSpace(username)) return;

            var cartId = GetCartId(context);

            if (cartId != username)
            {
                cartRepository.MigrateCart(username, cartId);
                comparisonRepository.MigrateComparisons(username, cartId);
                context.Session[CartSessionKey] = username;
            }
        }

        public void RemoveSessionCartIdOnLogout(HttpContextBase context)
        {
            if (context?.Session == null) return;
            context.Session[CartSessionKey] = null;
        }
    }
}