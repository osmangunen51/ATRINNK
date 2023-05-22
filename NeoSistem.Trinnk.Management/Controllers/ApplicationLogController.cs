using Trinnk.Services.Logs;
using NeoSistem.Trinnk.Management.Models;
using NeoSistem.Trinnk.Management.Models.Logs;
using System.Collections.Generic;
using System.Web.Mvc;

namespace NeoSistem.Trinnk.Management.Controllers
{
    public class ApplicationLogController : BaseController
    {
        private readonly IApplicationLogService _applicationLogService;

        public ApplicationLogController(IApplicationLogService applicationLogService)
        {
            this._applicationLogService = applicationLogService;
        }
        // GET: ApplicationLog
        public ActionResult Index()
        {
            FilterModel<LogItemModel> model = new FilterModel<LogItemModel>();
            PrepareLogItems(model);
            return View(model);
        }
        [HttpPost]
        public PartialViewResult Index(int page)
        {

            FilterModel<LogItemModel> model = new FilterModel<LogItemModel>();
            PrepareLogItems(model, page);
            return PartialView("_Item", model);
        }

        private void PrepareLogItems(FilterModel<LogItemModel> model, int page = 0)
        {
            List<LogItemModel> logItems = new List<LogItemModel>();
            int pageDimension = 100;
            var logs = _applicationLogService.GetApplicationLogs(page, pageDimension);
            foreach (var item in logs)
            {
                var logItem = new LogItemModel { Date = item.Date, ID = item.Id, Logger = item.Logger, Message = item.Message };
                switch (item.Level)
                {
                    case "ERROR":
                        logItem.LogSituationText = "HATA";
                        logItem.LogSituatinColor = "red";
                        break;
                    case "WARNING":
                        logItem.LogSituationText = "UYARI";
                        logItem.LogSituatinColor = "yellow";
                        break;
                    default:
                        logItem.LogSituatinColor = "black";
                        logItem.LogSituationText = item.Level;
                        break;
                }
                logItems.Add(logItem);
            }
            model.CurrentPage = page;
            model.TotalRecord = logs.TotalCount;
            model.Source = logItems;
            model.PageDimension = pageDimension;

        }
    }
}