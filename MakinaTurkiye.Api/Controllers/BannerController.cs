using MakinaTurkiye.Api.View;
using MakinaTurkiye.Core.Infrastructure;
using MakinaTurkiye.Services.Common;
using MakinaTurkiye.Utilities.ImageHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace MakinaTurkiye.Api.Controllers
{
    public class BannerController : BaseApiController
    {
        private readonly IBannerService ProductService;
        public BannerController()
        {
            ProductService = EngineContext.Current.Resolve<IBannerService>();
        }

        public HttpResponseMessage GetAll()
        {
            ProcessStatus ProcessStatus = new ProcessStatus();
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
                    bannerItemModel.PicturePathPc = bannerPc.BannerResource != null ? "https:" + ImageHelper.GetBannerImagePath(bannerPc.BannerResource):null;
                    if (bannerTablet != null)
                        bannerItemModel.PicturePathTablet = bannerTablet.BannerResource != null ? "https:" + ImageHelper.GetBannerImagePath(bannerTablet.BannerResource):null;
                    if (bannerMobile != null)
                        bannerItemModel.PicturePathMobile = bannerMobile.BannerResource != null ? "https:" + ImageHelper.GetBannerImagePath(bannerMobile.BannerResource):null;

                    Result.Add(bannerItemModel);
                    index++;
                }

                if (Result != null && Result.Count() > 0)
                {
                    ProcessStatus.Result = Result;
                    ProcessStatus.ActiveResultRowCount = Result.Count();
                    ProcessStatus.TotolRowCount = ProcessStatus.ActiveResultRowCount;

                    ProcessStatus.Message.Header = "Banners Operations";
                    ProcessStatus.Message.Text = "Success";
                    ProcessStatus.Status = true;
                }
                else
                {
                    ProcessStatus.Message.Header = "Banners Operations";
                    ProcessStatus.Message.Text = "Entity Not Found";
                    ProcessStatus.Status = false;
                    ProcessStatus.Result = null;
                }

            }
            catch (Exception Error)
            {
                ProcessStatus.Message.Header = "Banners Operations";
                ProcessStatus.Message.Text = "Error";
                ProcessStatus.Status = false;
                ProcessStatus.Result = null;
                ProcessStatus.Error = Error;
            }
            return Request.CreateResponse(HttpStatusCode.OK, ProcessStatus);
        }



    }
}
