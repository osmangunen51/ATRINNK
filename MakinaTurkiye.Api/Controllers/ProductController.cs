using MakinaTurkiye.Api.Helpers;
using MakinaTurkiye.Api.View;
using MakinaTurkiye.Api.View.Products;
using MakinaTurkiye.Api.View.Result;
using MakinaTurkiye.Core.Infrastructure;
using MakinaTurkiye.Entities.Tables.Catalog;
using MakinaTurkiye.Entities.Tables.Common;
using MakinaTurkiye.Entities.Tables.Members;
using MakinaTurkiye.Entities.Tables.Messages;
using MakinaTurkiye.Services.Catalog;
using MakinaTurkiye.Services.Common;
using MakinaTurkiye.Services.Media;
using MakinaTurkiye.Services.Members;
using MakinaTurkiye.Services.Messages;
using MakinaTurkiye.Services.Stores;
using MakinaTurkiye.Utilities.HttpHelpers;
using MakinaTurkiye.Utilities.ImageHelpers;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Web.Http;

namespace MakinaTurkiye.Api.Controllers
{
    public class ProductController : BaseApiController
    {
        private readonly IProductService _productService;
        private readonly IAddressService _addressService;
        private readonly IPictureService _pictureService;
        private readonly IMemberStoreService _memberStoreService;
        private readonly IStoreService _storeService;
        private readonly IMemberService _memberService;
        private readonly IPhoneService _phoneService;
        private readonly IProductComplainService _productComplainService;
        private readonly IMessagesMTService _messagesMTService;
        private readonly ICategoryService _categoryService;


        public ProductController()
        {
            _productService = EngineContext.Current.Resolve<IProductService>();
            _addressService = EngineContext.Current.Resolve<IAddressService>();
            _pictureService = EngineContext.Current.Resolve<IPictureService>();
            _memberStoreService = EngineContext.Current.Resolve<IMemberStoreService>();
            _storeService = EngineContext.Current.Resolve<IStoreService>();
            _memberService = EngineContext.Current.Resolve<IMemberService>();
            _phoneService = EngineContext.Current.Resolve<IPhoneService>();
            _productComplainService = EngineContext.Current.Resolve<IProductComplainService>();
            _messagesMTService = EngineContext.Current.Resolve<IMessagesMTService>();
            _categoryService = EngineContext.Current.Resolve<ICategoryService>();
        }
        public HttpResponseMessage Get(int No)
        {
            ProcessResult ProcessStatus = new ProcessResult();
            try
            {
                var Result = _productService.GetProductByProductId(No);
                if (Result != null)
                {
                    var Currency = Result.GetCurrency();
                    View.Result.ProductSearchResult TmpResult = new View.Result.ProductSearchResult
                    {
                        ProductId = Result.ProductId,
                        CurrencyCodeName = Currency,
                        ProductName = Result?.ProductName,
                        BrandName = Result?.Brand?.CategoryName,
                        ModelName = Result?.Model?.CategoryName,
                        MainPicture = "",
                        StoreName = "",
                        PictureList = "".Split(',').ToList(),
                        MainPartyId = (int)Result.MainPartyId,
                        ProductPrice = (Result.ProductPrice.HasValue ? Result.ProductPrice.Value : 0),
                        ProductPriceType = (byte)Result.ProductPriceType,
                        ProductPriceLast = (Result.ProductPriceLast.HasValue ? Result.ProductPriceLast.Value : 0),
                        ProductPriceBegin = (Result.ProductPriceBegin.HasValue ? Result.ProductPriceBegin.Value : 0),
                        HasVideo = Result.HasVideo,
                    };

                    var Product = _productService.GetProductByProductId(TmpResult.ProductId);
                    if (Product != null)
                    {
                        TmpResult.DatePublished = default;

                        if (Product.ProductRecordDate.HasValue)
                        {
                            TmpResult.DatePublished = Product.ProductRecordDate.Value;
                        }
                    }

                    var memberStore = _memberStoreService.GetMemberStoreByMemberMainPartyId(TmpResult.MainPartyId);
                    var Store = _storeService.GetStoreByMainPartyId(memberStore.StoreMainPartyId.Value);
                    TmpResult.StoreName = Store.StoreName;
                    if (Store != null)
                    {
                        TmpResult.StoreMainPartyId = Store.MainPartyId;
                        TmpResult.Storelogo = !string.IsNullOrEmpty(Store.StoreLogo) ? "https:" + ImageHelper.GetStoreLogoPath(Store.MainPartyId, Store.StoreLogo, 300) : null;
                        var phones = _phoneService.GetPhonesByMainPartyId(Store.MainPartyId);
                        var StorePhone = phones.FirstOrDefault(x => x.PhoneType == (byte)PhoneType.Phone);
                        if (StorePhone != null)
                        {
                            TmpResult.StorePhone = StorePhone.PhoneNumber;
                            TmpResult.StoreBussinesPhone = StorePhone.PhoneNumber;
                        }
                        var StoreGsm = phones.FirstOrDefault(x => x.PhoneType == (byte)PhoneType.Gsm);
                        if (StoreGsm != null)
                        {
                            TmpResult.StoreGsm = StoreGsm.PhoneNumber;
                        }
                    }

                    string picturePath = "";
                    var picture = _pictureService.GetFirstPictureByProductId(TmpResult.ProductId);
                    if (picture != null) picturePath = !string.IsNullOrEmpty(picture.PicturePath) ? "https:" + ImageHelper.GetProductImagePath(TmpResult.ProductId, picture.PicturePath, ProductImageSize.px500x375) : null;
                    TmpResult.MainPicture = picturePath;
                    if (TmpResult.MainPicture == null)
                    {
                        TmpResult.MainPicture = "";
                    }


                    var TmpPictureList = _pictureService.GetPicturesByProductId(TmpResult.ProductId);
                    TmpResult.PictureList.Clear();
                    foreach (var item in TmpPictureList)
                    {
                        var tmpPicturePath = !string.IsNullOrEmpty(item.PicturePath) ? "https:" + ImageHelper.GetProductImagePath(TmpResult.ProductId, item.PicturePath, ProductImageSize.px500x375) : null;
                        TmpResult.PictureList.Add(tmpPicturePath);
                    }

                    TmpResult.ProductStatus = Result.GetProductStatuText();
                    TmpResult.ProductType = Result.GetProductTypeText();
                    TmpResult.Mensei = Result.GetMenseiText();
                    TmpResult.DeliveryStatus = Result.GetOrderStatusText();
                    TmpResult.SalesDetails = Result.GetProductSalesTypeText();
                    TmpResult.ShortDescription = Result.GetBriefDetailText();
                    TmpResult.CategoryName = !string.IsNullOrEmpty(Result.Category.CategoryContentTitle) ? Result.Category.CategoryContentTitle : Result.Category.CategoryName;


                    string MapCode = "";
                    string Location = "";
                    if (memberStore != null)
                    {
                        var address = _addressService.GetFisrtAddressByMainPartyId(memberStore.StoreMainPartyId.Value);
                        if (address != null)
                        {
                            MapCode = address.GetFullAddress();

                            //satici bilgileri
                            TmpResult.CityName = address.City != null ? address.City.CityName : string.Empty;
                            TmpResult.CountryName = address.Country != null ? address.Country.CountryName : string.Empty;
                            TmpResult.LocalityName = address.Locality != null ? address.Locality.LocalityName : string.Empty;
                            TmpResult.ProductContactUrl = UrlBuilder.GetProductContactUrl(Result.ProductId, Store?.StoreName, true);
                            Location = Result.GetLocation();
                        }
                        else
                        {
                            MapCode = Result.GetFullAddress();
                        }
                    }
                    else
                    {
                        MapCode = Result.GetFullAddress();
                    }

                    if (!string.IsNullOrEmpty(MapCode))
                    {
                        TmpResult.MapCode = string.Format("https://maps.google.com/maps?q={0}", MapCode);
                    }
                    TmpResult.ProductDescription = Result.ProductDescription;

                    TmpResult.Location = Location;
                    ProcessStatus.Result = TmpResult;
                    ProcessStatus.ActiveResultRowCount = 1;
                    ProcessStatus.TotolRowCount = ProcessStatus.ActiveResultRowCount;
                    ProcessStatus.Message.Header = "Product Operations";
                    ProcessStatus.Message.Text = "Success";
                    ProcessStatus.Status = true;
                }
                else
                {
                    ProcessStatus.Message.Header = "Product Operations";
                    ProcessStatus.Message.Text = "Entity Not Found";
                    ProcessStatus.Status = false;
                    ProcessStatus.Result = null;
                }
            }
            catch (Exception Error)
            {
                ProcessStatus.Message.Header = "Product Operations";
                ProcessStatus.Message.Text = "Error";
                ProcessStatus.Status = false;
                ProcessStatus.Result = null;
                ProcessStatus.Error = Error;
            }
            return Request.CreateResponse(HttpStatusCode.OK, ProcessStatus);
        }

