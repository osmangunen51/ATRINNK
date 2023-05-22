using Trinnk.Api.View;
using Trinnk.Core.Infrastructure;
using Trinnk.Services.Common;
using System;
using System.Linq;
using System.Net;
using System.Net.Http;

namespace Trinnk.Api.Controllers
{
    public class LocalityController : BaseApiController
    {
        private readonly IAddressService _adressService;

        public LocalityController()
        {
            _adressService = EngineContext.Current.Resolve<IAddressService>();
        }
        public HttpResponseMessage GetLocalitiesByCityId(int CityId)
        {
            ProcessResult processStatus = new ProcessResult();
            try
            {
                processStatus.Message.Header = "Locality İşlemleri";
                processStatus.Message.Text = "Başarılı";
                processStatus.Status = true;
                processStatus.Result = _adressService.GetLocalitiesByCityId(CityId).Select(Snc =>
                        new Api.View.Locality()
                        {
                            LocalityId = Snc.LocalityId,
                            LocalityName = Snc.LocalityName,
                        }
                    );
                processStatus.Error = null;
            }
            catch (Exception ex)
            {
                processStatus.Message.Header = "Locality İşlemleri";
                processStatus.Message.Text = "İşlem başarısız.";
                processStatus.Status = false;
                processStatus.Result = "Hata ile karşılaşıldı!";
                processStatus.Error = ex;
            }
            return Request.CreateResponse(HttpStatusCode.OK, processStatus);
        }



    }
}