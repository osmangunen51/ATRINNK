using System.Web.Http;

namespace MakinaTurkiye.Api
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API routes
            config.MapHttpAttributeRoutes();
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "{controller}/{action}/{No}",
                defaults: new
                {
                    No = RouteParameter.Optional
                }
            );

            config.Routes.MapHttpRoute(
                name: "DefaultApi2",
                routeTemplate: "{controller}/{action}/{PrmTxt}",
                defaults: new
                {
                    PrmTxt = RouteParameter.Optional
                }
            );
        }
    }
}
