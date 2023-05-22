using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace NeoSistem.Trinnk
{
    public static class UrlHelperExtensions
    {
        /// <summary>
        /// Generates an absolute path from the specified relative path
        /// </summary>
        public static string GetAbsoluteUrl(this UrlHelper helper, string relativepath)
        {
            if (HttpContext.Current != null)
            {
                return HttpContext.Current.Request.Url.Scheme + "://" + HttpContext.Current.Request.Headers["Host"] + relativepath;
            }
            return relativepath;
        }

        public static string GetCurrentAbsoluteUrl(this UrlHelper helper)
        {
            if (HttpContext.Current != null)
            {
                return HttpContext.Current.Request.Url.Scheme + "://" + HttpContext.Current.Request.Url.DnsSafeHost + HttpContext.Current.Request.Path;
            }
            return null;
        }

        public static RouteData GetCurrentRouteData(this UrlHelper helper)
        {
            var routedata = helper.RequestContext.RouteData;
            return routedata;
        }

        public static RouteValueDictionary GetCurrentRouteData(this UrlHelper helper, object routeValues)
        {
            var routedata = helper.RequestContext.RouteData;

            var values = new RouteValueDictionary(routeValues);

            foreach (var item in routedata.Values)
            {
                var key = item.Key.ToLower();
                if (!values.Any(c => c.Key.ToLower() == key))
                {
                    values.Add(item.Key, item.Value);
                }
            }

            return values;
        }
    }
}
