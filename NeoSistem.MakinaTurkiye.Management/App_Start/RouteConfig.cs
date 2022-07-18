using System.Web.Mvc;
using System.Web.Routing;

namespace NeoSistem.MakinaTurkiye.Management.App_Start
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.IgnoreRoute("{*jpg}", new { jpg = @".*\.jpg(/.*)?" });
            routes.IgnoreRoute("{*gif}", new { gif = @".*\.gif(/.*)?" });
            routes.IgnoreRoute("{*png}", new { png = @".*\.png(/.*)?" });
            routes.IgnoreRoute("{*jpeg}", new { jpg = @".*\.jpeg(/.*)?" });
            routes.IgnoreRoute("{*ico}", new { ico = @".*\.ico(/.*)?" });
            routes.IgnoreRoute("{*bmp}", new { bmp = @".*\.bmp(/.*)?" });
            routes.IgnoreRoute("{*swf}", new { swf = @".*\.swf(/.*)?" });
            routes.LowercaseUrls = true;
            routes.MapRoute(
              "Default", // Route name
              "{controller}/{action}/{id}", // URL with parameters
              new { controller = "Home", action = "Index", id = UrlParameter.Optional } // Parameter defaults
            );
            routes.MapRoute("WithTarget", "{controller}/{action}/{id}#{target}");
            routes.MapRoute("Login", "{controller}/{action}", new { controller = "Account", action = "Login" });
        }
    }
}