        public HttpResponseMessage GetWithPageNo(int pageNo, bool allDetails, int pageSize = 50)
        {
            ProcessResult ProcessStatus = new ProcessResult();
            try
            {
                var Result = _productService.GetProductsWithPageNo(PageNo: pageNo, PageSize: pageSize);
                if (Result != null)
                {
                    if (allDetails)
                    {
                        ProcessStatus.Result = Result.ToList();
                    }
                    else
                    {
                        List<View.Result.ProductSearchResult> TmpResult = Result.Select(Snc =>
                                                new View.Result.ProductSearchResult
                                                {
                                                    ProductId = Snc.ProductId,
                                                    CurrencyCodeName = Snc.GetCurrency(),
                                                    ProductName = Snc.ProductName,
                                                    BrandName = Snc.Brand.CategoryName,
                                                    ModelName = Snc.Model.CategoryName,
                                                    MainPicture = "",
                                                    StoreName = "",
                                                    MainPartyId = (int)Snc.MainPartyId,
                                                    ProductPrice = (Snc.ProductPrice ?? 0),
                                                    ProductPriceType = (byte)Snc.ProductPriceType,
                                                    ProductPriceLast = (Snc.ProductPriceLast ?? 0),
                                                    ProductPriceBegin = (Snc.ProductPriceBegin ?? 0),
                                                    HasVideo = Snc.HasVideo,
                                                }
                                            ).ToList();

                        foreach (var item in TmpResult)
                        {
                            string picturePath = "";
                            var picture = _pictureService.GetFirstPictureByProductId(item.ProductId);
                            if (picture != null)
                                picturePath = !string.IsNullOrEmpty(picture.PicturePath) ? "https:" + ImageHelper.GetProductImagePath(item.ProductId, picture.PicturePath, ProductImageSize.px500x375) : null;
                            var memberStore = _memberStoreService.GetMemberStoreByMemberMainPartyId(item.MainPartyId);
                            item.MainPicture = picturePath;
                            if (item.MainPicture == null)
                            {
                                item.MainPicture = "";
                            }
                            var Product = _productService.GetProductByProductId(item.ProductId);
                            if (Product != null)
                            {
                                item.DatePublished = default;

                                if (Product.ProductRecordDate.HasValue)
                                {
                                    item.DatePublished = Product.ProductRecordDate.Value;
                                }
                            }
                            var Store = _storeService.GetStoreByMainPartyId(memberStore.StoreMainPartyId.Value);
                            if (Store != null)
                            {
                                item.StoreName = Store.StoreName;

                                item.Storelogo = !string.IsNullOrEmpty(Store.StoreLogo) ? "https:" + ImageHelper.GetStoreLogoPath(Store.MainPartyId, Store.StoreLogo, 300) : null;
                                var phones = _phoneService.GetPhonesByMainPartyId(Store.MainPartyId);
                                var StorePhone = phones.FirstOrDefault(x => x.PhoneType == (byte)PhoneType.Phone);
                                if (StorePhone != null)
                                {
                                    item.StorePhone = StorePhone.PhoneNumber;
                                    item.StoreBussinesPhone = StorePhone.PhoneNumber;
                                }
                                var StoreGsm = phones.FirstOrDefault(x => x.PhoneType == (byte)PhoneType.Gsm);
                                if (StoreGsm != null)
                                {
                                    item.StoreGsm = StoreGsm.PhoneNumber;
                                }
                            }
                        }
                        ProcessStatus.Result = TmpResult;
                    }

                    ProcessStatus.ActiveResultRowCount = Result.Count();
                    ProcessStatus.TotolRowCount = ProcessStatus.ActiveResultRowCount;
                    ProcessStatus.Message.Header = "Product Operations";
                    ProcessStatus.Message.Text = "Success";
                    ProcessStatus.Status = true;
                }
                else
                {
                    ProcessStatus.Message.Header = "Product Operations";
                    ProcessStatus.Message.Text = "Entity Not Found";
                    ProcessStatus.Status = false;
                    ProcessStatus.Result = null;
                }
            }
            catch (Exception Error)
            {
                ProcessStatus.Message.Header = "Product Operations";
                ProcessStatus.Message.Text = "Error";
                ProcessStatus.Status = false;
                ProcessStatus.Result = null;
                ProcessStatus.Error = Error;
            }
            return Request.CreateResponse(HttpStatusCode.OK, ProcessStatus);
        }

