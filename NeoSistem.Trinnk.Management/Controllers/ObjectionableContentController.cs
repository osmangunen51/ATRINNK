using Trinnk.Services.Logs;
using NeoSistem.Trinnk.Management.Models;
using NeoSistem.Trinnk.Management.Models.Logs;
using System.Web.Mvc;

namespace NeoSistem.Trinnk.Management.Controllers
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