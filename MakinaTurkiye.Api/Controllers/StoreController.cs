using MakinaTurkiye.Api.ExtentionsMethod;
using MakinaTurkiye.Api.Helpers;
using MakinaTurkiye.Api.View;
using MakinaTurkiye.Core;
using MakinaTurkiye.Core.Infrastructure;
using MakinaTurkiye.Entities.Tables.Catalog;
using MakinaTurkiye.Entities.Tables.Common;
using MakinaTurkiye.Entities.Tables.Media;
using MakinaTurkiye.Entities.Tables.Stores;
using MakinaTurkiye.Services.Catalog;
using MakinaTurkiye.Services.Common;
using MakinaTurkiye.Services.Media;
using MakinaTurkiye.Services.Members;
using MakinaTurkiye.Services.Messages;
using MakinaTurkiye.Services.Stores;
using MakinaTurkiye.Utilities.FileHelpers;
using MakinaTurkiye.Utilities.HttpHelpers;
using MakinaTurkiye.Utilities.ImageHelpers;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Hosting;
using System.Web.Http;
using MakinaTurkiye.Utilities.MailHelpers;
using MakinaTurkiye.Services.Settings;
using MakinaTurkiye.Services.Videos;

namespace MakinaTurkiye.Api.Controllers
{
    public class StoreController : BaseApiController
    {
        private readonly IStoreService _storeService;
        private readonly IStoreNewService _storeNewService;
        private readonly ICategoryService _categoryService;
        private readonly IProductService _productService;
        private readonly IPictureService _pictureService;
        private readonly IAddressService _addressService;
        private readonly IStoreActivityTypeService _storeActivityTypeService;
        private readonly IConstantService _constantService;
        private readonly IStoreInfoNumberShowService _storeNumberShowService;
        private readonly IPhoneService _phoneService;
        private readonly IStoreDealerService _storeDealerService;
        private readonly IAddressService _adressService;
        private readonly IStoreBrandService _storeBrandService;
        private readonly IDealarBrandService _dealarBrandService;
        private readonly IMemberService _memberService;
        private readonly ICertificateTypeService _certificateTypeService;
        private readonly IStoreCatologFileService _storeCatologFileService;
        private readonly IMemberStoreService _memberStoreService;
        private readonly IMessagesMTService _messageMTService;
        private readonly IMemberSettingService _memberSettingService;
        private readonly IStoreChangeHistoryService _storeChangeHistoryService;
        private readonly IStoreActivityCategoryService _storeActivityCategoryService;
        private readonly IActivityTypeService _activityTypeService;
        private readonly ICategoryPlaceChoiceService _categoryPlaceChoiceService;
        private readonly IStoreSectorService _storeSectorService;
        private readonly IVideoService _videoService;

        public StoreController()
        {
            _storeService = EngineContext.Current.Resolve<IStoreService>();
            _storeNewService = EngineContext.Current.Resolve<IStoreNewService>();
            _categoryService = EngineContext.Current.Resolve<ICategoryService>();
            _productService = EngineContext.Current.Resolve<IProductService>();
            _pictureService = EngineContext.Current.Resolve<IPictureService>();
            _addressService = EngineContext.Current.Resolve<IAddressService>();
            _storeActivityTypeService = EngineContext.Current.Resolve<IStoreActivityTypeService>();
            _constantService = EngineContext.Current.Resolve<IConstantService>();
            _storeNumberShowService = EngineContext.Current.Resolve<IStoreInfoNumberShowService>();
            _phoneService = EngineContext.Current.Resolve<IPhoneService>();
            _storeDealerService = EngineContext.Current.Resolve<IStoreDealerService>();
            _adressService = EngineContext.Current.Resolve<IAddressService>();
            _storeBrandService = EngineContext.Current.Resolve<IStoreBrandService>();
            _dealarBrandService = EngineContext.Current.Resolve<IDealarBrandService>();
            _memberService = EngineContext.Current.Resolve<IMemberService>();
            _certificateTypeService = EngineContext.Current.Resolve<ICertificateTypeService>();
            _storeCatologFileService = EngineContext.Current.Resolve<IStoreCatologFileService>();
            _memberStoreService = EngineContext.Current.Resolve<IMemberStoreService>();
            _storeChangeHistoryService = EngineContext.Current.Resolve<IStoreChangeHistoryService>();
            _storeActivityCategoryService = EngineContext.Current.Resolve<IStoreActivityCategoryService>();
            _activityTypeService = EngineContext.Current.Resolve<IActivityTypeService>();
            _categoryPlaceChoiceService = EngineContext.Current.Resolve<ICategoryPlaceChoiceService>();
            _storeSectorService = EngineContext.Current.Resolve<IStoreSectorService>();
            _videoService = EngineContext.Current.Resolve<IVideoService>();
        }

        public HttpResponseMessage GetInformation(int MainPartyId)
        {
            ProcessResult processStatus = new ProcessResult();
            try
            {
                var LoginUserEmail = Request.CheckLoginUserClaims().LoginMemberEmail;
                var loginmember = !string.IsNullOrEmpty(LoginUserEmail) ? _memberService.GetMemberByMemberEmail(LoginUserEmail) : null;
                if (loginmember != null)
                {
                    StoreInformation StoreInformation = new StoreInformation();

                    var store = _storeService.GetStoreByMainPartyId(MainPartyId);
                    var phones = _phoneService.GetPhonesByMainPartyId(MainPartyId).FirstOrDefault();
                    var address = _addressService.GetAddressesByMainPartyId(MainPartyId).FirstOrDefault();
                    var StoreActivityItems = _storeActivityTypeService.GetStoreActivityTypesByStoreId(MainPartyId);

                    if (store != null)
                    {
                        StoreInformation = new StoreInformation()
                        {
                            MainPartyId = store.MainPartyId,
                            cadde = address.Avenue,
                            sokak = address.Street,
                            selectedCityID = (int)address.CityId,
                            selectedCountryID = (int)address.CountryId,
                            selectedLocalityID = (int)address.LocalityId,
                            selectedTownID = (int)address.TownId,
                            memberTitleID = (int)loginmember.MemberTitleType.Value,
                            posta = store.StoreEMail,
                            storeActivitySelected = StoreActivityItems.Select(x => x.Id).ToList(),
                            storeCapID = (int)store.StoreCapital,
                            storeEmpCountID = (int)store.StoreEmployeesCount,
                            storeEndorseID = (int)store.StoreEndorsement,
                            storeEstDate = (int)store.StoreEstablishmentDate,
                            storeName = store.StoreName,
                            storeTypeID = (int)store.StoreType,
                            storeUrl = store.StoreUrlName,
                            storeWeb = store.StoreWeb,
                            addressId = address.AddressId,
                        };
                        processStatus.Result = StoreInformation;
                        processStatus.ActiveResultRowCount = 1;
                        processStatus.TotolRowCount = processStatus.ActiveResultRowCount;
                        processStatus.Message.Header = "Store İşlemleri";
                        processStatus.Message.Text = "Başarılı";
                        processStatus.Status = true;
                    }
                    else
                    {
                        processStatus.Message.Header = "Store İşlemleri";
                        processStatus.Message.Text = "Store Bulunamadı";
                        processStatus.Status = false;
                        processStatus.Result = null;
                        processStatus.Error = null;
                    }
                }
                else
                {
                    processStatus.Message.Header = "Store İşlemleri";
                    processStatus.Message.Text = "Başarısız";
                    processStatus.Status = false;
                    processStatus.Result = null;
                    processStatus.Error = null;
                }
            }
            catch (Exception Error)
            {
                processStatus.Message.Header = "Store İşlemleri";
                processStatus.Message.Text = "Başarısız";
                processStatus.Status = false;
                processStatus.Result = null;
                processStatus.Error = Error;
            }

            return Request.CreateResponse(HttpStatusCode.OK, processStatus);
        }

        public HttpResponseMessage SetInformation(StoreInformation Model)
        {
            ProcessResult processStatus = new ProcessResult();
            using (System.Transactions.TransactionScope Transaction = new System.Transactions.TransactionScope(System.Transactions.TransactionScopeOption.Required, TimeSpan.FromMinutes(30)))
            {
                try
                {
                    var LoginUserEmail = Request.CheckLoginUserClaims().LoginMemberEmail;
                    var loginmember = !string.IsNullOrEmpty(LoginUserEmail) ? _memberService.GetMemberByMemberEmail(LoginUserEmail) : null;
                    if (loginmember != null)
                    {
                        int MainPartyId = Model.MainPartyId;
                        var store = _storeService.GetStoreByMainPartyId(MainPartyId);
                        var address = _addressService.GetAddressByAddressId(Model.addressId);
                        var StoreActivityItems = _storeActivityTypeService.GetStoreActivityTypesByStoreId(MainPartyId).ToList();
                        if (store != null)
                        {
                            store.StoreEMail = Model.posta;
                            store.StoreCapital = (byte)Model.storeCapID;
                            store.StoreEmployeesCount = (byte)Model.storeEmpCountID;
                            store.StoreEndorsement = (byte)Model.storeEndorseID;
                            store.StoreEstablishmentDate = (int)Model.storeEstDate;
                            store.StoreName = Model.storeName;
                            store.StoreType = (byte)Model.storeTypeID;
                            store.StoreUrlName = Model.storeUrl;
                            store.StoreWeb = Model.storeWeb;

                            _storeService.UpdateStore(store);

                            address.Street = Model.sokak;
                            address.Avenue = Model.cadde;
                            address.CityId = (byte)Model.selectedCityID;
                            address.CountryId = (byte)Model.selectedCountryID;
                            address.LocalityId = (byte)Model.selectedLocalityID;
                            address.TownId = (byte)Model.selectedTownID;
                            _addressService.UpdateAddress(address);

                            loginmember.MemberTitleType = (byte)Model.memberTitleID;
                            _memberService.UpdateMember(loginmember);
                        }

                        // Tümü Siliniyor...
                        var storeAcTypes = _storeActivityTypeService.GetStoreActivityTypesByStoreId(Model.MainPartyId);
                        foreach (var item in storeAcTypes)
                        {
                            _storeActivityTypeService.DeleteStoreActivityType(item);
                        }

                        // Yoksa Ekleniyor.

                        foreach (var ActivityId in Model.storeActivitySelected)
                        {
                            var storeActivityType = new StoreActivityType
                            {
                                StoreId = Model.MainPartyId,
                                ActivityTypeId = (byte)ActivityId
                            };
                            _storeActivityTypeService.InsertStoreActivityType(storeActivityType);
                        }

                        processStatus.Result = Model;
                        processStatus.ActiveResultRowCount = 1;
                        processStatus.TotolRowCount = processStatus.ActiveResultRowCount;
                        processStatus.Message.Header = "Store İşlemleri";
                        processStatus.Message.Text = "Başarılı";
                        processStatus.Status = true;
                        Transaction.Complete();
                    }
                    else
                    {
                        processStatus.Message.Header = "Store İşlemleri";
                        processStatus.Message.Text = "Başarısız";
                        processStatus.Status = false;
                        processStatus.Result = null;
                        processStatus.Error = null;
                    }
                }
                catch (Exception Error)
                {
                    processStatus.Message.Header = "Store İşlemleri";
                    processStatus.Message.Text = "Başarısız";
                    processStatus.Status = false;
                    processStatus.Result = null;
                    processStatus.Error = Error;
                }
            }
            return Request.CreateResponse(HttpStatusCode.OK, processStatus);
        }

        public HttpResponseMessage GetStoreConctactInformation(int MainPartyId)
        {
            ProcessResult processStatus = new ProcessResult();
            try
            {
                var LoginUserEmail = Request.CheckLoginUserClaims().LoginMemberEmail;
                var loginmember = !string.IsNullOrEmpty(LoginUserEmail) ? _memberService.GetMemberByMemberEmail(LoginUserEmail) : null;
                if (loginmember != null)
                {
                    StoreConctactInformation StoreConctactInformation = new StoreConctactInformation();

                    var store = _storeService.GetStoreByMainPartyId(MainPartyId);
                    var phones = _phoneService.GetPhonesByMainPartyId(MainPartyId).FirstOrDefault();
                    var address = _addressService.GetAddressesByMainPartyId(MainPartyId).FirstOrDefault();
                    var StoreActivityItems = _storeActivityTypeService.GetStoreActivityTypesByStoreId(MainPartyId);

                    if (store != null)
                    {
                        StoreConctactInformation = new StoreConctactInformation()
                        {
                            MainPartyId = store.MainPartyId,
                            cadde = address.Avenue,
                            sokak = address.Street,
                            selectedCityID = (int)address.CityId,
                            selectedCountryID = (int)address.CountryId,
                            selectedLocalityID = (int)address.LocalityId,
                            selectedTownID = (int)address.TownId,
                            posta = store.StoreEMail,
                            addressId = address.AddressId,
                        };
                        processStatus.Result = StoreConctactInformation;
                        processStatus.ActiveResultRowCount = 1;
                        processStatus.TotolRowCount = processStatus.ActiveResultRowCount;
                        processStatus.Message.Header = "Store İşlemleri";
                        processStatus.Message.Text = "Başarılı";
                        processStatus.Status = true;
                    }
                    else
                    {
                        processStatus.Message.Header = "Store İşlemleri";
                        processStatus.Message.Text = "Store Bulunamadı";
                        processStatus.Status = false;
                        processStatus.Result = null;
                        processStatus.Error = null;
                    }
                }
                else
                {
                    processStatus.Message.Header = "Store İşlemleri";
                    processStatus.Message.Text = "Başarısız";
                    processStatus.Status = false;
                    processStatus.Result = null;
                    processStatus.Error = null;
                }
            }
            catch (Exception Error)
            {
                processStatus.Message.Header = "Store İşlemleri";
                processStatus.Message.Text = "Başarısız";
                processStatus.Status = false;
                processStatus.Result = null;
                processStatus.Error = Error;
            }

            return Request.CreateResponse(HttpStatusCode.OK, processStatus);
        }

        public HttpResponseMessage SetStoreConctactInformation(StoreConctactInformation Model)
        {
            ProcessResult processStatus = new ProcessResult();
            using (System.Transactions.TransactionScope Transaction = new System.Transactions.TransactionScope(System.Transactions.TransactionScopeOption.Required, TimeSpan.FromMinutes(30)))
            {
                try
                {
                    var LoginUserEmail = Request.CheckLoginUserClaims().LoginMemberEmail;
                    var loginmember = !string.IsNullOrEmpty(LoginUserEmail) ? _memberService.GetMemberByMemberEmail(LoginUserEmail) : null;
                    if (loginmember != null)
                    {
                        int MainPartyId = Model.MainPartyId;
                        var store = _storeService.GetStoreByMainPartyId(MainPartyId);
                        var address = _addressService.GetAddressByAddressId(Model.addressId);
                        var StoreActivityItems = _storeActivityTypeService.GetStoreActivityTypesByStoreId(MainPartyId).ToList();
                        if (store != null)
                        {
                            store.StoreEMail = Model.posta;
                            _storeService.UpdateStore(store);

                            address.Street = Model.sokak;
                            address.Avenue = Model.cadde;
                            address.CityId = (byte)Model.selectedCityID;
                            address.CountryId = (byte)Model.selectedCountryID;
                            address.LocalityId = (byte)Model.selectedLocalityID;
                            address.TownId = (byte)Model.selectedTownID;
                            _addressService.UpdateAddress(address);
                        }
                        processStatus.Result = Model;
                        processStatus.ActiveResultRowCount = 1;
                        processStatus.TotolRowCount = processStatus.ActiveResultRowCount;
                        processStatus.Message.Header = "Store İşlemleri";
                        processStatus.Message.Text = "Başarılı";
                        processStatus.Status = true;
                        Transaction.Complete();
                    }
                    else
                    {
                        processStatus.Message.Header = "Store İşlemleri";
                        processStatus.Message.Text = "Başarısız";
                        processStatus.Status = false;
                        processStatus.Result = null;
                        processStatus.Error = null;
                    }
                }
                catch (Exception Error)
                {
                    processStatus.Message.Header = "Store İşlemleri";
                    processStatus.Message.Text = "Başarısız";
                    processStatus.Status = false;
                    processStatus.Result = null;
                    processStatus.Error = Error;
                }
            }
            return Request.CreateResponse(HttpStatusCode.OK, processStatus);
        }

