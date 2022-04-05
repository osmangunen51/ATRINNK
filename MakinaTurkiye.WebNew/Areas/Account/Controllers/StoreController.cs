using MakinaTurkiye.Core;
using MakinaTurkiye.Entities.Tables.Catalog;
using MakinaTurkiye.Entities.Tables.Common;
using MakinaTurkiye.Entities.Tables.Media;
using MakinaTurkiye.Entities.Tables.Members;
using MakinaTurkiye.Entities.Tables.Settings;
using MakinaTurkiye.Entities.Tables.Stores;
using MakinaTurkiye.Services.Catalog;
using MakinaTurkiye.Services.Common;
using MakinaTurkiye.Services.Media;
using MakinaTurkiye.Services.Members;
using MakinaTurkiye.Services.Settings;
using MakinaTurkiye.Services.Stores;
using MakinaTurkiye.Utilities.Controllers;
using MakinaTurkiye.Utilities.FileHelpers;
using MakinaTurkiye.Utilities.FormatHelpers;
using MakinaTurkiye.Utilities.HttpHelpers;
using NeoSistem.EnterpriseEntity.Extensions;
using NeoSistem.EnterpriseEntity.Extensions.Data;
using NeoSistem.MakinaTurkiye.Core.Web.Helpers;
using NeoSistem.MakinaTurkiye.Web.Areas.Account.Constants;
using NeoSistem.MakinaTurkiye.Web.Areas.Account.Models;
using NeoSistem.MakinaTurkiye.Web.Areas.Account.Models.StoreImage;
using NeoSistem.MakinaTurkiye.Web.Areas.Account.Models.Stores;
using NeoSistem.MakinaTurkiye.Web.Areas.Account.Models.Stores.StoresViewModel;
using NeoSistem.MakinaTurkiye.Web.Models;
using NeoSistem.MakinaTurkiye.Web.Models.Authentication;
using NeoSistem.MakinaTurkiye.Web.Models.Stores;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NeoSistem.MakinaTurkiye.Web.Areas.Account.Controllers
{



    public class StoreController : BaseAccountController
    {
        private readonly IStoreChangeHistoryService _storeChangeHistoryService;
        private readonly IStoreService _storeService;
        private readonly ICategoryService _categoryService;
        private readonly IPictureService _pictureService;
        private readonly IMemberStoreService _memberStoreService;
        private readonly IStoreCatologFileService _storeCatologFileService;
        private readonly IPhoneService _phoneService;
        private readonly IMemberSettingService _memberSettingService;
        private readonly IAddressService _addressService;
        private readonly IStoreActivityCategoryService _storeActivityCategoryService;
        private readonly IStoreActivityTypeService _storeActivityTypeService;
        private readonly IMemberService _memberService;
        private readonly IActivityTypeService _activityTypeService;
        private readonly ICategoryPlaceChoiceService _categoryPlaceChoiceService;
        private readonly IConstantService _constantService;
        private readonly IStoreSectorService _storeSectorService;
        private readonly ICertificateTypeService _certificateTypeService;

        public StoreController(IStoreChangeHistoryService storeChangeHistoryService,
            IStoreService storeService, ICategoryService categoryService,
            IPictureService pictureService,
            IMemberStoreService memberStoreService,
            IStoreCatologFileService storeCatologFileService,
            IPhoneService phoneService,
            IMemberSettingService memberSettingService,
            IAddressService addressService,
            IStoreActivityCategoryService storeActivityCategoryService,
            IStoreActivityTypeService storeActivityTypeService,
            IMemberService memberService,
            IActivityTypeService activityTypeService,
            ICategoryPlaceChoiceService categoryPlaceChoiceService, IConstantService constantService, IStoreSectorService storeSectorService,
            ICertificateTypeService certificateTypeService)
        {
            this._storeChangeHistoryService = storeChangeHistoryService;
            this._storeService = storeService;
            this._categoryService = categoryService;
            this._pictureService = pictureService;
            this._memberStoreService = memberStoreService;
            this._storeCatologFileService = storeCatologFileService;
            this._phoneService = phoneService;
            this._memberSettingService = memberSettingService;
            this._addressService = addressService;
            this._storeActivityCategoryService = storeActivityCategoryService;
            this._storeActivityTypeService = storeActivityTypeService;
            this._memberService = memberService;
            this._activityTypeService = activityTypeService;
            this._categoryPlaceChoiceService = categoryPlaceChoiceService;
            this._constantService = constantService;
            this._storeSectorService = storeSectorService;
            this._certificateTypeService = certificateTypeService;
            this._storeService.CachingGetOrSetOperationEnabled = false;
            this._categoryService.CachingGetOrSetOperationEnabled = false;
            this._pictureService.CachingGetOrSetOperationEnabled = false;
            this._memberStoreService.CachingGetOrSetOperationEnabled = false;
            this._storeCatologFileService.CachingGetOrSetOperationEnabled = false;
            this._phoneService.CachingGetOrSetOperationEnabled = false;
            this._memberSettingService.CachingGetOrSetOperationEnabled = false;
            this._addressService.CachingGetOrSetOperationEnabled = false;
            this._storeActivityTypeService.CachingGetOrSetOperationEnabled = false;
            this._memberService.CachingGetOrSetOperationEnabled = false;
            this._activityTypeService.CachingGetOrSetOperationEnabled = false;
            this._categoryPlaceChoiceService.CachingGetOrSetOperationEnabled = false;
            this._constantService.CachingGetOrSetOperationEnabled = false;
            this._certificateTypeService.CachingGetOrSetOperationEnabled = false;

        }
        public ActionResult Index()
        {
            int mainPartyId = _memberStoreService.GetMemberStoreByMemberMainPartyId(AuthenticationUser.CurrentUser.Membership.MainPartyId).StoreMainPartyId.Value;

            var store = _storeService.GetStoreByMainPartyId(mainPartyId);
            var phones = _phoneService.GetPhonesByMainPartyId(mainPartyId);
            var address = _addressService.GetAddressesByMainPartyId(mainPartyId);

            var storeActivityCategories = _storeActivityCategoryService.GetStoreActivityCategoriesByMainPartyId(store.MainPartyId);

            var model = new StoreModel();

            model.Store = store;
            model.StoreActivityCategory = storeActivityCategories;
            model.PhoneItems = phones;
            model.AddressItems = address;

            var dataStoreActivityType = new Data.StoreActivityType();
            model.StoreActivityItems = _storeActivityTypeService.GetStoreActivityTypesByStoreId(mainPartyId);

            return View(model);
        }

        public ActionResult UpdateLogo()
        {
            int mainPartyId = AuthenticationUser.Membership.MainPartyId;
            var memberStore = _memberStoreService.GetMemberStoreByMemberMainPartyId(mainPartyId);
            var store = _storeService.GetStoreByMainPartyId(memberStore.StoreMainPartyId.Value);

            var model = new StoreModel();
            model.Store = store;
            model.LeftMenu = LeftMenuConstants.CreateLeftMenuModel(LeftMenuConstants.GroupName.StoreSettings, (byte)LeftMenuConstants.StoreSettingGeneralInfo.LogoUpdate);
            var categoryHelps = _categoryPlaceChoiceService.GetCategoryPlaceChoiceByCategoryPlaceTypeByIsProduct((byte)HelpCategoryPlace.StoreLogoUpdate, false);
            foreach (var item in categoryHelps)
            {
                string url = UrlBuilder.GetHelpCategoryUrl(item.CategoryId, item.Category.CategoryName);
                model.HelpList.Add(new MTHelpModeltem { Url = url, HelpCategoryName = item.Category.CategoryName });
            }
            return View(model);
        }



        [HttpPost]
        public ActionResult UpdateLogo(StoreModel model, bool Delete)
        {




            int mainPartyId = AuthenticationUser.Membership.MainPartyId;
            var memberStore = _memberStoreService.GetMemberStoreByMemberMainPartyId(mainPartyId);

            var store = _storeService.GetStoreByMainPartyId(memberStore.StoreMainPartyId.Value);



            HttpPostedFileBase file = Request.Files[0];
            if (file != null && file.ContentLength > 0)
            {
                _storeChangeHistoryService.AddStoreChangeHistoryForStore(store);

                string resizeStoreFolder = this.Server.MapPath(AppSettings.ResizeStoreLogoFolder);
                string storeLogoThumbSize = AppSettings.StoreLogoThumbSizes;
                string oldStoreLogo = store.StoreLogo;

                List<string> thumbSizesForStoreLogo = new List<string>();
                thumbSizesForStoreLogo.AddRange(storeLogoThumbSize.Split(';'));
                string resizeStoreLogoImageFilePath = resizeStoreFolder + store.MainPartyId.ToString() + "\\";
                if (!string.IsNullOrEmpty(oldStoreLogo))
                {
                    if (System.IO.File.Exists(this.Server.MapPath(AppSettings.StoreLogoFolder) + oldStoreLogo))
                    {
                        // kullanıcı yeni üye olduğu zamanki resimleri sil.
                        FileHelpers.Delete(AppSettings.StoreLogoFolder + oldStoreLogo);
                        FileHelpers.Delete(AppSettings.StoreLogoThumb100x100Folder + oldStoreLogo);
                        FileHelpers.Delete(AppSettings.StoreLogoThumb300x200Folder + oldStoreLogo);
                    }

                    if (!oldStoreLogo.Trim().Equals(store.StoreLogo))
                    {
                        //eski  resize resimleri sil.
                        FileHelpers.Delete(resizeStoreLogoImageFilePath + oldStoreLogo);
                        oldStoreLogo = oldStoreLogo.Replace("_logo.jpg", "");
                        foreach (var item in thumbSizesForStoreLogo)
                        {
                            string resizeStoreThumbsLogoName = oldStoreLogo + "-" + item.Replace("x*", "X") + ".jpg";
                            FileHelpers.Delete(resizeStoreLogoImageFilePath + "thumbs\\" + resizeStoreThumbsLogoName);
                        }
                    }

                }

                if (!Directory.Exists(resizeStoreLogoImageFilePath + "thumbs"))
                {
                    var di = Directory.CreateDirectory(string.Format("{0}{1}", resizeStoreFolder, store.MainPartyId.ToString()));
                    di.CreateSubdirectory("thumbs");
                }


                // eski logoyu kopyala, varsa ustune yaz
                string storeLogoImageFileName = store.StoreName.ToImageFileName() + "_logo.jpg";
                string storeLogoImageFileSavePath = resizeStoreLogoImageFilePath + storeLogoImageFileName;
                Request.Files[0].SaveAs(storeLogoImageFileSavePath);

                bool thumbResult = ImageProcessHelper.ImageResize(storeLogoImageFileSavePath,
                resizeStoreLogoImageFilePath + "thumbs\\" + store.StoreName.ToImageFileName(), thumbSizesForStoreLogo);
                store.StoreLogo = storeLogoImageFileName;

                _storeService.UpdateStore(store);


            }

            return RedirectToAction("updatelogo", "store");
        }
        public ActionResult UpdateBanner(string error)
        {
            if (!string.IsNullOrEmpty(error))
            {
                if (error == "null")
                {
                    ViewData["message"] = "Lütfen bir dosya seçiniz.";
                }
                else if (error == "type")
                {
                    ViewData["message"] = "Eklemiş olduğunuz dosya türü, uyuşmamaktdaır.";
                }
            }
            int mainPartyId = AuthenticationUser.Membership.MainPartyId;
            var memberStore = _memberStoreService.GetMemberStoreByMemberMainPartyId(mainPartyId);
            var store = _storeService.GetStoreByMainPartyId(memberStore.StoreMainPartyId.Value);

            var model = new StoreModel();
            model.Store = store;
            model.LeftMenu = LeftMenuConstants.CreateLeftMenuModel(LeftMenuConstants.GroupName.StoreSettings, (byte)LeftMenuConstants.StoreSettingGeneralInfo.StoreBannerUpdate);
            //var categoryHelps = _categoryService.GetCategoryPlaceChoiceByCategoryPlaceTypeByIsProduct((byte)HelpCategoryPlace.StoreLogoUpdate, false);
            //foreach (var item in categoryHelps)
            //{
            //    string url = UrlBuilder.GetHelpCategoryUrl(item.CategoryId, item.Category.CategoryName);
            //    model.HelpList.Add(new MTHelpModeltem { Url = url, HelpCategoryName = item.Category.CategoryName });
            //}
            model.StoreBanner = store.StoreBanner;
            return View(model);
        }
        [HttpPost]
        public ActionResult UpdateBanner(StoreModel model, bool Delete)
        {





            int mainPartyId = AuthenticationUser.Membership.MainPartyId;
            var memberStore = _memberStoreService.GetMemberStoreByMemberMainPartyId(mainPartyId);
            var store = _storeService.GetStoreByMainPartyId(memberStore.StoreMainPartyId.Value);



            HttpPostedFileBase file = Request.Files[0];
            if (file != null && file.ContentLength > 0)
            {
                string[] ImageContentType = { "image/bmp", "image/cis-cod", "image/gif", "image/ief", "image/jpeg", "image/jpg",
                                                "image/jpeg", "image/pipeg", "image/svg+xml", "image/tiff", "image/tiff",
                                                "image/x-cmu-raster", "image/x-cmx", "image/x-icon", "image/x-portable-anymap",
                                                "image/x-portable-bitmap", "image/x-portable-graymap", "image/x-portable-pixmap",
                                                "image/x-rgb", "image/x-xbitmap", "image/x-xpixmap", "image/x-xwindowdump",
                                                "image/pjpeg", "image/png", "image/x-png" };
                if (ImageContentType.Any(fc => fc == file.ContentType))
                {
                    //string storeBannerThumbSize = "1400x250";

                    string oldStoreBanner = store.StoreBanner;

                    //List<string> thumbSizesForStoreBanner = new List<string>();
                    //thumbSizesForStoreBanner.Add(storeBannerThumbSize);

                    if (!string.IsNullOrEmpty(oldStoreBanner))
                    {
                        if (System.IO.File.Exists(this.Server.MapPath(AppSettings.StoreBannerFolder) + store.StoreBanner))
                        {
                            // kullanıcı yeni üye olduğu zamanki resimleri sil.
                            FileHelpers.Delete(AppSettings.StoreBannerFolder + oldStoreBanner);

                        }
                        if (System.IO.File.Exists(this.Server.MapPath(AppSettings.StoreBannerFolder) + store.StoreBanner.Replace("_banner", "-1400x280")))
                        {
                            // kullanıcı yeni üye olduğu zamanki resimleri sil.
                            FileHelpers.Delete(AppSettings.StoreBannerFolder + oldStoreBanner.Replace("_banner", "-1400x280"));
                        }

                        //if (!oldStoreBanner.Trim().Equals(storeModel.StoreLogo))
                        //{
                        //    //eski  resize resimleri sil.
                        //    FileHelpers.Delete(resizeStoreLogoImageFilePath + oldStoreBanner);
                        //    oldStoreLogo = oldStoreLogo.Replace("_logo.jpg", "");
                        //    foreach (var item in thumbSizesForStoreLogo)
                        //    {
                        //        string resizeStoreThumbsLogoName = oldStoreLogo + "-" + item.Replace("x*", "X") + ".jpg";
                        //        FileHelpers.Delete(resizeStoreLogoImageFilePath + "thumbs\\" + resizeStoreThumbsLogoName);
                        //    }
                        //}

                    }

                    //if (!Directory.Exists(resizeStoreLogoImageFilePath + "thumbs"))
                    //{
                    //    var di = Directory.CreateDirectory(string.Format("{0}{1}", resizeStoreFolder, storeM.MainPartyId.ToString()));
                    //    di.CreateSubdirectory("thumbs");
                    //}


                    // eski logoyu kopyala, varsa ustune yaz
                    string mapPath = this.Server.MapPath(AppSettings.StoreBannerFolder);
                    string storeBannerImageFileName = store.StoreUrlName.ToImageFileName() + "-" + store.MainPartyId + "_banner.jpg";
                    string storeBannerImageFileSavePath = mapPath + storeBannerImageFileName;

                    Request.Files[0].SaveAs(storeBannerImageFileSavePath);
                    Image img = ImageProcessHelper.resizeImageBanner(1400, 280, storeBannerImageFileSavePath);
                    ImageProcessHelper.SaveJpeg(storeBannerImageFileSavePath, img, 80, "_banner", "-1400x280");

                    //bool thumbResult = ImageProcessHelper.ImageResize(storeBannerImageFileSavePath,
                    //mapPath + "\\" + storeModel.StoreUrlName.ToImageFileName()+"-"+storeModel.MainPartyId, thumbSizesForStoreBanner);

                    store.StoreBanner = storeBannerImageFileName;

                    _storeService.UpdateStore(store);
                    return RedirectToAction("updatebanner", "store", new { error = "no" });
                }
                else
                {
                    return RedirectToAction("updatebanner", "store", new { error = "type" });
                }


            }
            else
            {
                return RedirectToAction("updatebanner", "store", new { error = "null" });
            }
        }
        public ActionResult CreateCertificate()
        {
            CreateStoreCertificateModel model = new CreateStoreCertificateModel();
            model.LeftMenu = LeftMenuConstants.CreateLeftMenuModel(LeftMenuConstants.GroupName.StoreSettings, (byte)LeftMenuConstants.StoreSettingGeneralInfo.StoreCertificate);
            var certificateTypes = _certificateTypeService.GetCertificateTypes().Where(x => x.Active == true).ToList();
            for (int i = 0; i < certificateTypes.Count; i++)
            {
                var certificateItem = certificateTypes[i];
                model.CertificateTypes.Add(new SelectListItem
                {
                    Text = certificateItem.Name,
                    Value = certificateItem.CertificateTypeId.ToString()
                });
            }
            model.CertificateTypes.Add(new SelectListItem { Text = "Diğer", Value = "99999" });
            return View(model);
        }
        [HttpPost]
        public ActionResult CreateCertificate(CreateStoreCertificateModel model)
        {
            string[] ImageContentType = { "image/bmp", "image/cis-cod", "image/gif", "image/ief", "image/jpeg", "image/jpg",
                                                "image/jpeg", "image/pipeg", "image/svg+xml", "image/tiff", "image/tiff",
                                                "image/x-cmu-raster", "image/x-cmx", "image/x-icon", "image/x-portable-anymap",
                                                "image/x-portable-bitmap", "image/x-portable-graymap", "image/x-portable-pixmap",
                                                "image/x-rgb", "image/x-xbitmap", "image/x-xpixmap", "image/x-xwindowdump",
                                                "image/pjpeg", "image/png", "image/x-png" };

            int memberMainPartyId = Convert.ToInt32(AuthenticationUser.CurrentUser.Membership.MainPartyId);

            var memberStore = _memberStoreService.GetMemberStoreByMemberMainPartyId(memberMainPartyId);
            var storeModel = _storeService.GetStoreByMainPartyId(Convert.ToInt32(memberStore.StoreMainPartyId));

            if (Request.Files.Count > 0 && Request.Files[0].ContentLength > 0)
            {
                for (int i = 0; i < Request.Files.Count; i++)
                {

                    int storeCertificateId = 0;
                    if (i == 0)
                    {
                        var storeCertificate = new StoreCertificate();
                        storeCertificate.Active = true;
                        storeCertificate.RecordDate = DateTime.Now;
                        storeCertificate.UpdateDate = DateTime.Now;
                        storeCertificate.CertificateName = model.CeritificateName;
                        storeCertificate.Order = model.Order;
                        storeCertificate.MainPartyId = memberStore.StoreMainPartyId.Value;
                        _storeService.InsertStoreCertificate(storeCertificate);

                        storeCertificateId = storeCertificate.StoreCertificateId;
                        if (model.CertificateTypeId != 0 && model.CertificateTypeId != 99999)
                        {
                            var certificateTypeProduct = new CertificateTypeProduct
                            {
                                StoreCertificateId = storeCertificateId,
                                CertificateTypeId = model.CertificateTypeId
                            };
                            _certificateTypeService.InsertCertificateTypeProduct(certificateTypeProduct);
                        }
                        else
                        {
                            var certificateType = new CertificateType
                            {
                                Name = storeCertificate.CertificateName,
                                CreatedDate = DateTime.Now,
                                UpdatedDate = storeCertificate.UpdateDate,
                                Order = 0,
                                Active = false,
                                InsertedStoreMainPartyId = memberStore.StoreMainPartyId.Value
                            };
                            _certificateTypeService.InsertCertificateType(certificateType);

                            var certificateTypeProduct = new CertificateTypeProduct
                            {
                                StoreCertificateId = storeCertificateId,
                                CertificateTypeId = certificateType.CertificateTypeId
                            };
                            _certificateTypeService.InsertCertificateTypeProduct(certificateTypeProduct);
                        }
                    }
                    HttpPostedFileBase file = Request.Files[i];

                    if (ImageContentType.Any(x => x == file.ContentType) && file.ContentLength > 0)
                    {

                        string oldfile = file.FileName;
                        string mapPath = this.Server.MapPath(AppSettings.StoreCertificateImageFolder);
                        string uzanti = oldfile.Substring(oldfile.LastIndexOf("."), oldfile.Length - oldfile.LastIndexOf("."));
                        var fileName = Guid.NewGuid().ToString("N") + "_certificate";
                        string filename = fileName + uzanti;
                        var targetFile = new FileInfo(mapPath + filename);

                        if (targetFile.Exists)
                        {
                            fileName = Guid.NewGuid().ToString("N") + "_certificate";
                            filename = fileName + uzanti;
                        }

                        string storeBannerImageFileSavePath = mapPath + filename;
                        Request.Files[i].SaveAs(storeBannerImageFileSavePath);
                        List<string> thubmsizes = new List<string>();
                        thubmsizes.Add("500x800");
                        ImageProcessHelper.ImageResize(storeBannerImageFileSavePath, mapPath + fileName.Replace("_certificate", ""), thubmsizes);

                        var curPicture = new Picture()
                        {
                            PicturePath = filename,
                            ProductId = null,
                            MainPartyId = memberStore.StoreMainPartyId,
                            PictureName = String.Empty,
                            StoreImageType = (byte)StoreImageTypeEnum.StoreCertificate,
                            StoreCertificateId = storeCertificateId
                        };
                        _pictureService.InsertPicture(curPicture);
                    }
                }
                return RedirectToAction("Certificate");
            }
            else
            {
                CreateStoreCertificateModel modelNew = new CreateStoreCertificateModel();
                modelNew.LeftMenu = LeftMenuConstants.CreateLeftMenuModel(LeftMenuConstants.GroupName.StoreSettings, (byte)LeftMenuConstants.StoreSettingGeneralInfo.StoreCertificate);
                var certificateTypes = _certificateTypeService.GetCertificateTypes().Where(x => x.Active == true).ToList();
                for (int i = 0; i < certificateTypes.Count; i++)
                {
                    var certificateItem = certificateTypes[i];
                    model.CertificateTypes.Add(new SelectListItem
                    {
                        Text = certificateItem.Name,
                        Value = certificateItem.CertificateTypeId.ToString()
                    });
                }
                modelNew.CertificateTypes.Add(new SelectListItem { Text = "Diğer", Value = "99999" });
                ModelState.AddModelError("CertificateImages", "Sertifika İçin Fotoğraf Seçmelisiniz");
                return View(modelNew);
            }


        }
        public ActionResult Certificate()
        {
            int memberMainPartyId = AuthenticationUser.CurrentUser.Membership.MainPartyId;
            var memberStore = _memberStoreService.GetMemberStoreByMemberMainPartyId(memberMainPartyId);

            var storeCertificates = _storeService.GetStoreCertificatesByMainPartyId(memberStore.StoreMainPartyId.Value);
            var model = new MTStoreCertificateModel();
            foreach (var item in storeCertificates)
            {
                var pictures = _pictureService.GetPictureByStoreCertificateId(item.StoreCertificateId);
                MTStoreCertificateItemModel certificateModel = new MTStoreCertificateItemModel();
                certificateModel.CertificateName = item.CertificateName;
                certificateModel.StoreCertificateId = item.StoreCertificateId;
                foreach (var picture in pictures)
                {
                    certificateModel.PhotoPaths.Add(AppSettings.StoreCertificateImageFolder + picture.PicturePath.Replace("_certificate", "-500x800"));
                }
                model.StoreCertificateItemModels.Add(certificateModel);
            }
            model.LeftMenu = LeftMenuConstants.CreateLeftMenuModel(LeftMenuConstants.GroupName.StoreSettings, (byte)LeftMenuConstants.StoreSettingGeneralInfo.StoreCertificate);
            return View(model);
        }

        [HttpGet]
        public JsonResult CertificateInfo(int certificateId)
        {
            var memberStore = _memberStoreService.GetMemberStoreByMemberMainPartyId(AuthenticationUser.Membership.MainPartyId);


            var certificate = _storeService.GetStoreCertificateByStoreCertificateId(certificateId);

            List<SelectListItem> certificateList = new List<SelectListItem>();
            var certificateTypes = _certificateTypeService.GetCertificateTypes();
            var certificateTypeStore = _certificateTypeService.GetCertificateTypeProductsByStoreCertificateId(certificateId);
            string certificateOptions = "";
            if (certificateTypeStore == null)
                certificateOptions = "<option value = '0' selected>Seçiniz</option>";
            foreach (var item in certificateTypes)
            {
                if (memberStore.StoreMainPartyId == item.InsertedStoreMainPartyId || item.Active == true)
                {
                    string selected = "";
                    if (certificateTypeStore != null && certificateTypeStore.Where(x => x.CertificateTypeId == item.CertificateTypeId).Count() > 0)
                        selected = "selected";

                    certificateOptions = certificateOptions + string.Format("<option value='{0}' {1}>{2}</option>", item.CertificateTypeId, selected, item.Name);
                }
            }

            return Json(new { Order = certificate.Order, Name = certificate.CertificateName, CertificateOptions = certificateOptions }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult UpdateCertificate(int certificateId, string name, int order, string certificateType)
        {
            var certificate = _storeService.GetStoreCertificateByStoreCertificateId(certificateId);
            certificate.CertificateName = name;
            certificate.Order = order;
            certificate.UpdateDate = DateTime.Now;
            _storeService.UpdateStoreCertificate(certificate);
            if (certificateType != "0")
            {
                var certificateTypeStore = _certificateTypeService.GetCertificateTypeProductsByStoreCertificateId(certificateId).FirstOrDefault();
                if (certificateTypeStore == null)
                {
                    certificateTypeStore = new CertificateTypeProduct
                    {
                        CertificateTypeId = Convert.ToInt32(certificateType),
                        StoreCertificateId = certificateId,

                    };
                    _certificateTypeService.InsertCertificateTypeProduct(certificateTypeStore);
                }
                certificateTypeStore.CertificateTypeId = Convert.ToInt32(certificateType);
                _certificateTypeService.UpdateCertificateTypeProduct(certificateTypeStore);
            }
            return Json(true, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult DeleteCertificate(int id)
        {


            var certificate = _storeService.GetStoreCertificateByStoreCertificateId(id);
            List<int> ids = new List<int>();
            ids.Add(id);

            var certificateTypes = _certificateTypeService.GetCertificatesByIds(ids);
            foreach (var item in certificateTypes)
            {
                _certificateTypeService.DeleteCertificateType(item);

            }

            var certificateTypeProducts = _certificateTypeService.GetCertificateTypeProductsByStoreCertificateId(id);
            foreach (var certificateTypeProduct in certificateTypeProducts)
            {
                _certificateTypeService.DeleteCertificateTypeProduct(certificateTypeProduct);

            }


            var pictures = _pictureService.GetPictureByStoreCertificateId(id);
            foreach (var item in pictures)
            {
                if (System.IO.File.Exists(this.Server.MapPath(AppSettings.StoreCertificateImageFolder) + item.PicturePath.Replace("_certificate", "-500x800")))
                {
                    FileHelpers.Delete(AppSettings.StoreCertificateImageFolder + item.PicturePath.Replace("_certificate", "-800x300"));
                }
                if (System.IO.File.Exists(this.Server.MapPath(AppSettings.StoreCertificateImageFolder) + item.PicturePath))
                {

                    FileHelpers.Delete(AppSettings.StoreCertificateImageFolder + item.PicturePath);
                }
                _pictureService.DeletePicture(item);
            }
            _storeService.DeleteStoreCertificate(certificate);
            return Json(true, JsonRequestBehavior.AllowGet);
        }
        public ActionResult CreateStoreSliderImage()
        {
            int mainPartyId = AuthenticationUser.Membership.MainPartyId;
            var memberStore = _memberStoreService.GetMemberStoreByMemberMainPartyId(mainPartyId);
            var model = new StoreImagesModel();
            model.LeftMenu = LeftMenuConstants.CreateLeftMenuModel(LeftMenuConstants.GroupName.StoreSettings, (byte)LeftMenuConstants.StoreSettingGeneralInfo.StoreSliderImage);

            //var categoryHelps = _categoryService.GetCategoryPlaceChoiceByCategoryPlaceTypeByIsProduct((byte)HelpCategoryPlace.StoreLogoUpdate, false);
            //foreach (var item in categoryHelps)
            //{
            //    string url = UrlBuilder.GetHelpCategoryUrl(item.CategoryId, item.Category.CategoryName);
            //    model.HelpList.Add(new MTHelpModeltem { Url = url, HelpCategoryName = item.Category.CategoryName });
            //}

            var storeImages = _pictureService.GetPictureByMainPartyIdWithStoreImageType(memberStore.StoreMainPartyId.Value, StoreImageTypeEnum.StoreProfileSliderImage);
            foreach (var storeImageItem in storeImages)
            {
                model.StoreImageItems.Add(new StoreImageItem { ImageId = storeImageItem.PictureId, ImagePath = AppSettings.StoreSliderImageFolder + storeImageItem.PicturePath });

            }
            return View(model);
        }
        [HttpPost]
        public ActionResult CreateStoreSliderImage(FormCollection frm)
        {

            string[] ImageContentType = { "image/bmp", "image/cis-cod", "image/gif", "image/ief", "image/jpeg", "image/jpg",
                                                "image/jpeg", "image/pipeg", "image/svg+xml", "image/tiff", "image/tiff",
                                                "image/x-cmu-raster", "image/x-cmx", "image/x-icon", "image/x-portable-anymap",
                                                "image/x-portable-bitmap", "image/x-portable-graymap", "image/x-portable-pixmap",
                                                "image/x-rgb", "image/x-xbitmap", "image/x-xpixmap", "image/x-xwindowdump",
                                                "image/pjpeg", "image/png", "image/x-png" };

            int memberMainPartyId = Convert.ToInt32(AuthenticationUser.Membership.MainPartyId);

            var memberStore = _memberStoreService.GetMemberStoreByMemberMainPartyId(memberMainPartyId);
            var storeModel = _storeService.GetStoreByMainPartyId(Convert.ToInt32(memberStore.StoreMainPartyId));
            var storeImages = _pictureService.GetPictureByMainPartyId(storeModel.MainPartyId).Where(x => x.StoreImageType == (byte)StoreImageType.StoreProfileSliderImage);
            int storeImagesCount = storeImages.ToList().Count;
            if (storeImages.ToList().Count < 8)
            {

                for (int i = 0; i < Request.Files.Count; i++)
                {

                    HttpPostedFileBase file = Request.Files[i];
                    if (ImageContentType.Any(x => x == file.ContentType) && file.ContentLength > 0)
                    {

                        string oldfile = file.FileName;
                        string mapPath = this.Server.MapPath(AppSettings.StoreSliderImageFolder);
                        string uzanti = oldfile.Substring(oldfile.LastIndexOf("."), oldfile.Length - oldfile.LastIndexOf("."));
                        string filename = Guid.NewGuid().ToString("N") + "_slider" + uzanti;
                        var targetFile = new FileInfo(mapPath + filename);

                        if (targetFile.Exists)
                        {
                            filename = Guid.NewGuid().ToString("N") + "_slider" + uzanti;
                        }

                        string storeBannerImageFileSavePath = mapPath + filename;
                        Request.Files[i].SaveAs(storeBannerImageFileSavePath);
                        Image img = ImageProcessHelper.resizeImageBanner(800, 300, storeBannerImageFileSavePath);
                        ImageProcessHelper.SaveJpeg(storeBannerImageFileSavePath, img, 80, "_slider", "-800x300");

                        var curPicture = new Picture()
                        {
                            PicturePath = filename,
                            ProductId = null,
                            MainPartyId = memberStore.StoreMainPartyId,
                            PictureName = String.Empty,
                            PictureOrder = storeImagesCount,
                            StoreImageType = (byte)StoreImageType.StoreProfileSliderImage
                        };

                        _pictureService.InsertPicture(curPicture);
                    }

                }
            }
            return RedirectToAction("createstoresliderimage");
        }
        [HttpPost]
        public JsonResult DeleteImage(int id)
        {
            var picture = _pictureService.GetPictureByPictureId(id);
            if (picture != null)
            {
                if (System.IO.File.Exists(this.Server.MapPath(AppSettings.StoreSliderImageFolder) + picture.PicturePath))
                {
                    // kullanıcı yeni üye olduğu zamanki resimleri sil.
                    FileHelpers.Delete(AppSettings.StoreBannerFolder + picture.PicturePath);
                }
                if (System.IO.File.Exists(this.Server.MapPath(AppSettings.StoreSliderImageFolder) + picture.PicturePath.Replace("_slider", "-800x300")))
                {
                    // kullanıcı yeni üye olduğu zamanki resimleri sil.
                    FileHelpers.Delete(AppSettings.StoreBannerFolder + picture.PicturePath.Replace("_slider", "-800x300"));
                }
                _pictureService.DeletePicture(picture);
                return Json(true, JsonRequestBehavior.AllowGet);
            }
            return Json(false, JsonRequestBehavior.AllowGet);

        }
        [HttpPost]
        public JsonResult DeleteBanner(int id)
        {

            var store = _storeService.GetStoreByMainPartyId(id);
            if (System.IO.File.Exists(this.Server.MapPath(AppSettings.StoreBannerFolder) + store.StoreBanner))
            {
                // kullanıcı yeni üye olduğu zamanki resimleri sil.
                FileHelpers.Delete(AppSettings.StoreBannerFolder + store.StoreBanner);
            }
            if (System.IO.File.Exists(this.Server.MapPath(AppSettings.StoreBannerFolder) + store.StoreBanner.Replace("_banner", "-1400x280")))
            {
                // kullanıcı yeni üye olduğu zamanki resimleri sil.
                FileHelpers.Delete(AppSettings.StoreBannerFolder + store.StoreBanner.Replace("_banner", "-1400x280"));
            }
            store.StoreBanner = null;
            _storeService.UpdateStore(store);
            return Json(true, JsonRequestBehavior.AllowGet);
        }

        public ActionResult UpdateContact()
        {
            int mainPartyId = AuthenticationUser.Membership.MainPartyId;
            if (AuthenticationUser.Membership.MemberType == (byte)MemberType.Enterprise)
            {
                mainPartyId = _memberStoreService.GetMemberStoreByMemberMainPartyId(mainPartyId).StoreMainPartyId.Value;
            }

            var address = _addressService.GetFisrtAddressByMainPartyId(mainPartyId);
            var phone = _phoneService.GetPhonesByMainPartyId(mainPartyId);

            var model = new AddressModel();
            model.PhoneItems = phone;

            IList<Locality> localityItems = new List<Locality>() { new Locality { LocalityId = 0, LocalityName = "< Lütfen Seçiniz >" } };
            IList<Town> townItems = new List<Town>() { new Town { TownId = 0, TownName = "< Lütfen Seçiniz >" } };

            if (address != null)
            {
                model.Street = address.Street;
                model.Avenue = address.Avenue;
                model.DoorNo = address.DoorNo;
                model.ApartmentNo = address.ApartmentNo;

                if (address.CityId.HasValue)
                {
                    model.CityId = address.CityId.Value;
                    localityItems = _addressService.GetLocalitiesByCityId(model.CityId);
                    localityItems.Insert(0, new Locality { LocalityId = 0, LocalityName = "< Lütfen Seçiniz >" });
                }

                if (address.LocalityId.HasValue)
                {
                    model.LocalityId = address.LocalityId.Value;
                    townItems = _addressService.GetTownsByLocalityId(address.LocalityId.Value);
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

            model.CountryItems = new SelectList(_addressService.GetAllCountries(), "CountryId", "CountryName", 0);
            model.CountryId = AppSettings.Turkiye;

            var cityItems = _addressService.GetCitiesByCountryId(model.CountryId);
            cityItems.Insert(0, new City { CityId = 0, CityName = "< Lütfen Seçiniz >" });

            model.CityItems = new SelectList(cityItems, "CityId", "CityName", 0);
            model.LocalityItems = new SelectList(localityItems, "LocalityId", "LocalityName");
            model.TownItems = new SelectList(townItems, "TownId", "TownName");

            return View(model);
        }

        [HttpPost]
        public ActionResult UpdateContact(AddressModel model, bool Redirect)
        {
            int mainPartyId = AuthenticationUser.Membership.MainPartyId;
            if (AuthenticationUser.Membership.MemberType == (byte)MemberType.Enterprise)
            {
                mainPartyId = _memberStoreService.GetMemberStoreByMemberMainPartyId(mainPartyId).StoreMainPartyId.Value;
            }

            bool hasAddress = false;
            var address = _addressService.GetFisrtAddressByMainPartyId(mainPartyId);
            if (address != null)
            {
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
            address.Avenue = model.Avenue;
            address.ApartmentNo = model.ApartmentNo;

            if (hasAddress)
            {
                _addressService.UpdateAddress(address);
            }
            else
            {
                _addressService.InsertAdress(address);
            }

            var phoneItems = _phoneService.GetPhonesByMainPartyId(mainPartyId);
            if (phoneItems != null)
            {
                foreach (var item in phoneItems)
                {
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
                var phoneGsm = new Phone
                {
                    MainPartyId = mainPartyId,
                    PhoneAreaCode = model.InstitutionalGSMAreaCode,
                    PhoneCulture = model.InstitutionalGSMCulture,
                    PhoneNumber = model.InstitutionalGSMNumber,
                    PhoneType = (byte)PhoneType.Gsm,
                    GsmType = model.GsmType
                };
                _phoneService.InsertPhone(phoneGsm);
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

            if (Redirect)
                return RedirectToAction("UpdateStore", "Store");
            else
                return RedirectToAction("Index", "Store");
        }

        public ActionResult UpdateStore()
        {
            int mainPartyId = AuthenticationUser.Membership.MainPartyId;

            var model = new StoreModel();

            var memberStore = _memberStoreService.GetMemberStoreByMemberMainPartyId(mainPartyId);
            var store = _storeService.GetStoreByMainPartyId(memberStore.StoreMainPartyId.Value);

            model.StoreActivityItems = _storeActivityTypeService.GetStoreActivityTypesByStoreId(store.MainPartyId);
            model.ActivityItems = _activityTypeService.GetAllActivityTypes();
            if (string.IsNullOrEmpty(store.StoreProfileHomeDescription))
            {
                var constant = _constantService.GetConstantByConstantType(ConstantTypeEnum.StoreProfileHomeDecriptionTemplate).FirstOrDefault();
                store.StoreProfileHomeDescription = constant.ContstantPropertie;
            }
            model.Store = store;
            model.LeftMenu = LeftMenuConstants.CreateLeftMenuModel(LeftMenuConstants.GroupName.StoreSettings, (byte)LeftMenuConstants.StoreSettingGeneralInfo.PromotionInformUpdate);

            return View(model);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult UpdateStore(StoreModel model, string[] ActivityName)
        {
            int mainPartyId = AuthenticationUser.Membership.MainPartyId;
            var memberStore = _memberStoreService.GetMemberStoreByMemberMainPartyId(mainPartyId);
            mainPartyId = memberStore.StoreMainPartyId.Value;

            bool storeChange = false;
            var store = _storeService.GetStoreByMainPartyId(memberStore.StoreMainPartyId.Value);
            //if (store.StoreName != model.Store.StoreName)
            //    storeChange = true;
            if (store.StoreWeb != model.Store.StoreWeb)
                storeChange = true;
            if (store.StoreCapital != model.Store.StoreCapital)
                storeChange = true;
            storeChange = store.StoreEstablishmentDate != model.Store.StoreEstablishmentDate ? true : storeChange;
            storeChange = store.StoreEmployeesCount != model.Store.StoreEmployeesCount ? true : storeChange;
            storeChange = store.StoreEndorsement != model.Store.StoreEndorsement ? true : storeChange;
            storeChange = store.StoreType != model.Store.StoreType ? true : storeChange;
            storeChange = store.StoreProfileHomeDescription != model.Store.StoreProfileHomeDescription ? true : storeChange;
            if (storeChange)
                _storeChangeHistoryService.AddStoreChangeHistoryForStore(store);
            //store.StoreName = model.Store.StoreName;
            store.StoreWeb = model.Store.StoreWeb;
            store.StoreCapital = model.Store.StoreCapital;
            store.StoreEstablishmentDate = model.Store.StoreEstablishmentDate;
            store.StoreEmployeesCount = model.Store.StoreEmployeesCount;
            store.StoreEndorsement = model.Store.StoreEndorsement;
            store.StoreType = model.Store.StoreType;
            store.StoreAbout = model.Store.StoreAbout;
            //store.StoreProfileHomeDescription = model.Store.StoreProfileHomeDescription;
            store.StoreType = model.Store.StoreType;
            store.StoreShortName = model.Store.StoreShortName;
            _storeService.UpdateStore(store);
            if (ActivityName != null && ActivityName.Length > 0)
            {
                var storeAcTypes = _storeActivityTypeService.GetStoreActivityTypesByStoreId(mainPartyId);

                foreach (var item in storeAcTypes)
                {
                    _storeActivityTypeService.DeleteStoreActivityType(item);
                }

                for (int i = 0; i < ActivityName.Length; i++)
                {
                    if (ActivityName.GetValue(i).ToString() != "false")
                    {
                        var storeActivity = new StoreActivityType
                        {
                            ActivityTypeId = ActivityName.GetValue(i).ToByte(),
                            StoreId = mainPartyId,
                        };
                        _storeActivityTypeService.InsertStoreActivityType(storeActivity);
                    }
                }
            }
            return RedirectToAction("UpdateStore", "Store");
        }

        public ActionResult UpdateActivity()
        {
            var model = new MemberModel();

            int mainPartyId = AuthenticationUser.Membership.MainPartyId;
            var memberStore = _memberStoreService.GetMemberStoreByMemberMainPartyId(mainPartyId);
            mainPartyId = memberStore.StoreMainPartyId.Value;
            //var store = _storeService.GetStoreByMainPartyId(mainPartyId);

            var sectorItems = _categoryService.GetCategoriesByCategoryType(CategoryTypeEnum.Sector);

            var storeActivityCategory = _storeActivityCategoryService.GetStoreActivityCategoriesByMainPartyId(mainPartyId);

            model.CategoryItems = sectorItems;
            model.StoreActivityCategory = storeActivityCategory;
            model.LeftMenu = LeftMenuConstants.CreateLeftMenuModel(LeftMenuConstants.GroupName.StoreSettings, (byte)LeftMenuConstants.StoreSettingGeneralInfo.ActivityAreasChange);

            return View(model);
        }
        [HttpPost]
        public ActionResult UpdateActivity(string[] StoreActivityCategory)
        {
            if (StoreActivityCategory != null)
            {
                int mainPartyId = AuthenticationUser.Membership.MainPartyId;

                var storeMainPartyId = _memberStoreService.GetMemberStoreByMemberMainPartyId(mainPartyId).StoreMainPartyId.Value;
                var storeAcCategory = _storeActivityCategoryService.GetStoreActivityCategoriesByMainPartyId(storeMainPartyId);
                if (storeAcCategory != null && storeAcCategory.Count() > 0)
                {
                    foreach (var item in storeAcCategory)
                    {
                        _storeActivityCategoryService.DeleteStoreActivityCategory(item);
                    }
                }

                for (int i = 0; i < StoreActivityCategory.Length; i++)
                {
                    if (StoreActivityCategory.GetValue(i).ToString() != "false")
                    {
                        var storeActivityType = new global::MakinaTurkiye.Entities.Tables.Stores.StoreActivityCategory
                        {
                            MainPartyId = storeMainPartyId,
                            CategoryId = StoreActivityCategory.GetValue(i).ToInt32()
                        };
                        _storeActivityCategoryService.InsertStoreActivityCategory(storeActivityType);
                    }
                }
            }
            return RedirectToAction("UpdateActivity", "Store");
        }

        public ActionResult UpdateAuthorized()
        {
            var member = _memberService.GetMemberByMainPartyId(AuthenticationUser.Membership.MainPartyId);
            var model = new MemberModel();
            model.Member = member;
            if (member.BirthDate.HasValue)
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

            return View(model);
        }

        [HttpPost]
        public ActionResult UpdateAuthorized(Member model, FormCollection coll)
        {
            var member = _memberService.GetMemberByMainPartyId(AuthenticationUser.Membership.MainPartyId);

            member.MemberName = model.MemberName;
            member.MemberSurname = model.MemberSurname;
            member.MemberTitleType = model.MemberTitleType;
            member.BirthDate = model.BirthDate;

            if (coll["Gender"] == "2")
            {
                member.Gender = true;
            }
            else
            {
                member.Gender = false;
            }

            _memberService.UpdateMember(member);

            return RedirectToAction("UpdateAuthorized", "Store");
        }

        [ChildActionOnly]
        public ActionResult _HeaderStoreCategoryGeneral()
        {

            MTStoreCategoryModel model = new MTStoreCategoryModel();
            var storeCategories = _categoryService.GetSPStoreCategoryByCategoryParentId(0).Where(x => x.CategoryType != (byte)CategoryType.Brand).ToList();
            IList<MTStoreCategoryItemModel> videoCategoryItemModels = new List<MTStoreCategoryItemModel>();
            foreach (var item in storeCategories)
            {

                var itemModel = new MTStoreCategoryItemModel
                {
                    CategoryUrl = UrlBuilder.GetStoreCategoryUrl(item.CategoryId, FormatHelper.GetCategoryNameWithSynTax(item.CategoryName, CategorySyntaxType.Store)),
                    CategoryType = item.CategoryType
                };

                if (item.CategoryType == (byte)CategoryTypeEnum.ProductGroup)
                {
                    var lastCategory = model.StoreTopCategoryItemModels.LastOrDefault();
                    string productGrupCategoryName = string.Format("{0}", item.CategoryName);
                    itemModel.CategoryName = FormatHelper.GetCategoryNameWithSynTax(productGrupCategoryName,
                   CategorySyntaxType.Store);

                    //var subCategories = _storeService.GetSPStoreCategoryByCategoryParentId(item.CategoryId);
                    //foreach (var subItem in subCategories)
                    //{
                    //    itemModel.SubStoreCategoryItemModes.Add(new MTStoreCategoryItemModel
                    //    {
                    //        CategoryName = FormatHelper.GetCategoryNameWithSynTax(subItem.CategoryName, CategorySyntaxType.Store),
                    //        CategoryUrl = UrlBuilder.GetStoreCategoryUrl(subItem.CategoryId, FormatHelper.GetCategoryNameWithSynTax(subItem.CategoryName, CategorySyntaxType.Store), selectedOrderby),
                    //        CategoryType = subItem.CategoryType,
                    //        CategoryId = subItem.CategoryId
                    //    });
                    //}
                }
                //else if (item.CategoryType == (byte)CategoryTypeEnum.Brand)
                //{
                //    int topCategoryCount = model.StoreCategoryModel.StoreTopCategoryItemModels.Count;
                //    var elementCategory = model.StoreCategoryModel.StoreTopCategoryItemModels.ElementAt(topCategoryCount - 1);
                //    string brandCategoryName = string.Format("{0}", item.CategoryName);
                //    itemModel.CategoryName = FormatHelper.GetCategoryNameWithSynTax(brandCategoryName, CategorySyntaxType.Store);
                //    itemModel.CategoryUrl = UrlBuilder.GetStoreCategoryUrl(item.CategoryId, FormatHelper.GetCategoryNameWithSynTax(brandCategoryName, CategorySyntaxType.Store), selectedOrderby);
                //}

                else if (item.CategoryType == (byte)CategoryTypeEnum.Model)
                {
                    int topCategoryCount = model.StoreTopCategoryItemModels.Count;
                    var elementCategory = model.StoreTopCategoryItemModels.ElementAt(topCategoryCount - 1);
                    string modelCategoryName = string.Format("{0}", item.CategoryName);
                    itemModel.CategoryName = FormatHelper.GetCategoryNameWithSynTax(modelCategoryName, CategorySyntaxType.Store);
                    itemModel.CategoryUrl = UrlBuilder.GetStoreCategoryUrl(item.CategoryId, FormatHelper.GetCategoryNameWithSynTax(modelCategoryName, CategorySyntaxType.Store));
                }
                else
                {
                    itemModel.CategoryName = FormatHelper.GetCategoryNameWithSynTax(item.CategoryName, CategorySyntaxType.Store);
                    itemModel.CategoryUrl = UrlBuilder.GetStoreCategoryUrl(item.CategoryId, FormatHelper.GetCategoryNameWithSynTax(item.CategoryName, CategorySyntaxType.Store));
                }
                if (itemModel.CategoryType != 5)
                {
                    videoCategoryItemModels.Add(itemModel);
                }
            }

            model.StoreCategoryItemModels = videoCategoryItemModels;
            return PartialView(model);
        }

        public ActionResult MyCatologList()
        {
            MTStoreCatologViewModel model = new MTStoreCatologViewModel();

            model.LeftMenuModel = LeftMenuConstants.CreateLeftMenuModel(LeftMenuConstants.GroupName.StoreSettings, (byte)LeftMenuConstants.StoreSettingGeneralInfo.StoreCatolog);
            var memberStore = _memberStoreService.GetMemberStoreByMemberMainPartyId(AuthenticationUser.CurrentUser.Membership.MainPartyId);
            var catologList = _storeCatologFileService.StoreCatologFilesByStoreMainPartyId(memberStore.StoreMainPartyId.Value).OrderBy(x => x.FileOrder).ThenByDescending(x => x.StoreCatologFileId);
            foreach (var item in catologList)
            {
                var filePath = FileUrlHelper.GetStoreCatologUrl(item.FileName, memberStore.StoreMainPartyId.Value);
                model.MTCatologItems.Add(new Models.Stores.MTCatologItem { CatologId = item.StoreCatologFileId, FileOrder = item.FileOrder, FilePath = filePath, Name = item.Name });
            }
            return View(model);
        }


        public ActionResult CreateCatolog()
        {
            MTCreateCatologViewModel model = new MTCreateCatologViewModel();
            model.LeftMenu = LeftMenuConstants.CreateLeftMenuModel(LeftMenuConstants.GroupName.StoreSettings, (byte)LeftMenuConstants.StoreSettingGeneralInfo.StoreCatolog);

            return View(model);
        }
        [HttpPost]
        public ActionResult CreateCatolog(MTCreateCatologViewModel model)
        {
            MTCreateCatologViewModel modelView = new MTCreateCatologViewModel();
            modelView.LeftMenu = LeftMenuConstants.CreateLeftMenuModel(LeftMenuConstants.GroupName.StoreSettings, (byte)LeftMenuConstants.StoreSettingGeneralInfo.StoreCatolog);

            if (model.CreateCatologForm.FilePaths.Count() == 0)
            {

                ModelState.AddModelError("CreateCatologForm.FilePath", "Lütfen dosya seçiniz");

            }
            else
            {
                string[] ImageContentTypes = { "application/pdf", "application/msword" };

                var mainPartyId = AuthenticationUser.CurrentUser.Membership.MainPartyId;
                int storeMainPartyId = _memberStoreService.GetMemberStoreByMemberMainPartyId(mainPartyId).StoreMainPartyId.Value;
                var store = _storeService.GetStoreByMainPartyId(storeMainPartyId);
                string newFileName = !string.IsNullOrEmpty(model.CreateCatologForm.CatologName) ? model.CreateCatologForm.CatologName : store.StoreName;
                int counter = 0;
                if (model.CreateCatologForm.FilePaths.Count > 1)
                {
                    counter = 1;
                }
                if (model.CreateCatologForm.FilePaths.Where(x => !ImageContentTypes.Contains(x.ContentType)).Count() > 0)
                {
                    ModelState.AddModelError("CreateCatologForm.FilePath", "Seçtiğiniz dosya formatı pdf veya doc olmalı.");
                }
                else
                {
                    foreach (var file in model.CreateCatologForm.FilePaths)
                    {
                        string filePath = FileUploadHelper.UploadFile(file, AppSettings.StoreCatologFolder + "/" + store.MainPartyId.ToString(), newFileName, counter);
                        var storeCatologFile = new global::MakinaTurkiye.Entities.Tables.Stores.StoreCatologFile();
                        storeCatologFile.FileName = filePath;
                        storeCatologFile.Name = model.CreateCatologForm.CatologName;
                        storeCatologFile.StoreMainPartyId = store.MainPartyId;
                        storeCatologFile.FileOrder = 0;
                        _storeCatologFileService.InsertStoreCatologFile(storeCatologFile);
                    }
                    TempData["success"] = "basarili";
                    return RedirectToAction("CreateCatolog");
                }
            }

            return View(modelView);

        }
        [HttpPost]
        public JsonResult DeleteCatolog(int id)
        {
            var catolog = _storeCatologFileService.GetStoreCatologFileByCatologId(id);
            var filePath = FileUrlHelper.GetStoreCatologUrl(catolog.FileName, catolog.StoreMainPartyId);
            FileHelpers.Delete(AppSettings.StoreCatologFolder + "/" + catolog.StoreMainPartyId + "/" + catolog.FileName);
            _storeCatologFileService.DeleteStoreCatologFile(catolog);
            return Json(true, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult GetCatologInfo(int id)
        {
            var catolog = _storeCatologFileService.GetStoreCatologFileByCatologId(id);
            MTCatologItem item = new MTCatologItem();
            item.CatologId = catolog.StoreCatologFileId;
            item.Name = catolog.Name;
            item.FileOrder = catolog.FileOrder;
            return Json(item, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult UpdateCatolog(MTCatologItem model)
        {
            var catolog = _storeCatologFileService.GetStoreCatologFileByCatologId(model.CatologId);
            int fileOrder = 0;
            if (model.FileOrder.HasValue)
                fileOrder = model.FileOrder.Value;
            catolog.FileOrder = fileOrder;
            catolog.Name = model.Name;
            _storeCatologFileService.UpdateStoreCatologFile(catolog);
            return Json(true, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ContactSettings()
        {
            MTContactSettingsModel model = new MTContactSettingsModel();
            int mainPartyId = AuthenticationUser.CurrentUser.Membership.MainPartyId;
            int storeMainPartyId = _memberStoreService.GetMemberStoreByMemberMainPartyId(mainPartyId).StoreMainPartyId.Value;
            var phones = _phoneService.GetPhonesByMainPartyId(storeMainPartyId);
            if (phones.Count == 0)
                phones = _phoneService.GetPhonesByMainPartyId(mainPartyId);
            foreach (var item in phones)
            {

                model.MTSettingItems.Add(PrepareMTSettingItemModel(item, storeMainPartyId));
            }
            model.LeftMenu = LeftMenuConstants.CreateLeftMenuModel(LeftMenuConstants.GroupName.StoreSettings, (byte)LeftMenuConstants.StoreSettingGeneralInfo.ContactSettings);
            return View(model);
        }

        public MTSettingItemModel PrepareMTSettingItemModel(global::MakinaTurkiye.Entities.Tables.Common.Phone phone, int storeMainPartyId)
        {

            string phoneTypeText = "";
            switch (phone.PhoneType)
            {
                case (byte)PhoneType.Gsm:
                    phoneTypeText = "Cep Telefon";
                    break;
                case (byte)PhoneType.Phone:
                    phoneTypeText = "İş telefonu";
                    break;
                case (byte)PhoneType.Fax:
                    phoneTypeText = "Fax";
                    break;

                default:
                    phoneTypeText = "Whatsapp";
                    break;
            }
            string name = GetEnumValue<PhoneType>(Convert.ToByte(phone.PhoneType.Value)).ToString();
            var setting = _memberSettingService.GetMemberSettingsBySettingNameWithStoreMainPartyId(name, storeMainPartyId).FirstOrDefault();
            var settingModel = new MTSettingItemModel
            {
                PhoneId = phone.PhoneId,
                StoreMainPartyId = storeMainPartyId,
                PhoneNumber = phone.PhoneCulture + " " + phone.PhoneAreaCode + " " + phone.PhoneNumber,
                PhoneTypeText = phoneTypeText,
                StartTime = "",
                EndTime = ""
            };
            if (setting != null)
            {
                if (!string.IsNullOrEmpty(setting.SecondValue))
                {
                    string[] weekendWorking = setting.SecondValue.Split('-');
                    settingModel.SaturdayWorking = false;
                    settingModel.SundayWorking = false;
                    if (weekendWorking[0] == "1")
                        settingModel.SaturdayWorking = true;
                    if (weekendWorking[1] == "1")
                        settingModel.SundayWorking = true;
                }
                settingModel.StartTime = setting.FirstValue.Split('-')[0];
                settingModel.EndTime = setting.FirstValue.Split('-')[1];
            }

            return settingModel;

        }

        [HttpGet]
        public JsonResult GetPhoneSetting(int phoneId)
        {
            ResponseModel<MTSettingItemModel> response = new ResponseModel<MTSettingItemModel>();


            var phone = _phoneService.GetPhoneByPhoneId(phoneId);
            string name = GetEnumValue<PhoneType>(Convert.ToByte(phone.PhoneType.Value)).ToString();
            int mainPartyId = AuthenticationUser.CurrentUser.Membership.MainPartyId;
            int storeMainPartyId = _memberStoreService.GetMemberStoreByMemberMainPartyId(mainPartyId).StoreMainPartyId.Value;
            if (phone != null)
            {
                var settingItemModel = PrepareMTSettingItemModel(phone, storeMainPartyId);
                response.IsSuccess = true;
                response.Result = settingItemModel;

            }
            else
            {
                response.IsSuccess = false;
                response.Message = "Telefon bulunamadı";
            }
            return Json(response, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult ChangePhoneSettings(MTContactSettingItemFormModel model)
        {
            var phone = _phoneService.GetPhoneByPhoneId(model.PhoneId);
            var settingName = GetEnumValue<PhoneType>(Convert.ToByte(phone.PhoneType.Value)).ToString();
            var setting = _memberSettingService.GetSettingBySettingName(settingName);
            string name = GetEnumValue<PhoneType>((int)phone.PhoneType).ToString();
            var memberSetting = _memberSettingService.GetMemberSettingsBySettingNameWithStoreMainPartyId(name, model.StoreMainPartyId).FirstOrDefault();
            if (memberSetting != null)
            {
                if (model.AvaliableAlways)
                {
                    _memberSettingService.DeleteMemberSetting(memberSetting);
                }
                else
                {
                    memberSetting.FirstValue = model.StartTime + "-" + model.EndTime;
                    //memberSetting.SecondValue = model.EndTime;

                    string weekendWorking = "";
                    if (model.SaturdayWorking)
                        weekendWorking = "1";
                    else
                        weekendWorking = "0";

                    if (model.SundayWorking)
                    {
                        weekendWorking = weekendWorking + "-1";
                    }
                    else
                    {
                        weekendWorking = weekendWorking + "-0";
                    }
                    memberSetting.SecondValue = weekendWorking;
                    memberSetting.UpdateDate = DateTime.Now;
                    _memberSettingService.UpdateMemberSetting(memberSetting);
                }
            }
            else
            {
                memberSetting = new MemberSetting();
                memberSetting.SettingId = setting.SettingId;
                memberSetting.StoreMainPartyId = model.StoreMainPartyId;
                memberSetting.FirstValue = model.StartTime + "-" + model.EndTime;
                //memberSetting.SecondValue = model.EndTime;
                string weekendWorking = "";
                if (model.SaturdayWorking)
                    weekendWorking = "1";
                else
                    weekendWorking = "0";

                if (model.SundayWorking)
                {
                    weekendWorking = weekendWorking + "-1";
                }
                else
                {
                    weekendWorking = weekendWorking + "-0";
                }
                memberSetting.SecondValue = weekendWorking;
                memberSetting.RecordDate = DateTime.Now;
                memberSetting.UpdateDate = DateTime.Now;
                _memberSettingService.InsertMemberSetting(memberSetting);
            }

            ResponseModel<string> resModel = new ResponseModel<string>();
            resModel.IsSuccess = true;
            return Json(resModel, JsonRequestBehavior.AllowGet);
        }


        public ActionResult Sector()
        {

            var sectoreCategories = _categoryService.GetMainCategories();
            MTStoreActivityModel model = new MTStoreActivityModel();
            var memberMainPartyId = AuthenticationUser.CurrentUser.Membership.MainPartyId;
            int storeMainPartyId = _memberStoreService.GetMemberStoreByMemberMainPartyId(memberMainPartyId).StoreMainPartyId.Value;
            var storeSectors = _storeSectorService.GetStoreSectorsByMainPartyId(storeMainPartyId);

            foreach (var item in sectoreCategories)
            {
                var itemCategory = new SelectListItem
                {
                    Value = item.CategoryId.ToString(),
                    Text = !string.IsNullOrEmpty(item.CategoryContentTitle) ? item.CategoryContentTitle : item.CategoryName
                };
                if (storeSectors.FirstOrDefault(x => x.CategoryId == item.CategoryId) != null)
                {
                    itemCategory.Selected = true;
                }
                model.Categories.Add(itemCategory);
            }
            model.LeftMenu = LeftMenuConstants.CreateLeftMenuModel(LeftMenuConstants.GroupName.StoreSettings, (byte)LeftMenuConstants.StoreSettingOtherInfo.StoreSector);
            foreach (var item in storeSectors)
            {
                var category = _categoryService.GetCategoryByCategoryId(item.CategoryId);
                model.StoreActivityCategories.Add(item.StoreSectorId, !string.IsNullOrEmpty(category.CategoryContentTitle) ? category.CategoryContentTitle : category.CategoryName);
            }
            return View(model);

        }
        [HttpPost]
        public ActionResult Sector(int[] subcategory)
        {
            var memberMainPartyId = AuthenticationUser.CurrentUser.Membership.MainPartyId;
            int storeMainPartyId = _memberStoreService.GetMemberStoreByMemberMainPartyId(memberMainPartyId).StoreMainPartyId.Value;
            var store = _storeService.GetStoreByMainPartyId(storeMainPartyId);
            var storeSectors = _storeSectorService.GetStoreSectorsByMainPartyId(storeMainPartyId);
            if (subcategory != null && subcategory.Length > 0)
            {
                foreach (var item in subcategory)
                {
                    if (!storeSectors.Any(x => x.CategoryId == item))
                    {
                        var storeSector = new StoreSector
                        {
                            CategoryId = item,
                            RecordDate = DateTime.Now,
                            StoreMainPartyId = storeMainPartyId,

                        };
                        _storeSectorService.InsertStoreSector(storeSector);

                    }
                }
                TempData["SuccessMessage"] = "Firma ilgili sektör kayıt edilmiştir.";
            }
            else
            {
                TempData["ErrorMessage"] = "Lütfen sektör seçiniz";
            }
            return RedirectToAction("Sector");
        }


        [HttpGet]
        public JsonResult DeleteSector(int storeSectorId)
        {
            var storeSector = _storeSectorService.GetStoreSectorByStoreSectorId(storeSectorId);
            if (storeSector != null)
            {
                _storeSectorService.DeleteStoreSector(storeSector);
            }
            return Json(true, JsonRequestBehavior.AllowGet);
        }

    }
}