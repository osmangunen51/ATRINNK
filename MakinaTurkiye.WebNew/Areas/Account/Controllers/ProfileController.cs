
#region Using Directives

using MakinaTurkiye.Entities.Tables.Common;
using MakinaTurkiye.Entities.Tables.Media;
using MakinaTurkiye.Entities.Tables.Stores;
using MakinaTurkiye.Services.Common;
using MakinaTurkiye.Services.Media;
using MakinaTurkiye.Services.Members;
using MakinaTurkiye.Services.Stores;
using MakinaTurkiye.Utilities.Controllers;
using MakinaTurkiye.Utilities.FileHelpers;
using NeoSistem.MakinaTurkiye.Core.Web.Helpers;
using NeoSistem.MakinaTurkiye.Web.Areas.Account.Constants;
using NeoSistem.MakinaTurkiye.Web.Areas.Account.Models.Profile;
using NeoSistem.MakinaTurkiye.Web.Areas.Account.Models.StoreImage;
using NeoSistem.MakinaTurkiye.Web.Models;
using NeoSistem.MakinaTurkiye.Web.Models.Authentication;
using NeoSistem.MakinaTurkiye.Web.Models.UtilityModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

#endregion

namespace NeoSistem.MakinaTurkiye.Web.Areas.Account.Controllers
{

    public class ProfileController : BaseAccountController
    {

        private readonly IMemberStoreService _memberStoreService;
        private readonly IStoreService _storeService;
        private readonly IStoreDealerService _storeDealerService;
        private readonly IAddressService _adressService;
        private readonly IStoreBrandService _storeBrandService;
        private readonly IPictureService _pictureService;
        private readonly IPhoneService _phoneService;
        private readonly IDealarBrandService _dealarBrandService;

        public ProfileController(IMemberStoreService memberStoreService, IStoreService storeService, IStoreDealerService storeDealerService,
            IAddressService addressService, IStoreBrandService storeBrandService,
            IPictureService pictureService, IPhoneService phoneService, IDealarBrandService dealarBrandService)
        {
            this._memberStoreService = memberStoreService;
            this._storeService = storeService;
            this._storeDealerService = storeDealerService;
            this._adressService = addressService;
            this._storeBrandService = storeBrandService;
            this._pictureService = pictureService;
            this._phoneService = phoneService;
            this._dealarBrandService = dealarBrandService;

            this._memberStoreService.CachingGetOrSetOperationEnabled = false;
            this._storeService.CachingGetOrSetOperationEnabled = false;
            this._storeDealerService.CachingGetOrSetOperationEnabled = false;
            this._adressService.CachingGetOrSetOperationEnabled = false;
            this._storeBrandService.CachingGetOrSetOperationEnabled = false;
            this._pictureService.CachingGetOrSetOperationEnabled = false;
            this._phoneService.CachingGetOrSetOperationEnabled = false;
            this._dealarBrandService.CachingGetOrSetOperationEnabled = false;
        }

