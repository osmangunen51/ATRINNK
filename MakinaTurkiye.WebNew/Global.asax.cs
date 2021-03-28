using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using MakinaTurkiye.Core.Infrastructure;
using NeoSistem.MakinaTurkiye.Web.App_Start;
using MakinaTurkiye.Core.Configuration;
using MakinaTurkiye.Utilities.Mvc;
using MakinaTurkiye.Logging;
using NeoSistem.MakinaTurkiye.Web.Controllers;
using MakinaTurkiye.Services.Common;
using System.Web.Optimization;
using MakinaTurkiye.Caching;
using System.Linq;
using MakinaTurkiye.Services.Catalog;
using MakinaTurkiye.Utilities.HttpHelpers;

namespace NeoSistem.MakinaTurkiye.Web
{
    public class MvcApplication : HttpApplication
    {

        protected void Application_Start()
        {

            //initialize engine context
            EngineContext.Initialize(false);

            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);

            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);

            ViewEngines.Engines.Clear();
            ViewEngines.Engines.Add(new CustomWebFormViewEngine());
            //ViewEngines.Engines.Clear();

            // register Razor view engine only
            ViewEngines.Engines.Add(new RazorViewEngine());

            ILogger logger = EngineContext.Current.Resolve<ILogger>();
            logger.Fatal("Application start");
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        private void Application_End()
        {
            ILogger logger = EngineContext.Current.Resolve<ILogger>();
            logger.Fatal("Application end");
        }

        protected void Application_Error(object sender, EventArgs e)
        {
            MakinaTurkiyeConfig config = EngineContext.Current.Resolve<MakinaTurkiyeConfig>();
            if (!config.ApplicationTestModeEnabled)
            {
                Exception exception = Server.GetLastError();
                string requestUrl = Request.ServerVariables["REQUEST_URI"];
                if (requestUrl.EndsWith(">"))
                {
                    requestUrl = requestUrl.Substring(0, requestUrl.Length - 1);
                    Response.RedirectPermanent(requestUrl);
                }
            }


              if (config.ApplicationLogEnabled)
          {
              Exception exception = Server.GetLastError();

         ILogger logger = EngineContext.Current.Resolve<ILogger>();
            exception.Data.Add("Url", Context.Request.Url.ToString());
            exception.HelpLink = Context.Request.Url.ToString();
            logger.Error("Global.asax error", exception);  
            HttpException httpException = exception as HttpException;
                RouteData routeData = new RouteData();
                routeData.Values.Add("controller", "Home");
                string PrmQuery = Context.Request.Url.PathAndQuery.ToString();
                IUrlRedirectService _urlRedirectService = EngineContext.Current.Resolve<IUrlRedirectService>();
                var urlRedirect = _urlRedirectService.GetUrlRedirectByOldUrl(PrmQuery);
                if (urlRedirect != null)
                {
                    Context.Response.RedirectPermanent(urlRedirect.NewUrl);
                }
                if (httpException != null || exception != null)
                {
                    if (httpException != null)
                    {
                        if (httpException.ErrorCode == 404)
                        {
                            //ILog log = log4net.LogManager.GetLogger("global.asax");
                            //log.Error($"404 error kod: {httpException.Message.ToString()}");
                            GetGeneralErrorPage(exception, routeData);
                        }
                        // 404 Hatalarının Dz
                        if (httpException.Message.Contains("was not found or does not implement IController."))
                        {
                            string RedirectUrl = "";
                            int NSayisi = 0;
                            string DomainUrl = Context.Request.Url.ToString().Replace(Context.Request.Url.PathAndQuery, "");

                            for (int Don = PrmQuery.Length - 1; Don > -1; Don--)
                            {
                                int n;
                                string Txt = "";
                                Txt = PrmQuery.Substring(Don, 1);
                                var isNumeric = int.TryParse(Txt, out n);
                                if (isNumeric)
                                {
                                    NSayisi++;
                                }
                                else
                                {
                                    if (Txt.ToLower() == "m")
                                    {
                                        if (NSayisi > 4)
                                        {
                                            string Prm1 = PrmQuery.Substring(0, Don);
                                            string Prm2 = PrmQuery.Substring(Don + 1, NSayisi);
                                            RedirectUrl = string.Format("{0}{1}{2}{3}", DomainUrl, Prm1, "-m-", Prm2);
                                            break;
                                        }
                                        else
                                        {
                                            break;
                                        }
                                    }
                                    else if (Txt.ToLower() == "s")
                                    {
                                        if (NSayisi > 4)
                                        {
                                            string Prm1 = PrmQuery.Substring(0, Don);
                                            string Prm2 = PrmQuery.Substring(Don + 1, NSayisi);
                                            RedirectUrl = string.Format("{0}{1}{2}{3}", DomainUrl, Prm1, "-s-", Prm2);
                                            break;
                                        }
                                        else
                                        {
                                            break;
                                        }
                                    }
                                    else
                                    {
                                        break;
                                    }
                                }
                            }
                            if (RedirectUrl != "")
                            {
                                Context.Response.RedirectPermanent(RedirectUrl);
                            }
                            else
                            {
                                GetGeneralErrorPage(exception, routeData);
                            }
                        }
                        else
                        {
                            GetGeneralErrorPage(exception, routeData);
                        }
                    }
                    else
                    {
                        GetGeneralErrorPage(exception, routeData);
                    }
                }
            }
               

        }