        public HttpResponseMessage GetStoreTaxAdministration(int MainPartyId)
        {
            ProcessResult processStatus = new ProcessResult();
            try
            {
                var LoginUserEmail = Request.CheckLoginUserClaims().LoginMemberEmail;
                var loginmember = !string.IsNullOrEmpty(LoginUserEmail) ? _memberService.GetMemberByMemberEmail(LoginUserEmail) : null;
                if (loginmember != null)
                {
                    var store = _storeService.GetStoreByMainPartyId(MainPartyId);
                    var storeInfoNumber = _storeNumberShowService.GetStoreInfoNumberShowByStoreMainPartyId(Convert.ToInt32(store.MainPartyId));
                    if (store != null)
                    {
                        bool MersisNoState = storeInfoNumber.MersisNoShow;
                        bool TicaretSicilNoNoState = storeInfoNumber.TradeRegistryNoShow;
                        bool NameState = storeInfoNumber.TaxOfficeShow;
                        bool NoState = storeInfoNumber.TaxNumberShow;

                        StoreTaxAdministration StoreTaxAdministration = new StoreTaxAdministration()
                        {
                            MainPartyId = store.MainPartyId,
                            TicaretSicilNo = new StoreTaxAdministrationItem()
                            {
                                ProfileState = TicaretSicilNoNoState,
                                Value = store.TradeRegistrNo,
                            },
                            No = new StoreTaxAdministrationItem()
                            {
                                ProfileState = NoState,
                                Value = store.TaxNumber,
                            },
                            MersisNo = new StoreTaxAdministrationItem()
                            {
                                ProfileState = MersisNoState,
                                Value = store.MersisNo,
                            },
                            Name = new StoreTaxAdministrationItem()
                            {
                                ProfileState = NameState,
                                Value = store.TaxOffice,
                            },
                        };
                        processStatus.Result = StoreTaxAdministration;
                        processStatus.ActiveResultRowCount = 1;
                        processStatus.TotolRowCount = processStatus.ActiveResultRowCount;
                        processStatus.Message.Header = "Store İşlemleri";
                        processStatus.Message.Text = "Başarılı";
                        processStatus.Status = true;
                    }
                    else
                    {
                        processStatus.Message.Header = "Store İşlemleri";
                        processStatus.Message.Text = "Store Bulunamadı";
                        processStatus.Status = false;
                        processStatus.Result = null;
                        processStatus.Error = null;
                    }
                }
                else
                {
                    processStatus.Message.Header = "Store İşlemleri";
                    processStatus.Message.Text = "Başarısız";
                    processStatus.Status = false;
                    processStatus.Result = null;
                    processStatus.Error = null;
                }
            }
            catch (Exception Error)
            {
                processStatus.Message.Header = "Store İşlemleri";
                processStatus.Message.Text = "Başarısız";
                processStatus.Status = false;
                processStatus.Result = null;
                processStatus.Error = Error;
            }

            return Request.CreateResponse(HttpStatusCode.OK, processStatus);
        }

        public HttpResponseMessage SetGetStoreTaxAdministration(StoreTaxAdministration Model)
        {
            ProcessResult processStatus = new ProcessResult();
            using (System.Transactions.TransactionScope Transaction = new System.Transactions.TransactionScope(System.Transactions.TransactionScopeOption.Required, TimeSpan.FromMinutes(30)))
            {
                try
                {
                    var LoginUserEmail = Request.CheckLoginUserClaims().LoginMemberEmail;
                    var loginmember = !string.IsNullOrEmpty(LoginUserEmail) ? _memberService.GetMemberByMemberEmail(LoginUserEmail) : null;
                    if (loginmember != null)
                    {
                        var store = _storeService.GetStoreByMainPartyId(Model.MainPartyId);
                        var storeInfoNumber = _storeNumberShowService.GetStoreInfoNumberShowByStoreMainPartyId(Convert.ToInt32(store.MainPartyId));
                        if (store != null)
                        {
                            store.TradeRegistrNo = Model.TicaretSicilNo.Value;
                            store.TaxNumber = Model.No.Value;
                            store.MersisNo = Model.MersisNo.Value;
                            store.TaxOffice = Model.Name.Value;
                            _storeService.UpdateStore(store);

                            storeInfoNumber.MersisNoShow = Model.MersisNo.ProfileState;
                            storeInfoNumber.TradeRegistryNoShow = Model.TicaretSicilNo.ProfileState;
                            storeInfoNumber.TaxNumberShow = Model.No.ProfileState;
                            storeInfoNumber.TaxOfficeShow = Model.Name.ProfileState;
                            _storeNumberShowService.UpdateStoreInfoNumberShow(storeInfoNumber);

                            processStatus.Result = Model;
                            processStatus.ActiveResultRowCount = 1;
                            processStatus.TotolRowCount = processStatus.ActiveResultRowCount;
                            processStatus.Message.Header = "Store İşlemleri";
                            processStatus.Message.Text = "Başarılı";
                            processStatus.Status = true;
                            Transaction.Complete();
                        }
                        else
                        {
                            processStatus.Message.Header = "Store İşlemleri";
                            processStatus.Message.Text = "Store Bulunamadı";
                            processStatus.Status = false;
                            processStatus.Result = null;
                            processStatus.Error = null;
                        }
                    }
                    else
                    {
                        processStatus.Message.Header = "Store İşlemleri";
                        processStatus.Message.Text = "Başarısız";
                        processStatus.Status = false;
                        processStatus.Result = null;
                        processStatus.Error = null;
                    }
                }
                catch (Exception Error)
                {
                    processStatus.Message.Header = "Store İşlemleri";
                    processStatus.Message.Text = "Başarısız";
                    processStatus.Status = false;
                    processStatus.Result = null;
                    processStatus.Error = Error;
                }
            }
            return Request.CreateResponse(HttpStatusCode.OK, processStatus);
        }

        public HttpResponseMessage GetSlideImage(int MainPartyId)
        {
            ProcessResult processStatus = new ProcessResult();
            try
            {
                var LoginUserEmail = Request.CheckLoginUserClaims().LoginMemberEmail;
                var loginmember = !string.IsNullOrEmpty(LoginUserEmail) ? _memberService.GetMemberByMemberEmail(LoginUserEmail) : null;
                if (loginmember != null)
                {
                    var store = _storeService.GetStoreByMainPartyId(MainPartyId);
                    var storeImages = _pictureService.GetPictureByMainPartyIdWithStoreImageType(MainPartyId, StoreImageTypeEnum.StoreProfileSliderImage).ToList();
                    if (store != null)
                    {
                        string StoreSliderImageFolder = AppSettings.StoreSliderImageFolder;
                        StorePicture StorePicture = new StorePicture()
                        {
                            MainPartyId = store.MainPartyId,
                            List = storeImages.Select(x => new StorePictureItem()
                            {
                                PictureId = x.PictureId,
                                Value = StoreSliderImageFolder + x.PicturePath
                            }).ToList(),
                        };
                        processStatus.Result = StorePicture;
                        processStatus.ActiveResultRowCount = 1;
                        processStatus.TotolRowCount = processStatus.ActiveResultRowCount;
                        processStatus.Message.Header = "Store İşlemleri";
                        processStatus.Message.Text = "Başarılı";
                        processStatus.Status = true;
                    }
                    else
                    {
                        processStatus.Message.Header = "Store İşlemleri";
                        processStatus.Message.Text = "Store Bulunamadı";
                        processStatus.Status = false;
                        processStatus.Result = null;
                        processStatus.Error = null;
                    }
                }
                else
                {
                    processStatus.Message.Header = "Store İşlemleri";
                    processStatus.Message.Text = "Başarısız";
                    processStatus.Status = false;
                    processStatus.Result = null;
                    processStatus.Error = null;
                }
            }
            catch (Exception Error)
            {
                processStatus.Message.Header = "Store İşlemleri";
                processStatus.Message.Text = "Başarısız";
                processStatus.Status = false;
                processStatus.Result = null;
                processStatus.Error = Error;
            }

            return Request.CreateResponse(HttpStatusCode.OK, processStatus);
        }

        public HttpResponseMessage DeleteSlideImage(StorePicture Model)
        {
            ProcessResult processStatus = new ProcessResult();
            using (System.Transactions.TransactionScope Transaction = new System.Transactions.TransactionScope(System.Transactions.TransactionScopeOption.Required, TimeSpan.FromMinutes(30)))
            {
                try
                {
                    var LoginUserEmail = Request.CheckLoginUserClaims().LoginMemberEmail;
                    var loginmember = !string.IsNullOrEmpty(LoginUserEmail) ? _memberService.GetMemberByMemberEmail(LoginUserEmail) : null;
                    if (loginmember != null)
                    {
                        var store = _storeService.GetStoreByMainPartyId(Model.MainPartyId);
                        if (store != null)
                        {
                            string StoreSliderImageFolder = AppSettings.StoreSliderImageFolder;
                            List<string> listDeleteFile = new List<string>();
                            var storeImages = _pictureService.GetPictureByMainPartyIdWithStoreImageType(store.MainPartyId, StoreImageTypeEnum.StoreProfileSliderImage).ToList();
                            foreach (var item in Model.List)
                            {
                                var storeImage = storeImages.FirstOrDefault(x => x.PictureId == item.PictureId);
                                if (storeImage != null)
                                {
                                    _pictureService.DeletePicture(storeImage);
                                    if (System.IO.File.Exists(StoreSliderImageFolder + storeImage.PicturePath))
                                    {
                                        FileHelpers.Delete(AppSettings.StoreBannerFolder + storeImage.PicturePath);
                                    }
                                    if (System.IO.File.Exists(StoreSliderImageFolder + storeImage.PicturePath.Replace("_slider", "-800x300")))
                                    {
                                        FileHelpers.Delete(AppSettings.StoreBannerFolder + storeImage.PicturePath.Replace("_slider", "-800x300"));
                                    }
                                }
                            }

                            processStatus.Result = null;
                            processStatus.ActiveResultRowCount = 1;
                            processStatus.TotolRowCount = processStatus.ActiveResultRowCount;
                            processStatus.Message.Header = "Store İşlemleri";
                            processStatus.Message.Text = "Başarılı";
                            processStatus.Status = true;
                            Transaction.Complete();

                            //Dosyalar Diskten Siliniyor...
                            foreach (var item in listDeleteFile)
                            {
                                FileHelpers.Delete(item);
                            }
                        }
                        else
                        {
                            processStatus.Message.Header = "Store İşlemleri";
                            processStatus.Message.Text = "Store Bulunamadı";
                            processStatus.Status = false;
                            processStatus.Result = null;
                            processStatus.Error = null;
                        }
                    }
                    else
                    {
                        processStatus.Message.Header = "Store İşlemleri";
                        processStatus.Message.Text = "Başarısız";
                        processStatus.Status = false;
                        processStatus.Result = null;
                        processStatus.Error = null;
                    }
                }
                catch (Exception Error)
                {
                    processStatus.Message.Header = "Store İşlemleri";
                    processStatus.Message.Text = "Başarısız";
                    processStatus.Status = false;
                    processStatus.Result = null;
                    processStatus.Error = Error;
                }
            }
            return Request.CreateResponse(HttpStatusCode.OK, processStatus);
        }

        public HttpResponseMessage AddSlideImage(StorePicture Model)
        {
            ProcessResult processStatus = new ProcessResult();
            using (System.Transactions.TransactionScope Transaction = new System.Transactions.TransactionScope(System.Transactions.TransactionScopeOption.Required, TimeSpan.FromMinutes(30)))
            {
                try
                {
                    var LoginUserEmail = Request.CheckLoginUserClaims().LoginMemberEmail;
                    var loginmember = !string.IsNullOrEmpty(LoginUserEmail) ? _memberService.GetMemberByMemberEmail(LoginUserEmail) : null;
                    if (loginmember != null)
                    {
                        var store = _storeService.GetStoreByMainPartyId(Model.MainPartyId);
                        if (store != null)
                        {
                            string StoreSliderImageFolder = AppSettings.StoreSliderImageFolder;
                            var storeImages = _pictureService.GetPictureByMainPartyIdWithStoreImageType(store.MainPartyId, StoreImageTypeEnum.StoreProfileSliderImage).ToList();
                            int imageCount = storeImages.Count();
                            foreach (var item in Model.List)
                            {
                                string Logo = item.Value;
                                string Uzanti = "";
                                bool IslemDurum = false;
                                if (Uzanti == "jpg")
                                {
                                    IslemDurum = true;
                                }
                                if (Uzanti == "png")
                                {
                                    IslemDurum = true;
                                }
                                if (IslemDurum)
                                {
                                    string filename = Guid.NewGuid().ToString("N") + "_slider" + Uzanti;
                                    string mapPath = System.Web.Hosting.HostingEnvironment.MapPath(AppSettings.StoreBannerFolder);
                                    var targetFile = new FileInfo(mapPath + filename);
                                    if (targetFile.Exists)
                                    {
                                        filename = Guid.NewGuid().ToString("N") + "_slider" + Uzanti;
                                    }
                                    string storeBannerImageFileSavePath = mapPath + filename;
                                    System.Drawing.Image Img = Logo.ToImage();
                                    if (Uzanti == "png")
                                    {
                                        Img.Save(storeBannerImageFileSavePath, System.Drawing.Imaging.ImageFormat.Png);
                                    }
                                    if (Uzanti == "jpg")
                                    {
                                        Img.Save(storeBannerImageFileSavePath, System.Drawing.Imaging.ImageFormat.Jpeg);
                                    }

                                    Img = ImageProcessHelper.resizeImageBanner(800, 300, storeBannerImageFileSavePath);
                                    ImageProcessHelper.SaveJpeg(storeBannerImageFileSavePath, Img, 80, "_slider", "-800x300");
                                    imageCount++;
                                    var curPicture = new Picture()
                                    {
                                        PicturePath = filename,
                                        ProductId = null,
                                        MainPartyId = store.MainPartyId,
                                        PictureName = String.Empty,
                                        PictureOrder = imageCount,
                                        StoreImageType = (byte)StoreImageType.StoreProfileSliderImage
                                    };
                                    _pictureService.InsertPicture(curPicture);
                                }
                            }

                            storeImages = _pictureService.GetPictureByMainPartyIdWithStoreImageType(store.MainPartyId, StoreImageTypeEnum.StoreProfileSliderImage).ToList();
                            StorePicture StorePicture = new StorePicture()
                            {
                                MainPartyId = store.MainPartyId,
                                List = storeImages.Select(x => new StorePictureItem()
                                {
                                    PictureId = x.PictureId,
                                    Value = StoreSliderImageFolder + x.PicturePath
                                }).ToList(),
                            };

                            processStatus.Result = StorePicture;
                            processStatus.ActiveResultRowCount = 1;
                            processStatus.TotolRowCount = processStatus.ActiveResultRowCount;
                            processStatus.Message.Header = "Store İşlemleri";
                            processStatus.Message.Text = "Başarılı";
                            processStatus.Status = true;
                            Transaction.Complete();
                        }
                        else
                        {
                            processStatus.Message.Header = "Store İşlemleri";
                            processStatus.Message.Text = "Store Bulunamadı";
                            processStatus.Status = false;
                            processStatus.Result = null;
                            processStatus.Error = null;
                        }
                    }
                    else
                    {
                        processStatus.Message.Header = "Store İşlemleri";
                        processStatus.Message.Text = "Başarısız";
                        processStatus.Status = false;
                        processStatus.Result = null;
                        processStatus.Error = null;
                    }
                }
                catch (Exception Error)
                {
                    processStatus.Message.Header = "Store İşlemleri";
                    processStatus.Message.Text = "Başarısız";
                    processStatus.Status = false;
                    processStatus.Result = null;
                    processStatus.Error = Error;
                }
            }
            return Request.CreateResponse(HttpStatusCode.OK, processStatus);
        }

        public HttpResponseMessage GetCertificate(int MainPartyId)
        {
            ProcessResult processStatus = new ProcessResult();
            try
            {
                var LoginUserEmail = Request.CheckLoginUserClaims().LoginMemberEmail;
                var loginmember = !string.IsNullOrEmpty(LoginUserEmail) ? _memberService.GetMemberByMemberEmail(LoginUserEmail) : null;
                if (loginmember != null)
                {
                    var store = _storeService.GetStoreByMainPartyId(MainPartyId);
                    if (store != null)
                    {
                        var storeCertificates = _storeService.GetStoreCertificatesByMainPartyId(MainPartyId);
                        List<StoreCertificateItem> StoreCertificateItemList = new List<StoreCertificateItem>();
                        foreach (var item in storeCertificates)
                        {
                            List<string> pictureslist = new List<string>();
                            var pictures = _pictureService.GetPictureByStoreCertificateId(item.StoreCertificateId);
                            foreach (var picture in pictures)
                            {
                                pictureslist.Add(AppSettings.StoreCertificateImageFolder + picture.PicturePath.Replace("_certificate", "-500x800"));
                            }
                            StoreCertificateItem StoreCertificateItem = new StoreCertificateItem();
                            StoreCertificateItem.Name = item.CertificateName;
                            StoreCertificateItem.CertificateId = item.StoreCertificateId;
                            if (pictureslist.Count > 0)
                            {
                                StoreCertificateItem.Image = pictureslist;
                            }
                            StoreCertificateItemList.Add(StoreCertificateItem);
                        }
                        MakinaTurkiye.Api.View.StoreCertificate StoreCertificate = new MakinaTurkiye.Api.View.StoreCertificate()
                        {
                            MainPartyId = store.MainPartyId,
                            List = StoreCertificateItemList
                        };
                        processStatus.Result = StoreCertificate;
                        processStatus.ActiveResultRowCount = 1;
                        processStatus.TotolRowCount = processStatus.ActiveResultRowCount;
                        processStatus.Message.Header = "Store İşlemleri";
                        processStatus.Message.Text = "Başarılı";
                        processStatus.Status = true;
                    }
                    else
                    {
                        processStatus.Message.Header = "Store İşlemleri";
                        processStatus.Message.Text = "Store Bulunamadı";
                        processStatus.Status = false;
                        processStatus.Result = null;
                        processStatus.Error = null;
                    }
                }
                else
                {
                    processStatus.Message.Header = "Store İşlemleri";
                    processStatus.Message.Text = "Başarısız";
                    processStatus.Status = false;
                    processStatus.Result = null;
                    processStatus.Error = null;
                }
            }
            catch (Exception Error)
            {
                processStatus.Message.Header = "Store İşlemleri";
                processStatus.Message.Text = "Başarısız";
                processStatus.Status = false;
                processStatus.Result = null;
                processStatus.Error = Error;
            }

            return Request.CreateResponse(HttpStatusCode.OK, processStatus);
        }

