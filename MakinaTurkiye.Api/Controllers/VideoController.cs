using MakinaTurkiye.Api.View;
using MakinaTurkiye.Core.Infrastructure;
using MakinaTurkiye.Services.Common;
using MakinaTurkiye.Services.Videos;
using MakinaTurkiye.Utilities.ImageHelpers;
using MakinaTurkiye.Utilities.VideoHelpers;
using System;
using System.Linq;
using System.Net;
using System.Net.Http;

namespace MakinaTurkiye.Api.Controllers
{
    public class VideoController : BaseApiController
    {
        private readonly IVideoService videoService;

        public VideoController()
        {
            videoService = EngineContext.Current.Resolve<IVideoService>();
        }

        public HttpResponseMessage GetPopularVideos()
        {
            ProcessResult processStatus = new ProcessResult();
            try
            {
                var tmp = videoService.GetSPPopularVideos().ToList();
                foreach (var item in tmp)
                {
                    if (!string.IsNullOrEmpty(item.VideoPicturePath) && !item.VideoPicturePath.StartsWith("https"))
                    {
                        item.VideoPicturePath = "https:" + ImageHelper.GetVideoImagePath(item.VideoPicturePath);
                    }
                }
                processStatus.Message.Header = "Video İşlemleri";
                processStatus.Message.Text = "Başarılı";
                processStatus.Status = true;
                processStatus.Result = tmp;
                processStatus.Error = null;
            }
            catch (Exception ex)
            {
                processStatus.Message.Header = "Video İşlemleri";
                processStatus.Message.Text = "İşlem başarısız.";
                processStatus.Status = false;
                processStatus.Result = "Hata ile karşılaşıldı!";
                processStatus.Error = ex;
            }
            return Request.CreateResponse(HttpStatusCode.OK, processStatus);
        }


        public HttpResponseMessage GetVideoByCategoryId(int categoryId, int pageIndex, int pageSize)
        {
            ProcessResult processStatus = new ProcessResult();
            try
            {
                var tmp = videoService.GetSPVideoByCategoryId(categoryId,pageIndex,pageSize).ToList();
                foreach (var item in tmp)
                {
                    if (!string.IsNullOrEmpty(item.VideoPicturePath) && !item.VideoPicturePath.StartsWith("https"))
                    {
                        item.VideoPicturePath = "https:" + ImageHelper.GetVideoImagePath(item.VideoPicturePath);
                    }

                    if (!string.IsNullOrEmpty(item.VideoPath) && !item.VideoPath.StartsWith("https"))
                    {
                        item.VideoPath = "https:" + VideoHelper.GetVideoPath(item.VideoPath);
                    }
                    
                }
                processStatus.Message.Header = "Video İşlemleri";
                processStatus.Message.Text = "Başarılı";
                processStatus.Status = true;
                processStatus.Result = tmp;
                processStatus.Error = null;
            }
            catch (Exception ex)
            {
                processStatus.Message.Header = "Video İşlemleri";
                processStatus.Message.Text = "İşlem başarısız.";
                processStatus.Status = false;
                processStatus.Result = "Hata ile karşılaşıldı!";
                processStatus.Error = ex;
            }
            return Request.CreateResponse(HttpStatusCode.OK, processStatus);
        }

        public HttpResponseMessage GetOtherVideoByCategoryIdAndSelectedCategoryId(int categoryId,  int selectedCategoryId, int topCount = 10)
        {
            ProcessResult processStatus = new ProcessResult();
            try
            {
                var tmp = videoService.GetSPOtherVideoByCategoryIdAndSelectedCategoryId(categoryId, topCount, selectedCategoryId).ToList();
                foreach (var item in tmp)
                {
                    if (!string.IsNullOrEmpty(item.VideoPicturePath) && !item.VideoPicturePath.StartsWith("https"))
                    {
                        item.VideoPicturePath = "https:" + ImageHelper.GetVideoImagePath(item.VideoPicturePath);
                    }
                    if (!string.IsNullOrEmpty(item.VideoPath) && !item.VideoPath.StartsWith("https"))
                    {
                        item.VideoPath = "https:" + VideoHelper.GetVideoPath(item.VideoPath);
                    }
                }
                processStatus.Message.Header = "Video İşlemleri";
                processStatus.Message.Text = "Başarılı";
                processStatus.Status = true;
                processStatus.Result = tmp;
                processStatus.Error = null;
            }
            catch (Exception ex)
            {
                processStatus.Message.Header = "Video İşlemleri";
                processStatus.Message.Text = "İşlem başarısız.";
                processStatus.Status = false;
                processStatus.Result = "Hata ile karşılaşıldı!";
                processStatus.Error = ex;
            }
            return Request.CreateResponse(HttpStatusCode.OK, processStatus);
        }

