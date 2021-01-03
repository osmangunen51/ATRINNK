using System;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Net.Mail;
using System.Net;
using System.Configuration;
using System.Globalization;
using System.Text.RegularExpressions;

using MakinaTurkiye.Utilities.HttpHelpers;
using MakinaTurkiye.Services.Common;
using MakinaTurkiye.Services.Stores;
using MakinaTurkiye.Services.Members;
using MakinaTurkiye.Entities.Tables.Common;
using MakinaTurkiye.Entities.Tables.Stores;
using MakinaTurkiye.Entities.Tables.Members;
using MakinaTurkiye.Services.Packets;
using MakinaTurkiye.Entities.Tables.Messages;
using MakinaTurkiye.Services.Messages;
using MakinaTurkiye.Entities.Tables.Logs;
using MakinaTurkiye.Services.Logs;

using NeoSistem.EnterpriseEntity.Extensions.Data;
using NeoSistem.MakinaTurkiye.Web.Models;
using NeoSistem.MakinaTurkiye.Web.Models.Authentication;
using NeoSistem.MakinaTurkiye.Core.Web.Helpers;
using NeoSistem.MakinaTurkiye.Web.Models.UtilityModel;
using NeoSistem.MakinaTurkiye.Web.Areas.Account.Constants;
using NeoSistem.MakinaTurkiye.Web.Controllers;
using NeoSistem.MakinaTurkiye.Web.Helpers;
using MakinaTurkiye.Utilities.Controllers;

namespace NeoSistem.MakinaTurkiye.Web.Areas.Account.Controllers
{

    public class MemberTypeController : BaseAccountController
    {

        #region Fields

        private readonly IPhoneService _phoneService;
        private readonly IActivityTypeService _activityTypeService;
        private readonly IStoreService _storeService;
        private readonly IMemberService _memberService;
        private readonly IAddressService _addressService;
        private readonly IConstantService _constantService;
        private readonly IPacketService _packetService;
        private readonly IStoreActivityTypeService _storeActivityTypeService;
        private readonly IStoreActivityCategoryService _storeActivityCategoryService;
        private readonly IMemberStoreService _memberStoreService;
        private readonly IMessagesMTService _messagesMTService;
        private readonly IUserLogService _userLogService;
        private readonly IMobileMessageService _mobileMessageService;

        #endregion

        #region Ctor

        public MemberTypeController(IPhoneService phoneService,IActivityTypeService activityTypeService,
            IStoreService storeService, IMemberService memberService, IAddressService addressService,
            IConstantService constantService, IPacketService packetService,
            IStoreActivityTypeService storeActivityTypeService,
            IStoreActivityCategoryService storeActivityCategoryService,
            IMemberStoreService memberStoreService,
            IMessagesMTService messagesMTService, IUserLogService userLogService,
            IMobileMessageService mobileMessageService)
        {
            this._phoneService = phoneService;
            this._activityTypeService = activityTypeService;
            this._storeService = storeService;
            this._memberService = memberService;
            this._addressService = addressService;
            this._constantService = constantService;
            this._packetService = packetService;
            this._storeActivityTypeService = storeActivityTypeService;
            this._storeActivityCategoryService = storeActivityCategoryService;
            this._memberStoreService = memberStoreService;
            this._messagesMTService = messagesMTService;
            this._userLogService = userLogService;
            this._mobileMessageService = mobileMessageService;


            this._phoneService.CachingGetOrSetOperationEnabled = false;
            this._activityTypeService.CachingGetOrSetOperationEnabled = false;
            this._storeService.CachingGetOrSetOperationEnabled = false; ;
            this._memberService.CachingGetOrSetOperationEnabled = false;
            this._addressService.CachingGetOrSetOperationEnabled = false;
            this._constantService.CachingGetOrSetOperationEnabled = false;
            this._packetService.CachingGetOrSetOperationEnabled = false;
            this._storeActivityTypeService.CachingGetOrSetOperationEnabled = false;
            this._memberStoreService.CachingGetOrSetOperationEnabled = false;
        }

        #endregion

        public ActionResult Completed()
        {
            return View();
        }

        public ActionResult Error()
        {
            return View();
        }
        private static Regex sUserNameAllowedRegEx = new Regex("^[a-zA-Z0-9]+$", RegexOptions.Compiled);
        public MembershipViewModel MembershipSessionModel
        {
            get
            {
                if (Session["MembershipSessionModel"] == null)
                {
                    Session["MembershipSessionModel"] = new MembershipViewModel();
                }
                return Session["MembershipSessionModel"] as MembershipViewModel;
            }
            set { Session["MembershipSessionModel"] = value; }
        }

        //----------------------------------------------------------------------------FİRMA ÜYELİĞİ----------------------------------------------------------------------------\\

        public ActionResult InstitutionalStep()
        {

            var address = _addressService.GetFisrtAddressByMainPartyId(AuthenticationUser.Membership.MainPartyId);

            if(address==null)
            {

                return RedirectToAction("ChangeAddress", "Personal", new { gelenSayfa = "kurumsalaGec" });
            }
            else {
                if(address.CityId==null)
                {
                    return RedirectToAction("ChangeAddress", "Personal", new { gelenSayfa = "kurumsalaGec" });

                }
                var phone = _phoneService.GetPhonesByMainPartyIdByPhoneType(AuthenticationUser.Membership.MainPartyId, PhoneTypeEnum.Gsm);
                if (phone != null)
                {
                    if (phone.active != 1)
                    {
                        return RedirectToAction("ChangeAddress", "Personal", new { error = "PhoneActive", gelenSayfa = "kurumsalaGec" });
                    }
                }
                if (AuthenticationUser.Membership.MemberType == (byte)MemberType.Enterprise)
                {
                    return RedirectToAction("Index", "Home");
                }
                else
                {

                    ViewData["leftMenu"] = LeftMenuConstants.CreateLeftMenuModel(LeftMenuConstants.GroupName.MyAccount);

                    return View(MembershipSessionModel);
                }
            }
        }

        [HttpPost]
        public ActionResult InstitutionalStep(MembershipViewModel model, string Day, string Month, string Year, FormCollection coll)
        {


            if (AuthenticationUser.Membership.MemberType == (byte)MemberType.Enterprise)
            {
                return RedirectToAction("Index", "Home");
            }
            else if (Session["MembershipSessionModel"] == null)
            {
                return RedirectToAction("InstitutionalStep", "MemberType");
            }
            else
            {

                SessionMembershipViewModel.MembershipViewModel.MembershipModel.MemberType = (byte)MemberType.Enterprise;
                SessionMembershipViewModel.MembershipViewModel.MembershipModel.MemberTitleType = model.MembershipModel.StoreActiveType;
                return RedirectToAction("InstitutionalStep1", "MemberType");
            }
        }

        public ActionResult InstitutionalStep1()
        {
            if (AuthenticationUser.Membership.MemberType == (byte)MemberType.Enterprise)
            {
                return RedirectToAction("Index", "Home");
            }
            else if (Session["MembershipSessionModel"] == null)
            {
                return RedirectToAction("InstitutionalStep", "MemberType");
            }
            else
            {
                ViewData["leftMenu"] = LeftMenuConstants.CreateLeftMenuModel(LeftMenuConstants.GroupName.MyAccount);
                return View(MembershipSessionModel);
            }
        }

        [HttpPost]
        public ActionResult InstitutionalStep1(FormCollection coll, bool InsertButton)
        {
            if (AuthenticationUser.Membership.MemberType == (byte)MemberType.Enterprise)
            {
                return RedirectToAction("Index", "Home");
            }
              if (SessionMembershipViewModel.MembershipViewModel.MembershipModel.MemberType==0)
            {
                Session["TimeOut"] = true;
                return RedirectToAction("InstitutionalStep", "MemberType");
            }
            else
            {
                if (InsertButton)
                {
                    if (!string.IsNullOrWhiteSpace(SessionMembershipViewModel.MembershipViewModel.MembershipModel.StoreLogo))
                    {
                        FileHelpers.Delete(AppSettings.StoreLogoFolder + SessionMembershipViewModel.MembershipViewModel.MembershipModel.StoreLogo);
                        FileHelpers.Delete(AppSettings.StoreLogoThumb100x100Folder + SessionMembershipViewModel.MembershipViewModel.MembershipModel.StoreLogo);
                        FileHelpers.Delete(AppSettings.StoreLogoThumb300x200Folder + SessionMembershipViewModel.MembershipViewModel.MembershipModel.StoreLogo);
                        //FileHelpers.Delete(AppSettings.StoreLogoThumb170x90Folder + SessionMembershipViewModel.MembershipViewModel.MembershipModel.StoreLogo);
                        //FileHelpers.Delete(AppSettings.StoreLogoThumb200x100Folder + SessionMembershipViewModel.MembershipViewModel.MembershipModel.StoreLogo);
                        //FileHelpers.Delete(AppSettings.StoreLogoThumb55x40Folder + SessionMembershipViewModel.MembershipViewModel.MembershipModel.StoreLogo);
                        //FileHelpers.Delete(AppSettings.StoreLogoThumb75x75Folder + SessionMembershipViewModel.MembershipViewModel.MembershipModel.StoreLogo);
                    }
                    string fileName = String.Empty;
                    if (Request.Files.Count > 0)
                    {
                        HttpPostedFileBase file = Request.Files[0];
                        if (file.ContentLength > 0)
                        {
                            //var thumns = new Dictionary<string, string>();
                            //thumns.Add(AppSettings.StoreLogoThumb110x110Folder, "110x110");
                            //thumns.Add(AppSettings.StoreLogoThumb150x90Folder, "150x90");
                            //thumns.Add(AppSettings.StoreLogoThumb170x90Folder, "170x90");
                            //thumns.Add(AppSettings.StoreLogoThumb200x100Folder, "200x100");
                            //thumns.Add(AppSettings.StoreLogoThumb55x40Folder, "55x40");
                            //thumns.Add(AppSettings.StoreLogoThumb75x75Folder, "75x75");
                            //fileName = FileHelpers.ImageResize(AppSettings.StoreLogoFolder, file, thumns);
                            var thumns = new Dictionary<string, string>();
                            thumns.Add(AppSettings.StoreLogoThumb100x100Folder, "100x100");
                            thumns.Add(AppSettings.StoreLogoThumb300x200Folder, "300x200");
                            fileName = FileHelpers.ImageResize(AppSettings.StoreLogoFolder, file, thumns);
                        }
                    }

                    SessionMembershipViewModel.MembershipViewModel.MembershipModel.StoreLogo = fileName;
                    ViewData["leftMenu"] = LeftMenuConstants.CreateLeftMenuModel(LeftMenuConstants.GroupName.MyAccount);
                    var model = SessionMembershipViewModel.MembershipViewModel;
                    return View(model);
                }
                else
                {
                    string fileName = String.Empty;

                    if (Request.Files.Count > 0)
                    {
                        HttpPostedFileBase file = Request.Files[0];
                        if (file.ContentLength > 0)
                        {
                            if (!string.IsNullOrWhiteSpace(SessionMembershipViewModel.MembershipViewModel.MembershipModel.StoreLogo))
                            {
                                FileHelpers.Delete(AppSettings.StoreLogoFolder + SessionMembershipViewModel.MembershipViewModel.MembershipModel.StoreLogo);
                            }

                            //fileName = FileHelpers.ImageThumbnail(AppSettings.StoreLogoFolder, file, 100, FileHelpers.ThumbnailType.Width);

                            var thumns = new Dictionary<string, string>();
                            thumns.Add(AppSettings.StoreLogoThumb100x100Folder, "100x100");
                            thumns.Add(AppSettings.StoreLogoThumb300x200Folder, "300x200");
                            fileName = FileHelpers.ImageResize(AppSettings.StoreLogoFolder, file, thumns);

                            SessionMembershipViewModel.MembershipViewModel.MembershipModel.StoreLogo = fileName;
                        }
                    }
                }

                    return RedirectToAction("InstitutionalStep3", "MemberType");

            }



        }