        public HttpResponseMessage DeleteCertificate(MakinaTurkiye.Api.View.StoreCertificate Model)
        {
            ProcessResult processStatus = new ProcessResult();
            using (System.Transactions.TransactionScope Transaction = new System.Transactions.TransactionScope(System.Transactions.TransactionScopeOption.Required, TimeSpan.FromMinutes(30)))
            {
                try
                {
                    var LoginUserEmail = Request.CheckLoginUserClaims().LoginMemberEmail;
                    var loginmember = !string.IsNullOrEmpty(LoginUserEmail) ? _memberService.GetMemberByMemberEmail(LoginUserEmail) : null;
                    if (loginmember != null)
                    {
                        var store = _storeService.GetStoreByMainPartyId(Model.MainPartyId);
                        if (store != null)
                        {
                            string StoreSliderImageFolder = AppSettings.StoreSliderImageFolder;
                            List<string> listDeleteFile = new List<string>();
                            var storeCertificates = _storeService.GetStoreCertificatesByMainPartyId(store.MainPartyId);
                            List<string> pictureslist = new List<string>();
                            var storeImages = _pictureService.GetPictureByMainPartyIdWithStoreImageType(store.MainPartyId, StoreImageTypeEnum.StoreProfileSliderImage).ToList();
                            foreach (var item in Model.List)
                            {
                                var storeCertificate = storeCertificates.FirstOrDefault(x => x.StoreCertificateId == item.CertificateId);
                                if (storeCertificate != null)
                                {
                                    List<int> ids = new List<int>();
                                    ids.Add(item.CertificateId);
                                    var certificateTypes = _certificateTypeService.GetCertificatesByIds(ids);
                                    foreach (var certificateTypesitem in certificateTypes)
                                    {
                                        _certificateTypeService.DeleteCertificateType(certificateTypesitem);
                                    }

                                    var certificateTypeProducts = _certificateTypeService.GetCertificateTypeProductsByStoreCertificateId(item.CertificateId);
                                    foreach (var certificateTypeProduct in certificateTypeProducts)
                                    {
                                        _certificateTypeService.DeleteCertificateTypeProduct(certificateTypeProduct);
                                    }
                                    var pictures = _pictureService.GetPictureByStoreCertificateId(item.CertificateId);
                                    foreach (var pic in pictures)
                                    {
                                        string file = HostingEnvironment.MapPath(AppSettings.StoreCertificateImageFolder) + pic.PicturePath.Replace("_certificate", "-500x800");
                                        if (System.IO.File.Exists(file))
                                        {
                                            listDeleteFile.Add(file);
                                        }
                                        file = HostingEnvironment.MapPath(AppSettings.StoreCertificateImageFolder) + pic.PicturePath.Replace("_certificate", "-500x800");
                                        if (System.IO.File.Exists(file))
                                        {
                                            listDeleteFile.Add(file);
                                        }
                                    }
                                }
                            }

                            processStatus.Result = null;
                            processStatus.ActiveResultRowCount = 1;
                            processStatus.TotolRowCount = processStatus.ActiveResultRowCount;
                            processStatus.Message.Header = "Store İşlemleri";
                            processStatus.Message.Text = "Başarılı";
                            processStatus.Status = true;
                            Transaction.Complete();

                            //Dosyalar Diskten Siliniyor...
                            foreach (var item in listDeleteFile)
                            {
                                FileHelpers.Delete(item);
                            }
                        }
                        else
                        {
                            processStatus.Message.Header = "Store İşlemleri";
                            processStatus.Message.Text = "Store Bulunamadı";
                            processStatus.Status = false;
                            processStatus.Result = null;
                            processStatus.Error = null;
                        }
                    }
                    else
                    {
                        processStatus.Message.Header = "Store İşlemleri";
                        processStatus.Message.Text = "Başarısız";
                        processStatus.Status = false;
                        processStatus.Result = null;
                        processStatus.Error = null;
                    }
                }
                catch (Exception Error)
                {
                    processStatus.Message.Header = "Store İşlemleri";
                    processStatus.Message.Text = "Başarısız";
                    processStatus.Status = false;
                    processStatus.Result = null;
                    processStatus.Error = Error;
                }
            }
            return Request.CreateResponse(HttpStatusCode.OK, processStatus);
        }

        public HttpResponseMessage AddCertificate(MakinaTurkiye.Api.View.StoreCertificate Model)
        {
            ProcessResult processStatus = new ProcessResult();
            using (System.Transactions.TransactionScope Transaction = new System.Transactions.TransactionScope(System.Transactions.TransactionScopeOption.Required, TimeSpan.FromMinutes(30)))
            {
                List<string> listDeleteFile = new List<string>();
                try
                {
                    var LoginUserEmail = Request.CheckLoginUserClaims().LoginMemberEmail;
                    var loginmember = !string.IsNullOrEmpty(LoginUserEmail) ? _memberService.GetMemberByMemberEmail(LoginUserEmail) : null;
                    if (loginmember != null)
                    {
                        var store = _storeService.GetStoreByMainPartyId(Model.MainPartyId);
                        if (store != null)
                        {
                            string StoreSliderImageFolder = AppSettings.StoreSliderImageFolder;
                            var storeCertificates = _storeService.GetStoreCertificatesByMainPartyId(store.MainPartyId);
                            List<string> pictureslist = new List<string>();
                            var storeImages = _pictureService.GetPictureByMainPartyIdWithStoreImageType(store.MainPartyId, StoreImageTypeEnum.StoreProfileSliderImage).ToList();
                            foreach (var model in Model.List)
                            {
                                var storeCertificate = new MakinaTurkiye.Entities.Tables.Stores.StoreCertificate();
                                storeCertificate.Active = true;
                                storeCertificate.RecordDate = DateTime.Now;
                                storeCertificate.UpdateDate = DateTime.Now;
                                storeCertificate.CertificateName = model.Name;
                                storeCertificate.Order = model.Sira;
                                storeCertificate.MainPartyId = Model.MainPartyId;
                                _storeService.InsertStoreCertificate(storeCertificate);

                                if (model.Type != 0 && model.Type != 99999)
                                {
                                    var certificateTypeProduct = new CertificateTypeProduct
                                    {
                                        StoreCertificateId = storeCertificate.StoreCertificateId,
                                        CertificateTypeId = model.Type
                                    };
                                    _certificateTypeService.InsertCertificateTypeProduct(certificateTypeProduct);
                                }
                                else
                                {
                                    var certificateType = new CertificateType
                                    {
                                        Name = storeCertificate.CertificateName,
                                        CreatedDate = DateTime.Now,
                                        UpdatedDate = storeCertificate.UpdateDate,
                                        Order = 0,
                                        Active = false,
                                        InsertedStoreMainPartyId = Model.MainPartyId
                                    };
                                    _certificateTypeService.InsertCertificateType(certificateType);
                                    var certificateTypeProduct = new CertificateTypeProduct
                                    {
                                        StoreCertificateId = storeCertificate.StoreCertificateId,
                                        CertificateTypeId = certificateType.CertificateTypeId
                                    };
                                    _certificateTypeService.InsertCertificateTypeProduct(certificateTypeProduct);
                                }

                                foreach (var ImgPic in model.Image)
                                {
                                    string Logo = ImgPic; string Uzanti = "";
                                    bool IslemDurum = false;
                                    if (Uzanti == "jpg")
                                    {
                                        IslemDurum = true;
                                    }
                                    if (Uzanti == "png")
                                    {
                                        IslemDurum = true;
                                    }
                                    if (IslemDurum)
                                    {
                                        System.Drawing.Image Img = Logo.ToImage();

                                        string mapPath = HostingEnvironment.MapPath(AppSettings.StoreCertificateImageFolder);
                                        var fileName = Guid.NewGuid().ToString("N") + "_certificate";
                                        string filename = fileName + Uzanti;
                                        var targetFile = new FileInfo(mapPath + filename);
                                        if (targetFile.Exists)
                                        {
                                            fileName = Guid.NewGuid().ToString("N") + "_certificate";
                                            filename = fileName + Uzanti;
                                        }
                                        string storeBannerImageFileSavePath = mapPath + filename;

                                        if (Uzanti == "png")
                                        {
                                            Img.Save(storeBannerImageFileSavePath, System.Drawing.Imaging.ImageFormat.Png);
                                        }
                                        if (Uzanti == "jpg")
                                        {
                                            Img.Save(storeBannerImageFileSavePath, System.Drawing.Imaging.ImageFormat.Jpeg);
                                        }

                                        List<string> thubmsizes = new List<string>();
                                        thubmsizes.Add("500x800");
                                        ImageProcessHelper.ImageResize(storeBannerImageFileSavePath, mapPath + fileName.Replace("_certificate", ""), thubmsizes);

                                        var curPicture = new Picture()
                                        {
                                            PicturePath = filename,
                                            ProductId = null,
                                            MainPartyId = store.MainPartyId,
                                            PictureName = String.Empty,
                                            StoreImageType = (byte)StoreImageTypeEnum.StoreCertificate,
                                            StoreCertificateId = storeCertificate.StoreCertificateId
                                        };
                                        _pictureService.InsertPicture(curPicture);
                                    }
                                }
                            }
                            processStatus.Result = null;
                            processStatus.ActiveResultRowCount = 1;
                            processStatus.TotolRowCount = processStatus.ActiveResultRowCount;
                            processStatus.Message.Header = "Store İşlemleri";
                            processStatus.Message.Text = "Başarılı";
                            processStatus.Status = true;
                            Transaction.Complete();
                        }
                        else
                        {
                            processStatus.Message.Header = "Store İşlemleri";
                            processStatus.Message.Text = "Store Bulunamadı";
                            processStatus.Status = false;
                            processStatus.Result = null;
                            processStatus.Error = null;
                        }
                    }
                    else
                    {
                        processStatus.Message.Header = "Store İşlemleri";
                        processStatus.Message.Text = "Başarısız";
                        processStatus.Status = false;
                        processStatus.Result = null;
                        processStatus.Error = null;
                    }
                }
                catch (Exception Error)
                {
                    foreach (var item in listDeleteFile)
                    {
                        FileHelpers.Delete(item);
                    }
                    processStatus.Message.Header = "Store İşlemleri";
                    processStatus.Message.Text = "Başarısız";
                    processStatus.Status = false;
                    processStatus.Result = null;
                    processStatus.Error = Error;
                }
            }
            return Request.CreateResponse(HttpStatusCode.OK, processStatus);
        }

        public HttpResponseMessage GetCatalog(int MainPartyId)
        {
            ProcessResult processStatus = new ProcessResult();
            try
            {
                var LoginUserEmail = Request.CheckLoginUserClaims().LoginMemberEmail;
                var loginmember = !string.IsNullOrEmpty(LoginUserEmail) ? _memberService.GetMemberByMemberEmail(LoginUserEmail) : null;
                if (loginmember != null)
                {
                    var store = _storeService.GetStoreByMainPartyId(MainPartyId);
                    if (store != null)
                    {
                        List<StoreCatalogItem> StoreCatalogItemList = new List<StoreCatalogItem>();

                        var catologList = _storeCatologFileService.StoreCatologFilesByStoreMainPartyId(store.MainPartyId).OrderBy(x => x.FileOrder).ThenByDescending(x => x.StoreCatologFileId);

                        foreach (var item in catologList)
                        {
                            var filePath = FileUrlHelper.GetStoreCatologUrl(item.FileName, store.MainPartyId);
                            StoreCatalogItemList.Add(
                                    new StoreCatalogItem
                                    {
                                        CatalogId = item.StoreCatologFileId,
                                        Order = item.FileOrder,
                                        File = filePath,
                                        Name = item.Name
                                    });
                        }

                        MakinaTurkiye.Api.View.StoreCatalog StoreCatalog = new MakinaTurkiye.Api.View.StoreCatalog()
                        {
                            MainPartyId = store.MainPartyId,
                            List = StoreCatalogItemList
                        };
                        processStatus.Result = StoreCatalog;
                        processStatus.ActiveResultRowCount = 1;
                        processStatus.TotolRowCount = processStatus.ActiveResultRowCount;
                        processStatus.Message.Header = "Store İşlemleri";
                        processStatus.Message.Text = "Başarılı";
                        processStatus.Status = true;
                    }
                    else
                    {
                        processStatus.Message.Header = "Store İşlemleri";
                        processStatus.Message.Text = "Store Bulunamadı";
                        processStatus.Status = false;
                        processStatus.Result = null;
                        processStatus.Error = null;
                    }
                }
                else
                {
                    processStatus.Message.Header = "Store İşlemleri";
                    processStatus.Message.Text = "Başarısız";
                    processStatus.Status = false;
                    processStatus.Result = null;
                    processStatus.Error = null;
                }
            }
            catch (Exception Error)
            {
                processStatus.Message.Header = "Store İşlemleri";
                processStatus.Message.Text = "Başarısız";
                processStatus.Status = false;
                processStatus.Result = null;
                processStatus.Error = Error;
            }

            return Request.CreateResponse(HttpStatusCode.OK, processStatus);
        }

        public HttpResponseMessage DeleteCatalog(MakinaTurkiye.Api.View.StoreCatalog Model)
        {
            ProcessResult processStatus = new ProcessResult();
            using (System.Transactions.TransactionScope Transaction = new System.Transactions.TransactionScope(System.Transactions.TransactionScopeOption.Required, TimeSpan.FromMinutes(30)))
            {
                try
                {
                    var LoginUserEmail = Request.CheckLoginUserClaims().LoginMemberEmail;
                    var loginmember = !string.IsNullOrEmpty(LoginUserEmail) ? _memberService.GetMemberByMemberEmail(LoginUserEmail) : null;
                    if (loginmember != null)
                    {
                        var store = _storeService.GetStoreByMainPartyId(Model.MainPartyId);
                        if (store != null)
                        {
                            List<string> listDeleteFile = new List<string>();

                            foreach (var item in Model.List)
                            {
                                var catolog = _storeCatologFileService.GetStoreCatologFileByCatologId(item.CatalogId);
                                var filePath = FileUrlHelper.GetStoreCatologUrl(catolog.FileName, catolog.StoreMainPartyId);
                                listDeleteFile.Add(AppSettings.StoreCatologFolder + "/" + catolog.StoreMainPartyId + "/" + catolog.FileName);
                                _storeCatologFileService.DeleteStoreCatologFile(catolog);
                            }
                            processStatus.Result = null;
                            processStatus.ActiveResultRowCount = 1;
                            processStatus.TotolRowCount = processStatus.ActiveResultRowCount;
                            processStatus.Message.Header = "Store İşlemleri";
                            processStatus.Message.Text = "Başarılı";
                            processStatus.Status = true;
                            Transaction.Complete();
                            //Dosyalar Diskten Siliniyor...
                            foreach (var item in listDeleteFile)
                            {
                                FileHelpers.Delete(item);
                            }
                        }
                        else
                        {
                            processStatus.Message.Header = "Store İşlemleri";
                            processStatus.Message.Text = "Store Bulunamadı";
                            processStatus.Status = false;
                            processStatus.Result = null;
                            processStatus.Error = null;
                        }
                    }
                    else
                    {
                        processStatus.Message.Header = "Store İşlemleri";
                        processStatus.Message.Text = "Başarısız";
                        processStatus.Status = false;
                        processStatus.Result = null;
                        processStatus.Error = null;
                    }
                }
                catch (Exception Error)
                {
                    processStatus.Message.Header = "Store İşlemleri";
                    processStatus.Message.Text = "Başarısız";
                    processStatus.Status = false;
                    processStatus.Result = null;
                    processStatus.Error = Error;
                }
            }
            return Request.CreateResponse(HttpStatusCode.OK, processStatus);
        }

