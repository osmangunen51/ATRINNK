using MakinaTurkiye.Api.View;
using MakinaTurkiye.Core.Infrastructure;
using MakinaTurkiye.Services.Common;
using System;
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

        public HttpResponseMessage GetAll()
        {
            ProcessResult processStatus = new ProcessResult();
            try
            {
                processStatus.Message.Header = "World İşlemleri";
                processStatus.Message.Text = "Başarılı";
                processStatus.Status = true;
                var CountryListesi = _adressService.GetAllCountries().Select(Snc =>
                         new Api.View.Country()
                         {
                             CountryId = Snc.CountryId,
                             CountryName = Snc.CountryName,
                         }
                    );

                var CityListesi = _adressService.GetAllCities();
                var LocalityListesi = _adressService.GetAllLocality();
                processStatus.Result = CountryListesi.Select(x => new
                {
                    CountryId = x.CountryId,
                    CountryName = x.CountryName,
                    Cities = CityListesi.Where(y => y.CountryId == x.CountryId).Select(y => new
                    {
                        CityId = y.CityId,
                        CityName = y.CityName,
                        Localities = LocalityListesi.Where(z => z.CityId == y.CityId && z.CountryId == x.CountryId).Select(z => new
                        {
                            LocalityId = z.LocalityId,
                            LocalityName = z.LocalityName,
                        })
                    })
                }).ToList();
                processStatus.Error = null;
            }
            catch (Exception ex)
            {
                processStatus.Message.Header = "World İşlemleri";
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
                processStatus.Result = _adressService.GetCountryByCountryId(CountryId)?.CultureCode;
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