        public ActionResult InstitutionalStep2()
        {
            if (AuthenticationUser.Membership.MemberType == (byte)MemberType.Enterprise)
            {
                return RedirectToAction("Index", "Home");
            }
            else if (Session["MembershipSessionModel"] == null)
            {
                return RedirectToAction("InstitutionalStep", "MemberType");
            }
            else
            {
                var address = _addressService.GetFisrtAddressByMainPartyId(AuthenticationUser.Membership.MainPartyId);
                MembershipSessionModel.PhoneItems = _phoneService.GetPhonesByMainPartyId(AuthenticationUser.Membership.MainPartyId);

                if (address != null && AuthenticationUser.Membership.MemberType == (byte)MemberType.Individual)
                {
                    MembershipSessionModel.MembershipModel.Street = address.Street;
                    MembershipSessionModel.MembershipModel.Avenue = address.Avenue;
                    MembershipSessionModel.MembershipModel.DoorNo = address.DoorNo;
                    MembershipSessionModel.MembershipModel.ApartmentNo = address.ApartmentNo;
                    MembershipSessionModel.MembershipModel.AddressTypeId = address.AddressTypeId.Value;

                    MembershipSessionModel.CountryItems = new SelectList(_addressService.GetAllCountries(), "CountryId", "CountryName", 0);
                    MembershipSessionModel.MembershipModel.CountryId = AppSettings.Turkiye;

                    var cityItems = _addressService.GetCitiesByCountryId(MembershipSessionModel.MembershipModel.CountryId);
                    cityItems.Insert(0, new City { CityId = 0, CityName = "< Lütfen Seçiniz >" });

                    List<Locality> localityItems = new List<Locality>() { new Locality { LocalityId = 0, LocalityName = "< Lütfen Seçiniz >" } };

                    if (address.CityId.HasValue)
                    {
                        MembershipSessionModel.MembershipModel.CityId = address.CityId.Value;
                        localityItems = _addressService.GetLocalitiesByCityId(MembershipSessionModel.MembershipModel.CityId).ToList();
                        localityItems.Insert(0, new Locality { LocalityId = 0, LocalityName = "< Lütfen Seçiniz >" });
                    }

                    List<Town> townItems = new List<Town>() { new Town { TownId = 0, TownName = "< Lütfen Seçiniz >" } };
                    if (address.LocalityId.HasValue)
                    {
                        MembershipSessionModel.MembershipModel.LocalityId = address.LocalityId.Value;

                        if (address.TownId.HasValue)
                        {
                            MembershipSessionModel.MembershipModel.TownId = address.TownId.Value;
                            townItems = _addressService.GetTownsByLocalityId(MembershipSessionModel.MembershipModel.LocalityId).ToList();
                            townItems.Insert(0, new Town { TownId = 0, TownName = "< Lütfen Seçiniz >" });
                        }
                    }

                    MembershipSessionModel.CityItems = new SelectList(cityItems, "CityId", "CityName", address.CityId);
                    MembershipSessionModel.LocalityItems = new SelectList(localityItems, "LocalityId", "LocalityName");
                    MembershipSessionModel.TownItems = new SelectList(townItems, "TownId", "TownName");
                }
                else
                {
                    MembershipSessionModel.CityItems = null;
                }

                var phoneItems = _phoneService.GetPhonesByMainPartyId(AuthenticationUser.Membership.MainPartyId);
                if (phoneItems != null)
                {
                    string InstitutionalPhoneAreaCode = "";
                    string InstitutionalPhoneAreaCode2 = "";
                    string InstitutionalPhoneCulture = "";
                    string InstitutionalPhoneCulture2 = "";
                    string InstitutionalPhoneNumber = "";
                    string InstitutionalPhoneNumber2 = "";

                    string InstitutionalGSMAreaCode = "";
                    string InstitutionalGSMCulture = "";
                    string InstitutionalGSMNumber = "";

                    string InstitutionalFaxNumber = "";
                    string InstitutionalFaxCulture = "";
                    string InstitutionalFaxAreaCode = "";

                    var phone = phoneItems.Where(c => c.PhoneType == (byte)PhoneType.Phone).AsEnumerable();
                    if (phone.Count() > 0)
                    {
                        InstitutionalPhoneAreaCode = phone.First().PhoneAreaCode;
                        InstitutionalPhoneCulture = phone.First().PhoneCulture;
                        InstitutionalPhoneNumber = phone.First().PhoneNumber;
                        if (phone.Count() > 1)
                        {
                            InstitutionalPhoneAreaCode2 = phone.Last().PhoneAreaCode;
                            InstitutionalPhoneCulture2 = phone.Last().PhoneCulture;
                            InstitutionalPhoneNumber2 = phone.Last().PhoneNumber;
                        }
                    }

                    var gsmItem = phoneItems.SingleOrDefault(c => c.PhoneType == (byte)PhoneType.Gsm);
                    if (gsmItem != null)
                    {
                        InstitutionalGSMAreaCode = gsmItem.PhoneAreaCode;
                        InstitutionalGSMCulture = gsmItem.PhoneCulture;
                        InstitutionalGSMNumber = gsmItem.PhoneNumber;

                        MembershipSessionModel.MembershipModel.GsmType = gsmItem.GsmType.Value;
                    }
                    var faxItem = phoneItems.SingleOrDefault(c => c.PhoneType == (byte)PhoneType.Fax);
                    if (faxItem != null)
                    {
                        InstitutionalFaxAreaCode = faxItem.PhoneAreaCode;
                        InstitutionalFaxCulture = faxItem.PhoneCulture;
                        InstitutionalFaxNumber = faxItem.PhoneNumber;
                    }

                    MembershipSessionModel.MembershipModel.InstitutionalPhoneAreaCode = InstitutionalPhoneAreaCode;
                    MembershipSessionModel.MembershipModel.InstitutionalPhoneAreaCode2 = InstitutionalPhoneAreaCode2;
                    MembershipSessionModel.MembershipModel.InstitutionalPhoneCulture = InstitutionalPhoneCulture;
                    MembershipSessionModel.MembershipModel.InstitutionalPhoneCulture2 = InstitutionalPhoneCulture2;
                    MembershipSessionModel.MembershipModel.InstitutionalPhoneNumber = InstitutionalPhoneNumber;
                    MembershipSessionModel.MembershipModel.InstitutionalPhoneNumber2 = InstitutionalPhoneNumber2;
                    MembershipSessionModel.MembershipModel.InstitutionalGSMAreaCode = InstitutionalGSMAreaCode;
                    MembershipSessionModel.MembershipModel.InstitutionalGSMCulture = InstitutionalGSMCulture;
                    MembershipSessionModel.MembershipModel.InstitutionalGSMNumber = InstitutionalGSMNumber;
                    MembershipSessionModel.MembershipModel.InstitutionalFaxNumber = InstitutionalFaxNumber;
                    MembershipSessionModel.MembershipModel.InstitutionalFaxCulture = InstitutionalFaxCulture;
                    MembershipSessionModel.MembershipModel.InstitutionalFaxAreaCode = InstitutionalFaxAreaCode;
                }

                return View(MembershipSessionModel);
            }

        }