        public HttpResponseMessage AddCatalog(MakinaTurkiye.Api.View.StoreCatalog Model)
        {
            ProcessResult processStatus = new ProcessResult();
            using (System.Transactions.TransactionScope Transaction = new System.Transactions.TransactionScope(System.Transactions.TransactionScopeOption.Required, TimeSpan.FromMinutes(30)))
            {
                List<string> listDeleteFile = new List<string>();
                try
                {
                    var LoginUserEmail = Request.CheckLoginUserClaims().LoginMemberEmail;
                    var loginmember = !string.IsNullOrEmpty(LoginUserEmail) ? _memberService.GetMemberByMemberEmail(LoginUserEmail) : null;
                    if (loginmember != null)
                    {
                        var store = _storeService.GetStoreByMainPartyId(Model.MainPartyId);
                        if (store != null)
                        {
                            foreach (var model in Model.List)
                            {
                                var storeCatologFile = new MakinaTurkiye.Entities.Tables.Stores.StoreCatologFile();
                                storeCatologFile.FileName = "";
                                storeCatologFile.Name = model.Name;
                                storeCatologFile.StoreMainPartyId = store.MainPartyId;
                                storeCatologFile.FileOrder = 0;
                                _storeCatologFileService.InsertStoreCatologFile(storeCatologFile);
                                string Logo = model.File;
                                string Uzanti = "";
                                bool IslemDurum = false;
                                if (Uzanti == "jpg")
                                {
                                    IslemDurum = true;
                                }
                                if (Uzanti == "png")
                                {
                                    IslemDurum = true;
                                }
                                if (Uzanti == "pdf")
                                {
                                    IslemDurum = true;
                                }
                                if (IslemDurum)
                                {
                                    string newFileName = !string.IsNullOrEmpty(model.Name) ? model.Name : $"{store.StoreName}-{Guid.NewGuid()}";
                                    int counter = 0;
                                    var file = Logo.ToFile(newFileName);
                                    string filePath = FileUploadHelper.UploadFile(file, AppSettings.StoreCatologFolder + "/" + store.MainPartyId.ToString(), newFileName, counter);
                                    storeCatologFile.FileName = filePath;
                                    _storeCatologFileService.UpdateStoreCatologFile(storeCatologFile);
                                }
                            }

                            processStatus.Result = null;
                            processStatus.ActiveResultRowCount = 1;
                            processStatus.TotolRowCount = processStatus.ActiveResultRowCount;
                            processStatus.Message.Header = "Store İşlemleri";
                            processStatus.Message.Text = "Başarılı";
                            processStatus.Status = true;
                            Transaction.Complete();
                        }
                        else
                        {
                            processStatus.Message.Header = "Store İşlemleri";
                            processStatus.Message.Text = "Store Bulunamadı";
                            processStatus.Status = false;
                            processStatus.Result = null;
                            processStatus.Error = null;
                        }
                    }
                    else
                    {
                        processStatus.Message.Header = "Store İşlemleri";
                        processStatus.Message.Text = "Başarısız";
                        processStatus.Status = false;
                        processStatus.Result = null;
                        processStatus.Error = null;
                    }
                }
                catch (Exception Error)
                {
                    foreach (var item in listDeleteFile)
                    {
                        FileHelpers.Delete(item);
                    }
                    processStatus.Message.Header = "Store İşlemleri";
                    processStatus.Message.Text = "Başarısız";
                    processStatus.Status = false;
                    processStatus.Result = null;
                    processStatus.Error = Error;
                }
            }
            return Request.CreateResponse(HttpStatusCode.OK, processStatus);
        }


        public HttpResponseMessage GetPermissionUser(int MainPartyId)
        {
            ProcessResult processStatus = new ProcessResult();
            try
            {
                var LoginUserEmail = Request.CheckLoginUserClaims().LoginMemberEmail;
                var loginmember = !string.IsNullOrEmpty(LoginUserEmail) ? _memberService.GetMemberByMemberEmail(LoginUserEmail) : null;
                if (loginmember != null)
                {
                    var store = _storeService.GetStoreByMainPartyId(MainPartyId);
                    if (store != null)
                    {
                        List<StorePermissionUserItem> StorePermissionUserItemList = new List<StorePermissionUserItem>();
                        var memberMainPartyIds = _memberStoreService.GetMemberStoresByStoreMainPartyId(store.MainPartyId).Where(x => x.MemberMainPartyId != loginmember.MainPartyId).Select(x => x.MemberMainPartyId);
                        var members = _memberService.GetMembersByMainPartyIds(memberMainPartyIds.ToList());
                        foreach (var item in members)
                        {
                            var mainParty = _memberService.GetMainPartyByMainPartyId(item.MainPartyId);
                            StorePermissionUserItemList.Add(
                                new StorePermissionUserItem{
                                        Active =item.Active,
                                        Mail = item.MemberEmail,
                                        PermissionMainPartyId = item.MainPartyId,
                                        Name = item.MemberName,
                                        Surname =item.MemberSurname, 
                                        Password = item.MemberPassword
                                    }
                                );
                        }
                        
                        MakinaTurkiye.Api.View.StorePermissionUser StorePermissionUser = new MakinaTurkiye.Api.View.StorePermissionUser()
                        {
                            MainPartyId = store.MainPartyId,
                            List = StorePermissionUserItemList
                        };

                        processStatus.Result = StorePermissionUser;
                        processStatus.ActiveResultRowCount = 1;
                        processStatus.TotolRowCount = processStatus.ActiveResultRowCount;
                        processStatus.Message.Header = "Store İşlemleri";
                        processStatus.Message.Text = "Başarılı";
                        processStatus.Status = true;
                    }
                    else
                    {
                        processStatus.Message.Header = "Store İşlemleri";
                        processStatus.Message.Text = "Store Bulunamadı";
                        processStatus.Status = false;
                        processStatus.Result = null;
                        processStatus.Error = null;
                    }
                }
                else
                {
                    processStatus.Message.Header = "Store İşlemleri";
                    processStatus.Message.Text = "Başarısız";
                    processStatus.Status = false;
                    processStatus.Result = null;
                    processStatus.Error = null;
                }
            }
            catch (Exception Error)
            {
                processStatus.Message.Header = "Store İşlemleri";
                processStatus.Message.Text = "Başarısız";
                processStatus.Status = false;
                processStatus.Result = null;
                processStatus.Error = Error;
            }

            return Request.CreateResponse(HttpStatusCode.OK, processStatus);
        }

        public HttpResponseMessage AddPermissionUser(MakinaTurkiye.Api.View.StorePermissionUser Model)
        {
            ProcessResult processStatus = new ProcessResult();
            using (System.Transactions.TransactionScope Transaction = new System.Transactions.TransactionScope(System.Transactions.TransactionScopeOption.Required, TimeSpan.FromMinutes(30)))
            {
                List<string> listDeleteFile = new List<string>();
                try
                {
                    var LoginUserEmail = Request.CheckLoginUserClaims().LoginMemberEmail;
                    var loginmember = !string.IsNullOrEmpty(LoginUserEmail) ? _memberService.GetMemberByMemberEmail(LoginUserEmail) : null;
                    if (loginmember != null)
                    {
                        var store = _storeService.GetStoreByMainPartyId(Model.MainPartyId);
                        if (store != null)
                        {
                            foreach (var model in Model.List)
                            {

                                var mainParty = new MakinaTurkiye.Entities.Tables.Members.MainParty
                                {
                                    Active = false,
                                    MainPartyType = (byte)MainPartyType.FastMember,
                                    MainPartyRecordDate = DateTime.Now,
                                    MainPartyFullName = CultureInfo.CurrentCulture.TextInfo.ToTitleCase($"{model.Name} {model.Surname}".ToLower())
                                };
                                _memberService.InsertMainParty(mainParty);

                                var member = new MakinaTurkiye.Entities.Tables.Members.Member();
                                int mainPartyId = mainParty.MainPartyId;
                                member.MainPartyId = mainPartyId;
                                member.MemberEmail = model.Mail;
                                member.MemberPassword = model.Password;
                                member.MemberName = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(model.Name.ToLower());
                                member.MemberSurname = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(model.Surname);
                                member.MemberType = (byte)MemberType.Enterprise;
                                member.Active = true;
                                member.FastMemberShipType = (byte)FastMembershipType.Normal;
                                member.Gender = Convert.ToBoolean(model.Gender);
                                _memberService.InsertMember(member);


                                string memberNo = "##";
                                for (int i = 0; i < 7 - mainParty.MainPartyId.ToString().Length; i++)
                                {
                                    memberNo = memberNo + "0";
                                }
                                memberNo = memberNo + mainParty.MainPartyId;

                                var memberForMemberNo = _memberService.GetMemberByMainPartyId(mainPartyId);
                                member.MemberNo = memberNo;
                                _memberService.UpdateMember(member);

                                var memberStore = new MakinaTurkiye.Entities.Tables.Members.MemberStore();
                                memberStore.MemberMainPartyId = mainParty.MainPartyId;
                                memberStore.StoreMainPartyId = store.MainPartyId;
                                memberStore.MemberStoreType = (byte)MemberStoreType.Helper;
                                _memberStoreService.InsertMemberStore(memberStore);


                                var mailMessageTemplate = _messageMTService.GetMessagesMTByMessageMTName("kullanicibilgileri");
                                string content = mailMessageTemplate.MailContent.Replace("#email#", model.Mail).Replace("#sifre#", model.Password).Replace("#firmaadi#", store.StoreName);
                                var toMails = new List<string>();
                                toMails.Add(model.Mail);
                                MailHelper mailHelper = new MailHelper();
                                mailHelper.Content = content;
                                mailHelper.FromMail = mailMessageTemplate.Mail;
                                mailHelper.Password = mailMessageTemplate.MailPassword;
                                mailHelper.ToMails = toMails;

                                mailHelper.Subject = mailMessageTemplate.MessagesMTTitle;
                                mailHelper.FromName = mailMessageTemplate.MailSendFromName;
                                mailHelper.Send();
                            }

                            processStatus.Result = null;
                            processStatus.ActiveResultRowCount = 1;
                            processStatus.TotolRowCount = processStatus.ActiveResultRowCount;
                            processStatus.Message.Header = "Store İşlemleri";
                            processStatus.Message.Text = "Başarılı";
                            processStatus.Status = true;
                            Transaction.Complete();
                        }
                        else
                        {
                            processStatus.Message.Header = "Store İşlemleri";
                            processStatus.Message.Text = "Store Bulunamadı";
                            processStatus.Status = false;
                            processStatus.Result = null;
                            processStatus.Error = null;
                        }
                    }
                    else
                    {
                        processStatus.Message.Header = "Store İşlemleri";
                        processStatus.Message.Text = "Başarısız";
                        processStatus.Status = false;
                        processStatus.Result = null;
                        processStatus.Error = null;
                    }
                }
                catch (Exception Error)
                {
                    foreach (var item in listDeleteFile)
                    {
                        FileHelpers.Delete(item);
                    }
                    processStatus.Message.Header = "Store İşlemleri";
                    processStatus.Message.Text = "Başarısız";
                    processStatus.Status = false;
                    processStatus.Result = null;
                    processStatus.Error = Error;
                }
            }
            return Request.CreateResponse(HttpStatusCode.OK, processStatus);
        }



        public HttpResponseMessage GetWorkingTime(MakinaTurkiye.Api.View.StoreWorkingTime Model)
        {
            ProcessResult processStatus = new ProcessResult();
            try
            {
                var LoginUserEmail = Request.CheckLoginUserClaims().LoginMemberEmail;
                var loginmember = !string.IsNullOrEmpty(LoginUserEmail) ? _memberService.GetMemberByMemberEmail(LoginUserEmail) : null;
                if (loginmember != null)
                {
                    var store = _storeService.GetStoreByMainPartyId(Model.MainPartyId);
                    if (store != null)
                    {
                        var phone = _phoneService.GetPhonesByMainPartyId(store.MainPartyId).FirstOrDefault();
                        string phoneTypeText = "";
                        switch (phone.PhoneType)
                        {
                            case (byte)PhoneType.Gsm:
                                phoneTypeText = "Cep Telefon";
                                break;

                            case (byte)PhoneType.Phone:
                                phoneTypeText = "İş telefonu";
                                break;

                            case (byte)PhoneType.Fax:
                                phoneTypeText = "Fax";
                                break;

                            default:
                                phoneTypeText = "Whatsapp";
                                break;
                        }
                        string name = GetEnumValue<PhoneType>(Convert.ToByte(phone.PhoneType.Value)).ToString();
                        MakinaTurkiye.Api.View.StoreWorkingTime StoreWorkingTime = new MakinaTurkiye.Api.View.StoreWorkingTime()
                        {
                            PhoneId=phone.PhoneId,
                            MainPartyId = store.MainPartyId,
                            PhoneName= phoneTypeText,
                            Phone= phone.PhoneCulture + " " + phone.PhoneAreaCode + " " + phone.PhoneNumber,
                            StartTime="",
                            EndTime = "",
                            IsAllDays = true,
                            IsSunday= true,
                            IsSaturday = true,
                        };

                        var setting = _memberSettingService.GetMemberSettingsBySettingNameWithStoreMainPartyId(name, store.MainPartyId).FirstOrDefault();
                        if (setting != null)
                        {
                            if (!string.IsNullOrEmpty(setting.SecondValue))
                            {
                                string[] weekendWorking = setting.SecondValue.Split('-');
                                StoreWorkingTime.IsSaturday = false;
                                StoreWorkingTime.IsSunday = false;
                                if (weekendWorking[0] == "1")
                                    StoreWorkingTime.IsSaturday = true;
                                if (weekendWorking[1] == "1")
                                    StoreWorkingTime.IsSunday = true;
                            }
                            StoreWorkingTime.StartTime = setting.FirstValue.Split('-')[0];
                            StoreWorkingTime.EndTime = setting.FirstValue.Split('-')[1];
                        }

                        StoreWorkingTime.IsAllDays =(bool)(StoreWorkingTime.IsSunday && StoreWorkingTime.IsSaturday);
                        processStatus.Result = StoreWorkingTime;
                        processStatus.ActiveResultRowCount = 1;
                        processStatus.TotolRowCount = processStatus.ActiveResultRowCount;
                        processStatus.Message.Header = "Store İşlemleri";
                        processStatus.Message.Text = "Başarılı";
                        processStatus.Status = true;
                    }
                    else
                    {
                        processStatus.Message.Header = "Store İşlemleri";
                        processStatus.Message.Text = "Store Bulunamadı";
                        processStatus.Status = false;
                        processStatus.Result = null;
                        processStatus.Error = null;
                    }
                }
                else
                {
                    processStatus.Message.Header = "Store İşlemleri";
                    processStatus.Message.Text = "Başarısız";
                    processStatus.Status = false;
                    processStatus.Result = null;
                    processStatus.Error = null;
                }
            }
            catch (Exception Error)
            {
                processStatus.Message.Header = "Store İşlemleri";
                processStatus.Message.Text = "Başarısız";
                processStatus.Status = false;
                processStatus.Result = null;
                processStatus.Error = Error;
            }
            return Request.CreateResponse(HttpStatusCode.OK, processStatus);
        }


        public HttpResponseMessage AddUpdateWorkingTime(MakinaTurkiye.Api.View.StoreWorkingTime Model)
        {
            if (Model.IsAllDays)
            {
                Model.IsSaturday = Model.IsAllDays;
                Model.IsSunday = Model.IsAllDays;
            }
            ProcessResult processStatus = new ProcessResult();
            using (System.Transactions.TransactionScope Transaction = new System.Transactions.TransactionScope(System.Transactions.TransactionScopeOption.Required, TimeSpan.FromMinutes(30)))
            {
                List<string> listDeleteFile = new List<string>();
                try
                {
                    var LoginUserEmail = Request.CheckLoginUserClaims().LoginMemberEmail;
                    var loginmember = !string.IsNullOrEmpty(LoginUserEmail) ? _memberService.GetMemberByMemberEmail(LoginUserEmail) : null;
                    if (loginmember != null)
                    {
                        var store = _storeService.GetStoreByMainPartyId(Model.MainPartyId);
                        if (store != null)
                        {
                            var phone = _phoneService.GetPhonesByMainPartyId(store.MainPartyId).FirstOrDefault();
                            if (phone != null)
                            {
                                var settingName = GetEnumValue<PhoneType>(Convert.ToByte(phone.PhoneType.Value)).ToString();
                                var setting = _memberSettingService.GetSettingBySettingName(settingName);
                                string name = GetEnumValue<PhoneType>((int)phone.PhoneType).ToString();
                                var memberSetting = _memberSettingService.GetMemberSettingsBySettingNameWithStoreMainPartyId(name, store.MainPartyId).FirstOrDefault();
                                if (memberSetting != null)
                                {
                                    if (Model.IsAllDays)
                                    {
                                        _memberSettingService.DeleteMemberSetting(memberSetting);
                                    }
                                    else
                                    {
                                        memberSetting.FirstValue = Model.StartTime + "-" + Model.EndTime;
                                        string weekendWorking = "";
                                        if (Model.IsSaturday)
                                            weekendWorking = "1";
                                        else
                                            weekendWorking = "0";

                                        if (Model.IsSunday)
                                        {
                                            weekendWorking = weekendWorking + "-1";
                                        }
                                        else
                                        {
                                            weekendWorking = weekendWorking + "-0";
                                        }
                                        memberSetting.SecondValue = weekendWorking;
                                        memberSetting.UpdateDate = DateTime.Now;
                                        _memberSettingService.UpdateMemberSetting(memberSetting);
                                    }
                                }
                                else
                                {
                                    memberSetting = new MakinaTurkiye.Entities.Tables.Settings.MemberSetting();
                                    memberSetting.SettingId = setting.SettingId;
                                    memberSetting.StoreMainPartyId = store.MainPartyId;
                                    memberSetting.FirstValue = Model.StartTime + "-" + Model.EndTime;
                                    string weekendWorking = "";
                                    if (Model.IsSaturday)
                                        weekendWorking = "1";
                                    else
                                        weekendWorking = "0";

                                    if (Model.IsSunday)
                                    {
                                        weekendWorking = weekendWorking + "-1";
                                    }
                                    else
                                    {
                                        weekendWorking = weekendWorking + "-0";
                                    }
                                    memberSetting.SecondValue = weekendWorking;
                                    memberSetting.RecordDate = DateTime.Now;
                                    memberSetting.UpdateDate = DateTime.Now;
                                    _memberSettingService.InsertMemberSetting(memberSetting);
                                }
                                processStatus.Result = null;
                                processStatus.ActiveResultRowCount = 1;
                                processStatus.TotolRowCount = processStatus.ActiveResultRowCount;
                                processStatus.Message.Header = "Store İşlemleri";
                                processStatus.Message.Text = "Başarılı";
                                processStatus.Status = true;
                                Transaction.Complete();

                            }
                            else
                            {
                                processStatus.Message.Header = "Store İşlemleri";
                                processStatus.Message.Text = "Phone Bulunamadı";
                                processStatus.Status = false;
                                processStatus.Result = null;
                                processStatus.Error = null;
                            }
                        }
                        else
                        {
                            processStatus.Message.Header = "Store İşlemleri";
                            processStatus.Message.Text = "Store Bulunamadı";
                            processStatus.Status = false;
                            processStatus.Result = null;
                            processStatus.Error = null;
                        }
                    }
                    else
                    {
                        processStatus.Message.Header = "Store İşlemleri";
                        processStatus.Message.Text = "Başarısız";
                        processStatus.Status = false;
                        processStatus.Result = null;
                        processStatus.Error = null;
                    }
                }
                catch (Exception Error)
                {
                    foreach (var item in listDeleteFile)
                    {
                        FileHelpers.Delete(item);
                    }
                    processStatus.Message.Header = "Store İşlemleri";
                    processStatus.Message.Text = "Başarısız";
                    processStatus.Status = false;
                    processStatus.Result = null;
                    processStatus.Error = Error;
                }
            }
            return Request.CreateResponse(HttpStatusCode.OK, processStatus);
        }


