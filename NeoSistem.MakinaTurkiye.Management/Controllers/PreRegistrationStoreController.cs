using MakinaTurkiye.Entities.Tables.Common;
using MakinaTurkiye.Entities.Tables.Members;
using MakinaTurkiye.Entities.Tables.Stores;
using MakinaTurkiye.Services.Common;
using MakinaTurkiye.Services.Members;
using MakinaTurkiye.Services.Packets;
using MakinaTurkiye.Services.Stores;
using NeoSistem.MakinaTurkiye.Management.Models;
using NeoSistem.MakinaTurkiye.Management.Models.PreRegistrations;
using System;
using System.Collections.Generic;
using System.Globalization;
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
        private readonly IPacketService _packetService;
        private readonly IPhoneService _phoneService;

        public PreRegistrationStoreController(IPreRegistirationStoreService preRegistirationStoreService, IStoreService storeService,
            IMemberService memberService, IMemberStoreService memberStoreService,
            IMemberDescriptionService memberDescriptonService, IPacketService packetService,
            IPhoneService phoneService)
        {
            this._preRegistrationService = preRegistirationStoreService;
            this._storeService = storeService;
            this._memberService = memberService;
            this._memberStoreService = memberStoreService;
            this._memberDescriptonService = memberDescriptonService;
            this._packetService = packetService;
            this._phoneService = phoneService;
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
                bool isInserted = false;
                if (!string.IsNullOrEmpty(item.Email))
                {
                    isInserted = _memberService.GetMemberByMemberEmail(item.Email) != null ? true : false;
                }
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
                    City = item.City,
                    ContactNameSurname= item.ContactNameSurname,
                    ContactPhoneNumber= item.ContactPhoneNumber,
                    PhoneNumber2 = item.PhoneNumber2,
                    PhoneNumber3 = item.PhoneNumber3,
                    HasDescriptions = entities.MemberDescriptions.Any(x => x.PreRegistrationStoreId == item.PreRegistrationStoreId),
                    IsInserted = isInserted
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
                bool isInserted = false;
                if (!string.IsNullOrEmpty(item.Email))
                {
                    isInserted = _memberService.GetMemberByMemberEmail(item.Email) != null ? true : false;
                }
                source.Add(new PreRegistrationItem
                {
                    Email = item.Email,
                    MemberName = item.MemberName,
                    MemberSurname = item.MemberSurname,
                    Id = item.PreRegistrationStoreId,
                    PhoneNumber = item.PhoneNumber,
                    StoreName = item.StoreName,
                    WebUrl = item.WebUrl,
                    City = item.City,
                    ContactNameSurname = item.ContactNameSurname,
                    ContactPhoneNumber = item.ContactPhoneNumber,
                    RecordDate = item.RecordDate,
                    PhoneNumber2 = item.PhoneNumber2,
                    PhoneNumber3 = item.PhoneNumber3,
                    HasDescriptions = entities.MemberDescriptions.Any(x => x.PreRegistrationStoreId == item.PreRegistrationStoreId),
                    IsInserted = isInserted
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
                preRegistrationStore.City = model.City;
                preRegistrationStore.ContactNameSurname = model.ContactNameSurname;
                preRegistrationStore.ContactPhoneNumber = model.ContactPhoneNumber;

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
            model.City = preRegistration.City;
            model.ContactNameSurname = preRegistration.ContactNameSurname;
            model.ContactPhoneNumber = preRegistration.ContactPhoneNumber;
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
            preRegistration.City= model.City;
            preRegistration.ContactPhoneNumber = model.ContactPhoneNumber;
            preRegistration.ContactNameSurname = model.ContactNameSurname;
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
                    string memberEmail = "";
                    string phoneNumber = "";
                    int MemberMainPartyId = 0;

                    if (memberStore != null)
                    {
                        var member = _memberService.GetMemberByMainPartyId(memberStore.MemberMainPartyId.Value);
                        memberName = member.MemberName + " " + member.MemberSurname;
                        memberNo = member.MemberSurname;
                        memberEmail = member.MemberEmail;
                        MemberMainPartyId = member.MainPartyId;

                    }
                    var phones = _phoneService.GetPhonesByMainPartyId(item.MainPartyId);
                    foreach (var phone in phones)
                    {
                        phoneNumber += ", " + phone.PhoneCulture + " " + phone.PhoneAreaCode + " " + phone.PhoneNumber;
                    }
                    model.Add(new StoreItem
                    {
                        StoreMainPartId = item.MainPartyId,
                        StoreName = item.StoreName,
                        StoreNo = item.StoreNo,
                        MemberNo = memberNo,
                        MemberNameSurname = memberName,
                        Type = "Normal Kayıt",
                        WebUrl = item.StoreWeb,
                        MemberEmail = memberEmail,
                        PhoneNumbers = phoneNumber,
                        MemberMainPartyId = MemberMainPartyId,
                        ContactNameSurname=item.ContactNameSurname,
                        ContactPhoneNumber = item.ContactPhoneNumber,
                    });
                }

                var preStores = _preRegistrationService.GetPreRegistrationStoreSearchByName(storename);
                foreach (var item in preStores)
                {
                    model.Add(new StoreItem
                    {
                        StoreMainPartId = 0,
                        StoreName = item.StoreName,
                        MemberNameSurname = item.MemberName,
                        Type = "Ön  Kayıt",
                        MemberEmail = item.Email,
                        PhoneNumbers = item.PhoneNumber + "," + item.PhoneNumber2 + "," + item.PhoneNumber3,
                        WebUrl = item.WebUrl,
                        City = item.City,
                        ContactPhoneNumber = item.ContactPhoneNumber,
                        ContactNameSurname = item.ContactNameSurname
                    });
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
                        model.Add(new StoreItem
                        {
                            StoreNo = storeNo,
                            MemberNameSurname = member.MemberName + " " + member.MemberSurname,
                            StoreName = storeName,
                            MemberNo = member.MemberNo,
                        });
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
        public ActionResult NewStore(int preRegistrationId)
        {
            try
            {
                var preRegisterStore = _preRegistrationService.GetPreRegistirationStoreByPreRegistrationStoreId(preRegistrationId);


                var mainParty = new MainParty
                {
                    Active = false,
                    MainPartyType = (byte)2,
                    MainPartyRecordDate = DateTime.Now,
                    MainPartyFullName = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(preRegisterStore.MemberName.ToLower() + " " + preRegisterStore.MemberSurname.ToLower())
                };
                _memberService.InsertMainParty(mainParty);

                var member = new Member
                {
                    MainPartyId = mainParty.MainPartyId,
                    Gender = true,
                    MemberEmail = preRegisterStore.Email,
                    MemberName = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(!string.IsNullOrEmpty(preRegisterStore.PhoneNumber) ? preRegisterStore.MemberName.ToLower() : preRegisterStore.StoreName.ToLower()),
                    MemberPassword = "",
                    MemberSurname = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(preRegisterStore.MemberSurname.ToLower()),
                    MemberType = (byte)MemberType.FastIndividual,
                    Active = false
                };
                string memberNo = "##";
                for (int i = 0; i < 7 - mainParty.MainPartyId.ToString().Length; i++)
                {
                    memberNo = memberNo + "0";
                }
                memberNo = memberNo + mainParty.MainPartyId;
                member.MemberNo = memberNo;

                _memberService.InsertMember(member);
                var curStoreMainParty = new MainParty
                {
                    Active = false,
                    MainPartyType = (byte)MainPartyType.Firm,
                    MainPartyRecordDate = DateTime.Now,
                    MainPartyFullName = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(preRegisterStore.StoreName.ToLower()),
                };
                _memberService.InsertMainParty(curStoreMainParty);


                var storeMainPartyId = curStoreMainParty.MainPartyId;
                var packet = _packetService.GetPacketByIsStandart(true);
                var curStore = new Store
                {

                    MainPartyId = storeMainPartyId,
                    PacketId = packet.PacketId,
                    StoreActiveType = (byte)PacketStatu.Inceleniyor,
                    StoreEMail = member.MemberEmail,
                    StorePacketBeginDate = DateTime.Now,
                    StorePacketEndDate = DateTime.Now.AddDays(packet.PacketDay),
                    StoreName = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(preRegisterStore.StoreName.ToLower()),
                    StoreWeb = preRegisterStore.WebUrl,
                    StoreRecordDate = DateTime.Now
                };
                string storeNo = "###";
                for (int i = 0; i < 6 - storeMainPartyId.ToString().Length; i++)
                {
                    storeNo = storeNo + "0";
                }
                storeNo = storeNo + storeMainPartyId;
                curStore.StoreNo = storeNo;

                _storeService.InsertStore(curStore);

                var curMemberStore = new MemberStore
                {
                    MemberMainPartyId = member.MainPartyId,
                    StoreMainPartyId = storeMainPartyId
                };

                _memberStoreService.InsertMemberStore(curMemberStore);

                if (!string.IsNullOrEmpty(preRegisterStore.PhoneNumber) && preRegisterStore.PhoneNumber.Length > 6)
                {
                    var phoneNumbers = clearPhoneNumber(preRegisterStore.PhoneNumber);
                    var phone = new Phone
                    {
                        MainPartyId = storeMainPartyId,
                        PhoneCulture = phoneNumbers[0],
                        PhoneAreaCode = phoneNumbers[1],
                        PhoneNumber = phoneNumbers[2],
                        PhoneType = (byte)PhoneType.Gsm,
                        active = 1
                    };
                    _phoneService.InsertPhone(phone);
                }
                if (!string.IsNullOrEmpty(preRegisterStore.PhoneNumber2) && preRegisterStore.PhoneNumber2.Length > 6)
                {
                    var phoneNumbers = clearPhoneNumber(preRegisterStore.PhoneNumber2);
                    var phone = new Phone
                    {
                        MainPartyId = storeMainPartyId,
                        PhoneCulture = phoneNumbers[0],
                        PhoneAreaCode = phoneNumbers[1],
                        PhoneNumber = phoneNumbers[2],
                        PhoneType = (byte)PhoneType.Phone,
                        active = 1
                    };
                    _phoneService.InsertPhone(phone);
                }
                if (!string.IsNullOrEmpty(preRegisterStore.PhoneNumber3) && preRegisterStore.PhoneNumber3.Length > 6)
                {
                    var phoneNumbers = clearPhoneNumber(preRegisterStore.PhoneNumber3);
                    var phone = new Phone
                    {
                        MainPartyId = storeMainPartyId,
                        PhoneCulture = phoneNumbers[0],
                        PhoneAreaCode = phoneNumbers[1],
                        PhoneNumber = phoneNumbers[2],
                        PhoneType = (byte)PhoneType.Phone,
                        active = 1
                    };
                    _phoneService.InsertPhone(phone);
                }

                return RedirectToAction("Index", "Store");

            }
            catch (Exception ex)
            {

                TempData["ErrorMessage"] = "Hızlı firma üyeliği için şartlar sağlanmadı. Lütfen kendiniz üye yapınız.Hata Mesajı:" + ex.Message;
                return RedirectToAction("Index");
            }



        }
        private string[] clearPhoneNumber(string phoneNumber)
        {
            string[] phoneNumbers = new string[3];

            phoneNumber = phoneNumber.Replace("(", "").Replace(")", "").Replace(" ", "");
            if (phoneNumber.StartsWith("+"))
            {
                phoneNumbers[0] = phoneNumber.Substring(0, 3);
                phoneNumbers[1] = phoneNumber.Substring(3, 3);
                phoneNumbers[2] = phoneNumber.Substring(6, phoneNumber.Length - 6);
            }
            else if (phoneNumber.StartsWith("90"))
            {
                phoneNumbers[0] = phoneNumber.Substring(0, 2);
                phoneNumbers[1] = phoneNumber.Substring(2, 3);
                phoneNumbers[2] = phoneNumber.Substring(5, phoneNumber.Length - 5);
            }
            else
            {
                phoneNumbers[0] = phoneNumber.Substring(0, 1);
                phoneNumbers[1] = phoneNumber.Substring(1, 3);
                phoneNumbers[2] = phoneNumber.Substring(4, phoneNumber.Length - 4);
            }
            return phoneNumbers;
        }
    }
}