        public HttpResponseMessage GetVideosBySearchText(string searchText, int categoryId, int pageSize, int pageIndex)
        {
            ProcessResult processStatus = new ProcessResult();
            try
            {
                var tmp = videoService.GetSpVideosBySearchText(searchText, categoryId, pageSize, pageIndex).ToList();
                foreach (var item in tmp)
                {
                    if (!string.IsNullOrEmpty(item.VideoPicturePath) && !item.VideoPicturePath.StartsWith("https"))
                    {
                        item.VideoPicturePath = "https:" + ImageHelper.GetVideoImagePath(item.VideoPicturePath);
                    }
                    if (!string.IsNullOrEmpty(item.VideoPath) && !item.VideoPath.StartsWith("https"))
                    {
                        item.VideoPath = "https:" + VideoHelper.GetVideoPath(item.VideoPath);
                    }
                }
                processStatus.Message.Header = "Video İşlemleri";
                processStatus.Message.Text = "Başarılı";
                processStatus.Status = true;
                processStatus.Result = tmp;
                processStatus.Error = null;
            }
            catch (Exception ex)
            {
                processStatus.Message.Header = "Video İşlemleri";
                processStatus.Message.Text = "İşlem başarısız.";
                processStatus.Status = false;
                processStatus.Result = "Hata ile karşılaşıldı!";
                processStatus.Error = ex;
            }
            return Request.CreateResponse(HttpStatusCode.OK, processStatus);
        }


        public HttpResponseMessage GetShowOnShowcaseVideos()
        {
            ProcessResult processStatus = new ProcessResult();
            try
            {
                var tmp = videoService.GetShowOnShowcaseVideos().ToList();
                foreach (var item in tmp)
                {
                    if (!string.IsNullOrEmpty(item.VideoPicturePath) && !item.VideoPicturePath.StartsWith("https"))
                    {
                        item.VideoPicturePath = "https:" + ImageHelper.GetVideoImagePath(item.VideoPicturePath);
                    }
                    if (!string.IsNullOrEmpty(item.VideoPath) && !item.VideoPath.StartsWith("https"))
                    {
                        item.VideoPath = "https:" + VideoHelper.GetVideoPath(item.VideoPath);
                    }
                }
                processStatus.Message.Header = "Video İşlemleri";
                processStatus.Message.Text = "Başarılı";
                processStatus.Status = true;
                processStatus.Result = tmp;
                processStatus.Error = null;
            }
            catch (Exception ex)
            {
                processStatus.Message.Header = "Video İşlemleri";
                processStatus.Message.Text = "İşlem başarısız.";
                processStatus.Status = false;
                processStatus.Result = "Hata ile karşılaşıldı!";
                processStatus.Error = ex;
            }
            return Request.CreateResponse(HttpStatusCode.OK, processStatus);
        }


        public HttpResponseMessage GetVideoByVideoId(int videoId, bool showHidden = false)
        {
            ProcessResult processStatus = new ProcessResult();
            try
            {
                var tmp = videoService.GetVideoByVideoId(videoId, showHidden);
                if (!tmp.VideoPicturePath.StartsWith("https"))
                {
                    tmp.VideoPicturePath = "https:" + ImageHelper.GetVideoImagePath(tmp.VideoPicturePath);
                }
                if (!tmp.VideoPath.StartsWith("https"))
                {
                    tmp.VideoPath = "https:" + VideoHelper.GetVideoPath(tmp.VideoPath);
                }
                processStatus.Message.Header = "Video İşlemleri";
                processStatus.Message.Text = "Başarılı";
                processStatus.Status = true;
                processStatus.Result = tmp;
                processStatus.Error = null;
            }
            catch (Exception ex)
            {
                processStatus.Message.Header = "Video İşlemleri";
                processStatus.Message.Text = "İşlem başarısız.";
                processStatus.Status = false;
                processStatus.Result = "Hata ile karşılaşıldı!";
                processStatus.Error = ex;
            }
            return Request.CreateResponse(HttpStatusCode.OK, processStatus);
        }

        public HttpResponseMessage GetCategoriesByCategoryId(int categoryId=0)
        {
            ProcessResult processStatus = new ProcessResult();
            try
            {
                var tmp = videoService.GetSPVideoCategoryByCategoryParentIdNew(categoryId);
                processStatus.Message.Header = "Video İşlemleri";
                processStatus.Message.Text = "Başarılı";
                processStatus.Status = true;
                processStatus.Result = tmp;
                processStatus.Error = null;
            }
            catch (Exception ex)
            {
                processStatus.Message.Header = "Video İşlemleri";
                processStatus.Message.Text = "İşlem başarısız.";
                processStatus.Status = false;
                processStatus.Result = "Hata ile karşılaşıldı!";
                processStatus.Error = ex;
            }
            return Request.CreateResponse(HttpStatusCode.OK, processStatus);
        }