        [HttpPost]
        public ActionResult InstitutionalStep2(MembershipViewModel model, string InstitutionalPhoneNumber, string InstitutionalPhoneCulture, string InstitutionalPhoneAreaCode, string InstitutionalPhoneNumber2, string InstitutionalPhoneCulture2, string InstitutionalPhoneAreaCode2, string InstitutionalGSMNumber, string InstitutionalGSMCulture, string InstitutionalGSMAreaCode, string InstitutionalGSMNumber2, string InstitutionalGSMCulture2, string InstitutionalGSMAreaCode2, Nullable<byte> GsmType, string InstitutionalFaxAreaCode, string InstitutionalFaxCulture, string InstitutionalFaxNumber)
        {
            if (AuthenticationUser.Membership.MemberType == (byte)MemberType.Enterprise)
            {
                return RedirectToAction("Index", "Home");
            }
            else if (Session["MembershipSessionModel"] == null)
            {
                return RedirectToAction("InstitutionalStep", "MemberType");
            }
            else
            {
                MembershipSessionModel.MembershipModel.AddressTypeId = model.MembershipModel.AddressTypeId;

                MembershipSessionModel.MembershipModel.CountryId = model.MembershipModel.CountryId;
                MembershipSessionModel.MembershipModel.CityId = model.MembershipModel.CityId;
                MembershipSessionModel.MembershipModel.LocalityId = model.MembershipModel.LocalityId;
                MembershipSessionModel.MembershipModel.TownId = model.MembershipModel.TownId;

                MembershipSessionModel.MembershipModel.Avenue = model.MembershipModel.Avenue;
                MembershipSessionModel.MembershipModel.Street = model.MembershipModel.Street;
                MembershipSessionModel.MembershipModel.ApartmentNo = model.MembershipModel.ApartmentNo;
                MembershipSessionModel.MembershipModel.DoorNo = model.MembershipModel.DoorNo;

                MembershipSessionModel.MembershipModel.InstitutionalPhoneNumber = InstitutionalPhoneNumber;
                MembershipSessionModel.MembershipModel.InstitutionalPhoneCulture = InstitutionalPhoneCulture;
                MembershipSessionModel.MembershipModel.InstitutionalPhoneAreaCode = InstitutionalPhoneAreaCode;

                MembershipSessionModel.MembershipModel.InstitutionalPhoneNumber2 = InstitutionalPhoneNumber2;
                MembershipSessionModel.MembershipModel.InstitutionalPhoneCulture2 = InstitutionalPhoneCulture2;
                MembershipSessionModel.MembershipModel.InstitutionalPhoneAreaCode2 = InstitutionalPhoneAreaCode2;

                MembershipSessionModel.MembershipModel.InstitutionalGSMNumber = InstitutionalGSMNumber;
                MembershipSessionModel.MembershipModel.InstitutionalGSMCulture = InstitutionalGSMCulture;
                MembershipSessionModel.MembershipModel.InstitutionalGSMAreaCode = InstitutionalGSMAreaCode;

                MembershipSessionModel.MembershipModel.InstitutionalFaxAreaCode = InstitutionalFaxAreaCode;
                MembershipSessionModel.MembershipModel.InstitutionalFaxCulture = InstitutionalFaxCulture;
                MembershipSessionModel.MembershipModel.InstitutionalFaxNumber = InstitutionalFaxNumber;

                if (GsmType.HasValue)
                {
                    MembershipSessionModel.MembershipModel.GsmType = GsmType.Value;
                }


                return RedirectToAction("InstitutionalStep3", "MemberType");
            }
        }

        public ActionResult InstitutionalStep3()
        {
            if (SessionMembershipViewModel.MembershipViewModel.MembershipModel.MemberType == 0)
            {
                Session["TimeOut"] = true;
                return RedirectToAction("InstitutionalStep", "MemberType");
            }
            if (AuthenticationUser.Membership.MemberType == (byte)MemberType.Enterprise)
            {
                return RedirectToAction("Index", "Home");
            }
            else if (Session["MembershipSessionModel"] == null)
            {
                return RedirectToAction("InstitutionalStep", "MemberType");
            }
            else
            {
                ViewData["leftMenu"] = LeftMenuConstants.CreateLeftMenuModel(LeftMenuConstants.GroupName.MyAccount);
                var model = SessionMembershipViewModel.MembershipViewModel;
                model.ActivityItems = _activityTypeService.GetAllActivityTypes();
                if (string.IsNullOrWhiteSpace(model.MembershipModel.StoreWeb))
                {
                    model.MembershipModel.StoreWeb = "http://";
                }
                return View(model);
            }
        }

        [HttpPost]
        public ActionResult InstitutionalStep3(MembershipViewModel model)
        {
            if (SessionMembershipViewModel.MembershipViewModel.MembershipModel.MemberType==0)
            {
                Session["TimeOut"] = true;
                return RedirectToAction("InstitutionalStep", "MemberType");
            }
            if (AuthenticationUser.Membership.MemberType == (byte)MemberType.Enterprise)
            {
                return RedirectToAction("Index", "Home");
            }
            else if (Session["MembershipSessionModel"] == null)
            {
                return RedirectToAction("InstitutionalStep", "MemberType");
            }
            else
            {
                if (model.MembershipModel.StoreUrlName == null)
                {
                    ViewData["usernameNull"] = "true";
                    model.MembershipModel.StoreUrlName = UrlBuilder.ToUrl(model.MembershipModel.StoreShortName);
                }
                if (!sUserNameAllowedRegEx.IsMatch(model.MembershipModel.StoreUrlName))
                {
                    model.MembershipModel.StoreUrlName = UrlBuilder.ToUrl(model.MembershipModel.StoreShortName);


                }
                var store= _storeService.GetStoreByStoreUrlName(model.MembershipModel.StoreUrlName);
                if(store!=null)
                {
                    ViewData["storeUrlCheck"] = "false";
                    ViewData["leftMenu"] = LeftMenuConstants.CreateLeftMenuModel(LeftMenuConstants.GroupName.MyAccount);
                    var model1 = SessionMembershipViewModel.MembershipViewModel;
                    model1.ActivityItems = _activityTypeService.GetAllActivityTypes();
                    model1.MembershipModel = model.MembershipModel;
                    return View(model1);
                }

                else
                {
                    SessionMembershipViewModel.MembershipViewModel.MembershipModel.StoreName = model.MembershipModel.StoreName;
                    SessionMembershipViewModel.MembershipViewModel.MembershipModel.StoreWeb = model.MembershipModel.StoreWeb;
                    SessionMembershipViewModel.MembershipViewModel.MembershipModel.ActivityName = model.MembershipModel.ActivityName;
                    SessionMembershipViewModel.MembershipViewModel.MembershipModel.StoreCapital = model.MembershipModel.StoreCapital;
                    SessionMembershipViewModel.MembershipViewModel.MembershipModel.StoreUrlName = model.MembershipModel.StoreUrlName;
                    SessionMembershipViewModel.MembershipViewModel.MembershipModel.StoreShortName = model.MembershipModel.StoreShortName;
                    SessionMembershipViewModel.MembershipViewModel.MembershipModel.StoreEstablishmentDate = model.MembershipModel.StoreEstablishmentDate;
                    SessionMembershipViewModel.MembershipViewModel.MembershipModel.StoreEmployeesCount = model.MembershipModel.StoreEmployeesCount;
                    SessionMembershipViewModel.MembershipViewModel.MembershipModel.StoreEndorsement = model.MembershipModel.StoreEndorsement;
                    SessionMembershipViewModel.MembershipViewModel.MembershipModel.StoreAbout = model.MembershipModel.StoreAbout;
                    SessionMembershipViewModel.MembershipViewModel.MembershipModel.PurchasingDepartmentEmail = model.MembershipModel.PurchasingDepartmentEmail;
                    SessionMembershipViewModel.MembershipViewModel.MembershipModel.PurchasingDepartmentName = model.MembershipModel.PurchasingDepartmentName;
                    SessionMembershipViewModel.MembershipViewModel.MembershipModel.StoreType = model.MembershipModel.StoreType;
                    SessionMembershipViewModel.MembershipViewModel.MembershipModel.ReceiveEmail = false;
                    SessionMembershipViewModel.MembershipViewModel.MembershipModel.TaxNumber = model.MembershipModel.TaxNumber;
                    SessionMembershipViewModel.MembershipViewModel.MembershipModel.TaxOffice = model.MembershipModel.TaxOffice;
                    return RedirectToAction("InstitutionalStep4", "MemberType");
                }
            }
        }

        public ActionResult InstitutionalStep4()
        {
            if (SessionMembershipViewModel.MembershipViewModel.MembershipModel.MemberType == 0)
            {
                Session["TimeOut"] = true;
                return RedirectToAction("InstitutionalStep", "MemberType");
            }
            if (AuthenticationUser.Membership.MemberType == (byte)MemberType.Enterprise)
            {
                return RedirectToAction("Index", "Home");
            }
            else if (Session["MembershipSessionModel"] == null)
            {
                return RedirectToAction("InstitutionalStep", "MemberType");
            }
            else
            {
                IList<Constant> dataConstant = _constantService.GetAllConstants();

                if (SessionMembershipViewModel.MembershipViewModel.MembershipModel.StoreEndorsement > 0)
                {
                    SessionMembershipViewModel.MembershipViewModel.MembershipModel.StoreEndorsementName = dataConstant.FirstOrDefault(c => c.ConstantId == SessionMembershipViewModel.MembershipViewModel.MembershipModel.StoreEndorsement).ConstantName;
                }

                if (SessionMembershipViewModel.MembershipViewModel.MembershipModel.StoreCapital > 0)
                {
                    SessionMembershipViewModel.MembershipViewModel.MembershipModel.StoreCapitalName = dataConstant.FirstOrDefault(c => c.ConstantId == SessionMembershipViewModel.MembershipViewModel.MembershipModel.StoreCapital).ConstantName;
                }

                if (SessionMembershipViewModel.MembershipViewModel.MembershipModel.StoreEmployeesCount > 0)
                {
                    SessionMembershipViewModel.MembershipViewModel.MembershipModel.StoreEmployeesCountName = dataConstant.FirstOrDefault(c => c.ConstantId == SessionMembershipViewModel.MembershipViewModel.MembershipModel.StoreEmployeesCount).ConstantName;
                }

                ViewData["leftMenu"] = LeftMenuConstants.CreateLeftMenuModel(LeftMenuConstants.GroupName.MyAccount);
                MembershipViewModel model = SessionMembershipViewModel.MembershipViewModel;
                return View(model);
            }
        }

