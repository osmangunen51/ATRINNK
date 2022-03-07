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
    public class CountryController : BaseApiController
    {
        private readonly IAddressService _adressService;

        public CountryController()
        {
            _adressService = EngineContext.Current.Resolve<IAddressService>();
        }
        public HttpResponseMessage GetAllCountries()
        {
            ProcessResult processStatus = new ProcessResult();
            try
            {
                processStatus.Message.Header = "Country İşlemleri";
                processStatus.Message.Text = "Başarılı";
                processStatus.Status = true;
                processStatus.Result = _adressService.GetAllCountries().Select(Snc =>
                         new Api.View.Country()
                         {
                             CountryId = Snc.CountryId,
                             CountryName = Snc.CountryName,
                         }
                    );
                processStatus.Error = null;
            }
            catch (Exception ex)
            {
                processStatus.Message.Header = "Country İşlemleri";
                processStatus.Message.Text = "İşlem başarısız.";
                processStatus.Status = false;
                processStatus.Result = "Hata ile karşılaşıldı!";
                processStatus.Error = ex;
            }
            return Request.CreateResponse(HttpStatusCode.OK, processStatus);
        }

        public HttpResponseMessage GetCultureCode(int CountryId)
        {
            ProcessResult processStatus = new ProcessResult();
            try
            {
                processStatus.Message.Header = "Country İşlemleri";
                processStatus.Message.Text = "Başarılı";
                processStatus.Status = true;
                processStatus.Result =_adressService.GetCountryByCountryId(CountryId)?.CultureCode;
                processStatus.Error = null;
            }
            catch (Exception ex)
            {
                processStatus.Message.Header = "Country İşlemleri";
                processStatus.Message.Text = "İşlem başarısız.";
                processStatus.Status = false;
                processStatus.Result = "Hata ile karşılaşıldı!";
                processStatus.Error = ex;
            }
            return Request.CreateResponse(HttpStatusCode.OK, processStatus);
        }
    }
}