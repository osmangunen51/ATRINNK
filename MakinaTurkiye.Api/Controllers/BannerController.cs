using MakinaTurkiye.Core.Infrastructure;
using MakinaTurkiye.Entities.Tables.Common;
using MakinaTurkiye.Services.Catalog;
using MakinaTurkiye.Services.Common;
using MakinaTurkiye.Utilities.ImageHelpers;
using MMakinaTurkiye.Api.View;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Web.Http;
using System.Web.Mvc;
using System.Xml;
using System.Xml.Serialization;

namespace MakinaTurkiye.Api.Controllers
{
    public class BannerController : ApiController
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
                    bannerItemModel.PicturePathPc = ImageHelper.GetBannerImagePath(bannerPc.BannerResource);
                    if (bannerTablet != null)
                        bannerItemModel.PicturePathTablet = ImageHelper.GetBannerImagePath(bannerTablet.BannerResource);
                    if (bannerMobile != null)
                        bannerItemModel.PicturePathMobile = ImageHelper.GetBannerImagePath(bannerMobile.BannerResource);

                    Result.Add(bannerItemModel);
                    index++;
                }

                if (Result != null  && Result.Count()>0)
                {
                    ProcessStatus.Result = Result;
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
