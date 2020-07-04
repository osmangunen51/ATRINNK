namespace NeoSistem.MakinaTurkiye.Management.Controllers
{
    using EnterpriseEntity.Extensions.Data;
    using NeoSistem.MakinaTurkiye.Management.Models.Entities;
    using System;
    using System.Linq;
    using System.Web.Mvc;

    public class LogController : BaseController
    {
        public ActionResult Index(int? page, int? status, int? id)
        {
            var entities = new MakinaTurkiyeEntities();
            int pageSize = 20;
            int skipRows = 0;
            skipRows = page == null || page == 0 ? 0 : (int)(page - 1) * pageSize;
            ViewData["page"] = page == null ? 0 : (int)page;

            ViewData["pageNumbers"] = Convert.ToInt32(Math.Ceiling(entities.UserLogs.Where(x => x.LogStatus != null).ToList().Count / (float)pageSize));
            var messageList = entities.UserLogs.Where(x => x.LogStatus != null).OrderByDescending(x => x.LogıD).ThenBy(x => x.LogDate).Skip(skipRows).Take(pageSize).ToList();
            return View(messageList);
        }

        [HttpPost]
        public JsonResult DeleteLog(int id)
        {
            var demand = entities.UserLogs.SingleOrDefault(x => x.LogıD == id);
            entities.UserLogs.DeleteObject(demand);
            entities.SaveChanges();
            return Json(true, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult GetLogType(int type, int? status, int? page)
        {
            int pageSize = 20;
            int skipRows = 0;
            skipRows = page == null || page == 0 ? 0 : (int)(page - 1) * pageSize;
            ViewData["page"] = page == null ? 0 : (int)page;

            if (status != null)
            {
                if (type == 0)
                {
                    var LogList = (from l in entities.UserLogs where l.LogStatus == status select new { logID = l.LogıD, logName = l.LogName, logShorDesc = l.LogShortDescription, logDesc = l.LogDescription, logDate = l.LogDate, logStatus = l.LogStatus, logType = l.LogType }).ToList().OrderByDescending(x => x.logID).Skip(skipRows).Take(pageSize);
                    return Json(LogList, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    var LogList = (from l in entities.UserLogs where l.LogType == type && l.LogStatus == status select new { logID = l.LogıD, logName = l.LogName, logShorDesc = l.LogShortDescription, logDesc = l.LogDescription, logDate = l.LogDate, logStatus = l.LogStatus, logType = l.LogType }).ToList().OrderByDescending(x => x.logID).Skip(skipRows).Take(pageSize);
                    return Json(LogList, JsonRequestBehavior.AllowGet);
                }

            }
            else
            {
                if (type == 0)
                {
                    var LogList = (from l in entities.UserLogs where l.LogStatus != null select new { logID = l.LogıD, logName = l.LogName, logShorDesc = l.LogShortDescription, logDesc = l.LogDescription, logDate = l.LogDate, logStatus = l.LogStatus, logType = l.LogType }).ToList().OrderByDescending(x => x.logID).Skip(skipRows).Take(pageSize);
                    return Json(LogList, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    var LogList = (from l in entities.UserLogs where l.LogType == type && l.LogStatus != null select new { logID = l.LogıD, logName = l.LogName, logShorDesc = l.LogShortDescription, logDesc = l.LogDescription, logDate = l.LogDate, logStatus = l.LogStatus, logType = l.LogType }).ToList().OrderByDescending(x => x.logID).Skip(skipRows).Take(pageSize);
                    return Json(LogList, JsonRequestBehavior.AllowGet);
                }
            }
        }

    }
}