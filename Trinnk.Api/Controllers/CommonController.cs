using Trinnk.Api.View;
using Trinnk.Api.View.Products;
using Trinnk.Core.Infrastructure;
using Trinnk.Services.Catalog;
using Trinnk.Services.Common;
using Trinnk.Services.Members;
using Trinnk.Services.Stores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;

namespace Trinnk.Api.Controllers
{
    public class CommonController : BaseApiController
    {
        private readonly IMemberService _memberService;
        private readonly IAddressService _addressService;
        private readonly IConstantService _constantService;
        private readonly IActivityTypeService _activityTypeService;
        private readonly ICurrencyService _currencyService;
        private readonly IProductComplainService _productComplainService;

        public CommonController()
        {
            _memberService = EngineContext.Current.Resolve<IMemberService>();
            _addressService = EngineContext.Current.Resolve<IAddressService>();
            _constantService = EngineContext.Current.Resolve<IConstantService>();
            _activityTypeService = EngineContext.Current.Resolve<IActivityTypeService>();
            _currencyService = EngineContext.Current.Resolve<ICurrencyService>();
            _productComplainService = EngineContext.Current.Resolve<IProductComplainService>();
        }

        public HttpResponseMessage GetConstants()
        {
            ProcessResult processStatus = new ProcessResult();
            try
            {
                var constantList = _constantService.GetAllConstants().OrderBy(x => x.ConstantType).ThenByDescending(x => x.Order).ToList();
                if (constantList != null)
                {
                    var Liste = constantList.Select(x => new
                    {
                        ConstantGrupNo = (int)x.ConstantType,
                        ConstantGrupAd = ((ConstantTypeEnum)x.ConstantType).GetDisplayName(),
                        ConstantNo = (int)x.ConstantId,
                        ConstantAd = x.ConstantName,
                        ConstantEkBilgi = x.ContstantPropertie,
                        ConstantSira = x.Order
                    }).ToList();
                    processStatus.Result = Liste;
                    processStatus.ActiveResultRowCount = constantList.Count;
                    processStatus.TotolRowCount = processStatus.ActiveResultRowCount;
                    processStatus.Message.Header = "Constant İşlemleri";
                    processStatus.Message.Text = "Başarılı";
                    processStatus.Status = true;
                }
                else
                {
                    processStatus.Message.Header = "Constant İşlemleri";
                    processStatus.Message.Text = "Başarısız";
                    processStatus.Status = false;
                    processStatus.Result = "Constant sonucu boş!";
                }
            }
            catch (Exception Error)
            {
                processStatus.Message.Header = "Constant İşlemleri";
                processStatus.Message.Text = "Başarısız";
                processStatus.Status = false;
                processStatus.Result = null;
                processStatus.Error = Error;
            }
            return Request.CreateResponse(HttpStatusCode.OK, processStatus);
        }

        public HttpResponseMessage GetCurrencies()
        {
            ProcessResult processStatus = new ProcessResult();
            try
            {
                var rslt = _currencyService.GetAllCurrencies().Select(x => new { Key = x.CurrencyId, Name = x.CurrencyName, FullName = x.CurrencyFullName }).ToList();
                if (rslt != null)
                {
                    processStatus.Result = rslt;
                    processStatus.ActiveResultRowCount = rslt.Count;
                    processStatus.TotolRowCount = processStatus.ActiveResultRowCount;
                    processStatus.Message.Header = "Currencies İşlemleri";
                    processStatus.Message.Text = "Başarılı";
                    processStatus.Status = true;
                }
                else
                {
                    processStatus.Message.Header = "Currencies İşlemleri";
                    processStatus.Message.Text = "Başarısız";
                    processStatus.Status = false;
                    processStatus.Result = "Currencies sonucu boş!";
                }
            }
            catch (Exception Error)
            {
                processStatus.Message.Header = "Currencies İşlemleri";
                processStatus.Message.Text = "Başarısız";
                processStatus.Status = false;
                processStatus.Result = null;
                processStatus.Error = Error;
            }
            return Request.CreateResponse(HttpStatusCode.OK, processStatus);
        }

        public HttpResponseMessage GetStoreActivityType()
        {
            ProcessResult processStatus = new ProcessResult();
            try
            {
                var constantList = _activityTypeService.GetAllActivityTypes().OrderBy(x => x.Order).ToList();
                if (constantList != null)
                {
                    processStatus.Result = constantList.Select(x => new
                    {
                        Id = (int)x.ActivityTypeId,
                        Name = x.ActivityName,
                        Order = x.Order
                    });
                    processStatus.ActiveResultRowCount = constantList.Count;
                    processStatus.TotolRowCount = processStatus.ActiveResultRowCount;
                    processStatus.Message.Header = "ActivityType İşlemleri";
                    processStatus.Message.Text = "Başarılı";
                    processStatus.Status = true;
                }
                else
                {
                    processStatus.Message.Header = "ActivityType İşlemleri";
                    processStatus.Message.Text = "Başarısız";
                    processStatus.Status = false;
                    processStatus.Result = "ActivityType sonucu boş!";
                }
            }
            catch (Exception Error)
            {
                processStatus.Message.Header = "ActivityType İşlemleri";
                processStatus.Message.Text = "Başarısız";
                processStatus.Status = false;
                processStatus.Result = null;
                processStatus.Error = Error;
            }
            return Request.CreateResponse(HttpStatusCode.OK, processStatus);
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

        public HttpResponseMessage GetAllProductComplainType()
        {
            ProcessResult processStatus = new ProcessResult();
            try
            {
                var result = _productComplainService.GetAllProductComplainType().Select(x =>
                    new ProductComplainTypeView
                    {
                        Name = x.Name,
                        ProductComplainTypeId = x.ProductComplainTypeId,
                        DisplayOrder = x.DisplayOrder
                    }).ToList();

                if (result != null)
                {
                    processStatus.Result = result;
                    processStatus.ActiveResultRowCount = result.Count;
                    processStatus.TotolRowCount = processStatus.ActiveResultRowCount;
                    processStatus.Message.Header = "ProductComplainType İşlemleri";
                    processStatus.Message.Text = "Başarılı";
                    processStatus.Status = true;
                }
                else
                {
                    processStatus.Message.Header = "ProductComplainType İşlemleri";
                    processStatus.Message.Text = "Başarısız";
                    processStatus.Status = false;
                    processStatus.Result = "Sorgu sonucu boş!";
                }
            }
            catch (Exception Error)
            {
                processStatus.Message.Header = "ProductComplainType İşlemleri";
                processStatus.Message.Text = "Başarısız";
                processStatus.Status = false;
                processStatus.Result = null;
                processStatus.Error = Error;
            }
            return Request.CreateResponse(HttpStatusCode.OK, processStatus);
        }
    }
}