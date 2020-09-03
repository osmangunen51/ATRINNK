using MakinaTurkiye.Api.View;
using MakinaTurkiye.Core.Infrastructure;
using MakinaTurkiye.Services.Common;
using MakinaTurkiye.Services.Members;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;

namespace MakinaTurkiye.Api.Controllers
{
    public class CommonController : BaseApiController
    {
        private readonly IMemberService _memberService;
        private readonly IAddressService _addressService;

        public CommonController()
        {
            _memberService = EngineContext.Current.Resolve<IMemberService>();
            _addressService = EngineContext.Current.Resolve<IAddressService>();
        }

        public HttpResponseMessage GetAllLocalityByCityId(int CityId)
        {
            ProcessResult processStatus = new ProcessResult();
            try
            {
                var locality = _addressService.GetLocalitiesByCityId(CityId);
                if (locality != null)
                {
                    processStatus.Result = locality;
                    processStatus.ActiveResultRowCount = locality.Count;
                    processStatus.TotolRowCount = processStatus.ActiveResultRowCount;
                    processStatus.Message.Header = "Locality İşlemleri";
                    processStatus.Message.Text = "Başarılı";
                    processStatus.Status = true;
                }
                else
                {
                    processStatus.Message.Header = "Locality İşlemleri";
                    processStatus.Message.Text = "Başarısız";
                    processStatus.Status = false;
                    processStatus.Result = "Sorgu sonucu boş!";
                }
            }
            catch (Exception Error)
            {
                processStatus.Message.Header = "Locality İşlemleri";
                processStatus.Message.Text = "Başarısız";
                processStatus.Status = false;
                processStatus.Result = null;
                processStatus.Error = Error;
            }
            return Request.CreateResponse(HttpStatusCode.OK, processStatus);
        }

        public HttpResponseMessage GetAllCityByCountryId(int CountryId)
        {
            ProcessResult processStatus = new ProcessResult();
            try
            {
                var city = _addressService.GetCitiesByCountryId(CountryId);
                if (city != null)
                {
                    processStatus.Result = city;
                    processStatus.ActiveResultRowCount = city.Count;
                    processStatus.TotolRowCount = processStatus.ActiveResultRowCount;
                    processStatus.Message.Header = "city İşlemleri";
                    processStatus.Message.Text = "Başarılı";
                    processStatus.Status = true;
                }
                else
                {
                    processStatus.Message.Header = "city İşlemleri";
                    processStatus.Message.Text = "Başarısız";
                    processStatus.Status = false;
                    processStatus.Result = "Sorgu sonucu boş!";
                }
            }
            catch (Exception Error)
            {
                processStatus.Message.Header = "city İşlemleri";
                processStatus.Message.Text = "Başarısız";
                processStatus.Status = false;
                processStatus.Result = null;
                processStatus.Error = Error;
            }
            return Request.CreateResponse(HttpStatusCode.OK, processStatus);
        }

        public HttpResponseMessage GetAllCountries()
        {
            ProcessResult processStatus = new ProcessResult();
            try
            {
                var country = _addressService.GetAllCountries();
                if (country != null)
                {
                    processStatus.Result = country;
                    processStatus.ActiveResultRowCount = country.Count;
                    processStatus.TotolRowCount = processStatus.ActiveResultRowCount;
                    processStatus.Message.Header = "country İşlemleri";
                    processStatus.Message.Text = "Başarılı";
                    processStatus.Status = true;
                }
                else
                {
                    processStatus.Message.Header = "country İşlemleri";
                    processStatus.Message.Text = "Başarısız";
                    processStatus.Status = false;
                    processStatus.Result = "Sorgu sonucu boş!";
                }
            }
            catch (Exception Error)
            {
                processStatus.Message.Header = "country İşlemleri";
                processStatus.Message.Text = "Başarısız";
                processStatus.Status = false;
                processStatus.Result = null;
                processStatus.Error = Error;
            }
            return Request.CreateResponse(HttpStatusCode.OK, processStatus);
        }

        public HttpResponseMessage GetCountriesByCountryId(int countryId)
        {
            ProcessResult processStatus = new ProcessResult();
            try
            {
                var Countries = new List<Entities.Tables.Common.Country>();
                var country = _addressService.GetCountryByCountryId(countryId);
                Countries.Add(country);
                if (Countries != null)
                {
                    processStatus.Result = Countries;
                    processStatus.ActiveResultRowCount = Countries.Count;
                    processStatus.TotolRowCount = processStatus.ActiveResultRowCount;
                    processStatus.Message.Header = "country İşlemleri";
                    processStatus.Message.Text = "Başarılı";
                    processStatus.Status = true;
                }
                else
                {
                    processStatus.Message.Header = "country İşlemleri";
                    processStatus.Message.Text = "Başarısız";
                    processStatus.Status = false;
                    processStatus.Result = "Sorgu sonucu boş!";
                }
            }
            catch (Exception Error)
            {
                processStatus.Message.Header = "country İşlemleri";
                processStatus.Message.Text = "Başarısız";
                processStatus.Status = false;
                processStatus.Result = null;
                processStatus.Error = Error;
            }
            return Request.CreateResponse(HttpStatusCode.OK, processStatus);
        }

        public HttpResponseMessage GetTownsByLocalityId(int LocalityId)
        {
            ProcessResult processStatus = new ProcessResult();
            try
            {
                var country = _addressService.GetTownsByLocalityId(LocalityId);
                if (country != null)
                {
                    processStatus.Result = country;
                    processStatus.ActiveResultRowCount = country.Count;
                    processStatus.TotolRowCount = processStatus.ActiveResultRowCount;
                    processStatus.Message.Header = "Towns İşlemleri";
                    processStatus.Message.Text = "Başarılı";
                    processStatus.Status = true;
                }
                else
                {
                    processStatus.Message.Header = "Towns İşlemleri";
                    processStatus.Message.Text = "Başarısız";
                    processStatus.Status = false;
                    processStatus.Result = "Sorgu sonucu boş!";
                }
            }
            catch (Exception Error)
            {
                processStatus.Message.Header = "Towns İşlemleri";
                processStatus.Message.Text = "Başarısız";
                processStatus.Status = false;
                processStatus.Result = null;
                processStatus.Error = Error;
            }
            return Request.CreateResponse(HttpStatusCode.OK, processStatus);
        }
    }
}