        public HttpResponseMessage GetVideo(int MainPartyId)
        {
            ProcessResult processStatus = new ProcessResult();
            try
            {
                var LoginUserEmail = Request.CheckLoginUserClaims().LoginMemberEmail;
                var loginmember = !string.IsNullOrEmpty(LoginUserEmail) ? _memberService.GetMemberByMemberEmail(LoginUserEmail) : null;
                if (loginmember != null)
                {
                    var store = _storeService.GetStoreByMainPartyId(MainPartyId);
                    var videos = _videoService.GetVideoByStoreMainPartyId(store.MainPartyId);
                    if (store != null)
                    {
                        string StoreSliderImageFolder = AppSettings.StoreSliderImageFolder;
                        StoreVideo StoreVideo = new StoreVideo()
                        {
                            MainPartyId = store.MainPartyId,
                            List = videos.Select(item => new StoreVideoItem()
                            {
                                File = ImageHelper.GetVideoImagePath(item.VideoPicturePath),
                                Minute = item.VideoMinute.HasValue ? item.VideoMinute.Value : 0,
                                VideoId = item.VideoId,
                                Second = item.VideoSecond.HasValue ? item.VideoSecond.Value : 0,
                                VideoPath = item.VideoPath,
                                ViewCount = item.SingularViewCount.HasValue ? item.SingularViewCount.Value : 0,
                                RecordDate = item.VideoRecordDate.Value,
                                Title = item.VideoTitle,
                                Order = item.Order.HasValue ? item.Order.Value : (byte)0
                            }).ToList(),
                        };
                        processStatus.Result = StoreVideo;
                        processStatus.ActiveResultRowCount = 1;
                        processStatus.TotolRowCount = processStatus.ActiveResultRowCount;
                        processStatus.Message.Header = "Store İşlemleri";
                        processStatus.Message.Text = "Başarılı";
                        processStatus.Status = true;
                    }
                    else
                    {
                        processStatus.Message.Header = "Store İşlemleri";
                        processStatus.Message.Text = "Store Bulunamadı";
                        processStatus.Status = false;
                        processStatus.Result = null;
                        processStatus.Error = null;
                    }
                }
                else
                {
                    processStatus.Message.Header = "Store İşlemleri";
                    processStatus.Message.Text = "Başarısız";
                    processStatus.Status = false;
                    processStatus.Result = null;
                    processStatus.Error = null;
                }
            }
            catch (Exception Error)
            {
                processStatus.Message.Header = "Store İşlemleri";
                processStatus.Message.Text = "Başarısız";
                processStatus.Status = false;
                processStatus.Result = null;
                processStatus.Error = Error;
            }

            return Request.CreateResponse(HttpStatusCode.OK, processStatus);
        }

        public HttpResponseMessage DeleteVideo(StoreVideo Model)
        {
            ProcessResult processStatus = new ProcessResult();
            using (System.Transactions.TransactionScope Transaction = new System.Transactions.TransactionScope(System.Transactions.TransactionScopeOption.Required, TimeSpan.FromMinutes(30)))
            {
                try
                {
                    var LoginUserEmail = Request.CheckLoginUserClaims().LoginMemberEmail;
                    var loginmember = !string.IsNullOrEmpty(LoginUserEmail) ? _memberService.GetMemberByMemberEmail(LoginUserEmail) : null;
                    if (loginmember != null)
                    {
                        var store = _storeService.GetStoreByMainPartyId(Model.MainPartyId);
                        if (store != null)
                        {
                            string StoreSliderImageFolder = AppSettings.StoreSliderImageFolder;
                            List<string> listDeleteFile = new List<string>();
                            var videos = _videoService.GetVideoByStoreMainPartyId(store.MainPartyId);
                            var storeImages = _pictureService.GetPictureByMainPartyIdWithStoreImageType(store.MainPartyId, StoreImageTypeEnum.StoreProfileSliderImage).ToList();
                            foreach (var item in Model.List)
                            {
                                var storevideo = videos.FirstOrDefault(x => x.VideoId == item.VideoId);
                                if (storevideo != null)
                                {
                                    var video = _videoService.GetVideoByVideoId(storevideo.VideoId);
                                    var imagePath = ImageHelper.GetVideoImagePath(video.VideoPicturePath);
                                    listDeleteFile.Add(imagePath);
                                    var videoPath = "/UserFiles/NewVideos/" + video.VideoPath + ".mp4";
                                    listDeleteFile.Add(videoPath);
                                    _videoService.DeleteVideo(video);
                                }
                            }
                            processStatus.Result = null;
                            processStatus.ActiveResultRowCount = 1;
                            processStatus.TotolRowCount = processStatus.ActiveResultRowCount;
                            processStatus.Message.Header = "Store İşlemleri";
                            processStatus.Message.Text = "Başarılı";
                            processStatus.Status = true;
                            Transaction.Complete();

                            //Dosyalar Diskten Siliniyor...

                            foreach (var item in listDeleteFile)
                            {
                                FileHelpers.Delete(item);
                            }

                        }
                        else
                        {
                            processStatus.Message.Header = "Store İşlemleri";
                            processStatus.Message.Text = "Store Bulunamadı";
                            processStatus.Status = false;
                            processStatus.Result = null;
                            processStatus.Error = null;
                        }
                    }
                    else
                    {
                        processStatus.Message.Header = "Store İşlemleri";
                        processStatus.Message.Text = "Başarısız";
                        processStatus.Status = false;
                        processStatus.Result = null;
                        processStatus.Error = null;
                    }
                }
                catch (Exception Error)
                {
                    processStatus.Message.Header = "Store İşlemleri";
                    processStatus.Message.Text = "Başarısız";
                    processStatus.Status = false;
                    processStatus.Result = null;
                    processStatus.Error = Error;
                }
            }
            return Request.CreateResponse(HttpStatusCode.OK, processStatus);
        }

        public HttpResponseMessage AddVideo(StoreVideo Model)
        {
            ProcessResult processStatus = new ProcessResult();
            using (System.Transactions.TransactionScope Transaction = new System.Transactions.TransactionScope(System.Transactions.TransactionScopeOption.Required, TimeSpan.FromMinutes(30)))
            {
                try
                {
                    var LoginUserEmail = Request.CheckLoginUserClaims().LoginMemberEmail;
                    var loginmember = !string.IsNullOrEmpty(LoginUserEmail) ? _memberService.GetMemberByMemberEmail(LoginUserEmail) : null;
                    if (loginmember != null)
                    {
                        var store = _storeService.GetStoreByMainPartyId(Model.MainPartyId);
                        if (store != null)
                        {
                            string StoreSliderImageFolder = AppSettings.StoreSliderImageFolder;
                            var storeImages = _pictureService.GetPictureByMainPartyIdWithStoreImageType(store.MainPartyId, StoreImageTypeEnum.StoreProfileSliderImage).ToList();
                            int imageCount = storeImages.Count();
                            foreach (var item in Model.List)
                            {
                                foreach (var model in Model.List)
                                {
                                    string Logo = model.File;
                                    string Uzanti = "";
                                    bool IslemDurum = false;
                                    if (Uzanti == "mp4")
                                    {
                                        IslemDurum = true;
                                    }
                                    if (IslemDurum)
                                    {
                                        string newFileName = !string.IsNullOrEmpty(model.Title) ? model.Title : $"{store.StoreName}-{Guid.NewGuid()}";
                                        var file = Logo.ToFile(newFileName);
                                        VideoModelHelper vModel = FileHelpers.fffmpegVideoConvert(file, AppSettings.TempFolder, AppSettings.VideoThumbnailFolder, AppSettings.NewVideosFolder, AppSettings.ffmpegFolder, 490, 328);
                                        DateTime timesplit;
                                        if (!(DateTime.TryParse(vModel.Duration, out timesplit)))
                                        {
                                            timesplit = DateTime.Now.Date;
                                        }
                                        var video =new MakinaTurkiye.Entities.Tables.Videos.Video()
                                        {
                                            Active = true,
                                            VideoPath = vModel.newFileName,
                                            VideoSize = null,
                                            VideoPicturePath = vModel.newFileName + ".jpg",
                                            VideoTitle = model.Title,
                                            VideoRecordDate = DateTime.Now,
                                            VideoMinute = (byte?)timesplit.Minute,
                                            VideoSecond = (byte?)timesplit.Second,
                                            StoreMainPartyId = store.MainPartyId
                                        };
                                        _videoService.InsertVideo(video);
                                    }
                                }
                            }
                            processStatus.Result = null;
                            processStatus.ActiveResultRowCount = 1;
                            processStatus.TotolRowCount = processStatus.ActiveResultRowCount;
                            processStatus.Message.Header = "Store İşlemleri";
                            processStatus.Message.Text = "Başarılı";
                            processStatus.Status = true;
                            Transaction.Complete();
                        }
                        else
                        {
                            processStatus.Message.Header = "Store İşlemleri";
                            processStatus.Message.Text = "Store Bulunamadı";
                            processStatus.Status = false;
                            processStatus.Result = null;
                            processStatus.Error = null;
                        }
                    }
                    else
                    {
                        processStatus.Message.Header = "Store İşlemleri";
                        processStatus.Message.Text = "Başarısız";
                        processStatus.Status = false;
                        processStatus.Result = null;
                        processStatus.Error = null;
                    }
                }
                catch (Exception Error)
                {
                    processStatus.Message.Header = "Store İşlemleri";
                    processStatus.Message.Text = "Başarısız";
                    processStatus.Status = false;
                    processStatus.Result = null;
                    processStatus.Error = Error;
                }
            }
            return Request.CreateResponse(HttpStatusCode.OK, processStatus);
        }

        public HttpResponseMessage GetBanner(int MainPartyId)
        {
            ProcessResult processStatus = new ProcessResult();
            try
            {
                var LoginUserEmail = Request.CheckLoginUserClaims().LoginMemberEmail;
                var loginmember = !string.IsNullOrEmpty(LoginUserEmail) ? _memberService.GetMemberByMemberEmail(LoginUserEmail) : null;
                if (loginmember != null)
                {
                    var store = _storeService.GetStoreByMainPartyId(MainPartyId);
                    if (store != null)
                    {
                        string StoreBanner = !string.IsNullOrEmpty(store.StoreBanner) ? ImageHelper.GetStoreBanner(store.MainPartyId, store.StoreBanner) : null;
                        processStatus.Result = StoreBanner;
                        processStatus.ActiveResultRowCount = 1;
                        processStatus.TotolRowCount = processStatus.ActiveResultRowCount;
                        processStatus.Message.Header = "Store İşlemleri";
                        processStatus.Message.Text = "Başarılı";
                        processStatus.Status = true;
                    }
                    else
                    {
                        processStatus.Message.Header = "Store İşlemleri";
                        processStatus.Message.Text = "Store Bulunamadı";
                        processStatus.Status = false;
                        processStatus.Result = null;
                        processStatus.Error = null;
                    }
                }
                else
                {
                    processStatus.Message.Header = "Store İşlemleri";
                    processStatus.Message.Text = "Başarısız";
                    processStatus.Status = false;
                    processStatus.Result = null;
                    processStatus.Error = null;
                }
            }
            catch (Exception Error)
            {
                processStatus.Message.Header = "Store İşlemleri";
                processStatus.Message.Text = "Başarısız";
                processStatus.Status = false;
                processStatus.Result = null;
                processStatus.Error = Error;
            }
            return Request.CreateResponse(HttpStatusCode.OK, processStatus);
        }

        [HttpPost]
        public HttpResponseMessage SetBanner(StoreBanner Model)
        {
            ProcessResult processStatus = new ProcessResult();
            try
            {
                string Logo = Model.Banner;
                var LoginUserEmail = Request.CheckLoginUserClaims().LoginMemberEmail;
                var loginmember = !string.IsNullOrEmpty(LoginUserEmail) ? _memberService.GetMemberByMemberEmail(LoginUserEmail) : null;
                if (loginmember != null)
                {
                    var store = _storeService.GetStoreByMainPartyId(Model.MainPartyId);
                    if (store != null)
                    {
                        if (!string.IsNullOrEmpty(store.StoreName))
                        {
                            // Gelen Base64String CVonvert Edilerek Kayıt Edilecek...
                            string Uzanti = Logo.GetUzanti();
                            if (!string.IsNullOrEmpty(Uzanti))
                            {
                                bool IslemDurum = false;
                                string ServerImageUrl = "";
                                if (Uzanti == "jpg")
                                {
                                    IslemDurum = true;
                                }
                                if (Uzanti == "png")
                                {
                                    IslemDurum = true;
                                }

                                if (IslemDurum)
                                {
                                    if (!string.IsNullOrEmpty(store.StoreBanner))
                                    {
                                        string ServerPath = System.Web.Hosting.HostingEnvironment.MapPath(AppSettings.StoreBannerFolder) + store.StoreBanner;
                                        if (System.IO.File.Exists(ServerPath))
                                        {
                                            FileHelpers.Delete(ServerPath);
                                        }
                                        ServerPath = System.Web.Hosting.HostingEnvironment.MapPath(AppSettings.StoreBannerFolder) + store.StoreBanner.Replace("_banner", "-1400x280");
                                        if (System.IO.File.Exists(ServerPath))
                                        {
                                            FileHelpers.Delete(ServerPath);
                                        }
                                    }

                                    string mapPath = System.Web.Hosting.HostingEnvironment.MapPath(AppSettings.StoreBannerFolder);
                                    string storeBannerImageFileName = store.StoreUrlName.ToImageFileName() + "-" + store.MainPartyId + "_banner.jpg";
                                    string storeBannerImageFileSavePath = mapPath + storeBannerImageFileName;

                                    System.Drawing.Image Img = Logo.ToImage();
                                    if (Uzanti == "png")
                                    {
                                        Img.Save(storeBannerImageFileSavePath, System.Drawing.Imaging.ImageFormat.Png);
                                    }
                                    if (Uzanti == "jpg")
                                    {
                                        Img.Save(storeBannerImageFileSavePath, System.Drawing.Imaging.ImageFormat.Jpeg);
                                    }

                                    Img = ImageProcessHelper.resizeImageBanner(1400, 280, storeBannerImageFileSavePath);

                                    ImageProcessHelper.SaveJpeg(storeBannerImageFileSavePath, Img, 80, "_banner", "-1400x280");

                                    store.StoreBanner = storeBannerImageFileName;

                                    _storeService.UpdateStore(store);
                                }
                                processStatus.Result = store.StoreBanner;
                                processStatus.ActiveResultRowCount = 1;
                                processStatus.TotolRowCount = processStatus.ActiveResultRowCount;
                                processStatus.Message.Header = "Store İşlemleri";
                                processStatus.Message.Text = "Başarılı";
                                processStatus.Status = true;
                            }
                            else
                            {
                                processStatus.ActiveResultRowCount = 1;
                                processStatus.TotolRowCount = processStatus.ActiveResultRowCount;
                                processStatus.Message.Header = "Store İşlemleri";
                                processStatus.Message.Text = "Resim Uzantısı Hatalı";
                                processStatus.Status = false;
                            }
                        }
                        else
                        {
                            processStatus.ActiveResultRowCount = 1;
                            processStatus.TotolRowCount = processStatus.ActiveResultRowCount;
                            processStatus.Message.Header = "Store İşlemleri";
                            processStatus.Message.Text = "Mağaza Adı Boş";
                            processStatus.Status = false;
                        }
                    }
                    else
                    {
                        processStatus.ActiveResultRowCount = 1;
                        processStatus.TotolRowCount = processStatus.ActiveResultRowCount;
                        processStatus.Message.Header = "Store İşlemleri";
                        processStatus.Message.Text = "Mağaza Bulunamadı";
                        processStatus.Status = false;
                    }
                }
                else
                {
                    processStatus.Message.Header = "Store İşlemleri";
                    processStatus.Message.Text = "Başarısız";
                    processStatus.Status = false;
                    processStatus.Result = null;
                    processStatus.Error = null;
                }
            }
            catch (Exception Error)
            {
                processStatus.Message.Header = "Store İşlemleri";
                processStatus.Message.Text = "Başarısız";
                processStatus.Status = false;
                processStatus.Result = null;
                processStatus.Error = Error;
            }
            return Request.CreateResponse(HttpStatusCode.OK, processStatus);
        }