        public HttpResponseMessage GetWithCategoryId(int categoryId, bool allDetails, int pageNo, int pageSize = 50, string SearchText = "", string ordertype = "a-z", int SeriresId = 0,int ModelId = 0, int BrandId = 0, int CountryId = 0, int CityId = 0, int LocalityId = 0,decimal SelectMinPrice=0,decimal SelectMaxPrice=0)
        {
            ProcessResult ProcessStatus = new ProcessResult();
            try
            {
                double MinPrice = 0;
                double MaxPrice = 0;

                int searchTypeId = 0;
                int orderById = 0;
                switch (ordertype)
                {
                    case "a-z": orderById = 2; break;
                    case "z-a": orderById = 3; break;
                    case "soneklenen": orderById = 4; break;
                    case "fiyat-artan": orderById = 6; break;
                    default: orderById = 0; break;
                }

                CategoryProductsResult result = _productService.GetCategoryProductsPriceRange(
                    categoryId, BrandId,ModelId, SeriresId, searchTypeId, 0,
                    CountryId,CityId,LocalityId, orderById, pageNo, pageSize, 
                    SearchText,
                    SelectMinPrice, SelectMaxPrice);
                ProcessStatus.TotolRowCount = result.Products.Count();
                List<View.Result.ProductSearchResult> TmpResult = result.Products.Select(Snc =>
                    new View.Result.ProductSearchResult
                    {
                        CurrencyCodeName = Snc.CurrencyCodeName,
                        StoreMainPartyId = Snc.StoreMainPartyId.Value,
                        ProductId = Snc.ProductId,
                        ProductName = Snc.ProductName,
                        BrandName = Snc.BrandName,
                        ModelName = Snc.ModelName,
                        MainPicture = (Snc.MainPicture == null ? "" : Snc.MainPicture),
                        StoreName = Snc.StoreName,
                        ProductPrice = (Snc.ProductPrice.HasValue ? Snc.ProductPrice.Value : 0),
                        ProductPriceType = (byte)Snc.ProductPriceType,
                        ProductPriceLast = (Snc.ProductPriceLast.HasValue ? Snc.ProductPriceLast.Value : 0),
                        ProductPriceBegin = (Snc.ProductPriceBegin.HasValue ? Snc.ProductPriceBegin.Value : 0),
                        HasVideo=(bool)Snc.HasVideo
                    }
                ).ToList();
                if (TmpResult.Count>0)
                {
                    var productList = _productService.GetProductByProductsIds(TmpResult.Select(x => x.ProductId).ToList());
                    var storeList = _storeService.GetStoresByMainPartyIds(TmpResult.Select(x => x.StoreMainPartyId).ToList());
                    var storephoneList = _phoneService.GetPhonesByMainPartyIds(TmpResult.Select(x => x.StoreMainPartyId).ToList());
                    foreach (var item in TmpResult)
                    {
                        var Product = productList.FirstOrDefault(x => x.ProductId == item.ProductId);
                        if (Product != null)
                        {
                            item.DatePublished = default;

                            if (Product.ProductRecordDate.HasValue)
                            {
                                item.DatePublished = Product.ProductRecordDate.Value;
                            }
                        }
                        item.CurrencyCodeName = Product.GetCurrency();
                        var Store = storeList.FirstOrDefault(x => x.MainPartyId == item.StoreMainPartyId);
                        if (Store != null)
                        {
                            item.Storelogo = !string.IsNullOrEmpty(Store.StoreLogo) ? "https:" + ImageHelper.GetStoreLogoPath(Store.MainPartyId, Store.StoreLogo, 300) : null;
                            var phones = storephoneList.Where(x => x.MainPartyId == Store.MainPartyId);
                            var StorePhone = phones.FirstOrDefault(x => x.PhoneType == (byte)PhoneType.Phone);
                            if (StorePhone != null)
                            {
                                item.StorePhone = StorePhone.PhoneNumber;
                                item.StoreBussinesPhone = StorePhone.PhoneNumber;
                            }
                            var StoreGsm = phones.FirstOrDefault(x => x.PhoneType == (byte)PhoneType.Gsm);
                            if (StoreGsm != null)
                            {
                                item.StoreGsm = StoreGsm.PhoneNumber;
                            }
                        }
                        item.MainPicture = !string.IsNullOrEmpty(item.MainPicture) ? "https:" + ImageHelper.GetProductImagePath(item.ProductId, item.MainPicture, ProductImageSize.px500x375) : "";
                    }
                }

                List<AdvancedSearchFilterItem> filterItems = new List<AdvancedSearchFilterItem>();

                var categories =_categoryService.GetCategoriesByCategoryIds(result.FilterableCategoryIds.Select(x => x.CategoryId).ToList());

                if (result.FilterableSeriesIds != null && result.FilterableSeriesIds.Count > 0)
                {
                    var filterableSeries = _categoryService.GetCategoriesByCategoryIds(result.FilterableSeriesIds.Distinct().ToList());
                    if (filterableSeries.Count > 0)
                    {
                        foreach (var item in filterableSeries)
                        {
                            filterItems.Add(new AdvancedSearchFilterItem
                            {
                                Value = item.CategoryId,
                                Name = item.CategoryName,
                                Type = (byte)AdvancedSearchFilterType.Serie,
                                ProductCount = result.FilterableSeriesIds.Count(c => c == item.CategoryId),
                                ProductCountAll =0,
                            });
                        }
                    }
                }

                if (result.FilterableModelIds != null && result.FilterableModelIds.Count > 0)
                {
                    var filterableModels = _categoryService.GetCategoriesByCategoryIds(result.FilterableModelIds.Distinct().ToList());
                    if (filterableModels.Count > 0)
                    {

                        foreach (var item in filterableModels)
                        {
                            filterItems.Add(new AdvancedSearchFilterItem
                            {
                                Value = item.CategoryId,
                                Name = item.CategoryName,
                                Type = (byte)AdvancedSearchFilterType.Model,
                                ProductCount = result.FilterableModelIds.Count(c => c == item.CategoryId),
                                ProductCountAll = 0,
                            });
                        }

                    }

                }


                if (result.FilterableBrandIds != null && result.FilterableBrandIds.Count > 0)
                {
                    List<int> catIdList = result.FilterableBrandIds.Distinct().ToList();
                    string aa = string.Join(",", catIdList.ToArray());
                    var filterableBrands = _categoryService.GetCategoriesByCategoryIds(catIdList);
                    if (filterableBrands.Count > 0)
                    {
                        var distinctfilterableBrands = filterableBrands.Select(b => b.CategoryName).Distinct();
                        if (distinctfilterableBrands.Count() == filterableBrands.Count)
                        {
                            foreach (var item in filterableBrands)
                            {
                                filterItems.Add(new AdvancedSearchFilterItem
                                {
                                    Value = item.CategoryId,
                                    Name = item.CategoryName,
                                    Type = (byte)AdvancedSearchFilterType.Brand,
                                    ProductCount = result.FilterableBrandIds.Count(c => c== item.CategoryId),
                                    ProductCountAll = 0,

                                });
                            }
                        }
                        else
                        {
                            foreach (var item in distinctfilterableBrands)
                            {
                                var brands = filterableBrands.Where(b => b.CategoryName == item);
                                if (brands.Count() == 1)
                                {
                                    filterItems.Add(new AdvancedSearchFilterItem
                                    {
                                        Value = brands.First().CategoryId,
                                        Name = brands.First().CategoryName,
                                        Type = (byte)AdvancedSearchFilterType.Brand,
                                        ProductCount = result.FilterableBrandIds.Count(c => c== brands.FirstOrDefault().CategoryId),
                                        ProductCountAll = 0,
                                    });
                                }
                                else
                                {
                                    filterItems.Add(new AdvancedSearchFilterItem
                                    {
                                        Value = brands.First().CategoryId,
                                        Name = brands.First().CategoryName,
                                        Type = (byte)AdvancedSearchFilterType.Brand,
                                        ProductCount = result.FilterableBrandIds.Count(c => brands.Select(x=>x.CategoryId).Contains(c)),
                                        ProductCountAll = 0,
                                    });
                                }
                            }
                        }
                    }
                }

                if (result.FilterableCountryIds != null && result.FilterableCountryIds.Count > 0)
                {
                    var filterableresult = _addressService.GetCountriesByCountryIds(result.FilterableCountryIds.Distinct().ToList());
                    if (filterableresult.Count > 0)
                    {
                        foreach (var item in filterableresult)
                        {
                            filterItems.Add(new AdvancedSearchFilterItem
                            {
                                Value = item.CountryId,
                                Name = item.CountryName,
                                Type = (byte)AdvancedSearchFilterType.Country,
                                ProductCount = result.FilterableCountryIds.Count(x => x == item.CountryId),
                                ProductCountAll =0,
                                Level=0
                            });
                        }
                    }
                }

                if (result.FilterableCityIds != null && result.FilterableCityIds.Count > 0)
                {
                    var filterableCities = _addressService.GetCitiesByCityIds(result.FilterableCityIds.Distinct().ToList());
                    if (filterableCities.Count > 0)
                    {
                        foreach (var item in filterableCities)
                        {
                            filterItems.Add(new AdvancedSearchFilterItem
                            {
                                Value = item.CityId,
                                Name = item.CityName,
                                Type = (byte)AdvancedSearchFilterType.City,
                                ProductCount = result.FilterableCityIds.Count(x => x == item.CityId),
                                ProductCountAll = 0,
                                Level = 1,
                            });
                        }
                    }
                }

                if (CityId > 0 | LocalityId>0)
                {
                    if (result.FilterableLocalityIds != null && result.FilterableLocalityIds.Count > 0)
                    {
                        var filterableLocalities = _addressService.GetLocalitiesByLocalityIds(result.FilterableLocalityIds.Distinct().ToList());
                        if (filterableLocalities.Count > 0)
                        {
                            foreach (var item in filterableLocalities)
                            {
                                filterItems.Add(new AdvancedSearchFilterItem
                                {
                                    Value = item.LocalityId,
                                    Name = item.LocalityName,
                                    Type = (byte)AdvancedSearchFilterType.Locality,
                                    ProductCount = result.FilterableLocalityIds.Count(x => x == item.LocalityId),
                                    ProductCountAll = 0,
                                    Level = 2,
                                });
                            }
                        }
                    }
                }

                int Ordr = 0;
                Dictionary<Category, int> lst = new Dictionary<Category, int>();
                var tmpCategory = categories.FirstOrDefault();
                while (tmpCategory != null)
                {
                    tmpCategory = _categoryService.GetCategoryByCategoryId((int)tmpCategory.CategoryParentId);
                    if (tmpCategory != null)
                    {
                        if (!lst.ContainsKey(tmpCategory))
                        {
                            lst.Add(tmpCategory, Ordr);
                            Ordr++;
                        }
                    }
                    if (tmpCategory.CategoryParentId == null | tmpCategory.CategoryParentId == categoryId)
                    {
                        break;
                    }
                }

                Ordr = 0;
                var lstCategory = lst.OrderByDescending(x => x.Value).Select(x => x.Key).ToList();
                var tmplstCategory = _categoryService.GetCategoriesByCategoryIds(lstCategory.Select(x => x.CategoryId).ToList());
                tmplstCategory = tmplstCategory.OrderBy(x => x.CategoryOrder).ToList();
                foreach (var item in tmplstCategory)
                {
                    filterItems.Add(new AdvancedSearchFilterItem
                    {
                        Value = item.CategoryId,
                        Name = item.CategoryName,
                        Type = (byte)AdvancedSearchFilterType.Category,
                        ProductCount = item.ProductCount,
                        ProductCountAll = item.ProductCountAll,
                        Level = Ordr
                    });
                    Ordr++;
                }

                // categoride sadece sektör ürün gurubu ve kategori olanla rlistelenecek.

                List<byte> AllowedCategoryListesi = new List<byte>() { 0, 1, 6 };

                categories = categories.Where(x => AllowedCategoryListesi.Contains((byte)x.CategoryType)).ToList();
                foreach (var item in categories)
                {
                    filterItems.Add(new AdvancedSearchFilterItem
                    {
                        Value = item.CategoryId,
                        Name = item.CategoryName,
                        Type = (byte)AdvancedSearchFilterType.Category,
                        ProductCount = item.ProductCount,
                        ProductCountAll = item.ProductCountAll,
                        Level = Ordr
                    });
                }

                ProcessStatus.Result = new {
                    Product = TmpResult,
                    Filters = filterItems,
                    MinPrice=result.MinPrice,
                    MaxPrice = result.MaxPrice
                };

                ProcessStatus.ActiveResultRowCount = TmpResult.Count();
                ProcessStatus.TotolRowCount = result.TotalCount;
                ProcessStatus.TotolPageCount = result.TotalPages;
                ProcessStatus.Message.Header = "Product İşlemleri";
                ProcessStatus.Message.Text = "Başarılı";
                ProcessStatus.Status = true;
            }
            catch (Exception Error)
            {
                ProcessStatus.Message.Header = "Product İşlemleri";
                ProcessStatus.Message.Text = "Başarısız";
                ProcessStatus.Status = false;
                ProcessStatus.Result = null;
                ProcessStatus.Error = Error;
            }
            return Request.CreateResponse(HttpStatusCode.OK, ProcessStatus);
        }

