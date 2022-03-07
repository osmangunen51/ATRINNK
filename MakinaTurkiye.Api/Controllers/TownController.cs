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
    public class TownController : BaseApiController
    {
        private readonly IAddressService _adressService;

        public TownController()
        {
            _adressService = EngineContext.Current.Resolve<IAddressService>();
        }
        public HttpResponseMessage GetTownsByLocalityId(int LocalityId)
        {
            ProcessResult processStatus = new ProcessResult();
            try
            {
                processStatus.Message.Header = "Locality İşlemleri";
                processStatus.Message.Text = "Başarılı";
                processStatus.Status = true;
                processStatus.Result = _adressService.GetTownsByLocalityId(LocalityId).Select(Snc =>
                         new Api.View.Town()
                         {
                             TownId=Snc.TownId,
                             TownName=Snc.TownName,
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


        public HttpResponseMessage GetZipCode(int TownId)
        {
            ProcessResult processStatus = new ProcessResult();
            try
            {
                var town = _adressService.GetTownByTownId(TownId);
                string zipCode = "";
                var district = _adressService.GetDistrictByDistrictId(town.DistrictId.Value);
                zipCode = district.ZipCode;

                processStatus.Message.Header = "Locality İşlemleri";
                processStatus.Message.Text = "Başarılı";
                processStatus.Status = true;
                processStatus.Result = zipCode;
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