        public HttpResponseMessage GetAbout(int MainPartyId)
        {
            ProcessResult processStatus = new ProcessResult();
            try
            {
                var LoginUserEmail = Request.CheckLoginUserClaims().LoginMemberEmail;
                var loginmember = !string.IsNullOrEmpty(LoginUserEmail) ? _memberService.GetMemberByMemberEmail(LoginUserEmail) : null;
                if (loginmember != null)
                {
                    MTStoreAboutModel model = new MTStoreAboutModel();
                    var store = _storeService.GetStoreByMainPartyId(MainPartyId);
                    if (store != null)
                    {
                        processStatus.Result = store.StoreAbout;
                        processStatus.ActiveResultRowCount = 1;
                        processStatus.TotolRowCount = processStatus.ActiveResultRowCount;
                        processStatus.Message.Header = "Store İşlemleri";
                        processStatus.Message.Text = "Başarılı";
                        processStatus.Status = true;
                    }
                    else
                    {
                        processStatus.Message.Header = "Store İşlemleri";
                        processStatus.Message.Text = "Store Bulunamadı";
                        processStatus.Status = false;
                        processStatus.Result = null;
                        processStatus.Error = null;
                    }
                }
                else
                {
                    processStatus.Message.Header = "Store İşlemleri";
                    processStatus.Message.Text = "Başarısız";
                    processStatus.Status = false;
                    processStatus.Result = null;
                    processStatus.Error = null;
                }
            }
            catch (Exception Error)
            {
                processStatus.Message.Header = "Store İşlemleri";
                processStatus.Message.Text = "Başarısız";
                processStatus.Status = false;
                processStatus.Result = null;
                processStatus.Error = Error;
            }
            return Request.CreateResponse(HttpStatusCode.OK, processStatus);
        }

        [HttpPost]
        public HttpResponseMessage SetAbout(int MainPartyId, string About)
        {
            ProcessResult processStatus = new ProcessResult();
            try
            {
                var LoginUserEmail = Request.CheckLoginUserClaims().LoginMemberEmail;
                var loginmember = !string.IsNullOrEmpty(LoginUserEmail) ? _memberService.GetMemberByMemberEmail(LoginUserEmail) : null;
                if (loginmember != null)
                {
                    var store = _storeService.GetStoreByMainPartyId(MainPartyId);
                    if (store != null)
                    {
                        store.StoreAbout = About;
                        _storeService.UpdateStore(store);
                        processStatus.Result = store.StoreAbout;
                        processStatus.ActiveResultRowCount = 1;
                        processStatus.TotolRowCount = processStatus.ActiveResultRowCount;
                        processStatus.Message.Header = "Store İşlemleri";
                        processStatus.Message.Text = "Başarılı";
                        processStatus.Status = true;
                    }
                    else
                    {
                        processStatus.Message.Header = "Store İşlemleri";
                        processStatus.Message.Text = "Store Bulunamadı";
                        processStatus.Status = false;
                        processStatus.Result = null;
                        processStatus.Error = null;
                    }
                }
                else
                {
                    processStatus.Message.Header = "Store İşlemleri";
                    processStatus.Message.Text = "Başarısız";
                    processStatus.Status = false;
                    processStatus.Result = null;
                    processStatus.Error = null;
                }
            }
            catch (Exception Error)
            {
                processStatus.Message.Header = "Store İşlemleri";
                processStatus.Message.Text = "Başarısız";
                processStatus.Status = false;
                processStatus.Result = null;
                processStatus.Error = Error;
            }
            return Request.CreateResponse(HttpStatusCode.OK, processStatus);
        }

        public HttpResponseMessage GetHomePageInfo(int MainPartyId)
        {
            ProcessResult processStatus = new ProcessResult();
            try
            {
                var LoginUserEmail = Request.CheckLoginUserClaims().LoginMemberEmail;
                var loginmember = !string.IsNullOrEmpty(LoginUserEmail) ? _memberService.GetMemberByMemberEmail(LoginUserEmail) : null;
                if (loginmember != null)
                {
                    var store = _storeService.GetStoreByMainPartyId(MainPartyId);
                    if (store != null)
                    {
                        processStatus.Result = store.StoreProfileHomeDescription;
                        processStatus.ActiveResultRowCount = 1;
                        processStatus.TotolRowCount = processStatus.ActiveResultRowCount;
                        processStatus.Message.Header = "Store İşlemleri";
                        processStatus.Message.Text = "Başarılı";
                        processStatus.Status = true;
                    }
                    else
                    {
                        processStatus.Message.Header = "Store İşlemleri";
                        processStatus.Message.Text = "Store Bulunamadı";
                        processStatus.Status = false;
                        processStatus.Result = null;
                        processStatus.Error = null;
                    }
                }
                else
                {
                    processStatus.Message.Header = "Store İşlemleri";
                    processStatus.Message.Text = "Başarısız";
                    processStatus.Status = false;
                    processStatus.Result = null;
                    processStatus.Error = null;
                }
            }
            catch (Exception Error)
            {
                processStatus.Message.Header = "Store İşlemleri";
                processStatus.Message.Text = "Başarısız";
                processStatus.Status = false;
                processStatus.Result = null;
                processStatus.Error = Error;
            }
            return Request.CreateResponse(HttpStatusCode.OK, processStatus);
        }

        [HttpPost]
        public HttpResponseMessage SetHomePageInfo(int MainPartyId, string StoreProfileHomeDescription)
        {
            ProcessResult processStatus = new ProcessResult();
            try
            {
                var LoginUserEmail = Request.CheckLoginUserClaims().LoginMemberEmail;
                var loginmember = !string.IsNullOrEmpty(LoginUserEmail) ? _memberService.GetMemberByMemberEmail(LoginUserEmail) : null;
                if (loginmember != null)
                {
                    var store = _storeService.GetStoreByMainPartyId(MainPartyId);
                    if (store != null)
                    {
                        store.StoreProfileHomeDescription = StoreProfileHomeDescription;
                        _storeService.UpdateStore(store);
                        processStatus.Result = store.StoreProfileHomeDescription;
                        processStatus.ActiveResultRowCount = 1;
                        processStatus.TotolRowCount = processStatus.ActiveResultRowCount;
                        processStatus.Message.Header = "Store İşlemleri";
                        processStatus.Message.Text = "Başarılı";
                        processStatus.Status = true;
                    }
                    else
                    {
                        processStatus.Message.Header = "Store İşlemleri";
                        processStatus.Message.Text = "Store Bulunamadı";
                        processStatus.Status = false;
                        processStatus.Result = null;
                        processStatus.Error = null;
                    }
                }
                else
                {
                    processStatus.Message.Header = "Store İşlemleri";
                    processStatus.Message.Text = "Başarısız";
                    processStatus.Status = false;
                    processStatus.Result = null;
                    processStatus.Error = null;
                }
            }
            catch (Exception Error)
            {
                processStatus.Message.Header = "Store İşlemleri";
                processStatus.Message.Text = "Başarısız";
                processStatus.Status = false;
                processStatus.Result = null;
                processStatus.Error = Error;
            }
            return Request.CreateResponse(HttpStatusCode.OK, processStatus);
        }

        public HttpResponseMessage GetLogo(int MainPartyId)
        {
            ProcessResult processStatus = new ProcessResult();
            try
            {
                var LoginUserEmail = Request.CheckLoginUserClaims().LoginMemberEmail;
                var loginmember = !string.IsNullOrEmpty(LoginUserEmail) ? _memberService.GetMemberByMemberEmail(LoginUserEmail) : null;
                if (loginmember != null)
                {
                    var store = _storeService.GetStoreByMainPartyId(MainPartyId);
                    if (store != null)
                    {
                        string StoreLogo = !string.IsNullOrEmpty(store.StoreLogo) ? "https:" + ImageHelper.GetStoreLogoParh(store.MainPartyId, store.StoreLogo, 300) : null;
                        processStatus.Result = StoreLogo;
                        processStatus.ActiveResultRowCount = 1;
                        processStatus.TotolRowCount = processStatus.ActiveResultRowCount;
                        processStatus.Message.Header = "Store İşlemleri";
                        processStatus.Message.Text = "Başarılı";
                        processStatus.Status = true;
                    }
                    else
                    {
                        processStatus.Message.Header = "Store İşlemleri";
                        processStatus.Message.Text = "Store Bulunamadı";
                        processStatus.Status = false;
                        processStatus.Result = null;
                        processStatus.Error = null;
                    }
                }
                else
                {
                    processStatus.Message.Header = "Store İşlemleri";
                    processStatus.Message.Text = "Başarısız";
                    processStatus.Status = false;
                    processStatus.Result = null;
                    processStatus.Error = null;
                }
            }
            catch (Exception Error)
            {
                processStatus.Message.Header = "Store İşlemleri";
                processStatus.Message.Text = "Başarısız";
                processStatus.Status = false;
                processStatus.Result = null;
                processStatus.Error = Error;
            }
            return Request.CreateResponse(HttpStatusCode.OK, processStatus);
        }

        [HttpPost]
        public HttpResponseMessage SetLogo(StoreLogo Model)
        {
            ProcessResult processStatus = new ProcessResult();
            try
            {
                string Logo = Model.Logo;
                var LoginUserEmail = Request.CheckLoginUserClaims().LoginMemberEmail;
                var loginmember = !string.IsNullOrEmpty(LoginUserEmail) ? _memberService.GetMemberByMemberEmail(LoginUserEmail) : null;
                if (loginmember != null)
                {
                    var store = _storeService.GetStoreByMainPartyId(Model.MainPartyId);
                    if (store != null)
                    {
                        if (!string.IsNullOrEmpty(store.StoreName))
                        {
                            // Gelen Base64String CVonvert Edilerek Kayıt Edilecek...
                            string Uzanti = Logo.GetUzanti();
                            if (!string.IsNullOrEmpty(Uzanti))
                            {
                                bool IslemDurum = false;
                                string ServerImageUrl = "";
                                if (Uzanti == "jpg")
                                {
                                    IslemDurum = true;
                                }
                                if (Uzanti == "png")
                                {
                                    IslemDurum = true;
                                }

                                if (IslemDurum)
                                {
                                    string storeLogoFolder = System.Web.Hosting.HostingEnvironment.MapPath(AppSettings.StoreLogoFolder);
                                    string resizeStoreFolder = System.Web.Hosting.HostingEnvironment.MapPath(AppSettings.ResizeStoreLogoFolder);
                                    string storeLogoThumbSize = AppSettings.StoreLogoThumbSizes;
                                    List<string> thumbSizesForStoreLogo = new List<string>();
                                    thumbSizesForStoreLogo.AddRange(storeLogoThumbSize.Split(';'));
                                    var di = System.IO.Directory.CreateDirectory(string.Format("{0}{1}", resizeStoreFolder, store.MainPartyId.ToString()));
                                    di.CreateSubdirectory("thumbs");

                                    string newStoreLogoImageFilePath = resizeStoreFolder + store.MainPartyId.ToString() + "\\";
                                    string newStoreLogoImageFileName = store.StoreName.ToImageFileName() + "_logo." + Uzanti;

                                    ServerImageUrl = $"~{AppSettings.StoreLogoFolder}/{store.StoreName.ToImageFileName()}_logo.{Uzanti}";
                                    string ServerFile = System.Web.Hosting.HostingEnvironment.MapPath(ServerImageUrl);
                                    System.Drawing.Image Img = Logo.ToImage();
                                    if (Uzanti == "png")
                                    {
                                        Img.Save(ServerFile, System.Drawing.Imaging.ImageFormat.Png);
                                    }
                                    if (Uzanti == "jpg")
                                    {
                                        Img.Save(ServerFile, System.Drawing.Imaging.ImageFormat.Jpeg);
                                    }
                                    store.StoreLogo = ServerImageUrl;
                                    _storeService.UpdateStore(store);
                                    bool thumbResult = ImageProcessHelper.ImageResize(ServerFile, newStoreLogoImageFilePath + "thumbs\\" + store.StoreName.ToImageFileName(), thumbSizesForStoreLogo);
                                }
                                processStatus.ActiveResultRowCount = 1;
                                processStatus.TotolRowCount = processStatus.ActiveResultRowCount;
                                processStatus.Message.Header = "Store İşlemleri";
                                processStatus.Message.Text = "Başarılı";
                                processStatus.Status = true;
                            }
                            else
                            {
                                processStatus.ActiveResultRowCount = 1;
                                processStatus.TotolRowCount = processStatus.ActiveResultRowCount;
                                processStatus.Message.Header = "Store İşlemleri";
                                processStatus.Message.Text = "Resim Uzantısı Hatalı";
                                processStatus.Status = false;
                            }
                        }
                        else
                        {
                            processStatus.ActiveResultRowCount = 1;
                            processStatus.TotolRowCount = processStatus.ActiveResultRowCount;
                            processStatus.Message.Header = "Store İşlemleri";
                            processStatus.Message.Text = "Mağaza Adı Boş";
                            processStatus.Status = false;
                        }
                    }
                    else
                    {
                        processStatus.ActiveResultRowCount = 1;
                        processStatus.TotolRowCount = processStatus.ActiveResultRowCount;
                        processStatus.Message.Header = "Store İşlemleri";
                        processStatus.Message.Text = "Mağaza Bulunamadı";
                        processStatus.Status = false;
                    }
                }
                else
                {
                    processStatus.Message.Header = "Store İşlemleri";
                    processStatus.Message.Text = "Başarısız";
                    processStatus.Status = false;
                    processStatus.Result = null;
                    processStatus.Error = null;
                }
            }
            catch (Exception Error)
            {
                processStatus.Message.Header = "Store İşlemleri";
                processStatus.Message.Text = "Başarısız";
                processStatus.Status = false;
                processStatus.Result = null;
                processStatus.Error = Error;
            }
            return Request.CreateResponse(HttpStatusCode.OK, processStatus);
        }

        public HttpResponseMessage GetBrand(int MainPartyId)
        {
            ProcessResult processStatus = new ProcessResult();
            try
            {
                var LoginUserEmail = Request.CheckLoginUserClaims().LoginMemberEmail;
                var loginmember = !string.IsNullOrEmpty(LoginUserEmail) ? _memberService.GetMemberByMemberEmail(LoginUserEmail) : null;
                if (loginmember != null)
                {
                    var List = _storeBrandService.GetStoreBrandByMainPartyId(MainPartyId);
                    processStatus.Result = List;
                    processStatus.ActiveResultRowCount = List.Count;
                    processStatus.TotolRowCount = processStatus.ActiveResultRowCount;
                    processStatus.Message.Header = "Store İşlemleri";
                    processStatus.Message.Text = "Başarılı";
                    processStatus.Status = true;
                }
                else
                {
                    processStatus.Message.Header = "Store İşlemleri";
                    processStatus.Message.Text = "Başarısız";
                    processStatus.Status = false;
                    processStatus.Result = null;
                    processStatus.Error = null;
                }
            }
            catch (Exception Error)
            {
                processStatus.Message.Header = "Store İşlemleri";
                processStatus.Message.Text = "Başarısız";
                processStatus.Status = false;
                processStatus.Result = null;
                processStatus.Error = Error;
            }
            return Request.CreateResponse(HttpStatusCode.OK, processStatus);
        }

        public HttpResponseMessage DeleteBrand(int StoreBrandId)
        {
            ProcessResult processStatus = new ProcessResult();
            try
            {
                var LoginUserEmail = Request.CheckLoginUserClaims().LoginMemberEmail;
                var loginmember = !string.IsNullOrEmpty(LoginUserEmail) ? _memberService.GetMemberByMemberEmail(LoginUserEmail) : null;
                if (loginmember != null)
                {
                    var StoreBrand = _storeBrandService.GetStoreBrandByStoreBrand(StoreBrandId);
                    if (StoreBrand != null)
                    {
                        var store = _storeService.GetStoreByMainPartyId(StoreBrand.MainPartyId);
                        if (store != null)
                        {
                            _storeBrandService.DeleteStoreBrand(StoreBrand);
                            processStatus.Result = "";
                            processStatus.ActiveResultRowCount = 1;
                            processStatus.TotolRowCount = processStatus.ActiveResultRowCount;
                            processStatus.Message.Header = "Store İşlemleri";
                            processStatus.Message.Text = "Başarılı";
                            processStatus.Status = true;
                        }
                        else
                        {
                            processStatus.Message.Header = "Store İşlemleri";
                            processStatus.Message.Text = "Store Bulunamadı.";
                            processStatus.Status = false;
                            processStatus.Result = null;
                        }
                    }
                    else
                    {
                        processStatus.Message.Header = "Store İşlemleri";
                        processStatus.Message.Text = "Brand Bulunamadı";
                        processStatus.Status = false;
                        processStatus.Result = null;
                        processStatus.Error = null;
                    }
                }
                else
                {
                    processStatus.Message.Header = "Store İşlemleri";
                    processStatus.Message.Text = "Başarısız";
                    processStatus.Status = false;
                    processStatus.Result = null;
                    processStatus.Error = null;
                }
            }
            catch (Exception Error)
            {
                processStatus.Message.Header = "Store İşlemleri";
                processStatus.Message.Text = "Başarısız";
                processStatus.Status = false;
                processStatus.Result = null;
                processStatus.Error = Error;
            }
            return Request.CreateResponse(HttpStatusCode.OK, processStatus);
        }