        [HttpPost]
        public ActionResult InstitutionalStep4(FormCollection frm,string gelenSayfa)
        {
            if (SessionMembershipViewModel.MembershipViewModel.MembershipModel.MemberType == 0)
            {
                Session["TimeOut"] = true;
                return RedirectToAction("InstitutionalStep", "MemberType");
            }
            if (AuthenticationUser.Membership.MemberType == (byte)MemberType.Enterprise)
            {
                return RedirectToAction("Index", "Home");
            }
            else if (Session["MembershipSessionModel"] == null)
            {
                return RedirectToAction("InstitutionalStep", "MemberType");
            }
            else
            {
                var model = SessionMembershipViewModel.MembershipViewModel;
                Store insertedStore = null;
                var storeMainParty = new MainParty
                {
                    Active = false,
                    MainPartyType = (byte)MainPartyType.Firm,
                    MainPartyRecordDate = DateTime.Now,
                    MainPartyFullName = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(model.MembershipModel.StoreName.ToLower()),
                };
                _memberService.InsertMainParty(storeMainParty);


                var member = _memberService.GetMemberByMainPartyId(AuthenticationUser.Membership.MainPartyId);
                member.MemberTitleType = model.MembershipModel.MemberType;
                member.MemberType =(byte)MemberType.Enterprise;
                member.FastMemberShipType = (byte)FastMembershipType.Normal;

                int memberMainPartyId = AuthenticationUser.Membership.MainPartyId;
                string memberNo = "##";
                for (int i = 0; i < 7 - memberMainPartyId.ToString().Length; i++)
                {
                    memberNo = memberNo + "0";
                }
                memberNo = memberNo + memberMainPartyId;
                member.MemberNo = memberNo;

                _memberService.UpdateMember(member);

                AuthenticationUser.Membership.MemberType = member.MemberType;
                AuthenticationUser.Membership.MemberTitleType = member.MemberTitleType;
                var storeMainPartyId = storeMainParty.MainPartyId;

                var packet = _packetService.GetPacketByIsStandart(true);

                var store = new Store
                {
                    MainPartyId = storeMainPartyId,
                    PacketId = packet.PacketId,
                    StoreName = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(model.MembershipModel.StoreName.ToLower()),
                    StoreEMail = member.MemberEmail,
                    StoreWeb = model.MembershipModel.StoreWeb,
                    StoreLogo = model.MembershipModel.StoreLogo,
                    StoreActiveType = (byte)PacketStatu.Inceleniyor,
                    StorePacketBeginDate = DateTime.Now,
                    StorePacketEndDate = DateTime.Now.AddDays(packet.PacketDay),
                    StoreAbout = model.MembershipModel.StoreAbout,
                    StoreRecordDate = DateTime.Now,
                    StoreEstablishmentDate = model.MembershipModel.StoreEstablishmentDate.HasValue ? model.MembershipModel.StoreEstablishmentDate.Value : 0,
                    StoreCapital = model.MembershipModel.StoreCapital,
                    StoreEmployeesCount = model.MembershipModel.StoreEmployeesCount,
                    StoreEndorsement = model.MembershipModel.StoreEndorsement,
                    StoreType = model.MembershipModel.StoreType,
                    TaxOffice=model.MembershipModel.TaxOffice,
                    TaxNumber=model.MembershipModel.TaxNumber,
                    StoreUrlName=model.MembershipModel.StoreUrlName,
                    StoreShortName = model.MembershipModel.StoreShortName,
                    PurchasingDepartmentEmail = model.MembershipModel.PurchasingDepartmentEmail,
                    PurchasingDepartmentName = model.MembershipModel.PurchasingDepartmentName,
                    FounderText = string.Empty,
                    GeneralText = string.Empty,
                    HistoryText = string.Empty,
                    PhilosophyText = string.Empty,
                    ViewCount = 0,
                    SingularViewCount = 0,
                    StoreShowcase = false,
                };

                string storeNo = "###";
                for (int i = 0; i < 6 - storeMainPartyId.ToString().Length; i++)
                {
                    storeNo = storeNo + "0";
                }
                storeNo = storeNo + storeMainPartyId;
                store.StoreNo = storeNo;


                _storeService.InsertStore(store);

                storeMainPartyId = store.MainPartyId;
                insertedStore = store;

                var address = _addressService.GetFisrtAddressByMainPartyId(AuthenticationUser.Membership.MainPartyId);
                if(address!=null)
                {
                    address.MainPartyId = storeMainPartyId;
                    _addressService.UpdateAddress(address);
                }


                //var phone = entities.Phones.Where(x => x.MainPartyId == AuthenticationUser.Membership.MainPartyId && x.PhoneType == (byte)PhoneType.Phone);
                //foreach (var phoneItem in phone.ToList())
                //{
                //    phoneItem.MainPartyId = storeMainPartyId;
                //    entities.SaveChanges();
                //}
                //var phoneGsm = entities.Phones.Where(x => x.MainPartyId == AuthenticationUser.Membership.MainPartyId && x.PhoneType == (Byte)PhoneType.Gsm).FirstOrDefault();
                //if(phoneGsm!=null)
                //{
                // phoneGsm.MainPartyId = storeMainPartyId;
                // entities.SaveChanges();
                //}
                //var phoneFax = entities.Phones.Where(x => x.MainPartyId == AuthenticationUser.Membership.MainPartyId && x.PhoneType == null).FirstOrDefault();
                //if(phoneFax!=null)
                //{
                //  phoneFax.MainPartyId = storeMainPartyId;
                //  entities.SaveChanges();
                //}

                var phones = _phoneService.GetPhonesByMainPartyId(AuthenticationUser.Membership.MainPartyId);
                foreach (var phoneItem in phones)
                {
                    phoneItem.MainPartyId = storeMainPartyId;
                    _phoneService.UpdatePhone(phoneItem);
                }

                if (model.MembershipModel.ActivityName != null)
                {
                    for (int i = 0; i < model.MembershipModel.ActivityName.Length; i++)
                    {
                        if (model.MembershipModel.ActivityName.GetValue(i).ToString() != "false")
                        {
                            var storeActivityType = new StoreActivityType
                            {
                                StoreId = storeMainPartyId,
                                ActivityTypeId = Convert.ToByte(model.MembershipModel.ActivityName.GetValue(i))
                            };
                            _storeActivityTypeService.InsertStoreActivityType(storeActivityType);
                        }
                    }
                }
                if (model.MembershipModel.StoreActivityCategory != null)
                {
                    var relCategory = model.MembershipModel.StoreActivityCategory.Where(c => c != "false").ToList();
                    for (int i = 0; i < relCategory.Count(); i++)
                    {
                        var storeActivityCategory = new StoreActivityCategory
                        {
                            MainPartyId = storeMainPartyId,
                            CategoryId = int.Parse(relCategory[i])
                        };
                        _storeActivityCategoryService.InsertStoreActivityCategory(storeActivityCategory);
                    }
                }
                var memberStore = new MemberStore
                {
                    MemberMainPartyId = AuthenticationUser.Membership.MainPartyId,
                    StoreMainPartyId = storeMainPartyId,
                    MemberStoreType = (byte)MemberStoreType.Owner

                };
                _memberStoreService.InsertMemberStore(memberStore);



                SessionMembershipViewModel.Flush();
                //firma logo düzenle
                if (!string.IsNullOrEmpty(insertedStore.StoreName))
                {
                    string storeLogoFolder = this.Server.MapPath(AppSettings.StoreLogoFolder);
                    string resizeStoreFolder = this.Server.MapPath(AppSettings.ResizeStoreLogoFolder);
                    string storeLogoThumbSize = AppSettings.StoreLogoThumbSizes;
                    List<string> thumbSizesForStoreLogo = new List<string>();
                    thumbSizesForStoreLogo.AddRange(storeLogoThumbSize.Split(';'));
                    var di = Directory.CreateDirectory(string.Format("{0}{1}", resizeStoreFolder, insertedStore.MainPartyId.ToString()));
                    di.CreateSubdirectory("thumbs");
                    string oldStoreLogoImageFilePath = string.Format("{0}{1}", storeLogoFolder, insertedStore.StoreLogo);
                    if (System.IO.File.Exists(oldStoreLogoImageFilePath))
                    {
                        // eski logoyu kopyala, varsa ustune yaz
                        string newStoreLogoImageFilePath = resizeStoreFolder + insertedStore.MainPartyId.ToString() + "\\";
                        string newStoreLogoImageFileName = insertedStore.StoreName.ToImageFileName() + "_logo.jpg";
                        System.IO.File.Copy(oldStoreLogoImageFilePath, newStoreLogoImageFilePath + newStoreLogoImageFileName, true);
                        bool thumbResult = ImageProcessHelper.ImageResize(newStoreLogoImageFilePath + newStoreLogoImageFileName,
                        newStoreLogoImageFilePath + "thumbs\\" + insertedStore.StoreName.ToImageFileName(), thumbSizesForStoreLogo);

                        insertedStore = _storeService.GetStoreByMainPartyId(insertedStore.MainPartyId);
                        insertedStore.StoreLogo = newStoreLogoImageFileName;
                        _storeService.UpdateStore(insertedStore);
                    }
                }

                #region bireyseldenkurumsalagecis

                //var settings = ConfigurationManager.AppSettings;
                MailMessage mail = new MailMessage();
                MessagesMT mailT = _messagesMTService.GetMessagesMTByMessageMTName("storedesc");
                mail.From = new MailAddress(mailT.Mail, mailT.MailSendFromName); //Mailin kimden gittiğini belirtiyoruz
                mail.To.Add(member.MemberEmail);                                                              //Mailin kime gideceğini belirtiyoruz
                mail.Subject = mailT.MessagesMTTitle;                                              //Mail konusu
                string template = mailT.MessagesMTPropertie;
                template = template.Replace("#kullaniciadi#", member.MemberName + " " + member.MemberSurname).Replace("#uyeeposta#", member.MemberEmail).Replace("#kullanicisifre#", member.MemberPassword).Replace("#firmaadi#",model.MembershipModel.StoreName);
                mail.Body = template;                                                            //Mailin içeriği
                mail.IsBodyHtml = true;
                mail.Priority = MailPriority.Normal;
                SmtpClient sc = new SmtpClient();                                                //sc adında SmtpClient nesnesi yaratıyoruz.
                sc.Port = 587;                                                                   //Gmail için geçerli Portu bildiriyoruz
                sc.Host = "smtp.gmail.com";                                                      //Gmailin smtp host adresini belirttik
                sc.EnableSsl = true;                                                             //SSL’i etkinleştirdik
                sc.Credentials = new NetworkCredential(mailT.Mail, mailT.MailPassword); //Gmail hesap kontrolü için bilgilerimizi girdi
                sc.Send(mail);

                #endregion


                #region bilgimakina

                MailMessage mailb = new MailMessage();
                MessagesMT mailTmpInf = _messagesMTService.GetMessagesMTByMessageMTName("bilgimakinasayfası");


                mailb.From = new MailAddress(mailTmpInf.Mail, mailTmpInf.MailSendFromName);
                mailb.To.Add("bilgi@makinaturkiye.com");
                mailb.Subject = "Firma Üyeliği " + member.MemberName + " " + member.MemberSurname;
                //var messagesmttemplate = entities.MessagesMTs.Where(c => c.MessagesMTId == 2).SingleOrDefault();
                //templatet = messagesmttemplate.MessagesMTPropertie;
                string bilgimakinaicin = mailTmpInf.MessagesMTPropertie;
                bilgimakinaicin = bilgimakinaicin.Replace("#kullanicimiz#", member.MemberName).Replace("#kullanicisoyadi#", member.MemberSurname).Replace("#kullanicitipi#", "Firma Üyelik");
                mailb.Body = bilgimakinaicin;
                mailb.IsBodyHtml = true;
                mailb.Priority = MailPriority.Normal;
                SmtpClient scr1 = new SmtpClient();
                scr1.Port = 587;
                scr1.Host = "smtp.gmail.com";
                scr1.EnableSsl = true;
                scr1.Credentials = new NetworkCredential(mailTmpInf.Mail, mailTmpInf.MailPassword);
                scr1.Send(mailb);
                #endregion
                return RedirectToAction("Index", "Home", new {gelenSayfa="KurumsalOnay"});
            }
        }

