using Trinnk.Core.Infrastructure;
using Trinnk.Entities.Tables.Members;
using Trinnk.Services.Members;
using Trinnk.Services.Stores;
using Trinnk.Services.Users;
using NeoSistem.Trinnk.Cache;
using NeoSistem.Trinnk.Management.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;
using System.Web.Mvc;

namespace NeoSistem.Trinnk.Management.Controllers
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