        [HttpPost]
        public HttpResponseMessage InsertBrand(int MainPartyId, string BrandName, string BrandDescription, string Logo)
        {
            ProcessResult processStatus = new ProcessResult();
            try
            {
                var LoginUserEmail = Request.CheckLoginUserClaims().LoginMemberEmail;
                var loginmember = !string.IsNullOrEmpty(LoginUserEmail) ? _memberService.GetMemberByMemberEmail(LoginUserEmail) : null;
                if (loginmember != null)
                {
                    var store = _storeService.GetStoreByMainPartyId(MainPartyId);
                    if (store != null)
                    {
                        string Uzanti = Logo.GetUzanti();
                        var Img = Logo.ToImage();
                        string fileName = FileHelpers.ImageThumbnail(AppSettings.DealerBrandImageFolder, Img, Uzanti, 50, FileHelpers.ThumbnailType.Width);
                        var StoreBrand = new StoreBrand()
                        {
                            BrandPicture = fileName,
                            MainPartyId = MainPartyId,
                            BrandDescription = BrandDescription,
                            BrandName = BrandName,
                        };
                        _storeBrandService.InsertStoreBrand(StoreBrand);
                        processStatus.Result = StoreBrand;
                        processStatus.ActiveResultRowCount = 1;
                        processStatus.TotolRowCount = processStatus.ActiveResultRowCount;
                        processStatus.Message.Header = "Store İşlemleri";
                        processStatus.Message.Text = "Başarılı";
                        processStatus.Status = true;
                    }
                    else
                    {
                        processStatus.Message.Header = "Store İşlemleri";
                        processStatus.Message.Text = "Store Bulunamadı.";
                        processStatus.Status = false;
                        processStatus.Result = null;
                    }
                }
                else
                {
                    processStatus.Message.Header = "Store İşlemleri";
                    processStatus.Message.Text = "Başarısız";
                    processStatus.Status = false;
                    processStatus.Result = null;
                    processStatus.Error = null;
                }
            }
            catch (Exception Error)
            {
                processStatus.Message.Header = "Store İşlemleri";
                processStatus.Message.Text = "Başarısız";
                processStatus.Status = false;
                processStatus.Result = null;
                processStatus.Error = Error;
            }
            return Request.CreateResponse(HttpStatusCode.OK, processStatus);
        }

        public HttpResponseMessage GetDealerShip(int MainPartyId)
        {
            ProcessResult processStatus = new ProcessResult();
            try
            {
                var LoginUserEmail = Request.CheckLoginUserClaims().LoginMemberEmail;
                var loginmember = !string.IsNullOrEmpty(LoginUserEmail) ? _memberService.GetMemberByMemberEmail(LoginUserEmail) : null;
                if (loginmember != null)
                {
                    var List = _dealarBrandService.GetDealarBrandsByMainPartyId(MainPartyId);
                    processStatus.Result = List;
                    processStatus.ActiveResultRowCount = List.Count;
                    processStatus.TotolRowCount = processStatus.ActiveResultRowCount;
                    processStatus.Message.Header = "Store İşlemleri";
                    processStatus.Message.Text = "Başarılı";
                    processStatus.Status = true;
                }
                else
                {
                    processStatus.Message.Header = "Store İşlemleri";
                    processStatus.Message.Text = "Başarısız";
                    processStatus.Status = false;
                    processStatus.Result = null;
                    processStatus.Error = null;
                }
            }
            catch (Exception Error)
            {
                processStatus.Message.Header = "Store İşlemleri";
                processStatus.Message.Text = "Başarısız";
                processStatus.Status = false;
                processStatus.Result = null;
                processStatus.Error = Error;
            }
            return Request.CreateResponse(HttpStatusCode.OK, processStatus);
        }

        public HttpResponseMessage DeleteDealerShip(int DealerBrandId)
        {
            ProcessResult processStatus = new ProcessResult();
            try
            {
                var LoginUserEmail = Request.CheckLoginUserClaims().LoginMemberEmail;
                var loginmember = !string.IsNullOrEmpty(LoginUserEmail) ? _memberService.GetMemberByMemberEmail(LoginUserEmail) : null;
                if (loginmember != null)
                {
                    var dealerBrand = _dealarBrandService.GetDealerBrandByDealerBrandId(DealerBrandId);
                    if (dealerBrand != null)
                    {
                        var store = _storeService.GetStoreByMainPartyId(dealerBrand.MainPartyId);
                        if (store != null)
                        {
                            _dealarBrandService.DeleteDealerBrand(dealerBrand);
                            processStatus.Result = "";
                            processStatus.ActiveResultRowCount = 1;
                            processStatus.TotolRowCount = processStatus.ActiveResultRowCount;
                            processStatus.Message.Header = "Store İşlemleri";
                            processStatus.Message.Text = "Başarılı";
                            processStatus.Status = true;
                        }
                        else
                        {
                            processStatus.Message.Header = "Store İşlemleri";
                            processStatus.Message.Text = "Store Bulunamadı.";
                            processStatus.Status = false;
                            processStatus.Result = null;
                        }
                    }
                    else
                    {
                        processStatus.Message.Header = "Store İşlemleri";
                        processStatus.Message.Text = "DealerShip Bulunamadı";
                        processStatus.Status = false;
                        processStatus.Result = null;
                        processStatus.Error = null;
                    }
                }
                else
                {
                    processStatus.Message.Header = "Store İşlemleri";
                    processStatus.Message.Text = "Başarısız";
                    processStatus.Status = false;
                    processStatus.Result = null;
                    processStatus.Error = null;
                }
            }
            catch (Exception Error)
            {
                processStatus.Message.Header = "Store İşlemleri";
                processStatus.Message.Text = "Başarısız";
                processStatus.Status = false;
                processStatus.Result = null;
                processStatus.Error = Error;
            }
            return Request.CreateResponse(HttpStatusCode.OK, processStatus);
        }

        [HttpPost]
        public HttpResponseMessage InsertDealerShip(int MainPartyId, string BrandName, string Logo)
        {
            ProcessResult processStatus = new ProcessResult();
            try
            {
                var store = _storeService.GetStoreByMainPartyId(MainPartyId);
                if (store != null)
                {
                    string Uzanti = Logo.GetUzanti();
                    var Img = Logo.ToImage();
                    string fileName = FileHelpers.ImageThumbnail(AppSettings.DealerBrandImageFolder, Img, Uzanti, 50, FileHelpers.ThumbnailType.Width);
                    var curDealerBrand = new DealerBrand()
                    {
                        DealerBrandPicture = fileName,
                        MainPartyId = MainPartyId,
                        DealerBrandName = BrandName
                    };

                    _dealarBrandService.InsertDealerBrand(curDealerBrand);
                    processStatus.Result = curDealerBrand;
                    processStatus.ActiveResultRowCount = 1;
                    processStatus.TotolRowCount = processStatus.ActiveResultRowCount;
                    processStatus.Message.Header = "Store İşlemleri";
                    processStatus.Message.Text = "Başarılı";
                    processStatus.Status = true;
                }
                else
                {
                    processStatus.Message.Header = "Store İşlemleri";
                    processStatus.Message.Text = "Store Bulunamadı.";
                    processStatus.Status = false;
                    processStatus.Result = null;
                }
            }
            catch (Exception Error)
            {
                processStatus.Message.Header = "Store İşlemleri";
                processStatus.Message.Text = "Başarısız";
                processStatus.Status = false;
                processStatus.Result = null;
                processStatus.Error = Error;
            }
            return Request.CreateResponse(HttpStatusCode.OK, processStatus);
        }

        public HttpResponseMessage GetWithName(string Name)
        {
            ProcessResult processStatus = new ProcessResult();
            try
            {
                var Result = _storeService.GetStoreSearchByStoreName(Name);
                foreach (var item in Result)
                {
                    item.StoreLogo = !string.IsNullOrEmpty(item.StoreLogo) ? "https:" + ImageHelper.GetStoreLogoParh(item.MainPartyId, item.StoreLogo, 300) : null;
                }
                processStatus.Result = Result;
                processStatus.ActiveResultRowCount = Result.Count;
                processStatus.TotolRowCount = processStatus.ActiveResultRowCount;
                processStatus.Message.Header = "Store İşlemleri";
                processStatus.Message.Text = "Başarılı";
                processStatus.Status = true;
            }
            catch (Exception Error)
            {
                processStatus.Message.Header = "Store İşlemleri";
                processStatus.Message.Text = "Başarısız";
                processStatus.Status = false;
                processStatus.Result = null;
                processStatus.Error = Error;
            }
            return Request.CreateResponse(HttpStatusCode.OK, processStatus);
        }

        public HttpResponseMessage GetStoreByStoreUrlName(string storeUrlName)
        {
            ProcessResult processStatus = new ProcessResult();
            try
            {
                var Result = _storeService.GetStoreByStoreUrlName(storeUrlName);
                Result.StoreLogo = !string.IsNullOrEmpty(Result.StoreLogo) ? "https:" + ImageHelper.GetStoreLogoParh(Result.MainPartyId, Result.StoreLogo, 300) : null;
                processStatus.Result = Result;
                processStatus.ActiveResultRowCount = 1;
                processStatus.TotolRowCount = processStatus.ActiveResultRowCount;
                processStatus.Message.Header = "Store İşlemleri";
                processStatus.Message.Text = "Başarılı";
                processStatus.Status = true;
            }
            catch (Exception Error)
            {
                processStatus.Message.Header = "Store İşlemleri";
                processStatus.Message.Text = "Başarısız";
                processStatus.Status = false;
                processStatus.Result = null;
                processStatus.Error = Error;
            }
            return Request.CreateResponse(HttpStatusCode.OK, processStatus);
        }

        public HttpResponseMessage GetByMainPartyId(int MainPartyId)
        {
            ProcessResult processStatus = new ProcessResult();
            try
            {
                var Result = _storeService.GetStoreByMainPartyId(MainPartyId);
                Result.StoreLogo = !string.IsNullOrEmpty(Result.StoreLogo) ? "https:" + ImageHelper.GetStoreLogoParh(Result.MainPartyId, Result.StoreLogo, 300) : null;
                processStatus.Result = Result;
                processStatus.ActiveResultRowCount = 1;
                processStatus.TotolRowCount = processStatus.ActiveResultRowCount;
                processStatus.Message.Header = "Store İşlemleri";
                processStatus.Message.Text = "Başarılı";
                processStatus.Status = true;
            }
            catch (Exception Error)
            {
                processStatus.Message.Header = "Store İşlemleri";
                processStatus.Message.Text = "Başarısız";
                processStatus.Status = false;
                processStatus.Result = null;
                processStatus.Error = Error;
            }
            return Request.CreateResponse(HttpStatusCode.OK, processStatus);
        }

        public HttpResponseMessage GetStoreDetailByMainPartyId(int MainPartyId)
        {
            ProcessResult processStatus = new ProcessResult();
            try
            {
                MTStoreAboutModel model = new MTStoreAboutModel();
                var store = _storeService.GetStoreByMainPartyId(MainPartyId);
                if (!string.IsNullOrEmpty(store.StoreProfileHomeDescription))
                {
                    model.AboutText = store.StoreProfileHomeDescription;
                    model.IsAboutText = false;
                }

                model.AboutImagePath = ImageHelper.GetStoreImage(store.MainPartyId, store.StoreLogo, "300");
                model.StorePicture = ImageHelper.GetStoreProfilePicture(store.StorePicture);
                model.StoreName = store.StoreName;
                var storeActivtyType = _storeActivityTypeService.GetStoreActivityTypesByStoreId(store.MainPartyId);
                foreach (var activity in storeActivtyType.ToList())
                {
                    model.StoreActivity += activity.ActivityType.ActivityName + " ";
                }
                if (store.StoreEmployeesCount > 0)
                    model.StoreEmployeeCount = _constantService.GetConstantByConstantId((short)store.StoreEmployeesCount).ConstantName;
                if (store.StoreActiveType != null)
                    if (store.StoreActiveType > 0)
                    {
                        if (store.StoreType != null)
                        {
                            var constant = _constantService.GetConstantByConstantId((short)store.StoreType);
                            if (constant != null)
                            {
                                model.StoreType = constant.ConstantName;
                            }
                        }
                    }
                model.StoreEstablishmentDate = Convert.ToString(store.StoreEstablishmentDate);
                if (store.StoreCapital != null)
                    if (store.StoreCapital > 0)
                        model.StoreCapital = _constantService.GetConstantByConstantId((short)store.StoreCapital).ConstantName;
                if (store.StoreEndorsement != null)
                    if (store.StoreEndorsement > 0)
                        model.StoreEndorsement = _constantService.GetConstantByConstantId((short)store.StoreEndorsement).ConstantName;
                model.TaxNumber = store.TaxOffice;
                model.TaxOffice = store.TaxOffice;
                model.TradeRegistrNo = store.TradeRegistrNo;
                model.MersisNo = store.MersisNo;
                model.StoreAboutShort = store.StoreAbout;
                model.StoreUrl = UrlBuilder.GetStoreProfileUrl(store.MainPartyId, store.StoreName, store.StoreUrlName);
                model.StoreProfileHomeDescription = store.StoreProfileHomeDescription;
                model.GeneralText = store.GeneralText;
                var phones = _phoneService.GetPhonesByMainPartyId(store.MainPartyId);
                var gsm = phones.FirstOrDefault(x => x.PhoneType == (byte)PhoneTypeEnum.Gsm);
                var localPhones = phones.Where(x => x.PhoneType == (byte)PhoneTypeEnum.Phone);
                var whatsapp = phones.FirstOrDefault(x => x.PhoneType == (byte)PhoneTypeEnum.Whatsapp);

                model.Gsm = $"{gsm.PhoneCulture.Replace("+", "")}-{gsm.PhoneAreaCode}-{gsm.PhoneNumber}";
                model.StoreBanner = ImageHelper.GetStoreBanner(store.MainPartyId, store.StoreBanner);
                if (localPhones.Count() > 0)
                {
                    var localPhonesPhone = localPhones.ToArray()[0];
                    model.Phone1 = $"{localPhonesPhone.PhoneCulture.Replace("+", "")}-{localPhonesPhone.PhoneAreaCode}-{localPhonesPhone.PhoneNumber}";
                }

                if (localPhones.Count() > 1)
                {
                    var localPhonesPhone = localPhones.ToArray()[1];
                    model.Phone2 = $"{localPhonesPhone.PhoneCulture.Replace("+", "")}-{localPhonesPhone.PhoneAreaCode}-{localPhonesPhone.PhoneNumber}";
                }

                model.Whatsapp = $"{whatsapp.PhoneCulture.Replace("+", "")}-{whatsapp.PhoneAreaCode}-{whatsapp.PhoneNumber}";

                var address = _addressService.GetAddressesByMainPartyId(MainPartyId).OrderBy(x => x.AddressTypeId).FirstOrDefault();
                if (address != null)
                {
                    model.Address = address.GetFullAddress();
                }
                string encodeaddress = System.Web.HttpUtility.HtmlEncode(model.Address);
                //model.MapAddress = $"https://api.makinaturkiye.com/map/{encodeaddress}";
                processStatus.Result = model;
                processStatus.ActiveResultRowCount = 1;
                processStatus.TotolRowCount = processStatus.ActiveResultRowCount;
                processStatus.Message.Header = "Store İşlemleri";
                processStatus.Message.Text = "Başarılı";
                processStatus.Status = true;
            }
            catch (Exception Error)
            {
                processStatus.Message.Header = "Store İşlemleri";
                processStatus.Message.Text = "Başarısız";
                processStatus.Status = false;
                processStatus.Result = null;
                processStatus.Error = Error;
            }
            return Request.CreateResponse(HttpStatusCode.OK, processStatus);
        }

        public HttpResponseMessage GetStoreByStoreEmail(string storeEmail)
        {
            ProcessResult processStatus = new ProcessResult();
            try
            {
                var Result = _storeService.GetStoreByStoreEmail(storeEmail);
                Result.StoreLogo = !string.IsNullOrEmpty(Result.StoreLogo) ? "https:" + ImageHelper.GetStoreLogoParh(Result.MainPartyId, Result.StoreLogo, 300) : null;
                processStatus.Result = Result;
                processStatus.ActiveResultRowCount = 1;
                processStatus.TotolRowCount = processStatus.ActiveResultRowCount;
                processStatus.Message.Header = "Store İşlemleri";
                processStatus.Message.Text = "Başarılı";
                processStatus.Status = true;
            }
            catch (Exception Error)
            {
                processStatus.Message.Header = "Store İşlemleri";
                processStatus.Message.Text = "Başarısız";
                processStatus.Status = false;
                processStatus.Result = null;
                processStatus.Error = Error;
            }
            return Request.CreateResponse(HttpStatusCode.OK, processStatus);
        }

        public HttpResponseMessage GetStoresByMainPartyIds(List<int?> mainPartyIds)
        {
            ProcessResult processStatus = new ProcessResult();
            try
            {
                var Result = _storeService.GetStoresByMainPartyIds(mainPartyIds);
                foreach (var item in Result)
                {
                    item.StoreLogo = !string.IsNullOrEmpty(item.StoreLogo) ? "https:" + ImageHelper.GetStoreLogoParh(item.MainPartyId, item.StoreLogo, 300) : null;
                }
                processStatus.Result = Result;
                processStatus.ActiveResultRowCount = Result.Count();
                processStatus.TotolRowCount = processStatus.ActiveResultRowCount;
                processStatus.Message.Header = "Store İşlemleri";
                processStatus.Message.Text = "Başarılı";
                processStatus.Status = true;
            }
            catch (Exception Error)
            {
                processStatus.Message.Header = "Store İşlemleri";
                processStatus.Message.Text = "Başarısız";
                processStatus.Status = false;
                processStatus.Result = null;
                processStatus.Error = Error;
            }
            return Request.CreateResponse(HttpStatusCode.OK, processStatus);
        }

