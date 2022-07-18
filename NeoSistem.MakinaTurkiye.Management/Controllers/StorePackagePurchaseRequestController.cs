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
            //int p = 1;

            //int pageSize = 20;
            //var stores = _storePackagePurchaseRequestService.GetPreRegistirationStores(p, pageSize, "", "", false);
            return View();
        }
        [HttpPost]
        public PartialViewResult Index(string page, string storeName, string email)
        {
            return PartialView("_Item", null);
        }
    }
}