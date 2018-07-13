using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace WebApplication1test1.CustomHtmlHelpers
{
    public static class CustomHtmlHelpers
    {
        public static IHtmlString Image(this HtmlHelper helper, string src, string alt) // added after lesson 100
        {
            return Image(helper, src, alt, null);
        }

        public static IHtmlString Image(this HtmlHelper helper, string src, string alt, object htmlAttributes)
        {
            TagBuilder tagBuilder = new TagBuilder("img");
            tagBuilder.Attributes.Add("src", VirtualPathUtility.ToAbsolute(src));
            tagBuilder.Attributes.Add("alt", alt);
            tagBuilder.MergeAttributes(new RouteValueDictionary(htmlAttributes)); // added after lesson 100

            //lesson 49 was informational about html encoding and why not return a normal string of the TagBuilder instance
            return new MvcHtmlString(tagBuilder.ToString(TagRenderMode.SelfClosing));
        }
    }
}