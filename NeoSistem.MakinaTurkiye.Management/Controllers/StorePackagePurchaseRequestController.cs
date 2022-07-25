using MakinaTurkiye.Services.Stores;
using System.Web.Mvc;

namespace NeoSistem.MakinaTurkiye.Management.Controllers
{
    public class StorePackagePurchaseRequestController : BaseController
    {
        private IStorePackagePurchaseRequestService _storePackagePurchaseRequestService;

        public StorePackagePurchaseRequestController(IStorePackagePurchaseRequestService storePackagePurchaseRequestService)
        {
            _storePackagePurchaseRequestService = storePackagePurchaseRequestService;
        }

        public ActionResult Index()
        {
            var Model = _storePackagePurchaseRequestService.GetAll();
            return View(Model);
        }
        [HttpPost]
        public PartialViewResult Index(string page, string storeName, string email)
        {
            return PartialView("_Item", null);
        }
    }
}