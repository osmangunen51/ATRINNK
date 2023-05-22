using Trinnk.Entities.Tables.Common;
using System.Collections.Generic;

namespace Trinnk.Services.Common
{
    public interface IAddressService : ICachingSupported
    {
        IList<Locality> GetLocalitiesByLocalityIds(List<int> localityIds);

        IList<City> GetCitiesByCityIds(List<int> cityIds);

        IList<Locality> GetAllLocality();

        IList<City> GetAllCities();

        IList<Locality> GetLocalitiesByCityId(int cityId);

        Locality GetLocalityByLocalityId(int localityId);

        IList<Country> GetCountriesByCountryIds(List<int> countryIds);

        IList<Country> GetCountries();

        Country GetCountryByCountryId(int countryId);

        IList<City> GetCitiesByCountryId(int countryId);

        City GetCityByCityId(int cityId);

        IList<District> GetDistrictsByLocalityId(int localityId);

        IList<District> GetDistrictsByCityId(int localityId);

        District GetDistrictByDistrictId(int districtId);

        IList<Town> GetTownsByLocalityId(int localityId);

        Town GetTownByTownId(int townId);

        Address GetFisrtAddressByMainPartyId(int mainPartyId);

        Address GetAddressByAddressId(int addressId);

        IList<Address> GetAddressesByMainPartyId(int mainPartyId);

        IList<Country> GetAllCountries(bool showHidden = false);

        Address GetAddressByStoreDealerId(int storeDealerId);

        int GetSingleCityIdByCityName(string cityName);

        List<int> GetSingleLocalityIdByLocalityName(List<string> localityNames);

        void UpdateAddress(Address address);

        void InsertAdress(Address address);

        void DeleteAddress(Address address);

        List<AddressType> GetAddressTypes();

        List<Address> GetAddressByStoreDealerIds(List<int> storeDealerIds);
    }
}