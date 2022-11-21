using MakinaTurkiye.Localization;
using System;
using System.Web;
using System.Web.Mvc;

namespace NeoSistem.MakinaTurkiye.Web.Controllers
{
    public class DilController : Controller
    {
        public ActionResult Index(string lang = "tr_TR")
        {
            Response.Cookies["CacheLang"].Value = lang;
            if (Request.UrlReferrer != null)
                Response.Redirect(Request.UrlReferrer.ToString());
            var message = Localization.Get("changedlng");
            string Pth = "";
            if (HttpContext.Response.RedirectLocation != null)
            {
                Pth = new Uri(HttpContext.Response.RedirectLocation).AbsolutePath;
            }
            HttpResponse.RemoveOutputCacheItem(Pth);
            if (lang == "tr_TR")
            {
                System.Globalization.CultureInfo.DefaultThreadCurrentCulture = new System.Globalization.CultureInfo("tr-TR");
            }
            if (lang == "en_US")
            {
                System.Globalization.CultureInfo.DefaultThreadCurrentCulture = new System.Globalization.CultureInfo("en-US");
            }
            return Content(message);
        }
    }
}