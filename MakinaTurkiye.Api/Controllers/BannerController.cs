using MakinaTurkiye.Api.View;
using MakinaTurkiye.Core.Infrastructure;
using MakinaTurkiye.Services.Common;
using MakinaTurkiye.Utilities.ImageHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;

namespace MakinaTurkiye.Api.Controllers
{
    public class BannerController : BaseApiController
    {
        private readonly IBannerService _productService;

        public BannerController()
        {
            _productService = EngineContext.Current.Resolve<IBannerService>();
        }

        //public BannerController(IBannerService productService)
        //{
        //    this._productService = productService;
        //}
        public HttpResponseMessage GetAll()
        {
            ProcessResult processStatus = new ProcessResult();
            try
            {
                IBannerService _bannerService = EngineContext.Current.Resolve<IBannerService>();
                var banners = _bannerService.GetBannersByBannerType(13).OrderBy(x => x.BannerOrder).GroupBy(x => x.BannerOrder);
                List<MTHomeBannerModel> Result = new List<MTHomeBannerModel>();
                int index = 0;
                foreach (var item in banners)
                {
                    var bannersSpecial = _bannerService.GetBannersByBannerType(13).Where(x => x.BannerOrder == item.Key);
                    var bannerMobile = bannersSpecial.FirstOrDefault(x => x.BannerImageType == (byte)BannerImageType.Mobile);
                    var bannerTablet = bannersSpecial.FirstOrDefault(x => x.BannerImageType == (byte)BannerImageType.Tablet);
                    var bannerPc = bannersSpecial.FirstOrDefault(x => x.BannerImageType == (byte)BannerImageType.Pc);

                    var bannerItemModel = new MTHomeBannerModel
                    {
                        Index = index,
                        Url = bannerPc.BannerLink,
                        ImageTag = bannerPc.BannerAltTag
                    };
                    bannerItemModel.PicturePathPc = !string.IsNullOrEmpty(bannerPc.BannerResource) ? "https:" + ImageHelper.GetBannerImagePath(bannerPc.BannerResource) : null;
                    if (bannerTablet != null)
                        bannerItemModel.PicturePathTablet = !string.IsNullOrEmpty(bannerTablet.BannerResource) ? "https:" + ImageHelper.GetBannerImagePath(bannerTablet.BannerResource) : null;
                    if (bannerMobile != null)
                        bannerItemModel.PicturePathMobile = !string.IsNullOrEmpty(bannerMobile.BannerResource) ? "https:" + ImageHelper.GetBannerImagePath(bannerMobile.BannerResource) : null;

                    Result.Add(bannerItemModel);
                    index++;
                }

                if (Result != null && Result.Count() > 0)
                {
                    processStatus.Result = Result;
                    processStatus.ActiveResultRowCount = Result.Count();
                    processStatus.TotolRowCount = processStatus.ActiveResultRowCount;

                    processStatus.Message.Header = "Banners Operations";
                    processStatus.Message.Text = "Success";
                    processStatus.Status = true;
                }
                else
                {
                    processStatus.Message.Header = "Banners Operations";
                    processStatus.Message.Text = "Entity Not Found";
                    processStatus.Status = false;
                    processStatus.Result = null;
                }
            }
            catch (Exception Error)
            {
                processStatus.Message.Header = "Banners Operations";
                processStatus.Message.Text = "Error";
                processStatus.Status = false;
                processStatus.Result = null;
                processStatus.Error = Error;
            }
            return Request.CreateResponse(HttpStatusCode.OK, processStatus);
        }
    }
}