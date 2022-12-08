using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NeoSistem.MakinaTurkiye.Web.App_Start
{
    public class LanguageDetector : ActionFilterAttribute
    {

         public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (filterContext.RequestContext.HttpContext.Request.Url.AbsolutePath.ToString().ToLower().EndsWith("-tr"))
            {
                var context = filterContext.RequestContext.HttpContext;
                HttpCookie cookie = context.Request.Cookies.Get("CacheLang");
                if (cookie == null)
                {
                    cookie = new HttpCookie("CacheLang");
                }
                cookie.Value = "tr_TR";
                context.Response.Cookies.Add(cookie);
            }
            else if (filterContext.RequestContext.HttpContext.Request.Url.AbsolutePath.ToString().ToLower().EndsWith("-en"))
            {
                var context = filterContext.RequestContext.HttpContext;
                HttpCookie cookie = context.Request.Cookies.Get("CacheLang");
                if (cookie == null)
                {
                    cookie = new HttpCookie("CacheLang");
                }
                cookie.Value = "en_EN";
                context.Response.Cookies.Add(cookie);
            }
            else if (filterContext.RequestContext.HttpContext.Request.Url.AbsolutePath.ToString().ToLower().EndsWith("-fr"))
            {
                var context = filterContext.RequestContext.HttpContext;
                HttpCookie cookie = context.Request.Cookies.Get("CacheLang");
                if (cookie == null)
                {
                    cookie = new HttpCookie("CacheLang");
                }
                cookie.Value = "fr_FR";
                context.Response.Cookies.Add(cookie);
            }
            else
            {
                var context = filterContext.RequestContext.HttpContext;
                HttpCookie cookie = context.Request.Cookies.Get("CacheLang");
                if (cookie == null)
                {
                    cookie = new HttpCookie("CacheLang");
                }
                cookie.Value = "tr_TR";
                context.Response.Cookies.Add(cookie);
            }
        }
    }
}