using MakinaTurkiye.Api.View;
using MakinaTurkiye.Core.Infrastructure;
using MakinaTurkiye.Services.Catalog;
using MakinaTurkiye.Services.Common;
using MakinaTurkiye.Services.Media;
using MakinaTurkiye.Services.Stores;
using MakinaTurkiye.Utilities.HttpHelpers;
using MakinaTurkiye.Utilities.ImageHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using MakinaTurkiye.Entities.Tables.Common;

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
        }

        //public StoreController(IStoreService storeService)
        //{
        //    this._storeService = storeService;
        //}

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
                model.StoreProfileHomeDescription=store.StoreProfileHomeDescription;
                model.GeneralText = store.GeneralText;
                var phones = _phoneService.GetPhonesByMainPartyId(store.MainPartyId);
                var gsm = phones.FirstOrDefault(x => x.PhoneType == (byte)PhoneTypeEnum.Gsm);
                var localPhones = phones.Where(x => x.PhoneType == (byte)PhoneTypeEnum.Phone);
                var whatsapp = phones.FirstOrDefault(x => x.PhoneType == (byte)PhoneTypeEnum.Whatsapp);

                model.Gsm = $"{gsm.PhoneCulture.Replace("+", "")}-{gsm.PhoneAreaCode}-{gsm.PhoneNumber}";
                model.StoreBanner = ImageHelper.GetStoreBanner(store.MainPartyId, store.StoreBanner);
                if (localPhones.Count()>0)
                {
                    var localPhonesPhone = localPhones.ToArray()[0];
                    model.Phone1= $"{localPhonesPhone.PhoneCulture.Replace("+", "")}-{localPhonesPhone.PhoneAreaCode}-{localPhonesPhone.PhoneNumber}";
                }

                if (localPhones.Count() > 1)
                {
                    var localPhonesPhone = localPhones.ToArray()[1];
                    model.Phone2 = $"{localPhonesPhone.PhoneCulture.Replace("+", "")}-{localPhonesPhone.PhoneAreaCode}-{localPhonesPhone.PhoneNumber}";
                }

                model.Whatsapp = $"{whatsapp.PhoneCulture.Replace("+", "")}-{whatsapp.PhoneAreaCode}-{whatsapp.PhoneNumber}";
                
                var address = _addressService.GetAddressesByMainPartyId(MainPartyId).OrderBy(x=>x.AddressTypeId).FirstOrDefault();
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
                    var Result = _storeService.GetSPGetStoreForCategoryByCategoryIdAndBrandId(categoryId,brandId);
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
            int cityId = 0,  string searchText = "",
            int orderBy = 0, int pageIndex = 0, int pageSize = 0, string activityType = "")
        {
            {
            ProcessResult processStatus = new ProcessResult();
            try
            {
                List<StoreListItem> Result=new List<StoreListItem>();
                IList<int> localityIds =new List<int>();
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
                        var tmpprodustResult = _productService.GetSPProductsByStoreMainPartyId(6,1, IslemStore.MainPartyId,0,0);
                        List<View.Result.ProductSearchResult> TmpStoreProductList=new List<View.Result.ProductSearchResult>();
                        foreach (var item in tmpprodustResult)
                        {
                            View.Result.ProductSearchResult TmpResult = new View.Result.ProductSearchResult
                            {
                                ProductId = item.ProductId,
                                CurrencyCodeName =item.CurrencyCodeName,
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
                            TmpResult.MainPicture = picturePath;
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