        [HttpPost]
        public ActionResult InstitutionalStep5(FormCollection coll)
        {
            if (AuthenticationUser.Membership.MemberType == (byte)MemberType.Enterprise)
            {
                return RedirectToAction("Index", "Home");
                // üyelik tipi firma üyeliğine dönüşütürüldü ve anasayfaya yönlendirmeyapıldı

            }
            else if (Session["MembershipSessionModel"] == null)
            {
                return RedirectToAction("InstitutionalStep", "MemberType");
            }
            else
            {
                var model = MembershipSessionModel;
                bool hasRecord = false;
                Store insertedStore = null;
                try
                {

                    var member = _memberService.GetMemberByMainPartyId(AuthenticationUser.Membership.MainPartyId);
                    member.MemberType = (byte)MemberType.Enterprise;

                    string memberNo = "##";
                    for (int i = 0; i < 7 - member.MainPartyId.ToString().Length; i++)
                    {
                        memberNo = memberNo + "0";
                    }
                    memberNo = memberNo + member.MainPartyId;
                    member.MemberNo = memberNo;
                    _memberService.UpdateMember(member);

                    var curStoreMainParty = new MainParty
                    {
                        Active = false,
                        MainPartyType = (byte)MainPartyType.Firm,
                        MainPartyRecordDate = DateTime.Now,
                        MainPartyFullName = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(model.MembershipModel.StoreName.ToLower()),
                    };
                    _memberService.InsertMainParty(curStoreMainParty);

                    var storeMainPartyId = curStoreMainParty.MainPartyId;

                    var packet = _packetService.GetPacketByIsStandart(true);

                    var curStore = new Store
                    {
                        StoreAbout = model.MembershipModel.StoreAbout,
                        MainPartyId = storeMainPartyId,
                        PacketId = packet.PacketId,
                        StoreActiveType = (byte)PacketStatu.Inceleniyor,
                        StoreCapital = model.MembershipModel.StoreCapital,
                        StoreEMail = member.MemberEmail,
                        StoreEmployeesCount = model.MembershipModel.StoreEmployeesCount,
                        StoreEndorsement = model.MembershipModel.StoreEndorsement,
                        StoreEstablishmentDate = model.MembershipModel.StoreEstablishmentDate,
                        StoreLogo = model.MembershipModel.StoreLogo,
                        StorePacketBeginDate = DateTime.Now,
                        StorePacketEndDate = DateTime.Now.AddDays(packet.PacketDay),
                        StoreName = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(model.MembershipModel.StoreName.ToLower()),
                        StoreType = model.MembershipModel.StoreType,
                        StoreWeb = model.MembershipModel.StoreWeb,
                        StoreRecordDate = DateTime.Now,
                        TaxNumber = model.MembershipModel.TaxNumber,
                        TaxOffice = model.MembershipModel.TaxOffice,
                        PurchasingDepartmentEmail = model.MembershipModel.PurchasingDepartmentEmail,
                        PurchasingDepartmentName = model.MembershipModel.PurchasingDepartmentName,
                    };

                    string storeNo = "###";
                    for (int i = 0; i < 6 - storeMainPartyId.ToString().Length; i++)
                    {
                        storeNo = storeNo + "0";
                    }
                    storeNo = storeNo + storeMainPartyId;
                    curStore.StoreNo = storeNo;

                    _storeService.InsertStore(curStore);

                    insertedStore = curStore;

                    var address = _addressService.GetFisrtAddressByMainPartyId(AuthenticationUser.Membership.MainPartyId);
                    if (address != null)
                    {
                        address.MainPartyId = storeMainPartyId;
                        _addressService.UpdateAddress(address);
                    }



                    var phone = _phoneService.GetPhonesByMainPartyId(AuthenticationUser.Membership.MainPartyId);
                    foreach (var item in phone.ToList())
                    {
                        item.MainPartyId = storeMainPartyId;
                        _phoneService.UpdatePhone(item);

                    }


                    if (model.MembershipModel.ActivityName != null)
                    {
                        for (int i = 0; i < model.MembershipModel.ActivityName.Length; i++)
                        {
                            if (model.MembershipModel.ActivityName.GetValue(i).ToString() != "false")
                            {
                                var storeActivityType = new StoreActivityType
                                {
                                    StoreId = storeMainPartyId,
                                    ActivityTypeId = Convert.ToByte(model.MembershipModel.ActivityName.GetValue(i))
                                };
                                _storeActivityTypeService.InsertStoreActivityType(storeActivityType);
                            }
                        }
                    }

                    if (model.MembershipModel.StoreActivityCategory != null)
                    {
                        var relCategory = model.MembershipModel.StoreActivityCategory.Where(c => c != "false").ToList();
                        for (int i = 0; i < relCategory.Count(); i++)
                        {
                            var storeActivityCategory = new StoreActivityCategory
                            {
                                MainPartyId = storeMainPartyId,
                                CategoryId = int.Parse(relCategory[i])
                            };
                            _storeActivityCategoryService.InsertStoreActivityCategory(storeActivityCategory);
                        }
                    }

                    var curMemberStore = new MemberStore
                    {
                        MemberMainPartyId = AuthenticationUser.Membership.MainPartyId,
                        StoreMainPartyId = storeMainPartyId
                    };
                    _memberStoreService.InsertMemberStore(curMemberStore);

                    UserLog lg = new UserLog
                    {
                        LogName = "B.K",
                        LogDescription = member.MemberNo,
                        LogStatus = 1,//success
                        LogType = (byte)LogType.MemberShip,
                        CreatedDate = DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss")
                    };
                   // _userLogService.InsertUserLog(lg);

                    if (!string.IsNullOrEmpty(insertedStore.StoreName))
                    {
                        string storeLogoFolder = this.Server.MapPath(AppSettings.StoreLogoFolder);
                        string resizeStoreFolder = this.Server.MapPath(AppSettings.ResizeStoreLogoFolder);
                        string storeLogoThumbSize = AppSettings.StoreLogoThumbSizes;
                        List<string> thumbSizesForStoreLogo = new List<string>();
                        thumbSizesForStoreLogo.AddRange(storeLogoThumbSize.Split(';'));
                        var di = Directory.CreateDirectory(string.Format("{0}{1}", resizeStoreFolder, insertedStore.MainPartyId.ToString()));
                        di.CreateSubdirectory("thumbs");
                        string oldStoreLogoImageFilePath = string.Format("{0}{1}", storeLogoFolder, insertedStore.StoreLogo);
                        if (System.IO.File.Exists(oldStoreLogoImageFilePath))
                        {
                            // eski logoyu kopyala, varsa ustune yaz
                            string newStoreLogoImageFilePath = resizeStoreFolder + insertedStore.MainPartyId.ToString() + "\\";
                            string newStoreLogoImageFileName = insertedStore.StoreName.ToImageFileName() + "_logo.jpg";
                            System.IO.File.Copy(oldStoreLogoImageFilePath, newStoreLogoImageFilePath + newStoreLogoImageFileName, true);
                            bool thumbResult = ImageProcessHelper.ImageResize(newStoreLogoImageFilePath + newStoreLogoImageFileName,
                            newStoreLogoImageFilePath + "thumbs\\" + insertedStore.StoreName.ToImageFileName(), thumbSizesForStoreLogo);

                            insertedStore = _storeService.GetStoreByMainPartyId(insertedStore.MainPartyId);
                            insertedStore.StoreLogo = newStoreLogoImageFileName;

                            _storeService.UpdateStore(insertedStore);
                        }
                    }

                    #region bireyseldenkurumsalagecis
                    //var settings = ConfigurationManager.AppSettings;
                    MailMessage mail = new MailMessage();
                    MessagesMT mailT = _messagesMTService.GetMessagesMTByMessageMTName("storedesc");
                    mail.From = new MailAddress(mailT.Mail, mailT.MailSendFromName); //Mailin kimden gittiğini belirtiyoruz
                    mail.To.Add(member.MemberEmail);                                                              //Mailin kime gideceğini belirtiyoruz
                    mail.Subject = mailT.MessagesMTTitle;                                              //Mail konusu
                    string template = mailT.MessagesMTPropertie;
                    template = template.Replace("#kullaniciadi#", member.MemberName + " " + member.MemberSurname).Replace("#uyeeposta#", member.MemberEmail).Replace("#kullanicisifre#", member.MemberPassword).Replace("#firmaadi#", model.MembershipModel.StoreName);
                    mail.Body = template;                                                            //Mailin içeriği
                    mail.IsBodyHtml = true;
                    mail.Priority = MailPriority.Normal;
                    SmtpClient sc = new SmtpClient();                                                //sc adında SmtpClient nesnesi yaratıyoruz.
                    sc.Port = 587;                                                                   //Gmail için geçerli Portu bildiriyoruz
                    sc.Host = "smtp.gmail.com";                                                      //Gmailin smtp host adresini belirttik
                    sc.EnableSsl = true;                                                             //SSL’i etkinleştirdik
                    sc.Credentials = new NetworkCredential(mailT.Mail, mailT.MailPassword); //Gmail hesap kontrolü için bilgilerimizi girdi
                    sc.Send(mail);
                    #endregion


                    #region bilgimakina
                    MailMessage mailb = new MailMessage();
                    MessagesMT mailTmpInf = _messagesMTService.GetMessagesMTByMessageMTName("bilgimakinasayfası");


                    mailb.From = new MailAddress(mailTmpInf.Mail, mailTmpInf.MailSendFromName);
                    mailb.To.Add("bilgi@makinaturkiye.com");
                    mailb.Subject = "Firma Üyeliği " + member.MemberName + " " + member.MemberSurname;
                    //var messagesmttemplate = entities.MessagesMTs.Where(c => c.MessagesMTId == 2).SingleOrDefault();
                    //templatet = messagesmttemplate.MessagesMTPropertie;
                    string bilgimakinaicin = mailTmpInf.MessagesMTPropertie;
                    bilgimakinaicin = bilgimakinaicin.Replace("#kullanicimiz#", member.MemberName).Replace("#kullanicisoyadi#", member.MemberSurname).Replace("#kullanicitipi#", "Hızlı Üyelik");
                    mailb.Body = bilgimakinaicin;
                    mailb.IsBodyHtml = true;
                    mailb.Priority = MailPriority.Normal;
                    SmtpClient scr1 = new SmtpClient();
                    scr1.Port = 587;
                    scr1.Host = "smtp.gmail.com";
                    scr1.EnableSsl = true;
                    scr1.Credentials = new NetworkCredential(mailTmpInf.Mail, mailTmpInf.MailPassword);
                    scr1.Send(mailb);

                    #endregion



                    hasRecord = true;
                    AuthenticationUser.Membership.MemberType = member.MemberType.Value;

                }
                catch (Exception ex)
                {
                    var lg = new UserLog
                    {
                        LogDescription = ex.ToString(),
                        LogShortDescription = ex.Message,
                        LogName = "B.K",
                        LogType = (byte)LogType.MemberShip,
                        LogStatus = 0,
                        CreatedDate = DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss")
                    };

                   // _userLogService.InsertUserLog(lg);
                }

                if (hasRecord)
                    return RedirectToAction("Completed", "MemberType");
                else
                    return RedirectToAction("Error", "MemberType");
            }

        }

