using MakinaTurkiye.Entities.Tables.Stores;
using MakinaTurkiye.Services.Members;
using MakinaTurkiye.Services.Packets;
using MakinaTurkiye.Services.Stores;
using MakinaTurkiye.Utilities.Controllers;
using MakinaTurkiye.Utilities.HttpHelpers;
using MakinaTurkiye.Utilities.ImageHelpers;

using NeoSistem.MakinaTurkiye.Core.Web.Helpers;
using NeoSistem.MakinaTurkiye.Web.Areas.Account.Constants;
using NeoSistem.MakinaTurkiye.Web.Areas.Account.Models.StoreNews;
using NeoSistem.MakinaTurkiye.Web.Helpers;
using NeoSistem.MakinaTurkiye.Web.Models;
using NeoSistem.MakinaTurkiye.Web.Models.Authentication;
using NeoSistem.MakinaTurkiye.Web.Models.UtilityModel;

using System;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NeoSistem.MakinaTurkiye.Web.Areas.Account.Controllers
{
    public class StoreNewController : BaseAccountController
    {
        private readonly IStoreNewService _storeNewService;
        private readonly IStoreService _storeService;
        private readonly IPacketService _packetService;
        private readonly IMemberStoreService _memberStoreService;


        string[] ImageContentType = { "image/bmp", "image/cis-cod", "image/gif", "image/ief", "image/jpeg", "image/jpg",
                                                "image/jpeg", "image/pipeg", "image/svg+xml", "image/tiff", "image/tiff",
                                                "image/x-cmu-raster", "image/x-cmx", "image/x-icon", "image/x-portable-anymap",
                                                "image/x-portable-bitmap", "image/x-portable-graymap", "image/x-portable-pixmap",
                                                "image/x-rgb", "image/x-xbitmap", "image/x-xpixmap", "image/x-xwindowdump",
                                                "image/pjpeg", "image/png", "image/x-png" };


        public StoreNewController(IStoreNewService storeNewService, IStoreService storeService,
            IPacketService packetService, IMemberStoreService memberStoreService)
        {
            this._storeNewService = storeNewService;
            this._storeService = storeService;
            this._packetService = packetService;
            this._memberStoreService = memberStoreService;

            this._storeNewService.CachingGetOrSetOperationEnabled = false;
            this._storeService.CachingGetOrSetOperationEnabled = false;
            this._packetService.CachingGetOrSetOperationEnabled = false;
            this._memberStoreService.CachingGetOrSetOperationEnabled = false;

        }
        // GET: Account/StoreNew
        public ActionResult Index(byte newType)
        {
            if (AuthenticationUser.CurrentUser.Membership.MainPartyId != 0)
            {
                var memberStore = _memberStoreService.GetMemberStoreByMemberMainPartyId(AuthenticationUser.CurrentUser.Membership.MainPartyId);
                if (memberStore != null)
                {
                    var store = _storeService.GetStoreByMainPartyId(memberStore.StoreMainPartyId.Value);
                    var packet = _packetService.GetPacketByPacketId(store.PacketId);
                    if (packet.PacketPrice > 0)
                    {
                        var model = new MTStoreNewModel();
                        model.LeftMenu = LeftMenuConstants.CreateLeftMenuModel(LeftMenuConstants.GroupName.StoreSettings);
                        model.NewType = newType;
                        model.PageTitle = "Haberler";
                        if (newType == (byte)StoreNewType.SuccessStories)
                        {
                            model.PageTitle = "Başarı Hikayeleri";
                        }

                        var news = _storeNewService.GetStoreNewsByStoreMainPartyId(store.MainPartyId, (StoreNewTypeEnum)newType).OrderByDescending(x => x.StoreNewId);

                        foreach (var item in news)
                        {
                            model.MTStoreNews.Add(new MTStoreNewItem
                            {
                                Active = item.Active,
                                RecordDate = item.RecordDate,
                                ImagePath = ImageHelper.GetStoreNewImagePath(item.ImageName, StoreNewImageSize.px100x100.ToString()),
                                StoreNewId = item.StoreNewId,
                                Title = item.Title,
                                UpdateDate = item.UpdateDate,
                                NewUrl = UrlBuilder.GetStoreNewUrl(item.StoreNewId, item.Title),
                                ViewCount = item.ViewCount
                            });
                        }
                        return View(model);
                    }
                }
            }
            return RedirectToAction("index", "Home");

        }

        public ActionResult Create(byte newType)
        {
            if (AuthenticationUser.CurrentUser.Membership.MainPartyId != 0)
            {
                var memberStore = _memberStoreService.GetMemberStoreByMemberMainPartyId(AuthenticationUser.CurrentUser.Membership.MainPartyId);
                if (memberStore != null)
                {
                    var store = _storeService.GetStoreByMainPartyId(memberStore.StoreMainPartyId.Value);
                    var packet = _packetService.GetPacketByPacketId(store.PacketId);
                    if (packet.PacketPrice > 0)
                    {
                        MTCreateStoreNewForm model = new MTCreateStoreNewForm();
                        model.LeftMenu = LeftMenuConstants.CreateLeftMenuModel(LeftMenuConstants.GroupName.StoreSettings);
                        model.StoreMainPartyId = store.MainPartyId;
                        model.NewType = newType;
                        model.PageTitle = "Haber";
                        if (newType == (byte)StoreNewType.SuccessStories)
                            model.PageTitle = "Başarı Hikayesi";
                        return View(model);
                    }
                }
            }
            return RedirectToAction("index", "Home");
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult Create(MTCreateStoreNewForm model)
        {
            if (string.IsNullOrEmpty(model.Title) || string.IsNullOrEmpty(model.Content))
            {
                if (string.IsNullOrEmpty(model.Title))
                    ModelState.AddModelError("Title", "Başlık alanı boş geçilemez");
                if (string.IsNullOrEmpty(model.Content))
                {
                    ModelState.AddModelError("Content", "İçerik alanı boş geçilemez");
                }
                model.LeftMenu = LeftMenuConstants.CreateLeftMenuModel(LeftMenuConstants.GroupName.StoreSettings);
            }
            else
            {

                var storeNew = new StoreNew();
                storeNew.Title = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(LimitText(StringHelpers.ProductNameRegulator(model.Title).Trim(), 250).ToLower());
                storeNew.Content = model.Content;
                storeNew.RecordDate = DateTime.Now;
                storeNew.StoreMainPartyId = model.StoreMainPartyId;
                storeNew.Active = false;
                storeNew.UpdateDate = DateTime.Now;
                storeNew.ViewCount = 0;
                storeNew.NewType = model.NewType;
                _storeNewService.InsertStoreNew(storeNew);
                if (Request.Files.Count > 0)
                {
                    HttpPostedFileBase file = Request.Files[0];
                    string filename = "";
                    if (ImageContentType.Any(x => x == file.ContentType) && file.ContentLength > 0)
                    {

                        string oldfile = file.FileName;
                        string mapPath = this.Server.MapPath(AppSettings.StoreNewImageFolder);
                        string uzanti = oldfile.Substring(oldfile.LastIndexOf("."), oldfile.Length - oldfile.LastIndexOf("."));
                        filename = storeNew.Title.ToImageFileName() + storeNew.StoreNewId + "_haber" + uzanti;
                        var targetFile = new FileInfo(mapPath + filename);
                        string storeNewImage = mapPath + filename;
                        file.SaveAs(storeNewImage);

                        var imageizes = AppSettings.StoreNewImageSize.Split(';');
                        foreach (var item in imageizes)
                        {
                            int width = Convert.ToInt32(item.Split('x')[0]);
                            int height = Convert.ToInt32(item.Split('x')[1]);

                            Image img = ImageProcessHelper.resizeImageBanner(width, height, storeNewImage);
                            ImageProcessHelper.SaveJpeg(storeNewImage, img, 100, "_haber", "_" + item);
                        }

                    }
                    storeNew.ImageName = filename;
                    _storeNewService.UpdateStoreNew(storeNew);
                }
                TempData["success"] = "basarili";
                return RedirectToAction("Create", new { @newType = model.NewType });
            }

            return View(model);
        }
        public ActionResult Update(int id)
        {
            var storeNew = _storeNewService.GetStoreNewByStoreNewId(id);
            MTCreateStoreNewForm model = new MTCreateStoreNewForm();
            model.Content = storeNew.Content;
            model.LeftMenu = LeftMenuConstants.CreateLeftMenuModel(LeftMenuConstants.GroupName.StoreSettings); ;
            model.StoreMainPartyId = storeNew.StoreMainPartyId;
            model.StoreNewId = id;
            model.Title = storeNew.Title;
            model.NewType = storeNew.NewType;
            model.ImagePath = ImageHelper.GetStoreNewImagePath(storeNew.ImageName, StoreNewImageSize.px100x100.ToString());
            model.PageTitle = "Haber";
            if (model.NewType == (byte)StoreNewType.SuccessStories)
                model.PageTitle = "Başarı Hikayesi";
            return View(model);

        }
        [HttpPost, ValidateInput(false)]
        public ActionResult Update(MTCreateStoreNewForm model)
        {
            if (string.IsNullOrEmpty(model.Title) || string.IsNullOrEmpty(model.Content))
            {
                if (string.IsNullOrEmpty(model.Title))
                    ModelState.AddModelError("Title", "Başlık alanı boş geçilemez");
                if (string.IsNullOrEmpty(model.Content))
                {
                    ModelState.AddModelError("Content", "İçerik alanı boş geçilemez");
                }
                model.LeftMenu = LeftMenuConstants.CreateLeftMenuModel(LeftMenuConstants.GroupName.StoreSettings);
            }
            else
            {
                var storeNew = _storeNewService.GetStoreNewByStoreNewId(model.StoreNewId);
                storeNew.Title = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(LimitText(StringHelpers.ProductNameRegulator(model.Title).Trim(), 250).ToLower());
                storeNew.UpdateDate = DateTime.Now;
                storeNew.Content = model.Content;
                storeNew.Active = false;

                if (Request.Files.Count > 0)
                {
                    HttpPostedFileBase file = Request.Files[0];
                    if (ImageContentType.Any(x => x == file.ContentType) && file.ContentLength > 0)
                    {
                        var mapPathOld = Server.MapPath(AppSettings.StoreNewImageFolder);
                        var fileOld = mapPathOld + storeNew.ImageName;
                        var imageizes = AppSettings.StoreNewImageSize.Split(';');
                        foreach (var item in imageizes)
                        {
                            var fileOldAnother = fileOld.Replace("haber", item);
                            if (System.IO.File.Exists(fileOld))
                            {
                                System.IO.File.Delete(fileOld);
                            }
                            if (System.IO.File.Exists(fileOldAnother))
                            {
                                System.IO.File.Delete(fileOldAnother);
                            }
                        }

                        string oldfile = file.FileName;
                        string mapPath = this.Server.MapPath(AppSettings.StoreNewImageFolder);
                        string uzanti = oldfile.Substring(oldfile.LastIndexOf("."), oldfile.Length - oldfile.LastIndexOf("."));
                        string filename = storeNew.Title.ToImageFileName() + storeNew.StoreNewId + "_haber" + uzanti;
                        var targetFile = new FileInfo(mapPath + filename);
                        string storeNewImage = mapPath + filename;
                        file.SaveAs(storeNewImage);
                        foreach (var item in imageizes)
                        {
                            int width = Convert.ToInt32(item.Split('x')[0]);
                            int height = Convert.ToInt32(item.Split('x')[1]);

                            Image img = ImageProcessHelper.resizeImageBanner(width, height, storeNewImage);
                            ImageProcessHelper.SaveJpeg(storeNewImage, img, 100, "_haber", "_" + item);
                        }

                        storeNew.ImageName = filename;
                    }
                }

                _storeNewService.UpdateStoreNew(storeNew);

                TempData["success"] = "basarili";
                return RedirectToAction("Update", "StoreNew", new { @newType = model.NewType });
            }

            return View(model);
        }
        [HttpPost]
        public JsonResult Delete(int id)
        {
            var storeNew = _storeNewService.GetStoreNewByStoreNewId(id);
            var mapPathOld = Server.MapPath(AppSettings.StoreNewImageFolder);
            var fileOld = mapPathOld + storeNew.ImageName;
            var imageSizes = AppSettings.StoreNewImageSize.Split(';');
            foreach (var item in imageSizes)
            {
                var fileOldAnother = fileOld.Replace("haber", item);
                if (System.IO.File.Exists(fileOld))
                {
                    System.IO.File.Delete(fileOld);
                }
                if (System.IO.File.Exists(fileOldAnother))
                {
                    System.IO.File.Delete(fileOldAnother);
                }
            }
            _storeNewService.DeleteStoreNew(storeNew);
            return Json(true, JsonRequestBehavior.AllowGet);
        }
        public string LimitText(string text, short limit)
        {
            if (!string.IsNullOrEmpty(text))
            {
                if (text.Length > limit)
                {
                    return text.Substring(0, limit);
                }
                return text;
            }
            return "";
        }
    }
}