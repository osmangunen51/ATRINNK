using MakinaTurkiye.Services.Videos;
using NeoSistem.MakinaTurkiye.Management.Models.Videos;
using System.Collections.Generic;
using System.Web.Mvc;

namespace NeoSistem.MakinaTurkiye.Management.Controllers
{
    public class VideoController : Controller
    {
        #region Fields

        IVideoService _videoService;

        #endregion

        #region Ctor


        public VideoController(IVideoService videoService)
        {
            _videoService = videoService;
        }

        #endregion

        #region Methods

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult EditShowcase()
        {
            var model = new List<VideoShowcaseModel>();
            var showOnShowcaseVideos = _videoService.GetShowOnShowcaseVideos();
            foreach (var item in showOnShowcaseVideos)
            {
                model.Add(new VideoShowcaseModel { VideoId = item.VideoId, ProductName = item.Product.ProductName });
            }
            return View(model);
        }

        [HttpPost]
        public ActionResult EditShowcase(int videoId)
        {
            var video = _videoService.GetVideoByVideoId(videoId,true);
            if (video != null)
            {
                video.ShowOnShowcase = false;
                _videoService.UpdateVideo(video);
            }

            return RedirectToAction("EditShowcase");
        }

        //public string UpdateVideoTitle()
        //{
        //    MakinaTurkiyeEntities entities=new MakinaTurkiyeEntities();
        //    var videos=entities.Videos.Where(x=>x.VideoTitle=="" || x.VideoTitle==null);
        //    int sayi = videos.ToList().Count;
        //    foreach (var item in videos.ToList())
        //    {
        //        var product=entities.Products.FirstOrDefault(x=>x.ProductId==item.ProductId);
        //        if(product!=null)
        //        { 
        //        item.VideoTitle=product.ProductName;
        //        entities.SaveChanges();
        //        }
        //    }
        //    return "tamamlandı";
        //}

        #endregion

    }
}
