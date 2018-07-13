using System.Web.Mvc;
using System.Web.Routing;

namespace WebApplication1test1
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            // for lesson 52
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/",
                defaults: new { controller = "Home", action = "Index" }
            );

            //routes.MapRoute(
            //    name: "Default",
            //    url: "{controller}/{action}/{id}/{newtext}",
            //    defaults: new { controller = "Crazy"/*was before lesson 25 "Home"*/, action = "ShowAll", id = UrlParameter.Optional, newtext = "you haven't entered a text yet" }
            //);

            //routes.MapRoute(
            //    name: "Default",
            //    url: "{controller}/{action}",
            //    defaults: new { controller = "Class1", action = "E"}
            //);

            //didn't work as expected
            //routes.MapRoute(
            //    name: "EController",
            //    url: "{controller}/E/{action}",
            //    defaults: new { controller = "Class1", action = "Create"}
            //);


            // added with to solve the /E/ alongside the nuget package attributeRoute
            routes.MapMvcAttributeRoutes();
        }
    }
}