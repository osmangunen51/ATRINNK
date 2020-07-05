using NeoSistem.MakinaTurkiye.Cache;
using NeoSistem.MakinaTurkiye.Management.Models;
using System.Web.Mvc;

namespace NeoSistem.MakinaTurkiye.Management.Controllers
{
    [HandleError]
    public class HomeController : BaseController
    {
        public ActionResult Index()
        {
            PAGEID = PermissionPage.AnaSayfa;
            return View();
        }

        public ActionResult ClearCache()
        {
            CacheUtilities.ClearAllCache();
            return RedirectToAction("/", "Home");
        }

        public ActionResult Forbidden()
        {
            return View();
        }
    }
}
