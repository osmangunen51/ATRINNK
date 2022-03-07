using MakinaTurkiye.Api.Helpers;
using MakinaTurkiye.Api.View;
using MakinaTurkiye.Core.Infrastructure;
using MakinaTurkiye.Services.Catalog;
using MakinaTurkiye.Services.Common;
using MakinaTurkiye.Services.Members;
using MakinaTurkiye.Services.Packets;
using MakinaTurkiye.Services.Stores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;

namespace MakinaTurkiye.Api.Controllers
{
    public class NewsletterController : BaseApiController
    {
        private readonly IAddressService _adressService;

        public NewsletterController()
        {
            _adressService = EngineContext.Current.Resolve<IAddressService>();
        }

        public HttpResponseMessage Add(string email)
        {
            ProcessResult processStatus = new ProcessResult();
            try
            {
                processStatus.Message.Header = "Newsletter İşlemleri";
                processStatus.Message.Text = "Yapılacak Daha Yapılmadı";
                processStatus.Status = false;
                processStatus.Result = "Yapılacak Daha Yapılmadı";
                processStatus.Error = null;
            }
            catch (Exception ex)
            {
                processStatus.Message.Header = "Newsletter İşlemleri";
                processStatus.Message.Text = "İşlem başarısız.";
                processStatus.Status = false;
                processStatus.Result = "Hata ile karşılaşıldı!";
                processStatus.Error = ex;
            }
            return Request.CreateResponse(HttpStatusCode.OK, processStatus);
        }
    }
}