        public HttpResponseMessage GetAll(bool allDetails, int pageNo, int pageSize = 50)
        {
            ProcessResult ProcessStatus = new ProcessResult();
            try
            {
                var Result = _productService.GetProductsAll();
                if (allDetails)
                {
                    ProcessStatus.Result = Result;
                }
                else
                {
                    List<MakinaTurkiye.Api.View.Result.ProductSearchResult> TmpResult = Result.Select(Snc =>
                                            new MakinaTurkiye.Api.View.Result.ProductSearchResult
                                            {
                                                ProductId = Snc.ProductId,
                                                CurrencyCodeName = Snc.GetCurrency(),
                                                ProductName = Snc.ProductName,
                                                BrandName = Snc.Brand.CategoryName,
                                                ModelName = Snc.Model.CategoryName,
                                                MainPicture = "",
                                                StoreName ="",
                                                MainPartyId = (int)Snc.MainPartyId,
                                                ProductPrice = (Snc.ProductPrice.HasValue ? Snc.ProductPrice.Value : 0),
                                                ProductPriceType = (byte)Snc.ProductPriceType,
                                                ProductPriceLast = (Snc.ProductPriceLast.HasValue ? Snc.ProductPriceLast.Value : 0),
                                                ProductPriceBegin = (Snc.ProductPriceBegin.HasValue ? Snc.ProductPriceBegin.Value : 0),
                                                HasVideo=Snc.HasVideo,
                                            }
                                        ).ToList();

                    foreach (var item in TmpResult)
                    {
                        string picturePath = "";
                        var picture = _pictureService.GetFirstPictureByProductId(item.ProductId);
                        if (picture != null)
                            picturePath = !string.IsNullOrEmpty(picture.PicturePath) ? "https:" + ImageHelper.GetProductImagePath(item.ProductId, picture.PicturePath, ProductImageSize.px500x375) : null;
                        var memberStore = _memberStoreService.GetMemberStoreByMemberMainPartyId(item.MainPartyId);
                        var store = _storeService.GetStoreByMainPartyId(memberStore.StoreMainPartyId.Value);
                        item.MainPicture = (picturePath == null ? "" : picturePath);
                        item.StoreName = store.StoreName;
                    }

                    ProcessStatus.Result = TmpResult;
                }

                ProcessStatus.TotolRowCount = Result.Count();
                if (ProcessStatus.TotolRowCount < pageSize)
                {
                    pageNo = 0;
                }
                ProcessStatus.Result = Result.Skip(pageSize * pageNo).Take(pageSize).ToList();

                ProcessStatus.ActiveResultRowCount = Result.Count();


                ProcessStatus.Message.Header = "Product İşlemleri";
                ProcessStatus.Message.Text = "Başarılı";
                ProcessStatus.Status = true;
            }
            catch (Exception Error)
            {
                ProcessStatus.Message.Header = "Product İşlemleri";
                ProcessStatus.Message.Text = "Başarısız";
                ProcessStatus.Status = false;
                ProcessStatus.Result = null;
                ProcessStatus.Error = Error;
            }
            return Request.CreateResponse(HttpStatusCode.OK, ProcessStatus);
        }

