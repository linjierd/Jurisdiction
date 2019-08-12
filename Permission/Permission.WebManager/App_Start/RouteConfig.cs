
using System.Web.Mvc;
using System.Web.Routing;

namespace Permission.WebManager
{
    public class RouteConfig
    {
 
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.MapRoute(
            "Login", // Route name
             "login", // URL with parameters
             defaults:new { controller = "Common", action = "Login", id = UrlParameter.Optional }  // Parameter defaults
            );
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                 defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );

        }
    }
}
