using System.Collections.Generic;
using System.Web.Mvc;

namespace Trinnk.Utilities.Mvc
{
    public class AllowSameSiteAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var response = filterContext.RequestContext.HttpContext.Response;

            var domains = new List<string> { "magaza.trinnk.com", "trinnk.com", "urun.trinnk.com", "video.trinnk.com" };

            if (filterContext.RequestContext.HttpContext.Request.UrlReferrer != null && domains.Contains(filterContext.RequestContext.HttpContext.Request.UrlReferrer.Host))
            {
                if (domains.Contains(filterContext.RequestContext.HttpContext.Request.UrlReferrer.Host))
                {
                    filterContext.RequestContext.HttpContext.Response.AddHeader("Access-Control-Allow-Origin", "*");
                    filterContext.RequestContext.HttpContext.Response.AddHeader("Access-Control-Allow-Methods", "*");
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

    public class AllowAllSiteAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var ctx = filterContext.RequestContext.HttpContext;
            var origin = ctx.Request.Headers["Origin"];
            var allowOrigin = !string.IsNullOrWhiteSpace(origin) ? origin : "*";
            ctx.Response.AppendHeader("Access-Control-Allow-Origin", "*");
            ctx.Response.AppendHeader("Access-Control-Allow-Headers", "*");
            ctx.Response.AppendHeader("Access-Control-Allow-Methods", "*");

            ctx.Response.AppendHeader("Set-Cookie", "HttpOnly;Secure;SameSite=None");
            ctx.Response.AppendHeader("Access-Control-Allow-Credentials", "true");
            base.OnActionExecuting(filterContext);
        }
    }
}
