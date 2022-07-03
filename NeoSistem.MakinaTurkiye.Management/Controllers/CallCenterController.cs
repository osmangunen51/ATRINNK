namespace NeoSistem.MakinaTurkiye.Management.Controllers
{
    using EnterpriseEntity.Extensions.Data;
    using global::MakinaTurkiye.Core;
    using global::MakinaTurkiye.Services.CallCenter;
    using global::MakinaTurkiye.Services.Users;
    using Models;
    using Models.Authentication;
    using System;
    using System.Web.Mvc;

    public class CallCenterController : Controller
    {
        ICallCenterService _callCenterService;

        public CallCenterController(ICallCenterService callCenterService)
        {
            _callCenterService = callCenterService;
            _callCenterService.Token = AppSettings.CallCenterToken;
        }

        public JsonResult Calling(string destination, string number)
        {
            var result=_callCenterService.Calling(destination,number);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public JsonResult StopCall(string number)
        {
            var result = _callCenterService.StopCall(number);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}