        public HttpResponseMessage GetStoreMainPartyId(int storeMainPartyId, string SearchText = "")
        {
            ProcessResult ProcessStatus = new ProcessResult();
            try
            {
                var memberStore1 = _memberStoreService.GetMemberStoreByStoreMainPartyId(storeMainPartyId);
                var Result = _productService.GetProductsByMainPartyId(Convert.ToInt32(memberStore1.MemberMainPartyId));
                if (!string.IsNullOrEmpty(SearchText))
                {
                    Result = Result.Where(x => x.ProductName.ToLower().Contains(SearchText.ToLower())).ToList();
                }

                List<View.Result.ProductSearchResult> TmpResult = Result.Select(Snc =>
                                                new View.Result.ProductSearchResult
                                                {
                                                    ProductId = Snc.ProductId,
                                                    CurrencyCodeName = Snc.GetCurrency(),
                                                    ProductName = Snc.ProductName,
                                                    BrandName = (Snc.Brand == null ? "" : Snc.Brand.CategoryName),
                                                    ModelName = (Snc.Model == null ? "" : Snc.Model.CategoryName),
                                                    MainPicture = "",
                                                    StoreName = "",
                                                    MainPartyId = (int)Snc.MainPartyId,
                                                    ProductPrice = (Snc.ProductPrice ?? 0),
                                                    ProductPriceType = (byte)Snc.ProductPriceType,
                                                    ProductPriceLast = (Snc.ProductPriceLast ?? 0),
                                                    ProductPriceBegin = (Snc.ProductPriceBegin ?? 0),
                                                    HasVideo = Snc.HasVideo
                                                }
                                            ).ToList();

                foreach (var item in TmpResult)
                {
                    string picturePath = "";
                    var picture = _pictureService.GetFirstPictureByProductId(item.ProductId);
                    if (picture != null)
                        picturePath = !string.IsNullOrEmpty(picture.PicturePath) ? "https:" + ImageHelper.GetProductImagePath(item.ProductId, picture.PicturePath, ProductImageSize.px500x375) : null;
                    var memberStore = _memberStoreService.GetMemberStoreByMemberMainPartyId(item.MainPartyId);
                    item.MainPicture = (picturePath == null ? "" : picturePath);
                    var Product = _productService.GetProductByProductId(item.ProductId);
                    if (Product != null)
                    {
                        item.DatePublished = default;

                        if (Product.ProductRecordDate.HasValue)
                        {
                            item.DatePublished = Product.ProductRecordDate.Value;
                        }
                    }
                    var Store = _storeService.GetStoreByMainPartyId(memberStore.StoreMainPartyId.Value);
                    if (Store != null)
                    {
                        item.StoreName = Store.StoreName;

                        item.Storelogo = !string.IsNullOrEmpty(Store.StoreLogo) ? "https:" + ImageHelper.GetStoreLogoPath(Store.MainPartyId, Store.StoreLogo, 300) : null;
                        var phones = _phoneService.GetPhonesByMainPartyId(Store.MainPartyId);
                        var StorePhone = phones.FirstOrDefault(x => x.PhoneType == (byte)PhoneType.Phone);
                        if (StorePhone != null)
                        {
                            item.StorePhone = StorePhone.PhoneNumber;
                            item.StoreBussinesPhone = StorePhone.PhoneNumber;
                        }
                        var StoreGsm = phones.FirstOrDefault(x => x.PhoneType == (byte)PhoneType.Gsm);
                        if (StoreGsm != null)
                        {
                            item.StoreGsm = StoreGsm.PhoneNumber;
                        }
                    }
                }
                ProcessStatus.Result = TmpResult;
                ProcessStatus.ActiveResultRowCount = TmpResult.Count();
                ProcessStatus.Message.Header = "Product İşlemleri";
                ProcessStatus.Message.Text = "Başarılı";
                ProcessStatus.Status = true;
            }
            catch (Exception Error)
            {
                ProcessStatus.Message.Header = "Product İşlemleri";
                ProcessStatus.Message.Text = "Başarısız";
                ProcessStatus.Status = false;
                ProcessStatus.Result = null;
                ProcessStatus.Error = Error;
            }
            return Request.CreateResponse(HttpStatusCode.OK, ProcessStatus);
        }

