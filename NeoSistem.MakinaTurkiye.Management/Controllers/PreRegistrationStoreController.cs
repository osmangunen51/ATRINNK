using MakinaTurkiye.Entities.Tables.Stores;
using MakinaTurkiye.Services.Members;
using MakinaTurkiye.Services.Stores;
using NeoSistem.MakinaTurkiye.Management.Models;
using NeoSistem.MakinaTurkiye.Management.Models.PreRegistrations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace NeoSistem.MakinaTurkiye.Management.Controllers
{
    public class PreRegistrationStoreController : BaseController
    {
        private readonly IPreRegistirationStoreService _preRegistrationService;
        private readonly IStoreService _storeService;
        private readonly IMemberService _memberService;
        private readonly IMemberStoreService _memberStoreService;
        private readonly IMemberDescriptionService _memberDescriptonService;

        public PreRegistrationStoreController(IPreRegistirationStoreService preRegistirationStoreService, IStoreService storeService,
            IMemberService memberService, IMemberStoreService memberStoreService,
            IMemberDescriptionService memberDescriptonService)
        {
            this._preRegistrationService = preRegistirationStoreService;
            this._storeService = storeService;
            this._memberService = memberService;
            this._memberStoreService = memberStoreService;
            this._memberDescriptonService = memberDescriptonService;
        }
        // GET: PreRegistrationStore
        public ActionResult Index()
        {
            int p = 1;

            int pageSize = 20;
            var stores = _preRegistrationService.GetPreRegistirationStores(p, pageSize, "", "");
            FilterModel<PreRegistrationItem> preRegistrations = new FilterModel<PreRegistrationItem>();
            List<PreRegistrationItem> source = new List<PreRegistrationItem>();
            foreach (var item in stores)
            {
                source.Add(new PreRegistrationItem
                {
                    Email = item.Email,
                    MemberName = item.MemberName,
                    MemberSurname = item.MemberSurname,
                    Id = item.PreRegistrationStoreId,
                    PhoneNumber = item.PhoneNumber,
                    StoreName = item.StoreName,
                    RecordDate = item.RecordDate,
                    WebUrl = item.WebUrl,
                    PhoneNumber2 = item.PhoneNumber2,
                    PhoneNumber3 = item.PhoneNumber3,
                    HasDescriptions = entities.MemberDescriptions.Any(x => x.PreRegistrationStoreId == item.PreRegistrationStoreId)
                });
            }
            preRegistrations.Source = source;
            preRegistrations.PageDimension = pageSize;
            preRegistrations.TotalRecord = stores.TotalCount;
            preRegistrations.CurrentPage = p;
            return View(preRegistrations);
        }
        [HttpPost]
        public PartialViewResult Index(string page, string storeName, string email)
        {
            int p = Convert.ToInt32(page);
            int pageSize = 20;
            var stores = _preRegistrationService.GetPreRegistirationStores(p, pageSize, storeName, email);
            FilterModel<PreRegistrationItem> preRegistrations = new FilterModel<PreRegistrationItem>();
            List<PreRegistrationItem> source = new List<PreRegistrationItem>();
            foreach (var item in stores)
            {
                source.Add(new PreRegistrationItem
                {
                    Email = item.Email,
                    MemberName = item.MemberName,
                    MemberSurname = item.MemberSurname,
                    Id = item.PreRegistrationStoreId,
                    PhoneNumber = item.PhoneNumber,
                    StoreName = item.StoreName,
                    WebUrl = item.WebUrl,
                    RecordDate = item.RecordDate,
                    PhoneNumber2 = item.PhoneNumber2,
                    PhoneNumber3 = item.PhoneNumber3,
                    HasDescriptions = entities.MemberDescriptions.Any(x => x.PreRegistrationStoreId == item.PreRegistrationStoreId)
                });
            }
            preRegistrations.Source = source;
            preRegistrations.PageDimension = pageSize;
            preRegistrations.TotalRecord = stores.TotalCount;
            preRegistrations.CurrentPage = p;
            return PartialView("_Item", preRegistrations);
        }
        public ActionResult Create()
        {
            PreRegistrainFormModel model = new PreRegistrainFormModel();
            return View(model);
        }
        [HttpPost]
        public ActionResult Create(PreRegistrainFormModel model)
        {

            if (string.IsNullOrEmpty(model.StoreName))
            {
                ModelState.AddModelError("StoreName", "Lütfen Firma Adını Giriniz");
            }
            else
            {
                var preRegistrationStore = new PreRegistrationStore();
                preRegistrationStore.Email = model.Email;
                preRegistrationStore.MemberName = model.MemberName;
                preRegistrationStore.MemberSurname = model.MemberSurname;
                preRegistrationStore.PhoneNumber = model.PhoneNumber;
                preRegistrationStore.PhoneNumber2 = model.PhoneNumber2;
                preRegistrationStore.PhoneNumber3 = model.PhoneNumber3;
                preRegistrationStore.StoreName = model.StoreName;
                preRegistrationStore.RecordDate = DateTime.Now;
                preRegistrationStore.WebUrl = model.WebUrl;
                _preRegistrationService.InsertPreRegistrationStore(preRegistrationStore);
                TempData["success"] = true;
                return RedirectToAction("Create");
            }
            return View(model);
        }
        public ActionResult Edit(int id)
        {
            var preRegistration = _preRegistrationService.GetPreRegistirationStoreByPreRegistrationStoreId(id);
            PreRegistrainFormModel model = new PreRegistrainFormModel();
            model.Email = preRegistration.Email;
            model.Id = preRegistration.PreRegistrationStoreId;
            model.MemberName = preRegistration.MemberName;
            model.MemberSurname = preRegistration.MemberSurname;
            model.PhoneNumber = preRegistration.PhoneNumber;
            model.StoreName = preRegistration.StoreName;
            model.PhoneNumber2 = preRegistration.PhoneNumber2;
            model.PhoneNumber3 = preRegistration.PhoneNumber3;
            model.WebUrl = preRegistration.WebUrl;
            return View(model);
        }
        [HttpPost]
        public ActionResult Edit(PreRegistrainFormModel model)
        {
            var preRegistration = _preRegistrationService.GetPreRegistirationStoreByPreRegistrationStoreId(model.Id);
            preRegistration.Email = model.Email;
            preRegistration.MemberName = model.MemberName;
            preRegistration.MemberSurname = model.MemberSurname;
            preRegistration.PhoneNumber = model.PhoneNumber;
            preRegistration.StoreName = model.StoreName;
            preRegistration.PhoneNumber2 = model.PhoneNumber2;
            preRegistration.PhoneNumber3 = model.PhoneNumber3;
            preRegistration.WebUrl = model.WebUrl;
            _preRegistrationService.UpdatePreRegistrationStore(preRegistration);
            return RedirectToAction("Index");
        }
        [HttpPost]
        public PartialViewResult SerachByName(string storename, string email)
        {

            List<StoreItem> model = new List<StoreItem>();
            if (!string.IsNullOrEmpty(storename))
            {
                var stores = _storeService.GetStoreSearchByStoreName(storename);
                foreach (var item in stores)
                {
                    var memberStore = _memberStoreService.GetMemberStoreByStoreMainPartyId(item.MainPartyId);
                    string memberName = "";
                    string memberNo = "";
                    if (memberStore != null)
                    {
                        var member = _memberService.GetMemberByMainPartyId(memberStore.MemberMainPartyId.Value);
                        memberName = member.MemberName + " " + member.MemberSurname;
                        memberNo = member.MemberSurname;
                    }
                    model.Add(new StoreItem { StoreMainPartId = item.MainPartyId, StoreName = item.StoreName, StoreNo = item.StoreNo, MemberNo = memberNo, MemberNameSurname = memberName, Type = "Normal Kayıt" });
                }

                var preStores = _preRegistrationService.GetPreRegistrationStoreSearchByName(storename);
                foreach (var item in preStores)
                {
                    model.Add(new StoreItem { StoreMainPartId = 0, StoreName = item.StoreName, MemberNameSurname = item.MemberName, Type = "Ön  Kayıt" });
                }


            }
            if (model.Count == 0)
            {
                if (!string.IsNullOrEmpty(email))
                {
                    var member = _memberService.GetMemberByMemberEmail(email);
                    string storeName = "";
                    string storeNo = "";
                    if (member != null)
                    {
                        var memberStore = _memberStoreService.GetMemberStoreByMemberMainPartyId(member.MainPartyId);
                        if (memberStore != null)
                        {
                            var store = _storeService.GetStoreByMainPartyId(memberStore.StoreMainPartyId.Value);
                            if (store != null)
                            {
                                storeName = store.StoreName;
                                storeNo = store.StoreNo;
                            }
                        }
                        model.Add(new StoreItem { StoreNo = storeNo, MemberNameSurname = member.MemberName + " " + member.MemberSurname, StoreName = storeName, MemberNo = member.MemberNo });
                    }
                }
            }
            return PartialView("_StoreItem", model);
        }

        public ActionResult Delete(int Id)
        {
            PreRegistrationStoreDeleteModel model = new PreRegistrationStoreDeleteModel();
            model.Id = Id;
            return View(model);
        }
        [HttpPost]
        public ActionResult Delete(PreRegistrationStoreDeleteModel model)
        {
            PreRegistrationStoreDeleteModel modelNew = new PreRegistrationStoreDeleteModel();

            var memberdescriptions = _memberDescriptonService.GetMemberDescriptionByPreRegistrationStoreId(model.Id);
            if (!string.IsNullOrEmpty(model.StoreNo))
            {
                var store = _storeService.GetStoreByStoreNo(model.StoreNo);
                if (store != null)
                {
                    var memberStore = _memberStoreService.GetMemberStoreByStoreMainPartyId(store.MainPartyId);
                    var memberDescriptions = _memberDescriptonService.GetMemberDescriptionByPreRegistrationStoreId(model.Id);
                    foreach (var item in memberDescriptions)
                    {
                        item.PreRegistrationStoreId = null;
                        item.MainPartyId = memberStore.MemberMainPartyId.Value;
                        _memberDescriptonService.UpdateMemberDescription(item);
                    }

                    var preRegistration = _preRegistrationService.GetPreRegistirationStoreByPreRegistrationStoreId(model.Id);
                    if (preRegistration != null)
                        _preRegistrationService.DeletePreRegistrationStore(preRegistration);
                    modelNew.Message = "İşlem başarıyla gerçekleşmiştir";
                }
                else
                {
                    modelNew.Message = "Lütfen firma numarasını doğru giriniz";
                }

            }
            else
            {
                var memberDescriptions = _memberDescriptonService.GetMemberDescriptionByPreRegistrationStoreId(model.Id);

                foreach (var item in memberDescriptions)
                {
                    _memberDescriptonService.DeleteMemberDescription(item);
                }

                var preRegistration = _preRegistrationService.GetPreRegistirationStoreByPreRegistrationStoreId(model.Id);
                if (preRegistration != null)
                    _preRegistrationService.DeletePreRegistrationStore(preRegistration);
                modelNew.Message = "İşlem başarıyla gerçekleşmiştir";
            }


            return View(modelNew);
        }
    }
}