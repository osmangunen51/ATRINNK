using MakinaTurkiye.Caching;
using MakinaTurkiye.Core.Data;
using MakinaTurkiye.Entities.Tables.Common;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace MakinaTurkiye.Services.Common
{
    public class AddressService : BaseService, IAddressService
    {

        #region Constants

        private const string ADDRESSES_BY_MAINPARTY_ID_KEY = "makinaturkiye.address.bymainpartyId-{0}";
        private const string ADDRESSES_BY_FIRST_MAINPARTY_ID_KEY = "makinaturkiye.address.firstaddress.bymainpartyId-{0}";

        private const string LOCALITIES_BY_CITY_ID_KEY = "makinaturkiye.locality.bycityId-{0}";
        private const string CITIES_BY_COUNTRY_ID_KEY = "makinaturkiye.city.bycountryId-{0}";
        private const string DISTRICTS_BY_CITY_ID_KEY = "makinaturkiye.district.bycityId-{0}";
        private const string DISTRICTS_BY_LOCALITY_ID_KEY = "makinaturkiye.district.bylocalityId-{0}";
        private const string TOWNS_BY_LOCALITY_ID_KEY = "makinaturkiye.town.bylocalityId-{0}";
        private const string ADDRESSES_BY_STORE_DEALER_ID_KEY = "makinaturkiye.address.bystoredealerid-{0}";
        private const string CITYES_BY_CITY_IDS_KEY = "makinaturkiye.city.bycityids-{0}";

        private const string COUNTRIES_BY_COUNTRY_ID_KEY = "makinaturkiye.country.bycountryid-{0}";
        private const string CITIES_BY_CITIY_ID_KEY = "makinaturkiye.citiy.bycitiyid-{0}";
        private const string LOCALITIES_BY_LOCALITY_ID_KEY = "makinaturkiye.locality.bylocalityid-{0}";

        #endregion

        #region Fileds

        private readonly IRepository<City> _cityRepository;
        private readonly IRepository<Locality> _localityRepository;
        private readonly IRepository<Country> _countryRepository;
        private readonly IRepository<Address> _addressRepository;
        private readonly IRepository<Town> _townRepository;
        private readonly IRepository<AddressType> _addressTypeRepository;
        private readonly IRepository<District> _districtRepository;
        private readonly ICacheManager _cacheManager;

        #endregion

        #region Ctor

        public AddressService(IRepository<City> cityRepository, IRepository<Locality> localityRepository, IRepository<Country> countryRepository,
            IRepository<Address> addressRepository, IRepository<Town> townRepository, ICacheManager cacheManager,
            IRepository<AddressType> addressTypeRepository, IRepository<District> districtRepository) : base(cacheManager)
        {
            _cityRepository = cityRepository;
            _localityRepository = localityRepository;
            _countryRepository = countryRepository;
            _addressRepository = addressRepository;
            _townRepository = townRepository;
            _cacheManager = cacheManager;
            _addressTypeRepository = addressTypeRepository;
            _districtRepository = districtRepository;

        }

        #endregion

        #region Utilities

        private void RemoveAddressCache(Address address)
        {
            string fisrtMainPartyIdKey = string.Format(ADDRESSES_BY_FIRST_MAINPARTY_ID_KEY, address.MainPartyId);
            string mainPartyIdKey = string.Format(ADDRESSES_BY_MAINPARTY_ID_KEY, address.MainPartyId);

            _cacheManager.Remove(fisrtMainPartyIdKey);
            _cacheManager.Remove(mainPartyIdKey);

        }

        #endregion

        #region Methods

        #region Address

        public Address GetAddressByStoreDealerId(int storeDealerId)
        {
            if (storeDealerId == 0)
                throw new ArgumentNullException("storeDealerId");

            string key = string.Format(ADDRESSES_BY_STORE_DEALER_ID_KEY, storeDealerId);
            return _cacheManager.Get(key, () =>
            {
                var query = _addressRepository.Table;
                return query.FirstOrDefault(x => x.StoreDealerId == storeDealerId);
            });
        }

        public List<Address> GetAddressByStoreDealerIds(List<int> storeDealerIds)
        {
            if (storeDealerIds.Count == 0)
                throw new ArgumentNullException("storeDealerIds");

            var query = _addressRepository.Table;
            query = query.Where(x => storeDealerIds.Contains(x.StoreDealerId.Value));
            return query.ToList();
        }

        public Address GetFisrtAddressByMainPartyId(int mainPartyId)
        {
            if (mainPartyId == 0)
                return null;

            string key = string.Format(ADDRESSES_BY_FIRST_MAINPARTY_ID_KEY, mainPartyId);
            return _cacheManager.Get(key, () =>
            {
                var query = _addressRepository.Table;

                query = query.Include(a => a.Phones);
                query = query.Include(x => x.Country);
                query = query.Include(x => x.City);
                query = query.Include(x => x.Locality);

                query = query.Include(x => x.Town);
                return query.FirstOrDefault(a => a.MainPartyId == mainPartyId);
            });
        }

        public IList<Address> GetAddressesByMainPartyId(int mainPartyId)
        {
            if (mainPartyId <= 0)
                throw new ArgumentNullException("mainPartyId");


            var query = _addressRepository.Table;


            query = query.Include(a => a.Phones);
            query = query.Include(x => x.Country);
            query = query.Include(x => x.City);
            query = query.Include(x => x.Locality);

            query = query.Where(a => a.MainPartyId == mainPartyId);

            var addressess = query.ToList();
            return addressess;
        }

        public Address GetAddressByAddressId(int addressId)
        {
            if (addressId <= 0)
                throw new ArgumentNullException("addressId");

            var query = _addressRepository.Table;

            query = query.Include(a => a.Phones);

            var address = query.FirstOrDefault(a => a.AddressId == addressId);
            return address;
        }

        public void InsertAdress(Address address)
        {
            if (address == null)
                throw new ArgumentNullException("address");

            _addressRepository.Insert(address);

            RemoveAddressCache(address);
        }

        public void DeleteAddress(Address address)
        {
            if (address == null)
                throw new ArgumentNullException("address");

            _addressRepository.Delete(address);

            RemoveAddressCache(address);

        }

        public void UpdateAddress(Address address)
        {
            if (address == null)
                throw new ArgumentNullException("adress");

            _addressRepository.Update(address);

            RemoveAddressCache(address);
        }


        #endregion

        public IList<Locality> GetLocalitiesByLocalityIds(List<int> localIds)
        {
            if (localIds == null || localIds.Count == 0)
                return new List<Locality>();

            var result = from ct in _localityRepository.Table
                         where localIds.Contains(ct.LocalityId)
                         orderby ct.LocalityName
                         select ct;

            return result.ToList();
        }

        public Locality GetLocalityByLocalityId(int localityId)
        {
            if (localityId <= 0)
                throw new ArgumentNullException("localityId");

            string key = string.Format(LOCALITIES_BY_LOCALITY_ID_KEY, localityId);

            return _cacheManager.Get(key, () => 
            {
                var query = _localityRepository.Table;

                var locality = query.FirstOrDefault(l => l.LocalityId == localityId);
                return locality;

            });
        }

        public IList<City> GetCitiesByCityIds(List<int> cityIds)
        {
            if (cityIds == null || cityIds.Count <= 0)
                throw new ArgumentNullException("cityIds");

            string cityIdsText = string.Empty;
            foreach (var item in cityIds)
            {
                cityIdsText += string.Format("{0},", item.ToString());
            }
            cityIdsText = cityIdsText.Substring(0, cityIdsText.Length - 1);

            string key = string.Format(CITYES_BY_CITY_IDS_KEY, cityIdsText);
            return _cacheManager.Get(key, () =>
            {
                var result = from ct in _cityRepository.Table
                             where cityIds.Contains(ct.CityId)
                             orderby ct.CityName
                             select ct;

                return result.ToList();
            });
        }

        public IList<Country> GetCountriesByCountryIds(List<int> countryIds)
        {
            if (countryIds == null || countryIds.Count == 0)
                return new List<Country>();

            var result = from ct in _countryRepository.Table
                         where countryIds.Contains(ct.CountryId)
                         orderby ct.CountryName
                         select ct;

            return result.ToList();
        }

        public Country GetCountryByCountryId(int countryId)
        {
            if (countryId <= 0)
                throw new ArgumentNullException("countryId");

            string key = string.Format(COUNTRIES_BY_COUNTRY_ID_KEY, countryId);

            return _cacheManager.Get(key, () => 
            {
                var query = _countryRepository.Table;

                var country = query.FirstOrDefault(c => c.CountryId == countryId);
                return country;
            });
        }


        public int GetSingleCityIdByCityName(string cityName)
        {
            var city = _cityRepository.Table.FirstOrDefault(p => p.CityName == cityName);
            return city == null ? 0 : city.CityId;
        }


        public List<int> GetSingleLocalityIdByLocalityName(List<string> localityNames)
        {
            var localityList = _localityRepository.Table.ToList();
            return (from locality in localityList where localityNames.Contains(locality.LocalityName) select locality.LocalityId).ToList();
        }

        public IList<Locality> GetLocalitiesByCityId(int cityId)
        {
            if (cityId == 0)
                return new List<Locality>();

            string key = string.Format(LOCALITIES_BY_CITY_ID_KEY, cityId);
            return _cacheManager.Get(key, () =>
            {
                var result = _localityRepository.Table.Where(l => l.CityId == cityId);
                result = result.OrderBy(l => l.LocalityName);

                return result.ToList();
            });
        }

        public IList<City> GetCitiesByCountryId(int countryId)
        {
            if (countryId == 0)
                return new List<City>();

            string key = string.Format(CITIES_BY_COUNTRY_ID_KEY, countryId);
            return _cacheManager.Get(key, () =>
            {
                var result = _cityRepository.Table.Where(l => l.CountryId == countryId);
                result = result.OrderBy(c => c.CityOrder).ThenBy(c => c.CityName);
                return result.ToList();
            });
        }

        public City GetCityByCityId(int cityId)
        {
            if (cityId <= 0)
                throw new ArgumentNullException("cityId");

            string key = string.Format(CITIES_BY_CITIY_ID_KEY, cityId);

            return _cacheManager.Get(key, () => 
            {
                var query = _cityRepository.Table;

                var city = query.FirstOrDefault(c => c.CityId == cityId);
                return city;

            });
        }
        public IList<District> GetDistrictsByCityId(int cityId)
        {
            if (cityId <= 0)
                return new List<District>();


            string key = string.Format(DISTRICTS_BY_LOCALITY_ID_KEY, cityId);
            return _cacheManager.Get(key, () =>
            {

                var query = _districtRepository.Table;
                query = query.Where(d => d.CityId == cityId);
                var districts = query.ToList();
                return districts;

            });
        }

        public IList<District> GetDistrictsByLocalityId(int localityId)
        {
            if (localityId <= 0)
                return new List<District>();


            string key = string.Format(DISTRICTS_BY_LOCALITY_ID_KEY, localityId);
            return _cacheManager.Get(key, () =>
            {

                var query = _districtRepository.Table;
                query = query.Where(d => d.LocalityId == localityId);

                var districts = query.ToList();
                return districts;

            });

        }

        public District GetDistrictByDistrictId(int districtId)
        {
            if (districtId <= 0)
                throw new ArgumentNullException("districtId");

            var query = _districtRepository.Table;

            var district = query.FirstOrDefault(c => c.DistrictId == districtId);
            return district;
        }

        public IList<Town> GetTownsByLocalityId(int localityId)
        {
            if (localityId == 0)
                return new List<Town>();


            string key = string.Format(TOWNS_BY_LOCALITY_ID_KEY, localityId);
            return _cacheManager.Get(key, () =>
            {

                var query = _townRepository.Table;
                query = query.Where(t => t.LocalityId == localityId);
                query = query.OrderBy(t => t.TownName);

                var towns = query.ToList();
                return towns;

            });
        }

        public Town GetTownByTownId(int townId)
        {
            if (townId <= 0)
                throw new ArgumentNullException("townId");

            var query = _townRepository.Table;

            var town = query.FirstOrDefault(c => c.TownId == townId);
            return town;
        }

        public IList<Country> GetAllCountries(bool showHidden=false)
        {
            var query = _countryRepository.Table;

            if (!showHidden)
                query = query.Where(c => c.Active==true);

            query = query.OrderBy(c => c.CountryOrder).ThenBy(c => c.CountryName);
            var countries = query.ToList();
            return countries;
        }

        public List<AddressType> GetAddressTypes()
        {
            var query = _addressTypeRepository.Table;
            return query.ToList();
        }

        public IList<Country> GetCountries()
        {
            var query = _countryRepository.Table;
            return query.OrderBy(x => x.CountryOrder).ThenBy(x => x.CountryName).ToList();
        }

        #endregion

    }
}
