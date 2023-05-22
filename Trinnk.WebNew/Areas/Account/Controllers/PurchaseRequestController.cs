
using Trinnk.Utilities.Controllers;
using NeoSistem.Trinnk.Web.Models;
using System.Web.Mvc;

namespace NeoSistem.Trinnk.Web.Areas.Account.Controllers
{

    public class PurchaseRequestController : BaseAccountController
    {
        string activeStyle = "color: #0769cd";
        public ActionResult Index()
        {
            var PurchaseRequest = (PurchaseRequestType)System.Convert.ToByte(Request.QueryString["PurchaseRequestType"]);

            switch (PurchaseRequest)
            {
                case PurchaseRequestType.Onaydaki:
                    ViewData["PurchaseRequestOnaydaki"] = activeStyle;
                    break;
                case PurchaseRequestType.Aktif:
                    ViewData["PurchaseRequestAktif"] = activeStyle;
                    break;
                case PurchaseRequestType.Pasif:
                    ViewData["PurchaseRequestPasif"] = activeStyle;
                    break;
                case PurchaseRequestType.Onaylanmamis:
                    ViewData["PurchaseRequestOnaylanmamis"] = activeStyle;
                    break;
                case PurchaseRequestType.Silinen:
                    ViewData["PurchaseRequestSilinen"] = activeStyle;
                    break;
                default:
                    break;
            }
            return View();
        }

        public ActionResult OneStep()
        {
            ViewData["PurchaseRequestAdd"] = activeStyle;
            return View();
        }

        public ActionResult TwoStep()
        {
            return View();
        }

        public ActionResult ThreeStep()
        {
            return View();
        }

        public ActionResult FourStep()
        {
            return View();
        }

        public ActionResult FiveStep()
        {
            return View();
        }

        public ActionResult SixStep()
        {
            return View();
        }

    }
}