        //----------------------------------------------------------------------------BİREYSEL ÜYELİK----------------------------------------------------------------------------\\

        public ActionResult Individual(string gelenSayfa, string sonuc,string memberType,string error, string type,string urunNo, string uyeNo, string mtypePage)
        {
            if (AuthenticationUser.Membership.MemberType!=(byte)MemberType.FastIndividual)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ViewData["mtypePage"] = mtypePage;
                ViewData["uyeNo"] = uyeNo;
                ViewData["urunNo"] = urunNo;
                ViewData["type"] = type;
                ViewData["memberType"] = memberType;

                var membershipViewModel = new MembershipViewModel();

                //membershipViewModel.CountryItems = new SelectList(entities.Countries.AsEnumerable(), "CountryId", "CountryName", 0);

                //var cityItems = entities.Cities.Where(c => c.CountryId == AppSettings.Turkiye).ToList();
                //cityItems.Insert(0, new City { CityId = 0, CityName = "< Lütfen Seçiniz >" });

                //membershipViewModel.CityItems = new SelectList(cityItems, "CityId", "CityName", 0);

                MembershipModel membershipModel;
                if (Session["MembershipModel"] != null)
                {
                    membershipModel = (MembershipModel)Session["MembershipModel"];
                    Session["MembershipModel"] = null;
                }
                else
                {
                    membershipModel = new MembershipModel();
                }

                membershipModel.CountryId = AppSettings.Turkiye;
                membershipModel.CityId = 0;

                membershipViewModel.MembershipModel = membershipModel;
                ViewData["leftMenu"] = LeftMenuConstants.CreateLeftMenuModel(LeftMenuConstants.GroupName.MyAccount);

                var phone = _phoneService.GetPhonesByMainPartyIdByPhoneType(AuthenticationUser.Membership.MainPartyId, PhoneTypeEnum.Gsm);
                if (phone!=null)
                {
                    membershipViewModel.MembershipModel.InstitutionalGSMCulture = phone.PhoneCulture;
                    membershipViewModel.MembershipModel.InstitutionalGSMAreaCode = phone.PhoneAreaCode;
                    membershipViewModel.MembershipModel.InstitutionalGSMNumber = phone.PhoneNumber;
                    membershipViewModel.Phone = phone;
                }
                membershipViewModel.MembershipModel.MemberPassword = _memberService.GetMemberByMainPartyId(AuthenticationUser.Membership.MainPartyId).MemberPassword;
            return View(membershipViewModel);
            }

        }