        #region Http Get
        public ActionResult Index()
        {
            var storeModel = new StoreModel();
            var memberStore = _memberStoreService.GetMemberStoreByMemberMainPartyId(AuthenticationUser.CurrentUser.Membership.MainPartyId);

            int storeMainPartyId = memberStore.StoreMainPartyId.Value;

            DealerType pageType = (DealerType)byte.Parse(Request.QueryString["DealerType"]);
            switch (pageType)
            {
                case DealerType.Bayii:
                    ViewData["Dealer"] = true;


                    //var dealerItems = entities.StoreDealers.Where(c => c.MainPartyId.Value == storeMainPartyId && c.DealerType == (byte)DealerType.Bayii).ToList();
                    var dealerItems = _storeDealerService.GetStoreDealersByMainPartyId(storeMainPartyId, DealerTypeEnum.Dealer).ToList();
                    dealerItems.Insert(0, new StoreDealer { StoreDealerId = 0, DealerName = "< Lütfen Seçiniz >" });
                    storeModel.DealerItemsForDealer = new SelectList(dealerItems, "StoreDealerId", "DealerName", 0);


                    //var DealerIds = (from c in entities.StoreDealers where c.MainPartyId == storeMainPartyId && c.DealerType == (byte)DealerType.Bayii select c.StoreDealerId);
                    var dealerIds = dealerItems.Select(x => x.StoreDealerId).ToList();
                    storeModel.DealerAddressItems = _adressService.GetAddressByStoreDealerIds(dealerIds);
                    break;
                case DealerType.YetkiliServis:
                    ViewData["Service"] = true;

                    var serviceItems = _storeDealerService.GetStoreDealersByMainPartyId(storeMainPartyId, DealerTypeEnum.AuthorizedService);
                    serviceItems.Insert(0, new StoreDealer { StoreDealerId = 0, DealerName = "< Lütfen Seçiniz >" });
                    storeModel.DealerItemsForService = new SelectList(serviceItems, "StoreDealerId", "DealerName", 0);

                    var servisIds = serviceItems.Select(x => x.StoreDealerId).ToList();
                    storeModel.DealerAddressItems = _adressService.GetAddressByStoreDealerIds(servisIds);
                    break;
                case DealerType.Sube:
                    ViewData["Branch"] = true;

                    var branchItems = _storeDealerService.GetStoreDealersByMainPartyId(storeMainPartyId, DealerTypeEnum.Branch).ToList();
                    branchItems.Insert(0, new StoreDealer { StoreDealerId = 0, DealerName = "< Lütfen Seçiniz >" });
                    storeModel.DealerItemsForBranch = new SelectList(branchItems, "StoreDealerId", "DealerName", 0);

                    var subeIds = branchItems.Select(x => x.StoreDealerId).ToList();

                    storeModel.DealerAddressItems = _adressService.GetAddressByStoreDealerIds(subeIds);
                    break;
                default:
                    break;
            }

            var countryItems = _adressService.GetCountries();
            countryItems.Insert(0, new Country { CountryId = 0, CountryName = "< Lütfen Seçiniz >" });
            storeModel.CountryItems = new SelectList(countryItems, "CountryId", "CountryName");

            storeModel.LeftMenu = LeftMenuConstants.CreateLeftMenuModel(LeftMenuConstants.GroupName.StoreSettings, (byte)LeftMenuConstants.StoreSettingOtherInfo.Dealerships);

            return View(storeModel);
        }

        public ActionResult AboutUs()
        {
            ViewData["AboutUs"] = true;

            var memberStore = _memberStoreService.GetMemberStoreByMemberMainPartyId(AuthenticationUser.CurrentUser.Membership.MainPartyId);
            int storeMainPartyId = memberStore.StoreMainPartyId.Value;

            var store = _storeService.GetStoreByMainPartyId(storeMainPartyId);

            var storeModel = new StoreModel();

            storeModel.GeneralText = store.GeneralText;
            storeModel.FounderText = store.FounderText;
            storeModel.PhilosophyText = store.PhilosophyText;
            storeModel.HistoryText = store.HistoryText;


            storeModel.LeftMenu = LeftMenuConstants.CreateLeftMenuModel(LeftMenuConstants.GroupName.StoreSettings, (byte)LeftMenuConstants.StoreSettingOtherInfo.AboutUs);


            return View(storeModel);
        }

        public ActionResult Brand()
        {
            ViewData["Brand"] = true;
            var model = new StoreModel();

            var memberStore = _memberStoreService.GetMemberStoreByMemberMainPartyId(AuthenticationUser.CurrentUser.Membership.MainPartyId);
            var storeId = memberStore.StoreMainPartyId.Value;

            model.StoreBrandItems = _storeBrandService.GetStoreBrandByMainPartyId(storeId);
            model.LeftMenu = LeftMenuConstants.CreateLeftMenuModel(LeftMenuConstants.GroupName.StoreSettings, (byte)LeftMenuConstants.StoreSettingOtherInfo.OurBrands);

            return View(model);
        }

