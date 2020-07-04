using MakinaTurkiye.Services.Members;
using MakinaTurkiye.Services.Stores;
using MakinaTurkiye.Services.Videos;
using MakinaTurkiye.Utilities.Controllers;
using MakinaTurkiye.Utilities.ImageHelpers;
using NeoSistem.MakinaTurkiye.Core.Web.Helpers;
using NeoSistem.MakinaTurkiye.Web.Areas.Account.Constants;
using NeoSistem.MakinaTurkiye.Web.Areas.Account.Models.Videos;
using NeoSistem.MakinaTurkiye.Web.Models;
using NeoSistem.MakinaTurkiye.Web.Models.Authentication;
using NeoSistem.MakinaTurkiye.Web.Models.UtilityModel;
using System;
using System.Web.Mvc;

namespace NeoSistem.MakinaTurkiye.Web.Areas.Account.Controllers
{
    public class VideoController : BaseAccountController
    {
        IVideoService _videoService;
        IMemberStoreService _memberStoreService;
        IStoreService _storeService;

        public VideoController(IVideoService videoService,
            IMemberStoreService memberStoreService,
            IStoreService storeService)
        {
            this._videoService = videoService;
            this._memberStoreService = memberStoreService;
            this._storeService = storeService;

            this._videoService.CachingGetOrSetOperationEnabled=false;
            this._memberStoreService.CachingGetOrSetOperationEnabled=false;
            this._storeService.CachingGetOrSetOperationEnabled=false;
        }
        // GET: Account/Video
        public ActionResult Index()
        {
            if (AuthenticationUser.CurrentUser.Membership.MainPartyId != 0)
            {
                MTStoreVideoModel model = new MTStoreVideoModel();

                int memberMainPartId = AuthenticationUser.CurrentUser.Membership.MainPartyId;
                var memberStore = _memberStoreService.GetMemberStoreByMemberMainPartyId(memberMainPartId);
                if (memberStore != null)
                {
                    model.LeftMenu = LeftMenuConstants.CreateLeftMenuModel(LeftMenuConstants.GroupName.StoreSettings, (byte)LeftMenuConstants.StoreSettingGeneralInfo.Videos);
                    var videos = _videoService.GetVideoByStoreMainPartyId(memberStore.StoreMainPartyId.Value);
                    foreach (var item in videos)
                    {
                        model.StoreVideoItemModels.Add(
                            new MTStoreVideoItemModel
                            {
                                ImagePath = ImageHelper.GetVideoImagePath(item.VideoPicturePath),
                                Minute = item.VideoMinute.HasValue ? item.VideoMinute.Value : 0,
                                VideoId = item.VideoId,
                                Second = item.VideoSecond.HasValue ? item.VideoSecond.Value : 0,
                                VideoPath = item.VideoPath,
                                ViewCount = item.SingularViewCount.HasValue ? item.SingularViewCount.Value : 0,
                                RecordDate = item.VideoRecordDate.Value,
                                Title = item.VideoTitle,
                                Order = item.Order.HasValue ? item.Order.Value : (byte)0
                            }
                            );
                    }

                    return View(model);
                }
            }
            return RedirectToAction("Home", "Index");
        }
        public ActionResult Create()
        {
            MTVideoCreateModel model = new MTVideoCreateModel();
            model.LeftMenu = LeftMenuConstants.CreateLeftMenuModel(LeftMenuConstants.GroupName.StoreSettings, (byte)LeftMenuConstants.StoreSettingGeneralInfo.Videos);
            return View(model);
        }

        [HttpPost]
        public ActionResult Create(MTVideoCreateModel model)
        {
            int memberMainPartId = AuthenticationUser.CurrentUser.Membership.MainPartyId;
            var memberStore = _memberStoreService.GetMemberStoreByMemberMainPartyId(memberMainPartId);
            var file = Request.Files[0];
            if (file.ContentLength > 0)
            {

                VideoModelHelper vModel = FileHelpers.fffmpegVideoConvert(file, AppSettings.TempFolder, AppSettings.VideoThumbnailFolder, AppSettings.NewVideosFolder, AppSettings.ffmpegFolder, 490, 328);
                DateTime timesplit;


                if (DateTime.TryParse(vModel.Duration, out timesplit))
                {

                }
                else
                {
                    timesplit = DateTime.Now.Date;
                }


                var video = new global::MakinaTurkiye.Entities.Tables.Videos.Video
                {
                    Active = true,
                    VideoPath = vModel.newFileName,
                    VideoSize = null,
                    VideoPicturePath = vModel.newFileName + ".jpg",
                    VideoTitle = model.VideoTitle,
                    VideoRecordDate = DateTime.Now,
                    VideoMinute = (byte?)timesplit.Minute,
                    VideoSecond = (byte?)timesplit.Second,
                    StoreMainPartyId = memberStore.StoreMainPartyId.Value
                };
                _videoService.InsertVideo(video);
                TempData["success"] = true;
            }
            return RedirectToAction("Create");

        }
        [HttpGet]
        public JsonResult Delete(int id)
        {
            var video = _videoService.GetVideoByVideoId(id);
            var imagePath = ImageHelper.GetVideoImagePath(video.VideoPicturePath);
            FileHelpers.Delete(imagePath);
            var videoPath = "/UserFiles/NewVideos/" + video.VideoPath + ".mp4";
            FileHelpers.Delete(videoPath);
            _videoService.DeleteVideo(video);
            return Json(true, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetOrder(int id)
        {
            ResponseModel<MTStoreVideoItemModel> responseModel = new ResponseModel<MTStoreVideoItemModel>();
            var item = _videoService.GetVideoByVideoId(id);
            var storevideoItem =
                     new MTStoreVideoItemModel
                     {
                         ImagePath = ImageHelper.GetVideoImagePath(item.VideoPicturePath),
                         Minute = item.VideoMinute.HasValue ? item.VideoMinute.Value : 0,
                         VideoId = item.VideoId,
                         Second = item.VideoSecond.HasValue ? item.VideoSecond.Value : 0,
                         VideoPath = item.VideoPath,
                         ViewCount = item.SingularViewCount.HasValue ? item.SingularViewCount.Value : 0,
                         RecordDate = item.VideoRecordDate.Value,
                         Title = item.VideoTitle,
                         Order = item.Order.HasValue ? item.Order.Value : (byte)0
                     };
            responseModel.IsSuccess = true;
            responseModel.Result = storevideoItem;
            return Json(responseModel, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult UpdateOrder(int id, byte order,string title)
        {
            try
            {
                var video = _videoService.GetVideoByVideoId(id);
                video.Order = order;
                video.VideoTitle = title;
                _videoService.UpdateVideo(video);
                return Json(true, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }

        }
    }
}