using System;
using System.IO;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace MakinaTurkiye.WebNew.Tools
{
    public static partial class Helper
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

        class FakeController : ControllerBase { protected override void ExecuteCore() { } }
        public static string RenderViewToString(string controllerName, string viewName, object viewData)
        {
            using (var writer = new StringWriter())
            {
                var routeData = new RouteData();
                routeData.Values.Add("controller", controllerName);
                var fakeControllerContext = new ControllerContext(new HttpContextWrapper(new HttpContext(new HttpRequest(null, "http://google.com", null), new HttpResponse(null))), routeData, new FakeController());
                var razorViewEngine = new RazorViewEngine();
                var razorViewResult = razorViewEngine.FindView(fakeControllerContext, viewName, "", false);

                var viewContext = new ViewContext(fakeControllerContext, razorViewResult.View, new ViewDataDictionary(viewData), new TempDataDictionary(), writer);
                razorViewResult.View.Render(viewContext, writer);
                return writer.ToString();
            }
        }

    }
}
