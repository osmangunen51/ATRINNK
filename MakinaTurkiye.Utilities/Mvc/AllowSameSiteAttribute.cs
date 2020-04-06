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

            if (response != null)
            {
                response.AddHeader("Set-Cookie", "HttpOnly;Secure;SameSite=None");
            }

            base.OnActionExecuting(filterContext);
        }
    }
}
