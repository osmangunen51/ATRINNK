using System.Web.Http;
using System.Web.Http.Cors;

namespace Trinnk.Api
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            //#region CORS hatası için...
            ////Normalde diğer projeler için gerekmeye bilir ama burada test ederken CORS hatasına düşmeyecek.
            //var enableCorsAttribute = new EnableCorsAttribute("*",
            //                                   "Origin, Content-Type, Accept",
            //                                   "GET, PUT, POST, DELETE, OPTIONS");

            //config.EnableCors(enableCorsAttribute);
            //#endregion

            // Web API configuration and services

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
            config.IncludeErrorDetailPolicy = IncludeErrorDetailPolicy.Always;
        }
    }
}