        public HttpResponseMessage Search([FromBody] SearchInput Model)
        {
            ProcessResult ProcessStatus = new ProcessResult();
            try
            {
                int TotalRowCount = 0;
                var Result = _productService.Search(out TotalRowCount, Model.name, Model.companyName, Model.country, Model.town, Model.isnew, Model.isold, Model.sortByViews, Model.sortByDate, Model.minPrice, Model.maxPrice, Model.Page, Model.PageSize);

                List<MakinaTurkiye.Api.View.Result.ProductSearchResult> TmpResult = Result.Select(Snc =>
                        new MakinaTurkiye.Api.View.Result.ProductSearchResult
                        {
                            ProductId = Snc.ProductId,
                            CurrencyCodeName = Snc.GetCurrency(),
                            ProductName = Snc.ProductName,
                            BrandName = Snc.Brand.CategoryName,
                            ModelName = Snc.Model.CategoryName,
                            MainPicture = "",
                            StoreName = "",
                            MainPartyId = (int)Snc.MainPartyId,
                            ProductPrice = (Snc.ProductPrice.HasValue ? Snc.ProductPrice.Value : 0),
                            ProductPriceType = (byte)Snc.ProductPriceType,
                            ProductPriceLast = (Snc.ProductPriceLast.HasValue ? Snc.ProductPriceLast.Value : 0),
                            ProductPriceBegin = (Snc.ProductPriceBegin.HasValue ? Snc.ProductPriceBegin.Value : 0),
                            HasVideo=Snc.HasVideo
                        }
                    ).ToList();

                foreach (var item in TmpResult)
                {
                    string picturePath = "";
                    var picture = _pictureService.GetFirstPictureByProductId(item.ProductId);
                    if (picture != null)
                        picturePath = !string.IsNullOrEmpty(picture.PicturePath) ? "https:" + ImageHelper.GetProductImagePath(item.ProductId, picture.PicturePath, ProductImageSize.px500x375) : null;
                    var memberStore = _memberStoreService.GetMemberStoreByMemberMainPartyId(item.MainPartyId);
                    var store = _storeService.GetStoreByMainPartyId(memberStore.StoreMainPartyId.Value);
                    item.MainPicture = (picturePath == null ? "" : picturePath);
                    item.StoreName = store.StoreName;
                }

                ProcessStatus.Result = TmpResult;

                ProcessStatus.Result = Result;
                ProcessStatus.ActiveResultRowCount = Result.Count();
                ProcessStatus.TotolRowCount = TotalRowCount;
                ProcessStatus.Message.Header = "Product İşlemleri";
                ProcessStatus.Message.Text = "Başarılı";
                ProcessStatus.Status = true;
            }
            catch (Exception Error)
            {
                ProcessStatus.Message.Header = "Product İşlemleri";
                ProcessStatus.Message.Text = "Başarısız";
                ProcessStatus.Status = false;
                ProcessStatus.Result = null;
                ProcessStatus.Error = Error;
            }
            return Request.CreateResponse(HttpStatusCode.OK, ProcessStatus);
        }