        public HttpResponseMessage GetVideosByProductId(int productId = 0)
        {
            ProcessResult processStatus = new ProcessResult();
            try
            {
                var tmp = videoService.GetVideosByProductId(productId).ToList();
                foreach (var item in tmp)
                {
                    if (!string.IsNullOrEmpty(item.VideoPicturePath) && !item.VideoPicturePath.StartsWith("https"))
                    {
                        item.VideoPicturePath = "https:" + ImageHelper.GetVideoImagePath(item.VideoPicturePath);
                    }
                    if (!string.IsNullOrEmpty(item.VideoPath) && !item.VideoPath.StartsWith("https"))
                    {
                        item.VideoPath = "https:" + VideoHelper.GetVideoPath(item.VideoPath);
                    }
                }
                processStatus.Message.Header = "Video İşlemleri";
                processStatus.Message.Text = "Başarılı";
                processStatus.Status = true;
                processStatus.Result = tmp;
                processStatus.Error = null;
            }
            catch (Exception ex)
            {
                processStatus.Message.Header = "Video İşlemleri";
                processStatus.Message.Text = "İşlem başarısız.";
                processStatus.Status = false;
                processStatus.Result = "Hata ile karşılaşıldı!";
                processStatus.Error = ex;
            }
            return Request.CreateResponse(HttpStatusCode.OK, processStatus);
        }

        public HttpResponseMessage GetVideoByStoreMainPartyId(int storeMainPartyId, bool showHidden = false)
        {
            ProcessResult processStatus = new ProcessResult();
            try
            {
                var tmp = videoService.GetVideoByStoreMainPartyId(storeMainPartyId, showHidden).ToList();
                foreach (var item in tmp)
                {
                    if (!string.IsNullOrEmpty(item.VideoPicturePath) && !item.VideoPicturePath.StartsWith("https"))
                    {
                        item.VideoPicturePath = "https:" + ImageHelper.GetVideoImagePath(item.VideoPicturePath);
                    }
                    if (!string.IsNullOrEmpty(item.VideoPath) && !item.VideoPath.StartsWith("https"))
                    {
                        item.VideoPath = "https:" + VideoHelper.GetVideoPath(item.VideoPath);
                    }
                }
                processStatus.Message.Header = "Video İşlemleri";
                processStatus.Message.Text = "Başarılı";
                processStatus.Status = true;
                processStatus.Result = tmp;
                processStatus.Error = null;
            }
            catch (Exception ex)
            {
                processStatus.Message.Header = "Video İşlemleri";
                processStatus.Message.Text = "İşlem başarısız.";
                processStatus.Status = false;
                processStatus.Result = "Hata ile karşılaşıldı!";
                processStatus.Error = ex;
            }
            return Request.CreateResponse(HttpStatusCode.OK, processStatus);
        }

        public HttpResponseMessage GetVideoByStoreMainPartyIdAndVideoName(string searchText, int categoryId, int pageSize, int pageIndex, int storeMainPartyId)
        {
            ProcessResult processStatus = new ProcessResult();
            try
            {
                var tmp = videoService.GetSpVideosBySearchText(searchText, categoryId, pageSize, pageIndex).Where(x=>x.StoreId==storeMainPartyId).ToList();
                foreach (var item in tmp)
                {
                    if (!string.IsNullOrEmpty(item.VideoPicturePath) && !item.VideoPicturePath.StartsWith("https"))
                    {
                        item.VideoPicturePath = "https:" + ImageHelper.GetVideoImagePath(item.VideoPicturePath);
                    }
                    if (!string.IsNullOrEmpty(item.VideoPath) && !item.VideoPath.StartsWith("https"))
                    {
                        item.VideoPath = "https:" + VideoHelper.GetVideoPath(item.VideoPath);
                    }
                }
                processStatus.Message.Header = "Video İşlemleri";
                processStatus.Message.Text = "Başarılı";
                processStatus.Status = true;
                processStatus.Result = tmp;
                processStatus.Error = null;
            }
            catch (Exception ex)
            {
                processStatus.Message.Header = "Video İşlemleri";
                processStatus.Message.Text = "İşlem başarısız.";
                processStatus.Status = false;
                processStatus.Result = "Hata ile karşılaşıldı!";
                processStatus.Error = ex;
            }
            return Request.CreateResponse(HttpStatusCode.OK, processStatus);
        }

    }
}