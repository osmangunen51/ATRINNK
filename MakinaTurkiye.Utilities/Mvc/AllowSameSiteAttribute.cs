using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace MakinaTurkiye.Utilities.Mvc
{
    public class AllowSameSiteAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var response = filterContext.RequestContext.HttpContext.Response;

            var domains = new List<string> { "magaza.makinaturkiye.com", "makinaturkiye.com","urun.makinaturkiye.com","video.makinaturkiye.com" };
            if (filterContext.RequestContext.HttpContext.Request.UrlReferrer!=null && domains.Contains(filterContext.RequestContext.HttpContext.Request.UrlReferrer.Host))
=======
            if (filterContext.RequestContext.HttpContext.Request.UrlReferrer != null)
>>>>>>> 037a3a3edd97e1d5c3e6d77f48e77aee339a4019
            {
                if (domains.Contains(filterContext.RequestContext.HttpContext.Request.UrlReferrer.Host))
                {
                    filterContext.RequestContext.HttpContext.Response.AddHeader("Access-Control-Allow-Origin", "*");
                }
                filterContext.RequestContext.HttpContext.Response.AddHeader("Access-Control-Allow-Headers", "*");
                filterContext.RequestContext.HttpContext.Response.AddHeader("Access-Control-Allow-Credentials", "true");

                if (response != null)
                {
                    response.AddHeader("Set-Cookie", "HttpOnly;Secure;SameSite=None");
                }
            }
            base.OnActionExecuting(filterContext);
        }
    }
}
