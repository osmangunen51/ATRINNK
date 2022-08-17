using MakinaTurkiye.Core.Infrastructure;
using MakinaTurkiye.Entities.Tables.Members;
using MakinaTurkiye.Services.Members;
using MakinaTurkiye.Services.Stores;
using MakinaTurkiye.Services.Users;
using NeoSistem.MakinaTurkiye.Cache;
using NeoSistem.MakinaTurkiye.Management.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;
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