        private void GetGeneralErrorPage(Exception exception, RouteData routeData)
        {

            // Call target Controller and pass the routeData.
            IController errorController = EngineContext.Current.Resolve<CommonController>();
            if (routeData.Values.ContainsKey("controller"))
            {
                routeData.Values["controller"] = "Common";
            }
            else
            {
                routeData.Values.Add("controller", "Common");
            }

            if (routeData.Values.ContainsKey("action"))
            {
                routeData.Values["action"] = "HataSayfasi";
            }
            else
            {
                routeData.Values.Add("action", "HataSayfasi");
            }

            if (routeData.Values.ContainsKey("error"))
            {
                routeData.Values["error"] = exception;
            }
            else
            {
                routeData.Values.Add("error", exception);
            }


            Server.ClearError();
            errorController.Execute(new RequestContext(new HttpContextWrapper(Context), routeData));
        }


        public void Application_BeginRequest(object sender, EventArgs e)
        {
            MakinaTurkiyeConfig config = EngineContext.Current.Resolve<MakinaTurkiyeConfig>();
            config.ApplicationTestModeEnabled = false;
            if (!config.ApplicationTestModeEnabled)
            {
                if (!Request.IsLocal)
                {
                    string host = Request.Url.Host;

                    if (host.ToLower() == "www.makinaturkiye.com/")
                    {
                        string Url = "https://wwww.makinaturkiye.com";
                        Response.RedirectPermanent(Url, true);
                    }
                    bool nonwww = false;
                    var nodes = host.Split('.');
                    if (nodes[0] != "www" && nodes[0] == "makinaturkiye")
                    {
                        nonwww = true;
                        host = "www." + Request.Url.Host;
                    }
                    switch (Request.Url.Scheme)
                    {
                        case "https":
                            if (nonwww)
                            {
                                var path = "https://" + host + Request.Url.PathAndQuery;
                                Response.AddHeader("Strict-Transport-Security", "max-age=31536000");
                                Response.Status = "301 Moved Permanently";
                                Response.AddHeader("Location", path);
                            }
                            break;
                        case "http":
                            var path2 = "https://" + host + Request.Url.PathAndQuery;
                            Response.Status = "301 Moved Permanently";
                            Response.AddHeader("Location", path2);
                            break;
                    }
                    string requestUrl = Request.ServerVariables["REQUEST_URI"];
                    string rewriteUrl = Request.ServerVariables["UNENCODED_URL"];
                    if (rewriteUrl.Contains("//") && !requestUrl.Contains("//")) Response.RedirectPermanent(requestUrl);

                    if (requestUrl.EndsWith(".htm") | requestUrl.EndsWith(".html"))
                    {
                        string link = "";
                        link = "/urun-kategori-c-0";
                        Response.RedirectPermanent(link);
                    }

                    if (requestUrl.Contains("Product/ProductContact?"))
                    {
                        var Url = this.Request.Url;
                        string Prm = HttpUtility.ParseQueryString(Url.Query).Get("productId");
                        if (!string.IsNullOrEmpty(Prm))
                        {
                            int ProductId = Convert.ToInt32(Prm);
                            IProductService _productService = EngineContext.Current.Resolve<IProductService>();
                            var product = _productService.GetProductByProductId(ProductId);
                            string link = "";
                            if (product != null)
                            {
                                link = UrlBuilder.GetProductUrl(product.ProductId, product.ProductName);
                            }
                            else
                            {
                                link = "/urun-kategori-c-0";
                            }
                            Response.RedirectPermanent(link);
                        }
                    }

                    string key = "makinaturkiye.urlredirect.{0}";
                    ICacheManager _cacheManager = EngineContext.Current.Resolve<ICacheManager>();
                    var urlRedirectlist = _cacheManager.Get(key, () =>
                    {
                        IUrlRedirectService _urlRedirectService = EngineContext.Current.Resolve<IUrlRedirectService>();
                        var model = _urlRedirectService.GetUrlRedirectAll();
                        return model;
                    });
                    string PrmQuery = Context.Request.Url.PathAndQuery.ToString();
                    var urlRedirect = urlRedirectlist.FirstOrDefault(x => x.OldUrl == PrmQuery);
                    if (urlRedirect != null)
                    {
                        Context.Response.RedirectPermanent(urlRedirect.NewUrl);
                    }
                }
            }
        }

    }

}