        public HttpResponseMessage ProductComplainAdd(MTProductComplainModel model)
        {
            ProcessResult processStatus = new ProcessResult();

            try
            {
                var LoginUserEmail = Request.CheckLoginUserClaims().LoginMemberEmail;

                var LoginMember = !string.IsNullOrEmpty(LoginUserEmail) ? _memberService.GetMemberByMemberEmail(LoginUserEmail) : null;

                if (LoginMember != null)
                {
                    var member = new Member();
                    int mainPartyId = 0;

                    //üye ekleme
                    if (_memberService.GetMemberByMemberEmail(model.UserEmail) == null)
                    {
                        var mainParty = new MainParty
                        {
                            Active = false,
                            MainPartyType = (byte)MainPartyType.Firm,
                            MainPartyRecordDate = DateTime.Now,
                            MainPartyFullName = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(model.UserName.ToLower() + " " + model.UserSurname.ToLower())
                        };

                        _memberService.InsertMainParty(mainParty);
                        mainPartyId = mainParty.MainPartyId;

                        Member member1 = new Member();
                        member1.MainPartyId = mainParty.MainPartyId;
                        member1.MemberName = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(model.UserName.ToLower());
                        member1.MemberSurname = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(model.UserSurname.ToLower());
                        member1.MemberEmail = model.UserEmail;
                        member1.MemberType = (byte)MemberType.FastIndividual;
                        member1.FastMemberShipType = (byte)FastMembershipType.ProductComplain;

                        _memberService.InsertMember(member);

                        if (model.UserPhone != "")
                        {
                            var phone = new Phone();
                            if (model.UserPhone.Substring(0, 1) == "0")
                            {
                                model.UserPhone = model.UserPhone.Substring(1, model.UserPhone.Length);
                            }

                            phone.PhoneCulture = "+90";
                            phone.MainPartyId = mainParty.MainPartyId;
                            phone.PhoneAreaCode = model.UserPhone.Substring(0, 3);
                            phone.PhoneNumber = model.UserPhone.Substring(3, 7);
                            phone.PhoneType = (byte)PhoneType.Gsm;
                            phone.GsmType = 0;

                            //_memberService.AddPhoneForMember(phone);
                            _phoneService.InsertPhone(phone);
                        }

                        var memberLast = _memberService.GetMemberByMainPartyId(mainParty.MainPartyId);
                        string memberNo = "MT";
                        for (int i = 0; i < 7 - memberLast.MainPartyId.ToString().Length; i++)
                        {
                            memberNo = memberNo + "0";
                        }
                        memberNo = memberNo + memberLast.MainPartyId;
                        memberLast.MemberNo = memberNo;
                        _memberService.UpdateMember(memberLast);
                    }
                    else
                    {
                        member = _memberService.GetMemberByMemberEmail(model.UserEmail);
                        mainPartyId = member.MainPartyId;
                    }

                    var productComplain = new ProductComplain();
                    if (LoginMember.MainPartyId > 0)
                    {
                        productComplain.UserName = LoginMember.MemberName;
                        productComplain.UserSurname = LoginMember.MemberSurname;
                        productComplain.MemberMainPartyId = mainPartyId;
                        productComplain.UserEmail = LoginMember.MemberEmail;
                        productComplain.IsMember = true;
                    }
                    else
                    {
                        productComplain.UserSurname = model.UserSurname;
                        productComplain.UserEmail = model.UserEmail;
                        productComplain.UserName = model.UserName;
                        productComplain.MemberMainPartyId = mainPartyId;
                        productComplain.IsMember = false;
                    }

                    productComplain.UserComment = model.UserComment;
                    productComplain.ProductId = model.ProductId;
                    productComplain.UserPhone = model.UserPhone;
                    productComplain.CreatedDate = DateTime.Now;
                    foreach (var item in model.ProductComplainTypeList)
                    {
                        ProductComplainDetail detail = new ProductComplainDetail();
                        detail.ProductComplainId = productComplain.ProductComplainId;
                        detail.ProductComplainTypeId = item.ProductComplainTypeId;
                        productComplain.ProductComplainDetails.Add(detail);
                    }
                    _productComplainService.InsertProductComplain(productComplain);

                    #region bilgimakina

                    string complainNames = string.Empty;
                    var insertedProductComplain = _productComplainService.GetProductComplainByProductComplainId(productComplain.ProductComplainId);
                    foreach (var item in insertedProductComplain.ProductComplainDetails)
                    {
                        complainNames += string.Format("{0} ,", item.ProductComplainType.Name);
                    }
                    complainNames = complainNames.Substring(0, complainNames.Length - 2);

                    MailMessage mailb = new MailMessage();

                    MessagesMT mailTmpInf = _messagesMTService.GetMessagesMTByMessageMTName("bilgimakinasayfası");

                    mailb.From = new MailAddress(mailTmpInf.Mail, "Bilgi Makina");
                    mailb.To.Add("bilgi@makinaturkiye.com");
                    mailb.Subject = "Ürün Şikayeti " + model.ProductName;
                    string bilgimakinaicin = "<table><tr><td>Kullanıcı Adı</td><td>" + model.UserName + " " + model.UserSurname + "</td></tr><tr><td>Email</td><td>" + model.UserEmail + "</td></tr><tr><td>Şikatet Tipi</td><td>" + complainNames + "</td></tr></table>";
                    mailb.Body = bilgimakinaicin;
                    mailb.IsBodyHtml = true;
                    mailb.Priority = MailPriority.Normal;
                    this.SendMail(mailb);

                    #endregion bilgimakina

                    processStatus.Result = "Ürün başarıyla Şikayet edilmiştir.";
                    processStatus.Message.Header = "Product Complain Add";
                    processStatus.Message.Text = "Başarılı";
                    processStatus.Status = true;
                }
                else
                {
                    processStatus.Result = "Oturum açmadan bu işlemi yapamazsınız";
                    processStatus.Message.Header = "Product Complain Add";
                    processStatus.Message.Text = "Başarısız";
                    processStatus.Status = false;
                }
            }
            catch (Exception ex)
            {
                processStatus.Message.Header = "Product Complain Add";
                processStatus.Message.Text = "İşlem başarısız.";
                processStatus.Status = false;
                processStatus.Result = "Hata ile karşılaşıldı!";
                processStatus.Error = ex;
            }
            return Request.CreateResponse(HttpStatusCode.OK, processStatus);
        }

        public HttpResponseMessage GetAllProductComplainTypeList()
        {
            ProcessResult processStatus = new ProcessResult();

            try
            {
                var LoginUserEmail = Request.CheckLoginUserClaims().LoginMemberEmail;

                var LoginMember = !string.IsNullOrEmpty(LoginUserEmail) ? _memberService.GetMemberByMemberEmail(LoginUserEmail) : null;

                if (LoginMember != null)
                {
                    var productComplaintTypesView = new List<ProductComplainTypeView>();
                    var productComplaintTypes = _productComplainService.GetAllProductComplainType();
                    foreach (var productComplaintType in productComplaintTypes)
                    {
                        var prodCompType = new ProductComplainTypeView()
                        {
                            Name = productComplaintType.Name,
                            ProductComplainTypeId = productComplaintType.ProductComplainTypeId,
                        };
                        productComplaintTypesView.Add(prodCompType);
                    }

                    processStatus.Result = productComplaintTypesView;
                    processStatus.Message.Header = "GetAllProductComplaintType";
                    processStatus.Message.Text = "Başarılı";
                    processStatus.Status = true;
                }
                else
                {
                    processStatus.Result = "Oturum açmadan bu işlemi yapamazsınız";
                    processStatus.Message.Header = "GetAllProductComplaintType";
                    processStatus.Message.Text = "Başarısız";
                    processStatus.Status = false;
                }
            }
            catch (Exception ex)
            {
                processStatus.Message.Header = "GetAllProductComplaintType";
                processStatus.Message.Text = "İşlem başarısız.";
                processStatus.Status = false;
                processStatus.Result = "Hata ile karşılaşıldı!";
                processStatus.Error = ex;
            }
            return Request.CreateResponse(HttpStatusCode.OK, processStatus);
        }

