using MakinaTurkiye.Services.Logs;
using NeoSistem.MakinaTurkiye.Management.Models;
using NeoSistem.MakinaTurkiye.Management.Models.Logs;
using System.Web.Mvc;

namespace NeoSistem.MakinaTurkiye.Management.Controllers
{
    public class ObjectionableContentController : BaseController
    {
        private readonly IApplicationLogService _applicationLogService;

        public ObjectionableContentController(IApplicationLogService applicationLogService)
        {
            this._applicationLogService = applicationLogService;
        }
        public ActionResult Index()
        {
            FilterModel<LogItemModel> model = new FilterModel<LogItemModel>();
            return View(model);
        }
    }
}