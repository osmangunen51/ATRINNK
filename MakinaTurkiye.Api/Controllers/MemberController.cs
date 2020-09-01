﻿using MakinaTurkiye.Api.Helpers;
using MakinaTurkiye.Api.View;
using MakinaTurkiye.Core.Infrastructure;
using MakinaTurkiye.Entities.Tables.Common;
using MakinaTurkiye.Entities.Tables.Messages;
using MakinaTurkiye.Services.Common;
using MakinaTurkiye.Services.Members;
using MakinaTurkiye.Services.Messages;
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
        private readonly IPhoneService _phoneService;
        private readonly IMobileMessageService _mobileMessageService;

        public MemberController()
        {
            _memberService = EngineContext.Current.Resolve<IMemberService>();
            _addressService = EngineContext.Current.Resolve<IAddressService>();
            _phoneService = EngineContext.Current.Resolve<IPhoneService>();
            _mobileMessageService = EngineContext.Current.Resolve<IMobileMessageService>();
        }

        public HttpResponseMessage GetMemberInfo(int No)
        {
            ProcessResult processStatus = new ProcessResult();
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
                    var AddressList = new List<View.Address>();

                    foreach (var userAdress in userAddresses)
                    {
                        var cityData = new View.City();
                        var countryData = new View.Country();
                        var townData = new View.Town();
                        var localityData = new View.Locality();
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

                        var adress = new View.Address()
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
            View.ProcessResult processStatus = new View.ProcessResult();
            try
            {
                var result = _memberService.GetAllMembers();
                var memberInfoList = new List<MemberInfo>();
                foreach (var resul in result)
                {
                    var userAddresses = _addressService.GetAddressesByMainPartyId(resul.MainPartyId).ToList();
                    var AddressList = new List<View.Address>();

                    foreach (var userAdress in userAddresses)
                    {
                        var cityData = new View.City();
                        var countryData = new View.Country();
                        var townData = new View.Town();
                        var localityData = new View.Locality();
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

                        var adress = new View.Address()
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
            catch (Exception ex)
            {
                processStatus.Message.Header = "Kullanıcı İşlemleri";
                processStatus.Message.Text = "Başarısız";
                processStatus.Status = false;
                processStatus.Result = null;
                processStatus.Error = ex;
            }
            return Request.CreateResponse(HttpStatusCode.OK, processStatus);
        }

        public HttpResponseMessage ChangePassword(ChangePasswordModel model)
        {
            View.ProcessResult processStatus = new View.ProcessResult();

            try
            {
                var member = _memberService.GetMemberByMainPartyId(model.MemberMainPartyId);
                if (member != null && model.NewPassword == model.NewPasswordAgain && member.MemberPassword == model.OldPassword && model.Email == member.MemberEmail)
                {
                    member.MemberPassword = model.NewPassword;
                    _memberService.UpdateMember(member);

                    processStatus.Message.Header = "Kullanıcı şifre işlemleri";
                    processStatus.Message.Text = "Başarılı";
                    processStatus.Status = true;
                    processStatus.Result = "Şifre değiştirme işlemi başarılı bir şekilde gerçekleşmiştir";
                }
                else
                {
                    processStatus.Message.Header = "Kullanıcı şifre işlemleri";
                    processStatus.Message.Text = "Başarısız";
                    processStatus.Status = false;
                    processStatus.Result = "Şifre değiştirme işlemi başarısızdır";
                }
            }
            catch (Exception ex)
            {
                processStatus.Status = false;
                processStatus.Error = ex;
                processStatus.Message.Header = "Kullanıcı şifre işlemleri";
                processStatus.Message.Text = string.Format("Hata Oluştu : {0}", ex.Message);
            }

            return Request.CreateResponse(HttpStatusCode.OK, processStatus);
        }

        public HttpResponseMessage UpdatePersonalInfo(UpdatePeronalInfoModel model)
        {
            View.ProcessResult processStatus = new View.ProcessResult();

            try
            {
                var member = _memberService.GetMemberByMainPartyId(model.MemberMainPartyId);
                if (member != null && model.Email == member.MemberEmail && model.GenderWoman != model.GenderMan && member.MemberPassword == model.Password)
                {
                    member.MemberName = model.Name;
                    member.MemberSurname = model.Surname;
                    member.BirthDate = model.BirthDate;
                    member.Gender = model.GenderWoman;
                    _memberService.UpdateMember(member);

                    processStatus.Message.Header = "Kullanıcı Kişisel bilgi işlemleri";
                    processStatus.Message.Text = "Başarılı";
                    processStatus.Status = true;
                    processStatus.Result = "Kişisel bilgi güncelleme işlemi başarılı bir şekilde gerçekleşmiştir";
                }
                else
                {
                    processStatus.Message.Header = "Kullanıcı Kişisel bilgi işlemleri";
                    processStatus.Message.Text = "Başarısız";
                    processStatus.Status = false;
                    processStatus.Result = "Kişisel bilgi güncelleme işlemi başarısızdır";
                }
            }
            catch (Exception ex)
            {
                processStatus.Status = false;
                processStatus.Error = ex;
                processStatus.Message.Header = "Kullanıcı Kişisel bilgi işlemleri";
                processStatus.Message.Text = string.Format("Hata Oluştu : {0}", ex.Message);
            }

            return Request.CreateResponse(HttpStatusCode.OK, processStatus);
        }

        public HttpResponseMessage UpdatePersonalAddressInfo(UpdatePersonalAddressInfo model)
        {
            View.ProcessResult processStatus = new View.ProcessResult();

            try
            {
                var member = _memberService.GetMemberByMainPartyId(model.MemberMainPartyId);
                if (member != null && model.Email == member.MemberEmail && member.MemberPassword == model.Password)
                {
                    var userAddresses = _addressService.GetAddressesByMainPartyId(model.MemberMainPartyId).ToList();

                    //Adres işlemleri
                    if (userAddresses.Count == 0)
                    {
                        var newAdress = new Entities.Tables.Common.Address()
                        {
                            CountryId = model.Country.CountryId,
                            CityId = model.City.CityId,
                            LocalityId = model.Locality.LocalityId,
                            TownId = model.Town.TownId,
                            ApartmentNo = model.ApartmentNo,
                            Avenue = model.Avenue,
                            DoorNo = model.DoorNo,
                            PostCode = model.PostCode,
                            Street = model.Street,
                            MainPartyId = model.MemberMainPartyId,
                            AddressDefault = true,
                            StoreDealerId = null,
                        };
                        _addressService.InsertAdress(newAdress);
                    }
                    else
                    {
                        foreach (var userAdress in userAddresses)
                        {
                            userAdress.CountryId = model.Country.CountryId;
                            userAdress.CityId = model.City.CityId;
                            userAdress.LocalityId = model.Locality.LocalityId;
                            userAdress.TownId = model.Town.TownId;
                            userAdress.ApartmentNo = model.ApartmentNo;
                            userAdress.Avenue = model.Avenue;
                            userAdress.DoorNo = model.DoorNo;
                            userAdress.PostCode = model.PostCode;
                            userAdress.Street = model.Street;

                            _addressService.UpdateAddress(userAdress);
                        }
                    }

                    //Telefon işlemleri
                    var userPhones = _phoneService.GetPhonesByMainPartyId(model.MemberMainPartyId).ToList();
                    var isCurrentGsmSame = userPhones.Where(x => x.PhoneType == (int)PhoneTypeEnum.Gsm &&
                                                                    x.active == 1 &&
                                                                    x.PhoneCulture == model.GsmCountryCode &&
                                                                    x.PhoneAreaCode == model.GsmAreaCode &&
                                                                    x.PhoneNumber == model.Gsm
                                                                    ).SingleOrDefault();

                    if (userPhones.Count != 0)
                    {
                        foreach (var phone in userPhones)
                            _phoneService.DeletePhone(phone);
                    }

                    var phoneList = new List<Phone>();
                    SmsHelper sms = new SmsHelper();
                    string activeCode = sms.CreateActiveCode();

                    var phoneGsm = new Phone()
                    {
                        MainPartyId = model.MemberMainPartyId,
                        PhoneCulture = model.GsmCountryCode,
                        PhoneAreaCode = model.GsmAreaCode,
                        PhoneNumber = model.Gsm,
                        PhoneType = (int)PhoneTypeEnum.Gsm,
                        active = isCurrentGsmSame?.active,
                        ActivationCode = isCurrentGsmSame != null ? isCurrentGsmSame.ActivationCode : activeCode,
                    };
                    var phoneWhatsapp = new Phone()
                    {
                        MainPartyId = model.MemberMainPartyId,
                        PhoneCulture = model.WhatsappGsmCountryCode,
                        PhoneAreaCode = model.WhatsappGsmAreaCode,
                        PhoneNumber = model.WhatsappGsm,
                        PhoneType = (int)PhoneTypeEnum.Whatsapp,
                    };
                    var phone1 = new Phone()
                    {
                        MainPartyId = model.MemberMainPartyId,
                        PhoneCulture = model.Phone1CountryCode,
                        PhoneAreaCode = model.Phone1AreaCode,
                        PhoneNumber = model.Phone1,
                        PhoneType = (int)PhoneTypeEnum.Phone,
                    };
                    var phone2 = new Phone()
                    {
                        MainPartyId = model.MemberMainPartyId,
                        PhoneCulture = model.Phone2CountryCode,
                        PhoneAreaCode = model.Phone2AreaCode,
                        PhoneNumber = model.Phone2,
                        PhoneType = (int)PhoneTypeEnum.Phone,
                    };
                    var fax = new Phone()
                    {
                        MainPartyId = model.MemberMainPartyId,
                        PhoneCulture = model.FaxCountryCode,
                        PhoneAreaCode = model.FaxAreaCode,
                        PhoneNumber = model.Fax,
                        PhoneType = (int)PhoneTypeEnum.Fax,
                    };

                    phoneList.Add(phoneGsm);
                    phoneList.Add(phoneWhatsapp);
                    phoneList.Add(phone1);
                    phoneList.Add(phone2);
                    phoneList.Add(fax);
                    var phonesWillAdd = phoneList.Where(x => (!string.IsNullOrEmpty(x.PhoneCulture)) && (!string.IsNullOrEmpty(x.PhoneAreaCode)) && (!string.IsNullOrEmpty(x.PhoneNumber))).ToList();

                    var addedGsmNo = phonesWillAdd.Where(x => x.PhoneType == (int)PhoneTypeEnum.Gsm).SingleOrDefault();

                    if (addedGsmNo != null && addedGsmNo.active != 1)
                    {
                        MobileMessage messageTmp = _mobileMessageService.GetMobileMessageByMessageName("telefononayi");
                        string messageMobile = messageTmp.MessageContent.Replace("#isimsoyisim#", member.MemberName + " " + member.MemberSurname).Replace("#aktivasyonkodu#", activeCode);

                        sms.SendPhoneMessage(phoneGsm.PhoneCulture + phoneGsm.PhoneAreaCode + phoneGsm.PhoneNumber, messageMobile);

                        processStatus.Message.Header = "Kullanıcı adres güncelleme işlemleri";
                        processStatus.Message.Text = "Başarılı. Aktivasyon gerekli";
                        processStatus.Status = true;
                        processStatus.Result = "Bilgileriniz başarıyla güncellenmiştir. Telefonunuza gelen Aktivasyon kodunu kullanarak aktif ediniz.";
                    }
                    else
                    {
                        processStatus.Message.Header = "Kullanıcı adres güncelleme işlemleri";
                        processStatus.Message.Text = "Başarılı";
                        processStatus.Status = true;
                        processStatus.Result = "Bilgileriniz başarıyla güncellenmiştir.";
                    }

                    foreach (var phoneWillAdd in phonesWillAdd)
                        _phoneService.InsertPhone(phoneWillAdd);
                }
                else
                {
                    processStatus.Message.Header = "Kullanıcı adres güncelleme işlemleri";
                    processStatus.Message.Text = "Başarısız";
                    processStatus.Status = false;
                    processStatus.Result = "Bilgilerinizi güncelleme işlemi başarısızdır";
                }
            }
            catch (Exception ex)
            {
                processStatus.Status = false;
                processStatus.Error = ex;
                processStatus.Message.Header = "Kullanıcı adres güncelleme işlemleri";
                processStatus.Message.Text = string.Format("Hata Oluştu : {0}", ex.Message);
            }

            return Request.CreateResponse(HttpStatusCode.OK, processStatus);
        }

        public HttpResponseMessage ActivatePhone(PhoneActivationModel model)
        {
            ProcessResult processStatus = new ProcessResult();

            try
            {
                var userPhone = _phoneService.GetPhoneByActivationCode(model.ActivationCode);

                if (userPhone.PhoneCulture == model.GsmCountryCode &&
                    userPhone.PhoneAreaCode == model.GsmAreaCode &&
                    userPhone.PhoneNumber == model.Gsm
                    )
                {
                    userPhone.active = 1;
                    _phoneService.UpdatePhone(userPhone);

                    processStatus.Message.Header = "Activate Phone";
                    processStatus.Message.Text = "İşlem başarılı";
                    processStatus.Status = true;
                    processStatus.Result = "Bilgileriniz başarıyla güncellenmiştir.";
                }
                else
                {
                    processStatus.Message.Header = "Activate Phone";
                    processStatus.Message.Text = "İşlem başarısız";
                    processStatus.Status = false;
                    processStatus.Result = "Yanlış Aktivasyon kodu girdiniz !";
                }
            }
            catch (Exception ex)
            {
                processStatus.Message.Header = "Activate Phone";
                processStatus.Message.Text = "İşlem başarısız";
                processStatus.Status = false;
                processStatus.Result = "Telefon aktivasyon işlemi başarısız.";
            }
            return Request.CreateResponse(HttpStatusCode.OK, processStatus);
        }

        public HttpResponseMessage UpdatePersonalEmail(ChangeEmailModel model)
        {
            ProcessResult processStatus = new ProcessResult();

            try
            {
                var member = _memberService.GetMemberByMemberEmail(model.OldEmail);
                if (member != null &&
                    (!string.IsNullOrEmpty(model.NewEmailAgain)) &&
                    model.NewEmailAgain == model.NewEmail &&
                    model.MemberMainPartyId == member.MainPartyId &&
                    member.MemberPassword == model.MemberPassword
                    )
                {
                    member.MemberEmail = model.NewEmail;
                    _memberService.UpdateMember(member);
                    processStatus.Message.Header = "Update Email";
                    processStatus.Message.Text = "İşlem başarılı";
                    processStatus.Status = true;
                    processStatus.Result = "Bilgileriniz başarıyla güncellenmiştir.";
                }
                else
                {
                    processStatus.Message.Header = "Update Email";
                    processStatus.Message.Text = "İşlem başarısız";
                    processStatus.Status = false;
                    processStatus.Result = "Güncelleme esnasında hata ile karşılaşıldı!";
                }
            }
            catch (Exception ex)
            {
                processStatus.Message.Header = "Update Email";
                processStatus.Message.Text = "İşlem başarısız." + ex;
                processStatus.Status = false;
                processStatus.Result = "Güncelleme esnasında hata ile karşılaşıldı!";
            }
            return Request.CreateResponse(HttpStatusCode.OK, processStatus);
        }
    }
}