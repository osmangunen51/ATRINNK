using MakinaTurkiye.Api.View;
using MakinaTurkiye.Core.Infrastructure;
using MakinaTurkiye.Entities.Tables.Members;
using MakinaTurkiye.Services.Common;
using MakinaTurkiye.Services.Members;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;

namespace MakinaTurkiye.Api.Controllers
{
    public class MemberController : BaseApiController
    {
        private readonly IMemberService _memberService;
        private readonly IAddressService _addressService;

        public MemberController()
        {
            _memberService = EngineContext.Current.Resolve<IMemberService>();
            _addressService = EngineContext.Current.Resolve<IAddressService>();
        }

        public HttpResponseMessage GetMemberInfo(int No)
        {
            ProcessStatus processStatus = new ProcessStatus();
            try
            {
                //var result = _memberService.GetAllMembers();
                var result = _memberService.GetMembersByMainPartyId(No);
                //var result = _memberService.GetMemberByMainPartyId(Id);
                var memberInfoList = new List<MemberInfo>();
                //var memberList = new List<Member>();
                //memberList.Add(result);
                foreach (var resul in result)
                {
                    var userAddresses = _addressService.GetAddressesByMainPartyId(resul.MainPartyId).ToList();
                    var AddressList = new List<Address>();

                    foreach (var userAdress in userAddresses)
                    {
                        var cityData = new City();
                        var countryData = new Country();
                        var townData = new Town();
                        var localityData = new Locality();
                        if (userAdress.Locality != null && userAdress.Locality.LocalityId > 0)
                        {
                            var locality = _addressService.GetLocalityByLocalityId(userAdress.Locality.LocalityId);
                            if (locality != null)
                            {
                                localityData.LocalityId = locality.LocalityId;
                                localityData.LocalityName = locality.LocalityName;

                            }
                        }

                        if (userAdress.City != null && userAdress.City.CityId > 0)
                        {
                            var city = _addressService.GetCityByCityId(userAdress.City.CityId);
                            if (city != null)
                            {
                                cityData.CityId = city.CityId;
                                cityData.CityName = city.CityName;
                            }
                        }

                        if (userAdress.Country != null && userAdress.Country.CountryId > 0)
                        {
                            var country = _addressService.GetCountryByCountryId(userAdress.Country.CountryId);
                            if (country != null)
                            {
                                countryData.CountryId = country.CountryId;
                                countryData.CountryName = country.CountryName;
                            }
                        }
                        if (userAdress.Town != null && userAdress.Town.TownId > 0)
                        {
                            var town = _addressService.GetTownByTownId(userAdress.Town.TownId);
                            if (town != null)
                            {
                                townData.TownId = town.TownId;
                                townData.TownName = town.TownName;
                            }
                        }

                        var adress = new Address()
                        {
                            City = cityData,
                            Locality = localityData,
                            Country = countryData,
                            Town = townData,
                            AddressId = userAdress.AddressId,
                            AdressDefault = userAdress.AddressDefault,
                            ApartmentNo = userAdress.ApartmentNo,
                            Avenue = userAdress.Avenue,
                            DoorNo = userAdress.DoorNo,
                            PostCode = userAdress.PostCode,
                            StoreDealerId = userAdress.StoreDealerId,
                            Street = userAdress.Street
                        };
                        AddressList.Add(adress);
                    }
                    var memberInfo = new MemberInfo()
                    {
                        Active = resul.Active,
                        BirthDate = resul.BirthDate,
                        Gender = resul.Gender.HasValue && resul.Gender.Value ? 1 : 0,
                        MainPartyId = resul.MainPartyId,
                        MemberEmail = resul.MemberEmail,
                        MemberName = resul.MemberName,
                        MemberNo = resul.MemberNo,
                        MemberPassword = null,
                        MemberSurname = resul.MemberSurname,
                        MemberTitleType = resul.MemberTitleType,
                        MemberType = resul.MemberType,
                        Address = AddressList
                    };

                    memberInfoList.Add(memberInfo);
                }

                //foreach (var item in Result)
                //{
                //    item.StoreLogo = ImageHelper.GetStoreLogoParh(item.MainPartyId, item.StoreLogo, 300);
                //}
                processStatus.Result = memberInfoList;
                processStatus.ActiveResultRowCount = memberInfoList.Count;
                processStatus.TotolRowCount = processStatus.ActiveResultRowCount;
                processStatus.Message.Header = "Kullanıcı İşlemleri";
                processStatus.Message.Text = "Başarılı";
                processStatus.Status = true;
            }
            catch (Exception Error)
            {
                processStatus.Message.Header = "Kullanıcı İşlemleri";
                processStatus.Message.Text = "Başarısız";
                processStatus.Status = false;
                processStatus.Result = null;
                processStatus.Error = Error;
            }
            return Request.CreateResponse(HttpStatusCode.OK, processStatus);
        }

