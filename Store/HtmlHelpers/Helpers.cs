using System.Web.Mvc;

namespace Store.HtmlHelpers
{
    public static class Helpers
    {
        public static MvcHtmlString Truncate(this HtmlHelper html, string input, int length)
        {
            if (input.Length <= length)
            {
                return MvcHtmlString.Create(input);
            }
            return MvcHtmlString.Create($"{input.Substring(0, length)}...");
        }
    }
}