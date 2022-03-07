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
    public class DistrictController : BaseApiController
    {
        private readonly IAddressService _adressService;

        public DistrictController()
        {
            _adressService = EngineContext.Current.Resolve<IAddressService>();
        }
        public HttpResponseMessage GetDistrictsByLocalityId(int LocalityId)
        {
            ProcessResult processStatus = new ProcessResult();
            try
            {
                processStatus.Message.Header = "District İşlemleri";
                processStatus.Message.Text = "Başarılı";
                processStatus.Status = true;
                processStatus.Result = _adressService.GetDistrictsByLocalityId(LocalityId).Select(Snc =>
                         new Api.View.District()
                         {
                             DistrictId= Snc.DistrictId,
                             DistrictName = Snc.DistrictName,
                             CityId = Snc.CityId,
                             LocalityId = Snc.LocalityId,
                             ZipCode = Snc.ZipCode,
                         }
                    );
                processStatus.Error = null;
            }
            catch (Exception ex)
            {
                processStatus.Message.Header = "District İşlemleri";
                processStatus.Message.Text = "İşlem başarısız.";
                processStatus.Status = false;
                processStatus.Result = "Hata ile karşılaşıldı!";
                processStatus.Error = ex;
            }
            return Request.CreateResponse(HttpStatusCode.OK, processStatus);
        }
        public HttpResponseMessage GetDistrictsByCityId(int CityId)
        {
            ProcessResult processStatus = new ProcessResult();
            try
            {
                processStatus.Message.Header = "District İşlemleri";
                processStatus.Message.Text = "Başarılı";
                processStatus.Status = true;
                processStatus.Result = _adressService.GetDistrictsByCityId(CityId).Select(Snc =>
                         new Api.View.District()
                         {
                             DistrictId = Snc.DistrictId,
                             DistrictName = Snc.DistrictName,
                             CityId = Snc.CityId,
                             LocalityId = Snc.LocalityId,
                             ZipCode = Snc.ZipCode,
                         }
                    );
                processStatus.Error = null;
            }
            catch (Exception ex)
            {
                processStatus.Message.Header = "District İşlemleri";
                processStatus.Message.Text = "İşlem başarısız.";
                processStatus.Status = false;
                processStatus.Result = "Hata ile karşılaşıldı!";
                processStatus.Error = ex;
            }
            return Request.CreateResponse(HttpStatusCode.OK, processStatus);
        }
    }
}