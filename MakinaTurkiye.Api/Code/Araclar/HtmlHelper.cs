using System;
using System.Web;
using System.Web.Mvc;
namespace MakinaTurkiye.Api.Araclar
{
    public static partial class HtmlHelpers
    {
        public static string ResolveUrl(string originalUrl)
        {
            if (!string.IsNullOrEmpty(originalUrl) && '~' == originalUrl[0])
            {
                int index = originalUrl.IndexOf('?');
                string queryString = (-1 == index) ? null : originalUrl.Substring(index);
                if (-1 != index) originalUrl = originalUrl.Substring(0, index);
                originalUrl = VirtualPathUtility.ToAbsolute(originalUrl) + queryString;
            }
            return originalUrl;
        }
        public static string ResolveServerUrl(string serverUrl, bool forceHttps)
        {
            Uri result = HttpContext.Current.Request.Url;
            if (!string.IsNullOrEmpty(serverUrl))
            {
                serverUrl = ResolveUrl(serverUrl);
                result = new Uri(result, serverUrl);
            }
            if (forceHttps && !string.Equals(result, Uri.UriSchemeHttps))
            {
                UriBuilder builder = new UriBuilder(result);
                builder.Scheme = Uri.UriSchemeHttps;
                builder.Port = 443;
                result = builder.Uri;
            }
            return result.ToString();
        }
        public static string RenderViewToString(ControllerContext context, string viewName, object model)
        {
            if (string.IsNullOrEmpty(viewName))
                viewName = context.RouteData.GetRequiredString("action");
            var viewData = new ViewDataDictionary(model);
            using (var sw = new System.IO.StringWriter())
            {
                var viewResult = ViewEngines.Engines.FindPartialView(context, viewName);
                var viewContext = new ViewContext(context, viewResult.View, viewData, new TempDataDictionary(), sw);
                viewResult.View.Render(viewContext, sw);
                return sw.GetStringBuilder().ToString();
            }
        }
    }
}