        public ActionResult Dealership()
        {
            ViewData["Dealership"] = true;
            var model = new StoreModel();
            var memberStore = _memberStoreService.GetMemberStoreByMemberMainPartyId(AuthenticationUser.CurrentUser.Membership.MainPartyId);
            var storeId = memberStore.StoreMainPartyId.Value;
            model.DealerBrandItems = _dealarBrandService.GetDealarBrandsByMainPartyId(storeId);
            model.LeftMenu = LeftMenuConstants.CreateLeftMenuModel(LeftMenuConstants.GroupName.StoreSettings, (byte)LeftMenuConstants.StoreSettingOtherInfo.Dealerships);

            return View(model);
        }

        public ActionResult StoreImage()
        {
            ViewData["StoreImage"] = true;

            var model = new StoreImagesModel();

            //var storeId = entities.MemberStores.SingleOrDefault(c => c.MemberMainPartyId == AuthenticationUser.Membership.MainPartyId).StoreMainPartyId.Value;

            //model.StoreDealerItems = entities.StoreDealers.Where(c => c.MainPartyId == storeId);

            //var DealerIds = (from c in entities.StoreDealers where c.MainPartyId == storeId select c.StoreDealerId);
            //model.PictureItems = (from c in entities.Pictures where DealerIds.Contains(c.StoreDealerId.Value) select c);
            model.LeftMenu = LeftMenuConstants.CreateLeftMenuModel(LeftMenuConstants.GroupName.StoreSettings, (byte)LeftMenuConstants.StoreSettingOtherInfo.CompanyVisual);
            int memberMainPartyId = Convert.ToInt32(AuthenticationUser.Membership.MainPartyId);
            var memberStore = _memberStoreService.GetMemberStoreByMemberMainPartyId(memberMainPartyId);

            var storeImages = _pictureService.GetPictureByMainPartyIdWithStoreImageType(memberStore.StoreMainPartyId.Value, StoreImageTypeEnum.StoreImage);

            foreach (var storeImageItem in storeImages)
            {
                model.StoreImageItems.Add(new StoreImageItem { ImageId = storeImageItem.PictureId, ImagePath = AppSettings.StoreImageFolder + storeImageItem.PicturePath });
            }
            return View(model);

        }

        public ActionResult ProfilePicture()
        {
            var model = new StoreModel();
            model.LeftMenu = LeftMenuConstants.CreateLeftMenuModel(LeftMenuConstants.GroupName.StoreSettings, (byte)LeftMenuConstants.StoreSettingOtherInfo.ProfileVisual);
            var memberStore = _memberStoreService.GetMemberStoreByMemberMainPartyId(AuthenticationUser.CurrentUser.Membership.MainPartyId);
            var storeId = memberStore.StoreMainPartyId.Value;
            var store = _storeService.GetStoreByMainPartyId(storeId);
            ViewData["ProfilePicture"] = true;
            model.Store = store;
            return View(model);
        }

        public ActionResult AreaCode(string CityId)
        {
            var city = _adressService.GetCityByCityId(Convert.ToInt32(CityId));
            return Content(city.AreaCode);
        }

        public ActionResult CultureCode(string CountryId)
        {
            var country = _adressService.GetCountryByCountryId(int.Parse(CountryId));
            return Content(country.CultureCode);
        }

        public ActionResult ZipCode(int TownId)
        {
            Town town = null;
            string zipCode = "";
            town = _adressService.GetTownByTownId(TownId);

            var district = _adressService.GetDistrictByDistrictId(town.DistrictId.Value);
            zipCode = district.ZipCode;
            return Json(zipCode, JsonRequestBehavior.AllowGet);
        }