        public HttpResponseMessage GetAllMembersInfo()
        {
            ProcessStatus processStatus = new ProcessStatus();
            try
            {
                var result = _memberService.GetAllMembers();
                var memberInfoList = new List<MemberInfo>();
                foreach (var resul in result)
                {
                    var userAddresses = _addressService.GetAddressesByMainPartyId(resul.MainPartyId).ToList();
                    var AddressList = new List<Address>();

                    foreach (var userAdress in userAddresses)
                    {
                        var cityData = new City();
                        var countryData = new Country();
                        var townData = new Town();
                        var localityData = new Locality();
                        if (userAdress.Locality != null && userAdress.Locality.LocalityId > 0)
                        {
                            var locality = _addressService.GetLocalityByLocalityId(userAdress.Locality.LocalityId);
                            if (locality != null)
                            {
                                localityData.LocalityId = locality.LocalityId;
                                localityData.LocalityName = locality.LocalityName;

                            }
                        }

                        if (userAdress.City != null && userAdress.City.CityId > 0)
                        {
                            var city = _addressService.GetCityByCityId(userAdress.City.CityId);
                            if (city != null)
                            {
                                cityData.CityId = city.CityId;
                                cityData.CityName = city.CityName;
                            }
                        }

                        if (userAdress.Country != null && userAdress.Country.CountryId > 0)
                        {
                            var country = _addressService.GetCountryByCountryId(userAdress.Country.CountryId);
                            if (country != null)
                            {
                                countryData.CountryId = country.CountryId;
                                countryData.CountryName = country.CountryName;
                            }
                        }
                        if (userAdress.Town != null && userAdress.Town.TownId > 0)
                        {
                            var town = _addressService.GetTownByTownId(userAdress.Town.TownId);
                            if (town != null)
                            {
                                townData.TownId = town.TownId;
                                townData.TownName = town.TownName;
                            }
                        }

                        var adress = new Address()
                        {
                            City = cityData,
                            Locality = localityData,
                            Country = countryData,
                            Town = townData,
                            AddressId = userAdress.AddressId,
                            AdressDefault = userAdress.AddressDefault,
                            ApartmentNo = userAdress.ApartmentNo,
                            Avenue = userAdress.Avenue,
                            DoorNo = userAdress.DoorNo,
                            PostCode = userAdress.PostCode,
                            StoreDealerId = userAdress.StoreDealerId,
                            Street = userAdress.Street
                        };
                        AddressList.Add(adress);
                    }
                    var memberInfo = new MemberInfo()
                    {
                        Active = resul.Active,
                        BirthDate = resul.BirthDate,
                        Gender = resul.Gender.HasValue && resul.Gender.Value ? 1 : 0,
                        MainPartyId = resul.MainPartyId,
                        MemberEmail = resul.MemberEmail,
                        MemberName = resul.MemberName,
                        MemberNo = resul.MemberNo,
                        MemberPassword = null,
                        MemberSurname = resul.MemberSurname,
                        MemberTitleType = resul.MemberTitleType,
                        MemberType = resul.MemberType,
                        Address = AddressList
                    };

                    memberInfoList.Add(memberInfo);
                }

                //foreach (var item in Result)
                //{
                //    item.StoreLogo = ImageHelper.GetStoreLogoParh(item.MainPartyId, item.StoreLogo, 300);
                //}
                processStatus.Result = memberInfoList;
                processStatus.ActiveResultRowCount = memberInfoList.Count;
                processStatus.TotolRowCount = processStatus.ActiveResultRowCount;
                processStatus.Message.Header = "Kullanıcı İşlemleri";
                processStatus.Message.Text = "Başarılı";
                processStatus.Status = true;
            }
            catch (Exception Error)
            {
                processStatus.Message.Header = "Kullanıcı İşlemleri";
                processStatus.Message.Text = "Başarısız";
                processStatus.Status = false;
                processStatus.Result = null;
                processStatus.Error = Error;
            }
            return Request.CreateResponse(HttpStatusCode.OK, processStatus);
        }
    }

}