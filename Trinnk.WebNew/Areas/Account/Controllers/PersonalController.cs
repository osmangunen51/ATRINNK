using Trinnk.Entities.Tables.Common;
using Trinnk.Entities.Tables.Messages;
using Trinnk.Services.Catalog;
using Trinnk.Services.Common;
using Trinnk.Services.Members;
using Trinnk.Services.Messages;
using Trinnk.Services.Stores;
using Trinnk.Utilities.Controllers;
using Trinnk.Utilities.HttpHelpers;
using NeoSistem.EnterpriseEntity.Extensions.Data;
using NeoSistem.Trinnk.Web.Areas.Account.Constants;
using NeoSistem.Trinnk.Web.Areas.Account.Models;
using NeoSistem.Trinnk.Web.Areas.Account.Models.Personal;
using NeoSistem.Trinnk.Web.Helpers;
using NeoSistem.Trinnk.Web.Models;
using NeoSistem.Trinnk.Web.Models.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace NeoSistem.Trinnk.Web.Areas.Account.Controllers
{
    public class PersonalController : BaseAccountController
    {
        private readonly IStoreService _storeService;
        private readonly IMemberStoreService _memberStoreService;
        private readonly IStoreInfoNumberShowService _storeNumberShowService;
        private readonly IStoreChangeHistoryService _storeChangeHistoryService;
        private readonly IPhoneService _phoneService;
        private readonly IPhoneChangeHistoryService _phoneChangeHistoryService;
        private readonly IAddressChangeHistoryService _addressChangeHistory;
        private readonly IAddressService _addressService;
        private readonly ICategoryService _categoryService;
        private readonly IMemberService _memberService;
        private readonly IMobileMessageService _mobileMessageService;
        private readonly ICategoryPlaceChoiceService _categoryPlaceChoiceService;

        public PersonalController(IStoreService storeService, IMemberStoreService memberStoreService,
            IStoreInfoNumberShowService storeNumberShowService, IStoreChangeHistoryService storeChangeHistoryService,
            IPhoneService phoneService, IPhoneChangeHistoryService phoneChangeHistoryService,
            IAddressChangeHistoryService addressChangeHistory, IAddressService addressService,
            ICategoryService categoryService, IMemberService memberService,
            IMobileMessageService mobileMessageService,
            ICategoryPlaceChoiceService categoryPlaceChoiceService)
        {
            this._storeService = storeService;
            this._memberStoreService = memberStoreService;
            this._storeNumberShowService = storeNumberShowService;
            this._storeChangeHistoryService = storeChangeHistoryService;
            this._phoneChangeHistoryService = phoneChangeHistoryService;
            this._phoneService = phoneService;
            this._addressService = addressService;
            this._addressChangeHistory = addressChangeHistory;
            this._categoryService = categoryService;
            this._memberService = memberService;
            this._mobileMessageService = mobileMessageService;
            this._categoryPlaceChoiceService = categoryPlaceChoiceService;

            this._storeService.CachingGetOrSetOperationEnabled = false;
            this._memberStoreService.CachingGetOrSetOperationEnabled = false;
            this._storeNumberShowService.CachingGetOrSetOperationEnabled = false;
            this._phoneService.CachingGetOrSetOperationEnabled = false;
            this._addressService.CachingGetOrSetOperationEnabled = false;
            this._categoryService.CachingGetOrSetOperationEnabled = false;
            this._memberService.CachingGetOrSetOperationEnabled = false;
            this._categoryPlaceChoiceService.CachingGetOrSetOperationEnabled = false;
        }

        #region Http Get

        public ActionResult Index()
        {
            int mainPartyId = AuthenticationUser.Membership.MainPartyId;

            if (AuthenticationUser.Membership.MemberType == (byte)MemberType.Enterprise)
            {
                mainPartyId = _memberStoreService.GetMemberStoreByMemberMainPartyId(mainPartyId).StoreMainPartyId.Value;
            }

            //var mainPartyCategoryItems = entities.RelMainPartyCategories.Where(c => c.MainPartyId == AuthenticationUser.Membership.MainPartyId);

            var phone = _phoneService.GetPhonesByMainPartyId(mainPartyId);
            var address = _addressService.GetAddressesByMainPartyId(mainPartyId);

            var model = new PersonalModel
            {
                //MainPartyCategoryItems = mainPartyCategoryItems,
                PhoneItems = phone,
                AddressItems = address,
                LeftMenu = LeftMenuConstants.CreateLeftMenuModel(LeftMenuConstants.GroupName.MyProfile, (byte)LeftMenuConstants.MyProfile.MyProfileHomePage)
            };
            var categoryHelps = _categoryPlaceChoiceService.GetCategoryPlaceChoiceByCategoryPlaceTypeByIsProduct((byte)HelpCategoryPlace.PersonalAccount, false);
            foreach (var item in categoryHelps)
            {
                string url = UrlBuilder.GetHelpCategoryUrl(item.CategoryId, item.Category.CategoryName);
                model.HelpList.Add(new MTHelpModeltem { Url = url, HelpCategoryName = item.Category.CategoryName });
            }
            var store = _storeService.GetStoreByMainPartyId(mainPartyId);
            if (store != null)
            {
                model.StoreLogo = store.StoreLogo;
                model.StoreName = store.StoreName;
                model.StoreMainPartyId = store.MainPartyId;
            }
            return View(model);
        }

        public ActionResult Update()
        {
            var model = new MemberModel();

            var member = _memberService.GetMemberByMainPartyId(AuthenticationUser.Membership.MainPartyId);

            model.MemberEmail = member.MemberEmail;
            model.MemberName = member.MemberName;
            model.MemberSurname = member.MemberSurname;
            //model.ReceiveEmail = member.ReceiveEmail.aVlue;
            model.Gender = member.Gender.HasValue ? member.Gender.Value : false;

            //var sectorItems = entities.Categories.Where(c => c.CategoryParentId == null);

            //var relMainPartyCategoryItems = entities.RelMainPartyCategories.Where(c => c.MainPartyId == AuthenticationUser.Membership.MainPartyId);

            //model.CategoryItems = sectorItems;
            //model.MemberRelatedCategory = relMainPartyCategoryItems;

            if (member.BirthDate.HasValue && member.BirthDate.ToString() != "01.01.0001 00:00:00")
            {
                model.Day = member.BirthDate.Value.Day;
                model.Month = member.BirthDate.Value.Month;
                model.Year = member.BirthDate.Value.Year;
            }
            else
            {
                model.Day = 0;
                model.Month = 0;
                model.Year = 0;
            }

            model.LeftMenu = LeftMenuConstants.CreateLeftMenuModel(LeftMenuConstants.GroupName.MyProfile, (byte)LeftMenuConstants.MyProfile.MyPersonalInfoUpdate);

            return View(model);
        }

        public ActionResult ChangePassword()
        {
            ChangePasswordModel changePasswordModel = new ChangePasswordModel();

            var memberModel = new MemberModel();

            var member = _memberService.GetMemberByMainPartyId(AuthenticationUser.Membership.MainPartyId);
            memberModel.MemberPassword = member.MemberPassword;
            memberModel.MemberEmail = member.MemberEmail;

            changePasswordModel.Member = memberModel;
            changePasswordModel.LeftMenu = LeftMenuConstants.CreateLeftMenuModel(LeftMenuConstants.GroupName.MyProfile, (byte)LeftMenuConstants.MyProfile.PasswordChange);

            return View(changePasswordModel);
        }

        public ActionResult ChangeEmail()
        {
            ChangeEmailModel changeEmailModel = new ChangeEmailModel();
            var memberModel = new MemberModel();
            var member = _memberService.GetMemberByMainPartyId(AuthenticationUser.Membership.MainPartyId);
            memberModel.MemberEmail = member.MemberEmail;

            changeEmailModel.Member = memberModel;
            changeEmailModel.LeftMenu = LeftMenuConstants.CreateLeftMenuModel(LeftMenuConstants.GroupName.MyProfile, (byte)LeftMenuConstants.MyProfile.EmailAddressChange);

            return View(changeEmailModel);
        }

        public ActionResult ChangeAddress(string gelenSayfa, string sonuc, string error, string urunNo, string uyeNo, string mtypePage)
        {
            ViewData["mtypePage"] = mtypePage;
            ViewData["uyeNo"] = uyeNo;
            ViewData["urunNo"] = urunNo;
            ViewData["error"] = "";
            if (gelenSayfa == "Teklif")
            {
                ViewData["gelenSayfa"] = "Teklif";
            }
            else if (gelenSayfa == "kurumsalaGec") { ViewData["gelenSayfa"] = "kurumsalaGec"; }
            if (sonuc == "basarili") ViewData["success"] = "true";

            if (error != null)
            {
                ViewData["error"] = error;
            }

            int mainPartyId = AuthenticationUser.Membership.MainPartyId;
            if (AuthenticationUser.Membership.MemberType == (byte)MemberType.Enterprise)
            {
                mainPartyId = _memberStoreService.GetMemberStoreByMemberMainPartyId(mainPartyId).StoreMainPartyId.Value;
            }
            var address = _addressService.GetFisrtAddressByMainPartyId(mainPartyId);
            var model = new AddressModel();
            var phone = _phoneService.GetPhonesByMainPartyId(mainPartyId);
            if (error == "PhoneActive") model.GsmPhone = phone.Where(x => x.PhoneType == (byte)PhoneType.Gsm).FirstOrDefault();
            ViewData["whatsappPhone"] = phone.Where(x => x.PhoneType == (byte)PhoneType.Whatsapp).FirstOrDefault();
            model.PhoneItems = phone;
            List<Locality> localityItems = new List<Locality>() { new Locality { LocalityId = 0, LocalityName = "< Lütfen Seçiniz >" } };
            List<Town> townItems = new List<Town>() { new Town { TownId = 0, TownName = "< Lütfen Seçiniz >" } };
            if (address != null)
            {
                model.Street = address.Street;
                model.Avenue = address.Avenue;
                model.DoorNo = address.DoorNo;
                model.ApartmentNo = address.ApartmentNo;
                model.PostCode = address.PostCode;
                model.CountryId = address.CountryId.HasValue ? address.CountryId.Value : 0;

                if (address.CityId.HasValue)
                {
                    model.CityId = address.CityId.Value;
                    _addressService.GetLocalitiesByCityId(model.CityId);
                    localityItems = _addressService.GetLocalitiesByCityId(model.CityId).ToList();
                    localityItems.Insert(0, new Locality { LocalityId = 0, LocalityName = "< Lütfen Seçiniz >" });
                }
                if (address.LocalityId.HasValue)
                {
                    model.LocalityId = address.LocalityId.Value;
                    townItems = _addressService.GetTownsByLocalityId(address.LocalityId.Value).ToList();
                    townItems.Insert(0, new Town { TownId = 0, TownName = "< Lütfen Seçiniz >" });
                }
                if (address.TownId.HasValue)
                {
                    model.TownId = address.TownId.Value;
                }
                if (address.AddressTypeId.HasValue)
                {
                    model.AddressTypeId = address.AddressTypeId.Value;
                }
            }
            else
                model.CountryId = 0;
            //var countryItems = entities.Countries.ToList();
            var countryItems = _addressService.GetAllCountries();
            countryItems.Insert(0, new Country { CountryId = 0, CountryName = "< Lütfen Seçiniz >" });
            model.CountryItems = new SelectList(countryItems, "CountryId", "CountryName", 0);
            var cityItems = _addressService.GetCitiesByCountryId(model.CountryId);
            cityItems.Insert(0, new City { CityId = 0, CityName = "< Lütfen Seçiniz >" });
            model.CityItems = new SelectList(cityItems, "CityId", "CityName", 0);
            model.LocalityItems = new SelectList(localityItems, "LocalityId", "LocalityName");
            model.TownItems = new SelectList(townItems, "TownId", "TownName");

            var isFirma = HttpContext.Request["display"];
            if (isFirma == null)
                model.LeftMenu = LeftMenuConstants.CreateLeftMenuModel(LeftMenuConstants.GroupName.MyProfile, (byte)LeftMenuConstants.MyProfile.MyPersonalAdressUpdate);
            else
                model.LeftMenu = LeftMenuConstants.CreateLeftMenuModel(LeftMenuConstants.GroupName.StoreSettings, (byte)LeftMenuConstants.MyProfile.MyPersonalAdressUpdate);
            return View(model);
        }

        public ActionResult PhoneActive(string activationCode, string typePage, string uyeNo, string urunNo, string gelenSayfa, string phoneNumber)
        {
            var phone1 = _phoneService.GetPhonesByMainPartyIdByPhoneType(AuthenticationUser.Membership.MainPartyId, PhoneTypeEnum.Gsm);
            if (phone1 != null)
            {
                ViewData["phoneNumber"] = phone1.PhoneCulture + phone1.PhoneAreaCode + phone1.PhoneNumber;
            }
            if (gelenSayfa == "kurumsalaGec")
            {
                ViewData["gelenSayfa"] = "kurumsalaGec";
            }
            else
            {
                ViewData["mtypePage"] = typePage;
                ViewData["uyeNo"] = uyeNo;
                ViewData["urunNo"] = urunNo;
            }

            int mainPartyId = AuthenticationUser.Membership.MainPartyId;
            if (AuthenticationUser.Membership.MemberType == (byte)MemberType.Enterprise)
            {
                mainPartyId = _memberStoreService.GetMemberStoreByMemberMainPartyId(mainPartyId).StoreMainPartyId.Value;
            }
            SmsHelper sms = new SmsHelper();
            string activeCode = sms.CreateActiveCode();
            Phone phone = _phoneService.GetPhonesByMainPartyIdByPhoneType(mainPartyId, PhoneTypeEnum.Gsm);
            phone.ActivationCode = activeCode;

            _phoneService.UpdatePhone(phone);

            MobileMessage messageTmp = _mobileMessageService.GetMobileMessageByMessageName("telefononayi");
            string messageMobile = messageTmp.MessageContent.Replace("#isimsoyisim#", AuthenticationUser.Membership.MemberName + " " + AuthenticationUser.Membership.MemberSurname).Replace("#aktivasyonkodu#", activeCode);

            sms.SendPhoneMessage(phone.PhoneCulture + phone.PhoneAreaCode + phone.PhoneNumber, messageMobile);
            return View();
        }

        //public ActionResult MyInterestsUpdate()
        //{
        //    MyInterestsUpdateModel model = new MyInterestsUpdateModel();
        //    var dataRelMainPartyCategory = new Data.RelMainPartyCategory();
        //    var member = entities.Members.SingleOrDefault(c => c.MainPartyId == AuthenticationUser.Membership.MainPartyId);

        //    model.CategoryList = entities.Categories.Where(c => c.CategoryParentId == null && c.MainCategoryType == (byte)MainCategoryType.Ana_Kategori).OrderBy(c => c.CategoryName);
        //    model.LeftMenu = LeftMenuConstants.CreateLeftMenuModel(LeftMenuConstants.GroupName.MyProfile, (byte)LeftMenuConstants.MyProfile.MyInterestsUpdate);
        //    model.SelectedMainCategoryList = dataRelMainPartyCategory.GetMainCategoryByAuthenticationUserID(AuthenticationUser.Membership.MainPartyId).AsCollection<CategoryListModel>().ToList();
        //    model.ReceiveEmail = member.ReceiveEmail.HasValue ? member.ReceiveEmail.Value : false;

        //    return View(model);
        //}

        #endregion Http Get

        #region Http Post

        [HttpPost]
        public ActionResult ChangeAddress(AddressModel model, string save, string gelSayfa, string DropDownInstitutionalPhoneAreaCode, string mtypePage, string uyeNo, string urunNo, string MembershipModel_AvenueOtherCountries)
        {
            if (uyeNo != "")
            {
                ViewData["mtypePage"] = mtypePage;
                ViewData["uyeNo"] = uyeNo;
                ViewData["urunNo"] = urunNo;
            }
            bool phoneActive = true;

            int mainPartyId = AuthenticationUser.Membership.MainPartyId;
            if (AuthenticationUser.Membership.MemberType == (byte)MemberType.Enterprise)
            {
                mainPartyId = _memberStoreService.GetMemberStoreByMemberMainPartyId(mainPartyId).StoreMainPartyId.Value;
            }
            if (save == "TelefonKayit")
            {
                if (model.InstitutionalGSMNumber != null && !string.IsNullOrWhiteSpace(model.InstitutionalGSMNumber))
                {
                    SmsHelper sms = new SmsHelper();
                    string activeCode = sms.CreateActiveCode();
                    //Telefon Güncelleme
                    var phoneUpdateBefore = _phoneService.GetPhonesByMainPartyIdByPhoneType(mainPartyId, PhoneTypeEnum.Gsm);
                    _phoneChangeHistoryService.InsertPhoneChangeHistoryForPhone(phoneUpdateBefore);

                    var phoneGsm = _phoneService.GetPhonesByMainPartyIdByPhoneType(mainPartyId, PhoneTypeEnum.Gsm);
                    phoneGsm.PhoneCulture = model.InstitutionalGSMCulture;
                    phoneGsm.PhoneAreaCode = model.InstitutionalGSMAreaCode;
                    phoneGsm.PhoneNumber = model.InstitutionalGSMNumber;
                    phoneGsm.ActivationCode = activeCode;

                    _phoneService.UpdatePhone(phoneGsm);

                    MobileMessage messageTmp = _mobileMessageService.GetMobileMessageByMessageName("telefononayi");

                    string messageMobile = messageTmp.MessageContent.Replace("#isimsoyisim#", AuthenticationUser.Membership.MemberName + " " + AuthenticationUser.Membership.MemberSurname).Replace("#aktivasyonkodu#", activeCode);
                    sms.SendPhoneMessage(phoneGsm.PhoneCulture + phoneGsm.PhoneAreaCode + phoneGsm.PhoneNumber, messageMobile);
                    ViewData["phoneNumber"] = phoneGsm.PhoneCulture + " " + phoneGsm.PhoneAreaCode + " " + phoneGsm.PhoneNumber;
                }
                ViewData["gelenSayfa"] = gelSayfa;
            }
            else
            {
                ViewData["gelenSayfa"] = gelSayfa;

                var phoneItems = _phoneService.GetPhonesByMainPartyId(mainPartyId);

                Phone phoneGsmLast = _phoneService.GetPhonesByMainPartyIdByPhoneType(mainPartyId, PhoneTypeEnum.Gsm);

                if (phoneItems != null)
                {
                    foreach (var item in phoneItems)
                    {
                        //Telefon Güncelleme
                        PhoneChangeHistory phoneChangeHistory = new PhoneChangeHistory();
                        phoneChangeHistory.MainPartyId = item.MainPartyId;
                        phoneChangeHistory.GsmType = item.GsmType;
                        phoneChangeHistory.PhoneAreaCode = item.PhoneAreaCode;
                        phoneChangeHistory.PhoneCulture = item.PhoneCulture;
                        phoneChangeHistory.PhoneId = item.PhoneId;
                        phoneChangeHistory.PhoneNumber = item.PhoneNumber;
                        phoneChangeHistory.PhoneType = item.PhoneType;
                        phoneChangeHistory.ActivationCode = item.ActivationCode;
                        phoneChangeHistory.active = item.active;
                        phoneChangeHistory.UpdatedDate = DateTime.Now;
                        _phoneChangeHistoryService.InsertPhoneChange(phoneChangeHistory);
                        _phoneService.DeletePhone(item);
                    }
                }

                if (model.InstitutionalPhoneNumber != null && !string.IsNullOrWhiteSpace(model.InstitutionalPhoneNumber))
                {
                    var phone1 = new Phone
                    {
                        MainPartyId = mainPartyId,
                        PhoneAreaCode = model.InstitutionalPhoneAreaCode,
                        PhoneCulture = model.InstitutionalPhoneCulture,
                        PhoneNumber = model.InstitutionalPhoneNumber,
                        PhoneType = (byte)PhoneType.Phone,
                        GsmType = null
                    };
                    _phoneService.InsertPhone(phone1);
                }

                if (model.InstitutionalPhoneNumber2 != null && !string.IsNullOrWhiteSpace(model.InstitutionalPhoneNumber2))
                {
                    var phone2 = new Phone
                    {
                        MainPartyId = mainPartyId,
                        PhoneAreaCode = model.InstitutionalPhoneAreaCode2,
                        PhoneCulture = model.InstitutionalPhoneCulture2,
                        PhoneNumber = model.InstitutionalPhoneNumber2,
                        PhoneType = (byte)PhoneType.Phone,
                        GsmType = null
                    };
                    _phoneService.InsertPhone(phone2);
                }

                if (model.InstitutionalGSMNumber != null && !string.IsNullOrWhiteSpace(model.InstitutionalGSMNumber))
                {
                    string phoneModel = model.InstitutionalGSMCulture + model.InstitutionalGSMAreaCode + model.InstitutionalGSMNumber;
                    string phoneData = "";
                    SmsHelper sms = new SmsHelper();
                    string activeCode = sms.CreateActiveCode();
                    var phoneGsm = new Phone
                    {
                        MainPartyId = mainPartyId,
                        PhoneAreaCode = model.InstitutionalGSMAreaCode,
                        PhoneCulture = model.InstitutionalGSMCulture,
                        PhoneNumber = model.InstitutionalGSMNumber,
                        PhoneType = (byte)PhoneType.Gsm,
                        ActivationCode = activeCode,
                        active = 0,
                        GsmType = model.GsmType
                    };

                    if (phoneGsmLast != null)
                    {
                        phoneData = phoneGsmLast.PhoneCulture + phoneGsmLast.PhoneAreaCode + phoneGsmLast.PhoneNumber;
                        if (phoneData != phoneModel || phoneGsmLast.active == 0)
                        {
                            phoneGsm.active = 0;

                            MobileMessage messageTmp = _mobileMessageService.GetMobileMessageByMessageName("telefononayi");
                            string messageMobile = messageTmp.MessageContent.Replace("#isimsoyisim#", AuthenticationUser.Membership.MemberName + " " + AuthenticationUser.Membership.MemberSurname).Replace("#aktivasyonkodu#", activeCode);

                            sms.SendPhoneMessage(phoneGsm.PhoneCulture + phoneGsm.PhoneAreaCode + phoneGsm.PhoneNumber, messageMobile);
                        }
                        else
                        {
                            phoneActive = false;
                            phoneGsm.active = 1;
                        }
                    }
                    else
                    {
                        MobileMessage messageTmp = _mobileMessageService.GetMobileMessageByMessageName("telefononayi");
                        string messageMobile = messageTmp.MessageContent.Replace("#isimsoyisim#", AuthenticationUser.Membership.MemberName + " " + AuthenticationUser.Membership.MemberSurname).Replace("#aktivasyonkodu#", activeCode);
                        sms.SendPhoneMessage(phoneGsm.PhoneCulture + phoneGsm.PhoneAreaCode + phoneGsm.PhoneNumber, messageMobile);
                    }

                    _phoneService.InsertPhone(phoneGsm);

                    ViewData["phoneNumber"] = phoneGsm.PhoneCulture + phoneGsm.PhoneAreaCode + phoneGsm.PhoneNumber;
                }
                else
                {
                    phoneActive = false;
                }

                var address = _addressService.GetFisrtAddressByMainPartyId(mainPartyId);
                bool hasAddress = false;
                if (address != null)
                {
                    //Addres change log save
                    //var addressModel = _addressService.GetFisrtAddressByMainPartyId(mainPartyId);
                    _addressChangeHistory.AddAddressChangeHistoryForAddress(address);
                    hasAddress = true;
                }
                else
                {
                    address = new Address();
                }

                if (model.CityId > 0)
                    address.CityId = model.CityId;
                else
                    address.CityId = null;

                if (model.CountryId > 0)
                    address.CountryId = model.CountryId;
                else
                    address.CountryId = null;

                if (model.TownId > 0)
                    address.TownId = model.TownId;
                else
                    address.TownId = null;

                if (model.LocalityId > 0)
                    address.LocalityId = model.LocalityId;
                else
                    address.LocalityId = null;

                address.MainPartyId = mainPartyId;

                if (model.AddressTypeId > 0)
                    address.AddressTypeId = model.AddressTypeId;
                else
                    address.AddressTypeId = null;

                address.Street = model.Street;
                address.DoorNo = model.DoorNo;
                address.PostCode = model.PostCode;
                if (model.CountryId != 246 && model.CountryId > 0)
                {
                    //phoneActive = false;
                    address.Avenue = MembershipModel_AvenueOtherCountries;
                }
                else
                {
                    address.Avenue = model.Avenue;
                }
                address.ApartmentNo = model.ApartmentNo;
                address.AddressDefault = true;

                if (hasAddress)
                {
                    _addressService.UpdateAddress(address);
                }
                else
                {
                    _addressService.InsertAdress(address);
                }

                if (model.InstitutionalFaxNumber != null && !string.IsNullOrWhiteSpace(model.InstitutionalFaxNumber))
                {
                    var phoneFax = new Phone
                    {
                        MainPartyId = mainPartyId,
                        PhoneAreaCode = model.InstitutionalFaxAreaCode,
                        PhoneCulture = model.InstitutionalFaxCulture,
                        PhoneNumber = model.InstitutionalFaxNumber,
                        PhoneType = (byte)PhoneType.Fax,
                        GsmType = null
                    };
                    _phoneService.InsertPhone(phoneFax);
                }
                if (!string.IsNullOrWhiteSpace(model.GsmWhatsappNumber))
                {
                    var curPhoneGsmForWp = new Phone
                    {
                        MainPartyId = mainPartyId,
                        PhoneAreaCode = model.GsmWhatsappAreaCode,
                        PhoneCulture = model.GsmWhatsappCulture,
                        PhoneNumber = model.GsmWhatsappNumber,
                        PhoneType = (byte)PhoneType.Whatsapp
                    };
                    _phoneService.InsertPhone(curPhoneGsmForWp);
                }
            }
            if (phoneActive)
                return View("PhoneActive");
            else return RedirectToAction("ChangeAddress", "Personal", new { sonuc = "basarili" });
        }

        [HttpPost]
        public ActionResult PhoneActive(FormCollection frm, string gelSayfa)
        {
            string mtypePage = frm[0].ToString();
            string memberNo = frm[1].ToString();
            string productNo = frm[2].ToString();
            string activationCode = frm[4].ToString();
            string gelenSayfa = frm[3].ToString();
            int mainPartyId = AuthenticationUser.Membership.MainPartyId;
            if (AuthenticationUser.Membership.MemberType == (byte)MemberType.Enterprise)
            {
                mainPartyId = _memberStoreService.GetMemberStoreByMemberMainPartyId(mainPartyId).StoreMainPartyId.Value;
            }
            var phone = _phoneService.GetPhonesByMainPartyIdByPhoneType(mainPartyId, PhoneTypeEnum.Gsm);
            if (phone != null)
            {
                if (phone.ActivationCode == activationCode)
                {
                    phone.active = 1;
                    _phoneService.UpdatePhone(phone);

                    int memberNoNumber = 0;
                    int.TryParse(memberNo, out memberNoNumber);
                    if (gelenSayfa == "kurumsalaGec")
                    {
                        return RedirectToAction("InstitutionalStep", "MemberType");
                    }
                    if (memberNoNumber > 0)
                    {
                        return RedirectToAction("index", "Message", new { MessagePageType = mtypePage, UyeNo = memberNo, UrunNo = productNo });
                    }
                    else
                    {
                        return RedirectToAction("ChangeAddress", "Personal", new { sonuc = "basarili" });
                    }
                }
                else
                {
                    ViewData["phoneNumber"] = phone.PhoneCulture + " " + phone.PhoneAreaCode + " " + phone.PhoneNumber;
                    ViewData["error"] = "true";
                }
            }
            else
            {
                return View();
            }
            return View();
        }

        [HttpPost]
        public JsonResult CheckMail(FormCollection coll)
        {
            if (coll.Count > 0)
            {
                string email = coll[0].ToString();
                var itemMember = _memberService.GetMemberByMemberEmail(email);
                var itemStore = _storeService.GetStoreByStoreEmail(email);

                if (itemMember != null || itemStore != null)
                {
                    return Json("&nbsp;&nbsp;&nbsp;<span style=\"color:Red;font-size:11px;\">E-Posta adresi kullanılıyor.</span>");
                }
                return Json(true);
            }
            return Json(true);
        }

        [HttpPost]
        public ActionResult ChangeEmail(ChangeEmailModel model)
        {
            var member = _memberService.GetMemberByMainPartyId(AuthenticationUser.Membership.MainPartyId);
            if (model.Member.NewEmail == model.Member.MemberEmailAgain && model.Member.MemberPassword == member.MemberPassword)
            {
                TempData["success"] = true;
                member.MemberEmail = model.Member.NewEmail;
                _memberService.UpdateMember(member);
            }
            else
            {
                TempData["success"] = false;
            }

            //var newMember = entities.Members.SingleOrDefault(c => c.MainPartyId == AuthenticationUser.Membership.MainPartyId);
            //var identity = new ClaimsIdentity(new[] { new Claim(ClaimTypes.NameIdentifier, newMember.MainPartyId.ToString()) }, "LoginCookie");

            //var ctx = Request.GetOwinContext();
            //var authManager = ctx.Authentication;
            //authManager.SignIn(identity);

            return RedirectToAction("ChangeEmail", "Personal");
        }

        [HttpPost]
        public ActionResult ChangePassword(ChangePasswordModel model)
        {
            var member = _memberService.GetMemberByMainPartyId(AuthenticationUser.Membership.MainPartyId);
            if (model.Member.NewPassword == model.Member.NewPasswordAgain)
            {
                member.MemberPassword = model.Member.NewPassword;
                _memberService.UpdateMember(member);
            }

            //var curMember = entities.Members.SingleOrDefault(c => c.MainPartyId == AuthenticationUser.Membership.MainPartyId);
            //AuthenticationUser.Membership = curMember;

            return RedirectToAction("Index", "Personal");
        }

        [HttpPost]
        public ActionResult Update(MemberModel model, string Day, string Month, string Year, FormCollection coll)
        {
            var member = _memberService.GetMemberByMainPartyId(AuthenticationUser.Membership.MainPartyId);
            member.MemberName = model.MemberName;
            member.MemberSurname = model.MemberSurname;

            if (int.Parse(Year) > 0 && int.Parse(Month) > 0 && int.Parse(Day) > 0)
            {
                var bithDate = new DateTime(int.Parse(Year), int.Parse(Month), int.Parse(Day));

                if (bithDate.Year > 0)
                    member.BirthDate = bithDate;
                else
                    member.BirthDate = null;
            }

            if (coll["Gender"] == "2")
            {
                member.Gender = true;
            }
            else
            {
                member.Gender = false;
            }

            _memberService.UpdateMember(member);

            //var curMember = entities.Members.SingleOrDefault(c => c.MainPartyId == AuthenticationUser.Membership.MainPartyId);

            //AuthenticationUser.Membership = curMember;

            return RedirectToAction("Index", "Personal");
        }

        //[HttpPost]
        //public ActionResult MyInterestsUpdate(MyInterestsUpdateModel model, FormCollection coll)
        //{
        //    #region Sector List

        //    var mainPartyCategories = entities.RelMainPartyCategories.Where(c => c.MainPartyId == AuthenticationUser.Membership.MainPartyId);

        //    var member = entities.Members.SingleOrDefault(c => c.MainPartyId == AuthenticationUser.Membership.MainPartyId);
        //    member.ReceiveEmail = model.ReceiveEmail;
        //    entities.SaveChanges();

        //    using (var trans = new TransactionScope())
        //    {
        //        foreach (var item in mainPartyCategories)
        //        {
        //            var mpCat = entities.RelMainPartyCategories.SingleOrDefault(c => c.RelMainPartyCategoryId == item.RelMainPartyCategoryId);
        //            entities.RelMainPartyCategories.DeleteObject(mpCat);
        //        }
        //        entities.SaveChanges();
        //        trans.Complete();
        //    }

        //    using (var trans = new TransactionScope())
        //    {
        //        if (coll["MainPartyRelatedCategory"] != null)
        //        {
        //            string[] acType = coll["MainPartyRelatedCategory"].Split(',');
        //            if (acType != null)
        //            {
        //                for (int i = 0; i < acType.Length; i++)
        //                {
        //                    if (acType.GetValue(i).ToString() != "false")
        //                    {
        //                        var curRelMainPartyCategory = new RelMainPartyCategory
        //                        {
        //                            CategoryId = acType.GetValue(i).ToInt32(),
        //                            MainPartyId = AuthenticationUser.Membership.MainPartyId,
        //                        };
        //                        entities.RelMainPartyCategories.AddObject(curRelMainPartyCategory);
        //                        entities.SaveChanges();
        //                    }
        //                }
        //                trans.Complete();
        //            }
        //        }
        //    }

        //    #endregion

        //    return RedirectToAction("Index", "Personal");
        //}

        public ActionResult TaxUpdate(string islem)
        {
            var memberStore = _memberStoreService.GetMemberStoreByMemberMainPartyId(Convert.ToInt32(AuthenticationUser.Membership.MainPartyId));
            var store = _storeService.GetStoreByMainPartyId(memberStore.StoreMainPartyId.Value);
            var storeInfoNumber = _storeNumberShowService.GetStoreInfoNumberShowByStoreMainPartyId(Convert.ToInt32(memberStore.StoreMainPartyId));

            TaxUpdateViewModel viewModel = new TaxUpdateViewModel();
            viewModel.TaxNumber = store.TaxNumber;
            viewModel.StoreMainPartyId = store.MainPartyId;
            viewModel.TaxOffice = store.TaxOffice;
            viewModel.MersisNo = store.MersisNo;
            viewModel.TradeRegistrNo = store.TradeRegistrNo;
            viewModel.TaxNumberShow = false;
            viewModel.TaxOfficeShow = false;
            viewModel.MersisNoShow = false;
            viewModel.TradeRegistrNoShow = false;
            if (storeInfoNumber != null)
            {
                viewModel.TaxNumberShow = storeInfoNumber.TaxNumberShow;
                viewModel.TaxOfficeShow = storeInfoNumber.TaxOfficeShow;
                viewModel.MersisNoShow = storeInfoNumber.MersisNoShow;
                viewModel.TradeRegistrNoShow = storeInfoNumber.TradeRegistryNoShow;
            }

            viewModel.LeftMenu = LeftMenuConstants.CreateLeftMenuModel(LeftMenuConstants.GroupName.MyProfile, (byte)LeftMenuConstants.MyProfile.MyProfileHomePage);
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult TaxUpdate(TaxUpdateViewModel modelView)
        {
            //Store Change History
            var store = _storeService.GetStoreByMainPartyId(Convert.ToInt32(modelView.StoreMainPartyId));
            _storeChangeHistoryService.AddStoreChangeHistoryForStore(store);

            store.TaxNumber = modelView.TaxNumber;
            store.TaxOffice = modelView.TaxOffice;
            store.MersisNo = modelView.MersisNo;
            store.TradeRegistrNo = modelView.TradeRegistrNo;

            _storeService.UpdateStore(store);

            modelView.LeftMenu = LeftMenuConstants.CreateLeftMenuModel(LeftMenuConstants.GroupName.MyProfile, (byte)LeftMenuConstants.MyProfile.MyProfileHomePage);
            bool storeInfoNew = false;
            var storeInfo = _storeNumberShowService.GetStoreInfoNumberShowByStoreMainPartyId(modelView.StoreMainPartyId);
            if (storeInfo == null)
            {
                storeInfoNew = true;
                storeInfo = new global::Trinnk.Entities.Tables.Stores.StoreInfoNumberShow();
            }
            storeInfo.TaxNumberShow = modelView.TaxNumberShow;
            storeInfo.TaxOfficeShow = modelView.TaxOfficeShow;
            storeInfo.MersisNoShow = modelView.MersisNoShow;
            storeInfo.StoreMainpartyId = modelView.StoreMainPartyId;
            storeInfo.TradeRegistryNoShow = modelView.TradeRegistrNoShow;
            if (storeInfoNew) _storeNumberShowService.InsertStoreInfoNumberShow(storeInfo);
            else _storeNumberShowService.UpdateStoreInfoNumberShow(storeInfo);

            ViewBag.opr = true;
            return View(modelView);
        }

        #endregion Http Post

        #region Partial View

        //public ActionResult GetCategories(int categoryID, bool isActive)
        //{
        //    GetCategoriesModel getCategoriesModel = new GetCategoriesModel();
        //    getCategoriesModel.ParentCategoryID = categoryID;
        //    getCategoriesModel.ProductGroupList = entities.Categories.Where(c => c.CategoryParentId == categoryID && c.CategoryType == (byte)CategoryType.ProductGroup && c.ProductCount.HasValue).OrderBy(c => c.CategoryOrder).ThenBy(e => e.CategoryName);
        //    getCategoriesModel.MemberRelatedCategory = entities.RelMainPartyCategories.Where(c => c.MainPartyId == AuthenticationUser.Membership.MainPartyId);
        //    getCategoriesModel.IsActive = isActive;
        //    return PartialView(getCategoriesModel);
        //}

        #endregion Partial View
    }
}