        public ActionResult StoreProfileHomeDescription()
        {
            StoreProfileHomeDescriptinModel model = new StoreProfileHomeDescriptinModel();
            int memberMainPartyId = AuthenticationUser.CurrentUser.Membership.MainPartyId;
            var memberStore = _memberStoreService.GetMemberStoreByMemberMainPartyId(memberMainPartyId);
            var store = _storeService.GetStoreByMainPartyId(memberStore.StoreMainPartyId.Value);
            model.LeftMenu = LeftMenuConstants.CreateLeftMenuModel(LeftMenuConstants.GroupName.StoreSettings, (byte)LeftMenuConstants.StoreSettingOtherInfo.StoreProfileHomeDescription);
            model.StoreProfileDescription = store.StoreProfileHomeDescription;
            return View(model);
        }
        #endregion

        #region Http Post

        [HttpPost]
        public ActionResult Index(AddressModel model, byte DealerTypeId)
        {
            var address = new Address
            {
                MainPartyId = null,
                Avenue = model.Avenue,
                Street = model.Street,
                DoorNo = model.DoorNo,
                ApartmentNo = model.ApartmentNo,
                AddressDefault = true,
                StoreDealerId = model.StoreDealerId,
                AddressTypeId = null,
                CountryId = model.CountryId
            };

            if (model.CountryId > 0)
                address.CountryId = model.CountryId;
            else
                address.CountryId = null;

            if (model.CityId > 0)
                address.CityId = model.CityId;
            else
                address.CityId = null;

            if (model.LocalityId > 0)
                address.LocalityId = model.LocalityId;
            else
                address.LocalityId = null;

            if (model.TownId > 0)
                address.TownId = model.TownId;
            else
                address.TownId = null;

            _adressService.InsertAdress(address);

            if (!string.IsNullOrWhiteSpace(model.InstitutionalPhoneNumber))
            {
                var phone = new Phone
                {
                    AddressId = address.AddressId,
                    MainPartyId = null,
                    PhoneAreaCode = model.InstitutionalPhoneAreaCode,
                    PhoneCulture = model.InstitutionalPhoneCulture,
                    PhoneNumber = model.InstitutionalPhoneNumber,
                    PhoneType = (byte)PhoneType.Phone,
                };
                _phoneService.InsertPhone(phone);
            }

            if (!string.IsNullOrWhiteSpace(model.InstitutionalPhoneNumber2))
            {
                var phone = new Phone
                {
                    AddressId = address.AddressId,
                    MainPartyId = null,
                    PhoneAreaCode = model.InstitutionalPhoneAreaCode2,
                    PhoneCulture = model.InstitutionalPhoneCulture2,
                    PhoneNumber = model.InstitutionalPhoneNumber2,
                    PhoneType = (byte)PhoneType.Phone,
                };
                _phoneService.InsertPhone(phone);
            }

            if (!string.IsNullOrWhiteSpace(model.InstitutionalFaxNumber))
            {
                var phone = new Phone
                {
                    AddressId = address.AddressId,
                    MainPartyId = null,
                    PhoneAreaCode = model.InstitutionalFaxAreaCode,
                    PhoneCulture = model.InstitutionalFaxCulture,
                    PhoneNumber = model.InstitutionalFaxNumber,
                    PhoneType = (byte)PhoneType.Fax,
                };
                _phoneService.InsertPhone(phone);
            }

            if (!string.IsNullOrWhiteSpace(model.InstitutionalGSMNumber))
            {
                var phone = new Phone
                {
                    AddressId = address.AddressId,
                    MainPartyId = null,
                    PhoneAreaCode = model.InstitutionalGSMAreaCode,
                    PhoneCulture = model.InstitutionalGSMCulture,
                    PhoneNumber = model.InstitutionalGSMNumber,
                    PhoneType = (byte)PhoneType.Gsm,
                };
                _phoneService.InsertPhone(phone);
            }

            if (!string.IsNullOrWhiteSpace(model.InstitutionalGSMNumber2))
            {
                var phone = new Phone
                {
                    AddressId = address.AddressId,
                    MainPartyId = null,
                    PhoneAreaCode = model.InstitutionalGSMAreaCode2,
                    PhoneCulture = model.InstitutionalGSMCulture2,
                    PhoneNumber = model.InstitutionalGSMNumber2,
                    PhoneType = (byte)PhoneType.Gsm,
                };
                _phoneService.InsertPhone(phone);
            }

            DealerType pageType = (DealerType)DealerTypeId;
            switch (pageType)
            {
                case DealerType.Bayii:
                    return RedirectToAction("Index", "Profile", new { DealerType = (byte)DealerType.Bayii });
                case DealerType.YetkiliServis:
                    return RedirectToAction("Index", "Profile", new { DealerType = (byte)DealerType.YetkiliServis });
                case DealerType.Sube:
                    return RedirectToAction("Index", "Profile", new { DealerType = (byte)DealerType.Sube });
                default:
                    return RedirectToAction("Index", "Profile", new { DealerType = (byte)DealerType.Bayii });
            }
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult AboutUs(string GeneralText)
        {
            ViewData["AboutUs"] = true;

            int storeMainPartyId = _memberStoreService.GetMemberStoreByMemberMainPartyId(AuthenticationUser.Membership.MainPartyId).StoreMainPartyId.Value;

            var store = _storeService.GetStoreByMainPartyId(storeMainPartyId);

            store.GeneralText = GeneralText;
            //store.FounderText = FounderText;
            //store.HistoryText = HistoryText;
            //store.PhilosophyText = PhilosophyText;
            _storeService.UpdateStore(store);

            return Json(true);
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult StoreProfileHomeDescription(string Text)
        {

            int memberMainPartyId = AuthenticationUser.CurrentUser.Membership.MainPartyId;
            var memberStore = _memberStoreService.GetMemberStoreByMemberMainPartyId(memberMainPartyId);
            var store = _storeService.GetStoreByMainPartyId(memberStore.StoreMainPartyId.Value);
            store.StoreProfileHomeDescription = Text;
            _storeService.UpdateStore(store);
            return Json(true, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult Brand(FormCollection coll, string StoreBrandName, string BrandDescription)
        {
            var storeId = _memberStoreService.GetMemberStoreByMemberMainPartyId(AuthenticationUser.Membership.MainPartyId).StoreMainPartyId.Value;

            for (int i = 0; i < Request.Files.Count; i++)
            {
                HttpPostedFileBase file = Request.Files[i];

                if (file.ContentLength > 0)
                {
                    string fileName = FileHelpers.ImageThumbnail(AppSettings.StoreBrandImageFolder, file, 100, FileHelpers.ThumbnailType.Width);

                    var curStoreBrand = new StoreBrand()
                    {
                        BrandPicture = fileName,
                        MainPartyId = storeId,
                        BrandName = StoreBrandName,
                        BrandDescription = BrandDescription,
                    };
                    _storeBrandService.InsertStoreBrand(curStoreBrand);
                }
            }
            return RedirectToAction("Brand");
        }

        [HttpPost]
        public ActionResult Dealership(FormCollection coll, string BrandName)
        {
            var storeId = _memberStoreService.GetMemberStoreByMemberMainPartyId(AuthenticationUser.Membership.MainPartyId).StoreMainPartyId.Value;

            for (int i = 0; i < Request.Files.Count; i++)
            {
                HttpPostedFileBase file = Request.Files[i];

                if (file.ContentLength > 0)
                {
                    string fileName = FileHelpers.ImageThumbnail(AppSettings.DealerBrandImageFolder, file, 50, FileHelpers.ThumbnailType.Width);

                    var curDealerBrand = new DealerBrand()
                    {
                        DealerBrandPicture = fileName,
                        MainPartyId = storeId,
                        DealerBrandName = BrandName,
                    };

                    _dealarBrandService.InsertDealerBrand(curDealerBrand);
                }
            }
            return RedirectToAction("Dealership");
        }

        [HttpPost]
        public ActionResult StoreImage(FormCollection coll)
        {
            ViewData["StoreImage"] = true;

            var model = new StoreImagesModel();


            model.LeftMenu = LeftMenuConstants.CreateLeftMenuModel(LeftMenuConstants.GroupName.StoreSettings, (byte)LeftMenuConstants.StoreSettingOtherInfo.CompanyVisual);

            int memberMainPartyId = Convert.ToInt32(AuthenticationUser.Membership.MainPartyId);
            var memberStore = _memberStoreService.GetMemberStoreByMemberMainPartyId(memberMainPartyId);
            for (int i = 0; i < Request.Files.Count; i++)
            {
                HttpPostedFileBase file = Request.Files[i];

                if (file.ContentLength > 0)
                {
                    string fileName = FileHelpers.ImageThumbnail(AppSettings.StoreImageFolder, file, 170, FileHelpers.ThumbnailType.Width);

                    var curPicture = new Picture()
                    {
                        PicturePath = fileName,
                        ProductId = null,
                        MainPartyId = memberStore.StoreMainPartyId,
                        PictureName = String.Empty,
                        StoreImageType = (byte)StoreImageType.StoreImage
                    };
                    _pictureService.InsertPicture(curPicture);

                }
            }
            var storeImages = _pictureService.GetPictureByMainPartyIdWithStoreImageType(memberStore.StoreMainPartyId.Value, StoreImageTypeEnum.StoreImage).OrderByDescending(x => x.PictureId);
            foreach (var storeImageItem in storeImages)
            {
                model.StoreImageItems.Add(new StoreImageItem { ImageId = storeImageItem.PictureId, ImagePath = AppSettings.StoreImageFolder + storeImageItem.PicturePath });
            }
            return View(model);
        }

        [HttpPost]
        public ActionResult DealerAddForBranch(string DealerNameForBranch)
        {
            var storeId = _memberStoreService.GetMemberStoreByMemberMainPartyId(AuthenticationUser.Membership.MainPartyId).StoreMainPartyId.Value;

            var curStoreDealer = new StoreDealer
            {
                DealerName = DealerNameForBranch,
                DealerType = (byte)DealerType.Sube,
                MainPartyId = storeId,
            };
            _storeDealerService.InsertStoreDealer(curStoreDealer);

            var dealerItems = _storeDealerService.GetStoreDealersByMainPartyId(storeId, DealerTypeEnum.Branch).Select(x => new { x.StoreDealerId, x.DealerName });

            return Json(dealerItems);
        }

        [HttpPost]
        public ActionResult DealerAddForDealer(string DealerNameForDealer)
        {
            var storeId = _memberStoreService.GetMemberStoreByMemberMainPartyId(AuthenticationUser.Membership.MainPartyId).StoreMainPartyId.Value;

            var curStoreDealer = new StoreDealer
            {
                DealerName = DealerNameForDealer,
                DealerType = (byte)DealerType.Bayii,
                MainPartyId = storeId,
            };
            _storeDealerService.InsertStoreDealer(curStoreDealer);


            var dealerItems = _storeDealerService.GetStoreDealersByMainPartyId(storeId, DealerTypeEnum.Dealer).Select(x => new { x.StoreDealerId, x.DealerName });

            return Json(dealerItems);
        }

        [HttpPost]
        public ActionResult DealerAddForService(string DealerNameForService)
        {
            var storeId = _memberStoreService.GetMemberStoreByMemberMainPartyId(AuthenticationUser.Membership.MainPartyId).StoreMainPartyId.Value;

            var curStoreDealer = new StoreDealer
            {
                DealerName = DealerNameForService,
                DealerType = (byte)DealerType.YetkiliServis,
                MainPartyId = storeId,
            };
            _storeDealerService.InsertStoreDealer(curStoreDealer);

            var dealerItems = _storeDealerService.GetStoreDealersByMainPartyId(storeId, DealerTypeEnum.AuthorizedService).Select(x => new { x.StoreDealerId, x.DealerName });

            return Json(dealerItems);
        }

        [HttpPost]
        public ActionResult DeletePicture(int id)
        {
            var storeId = _memberStoreService.GetMemberStoreByMemberMainPartyId(AuthenticationUser.Membership.MainPartyId).StoreMainPartyId.Value;

            var picture = _pictureService.GetPictureByPictureId(id);
            if (picture != null)
            {
                FileHelpers.Delete(AppSettings.StoreDealerImageFolder + picture.PicturePath);
                FileHelpers.Delete(AppSettings.StoreDealerImageFolder + FileHelper.ImageThumbnailName(picture.PicturePath));

                _pictureService.DeletePicture(picture);
            }

            var model = new StoreModel();

            // model.StoreDealerItems = entities.StoreDealers.Where(c => c.MainPartyId == storeId);

            model.StoreDealerItems = _storeDealerService.GetStoreDealersByMainPartyId(storeId, DealerTypeEnum.All);


            var dealerIds = model.StoreDealerItems.Select(x => x.StoreDealerId).ToList();
            model.PictureItems = _pictureService.GetPictureByStoreDealerIds(dealerIds);

            return View("/Areas/Account/Views/Profile/PictureList.cshtml", model);
        }

        [HttpPost]
        public ActionResult DeleteAddress(int AddressId, DealerType type)
        {
            var storeId = _memberStoreService.GetMemberStoreByMemberMainPartyId(AuthenticationUser.Membership.MainPartyId).StoreMainPartyId.Value;

            var address = _adressService.GetAddressByAddressId(AddressId);
            var phoneItems = _phoneService.GetPhonesAddressId(AddressId);
            foreach (var phoneItem in phoneItems)
            {
                _phoneService.DeletePhone(phoneItem);
            }
            _adressService.DeleteAddress(address);



            var storeModel = new StoreModel();
            IList<Address> addressItems = null;

            switch (type)
            {
                case DealerType.Bayii:
                    var dealerIds = _storeDealerService.GetStoreDealersByMainPartyId(storeId, DealerTypeEnum.Dealer).Select(x => x.StoreDealerId).ToList();
                    addressItems = _adressService.GetAddressByStoreDealerIds(dealerIds).ToList();
                    break;
                case DealerType.YetkiliServis:
                    var serviceIds = _storeDealerService.GetStoreDealersByMainPartyId(storeId, DealerTypeEnum.AuthorizedService).Select(x => x.StoreDealerId).ToList();
                    addressItems = _adressService.GetAddressByStoreDealerIds(serviceIds).ToList();
                    break;
                case DealerType.Sube:

                    var branchIds = _storeDealerService.GetStoreDealersByMainPartyId(storeId, DealerTypeEnum.Branch).Select(x => x.StoreDealerId).ToList();
                    addressItems = _adressService.GetAddressByStoreDealerIds(branchIds).ToList();
                    break;
                default:
                    break;
            }

            return View("/Areas/Account/Views/Profile/DealerAddressItems.cshtml", addressItems);
        }

        [HttpPost]
        public JsonResult DeleteDealerBrand(int DealerBrandId)
        {
            var issuccess = false;
            try
            {
                //var storeId = _memberStoreService.GetMemberStoreByMemberMainPartyId(AuthenticationUser.Membership.MainPartyId).StoreMainPartyId.Value;

                var dealerBrand = _dealarBrandService.GetDealerBrandByDealerBrandId(DealerBrandId);

                FileHelpers.Delete(AppSettings.DealerBrandImageFolder + dealerBrand.DealerBrandPicture);
                FileHelpers.Delete(AppSettings.DealerBrandImageFolder + FileHelper.ImageThumbnailName(dealerBrand.DealerBrandPicture));

                _dealarBrandService.DeleteDealerBrand(dealerBrand);

                issuccess = true;
            }
            catch
            {
                issuccess = false;
            }

            return Json(issuccess, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult DeleteStoreBrand(int StoreBrandId)
        {
            var issuccess = false;
            try
            {
                //    var storeId = entities.MemberStores.SingleOrDefault(c => c.MemberMainPartyId == AuthenticationUser.Membership.MainPartyId).StoreMainPartyId.Value;

                var storeBrand = _storeBrandService.GetStoreBrandByStoreBrand(StoreBrandId);

                FileHelpers.Delete(AppSettings.DealerBrandImageFolder + storeBrand.BrandPicture);
                FileHelpers.Delete(AppSettings.DealerBrandImageFolder + FileHelper.ImageThumbnailName(storeBrand.BrandPicture));

                _storeBrandService.DeleteStoreBrand(storeBrand);

                issuccess = true;
            }
            catch
            {
                issuccess = false;
            }

            return Json(issuccess, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult ProfilePicture(FormCollection coll, bool deleteButton)
        {
            var storeId = _memberStoreService.GetMemberStoreByMemberMainPartyId(AuthenticationUser.Membership.MainPartyId).StoreMainPartyId.Value;
            var store = _storeService.GetStoreByMainPartyId(storeId);
            if (deleteButton)
            {
                FileHelpers.Delete(AppSettings.StoreProfilePicture + store.StorePicture);
                store.StorePicture = string.Empty;

                _storeService.UpdateStore(store);
                return RedirectToAction("ProfilePicture");
            }

            HttpPostedFileBase file = Request.Files["ProfilePicture"];
            if (file.ContentLength > 0)
            {
                FileHelpers.Delete(AppSettings.StoreProfilePicture + store.StorePicture);

                string fileName = FileHelpers.ImageThumbnail(AppSettings.StoreProfilePicture, file, 800, FileHelpers.ThumbnailType.Width);
                store.StorePicture = fileName;
                _storeService.UpdateStore(store);
            }
            return RedirectToAction("ProfilePicture");
        }

        #endregion

        #region Json Result
        public JsonResult Cities(int id)
        {
            IList<City> cityItems;

            cityItems = _adressService.GetCitiesByCountryId(id);
            cityItems.Insert(0, new City { CityId = 0, CityName = "< Lütfen Seçiniz >" });

            return Json(new SelectList(cityItems, "CityId", "CityName"), JsonRequestBehavior.AllowGet);
        }

        public JsonResult Localities(int id)
        {
            IList<Locality> localityItems;

            localityItems = _adressService.GetLocalitiesByCityId(id);
            localityItems.Insert(0, new Locality { LocalityId = 0, LocalityName = "< Lütfen Seçiniz >" });

            return Json(new SelectList(localityItems, "LocalityId", "LocalityName"), JsonRequestBehavior.AllowGet);
        }

        public JsonResult Towns(int id)
        {
            IList<Town> townItems;

            townItems = _adressService.GetTownsByLocalityId(id);
            townItems.Insert(0, new Town { TownId = 0, TownName = "< Lütfen Seçiniz >" });

            return Json(new SelectList(townItems, "TownId", "TownName"), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult DeleteStoreImage(int ImageId)
        {
            var picture = _pictureService.GetPictureByPictureId(ImageId);
            if (picture != null)
            {
                var filePath = picture.PicturePath;
                FileHelpers.Delete(AppSettings.StoreDealerImageFolder + filePath);
                _pictureService.DeletePicture(picture);

            }
            return Json(true);
        }
        #endregion

        #region Public Metods
        public List<PictureModel> PictureList
        {
            get
            {
                if (Session["PictureItems"] == null)
                {
                    Session["PictureItems"] = new List<PictureModel>();
                }
                return Session["PictureItems"] as List<PictureModel>;
            }
            set { Session["PictureItems"] = value; }
        }
        #endregion

    }
}