using Trinnk.Api.View;
using Trinnk.Core.Infrastructure;
using Trinnk.Services.Common;
using System;
using System.Linq;
using System.Net;
using System.Net.Http;

namespace Trinnk.Api.Controllers
{
    public class CityController : BaseApiController
    {
        private readonly IAddressService _adressService;
        public CityController()
        {
            _adressService = EngineContext.Current.Resolve<IAddressService>();
        }

        public HttpResponseMessage GetCitiesByCountryId(int CountryId)
        {
            ProcessResult processStatus = new ProcessResult();
            try
            {
                processStatus.Message.Header = "City İşlemleri";
                processStatus.Message.Text = "Başarılı";
                processStatus.Status = true;
                processStatus.Result = _adressService.GetCitiesByCountryId(CountryId).Select(Snc =>
                         new Api.View.City()
                         {
                             CityId = Snc.CityId,
                             CityName = Snc.CityName,
                         }
                    );
                processStatus.Error = null;
            }
            catch (Exception ex)
            {
                processStatus.Message.Header = "City İşlemleri";
                processStatus.Message.Text = "İşlem başarısız.";
                processStatus.Status = false;
                processStatus.Result = "Hata ile karşılaşıldı!";
                processStatus.Error = ex;
            }
            return Request.CreateResponse(HttpStatusCode.OK, processStatus);
        }

        public HttpResponseMessage GetAreaCode(int CityId)
        {
            ProcessResult processStatus = new ProcessResult();
            try
            {
                processStatus.Message.Header = "City İşlemleri";
                processStatus.Message.Text = "Başarılı";
                processStatus.Status = true;
                processStatus.Result = _adressService.GetCityByCityId(Convert.ToInt32(CityId))?.AreaCode;
                processStatus.Error = null;
            }
            catch (Exception ex)
            {
                processStatus.Message.Header = "City İşlemleri";
                processStatus.Message.Text = "İşlem başarısız.";
                processStatus.Status = false;
                processStatus.Result = "Hata ile karşılaşıldı!";
                processStatus.Error = ex;
            }
            return Request.CreateResponse(HttpStatusCode.OK, processStatus);
        }
    }
}