using System.Web.Http;
using System.Web.Mvc;

namespace MakinaTurkiye.Api.Areas.Yardim
{
    public class YardimAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "Yardim";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "Yardim_Default",
                "Yardim/{action}/{apiId}",
                new { controller = "Yardim", action = "Index", apiId = UrlParameter.Optional });

            YardimConfig.Register(GlobalConfiguration.Configuration);
        }
    }
}