        public HttpResponseMessage NewAddProducts()
        {
            ProcessResult processStatus = new ProcessResult();
            try
            {
                List<View.Result.ProductSearchResult> TmpResult = new List<View.Result.ProductSearchResult>();
                var popularProducts = _productService.GetSPNewProducts();
                List<int> Liste = popularProducts.Select(x => (int)x.CategoryId).ToList();
                var CategoryList = _categoryService.GetCategoriesByCategoryIds(Liste).ToList();
                foreach (var Snc in popularProducts)
                {
                    var Result = _productService.GetProductByProductId(Snc.ProductId);
                    var tmp = new View.Result.ProductSearchResult
                    {
                        ProductId = Result.ProductId,
                        CurrencyCodeName = Result.GetCurrency(),
                        ProductName = Result.ProductName,
                        BrandName = (Result.Brand == null ? "" : Result.Brand.CategoryName),
                        ModelName = (Result.Model == null ? "" : Result.Model.CategoryName),
                        MainPicture = "",
                        StoreName = "",
                        MainPartyId = (int)Result.MainPartyId,
                        ProductPrice = (Result.ProductPrice ?? 0),
                        ProductPriceType = (byte)Result.ProductPriceType,
                        ProductPriceLast = (Result.ProductPriceLast ?? 0),
                        ProductPriceBegin = (Result.ProductPriceBegin ?? 0),
                        HasVideo = Result.HasVideo,
                    };
                    TmpResult.Add(tmp);
                }
                foreach (var item in TmpResult)
                {
                    string picturePath = "";
                    var picture = _pictureService.GetFirstPictureByProductId(item.ProductId);
                    if (picture != null)
                        picturePath = !string.IsNullOrEmpty(picture.PicturePath) ? "https:" + ImageHelper.GetProductImagePath(item.ProductId, picture.PicturePath, ProductImageSize.px500x375) : null;
                    var memberStore = _memberStoreService.GetMemberStoreByMemberMainPartyId(item.MainPartyId);
                    var store = _storeService.GetStoreByMainPartyId(memberStore.StoreMainPartyId.Value);
                    item.MainPicture = (picturePath == null ? "" : picturePath);
                    item.StoreName = store.StoreName;
                }
                processStatus.Result = TmpResult;
                processStatus.Message.Header = "NewAddProducts";
                processStatus.Message.Text = "Başarılı";
                processStatus.Status = true;
            }
            catch (Exception ex)
            {
                processStatus.Message.Header = "GetAllProductComplaintType";
                processStatus.Message.Text = "İşlem başarısız.";
                processStatus.Status = false;
                processStatus.Result = "Hata ile karşılaşıldı!";
                processStatus.Error = ex;
            }
            return Request.CreateResponse(HttpStatusCode.OK, processStatus);
        }


        public HttpResponseMessage Showcase()
        {
            ProcessResult processStatus = new ProcessResult();
            try
            {
                List<View.Result.ProductSearchResult> TmpResult = new List<View.Result.ProductSearchResult>();
                var popularProducts = _productService.GetProductsByShowCase();
                List<int> Liste = popularProducts.Select(x => (int)x.CategoryId).ToList();
                var CategoryList = _categoryService.GetCategoriesByCategoryIds(Liste).ToList();
                foreach (var Snc in popularProducts)
                {
                    var Result = _productService.GetProductByProductId(Snc.ProductId);
                    var tmp = new View.Result.ProductSearchResult
                    {
                        ProductId = Result.ProductId,
                        CurrencyCodeName = Result.GetCurrency(),
                        ProductName = Result.ProductName,
                        BrandName = (Result.Brand == null ? "" : Result.Brand.CategoryName),
                        ModelName = (Result.Model == null ? "" : Result.Model.CategoryName),
                        MainPicture = "",
                        StoreName = "",
                        MainPartyId = (int)Result.MainPartyId,
                        ProductPrice = (Result.ProductPrice ?? 0),
                        ProductPriceType = (byte)Result.ProductPriceType,
                        ProductPriceLast = (Result.ProductPriceLast ?? 0),
                        ProductPriceBegin = (Result.ProductPriceBegin ?? 0),
                        HasVideo=Result.HasVideo
                    };
                    TmpResult.Add(tmp);
                }
                foreach (var item in TmpResult)
                {
                    string picturePath = "";
                    var picture = _pictureService.GetFirstPictureByProductId(item.ProductId);
                    if (picture != null)
                        picturePath = !string.IsNullOrEmpty(picture.PicturePath) ? "https:" + ImageHelper.GetProductImagePath(item.ProductId, picture.PicturePath, ProductImageSize.px500x375) : null;
                    var memberStore = _memberStoreService.GetMemberStoreByMemberMainPartyId(item.MainPartyId);
                    if (memberStore!=null)
                    {
                        if (memberStore.StoreMainPartyId!=null)
                        {
                            var store = _storeService.GetStoreByMainPartyId(memberStore.StoreMainPartyId.Value);
                            item.MainPicture = (picturePath == null ? "" : picturePath);
                            item.StoreName = store.StoreName;
                        }
                    }
                }
                processStatus.Result = TmpResult;
                processStatus.Message.Header = "Showcase";
                processStatus.Message.Text = "Başarılı";
                processStatus.Status = true;
            }
            catch (Exception ex)
            {
                processStatus.Message.Header = "Showcase";
                processStatus.Message.Text = "İşlem başarısız.";
                processStatus.Status = false;
                processStatus.Result = "Hata ile karşılaşıldı!";
                processStatus.Error = ex;
            }
            return Request.CreateResponse(HttpStatusCode.OK, processStatus);
        }
    }
}