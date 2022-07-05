namespace NeoSistem.MakinaTurkiye.Management.Controllers
{
    using global::MakinaTurkiye.Core;
    using global::MakinaTurkiye.Services.CallCenter;
    using System.Linq;
    using System.Web.Mvc;

    public class CallCenterController : Controller
    {
        ICallCenterService _callCenterService;

        public CallCenterController(ICallCenterService callCenterService)
        {
            _callCenterService = callCenterService;
            _callCenterService.Token = AppSettings.CallCenterToken;
        }

        public JsonResult Calling(string number)
        {
            ResponseModel<CallInfo> result = new ResponseModel<CallInfo>();
            string destination = "";
            var Lst = NeoSistem.MakinaTurkiye.Management.Models.Authentication.CurrentUserModel.CurrentManagement.CallCenterUrl.Split('/').ToList().LastOrDefault();
            if (!string.IsNullOrEmpty(Lst))
            {
              destination = Lst;
              result = _callCenterService.Calling(destination, number);
            }
            else
            {
                result.IsSuccess = false;
                result.Message = "Kullanıcı CallCenter Ayarları Yapılmamış. Lütfen Kullanıcı düzenleme bölümünden tanımlama yapınız.";
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public JsonResult StopCall(string number)
        {
            var result = _callCenterService.StopCall(number);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}