        [HttpPost]
        public ActionResult Individual(MembershipViewModel model,string type,string memberType, string TextInstitutionalPhoneAreaCode, string TextInstitutionalPhoneAreaCode2, string TextInstitutionalFaxAreaCode, Nullable<byte> GsmType, string DropDownInstitutionalPhoneAreaCode, string DropDownInstitutionalPhoneAreaCode2, string DropDownInstitutionalFaxAreaCode, string sonuc, string error, string urunNo, string uyeNo, string mtypePage)
        {
            if(!string.IsNullOrEmpty(uyeNo))
            {
                ViewData["mtypePage"] = mtypePage;
                ViewData["uyeNo"] = uyeNo;
                ViewData["urunNo"] = urunNo;

            }


            if (model.MembershipModel.Year > 0 && model.MembershipModel.Month > 0 && model.MembershipModel.Day > 0)
            {
                DateTime birthDate = new DateTime(model.MembershipModel.Year, model.MembershipModel.Month, model.MembershipModel.Day);
                model.MembershipModel.BirthDate = birthDate;

            }

            bool hasRecord = false;
            bool changeGsm = false;
            bool phoneactive = false;
            try
            {
                int memberMainPartyId = AuthenticationUser.CurrentUser.Membership.MainPartyId;
                var member = _memberService.GetMemberByMainPartyId(memberMainPartyId);

                member.MemberType = (byte)MemberType.Individual;
                if (model.MembershipModel.MemberPassword != null)
                {
                    member.MemberPassword = model.MembershipModel.MemberPassword;
                }
                member.BirthDate = model.MembershipModel.BirthDate;
                _memberService.UpdateMember(member);

                var phoneItems = _phoneService.GetPhonesByMainPartyId(memberMainPartyId);

                string phoneGsmLast = "";

                if (phoneItems != null)
                {
                    var phoneGsm = phoneItems.Where(x => x.PhoneType == (byte)PhoneType.Gsm).FirstOrDefault();
                    if (phoneGsm != null)
                    {
                        phoneGsmLast = phoneGsm.PhoneCulture + phoneGsm.PhoneAreaCode + phoneGsm.PhoneNumber;//
                        if (phoneGsm.active == 1)
                            phoneactive = true;
                    }
                    string phoneGsmModel = model.MembershipModel.InstitutionalGSMCulture + model.MembershipModel.InstitutionalGSMAreaCode + model.MembershipModel.InstitutionalGSMNumber;
                    if (phoneGsmLast != phoneGsmModel || phoneactive==false)
                        changeGsm = true;
                    else
                        phoneItems = phoneItems.Where(x => x.PhoneType != (byte)PhoneType.Gsm).ToList();


                    foreach (var item in phoneItems)
                    {
                        _phoneService.DeletePhone(item);
                    }
                }
                var addresses = _addressService.GetAddressesByMainPartyId(memberMainPartyId);
                if (addresses.Count > 0)
                {
                    foreach (var item in addresses)
                    {
                        _addressService.DeleteAddress(item);
                    }
                }

                var curAddress = new Address
                {
                    MainPartyId = AuthenticationUser.Membership.MainPartyId,
                    Avenue = model.MembershipModel.Avenue,
                    Street = model.MembershipModel.Street,
                    DoorNo = model.MembershipModel.DoorNo,
                    ApartmentNo = model.MembershipModel.ApartmentNo,
                    AddressDefault = true,
                    PostCode = model.MembershipModel.PostCode
                };

                if (model.MembershipModel.CountryId > 0 && model.MembershipModel.CountryId != 246)
                {
                    curAddress.Avenue = model.MembershipModel.AvenueOtherCountries;
                }


                if (model.MembershipModel.CountryId > 0)
                    curAddress.CountryId = model.MembershipModel.CountryId;
                else
                    curAddress.CountryId = null;
                if (type == "fast")
                {
                    if (model.MembershipModel.AddressTypeId > 0)
                        curAddress.AddressTypeId = model.MembershipModel.AddressTypeId;
                    else
                        curAddress.AddressTypeId = null;
                }
                else
                    curAddress.AddressTypeId = 15;
                if (model.MembershipModel.CityId > 0)
                    curAddress.CityId = model.MembershipModel.CityId;
                else
                    curAddress.CityId = null;

                if (model.MembershipModel.LocalityId > 0)
                    curAddress.LocalityId = model.MembershipModel.LocalityId;
                else
                    curAddress.LocalityId = null;

                if (model.MembershipModel.TownId > 0)
                    curAddress.TownId = model.MembershipModel.TownId;
                else
                    curAddress.TownId = null;

                _addressService.InsertAdress(curAddress);

                if (!string.IsNullOrWhiteSpace(TextInstitutionalPhoneAreaCode) || !string.IsNullOrWhiteSpace(DropDownInstitutionalPhoneAreaCode))
                {
                    if (!string.IsNullOrWhiteSpace(model.MembershipModel.InstitutionalPhoneNumber))
                    {
                        var curPhone1 = new Phone
                        {
                            MainPartyId = AuthenticationUser.Membership.MainPartyId,
                            PhoneCulture = model.MembershipModel.InstitutionalPhoneCulture,
                            PhoneNumber = model.MembershipModel.InstitutionalPhoneNumber,
                            PhoneType = (byte)PhoneType.Phone,
                            GsmType = null
                        };

                        if (model.MembershipModel.CityId == 34)
                        {
                            curPhone1.PhoneAreaCode = DropDownInstitutionalPhoneAreaCode;
                        }
                        else
                        {
                            curPhone1.PhoneAreaCode = TextInstitutionalPhoneAreaCode;
                        }

                        _phoneService.InsertPhone(curPhone1);
                    }
                }

                if (!string.IsNullOrWhiteSpace(TextInstitutionalPhoneAreaCode2) || !string.IsNullOrWhiteSpace(DropDownInstitutionalPhoneAreaCode2))
                {
                    if (!string.IsNullOrWhiteSpace(model.MembershipModel.InstitutionalPhoneNumber2))
                    {
                        var curPhone2 = new Phone
                        {
                            MainPartyId = AuthenticationUser.Membership.MainPartyId,
                            PhoneCulture = model.MembershipModel.InstitutionalPhoneCulture2,
                            PhoneNumber = model.MembershipModel.InstitutionalPhoneNumber2,
                            PhoneType = (byte)PhoneType.Phone,
                            GsmType = null
                        };

                        if (model.MembershipModel.CityId == 34)
                        {
                            curPhone2.PhoneAreaCode = DropDownInstitutionalPhoneAreaCode2;
                        }
                        else
                        {
                            curPhone2.PhoneAreaCode = TextInstitutionalPhoneAreaCode2;
                        }

                        _phoneService.InsertPhone(curPhone2);
                    }
                }

                if (!string.IsNullOrWhiteSpace(model.MembershipModel.InstitutionalGSMNumber))
                {
                    if (changeGsm == true)
                    {
                        SmsHelper sms = new SmsHelper();
                        string activeCode = sms.CreateActiveCode();

                        //numaraya göre yap
                        var curPhoneGsm = new Phone
                        {
                            MainPartyId = AuthenticationUser.Membership.MainPartyId,
                            PhoneAreaCode = model.MembershipModel.InstitutionalGSMAreaCode,
                            PhoneCulture = model.MembershipModel.InstitutionalGSMCulture,
                            PhoneNumber = model.MembershipModel.InstitutionalGSMNumber,
                            PhoneType = (byte)PhoneType.Gsm,
                            ActivationCode = activeCode,
                            active = 0,
                            GsmType = (byte)PhoneType.Gsm
                        };
                        string phoneNumber = curPhoneGsm.PhoneCulture + curPhoneGsm.PhoneAreaCode + curPhoneGsm.PhoneNumber;
                        MobileMessage messageTmp = _mobileMessageService.GetMobileMessageByMessageName("telefononayi");
                        string messageMobile = messageTmp.MessageContent.Replace("#isimsoyisim#", member.MemberName + " " + member.MemberSurname).Replace("#aktivasyonkodu#", activeCode);
                        sms.SendPhoneMessage(phoneNumber, messageMobile);

                        _phoneService.InsertPhone(curPhoneGsm);

                        ViewData["phoneNumber"] = phoneNumber;
                    }
                    else
                    {
                        if (phoneactive == false)//phone değişmemiş ama numarası false ise
                        {
                            SmsHelper sms = new SmsHelper();
                            string activeCode = sms.CreateActiveCode();

                            //numaraya göre yap
                            var curPhoneGsm = _phoneService.GetPhonesByMainPartyIdByPhoneType(memberMainPartyId, PhoneTypeEnum.Gsm);

                            curPhoneGsm.MainPartyId = AuthenticationUser.Membership.MainPartyId;
                            curPhoneGsm.PhoneAreaCode = model.MembershipModel.InstitutionalGSMAreaCode;
                            curPhoneGsm.PhoneCulture = model.MembershipModel.InstitutionalGSMCulture;
                            curPhoneGsm.PhoneNumber = model.MembershipModel.InstitutionalGSMNumber;
                            curPhoneGsm.PhoneType = (byte)PhoneType.Gsm;
                            curPhoneGsm.ActivationCode = activeCode;
                            curPhoneGsm.active = 0;
                            GsmType = (byte)PhoneType.Gsm;
                            string phoneNumber = curPhoneGsm.PhoneCulture + curPhoneGsm.PhoneAreaCode + curPhoneGsm.PhoneNumber;
                            MobileMessage messageTmp = _mobileMessageService.GetMobileMessageByMessageName("telefononayi");
                            string messageMobile = messageTmp.MessageContent.Replace("#isimsoyisim#", member.MemberName + " " + member.MemberSurname).Replace("#aktivasyonkodu#", activeCode);
                            sms.SendPhoneMessage(phoneNumber, messageMobile);

                            _phoneService.InsertPhone(curPhoneGsm);

                            ViewData["phoneNumber"] = phoneNumber;
                        }

                    }
                }
                if (model.MembershipModel.IsGsmWhatsapp)
                {
                    if (!string.IsNullOrWhiteSpace(model.MembershipModel.InstitutionalGSMNumber))
                    {
                        var curPhoneGsmForWp = new Phone
                        {
                            MainPartyId = memberMainPartyId,
                            PhoneAreaCode = model.MembershipModel.InstitutionalGSMAreaCode,
                            PhoneCulture = model.MembershipModel.InstitutionalGSMCulture,
                            PhoneNumber = model.MembershipModel.InstitutionalGSMNumber,
                            PhoneType = (byte)PhoneType.Whatsapp

                        };
                        _phoneService.InsertPhone(curPhoneGsmForWp);

                    }
                }
                else
                {
                    if (!string.IsNullOrWhiteSpace(model.MembershipModel.GsmWhatsappNumber))
                    {
                        var curPhoneGsmForWp = new Phone
                        {
                            MainPartyId = AuthenticationUser.Membership.MainPartyId,
                            PhoneAreaCode = model.MembershipModel.GsmWhatsappAreaCode,
                            PhoneCulture = model.MembershipModel.GsmWhatsappCulture,
                            PhoneNumber = model.MembershipModel.GsmWhatsappNumber,
                            PhoneType = (byte)PhoneType.Whatsapp
                        };
                        _phoneService.InsertPhone(curPhoneGsmForWp);

                    }
                }

                if (!string.IsNullOrWhiteSpace(TextInstitutionalFaxAreaCode) || !string.IsNullOrWhiteSpace(DropDownInstitutionalFaxAreaCode))
                {

                    //numaraya göre
                    if (!string.IsNullOrWhiteSpace(model.MembershipModel.InstitutionalFaxNumber))
                    {
                        var curPhoneFax = new Phone
                        {
                            MainPartyId = memberMainPartyId,
                            PhoneCulture = model.MembershipModel.InstitutionalFaxCulture,
                            PhoneNumber = model.MembershipModel.InstitutionalFaxNumber,
                            PhoneType = (byte)PhoneType.Fax,
                            GsmType = null
                        };

                        if (model.MembershipModel.CityId == 34)
                        {
                            curPhoneFax.PhoneAreaCode = DropDownInstitutionalFaxAreaCode;
                        }
                        else
                        {
                            curPhoneFax.PhoneAreaCode = TextInstitutionalPhoneAreaCode;
                        }

                        _phoneService.InsertPhone(curPhoneFax);
                    }
                }
                //#region bilgimakina
                //MailMessage mailb = new MailMessage();
                //MessagesMT mailTmpInf = entities.MessagesMTs.First(x => x.MessagesMTName == "bilgimakinasayfası");


                //mailb.From = new MailAddress(mailTmpInf.Mail, mailTmpInf.MailSendFromName);
                //mailb.To.Add("bilgi@makinaturkiye.com");
                //mailb.Subject = "Bireysel Üyelik " + member.MemberName + " " + member.MemberSurname;
                ////var messagesmttemplate = entities.MessagesMTs.Where(c => c.MessagesMTId == 2).SingleOrDefault();
                ////templatet = messagesmttemplate.MessagesMTPropertie;
                //string bilgimakinaicin = mailTmpInf.MessagesMTPropertie;
                //bilgimakinaicin = bilgimakinaicin.Replace("#kullanicimiz#", member.MemberName).Replace("#kullanicisoyadi#", member.MemberSurname).Replace("#kullanicitipi#", "Hızlı Üyelik");
                //mailb.Body = bilgimakinaicin;
                //mailb.IsBodyHtml = true;
                //mailb.Priority = MailPriority.Normal;
                //SmtpClient scr1 = new SmtpClient();
                //scr1.Port = 587;
                //scr1.Host = "smtp.gmail.com";
                //scr1.EnableSsl = true;
                //scr1.Credentials = new NetworkCredential(mailTmpInf.Mail, mailTmpInf.MailPassword);
                //scr1.Send(mailb);
                //#endregion

                #region emailiburayayazıcam

                try
                {
                    MessagesMT mailTemplate = _messagesMTService.GetMessagesMTByMessageMTName("bireyseluyelik");
                    MailMessage mail = new MailMessage();
                    mail.From = new MailAddress(mailTemplate.Mail, mailTemplate.MailSendFromName);
                    mail.To.Add(member.MemberEmail);
                    mail.Subject = mailTemplate.MessagesMTTitle;
                    //var messagesmttemplate = entities.MessagesMTs.Where(c => c.MessagesMTId == 3).SingleOrDefault();
                    //templatet = messagesmttemplate.MessagesMTPropertie;
                    string template = mailTemplate.MessagesMTPropertie;
                    template = template.Replace("#kullaniciadi#", member.MemberName + " " + member.MemberSurname).Replace("#kullanicieposta#", member.MemberEmail).Replace("#kullanicisifre#", member.MemberPassword);
                    mail.Body = template;                                                            //Mailin içeriği
                    mail.IsBodyHtml = true;
                    mail.Priority = MailPriority.Normal;
                    SmtpClient sc = new SmtpClient();                                                //sc adında SmtpClient nesnesi yaratıyoruz.
                    sc.Port = 587;                                                                   //Gmail için geçerli Portu bildiriyoruz
                    sc.Host = "smtp.gmail.com";                                                      //Gmailin smtp host adresini belirttik
                    sc.EnableSsl = true;                                                             //SSL’i etkinleştirdik
                    sc.Credentials = new NetworkCredential(mailTemplate.Mail, mailTemplate.MailPassword); //Gmail hesap kontrolü için bilgilerimizi girdi
                    sc.Send(mail);
                }
                catch { }
                #endregion

                UserLog l = new UserLog();
                if (member.FastMemberShipType == (byte)FastMembershipType.Facebook)
                {
                    l.LogName = "FHÜ.B";
                    l.LogShortDescription = "Facebook'tan bireysele";
                }
                else if (member.FastMemberShipType == (byte)FastMembershipType.Phone)
                {
                    l.LogName = "MHÜ.B";
                    l.LogShortDescription = "Mesajdan bireysele";
                }
                else
                {
                    l.LogName = "NHÜ.B";
                    l.LogShortDescription = "Normaldenden bireysele";
                }
                l.LogDescription = member.MemberNo;
                l.LogType = (byte)LogType.MemberShip;
                l.LogStatus = 1;
                l.CreatedDate = DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss");

                //_userLogService.InsertUserLog(l);

                //member.FastMemberShipType = (byte)FastMembershipType.Normal;

                //_memberService.UpdateMember(member);

                AuthenticationUser.Membership.MemberType = member.MemberType.Value;
                Session["MembershipModel"] = null;
                hasRecord = true;
            }
            catch (Exception ex)
            {
                //var lg = new UserLog
                //{
                //    LogDescription = ex.ToString(),
                //    LogShortDescription = ex.Message,
                //    LogName = "H.B",
                //    LogType = 1,
                //    CreatedDate = DateTime.Now.ToString()
                //};
                //_userLogService.InsertUserLog(lg);

            }
            if (hasRecord)
            {
                ViewData["type"] = type;
                if (changeGsm || phoneactive==false)
                    return RedirectToAction("PhoneActive", new {type=type, uyeNo=uyeNo, urunNo=urunNo, mtypePage=mtypePage, phoneNumber=ViewData["phoneNumber"] });
                else
                  {
                      if (urunNo == "")
                      {
                          if(memberType!="")
                              return RedirectToAction("index", "Home", new { gelenSayfa = "bireyselUyelikOnay",memberType="hizli"});//telefon onayı varsa mesajdan gelmiyorsa
                          else
                          return RedirectToAction("index", "Home", new { gelenSayfa = "bireyselUyelikOnay" });//telefon onayı varsa mesajdan gelmiyorsa

                      }
                      else
                          return RedirectToAction("index", "Message", new { MessagePageType = mtypePage, UyeNo = uyeNo, UrunNo = urunNo });//eğer mesajdan geliyor ve telefon onayı varsa
                }
            }
            else
              return RedirectToAction("Error", "MemberType");
        }
        public ActionResult PhoneActive(string type, string urunNo, string uyeNo, string mtypePage, string phoneNumber)
        {
            if (!string.IsNullOrEmpty(uyeNo))
            {
                ViewData["mtypePage"] = mtypePage;
                ViewData["uyeNo"] = uyeNo;
                ViewData["urunNo"] = urunNo;

            }
            ViewData["PhoneNumber"] = phoneNumber;
            ViewData["type"] = type;

            return View();
        }
        [HttpPost]
        public ActionResult PhoneActive(string activationCode,string type, string sonuc, string error, string urunNo, string uyeNo, string mtypePage)
        {
           if(String.IsNullOrEmpty(activationCode) || String.IsNullOrWhiteSpace(activationCode))
           {
               ViewData["error"] = "true";
               return View();
           }
           else
           {
                var phone = _phoneService.GetPhonesByMainPartyIdByPhoneType(AuthenticationUser.Membership.MainPartyId, PhoneTypeEnum.Gsm);
               if(phone!=null)
               {

                   if (phone.ActivationCode == activationCode)
                   {
                       phone.active = 1;
                        _phoneService.UpdatePhone(phone);

                       if(string.IsNullOrEmpty(uyeNo))
                       {
                           if (type == "fast")
                           {
                               return RedirectToAction("InstitutionalStep1", "MemberType");
                           }
                           else
                               return RedirectToAction("index", "Home", new { gelenSayfa = "bireyselUyelikOnay" });

                       }
                       else
                       {

                           string memberNo = uyeNo;
                           string productNo = urunNo;
                           return RedirectToAction("index", "Message", new { MessagePageType = mtypePage, UyeNo = memberNo, UrunNo = productNo });

                       }
                   }
                   else
                   {
                       ViewData["phoneNumber"] = phone.PhoneCulture + phone.PhoneAreaCode + phone.PhoneNumber;
                       ViewData["error"] = "true";
                       return View();
                   }
               }
               else
               {
                   ViewData["phoneNumber"] = phone.PhoneCulture + phone.PhoneAreaCode + phone.PhoneNumber;
                   ViewData["error"] = "true";
                   return View();
               }

           }

        }
        public ActionResult PhoneUpate()
        {
            return View();
        }
        public string SendPhoneActiveSms(string gsmNumber, string activeCode, string nameSurname)
        {
            string kNumber = ConfigurationManager.AppSettings["smsKno"];
            string password = ConfigurationManager.AppSettings["smsSifre"];
            string uName = ConfigurationManager.AppSettings["smsKadi"];
            string gonderen = ConfigurationManager.AppSettings["smsObj"];
            string message = "\"" + activeCode + "\" Sayın " + nameSurname + " makinaturkiye.com 6 haneli telefon aktivasyon kodunuzdur.";
            string tur = "Turkce";
            string smsNN = "data=<sms><kno>" + kNumber + "</kno><kulad>" + uName + "</kulad><sifre>" + password + "</sifre>" +
            "<gonderen>" + gonderen + "</gonderen>" +
            "<telmesajlar>" +
            "<telmesaj><tel>" + gsmNumber + "</tel><mesaj>" + message + "</mesaj></telmesaj>" +
            "</telmesajlar>" +
            "<tur>" + tur + "</tur></sms>";
            return XmlPost("http://panel.vatansms.com/panel/smsgonderNNpost.php", smsNN);
        }
        private string XmlPost(string PostAddress, string xmlData)
        {
            using (WebClient wUpload = new WebClient())
            {
                wUpload.Headers.Add("Content-Type", "application/x-www-form-urlencoded");
                Byte[] bPostArray = Encoding.UTF8.GetBytes(xmlData);
                Byte[] bResponse = wUpload.UploadData(PostAddress, "POST", bPostArray);
                Char[] sReturnChars = Encoding.UTF8.GetChars(bResponse);
                string sWebPage = new string(sReturnChars);
                return sWebPage;
            }
        }
        public string CreateActiveCode()
        {
            string code = Guid.NewGuid().ToString();
            string lastCode = "";
            foreach (char k in code)
            {
                if (char.IsNumber(k)) lastCode = lastCode + k;

            }
            lastCode = lastCode.Substring(0, 6);

            var mCode = _phoneService.GetPhoneByActivationCode(lastCode);
            if (mCode!=null)
                lastCode = CreateActiveCode();
            return lastCode;
        }
        [HttpGet]
        public string CheckUserName(string username)
        {
            var checkUserName = _storeService.GetStoreByStoreUrlName(username);
            if (checkUserName!=null) return "false";
            else return "true";

            //bool checkUserName = entities.Stores.Any(x => x.StoreUrlName == username);
            //if (checkUserName) return "false";
            //else return "true";
        }
    }

}