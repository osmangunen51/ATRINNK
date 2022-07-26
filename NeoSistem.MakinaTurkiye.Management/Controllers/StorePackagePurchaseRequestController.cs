using global::MakinaTurkiye.Services.Stores;
using NeoSistem.EnterpriseEntity.Extensions.Data;
using System.Collections.Generic;
using System.Data;
using System.Linq;
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
            var tmp = _storePackagePurchaseRequestService.GetAll();
            var UserListesi = entities.Users.Where(x => x.UserId > 0).ToList();
            ICollection<NeoSistem.MakinaTurkiye.Management.Models.ViewModel.StorePackagePurchaseRequest> Model = new List<Models.ViewModel.StorePackagePurchaseRequest>();
            foreach (var StorePackagePurchaseRequest in tmp)
            {
                NeoSistem.MakinaTurkiye.Management.Models.ViewModel.StorePackagePurchaseRequest itm = new Models.ViewModel.StorePackagePurchaseRequest
                {
                    Id = StorePackagePurchaseRequest.Id,
                    Desciption = StorePackagePurchaseRequest.Desciption,
                    Date = StorePackagePurchaseRequest.Date,
                    FirstName = StorePackagePurchaseRequest.FirstName,
                    LastName = StorePackagePurchaseRequest.LastName,
                    MainPartyId = StorePackagePurchaseRequest.MainPartyId,
                    Phone = StorePackagePurchaseRequest.Phone,
                    ProductQuantity = StorePackagePurchaseRequest.ProductQuantity,
                    StoreName = StorePackagePurchaseRequest.StoreName,
                    AuthorizedId = StorePackagePurchaseRequest.AuthorizedId,
                    PortfoyUserId = StorePackagePurchaseRequest.PortfoyUserId,
                };

                var PortfoyYonetici = UserListesi.FirstOrDefault(y => y.UserId == StorePackagePurchaseRequest.PortfoyUserId);
                if (PortfoyYonetici != null)
                {
                    itm.PortfoyYonetici = $"{PortfoyYonetici.Name} {PortfoyYonetici.Surname}";
                }
                var TeleSatisSorumlu = UserListesi.FirstOrDefault(y => y.UserId == StorePackagePurchaseRequest.AuthorizedId);
                if (TeleSatisSorumlu != null)
                {
                    itm.TeleSatisSorumlu = $"{TeleSatisSorumlu.Name} {TeleSatisSorumlu.Surname}";
                }
                Model.Add(itm);
            }
            return View(Model);
        }
        [HttpPost]
        public PartialViewResult Index(string page, string storeName, string email)
        {
            return PartialView("_Item", null);
        }
    }
}