        public HttpResponseMessage GetSPGetStoreForCategoryByCategoryIdAndBrandId(int categoryId = 0, int brandId = 0)
        {
            ProcessResult processStatus = new ProcessResult();
            try
            {
                var Result = _storeService.GetSPGetStoreForCategoryByCategoryIdAndBrandId(categoryId, brandId);
                foreach (var item in Result)
                {
                    item.StoreLogo = !string.IsNullOrEmpty(item.StoreLogo) ? "https:" + ImageHelper.GetStoreLogoParh(item.MainPartyId, item.StoreLogo, 300) : null;
                }
                processStatus.Result = Result;
                processStatus.ActiveResultRowCount = 1;
                processStatus.TotolRowCount = processStatus.ActiveResultRowCount;
                processStatus.Message.Header = "Store İşlemleri";
                processStatus.Message.Text = "Başarılı";
                processStatus.Status = true;
            }
            catch (Exception Error)
            {
                processStatus.Message.Header = "Store İşlemleri";
                processStatus.Message.Text = "Başarısız";
                processStatus.Status = false;
                processStatus.Result = null;
                processStatus.Error = Error;
            }
            return Request.CreateResponse(HttpStatusCode.OK, processStatus);
        }

        public HttpResponseMessage GetBransList(int StoreMainPartyId)
        {
            ProcessResult processStatus = new ProcessResult();
            try
            {
                var Result = _categoryService.GetCategoriesByStoreMainPartyId(StoreMainPartyId).Where(x => x.CategoryType == (byte)CategoryType.Brand).ToList();
                processStatus.Result = Result;
                processStatus.ActiveResultRowCount = Result.Count;
                processStatus.TotolRowCount = processStatus.ActiveResultRowCount;
                processStatus.Message.Header = "Store İşlemleri";
                processStatus.Message.Text = "Başarılı";
                processStatus.Status = true;
            }
            catch (Exception Error)
            {
                processStatus.Message.Header = "Store İşlemleri";
                processStatus.Message.Text = "Başarısız";
                processStatus.Status = false;
                processStatus.Result = null;
                processStatus.Error = Error;
            }
            return Request.CreateResponse(HttpStatusCode.OK, processStatus);
        }

        public HttpResponseMessage GetModelList(int StoreMainPartyId)
        {
            ProcessResult processStatus = new ProcessResult();
            try
            {
                var Result = _categoryService.GetCategoriesByStoreMainPartyId(StoreMainPartyId).Where(x => x.CategoryType == (byte)CategoryType.Model).ToList();
                processStatus.Result = Result;
                processStatus.ActiveResultRowCount = Result.Count;
                processStatus.TotolRowCount = processStatus.ActiveResultRowCount;
                processStatus.Message.Header = "Store İşlemleri";
                processStatus.Message.Text = "Başarılı";
                processStatus.Status = true;
            }
            catch (Exception Error)
            {
                processStatus.Message.Header = "Store İşlemleri";
                processStatus.Message.Text = "Başarısız";
                processStatus.Status = false;
                processStatus.Result = null;
                processStatus.Error = Error;
            }
            return Request.CreateResponse(HttpStatusCode.OK, processStatus);
        }

        public HttpResponseMessage GetAllStores(int pageDimension, int page, StoreActiveTypeEnum? storeActiveType = StoreActiveTypeEnum.Seller)
        {
            ProcessResult processStatus = new ProcessResult();
            try
            {
                var Result = _storeNewService.GetAllStoreNews((byte)storeActiveType);
                var TotalRecord = Result.Count();
                Result = Result.OrderByDescending(x => x.UpdateDate).Skip(page * pageDimension - pageDimension).Take(pageDimension).ToList();
                foreach (var item in Result)
                {
                    item.ImageName = ImageHelper.GetStoreNewImagePath(item.ImageName, StoreNewImageSize.px300x300.ToString());
                }
                processStatus.Result = Result;
                processStatus.ActiveResultRowCount = Result.Count();
                processStatus.TotolRowCount = processStatus.ActiveResultRowCount;
                processStatus.Message.Header = "Store İşlemleri";
                processStatus.Message.Text = "Başarılı";
                processStatus.Status = true;
            }
            catch (Exception Error)
            {
                processStatus.Message.Header = "Store İşlemleri";
                processStatus.Message.Text = "Başarısız";
                processStatus.Status = false;
                processStatus.Result = null;
                processStatus.Error = Error;
            }
            return Request.CreateResponse(HttpStatusCode.OK, processStatus);
        }

        //public HttpResponseMessage GetAllStores(StoreActiveTypeEnum? storeActiveType = null)
        //{
        //    ProcessResult processStatus = new ProcessResult();
        //    try
        //    {
        //        var Result = _storeService.GetAllStores(storeActiveType);
        //        foreach (var item in Result)
        //        {
        //            item.StoreLogo = !string.IsNullOrEmpty(item.StoreLogo) ? "https:" + ImageHelper.GetStoreLogoParh(item.MainPartyId, item.StoreLogo, 300) : null;
        //        }
        //        processStatus.Result = Result;
        //        processStatus.ActiveResultRowCount = Result.Count();
        //        processStatus.TotolRowCount = processStatus.ActiveResultRowCount;
        //        processStatus.Message.Header = "Store İşlemleri";
        //        processStatus.Message.Text = "Başarılı";
        //        processStatus.Status = true;
        //    }
        //    catch (Exception Error)
        //    {
        //        processStatus.Message.Header = "Store İşlemleri";
        //        processStatus.Message.Text = "Başarısız";
        //        processStatus.Status = false;
        //        processStatus.Result = null;
        //        processStatus.Error = Error;
        //    }
        //    return Request.CreateResponse(HttpStatusCode.OK, processStatus);
        //}
        public HttpResponseMessage GetCategoryStores(int categoryId = 0, int modelId = 0, int brandId = 0,
            int cityId = 0, string searchText = "",
            int orderBy = 0, int pageIndex = 0, int pageSize = 0, string activityType = "")
        {
            {
                ProcessResult processStatus = new ProcessResult();
                try
                {
                    List<StoreListItem> Result = new List<StoreListItem>();
                    IList<int> localityIds = new List<int>();
                    var IslemResult = _storeService.GetCategoryStores(categoryId, modelId, brandId, cityId,
                    localityIds, searchText, orderBy, pageIndex, pageSize, activityType);
                    foreach (var item in IslemResult.Stores)
                    {
                        if (!item.StoreLogo.StartsWith("https:"))
                        {
                            item.StoreLogo = !string.IsNullOrEmpty(item.StoreLogo) ? "https:" + ImageHelper.GetStoreLogoParh(item.MainPartyId, item.StoreLogo, 300) : null;
                        }
                    }
                    foreach (var IslemStore in IslemResult.Stores)
                    {
                        var tmpprodustResult = _productService.GetSPProductsByStoreMainPartyId(6, 1, IslemStore.MainPartyId, 0, 0);
                        List<View.Result.ProductSearchResult> TmpStoreProductList = new List<View.Result.ProductSearchResult>();
                        foreach (var item in tmpprodustResult)
                        {
                            View.Result.ProductSearchResult TmpResult = new View.Result.ProductSearchResult
                            {
                                ProductId = item.ProductId,
                                CurrencyCodeName = item.CurrencyCodeName,
                                ProductName = item.ProductName,
                                BrandName = item.BrandName,
                                ModelName = item.CategoryName,
                                MainPicture = "",
                                StoreName = "",
                                PictureList = "".Split(',').ToList(),
                            };
                            string picturePath = "";
                            var picture = _pictureService.GetFirstPictureByProductId(TmpResult.ProductId);
                            if (picture != null)
                            {
                                if (!picture.PicturePath.StartsWith("https:"))
                                {
                                    picturePath = !string.IsNullOrEmpty(picture.PicturePath) ? "https:" + ImageHelper.GetProductImagePath(TmpResult.ProductId, picture.PicturePath, ProductImageSize.px500x375) : null;
                                }
                            }
                            TmpResult.MainPicture = (picturePath == null ? "" : picturePath);
                            TmpStoreProductList.Add(TmpResult);
                        }
                        Result.Add(new StoreListItem()
                        {
                            Store = IslemStore,
                            Products = TmpStoreProductList
                        });
                    }
                    processStatus.Result = Result;
                    processStatus.ActiveResultRowCount = Result.Count;
                    processStatus.TotolRowCount = IslemResult.TotalCount;
                    processStatus.TotolPageCount = IslemResult.TotalPages;
                    processStatus.Message.Header = "Store İşlemleri";
                    processStatus.Message.Text = "Başarılı";
                    processStatus.Status = true;
                }
                catch (Exception Error)
                {
                    processStatus.Message.Header = "Store İşlemleri";
                    processStatus.Message.Text = "Başarısız";
                    processStatus.Status = false;
                    processStatus.Result = null;
                    processStatus.Error = Error;
                }
                return Request.CreateResponse(HttpStatusCode.OK, processStatus);
            }
        }

        //public HttpResponseMessage GetStoresForCategoryBycategoryName(string categoryName)
        //    {
        //        {
        //            ProcessResult processStatus = new ProcessResult();
        //            try
        //            {
        //                byte MainCategory = (byte)MainCategoryTypeEnum.MainCategory;
        //                var Result = _categoryService.GetSPCategoryGetCategoryByCategoryName(categoryName).Select(x => new {
        //                    Text = (!string.IsNullOrEmpty(x.StorePageTitle)? x.StorePageTitle:(!string.IsNullOrEmpty(x.CategoryContentTitle) ? x.CategoryContentTitle : x.CategoryName)),
        //                    Value = x.CategoryId.ToString()
        //                }).ToList();

        //                processStatus.Result = Result;
        //                processStatus.ActiveResultRowCount = 1;
        //                processStatus.TotolRowCount = 1;
        //                processStatus.Message.Header = "Store İşlemleri";
        //                processStatus.Message.Text = "Başarılı";
        //                processStatus.Status = true;
        //            }
        //            catch (Exception Error)
        //            {
        //                processStatus.Message.Header = "Store İşlemleri";
        //                processStatus.Message.Text = "Başarısız";
        //                processStatus.Status = false;
        //                processStatus.Result = null;
        //                processStatus.Error = Error;
        //            }
        //            return Request.CreateResponse(HttpStatusCode.OK, processStatus);
        //        }
        //    }

        public HttpResponseMessage GetStoresForCategoryByCategoryId(int categoryId = 0)
        {
            {
                ProcessResult processStatus = new ProcessResult();
                try
                {
                    var Result = _storeService.GetSPStoresForCategoryByCategoryId(categoryId);
                    processStatus.Result = Result;
                    processStatus.ActiveResultRowCount = 1;
                    processStatus.TotolRowCount = 1;
                    processStatus.Message.Header = "Store İşlemleri";
                    processStatus.Message.Text = "Başarılı";
                    processStatus.Status = true;
                }
                catch (Exception Error)
                {
                    processStatus.Message.Header = "Store İşlemleri";
                    processStatus.Message.Text = "Başarısız";
                    processStatus.Status = false;
                    processStatus.Result = null;
                    processStatus.Error = Error;
                }
                return Request.CreateResponse(HttpStatusCode.OK, processStatus);
            }
        }

        public HttpResponseMessage GetStoreForVideoSearch(string searchText)
        {
            {
                ProcessResult processStatus = new ProcessResult();
                try
                {
                    var Result = _storeService.GetStoreForVideoSearch(searchText);
                    Result.StoreLogo = !string.IsNullOrEmpty(Result.StoreLogo) ? "https:" + ImageHelper.GetStoreLogoParh(Result.MainPartyId, Result.StoreLogo, 300) : null;
                    processStatus.Result = Result;
                    processStatus.ActiveResultRowCount = 1;
                    processStatus.TotolRowCount = processStatus.ActiveResultRowCount;
                    processStatus.Message.Header = "Store İşlemleri";
                    processStatus.Message.Text = "Başarılı";
                    processStatus.Status = true;
                }
                catch (Exception Error)
                {
                    processStatus.Message.Header = "Store İşlemleri";
                    processStatus.Message.Text = "Başarısız";
                    processStatus.Status = false;
                    processStatus.Result = null;
                    processStatus.Error = Error;
                }
                return Request.CreateResponse(HttpStatusCode.OK, processStatus);
            }
        }

        public HttpResponseMessage GetHomeStores(int pageSize = int.MaxValue)
        {
            {
                ProcessResult processStatus = new ProcessResult();
                try
                {
                    var Result = _storeService.GetHomeStores(pageSize);
                    foreach (var item in Result)
                    {
                        item.StoreLogo = !string.IsNullOrEmpty(item.StoreLogo) ? "https:" + ImageHelper.GetStoreLogoParh(item.MainPartyId, item.StoreLogo, 300) : null;
                    }
                    processStatus.Result = Result;
                    processStatus.ActiveResultRowCount = Result.Count();
                    processStatus.TotolRowCount = Result.Count();
                    processStatus.Message.Header = "Store İşlemleri";
                    processStatus.Message.Text = "Başarılı";
                    processStatus.Status = true;
                }
                catch (Exception Error)
                {
                    processStatus.Message.Header = "Store İşlemleri";
                    processStatus.Message.Text = "Başarısız";
                    processStatus.Status = false;
                    processStatus.Result = null;
                    processStatus.Error = Error;
                }
                return Request.CreateResponse(HttpStatusCode.OK, processStatus);
            }
        }

        public HttpResponseMessage GetStoreSearchByStoreName(string storeName)
        {
            {
                ProcessResult processStatus = new ProcessResult();
                try
                {
                    var Result = _storeService.GetStoreSearchByStoreName(storeName);
                    foreach (var item in Result)
                    {
                        item.StoreLogo = !string.IsNullOrEmpty(item.StoreLogo) ? "https:" + ImageHelper.GetStoreLogoParh(item.MainPartyId, item.StoreLogo, 300) : null;
                    }
                    processStatus.Result = Result;
                    processStatus.ActiveResultRowCount = Result.Count();
                    processStatus.TotolRowCount = processStatus.ActiveResultRowCount;
                    processStatus.Message.Header = "Store İşlemleri";
                    processStatus.Message.Text = "Başarılı";
                    processStatus.Status = true;
                }
                catch (Exception Error)
                {
                    processStatus.Message.Header = "Store İşlemleri";
                    processStatus.Message.Text = "Başarısız";
                    processStatus.Status = false;
                    processStatus.Result = null;
                    processStatus.Error = Error;
                }
                return Request.CreateResponse(HttpStatusCode.OK, processStatus);
            }
        }

        public HttpResponseMessage GetStoreByStoreNo(string storeNo)
        {
            {
                ProcessResult processStatus = new ProcessResult();
                try
                {
                    var Result = _storeService.GetStoreByStoreNo(storeNo);
                    Result.StoreLogo = !string.IsNullOrEmpty(Result.StoreLogo) ? "https:" + ImageHelper.GetStoreLogoParh(Result.MainPartyId, Result.StoreLogo, 300) : null;
                    processStatus.Result = Result;
                    processStatus.ActiveResultRowCount = 1;
                    processStatus.TotolRowCount = processStatus.ActiveResultRowCount;
                    processStatus.Message.Header = "Store İşlemleri";
                    processStatus.Message.Text = "Başarılı";
                    processStatus.Status = true;
                }
                catch (Exception Error)
                {
                    processStatus.Message.Header = "Store İşlemleri";
                    processStatus.Message.Text = "Başarısız";
                    processStatus.Status = false;
                    processStatus.Result = null;
                    processStatus.Error = Error;
                }
                return Request.CreateResponse(HttpStatusCode.OK, processStatus);
            }
        }

        public HttpResponseMessage GetStoreCertificatesByMainPartyId(int mainPartyId)
        {
            {
                ProcessResult processStatus = new ProcessResult();
                try
                {
                    var Result = _storeService.GetStoreCertificatesByMainPartyId(mainPartyId);
                    processStatus.Result = Result;
                    processStatus.ActiveResultRowCount = Result.Count();
                    processStatus.TotolRowCount = processStatus.ActiveResultRowCount;
                    processStatus.Message.Header = "Store İşlemleri";
                    processStatus.Message.Text = "Başarılı";
                    processStatus.Status = true;
                }
                catch (Exception Error)
                {
                    processStatus.Message.Header = "Store İşlemleri";
                    processStatus.Message.Text = "Başarısız";
                    processStatus.Status = false;
                    processStatus.Result = null;
                    processStatus.Error = Error;
                }
                return Request.CreateResponse(HttpStatusCode.OK, processStatus);
            }
        }

        public HttpResponseMessage GetStoreCertificateByStoreCertificateId(int mainPartyId)
        {
            {
                ProcessResult processStatus = new ProcessResult();
                try
                {
                    var Result = _storeService.GetStoreCertificatesByMainPartyId(mainPartyId);
                    processStatus.Result = Result;
                    processStatus.ActiveResultRowCount = Result.Count();
                    processStatus.TotolRowCount = processStatus.ActiveResultRowCount;
                    processStatus.Message.Header = "Store İşlemleri";
                    processStatus.Message.Text = "Başarılı";
                    processStatus.Status = true;
                }
                catch (Exception Error)
                {
                    processStatus.Message.Header = "Store İşlemleri";
                    processStatus.Message.Text = "Başarısız";
                    processStatus.Status = false;
                    processStatus.Result = null;
                    processStatus.Error = Error;
                }
                return Request.CreateResponse(HttpStatusCode.OK, processStatus);
            }
        }
    }
}