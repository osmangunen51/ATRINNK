using MakinaTurkiye.Core;
using MakinaTurkiye.Entities.StoredProcedures.Catalog;
using MakinaTurkiye.Entities.StoredProcedures.Stores;
using MakinaTurkiye.Entities.Tables.Catalog;
using MakinaTurkiye.Services.Catalog;
using MakinaTurkiye.Services.Common;
using MakinaTurkiye.Services.Media;
using MakinaTurkiye.Services.Members;
using MakinaTurkiye.Services.Search;
using MakinaTurkiye.Services.Seos;
using MakinaTurkiye.Services.Stores;
using MakinaTurkiye.Services.Videos;
using MakinaTurkiye.Utilities.FormatHelpers;
using MakinaTurkiye.Utilities.HttpHelpers;
using MakinaTurkiye.Utilities.ImageHelpers;
using MakinaTurkiye.Utilities.Mvc;

using NeoSistem.MakinaTurkiye.Web.Helpers;
using NeoSistem.MakinaTurkiye.Web.Models;
using NeoSistem.MakinaTurkiye.Web.Models.Authentication;
using NeoSistem.MakinaTurkiye.Web.Models.Catalog;
using NeoSistem.MakinaTurkiye.Web.Models.UtilityModel;
using NeoSistem.MakinaTurkiye.Web.Models.Videos;

using Schema.NET;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace NeoSistem.MakinaTurkiye.Web.Controllers
{
    [AllowAnonymous]
    public class CategoryController : BaseController
    {

        #region Constants

        private const string PAGE_INDEX_QUERY_STRING_KEY = "page";
        private const string SEARCH_TEXT_QUERY_STRING_KEY = "SearchText";
        private const string COUNTRY_QUERY_STRING_KEY = "ulke";
        private const string CITY_QUERY_STRING_KEY = "sehir";
        private const string LOCALITY_QUERY_STRING_KEY = "ilce";
        private const string CATEGORY_ID_QUERY_STRING_KEY = "CategoryId";
        private const string ORDER_BY_QUERY_STRING_KEY = "orderBy";
        private const string CUSTOM_FILTER_QUERY_STRING_KEY = "customFilter";
        private const string ORDER_QUERY_STRING_KEY = "Order";

        #endregion

        #region Fields

        ICategoryService _categoryService;
        IProductService _productService;
        IStoreService _storeService;
        IAddressService _addressService;
        IVideoService _videoService;
        IBannerService _bannerService;
        ISeoDefinitionService _seoDefinitionService;
        IPictureService _pictureService;
        IMemberStoreService _memberStoreService;
        IPhoneService _phoneService;
        IMemberService _memberService;
        ISearchScoreService _searchScoreService;


        #endregion

        #region Ctor

        public CategoryController(ICategoryService categoryService,
            IProductService productService,
            IStoreService storeService, IAddressService addressService,
            IBannerService bannerService, IVideoService videoService,
            ISeoDefinitionService seoDefinitionService,
            IPictureService pictureService,
            IMemberStoreService memberStoreService,
            IPhoneService phoneService,
            IMemberService memberService,
            ISearchScoreService searchScoreService
            )
        {
            _categoryService = categoryService;
            _productService = productService;
            _storeService = storeService;
            _addressService = addressService;
            _videoService = videoService;
            _bannerService = bannerService;
            _seoDefinitionService = seoDefinitionService;
            _pictureService = pictureService;
            _memberStoreService = memberStoreService;
            _phoneService = phoneService;
            _memberService = memberService;
            _searchScoreService = searchScoreService;
        }

        public void PrepareSeoLinks(MTCategoryProductViewModel model)
        {
            var request = this.Request;
            string requestAbsoluteUrl = request.Url.AbsolutePath;
            if (model.PagingModel.TotalPageCount > model.PagingModel.CurrentPageIndex + 1)
            {
                int nextPage = model.PagingModel.CurrentPageIndex + 1;
                model.NextPageUrl = QueryStringBuilder.ModifyQueryString(AppSettings.SiteUrl.Substring(0, AppSettings.SiteUrl.Length - 1) + request.Url.PathAndQuery, "page" + "=" + nextPage, null);
            }
            if (Request.QueryString["page"] != null)
            {
                if (Convert.ToInt32(Request.QueryString["page"]) > 1 && Convert.ToInt32(Request.QueryString["page"]) <= model.PagingModel.TotalPageCount)
                {
                    int prevPage = model.PagingModel.CurrentPageIndex - 1;
                    model.PrevPageUrl = QueryStringBuilder.ModifyQueryString(AppSettings.SiteUrl.Substring(0, AppSettings.SiteUrl.Length - 1) + request.Url.PathAndQuery, "page" + "=" + prevPage, null);

                }
            }
        }

        private string PrepareForLink(string selectedCategoryId)
        {
            int categoryId = GetCategoryIdQueryString();
            if (categoryId == 0)
            {
                categoryId = GetCategoryIdRouteData();
            }

            Category ustCat = null;
            if (categoryId > 0)
            {
                ustCat = _categoryService.GetCategoryByCategoryId(categoryId);
            }

            var selectedCategory = new Category();
            if (!string.IsNullOrEmpty(selectedCategoryId))
                selectedCategory = _categoryService.GetCategoryByCategoryId(Convert.ToInt32(selectedCategoryId));

            if ((ustCat == null || selectedCategory == null) && categoryId != 0)
            {
                return AppSettings.SiteAllCategoryUrl;
            }

            int brandId = GetBrandIdRouteData();

            int countryId = GetCountryIdRouteData();
            int cityId = GetCityIdRouteData();
            int localityId = GetLocalityIdRouteData();

            if (categoryId > 0 && countryId == 0 && cityId == 0 && localityId == 0)
            {
                int parameterCategoryId = brandId > 0 ? brandId : categoryId;
                var c = _categoryService.GetCategoryByCategoryId(parameterCategoryId);
                if (c != null)
                {
                    if (c.CategoryType == (byte)CategoryType.Sector ||
                         c.CategoryType == (byte)CategoryType.ProductGroup ||
                         c.CategoryType == (byte)CategoryType.Category)
                    {
                        var url = Request.Url.AbsolutePath;

                        if (!Request.IsLocal)
                        {
                            url = AppSettings.SiteUrlWithoutLastSlash + url;
                        }

                        string categoryNameForUrl = c.CategoryName;
                        if (!string.IsNullOrEmpty(c.CategoryContentTitle)) categoryNameForUrl = c.CategoryContentTitle;

                        var link = UrlBuilder.GetCategoryUrl(c.CategoryId, categoryNameForUrl, null, "");
                        if (url != link)
                        {
                            return link;
                        }
                    }
                    else if (c.CategoryType == (byte)CategoryType.Brand)
                    {
                        var url = Request.Url.AbsolutePath;
                        if (!Request.IsLocal)
                        {
                            url = AppSettings.SiteUrlWithoutLastSlash + url;
                        }


                        if (ustCat != null)
                        {
                            string categoryNameUrl = (!string.IsNullOrEmpty(ustCat.CategoryContentTitle)) ? ustCat.CategoryContentTitle : ustCat.CategoryName;

                            var link = UrlBuilder.GetCategoryUrl(ustCat.CategoryId, categoryNameUrl, c.CategoryId, c.CategoryName);
                            if (url != link)
                            {
                                return link;
                            }
                        }
                    }
                    else if (c.CategoryType == (byte)CategoryType.Series)
                    {

                        var url = Request.Url.AbsolutePath;
                        if (!Request.IsLocal)
                        {
                            url = AppSettings.SiteUrlWithoutLastSlash + url;
                        }

                        var topCategories = _categoryService.GetSPTopCategories(c.CategoryId);
                        return string.Empty;
                    }
                    else if (c.CategoryType == (byte)CategoryType.Model)
                    {
                        var link = Request.Url.AbsolutePath;
                        if (string.IsNullOrEmpty(selectedCategoryId))
                        {
                            string brandName = string.Empty;
                            string catName = string.Empty;
                            if (c.CategoryId == ustCat.CategoryId)
                            {
                                var brand = _categoryService.GetCategoryByCategoryId(c.CategoryParentId.Value);
                                ustCat = _categoryService.GetCategoryByCategoryId(brand.CategoryParentId.Value);
                                brandName = brand.CategoryName;

                            }


                            string categoryNameUrl = (!string.IsNullOrEmpty(ustCat.CategoryContentTitle)) ? ustCat.CategoryContentTitle : ustCat.CategoryName;

                            var url = UrlBuilder.GetModelUrl(c.CategoryId, c.CategoryName, brandName, categoryNameUrl, Convert.ToInt32(c.CategoryParentId));
                            if (link != url)
                                return url;

                        }
                        else
                        {
                            var ustCatBrand = _categoryService.GetCategoryByCategoryId(c.CategoryParentId.Value);
                            if (ustCatBrand.CategoryId.ToString() != selectedCategoryId)
                            {
                                string brandName = "";
                                if (c.CategoryId == ustCat.CategoryId)
                                {
                                    var brand = _categoryService.GetCategoryByCategoryId(c.CategoryParentId.Value);
                                    ustCat = _categoryService.GetCategoryByCategoryId(brand.CategoryParentId.Value);
                                    brandName = brand.CategoryName;

                                }
                                string categoryNameUrl = (!string.IsNullOrEmpty(ustCat.CategoryContentTitle)) ? ustCat.CategoryContentTitle : ustCat.CategoryName;

                                var url = UrlBuilder.GetModelUrl(c.CategoryId, c.CategoryName, brandName, categoryNameUrl, Convert.ToInt32(c.CategoryParentId));
                                if (link != url)
                                    return url;
                            }
                            //else
                            //{
                            //     ustCatBrand = _categoryService.GetCategoryByCategoryId(c.CategoryParentId.Value);
                            //     ustCat = _categoryService.GetCategoryByCategoryId(ustCatBrand.CategoryParentId.Value);
                            //    string categoryNameUrl = (!string.IsNullOrEmpty(ustCat.CategoryContentTitle)) ? ustCat.CategoryContentTitle : ustCat.CategoryName;

                            //    var url = UrlBuilder.GetModelUrl(c.CategoryId, c.CategoryName, ustCatBrand.CategoryName, categoryNameUrl, Convert.ToInt32(c.CategoryParentId));
                            //    if (link != url)
                            //        return url;

                            //}
                        }


                    }
                }
            }
            return "";
        }

        private void PrepareVideoModels(MTCategoryProductViewModel model)
        {
            var popularVideos = _videoService.GetSPPopularVideos(model.CategoryModel.SelectedCategoryId);
            List<MTPopularVideoModel> popularVideoModels = new List<MTPopularVideoModel>();
            foreach (var item in popularVideos)
            {
                popularVideoModels.Add(new MTPopularVideoModel
                {
                    CategoryName = item.CategoryName,
                    PicturePath = ImageHelper.GetVideoImagePath(item.VideoPicturePath),
                    ProductName = item.ProductName,
                    SingularViewCount = item.SingularViewCount,
                    TruncatetStoreName = StringHelper.Truncate(item.StoreName, 255),
                    VideoUrl = UrlBuilder.GetVideoUrl(item.VideoId, item.ProductName)
                });
            }
            model.VideoModels = popularVideoModels;
        }

        private void PrepareBannerModel(MTCategoryProductViewModel model)
        {
            var bannerItems = _bannerService.GetBannersByCategoryId(model.CategoryModel.SelectedCategoryId).Select(k => new MTBannerModel()
            {
                BannerDescription = k.BannerDescription,
                BannerId = k.BannerId,
                BannerLink = k.BannerLink,
                BannerResource = k.BannerResource,
                CategoryId = k.CategoryId,
                BannerType = k.BannerType,
                BannerAltTag = k.BannerAltTag
            }).ToList();
            model.BannerModels = bannerItems;

            if (bannerItems.Count > 0)
            {
                var otherBannerItems = new List<MTBannerModel>();
                foreach (var item in bannerItems)
                {
                    otherBannerItems.Add(item);
                }
                model.BannerModels = otherBannerItems;
            }

            var blankBanner = new MTBannerModel();
            blankBanner.BannerId = -1;
            blankBanner.CategoryId = -1;
            blankBanner.BannerType = (byte)BannerType.CategoryHorizontalBlankBanner;
            model.BannerModels.Add(blankBanner);
        }

        private void PrepareSectorCategories(MTCategoryProductViewModel model)
        {
            if (model.CategoryModel.SelectedCategoryId == 0)
            {
                model.CategoryModel.SelectedCategoryName = "Tüm Kategoriler";
                var mainCategories = _categoryService.GetMainCategories();
                model.ParentCategoryItems = new List<Category>();
                foreach (var item in mainCategories)
                {
                    model.ParentCategoryItems.Add(new Category
                    {
                        CategoryId = item.CategoryId,
                        CategoryName = item.CategoryName,
                        ProductCount = item.ProductCount
                    });
                }

                List<int> parentCategoryIds = (from c in model.ParentCategoryItems select c.CategoryId).ToList();
                model.ProductGroupParentItems = _categoryService.GetCategoriesByCategoryParentIds(parentCategoryIds);
            }
        }

        private void PrepareCategoryProductModel(MTCategoryProductViewModel model, out int serviceProductCount, out int newProductCount, out int usedProductCount,
                                                out IList<int> filterableCountryIds, out IList<int> filterableCityIds, out IList<int> filterableLocalityIds,
                                                out IList<FilterableCategoriesResult> filterableCategoryIds, out IList<int> filterableBrandIds,
                                                out IList<int> filterableModelIds, out IList<int> filterableSeriesIds, string SearchText)
        {
            filterableCategoryIds = new List<FilterableCategoriesResult>();
            filterableCountryIds = new List<int>();
            filterableCityIds = new List<int>();
            filterableLocalityIds = new List<int>();
            filterableBrandIds = new List<int>();
            filterableModelIds = new List<int>();
            filterableSeriesIds = new List<int>();

            serviceProductCount = 0;
            newProductCount = 0;
            usedProductCount = 0;

            int selectedCategoryId = 0;
            int selectedBrandId = 0;
            int selectedModelId = 0;
            int selectedSeriesId = 0;

            var seriesCategory = model.CategoryModel.TopCategoryItemModels.FirstOrDefault(c => c.CategoryType == (byte)CategoryType.Series);
            if (seriesCategory != null)
            {
                selectedSeriesId = seriesCategory.CategoryId;
            }
            var modelCategory = model.CategoryModel.TopCategoryItemModels.FirstOrDefault(c => c.CategoryType == (byte)CategoryType.Model);
            if (modelCategory != null)
            {
                selectedModelId = modelCategory.CategoryId;
            }
            var brandCategory = model.CategoryModel.TopCategoryItemModels.FirstOrDefault(c => c.CategoryType == (byte)CategoryType.Brand);
            if (brandCategory != null)
            {
                selectedBrandId = brandCategory.CategoryId;
            }
            else
            {
                selectedBrandId = GetBrandIdRouteData();
                if (selectedBrandId > 0)
                {
                    var brandC = _categoryService.GetCategoryByCategoryId(selectedBrandId);
                    brandCategory = new MTCategoryItemModel { CategoryId = selectedBrandId, CategoryName = brandC.CategoryName };
                }
            }

            MTCategoryItemModel categoryCategory = null;
            if (selectedBrandId > 0 || selectedModelId > 0 || selectedSeriesId > 0)
            {
                categoryCategory = model.CategoryModel.TopCategoryItemModels.LastOrDefault(c => c.CategoryType != (byte)CategoryType.Brand && c.CategoryType != (byte)CategoryType.Model && c.CategoryType != (byte)CategoryType.Series);
                if (categoryCategory != null)
                {
                    selectedCategoryId = categoryCategory.CategoryId;
                }
            }
            else
            {
                selectedCategoryId = GetCategoryIdRouteData();
                if (selectedCategoryId > 0)
                {
                    var categoryC = _categoryService.GetCategoryByCategoryId(selectedCategoryId);
                    categoryCategory = new MTCategoryItemModel { CategoryId = selectedCategoryId, CategoryName = categoryC.CategoryName };
                }
            }

            string searchType = GetSearchTypeQueryString();
            int searchTypeId = 0;
            switch (searchType)
            {
                case "sifir": searchTypeId = 72; break;
                case "ikinciel": searchTypeId = 73; break;
                case "hizmet": searchTypeId = 201; break;
                default: searchTypeId = 0; break;
            }
            string order = GetOrderQueryString();
            int orderById = 0;
            switch (order)
            {
                case "a-z": orderById = 2; break;
                case "z-a": orderById = 3; break;
                case "soneklenen": orderById = 4; break;
                case "fiyat-artan": orderById = 6; break;
                default: orderById = 0; break;
            }
            if (model.CategoryModel.SelectedCategoryType != (byte)CategoryType.Sector && orderById == 0)
                orderById = 5;
            if (!string.IsNullOrEmpty(SearchText))
                orderById = 5;


            int selectedCountryId = GetCountryIdQueryString();
            int selectedCityId = GetCityIdQueryString();
            int selectedLocalityId = GetLocalityIdQueryString();
            int pageIndex = GetPageQueryString();
            int pageSize = GetPageSizeQueryString();

            int mainPartyId = AuthenticationUser.CurrentUser.Membership.MainPartyId;

            CategoryProductsResult productResult = _productService.GetCategoryProducts(selectedCategoryId, selectedBrandId, selectedModelId, selectedSeriesId, searchTypeId, mainPartyId, selectedCountryId,
                                                                                        selectedCityId, selectedLocalityId, orderById, pageIndex, pageSize, SearchText);

            //newProductCount = _productService.GetProductCountBySearchType(selectedCategoryId, selectedBrandId, selectedModelId, selectedSeriesId, 72, model.SearchText, selectedCountryId, selectedCityId, selectedLocalityId);
            //usedProductCount = _productService.GetProductCountBySearchType(selectedCategoryId, selectedBrandId, selectedModelId, selectedSeriesId, 73, model.SearchText, selectedCountryId, selectedCityId, selectedLocalityId);
            //servicesProductCount = _productService.GetProductCountBySearchType(selectedCategoryId, selectedBrandId, selectedModelId, selectedSeriesId, 201, model.SearchText, selectedCountryId, selectedCityId, selectedLocalityId);

            newProductCount = productResult.NewProductCount;
            usedProductCount = productResult.UsedProductCount;
            serviceProductCount = productResult.ServicesProductCount;

            filterableBrandIds = productResult.FilterableBrandIds;
            filterableCityIds = productResult.FilterableCityIds;

            filterableCategoryIds = productResult.FilterableCategoryIds;
            filterableCountryIds = productResult.FilterableCountryIds;
            filterableLocalityIds = productResult.FilterableLocalityIds;
            filterableModelIds = productResult.FilterableModelIds;
            filterableSeriesIds = productResult.FilterableSeriesIds;
            model.TotalItemCount = productResult.TotalCount;
            model.FilteringContext.TotalItemCount = model.TotalItemCount;
            List<WebCategoryProductResult> producResultNew = productResult.Products.ToList();
            if (orderById == 5)
                producResultNew = productResult.Products.OrderByDescending(x => x.Doping).ToList();

            List<int?> mainPartyIds = producResultNew.Select(x => x.StoreMainPartyId).ToList();
            var StoreListesi = _storeService.GetStoresByMainPartyIds(mainPartyIds).ToList();
            foreach (WebCategoryProductResult product in producResultNew)
            {
                var store = StoreListesi.FirstOrDefault(x => x.MainPartyId == product.StoreMainPartyId);
                if (store != null)
                {
                    var categoryProductModel = new MTCategoryProductModel
                    {
                        BrandId = product.BrandId,
                        BrandName = product.BrandName,
                        //BriefDetailText = product.BriefDetailText,
                        CategoryId = product.CategoryId,

                        MainPartyId = product.MainPartyId,
                        ModelId = product.ModelId,
                        ModelName = product.ModelName,
                        ModelYear = product.ModelYear,
                        PicturePath = ImageHelper.GetProductImagePath(product.ProductId, product.MainPicture, ProductImageSize.px200x150),
                        ProductId = product.ProductId,
                        ProductName = product.ProductName,
                        ProductNo = product.ProductNo,
                        ProductPiceDesc = product.ProductPrice.HasValue ? product.ProductPrice.Value : 0,
                        CurrencySymbol = product.GetCurrencySymbol(),
                        ProductSalesTypeText = product.ProductSalesTypeText,
                        ProductStatuText = product.ProductStatuText,
                        ProductTypeText = product.ProductTypeText,
                        SeriesId = product.SeriesId,
                        StoreName = store.StoreShortName,
                        //ProductDescription = product.ProductDescription,
                        CurrencyCss = product.GetCurrencyCssName(),
                        Price = product.GetFormattedPrice(),
                        ProductPriceType = product.ProductPriceType,
                        HasVideo = product.HasVideo,
                        StoreMainPartyId = product.StoreMainPartyId,
                        StoreUrl = UrlBuilder.GetStoreProfileUrl(Convert.ToInt32(product.StoreMainPartyId), product.StoreName, store.StoreUrlName),
                        StoreConnectUrl = UrlBuilder.GetStoreConnectUrl(Convert.ToInt32(product.StoreMainPartyId), product.StoreName, store.StoreUrlName),
                        ProductContactUrl = UrlBuilder.GetProductContactUrl(product.ProductId, product.StoreName),
                        KdvOrFobText = product.GetKdvOrFobText(),

                        ProductPriceWithDiscount = product.DiscountType.HasValue && product.DiscountType.Value != 0 ? product.ProductPriceWithDiscount.Value.GetMoneyFormattedDecimalToString() : ""
                    };

                    if (orderById == 5)
                        categoryProductModel.Doping = product.Doping;
                    model.CategoryProductModels.Add(categoryProductModel);
                }

            }
        }

        private void PrepareDataFilterModel(MTCategoryProductViewModel model, IList<int> filterableCountryIds, IList<int> filterableCityIds,
                                            IList<int> filterableLocalityIds, IList<int> filterableBrandIds, IList<int> filterableModelIds,
                                            IList<int> filterableSeriesIds)
        {
            int selectedCountryId = GetCountryIdQueryString();
            int selectedCityId = GetCityIdQueryString();
            int selectedLocalityId = GetLocalityIdQueryString();
            int routeBrandId = GetBrandIdRouteData();

            string customFilter = GetCustomFilterQueryString();

            var searchText = model.SearchText;

            int selectedCategoryId = 0;
            int selectedBrandId = 0;
            int selectedModelId = 0;
            int selectedSeriesId = 0;
            int productCount = 0;
            var categoryFilterParams = GetCategoryFilterParameters(searchText, customFilter, selectedCountryId, selectedCityId,
                selectedLocalityId);

            var selectedSeriesCategory = model.CategoryModel.TopCategoryItemModels.FirstOrDefault(c => c.CategoryType == (byte)CategoryType.Series);
            if (selectedSeriesCategory != null)
            {
                selectedSeriesId = selectedSeriesCategory.CategoryId;
            }
            var selectedModelCategory = model.CategoryModel.TopCategoryItemModels.FirstOrDefault(c => c.CategoryType == (byte)CategoryType.Model);
            if (selectedModelCategory != null)
            {
                selectedModelId = selectedModelCategory.CategoryId;
            }
            var selectedBrandCategory = model.CategoryModel.TopCategoryItemModels.FirstOrDefault(c => c.CategoryType == (byte)CategoryType.Brand);
            if (selectedBrandCategory != null)
            {
                selectedBrandId = selectedBrandCategory.CategoryId;
            }
            else
            {
                selectedBrandId = GetBrandIdRouteData();
                if (selectedBrandId > 0)
                {
                    var brandC = _categoryService.GetCategoryByCategoryId(selectedBrandId);
                    selectedBrandCategory = new MTCategoryItemModel { CategoryId = selectedBrandId, CategoryName = brandC.CategoryName, CategoryParentId = brandC.CategoryParentId };
                }
            }

            MTCategoryItemModel selectedCategoryCategory = null;
            if (selectedBrandId > 0 || selectedModelId > 0 || selectedSeriesId > 0)
            {
                selectedCategoryCategory = model.CategoryModel.TopCategoryItemModels.LastOrDefault(c => c.CategoryType != (byte)CategoryType.Brand && c.CategoryType != (byte)CategoryType.Model && c.CategoryType != (byte)CategoryType.Series);
                if (selectedCategoryCategory != null)
                {
                    selectedCategoryId = selectedCategoryCategory.CategoryId;
                }
            }
            else
            {
                selectedCategoryId = GetCategoryIdRouteData();
                if (selectedCategoryId > 0)
                {
                    var categoryC = _categoryService.GetCategoryByCategoryId(selectedCategoryId);
                    selectedCategoryCategory = new MTCategoryItemModel { CategoryId = selectedCategoryId, CategoryName = categoryC.CategoryName };
                    selectedCategoryCategory.CategoryContentTitle = categoryC.CategoryContentTitle;
                    if (string.IsNullOrEmpty(categoryC.CategoryContentTitle))
                        selectedCategoryCategory.CategoryContentTitle = categoryC.CategoryName;

                    productCount = categoryC.ProductCount.Value;

                }
            }


            if (model.CategoryModel.SelectedCategoryType != (byte)CategoryType.Sector && productCount < 2000)
            {
                DataFilterModel brandDataFilterModel = new DataFilterModel();
                brandDataFilterModel.FilterId = 1;
                brandDataFilterModel.FilterName = "Marka";

                if (filterableBrandIds != null && filterableBrandIds.Count > 0)
                {
                    var filterableBrands = _categoryService.GetCategoriesByCategoryIds(filterableBrandIds.Distinct().ToList());

                    Category selectedBrandIdCategory = null;
                    if (selectedBrandId > 0)
                    {
                        selectedBrandIdCategory = _categoryService.GetCategoryByCategoryId(selectedBrandId);
                    }

                    bool brandSelected = selectedBrandIdCategory != null;

                    if (filterableBrands.Count > 0)
                    {
                        var distinctfilterableBrands = filterableBrands.Select(b => b.CategoryName).Distinct();
                        if (distinctfilterableBrands.Count() == filterableBrands.Count)
                        {
                            foreach (var item in filterableBrands)
                            {
                                string filterUrl = "";

                                if (brandSelected && selectedBrandIdCategory.CategoryName == item.CategoryName)
                                {

                                    filterUrl = UrlBuilder.GetCategoryUrl(selectedCategoryCategory.CategoryId,
                                        selectedCategoryCategory.CategoryContentTitle, null, null);
                                }
                                else
                                {

                                    filterUrl = UrlBuilder.GetCategoryUrl(selectedCategoryCategory.CategoryId,
                                     selectedCategoryCategory.CategoryContentTitle, item.CategoryId, item.CategoryName);
                                }

                                brandDataFilterModel.ItemModels.Add(new DataFilterItemModel
                                {
                                    FilterItemId = item.CategoryId,
                                    FilterName = item.CategoryName,
                                    FilterUrl = UrlBuilder.GetFilterUrl(filterUrl, categoryFilterParams),
                                    Selected = ((brandSelected ? selectedBrandIdCategory.CategoryName : "") == item.CategoryName),
                                    Count = filterableBrandIds.Count(c => c == item.CategoryId)

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
                                    string filterUrl = "";

                                    if (brandSelected && selectedBrandIdCategory.CategoryName == brands.First().CategoryName)
                                    {
                                        filterUrl = UrlBuilder.GetCategoryUrl(selectedCategoryCategory.CategoryId,
                                             selectedCategoryCategory.CategoryContentTitle, null, null);
                                    }
                                    else
                                    {
                                        filterUrl = UrlBuilder.GetCategoryUrl(selectedCategoryCategory.CategoryId, selectedCategoryCategory.CategoryContentTitle, brands.First().CategoryId, brands.First().CategoryName);
                                    }

                                    brandDataFilterModel.ItemModels.Add(new DataFilterItemModel
                                    {
                                        FilterItemId = brands.First().CategoryId,
                                        FilterName = brands.First().CategoryName,
                                        FilterUrl = UrlBuilder.GetFilterUrl(filterUrl, categoryFilterParams),
                                        Selected = ((brandSelected ? selectedBrandIdCategory.CategoryName : "") == brands.First().CategoryName),
                                        Count = filterableBrandIds.Count(c => c == brands.First().CategoryId)

                                    });
                                }
                                else
                                {
                                    int count = (from fb in filterableBrandIds
                                                 join b in brands on fb equals b.CategoryId
                                                 select fb).Count();

                                    string filterUrl = "";
                                    var brandAdded = brandDataFilterModel.ItemModels.FirstOrDefault(x => x.FilterName.Trim() == brands.First().CategoryName.Trim());
                                    if (brandAdded != null)
                                    {
                                        brandAdded.Count += count;
                                    }
                                    else
                                    {
                                        if (brandSelected && selectedBrandIdCategory.CategoryName == brands.First().CategoryName)
                                        {
                                            filterUrl = UrlBuilder.GetCategoryUrl(selectedCategoryCategory.CategoryId, selectedCategoryCategory.CategoryContentTitle
                                               , null, null);
                                        }
                                        else
                                        {

                                            filterUrl = UrlBuilder.GetCategoryUrl(selectedCategoryCategory.CategoryId, selectedCategoryCategory.CategoryContentTitle, brands.First().CategoryId, brands.First().CategoryName);
                                        }

                                        brandDataFilterModel.ItemModels.Add(new DataFilterItemModel
                                        {
                                            FilterItemId = brands.First().CategoryId,
                                            FilterName = brands.First().CategoryName,
                                            FilterUrl = UrlBuilder.GetFilterUrl(filterUrl, categoryFilterParams),
                                            Selected = ((brandSelected ? selectedBrandIdCategory.CategoryName : "") == brands.First().CategoryName),
                                            Count = count
                                        });
                                    }
                                }
                            }
                        }

                        DataFilterItemModel selectedBrand = brandDataFilterModel.ItemModels.FirstOrDefault(f => f.Selected);
                        var clearFilterUrl = UrlBuilder.GetCategoryUrl(selectedCategoryCategory.CategoryId,
                             selectedCategoryCategory.CategoryContentTitle, null, null);
                        if (selectedBrand != null)
                        {
                            brandDataFilterModel.SelectedFilter = true;
                            brandDataFilterModel.SelectedFilterItemCount = selectedBrand.Count;
                            brandDataFilterModel.SelectedFilterItemName = selectedBrand.FilterName;
                            brandDataFilterModel.ClearFilterText = "Tüm Markalar";
                            brandDataFilterModel.ClearFilterUrl = UrlBuilder.GetFilterUrl(clearFilterUrl, categoryFilterParams);
                            brandDataFilterModel.SelectedFilterUrl = selectedBrand.FilterUrl;
                        }
                    }
                }
                model.FilteringContext.DataFilterMoldes.Add(brandDataFilterModel);


                if (selectedBrandId > 0)
                {
                    if (filterableSeriesIds != null && filterableSeriesIds.Count > 0)
                    {
                        DataFilterModel seriesDataFilterModel = new DataFilterModel();
                        seriesDataFilterModel.FilterId = 3;
                        seriesDataFilterModel.FilterName = "Seri";

                        DataFilterItemModel selectedBrand = brandDataFilterModel.ItemModels.FirstOrDefault(f => f.Selected);

                        Category selectedSeriesIdCategory = null;
                        bool serieSelected = false;
                        if (selectedSeriesId > 0)
                        {
                            selectedSeriesIdCategory = _categoryService.GetCategoryByCategoryId(selectedSeriesId);
                            serieSelected = selectedSeriesIdCategory != null;
                        }

                        var filterableSeries = _categoryService.GetCategoriesByCategoryIds(filterableSeriesIds.Distinct().ToList());
                        if (filterableSeries.Count > 0)
                        {
                            foreach (var item in filterableSeries)
                            {
                                var filterUrl = "";

                                if (selectedSeriesId == item.CategoryId)
                                {
                                    filterUrl = UrlBuilder.GetCategoryUrl(selectedCategoryCategory.CategoryId,
                                        selectedCategoryCategory.CategoryContentTitle, selectedBrandCategory.CategoryId,
                                        selectedBrandCategory.CategoryName
                                        );
                                }
                                else
                                {
                                    filterUrl = UrlBuilder.GetSerieUrl(item.CategoryId, item.CategoryName,
                                    selectedBrandCategory.CategoryName, selectedCategoryCategory.CategoryContentTitle);
                                }

                                seriesDataFilterModel.ItemModels.Add(new DataFilterItemModel
                                {
                                    FilterItemId = item.CategoryId,
                                    FilterName = item.CategoryName,
                                    FilterUrl = UrlBuilder.GetFilterUrl(filterUrl, categoryFilterParams),
                                    Selected = (selectedSeriesId == item.CategoryId),
                                    Count = filterableSeriesIds.Count(l => l == item.CategoryId)
                                });
                            }
                            DataFilterItemModel selectedSeries = seriesDataFilterModel.ItemModels.FirstOrDefault(f => f.Selected);
                            var clearFilterUrl = UrlBuilder.GetCategoryUrl(selectedCategoryCategory.CategoryId, selectedCategoryCategory.CategoryContentTitle, selectedBrandCategory.CategoryId, selectedBrandCategory.CategoryName);
                            if (selectedSeries != null)
                            {
                                seriesDataFilterModel.SelectedFilter = true;
                                seriesDataFilterModel.SelectedFilterItemCount = selectedSeries.Count;
                                seriesDataFilterModel.SelectedFilterItemName = selectedSeries.FilterName;
                                seriesDataFilterModel.ClearFilterText = "Tüm Seriler";
                                seriesDataFilterModel.ClearFilterUrl = UrlBuilder.GetFilterUrl(clearFilterUrl, categoryFilterParams);
                                seriesDataFilterModel.SelectedFilterUrl = selectedSeries.FilterUrl;
                            }
                        }
                        model.FilteringContext.DataFilterMoldes.Add(seriesDataFilterModel);
                    }

                    if (selectedSeriesId > 0)
                    {
                        if (filterableModelIds != null && filterableModelIds.Count > 0)
                        {
                            DataFilterModel modelDataFilterModel = new DataFilterModel();
                            modelDataFilterModel.FilterId = 2;
                            modelDataFilterModel.FilterName = "Model";
                            DataFilterItemModel selectedBrand = brandDataFilterModel.ItemModels.FirstOrDefault(f => f.Selected);
                            bool modelSelected = false;
                            var selectedModelIdCategory = new Category();
                            if (selectedModelId != 0)
                            {
                                selectedModelIdCategory = _categoryService.GetCategoryByCategoryId(selectedModelId);
                                modelSelected = selectedModelIdCategory != null;
                            }


                            var filterableModels = _categoryService.GetCategoriesByCategoryIds(filterableModelIds.Distinct().ToList());
                            if (filterableModels.Count > 0)
                            {
                                foreach (var item in filterableModels)
                                {
                                    var filterUrl = "";

                                    if (modelSelected && selectedModelIdCategory.CategoryName == item.CategoryName)
                                    {
                                        filterUrl = UrlBuilder.GetCategoryUrl(selectedCategoryCategory.CategoryId,
                                            selectedCategoryCategory.CategoryContentTitle, selectedBrand.FilterItemId,
                                            selectedBrand.FilterName);
                                    }
                                    else
                                    {
                                        filterUrl = UrlBuilder.GetModelUrl(item.CategoryId, item.CategoryName,
                                        selectedBrandCategory.CategoryName, selectedCategoryCategory.CategoryContentTitle,
                                        selectedCategoryCategory.CategoryId);
                                    }

                                    modelDataFilterModel.ItemModels.Add(new DataFilterItemModel
                                    {
                                        FilterItemId = item.CategoryId,
                                        FilterName = item.CategoryName,
                                        FilterUrl = UrlBuilder.GetFilterUrl(filterUrl, categoryFilterParams),
                                        Selected = ((modelSelected ? selectedModelIdCategory.CategoryName : "") == item.CategoryName),
                                        Count = filterableModelIds.Count(l => l == item.CategoryId)
                                    });
                                }
                                DataFilterItemModel selectedModel = modelDataFilterModel.ItemModels.FirstOrDefault(f => f.Selected);

                                if (selectedModel != null)
                                {
                                    var clearFilterUrl = UrlBuilder.GetSerieUrl(selectedSeriesCategory.CategoryId, selectedSeriesCategory.CategoryName, selectedBrandCategory.CategoryName, selectedCategoryCategory.CategoryContentTitle);
                                    modelDataFilterModel.SelectedFilter = true;
                                    modelDataFilterModel.SelectedFilterItemCount = selectedModel.Count;
                                    modelDataFilterModel.SelectedFilterItemName = selectedModel.FilterName;
                                    modelDataFilterModel.ClearFilterText = "Tüm Modeller";
                                    modelDataFilterModel.ClearFilterUrl = UrlBuilder.GetFilterUrl(clearFilterUrl, categoryFilterParams);
                                    modelDataFilterModel.SelectedFilterUrl = selectedModel.FilterUrl;
                                }
                            }
                            model.FilteringContext.DataFilterMoldes.Add(modelDataFilterModel);
                        }
                    }
                    else if (filterableSeriesIds.Count == 0)
                    {
                        if (filterableModelIds != null && filterableModelIds.Count > 0)
                        {
                            DataFilterModel modelDataFilterModel = new DataFilterModel();
                            modelDataFilterModel.FilterId = 2;
                            modelDataFilterModel.FilterName = "Model";

                            DataFilterItemModel selectedBrand = brandDataFilterModel.ItemModels.FirstOrDefault(f => f.Selected);
                            bool modelSelected = false;
                            Category selectedModelIdCategory = null;
                            if (selectedModelId > 0)
                            {
                                selectedModelIdCategory = _categoryService.GetCategoryByCategoryId(selectedModelId);
                                modelSelected = selectedModelIdCategory != null;
                            }


                            var filterableModels = _categoryService.GetCategoriesByCategoryIds(filterableModelIds.Distinct().ToList());
                            if (filterableModels.Count > 0)
                            {
                                foreach (var item in filterableModels)
                                {
                                    var modelBrandParentCategory = _categoryService.GetCategoryByCategoryId(item.CategoryParentId.Value);
                                    var brandParentCategory = _categoryService.GetCategoryByCategoryId(modelBrandParentCategory.CategoryParentId.Value);
                                    var categoryUrlName = !string.IsNullOrEmpty(brandParentCategory.CategoryContentTitle) ? brandParentCategory.CategoryContentTitle : brandParentCategory.CategoryName;
                                    var filterUrl = "";

                                    if (modelSelected && selectedModelIdCategory.CategoryName == item.CategoryName)
                                    {
                                        filterUrl = UrlBuilder.GetCategoryUrl(selectedCategoryCategory.CategoryId,
                                            selectedCategoryCategory.CategoryContentTitle, selectedBrand.FilterItemId,
                                            selectedBrand.FilterName);
                                    }
                                    else
                                    {
                                        filterUrl = UrlBuilder.GetModelUrl(item.CategoryId, item.CategoryName,
                                        selectedBrandCategory.CategoryName, categoryUrlName,
                                       modelBrandParentCategory.CategoryId);
                                    }

                                    modelDataFilterModel.ItemModels.Add(new DataFilterItemModel
                                    {
                                        FilterItemId = item.CategoryId,
                                        FilterName = item.CategoryName,
                                        FilterUrl = UrlBuilder.GetFilterUrl(filterUrl, categoryFilterParams),
                                        Selected = ((modelSelected ? selectedModelIdCategory.CategoryName : "") == item.CategoryName),
                                        Count = filterableModelIds.Count(l => l == item.CategoryId)
                                    });
                                }
                                DataFilterItemModel selectedModel = modelDataFilterModel.ItemModels.FirstOrDefault(f => f.Selected);
                                if (selectedModel != null)
                                {
                                    var clearFilterUrl = UrlBuilder.GetCategoryUrl(selectedCategoryCategory.CategoryId, selectedCategoryCategory.CategoryContentTitle, selectedBrandCategory.CategoryId, selectedBrandCategory.CategoryName);
                                    modelDataFilterModel.SelectedFilter = true;
                                    modelDataFilterModel.SelectedFilterItemCount = selectedModel.Count;
                                    modelDataFilterModel.SelectedFilterItemName = selectedModel.FilterName;
                                    modelDataFilterModel.ClearFilterText = "Tüm Modeller";
                                    modelDataFilterModel.ClearFilterUrl = UrlBuilder.GetFilterUrl(clearFilterUrl, categoryFilterParams);
                                    modelDataFilterModel.SelectedFilterUrl = selectedModel.FilterUrl;
                                }
                            }
                            model.FilteringContext.DataFilterMoldes.Add(modelDataFilterModel);
                        }
                    }

                }

            }

            DataFilterModel countryDataFilterModel = new DataFilterModel();
            countryDataFilterModel.FilterId = 4;
            countryDataFilterModel.FilterName = "Ülke";
            if (filterableCountryIds != null && filterableCountryIds.Count > 0)
            {
                var filterableCountries = _addressService.GetCountriesByCountryIds(filterableCountryIds.Distinct().ToList());

                if (filterableCountries.Count > 0)
                {
                    foreach (var item in filterableCountries)
                    {
                        var countryUrl = "";
                        if (selectedCountryId == item.CountryId)
                        {
                            countryUrl = Request.Url.AbsolutePath;

                            if (!string.IsNullOrEmpty(searchText))
                            {
                                countryUrl += "?SearchText=" + searchText;
                            }
                        }
                        else
                        {
                            countryUrl = Request.Url.AbsolutePath + "?ulke=" + UrlBuilder.ToUrl(item.CountryName) + "-" + item.CountryId;

                            if (!string.IsNullOrEmpty(searchText))
                            {
                                countryUrl += "&SearchText=" + searchText;
                            }
                        }
                        if (Request.QueryString["Gorunum"] != null)
                        {
                            countryUrl += countryUrl.Contains("?") ? "&Gorunum=" + Request.QueryString["Gorunum"].ToString() : "?Gorunum=" + Request.QueryString["Gorunum"].ToString();
                        }

                        countryDataFilterModel.ItemModels.Add(new DataFilterItemModel
                        {
                            FilterItemId = item.CountryId,
                            FilterName = item.CountryName,
                            FilterUrl = countryUrl,
                            Selected = (selectedCountryId == item.CountryId),
                            Count = filterableCountryIds.Count(c => c == item.CountryId)

                        });
                    }

                    DataFilterItemModel selectedCountry = countryDataFilterModel.ItemModels.FirstOrDefault(f => f.Selected);
                    if (selectedCountry != null)
                    {
                        var clearFilterUrl = Request.Url.AbsolutePath;//UrlBuilder.CategoryUrl(selectedCurrentCategory.CategoryId, selectedCurrentCategory.CategoryName, null, null);
                        countryDataFilterModel.SelectedFilter = true;
                        countryDataFilterModel.SelectedFilterItemCount = selectedCountry.Count;
                        countryDataFilterModel.SelectedFilterItemName = selectedCountry.FilterName;
                        countryDataFilterModel.ClearFilterText = "Tüm Ülkeler";
                        countryDataFilterModel.ClearFilterUrl = string.IsNullOrEmpty(searchText) ? clearFilterUrl : clearFilterUrl + (clearFilterUrl.Contains("?") ? "&SearchText=" + searchText : "?SearchText=" + searchText);
                        countryDataFilterModel.SelectedFilterUrl = selectedCountry.FilterUrl;
                    }
                }
            }
            model.FilteringContext.DataFilterMoldes.Add(countryDataFilterModel);

            DataFilterModel cityDataFilterModel = new DataFilterModel();
            cityDataFilterModel.FilterId = 5;
            cityDataFilterModel.FilterName = "Şehir";
            if (filterableCityIds != null && filterableCityIds.Count > 0)
            {
                var filterableCities = _addressService.GetCitiesByCityIds(filterableCityIds.Distinct().ToList());
                if (filterableCities.Any())
                {
                    DataFilterItemModel selectedCountry = countryDataFilterModel.ItemModels.FirstOrDefault(f => f.Selected);

                    foreach (var item in filterableCities)
                    {
                        var cityUrl = "";

                        if (selectedCityId == item.CityId)
                        {
                            cityUrl = Request.Url.AbsolutePath + "?ulke=" + UrlBuilder.ToUrl(selectedCountry.FilterName) + "-" + selectedCountry.FilterItemId;
                        }
                        else
                        {
                            cityUrl = Request.Url.AbsolutePath + "?ulke=" + item.CountryId + "&sehir=" + UrlBuilder.ToUrl(item.CityName) + "-" + item.CityId;
                        }

                        if (!string.IsNullOrEmpty(searchText))
                        {
                            cityUrl += "&SearchText=" + searchText;
                        }

                        if (Request.QueryString["Gorunum"] != null)
                        {
                            cityUrl += "&Gorunum=" + Request.QueryString["Gorunum"].ToString();
                        }

                        cityDataFilterModel.ItemModels.Add(new DataFilterItemModel
                        {
                            FilterItemId = item.CityId,
                            FilterName = item.CityName,
                            FilterUrl = cityUrl,
                            Selected = (selectedCityId == item.CityId),
                            Count = filterableCityIds.Count(c => c == item.CityId)
                        });
                    }

                    DataFilterItemModel selectedCity = cityDataFilterModel.ItemModels.FirstOrDefault(f => f.Selected);
                    if (selectedCity != null)
                    {
                        var clearFilterUrl = Request.Url.AbsolutePath + "?ulke=" + UrlBuilder.ToUrl(selectedCountry.FilterName) + "-" + selectedCountry.FilterItemId;
                        cityDataFilterModel.SelectedFilter = true;
                        cityDataFilterModel.SelectedFilterItemCount = selectedCity.Count;
                        cityDataFilterModel.SelectedFilterItemName = selectedCity.FilterName;
                        cityDataFilterModel.ClearFilterText = "Tüm Şehirler";
                        cityDataFilterModel.ClearFilterUrl = string.IsNullOrEmpty(searchText) ? clearFilterUrl : clearFilterUrl + (clearFilterUrl.Contains("?") ? "&SearchText=" + searchText : "?SearchText=" + searchText);
                        cityDataFilterModel.SelectedFilterUrl = selectedCity.FilterUrl;
                    }
                }
            }
            model.FilteringContext.DataFilterMoldes.Add(cityDataFilterModel);

            if (selectedCityId > 0)
            {
                var selectedCity = cityDataFilterModel.ItemModels.FirstOrDefault(fc => fc.Selected);
                DataFilterModel localityDataFilterModel = new DataFilterModel();
                localityDataFilterModel.FilterId = 6;
                localityDataFilterModel.FilterName = "İlçe";

                if (filterableLocalityIds != null && filterableLocalityIds.Count > 0)
                {
                    var filterableLocalities = _addressService.GetLocalitiesByLocalityIds(filterableLocalityIds.Distinct().ToList()).Where(x => x.CityId == selectedCityId);
                    if (filterableLocalities.Any())
                    {
                        DataFilterItemModel selectedCountry = countryDataFilterModel.ItemModels.FirstOrDefault(f => f.Selected);

                        foreach (var item in filterableLocalities)
                        {
                            var localityUrl = "";

                            if (selectedLocalityId == item.LocalityId)
                            {
                                localityUrl = Request.Url.AbsolutePath + "?ulke=" + selectedCountry.FilterItemId + "&sehir=" + UrlBuilder.ToUrl(selectedCity.FilterName) + "-" + selectedCity.FilterItemId;
                            }
                            else
                            {
                                localityUrl = Request.Url.AbsolutePath + "?ulke=" + selectedCountry.FilterItemId + "&sehir=" + selectedCity.FilterItemId + "&ilce=" + UrlBuilder.ToUrl(item.LocalityName) + "-" + item.LocalityId;
                            }

                            if (!string.IsNullOrEmpty(searchText))
                            {
                                localityUrl += "&SearchText=" + searchText;
                            }
                            if (Request.QueryString["Gorunum"] != null)
                            {
                                localityUrl += "&Gorunum=" + Request.QueryString["Gorunum"].ToString();
                            }


                            localityDataFilterModel.ItemModels.Add(new DataFilterItemModel
                            {
                                FilterItemId = item.LocalityId,
                                FilterName = item.LocalityName,
                                FilterUrl = localityUrl,
                                Selected = (selectedLocalityId == item.LocalityId),
                                Count = filterableLocalityIds.Count(l => l == item.LocalityId)
                            });
                        }
                        DataFilterItemModel selectedLocality = localityDataFilterModel.ItemModels.FirstOrDefault(f => f.Selected);
                        if (selectedLocality != null)
                        {
                            var clearFilterUrl = Request.Url.AbsolutePath + "?ulke=" + selectedCountry.FilterItemId + "&sehir=" + UrlBuilder.ToUrl(selectedCity.FilterName) + "-" + selectedCity.FilterItemId;
                            localityDataFilterModel.SelectedFilter = true;
                            localityDataFilterModel.SelectedFilterItemCount = selectedLocality.Count;
                            localityDataFilterModel.SelectedFilterItemName = selectedLocality.FilterName;
                            localityDataFilterModel.ClearFilterText = "Tüm İlçeler";
                            localityDataFilterModel.ClearFilterUrl = string.IsNullOrEmpty(searchText) ? clearFilterUrl : clearFilterUrl + (clearFilterUrl.Contains("?") ? "&SearchText=" + searchText : "?SearchText=" + searchText);
                            localityDataFilterModel.SelectedFilterUrl = selectedLocality.FilterUrl;
                        }
                    }
                }
                model.FilteringContext.DataFilterMoldes.Add(localityDataFilterModel);
            }
        }

        public CategoryFilterHelper GetCategoryFilterParameters(string searchText, string searchType, int countryId = 0, int cityId = 0, int localityId = 0)
        {
            var catFilterModel = new CategoryFilterHelper();

            int customFilterId = 0;
            catFilterModel.SearchType = searchType;
            switch (searchType)
            {
                case "sifir": customFilterId = 1; break;
                case "ikinciel": customFilterId = 2; break;
                default: customFilterId = 0; break;
            }

            catFilterModel.CustomFilterId = customFilterId;

            catFilterModel.SearchText = searchText;
            if (countryId > 0)
            {
                var countryName = _addressService.GetCountriesByCountryIds(new List<int> { countryId }).First().CountryName;
                catFilterModel.CountryId = countryId;
                catFilterModel.CountryName = countryName;
            }

            if (cityId > 0)
            {
                var cityName = _addressService.GetCitiesByCityIds(new List<int> { cityId }).First().CityName;
                catFilterModel.CityId = cityId;
                catFilterModel.CityName = cityName;
            }

            if (localityId > 0)
            {
                var localityName =
                    _addressService.GetLocalitiesByLocalityIds(new List<int> { localityId }).First().LocalityName;
                catFilterModel.LocalityId = localityId;
                catFilterModel.LocalityName = localityName;
            }

            if (Request.QueryString["Gorunum"] != null)
            {
                catFilterModel.ViewType = Request.QueryString["Gorunum"].ToString();
            }
            return catFilterModel;
        }

        private void PrepareProductCategoryModel(MTCategoryProductViewModel model, IList<FilterableCategoriesResult> filterableCategoryIds)
        {
            int brandId = GetBrandIdRouteData();
            int countryId = GetCountryIdQueryString();
            int cityId = GetCityIdQueryString();
            int localityId = GetLocalityIdQueryString();
            string searchType = GetSearchTypeQueryString();

            var filterParams = GetCategoryFilterParameters(model.SearchText, searchType, countryId, cityId, localityId);

            var categories =
                _categoryService.GetCategoriesByCategoryIds(filterableCategoryIds.Select(x => x.CategoryId).ToList());

            var orderedCategories = (from c in categories
                                     join f in filterableCategoryIds on c.CategoryId equals f.CategoryId
                                     select
                                     new
                                     {
                                         CategoryOrder = c.CategoryOrder != null ? c.CategoryOrder.Value : 0,
                                         CategoryId = c.CategoryId,
                                         ProductCount = f.ProductCount
                                     }).OrderBy(x => x.CategoryOrder).ToList();

            List<int> Liste = orderedCategories.Select(x => (int)x.CategoryId).ToList();
            var CategoryList = _categoryService.GetCategoriesByCategoryIds(Liste).ToList();
            foreach (var item in orderedCategories)
            {
                var category = CategoryList.FirstOrDefault(x => x.CategoryId == item.CategoryId);
                string categoryNameUrl = (!string.IsNullOrEmpty(category.CategoryContentTitle)) ? category.CategoryContentTitle : category.CategoryName;

                string categoryUrl = UrlBuilder.GetCategoryUrl(item.CategoryId, categoryNameUrl, null, null);

                if (brandId > 0)
                {
                    var brandCategory = _categoryService.GetCategoryByCategoryId(brandId);

                    categoryUrl = UrlBuilder.GetCategoryUrl(item.CategoryId, categoryNameUrl, brandCategory.CategoryId, brandCategory.CategoryName);
                }


                var categoryItemModel = new MTProductCategoryItemModel
                {
                    CategoryId = item.CategoryId,
                    CategoryType = (byte)category.CategoryType,
                    CategoryName = category.CategoryName,
                    CategoryUrl = UrlBuilder.GetFilterUrl(categoryUrl, filterParams),
                    ProductCount = item.ProductCount,
                    TruncatedCategoryName = StringHelper.Truncate(category.CategoryName, 100)

                };
                model.CategoryModel.CategoryItemModels.Add(categoryItemModel);
            }




        }

        public void PrepareProductCategoryTopCategoryModel(MTCategoryProductViewModel model)
        {
            int brandId = GetBrandIdRouteData();
            int countryId = GetCountryIdQueryString();
            int cityId = GetCityIdQueryString();
            int localityId = GetLocalityIdQueryString();
            string customFilter = GetCustomFilterQueryString();
            int customFilterId = 0;
            model.AllCategoryUrl = AppSettings.SiteAllCategoryUrl;
            switch (customFilter)
            {
                case "sifir": customFilterId = 72; break;
                case "ikinciel": customFilterId = 73; break;
                default: customFilterId = 0; break;
            }

            var filterParams = GetCategoryFilterParameters(model.SearchText, customFilter, countryId, cityId, localityId);
            int selectedCategoryId = GetCategoryIdRouteData();

            var topCategories = _categoryService.GetSPTopCategories(selectedCategoryId);
            if (topCategories.Count > 0)
            {
                var lastCategory = topCategories.LastOrDefault();
                model.CategoryModel.SelectedCategoryId = selectedCategoryId;
                model.CategoryModel.SelectedCategoryType = lastCategory.CategoryType;
                model.CategoryModel.SelectedCategoryName = lastCategory.CategoryName;
                if (!string.IsNullOrEmpty(lastCategory.CategoryContentTitle))
                    model.CategoryModel.SelectedCategoryContentTitle = lastCategory.CategoryContentTitle;
                else
                    model.CategoryModel.SelectedCategoryContentTitle = lastCategory.CategoryName;
                var category = _categoryService.GetCategoryByCategoryId(selectedCategoryId);
                model.CategoryModel.SelectedCategoryDescription = category.Description;
            }

            foreach (var item in topCategories)
            {
                string categoryUrl = string.Empty;
                string categoryNameUrl = (!string.IsNullOrEmpty(item.CategoryContentTitle)) ? item.CategoryContentTitle : item.CategoryName;

                if (item.CategoryType == (byte)CategoryType.Sector || item.CategoryType == (byte)CategoryType.ProductGroup || item.CategoryType == (byte)CategoryType.Category)
                {

                    categoryUrl = UrlBuilder.GetCategoryUrl(item.CategoryId, categoryNameUrl, null, "");
                    categoryUrl = UrlBuilder.GetFilterUrl(categoryUrl, filterParams);
                }
                else if (item.CategoryType == (byte)CategoryType.Brand)
                {
                    var topCategory = topCategories.LastOrDefault(k => k.CategoryType == (byte)CategoryType.Category);
                    if (topCategory == null)
                    {
                        topCategory = topCategories.LastOrDefault(k => k.CategoryType == (byte)CategoryType.ProductGroup);

                    }
                    categoryNameUrl = (!string.IsNullOrEmpty(topCategory.CategoryContentTitle)) ? topCategory.CategoryContentTitle : topCategory.CategoryName;

                    categoryUrl = UrlBuilder.GetCategoryUrl(topCategory.CategoryId, categoryNameUrl, item.CategoryId,
                            item.CategoryName);
                    categoryUrl = UrlBuilder.GetFilterUrl(categoryUrl, filterParams);
                }
                else if (item.CategoryType == (byte)CategoryType.Model)
                {
                    var brand = topCategories.LastOrDefault(k => k.CategoryType == (byte)CategoryType.Brand);
                    var topCategory = topCategories.LastOrDefault(k => k.CategoryType == (byte)CategoryType.Category);

                    if (topCategory == null)
                    {
                        topCategory = topCategories.LastOrDefault(k => k.CategoryType == (byte)CategoryType.ProductGroup);

                    }


                    if (brand != null)
                    {
                        categoryUrl = UrlBuilder.GetModelUrl(item.CategoryId, categoryNameUrl, brand.CategoryName,
          topCategory.CategoryName, selectedCategoryId);

                        categoryUrl = UrlBuilder.GetFilterUrl(categoryUrl, filterParams);
                    }


                }
                else if (item.CategoryType == (byte)CategoryType.Series)
                {
                    var brand = topCategories.LastOrDefault(k => k.CategoryType == (byte)CategoryType.Brand);
                    var topCategory = topCategories.LastOrDefault(k => k.CategoryType == (byte)CategoryType.Category);
                    if (topCategory == null)
                    {
                        topCategory = topCategories.LastOrDefault(k => k.CategoryType == (byte)CategoryType.ProductGroup);

                    }
                    categoryUrl = UrlBuilder.GetSerieUrl(item.CategoryId, categoryNameUrl, brand.CategoryName, topCategory.CategoryName);
                    categoryUrl = UrlBuilder.GetFilterUrl(categoryUrl, filterParams);
                }

                if (!string.IsNullOrEmpty(categoryUrl))
                {
                    var categoryItemModel = new MTCategoryItemModel
                    {
                        CategoryId = item.CategoryId,
                        CategoryName = item.CategoryName,
                        CategoryParentId = item.CategoryParentId,
                        CategoryType = item.CategoryType,
                        CategoryUrl = categoryUrl,
                        DefaultCategoryName = item.CategoryName,
                        ProductCount = item.ProductCount ?? 0,
                        TruncatedCategoryName = StringHelper.Truncate(item.CategoryName, 100),
                        CategoryContentTitle = !string.IsNullOrEmpty(item.CategoryContentTitle) ? item.CategoryContentTitle : item.CategoryName
                    };

                    model.CategoryModel.TopCategoryItemModels.Add(categoryItemModel);
                }

            }

        }

        private void PrepareCategoryProductCategoryModel(MTProductCategoryModel model, string searchText)
        {
            int selectedCategoryId = GetCategoryIdRouteData();
            int countryId = GetCountryIdQueryString();
            int cityId = GetCityIdQueryString();
            int localityId = GetLocalityIdQueryString();
            int brandId = GetBrandIdRouteData();
            int modelId = GetModelIdRoutData();
            int seriesId = GetSeriesIdRoutData();
            string customFilter = GetCustomFilterQueryString();
            string searchType = GetSearchTypeQueryString();

            var filterParams = GetCategoryFilterParameters(searchText, customFilter, countryId, cityId, localityId);
            var topCategories = _categoryService.GetSPTopCategories(selectedCategoryId);

            if (topCategories.Count > 0)
            {
                var lastCategory = topCategories.LastOrDefault();
                model.SelectedCategoryId = selectedCategoryId;
                model.SelectedCategoryType = lastCategory.CategoryType;
                model.SelectedCategoryName = lastCategory.CategoryName;

                var category = _categoryService.GetCategoryByCategoryId(selectedCategoryId);
                model.SelectedCategoryDescription = category.Description;
            }
            foreach (var item in topCategories)
            {
                string categoryUrl = string.Empty;
                string categoryNameUrl = (!string.IsNullOrEmpty(item.CategoryContentTitle)) ? item.CategoryContentTitle : item.CategoryName;

                if (item.CategoryType == (byte)CategoryType.Sector || item.CategoryType == (byte)CategoryType.ProductGroup || item.CategoryType == (byte)CategoryType.Category)
                {

                    categoryUrl = UrlBuilder.GetCategoryUrl(item.CategoryId, categoryNameUrl, null, "");
                    categoryUrl = UrlBuilder.GetFilterUrl(categoryUrl, filterParams);
                }
                else if (item.CategoryType == (byte)CategoryType.Brand)
                {
                    var topCategory = topCategories.LastOrDefault(k => k.CategoryType == (byte)CategoryType.Category);
                    categoryNameUrl = (!string.IsNullOrEmpty(topCategory.CategoryContentTitle)) ? topCategory.CategoryContentTitle : item.CategoryName;

                    categoryUrl = UrlBuilder.GetCategoryUrl(topCategory.CategoryId, categoryNameUrl, item.CategoryId,
                            item.CategoryName);
                    categoryUrl = UrlBuilder.GetFilterUrl(categoryUrl, filterParams);
                }
                else if (item.CategoryType == (byte)CategoryType.Model)
                {
                    var brand = topCategories.LastOrDefault(k => k.CategoryType == (byte)CategoryType.Brand);
                    var topCategory = topCategories.LastOrDefault(k => k.CategoryType == (byte)CategoryType.Category);


                    categoryUrl = UrlBuilder.GetModelUrl(item.CategoryId, categoryNameUrl, brand.CategoryName,
                            topCategory.CategoryName, selectedCategoryId);
                    categoryUrl = UrlBuilder.GetFilterUrl(categoryUrl, filterParams);

                }
                else if (item.CategoryType == (byte)CategoryType.Series)
                {
                    var brand = topCategories.LastOrDefault(k => k.CategoryType == (byte)CategoryType.Brand);
                    var kat = topCategories.LastOrDefault(k => k.CategoryType == (byte)CategoryType.Category);

                    categoryUrl = UrlBuilder.GetSerieUrl(item.CategoryId, categoryNameUrl, brand.CategoryName, kat.CategoryName);
                    categoryUrl = UrlBuilder.GetFilterUrl(categoryUrl, filterParams);
                }

                var categoryItemModel = new MTCategoryItemModel
                {
                    CategoryId = item.CategoryId,
                    CategoryName = item.CategoryName,
                    CategoryParentId = item.CategoryParentId,
                    CategoryType = item.CategoryType,
                    CategoryUrl = categoryUrl,
                    DefaultCategoryName = item.CategoryName,
                    ProductCount = item.ProductCount ?? 0,
                    TruncatedCategoryName = StringHelper.Truncate(item.CategoryName, 100),
                    CategoryContentTitle = !string.IsNullOrEmpty(item.CategoryContentTitle) ? item.CategoryContentTitle : item.CategoryName
                };
                model.TopCategoryItemModels.Add(categoryItemModel);
            }

            var productCategories = _categoryService.GetSPProductCategoryForSearchProduct(searchText, selectedCategoryId, brandId, modelId, seriesId, 0, countryId, cityId, localityId).OrderBy(x => x.CategoryName);
            foreach (var item in productCategories)
            {
                string categoryNameUrl = (!string.IsNullOrEmpty(item.CategoryContentTitle)) ? item.CategoryContentTitle : item.CategoryName;

                string categoryUrl = UrlBuilder.GetCategoryUrl(item.CategoryId, categoryNameUrl, null, null);

                if (brandId > 0)
                {
                    var brandCategory = _categoryService.GetCategoryByCategoryId(brandId);
                    categoryUrl = UrlBuilder.GetCategoryUrl(item.CategoryId, categoryNameUrl, brandCategory.CategoryId, brandCategory.CategoryName);
                }

                var categoryItemModel = new MTProductCategoryItemModel
                {
                    CategoryId = item.CategoryId,
                    CategoryType = (byte)item.CategoryType,
                    CategoryName = item.CategoryName,
                    CategoryUrl = UrlBuilder.GetFilterUrl(categoryUrl, filterParams),
                    ProductCount = item.ProductCount == 0 ? 0 : item.ProductCount,
                    TruncatedCategoryName = StringHelper.Truncate(item.CategoryName, 100)

                };

            }

        }

        private void PrepareCategoryProductNavigation(MTProductCategoryModel model)
        {
            string navigationUrl = "";
            IList<Navigation> alMenu = new List<Navigation>();
            IList<Navigation> alMenuSecond = new List<Navigation>();
            alMenu.Add(new Navigation("Ana Sayfa", "/", Navigation.TargetType._self));

            alMenuSecond.Add(new Navigation("Ana Sayfa", "/", Navigation.TargetType._self));

            int selectedCategoryId = 0;
            if (GetSeriesIdRoutData() != 0)
            {
                selectedCategoryId = GetSeriesIdRoutData();
            }
            else if (GetModelIdRoutData() != 0)
            {
                selectedCategoryId = GetModelIdRoutData();
            }
            else if (GetBrandIdRouteData() != 0)
            {
                selectedCategoryId = GetBrandIdRouteData();
            }
            else
            {
                selectedCategoryId = model.SelectedCategoryId;
            }

            var topCategories = _categoryService.GetSPTopCategories(selectedCategoryId);
            foreach (var item in topCategories)
            {
                string categoryUrl = "";
                string categoryNameUrl = !string.IsNullOrEmpty(item.CategoryContentTitle) ? item.CategoryContentTitle : item.CategoryName;
                if (item.CategoryType == (byte)CategoryType.Sector || item.CategoryType == (byte)CategoryType.ProductGroup || item.CategoryType == (byte)CategoryType.Category)
                {
                    categoryUrl = UrlBuilder.GetCategoryUrl(item.CategoryId, categoryNameUrl, null, "");
                }
                else if (item.CategoryType == (byte)CategoryType.Brand)
                {
                    var topCategory = topCategories.LastOrDefault(k => k.CategoryType == (byte)CategoryType.Category);
                    if (topCategory == null)
                        topCategory = topCategories.LastOrDefault(k => k.CategoryType == (byte)CategoryType.ProductGroup);

                    categoryNameUrl = (!string.IsNullOrEmpty(topCategory.CategoryContentTitle)) ? topCategory.CategoryContentTitle : topCategory.CategoryName;
                    categoryUrl = UrlBuilder.GetCategoryUrl(topCategory.CategoryId, categoryNameUrl, item.CategoryId, item.CategoryName);


                }
                else if (item.CategoryType == (byte)CategoryType.Model)
                {
                    var brand = topCategories.LastOrDefault(k => k.CategoryType == (byte)CategoryType.Brand);
                    var topCategory = topCategories.LastOrDefault(k => k.CategoryType == (byte)CategoryType.Category);
                    if (topCategory == null)
                        topCategory = topCategories.LastOrDefault(k => k.CategoryType == (byte)CategoryType.ProductGroup);
                    if(brand!=null)
                        categoryUrl = UrlBuilder.GetModelUrl(item.CategoryId, categoryNameUrl, brand.CategoryName, topCategory.CategoryName, selectedCategoryId);


                }
                else if (item.CategoryType == (byte)CategoryType.Series)
                {
                    var brand = topCategories.LastOrDefault(k => k.CategoryType == (byte)CategoryType.Brand);
                    var topCategory = topCategories.LastOrDefault(k => k.CategoryType == (byte)CategoryType.Category);
                    if (topCategory == null)
                        topCategory = topCategories.LastOrDefault(k => k.CategoryType == (byte)CategoryType.ProductGroup);

                    categoryUrl = UrlBuilder.GetSerieUrl(item.CategoryId, categoryNameUrl, brand.CategoryName, topCategory.CategoryName);
                }
                if (!string.IsNullOrEmpty(categoryUrl))
                {
                    navigationUrl = categoryUrl.Replace(":443", "");
                    var categoryName = !string.IsNullOrEmpty(item.CategoryContentTitle) ? item.CategoryContentTitle : item.CategoryName;
                    alMenu.Add(new Navigation(categoryName, navigationUrl, Navigation.TargetType._self));
                    string navigationUrl1 = categoryUrl.Replace(":443", "").Replace(AppSettings.SiteUrlWithoutLastSlash, "");
                    alMenuSecond.Add(new Navigation(categoryNameUrl, navigationUrl1, Navigation.TargetType._self));
                }


            }


            model.MtJsonLdModel.Navigations = alMenuSecond;
            model.Navigation = LoadNavigationV3(alMenu);
        }
        private void PrepareJsonLd(MTCategoryProductViewModel model)
        {
            try
            {
                var webPage = new WebPage
                {
                    Name = model.CategoryModel.SelectedCategoryName.CheckNullString(),
                    InLanguage = "tr-TR",
                    Url = Request.Url,
                    Description = model.SeoModel.Description.CheckNullString(),
                    Breadcrumb = JsonLdHelper.SetBreadcrumbList(model.CategoryModel.MtJsonLdModel.Navigations)
                };


                model.CategoryModel.MtJsonLdModel.JsonLdString = webPage.ToString();
            }
            catch (Exception e)
            {
                model.CategoryModel.MtJsonLdModel.JsonLdString = string.Empty;
            }

        }

        private void PrepareCategoryStoreModel(MTCategoryProductViewModel model)
        {
            try
            {
                Category categoryAndBrand = null;
                int selectedBrandId = GetBrandIdRouteData();
                int selectedCategoryId = GetCategoryIdRouteData();
                if (selectedBrandId > 0)
                    categoryAndBrand = _categoryService.GetCategoryByCategoryId(selectedBrandId);
                else
                    categoryAndBrand = _categoryService.GetCategoryByCategoryId(selectedCategoryId);

                int selectedCountryId = GetCountryIdRouteData();
                int selectedCityId = GetCityIdRouteData();
                int selectedLocalityId = GetLocalityIdRouteData();

                IList<StoreForCategoryResult> categoryStores = _storeService.GetSPStoreForCategorySearch(selectedCategoryId, selectedBrandId, selectedCountryId, selectedCityId, selectedLocalityId).OrderBy(x => x.MainPartyId).Skip(0).Take(8).ToList();

                model.StoreModel.SelectedCategoryId = categoryAndBrand.CategoryId;
                model.StoreModel.SelectedCategoryType = categoryAndBrand.CategoryType.Value;
                model.StoreModel.SelectedCategoryName = categoryAndBrand.CategoryName;
                if (!string.IsNullOrEmpty(categoryAndBrand.CategoryContentTitle))
                    model.StoreModel.SelectedCategoryName = categoryAndBrand.CategoryContentTitle;

                List<int?> Liste = categoryStores.Select(x => (int?)x.MainPartyId).ToList();
                var StoreListesi = _storeService.GetStoresByMainPartyIds(Liste).OrderBy(x => x.storerate).ToList();
                foreach (var item in categoryStores)
                {
                    string storeUrlName = StoreListesi.FirstOrDefault(x => x.MainPartyId == item.MainPartyId).StoreUrlName;
                    model.StoreModel.StoreItemModes.Add(new MTCategoryStoreItemModel
                    {
                        MainPartyId = item.MainPartyId,
                        StoreName = item.StoreName,
                        StoreRate = item.StoreRate,
                        StoreProductCategoryUrl = UrlBuilder.GetStoreProfileProductCategoryUrl(categoryAndBrand.CategoryId, categoryAndBrand.CategoryName, storeUrlName),
                        StoreUrl = UrlBuilder.GetStoreProfileUrl(item.MainPartyId, item.StoreName, storeUrlName),
                        PictureLogoPath = ImageHelpers.GetStoreImage(item.MainPartyId, item.StoreLogo, "120")
                    });
                }
                string storePageTitle = "";
                if (!string.IsNullOrEmpty(categoryAndBrand.StorePageTitle))
                {
                    if (categoryAndBrand.StorePageTitle.Contains("Firma"))
                    {
                        storePageTitle = FormatHelper.GetCategoryNameWithSynTax(categoryAndBrand.StorePageTitle, CategorySyntaxType.CategoryNameOnyl);
                    }
                    else
                    {
                        storePageTitle = FormatHelper.GetCategoryNameWithSynTax(categoryAndBrand.StorePageTitle, CategorySyntaxType.Store);

                    }
                }
                else if (!string.IsNullOrEmpty(categoryAndBrand.CategoryContentTitle))
                    storePageTitle = FormatHelper.GetCategoryNameWithSynTax(categoryAndBrand.CategoryContentTitle, CategorySyntaxType.Store);
                else
                    storePageTitle = FormatHelper.GetCategoryNameWithSynTax(categoryAndBrand.CategoryName, CategorySyntaxType.Store);

                model.StoreModel.StoreCategoryUrl = UrlBuilder.GetStoreCategoryUrl(categoryAndBrand.CategoryId, storePageTitle);
            }
            catch
            {

            }


        }

        private void PrepareSortingModel(MTProductFilteringModel model)
        {
            string selectedOrder = GetOrderQueryString();
            string orderByUrl = QueryStringBuilder.RemoveQueryString(this.Request.Url.ToString(), PAGE_INDEX_QUERY_STRING_KEY);

            model.SortOptionModels.Add(new SortOptionModel
            {
                SortId = 0,
                SortOptionName = "a-Z",
                SortOptionUrl = QueryStringBuilder.ModifyQueryString(orderByUrl, ORDER_QUERY_STRING_KEY + "=a-z", null),
                Selected = (selectedOrder == "a-z")
            });

            model.SortOptionModels.Add(new SortOptionModel
            {
                SortId = 1,
                SortOptionName = "Z-a",
                SortOptionUrl = QueryStringBuilder.ModifyQueryString(orderByUrl, ORDER_QUERY_STRING_KEY + "z-a", null),
                Selected = (selectedOrder == "z-a")
            });

            model.SortOptionModels.Add(new SortOptionModel
            {
                SortId = 2,
                SortOptionName = "Son Eklenen",
                SortOptionUrl = QueryStringBuilder.ModifyQueryString(orderByUrl, ORDER_QUERY_STRING_KEY + "=soneklenen", null),
                Selected = (selectedOrder == "soneklenen")
            });

            model.SortOptionModels.Add(new SortOptionModel
            {
                SortId = 3,
                SortOptionName = "En Çok Görünütülenen",
                SortOptionUrl = QueryStringBuilder.ModifyQueryString(orderByUrl, ORDER_QUERY_STRING_KEY + "=encokgoruntulenen", null),
                Selected = (selectedOrder == "encokgoruntulenen")
            });
        }

        private void PrepareCustomFilterModel(MTProductFilteringModel model, int newProductCount, int usedProductCount, int serviceProductCount)
        {
            string searchText = GetSearchTextQueryString();
            string selectedCustomFilter = GetSearchTypeQueryString();
            string customFilterUrl = QueryStringBuilder.RemoveQueryString(this.Request.Url.ToString(), PAGE_INDEX_QUERY_STRING_KEY);
            if (!string.IsNullOrEmpty(searchText))
            {
                customFilterUrl = QueryStringBuilder.RemoveQueryString(customFilterUrl, SEARCH_TEXT_QUERY_STRING_KEY);
                customFilterUrl = QueryStringBuilder.ModifyQueryString(customFilterUrl, SEARCH_TEXT_QUERY_STRING_KEY + "=" + searchText, null);
            }

            model.CustomFilterModels.Add(new CustomFilterModel
            {
                FilterId = 0,
                FilterName = "Tümü",
                FilterUrl = QueryStringBuilder.ModifyQueryString(customFilterUrl, CUSTOM_FILTER_QUERY_STRING_KEY + "=tumu", null),
                Selected = (selectedCustomFilter == "" || selectedCustomFilter == "tumu" || selectedCustomFilter == "0")

            });

            model.CustomFilterModels.Add(new CustomFilterModel
            {
                FilterId = 1,
                FilterName = "Sıfır",
                FilterUrl = QueryStringBuilder.ModifyQueryString(customFilterUrl, CUSTOM_FILTER_QUERY_STRING_KEY + "=sifir", null),
                ProductCount = newProductCount,
                Selected = (selectedCustomFilter == "sifir" || selectedCustomFilter == "72")
            });

            model.CustomFilterModels.Add(new CustomFilterModel
            {
                FilterId = 2,
                FilterName = "İkinci El",
                ProductCount = usedProductCount,
                FilterUrl = QueryStringBuilder.ModifyQueryString(customFilterUrl, CUSTOM_FILTER_QUERY_STRING_KEY + "=ikinciel", null),
                Selected = (selectedCustomFilter == "ikinciel" || selectedCustomFilter == "73")
            });

            model.CustomFilterModels.Add(new CustomFilterModel
            {
                FilterId = 3,
                FilterName = "Hizmet",
                ProductCount = serviceProductCount,
                FilterUrl = QueryStringBuilder.ModifyQueryString(customFilterUrl, CUSTOM_FILTER_QUERY_STRING_KEY + "=hizmet", null),
                Selected = (selectedCustomFilter == "hizmet" || selectedCustomFilter == "201")
            });
        }

        private void PreparePagingModel(MTProductPagingModel model, int totalItemCount)
        {
            int pageSize = GetPageSizeQueryString();
            model.PageSize = pageSize;
            if (totalItemCount > pageSize)
            {
                int pageIndex = GetPageQueryString();
                model.CurrentPageIndex = pageIndex;
                if (totalItemCount % model.PageSize == 0)
                {
                    model.TotalPageCount = totalItemCount / pageSize;
                }
                else
                {
                    model.TotalPageCount = (totalItemCount / pageSize) + 1;
                }

                int firstPage = model.CurrentPageIndex >= 5 ? model.CurrentPageIndex - 4 : 1;
                int lastPage = firstPage + 8;
                if (lastPage >= model.TotalPageCount)
                {
                    lastPage = model.TotalPageCount;
                }

                model.FirstPageUrl = QueryStringBuilder.RemoveQueryString(this.Request.Url.ToString(), "page");
                model.LastPageUrl = QueryStringBuilder.ModifyQueryString(this.Request.Url.ToString(), PAGE_INDEX_QUERY_STRING_KEY + "=" + model.TotalPageCount, null);

                for (int i = firstPage; i <= lastPage; i++)
                {
                    model.PageUrls.Add(i, QueryStringBuilder.ModifyQueryString(this.Request.Url.ToString(), PAGE_INDEX_QUERY_STRING_KEY + "=" + i, null));

                }
            }
        }

        private void PrepareMostViewProductModel(MTCategoryProductViewModel model)
        {

            int selectedCategoryId = GetCategoryIdRouteData();
            var mostViewedProductModel = new MTMostViewedProductModel();
            mostViewedProductModel.SelectedCategoryName = model.CategoryModel.SelectedCategoryName;
            var products = _productService.GetSPMostViewProductsByCategoryId(selectedCategoryId);
            int mostViewProductIndex = 1;
            foreach (var item in products)
            {
                mostViewedProductModel.ProductItemModels.Add(new MTMostViewedProductItemModel
                {
                    ProductId = item.ProductId,
                    BrandName = string.IsNullOrEmpty(item.BrandName) ? string.Empty : item.BrandName,
                    Index = mostViewProductIndex,
                    ModelName = string.IsNullOrEmpty(item.ModelName) ? string.Empty : item.ModelName,
                    ProductName = item.ProductName,
                    ProductUrl = UrlBuilder.GetProductUrl(item.ProductId, item.ProductName),
                    SmallPictureName = StringHelper.Truncate(item.ProductName, 80),
                    SmallPicturePah = ImageHelper.GetProductImagePath(item.ProductId, item.MainPicture, ProductImageSize.px200x150),
                    CurrencyCss = item.GetCurrencyCssName(),
                    Price = item.GetFormattedPrice(),
                    ProductContactUrl = UrlBuilder.GetProductContactUrl(item.ProductId, item.StoreName),
                    StoreName = item.StoreName

                });
                mostViewProductIndex++;
            }
            int remindsProductNumber = 13 - mostViewProductIndex;
            if (remindsProductNumber != 0)
            {
                GetRemindMostViewProducts(mostViewedProductModel, remindsProductNumber, mostViewProductIndex);
            }
            model.MostViewedProductModel = mostViewedProductModel;
        }
        public void GetRemindMostViewProducts(MTMostViewedProductModel model, int remindsProductNumber, int productIndex)
        {
            int selectedCategoryId = GetCategoryIdRouteData();
            bool sameProduct = false;
            var topCategories = _categoryService.GetSPTopCategories(selectedCategoryId).Where(c => c.CategoryId != selectedCategoryId).OrderBy(c => c.CategoryOrderId);
            foreach (var item in topCategories)
            {
                var products = _productService.GetSPMostViewProductsByCategoryIdRemind(item.CategoryId, remindsProductNumber, selectedCategoryId);
                foreach (var item1 in products.ToList())
                {
                    if (model.ProductItemModels.Any(x => x.ProductName == item1.ProductName))
                    {
                        sameProduct = true;
                        break;

                    }
                    model.ProductItemModels.Add(new MTMostViewedProductItemModel
                    {
                        BrandName = string.IsNullOrEmpty(item1.BrandName) ? string.Empty : item1.BrandName,
                        Index = productIndex,
                        ModelName = string.IsNullOrEmpty(item1.ModelName) ? string.Empty : item1.ModelName,
                        ProductName = item1.ProductName,
                        ProductUrl = UrlBuilder.GetProductUrl(item1.ProductId, item1.ProductName),
                        SmallPictureName = StringHelper.Truncate(item1.ProductName, 80),
                        SmallPicturePah = ImageHelper.GetProductImagePath(item1.ProductId, item1.MainPicture, ProductImageSize.x160x120),
                        CurrencyCss = item1.GetCurrencyCssName(),
                        Price = item1.GetFormattedPrice(),
                        ProductContactUrl = UrlBuilder.GetProductContactUrl(item1.ProductId, item1.StoreName),
                        StoreName = item1.StoreName
                    });
                    productIndex++;
                    if (productIndex == 13)
                        break;
                }
                if (products.Count > 0 && sameProduct == false)
                {
                    if (model.ProductItemModels.Count < 12)
                    {
                        int remindProductsNumber = 12 - model.ProductItemModels.ToList().Count;
                        GetRemindMostViewProducts(model, remindProductsNumber, productIndex);
                        break;
                    }
                    else
                        break;
                }
            }

        }
        [HttpGet]
        public ActionResult ProductSearchOneStep()
        {

            string searchText = FormatHelper.GetSearchText(GetSearchTextQueryString());
            if (string.IsNullOrEmpty(searchText))
            {
                return RedirectToActionPermanent("Error", "Home");
            }

            //SeoPageType = (byte)PageType.ProductSearchPage;
            //CreateSeoParameter(SeoModel.SeoProductParemeters.ArananKelime, searchText);


            var modelCategoryOneStep = new MTCategorySearchModel();



            if (searchText.Length >= 3)
            {

                string keyword = new CultureInfo("tr-TR").TextInfo.ToTitleCase(searchText.Trim());
                var searchScore = _searchScoreService.GetSearchScoreByKeyword(keyword);
                if (searchScore != null)
                {
                    searchScore.Score += 1;
                    searchScore.UpdateDate = DateTime.Now;
                    _searchScoreService.UpdateSearchScore(searchScore);
                }
                else
                {
                    searchScore = new global::MakinaTurkiye.Entities.Tables.Searchs.SearchScore
                    {
                        CreateDate = DateTime.Now,
                        Keyword = keyword,
                        Score = 1,
                        UpdateDate = DateTime.Now
                    };
                    _searchScoreService.InsertSearchScore(searchScore);
                }
                modelCategoryOneStep.SearchText = searchText;
                //product category
                PrepareSearchProductCategoryModelOneStep(modelCategoryOneStep, searchText);
                if (modelCategoryOneStep.CategoryModel.TopCategoryItemModels.ToList().Count == 1)
                {
                    return Redirect(modelCategoryOneStep.CategoryModel.TopCategoryItemModels.First().CategoryUrl);

                }
            }
            return View(modelCategoryOneStep);
        }

        public void PrepareSearchProductCategoryModelOneStep(MTCategorySearchModel model, string searchText)
        {

            var productCategories = _categoryService.GetSPProductCategoryForSearchProductOneStep(searchText);
            var topCategories = productCategories.Where(x => x.CategoryType == 0);

            foreach (var item in topCategories)
            {
                int categoryId = item.CategoryId;

                if (categoryId > 0)
                {
                    string categoryNameUrl = !string.IsNullOrEmpty(item.CategoryContentTitle) ? item.CategoryContentTitle : item.CategoryName;
                    //string categoryUrl = QueryStringBuilder.RemoveQueryString(Request.Url.ToString().Replace("/kelime-arama", ""), PAGE_INDEX_QUERY_STRING_KEY);
                    //categoryUrl = categoryUrl.Insert(categoryUrl.IndexOf("?"), UrlBuilder.GetCategoryUrl(item.CategoryId, categoryNameUrl, null, ""));
                    string categoryUrl = UrlBuilder.GetCategoryUrl(item.CategoryId, categoryNameUrl, null, "", searchText);

                    //string urlCat = UrlBuilder.GetCategoryUrl(item.CategoryId, categoryNameUrl, null, string.Empty);

                    var categoryItemModel = new MTCategoryItemModel
                    {
                        CategoryId = item.CategoryId,
                        CategoryName = item.CategoryName,
                        CategoryUrl = categoryUrl,
                        DefaultCategoryName = item.CategoryName,
                        ProductCount = item.ProductCount,
                        CategoryContentTitle = !string.IsNullOrEmpty(item.CategoryContentTitle) ? item.CategoryContentTitle : item.CategoryName
                    };

                    if (string.IsNullOrEmpty(item.CategoryIcon))
                    {
                        categoryItemModel.CategoryIcon = "/Content/RibbonImages/BlogCategory.png";
                    }
                    else
                    {
                        categoryItemModel.CategoryIcon = AppSettings.CategoryIconImageFolder + item.CategoryIcon;
                    }
                    model.CategoryModel.TopCategoryItemModels.Add(categoryItemModel);

                    var productGroups = productCategories.Where(x => x.CategoryParentId == item.CategoryId && x.CategoryType == (byte)CategoryTypeEnum.ProductGroup);

                    foreach (var item2 in productGroups)
                    {
                        string categoryNameUrl1 = !string.IsNullOrEmpty(item2.CategoryContentTitle) ? item2.CategoryContentTitle : item2.CategoryName;

                        string categoryUrlCustom = UrlBuilder.GetCategoryUrl(item2.CategoryId, categoryNameUrl1, null, "", searchText);

                        //string urlCat1 = UrlBuilder.GetCategoryUrl(item.CategoryId, categoryNameUrl, null, string.Empty);
                        var categoryItemModel1 = new MTProductSearchOneStepCategoryItemModel
                        {
                            CategoryId = item2.CategoryId,
                            CategoryParentId = item.CategoryId,
                            CategoryName = item2.CategoryName,
                            CategoryUrl = categoryUrlCustom,
                            ProductCount = item2.ProductCount
                        };
                        model.CategoryModel.SubCategories.Add(categoryItemModel1);
                    }
                }
            }
            model.TotalCount = productCategories.Where(x => x.CategoryType == 0).Sum(x => x.ProductCount);

        }
        public ActionResult SearchResults()
        {
            string searchText = FormatHelper.GetSearchText(GetSearchTextQueryString());

            if (string.IsNullOrEmpty(searchText))
            {
                return RedirectToActionPermanent("Error", "Home");
            }

            //SeoPageType = (byte)PageType.ProductSearchPage;
            //CreateSeoParameter(SeoModel.SeoProductParemeters.ArananKelime, searchText);

            var model = new MTSearchProductViewModel();
            if (searchText.Length >= 3)
            {
                model.SearchText = searchText;
                //product category
                PrepareSearchProductCategoryModel(model);
                //navigation
                PrepareSearchNavigation(model);

                int categoryId = GetCategoryIdQueryString();
                int pageIndex = GetPageQueryString();
                string customFilter = GetCustomFilterQueryString();

                int customFilterId = 0;
                switch (customFilter)
                {
                    case "sifir": customFilterId = 1; break;
                    case "ikinciel": customFilterId = 2; break;
                    default: customFilterId = 0; break;
                }
                int pageSize = GetPageSizeQueryString();
                var products = _productService.SPWebSearch(searchText, categoryId, customFilterId, pageIndex, pageSize);
                model.FilteringContext.TotalItemCount = products.TotalCount;

                //products
                PrepareSearchProductModel(products, model);
                //paging
                PreparePagingModel(model.PagingModel, products.TotalCount);
                //custom filter
                PrepareCustomFilterModel(model.FilteringContext, -1, -1, -1);
            }
            return View(model);

        }


        private void PrepareSearchProductCategoryModel(MTSearchProductViewModel model)
        {
            int categoryId = model.CategoryModel.SelectedCategoryId;
            string searchText = GetSearchTextQueryString();
            string customFilter = GetCustomFilterQueryString();
            int customFilterId = 0;
            switch (customFilter)
            {
                case "sifir": customFilterId = 1; break;
                case "ikinciel": customFilterId = 2; break;
                default: customFilterId = 0; break;
            }

            if (categoryId > 0)
            {
                var topCategories = _categoryService.GetSPTopCategories(categoryId);
                if (topCategories.Count > 0)
                {
                    var lastCategory = topCategories.LastOrDefault();
                    model.CategoryModel.SelectedCategoryId = categoryId;
                    model.CategoryModel.SelectedCategoryType = lastCategory.CategoryType;
                    model.CategoryModel.SelectedCategoryName = lastCategory.CategoryName;
                }

                foreach (var item in topCategories)
                {
                    string categoryUrl = Request.Url.ToString().Replace(Request.Url.AbsolutePath, "");
                    var categoryNameUrl = !string.IsNullOrEmpty(item.CategoryContentTitle) ? item.CategoryContentTitle : item.CategoryName;
                    categoryUrl = categoryUrl.Insert(categoryUrl.IndexOf("?"),
                        UrlBuilder.GetCategoryUrl(item.CategoryId, categoryNameUrl, null, ""));

                    var categoryItemModel = new MTCategoryItemModel
                    {
                        CategoryId = item.CategoryId,
                        CategoryName = item.CategoryName,
                        CategoryParentId = item.CategoryParentId,
                        CategoryType = item.CategoryType,
                        CategoryUrl = categoryUrl,
                        DefaultCategoryName = item.CategoryName,
                        ProductCount = item.ProductCount ?? 0,
                        CategoryContentTitle = !string.IsNullOrEmpty(item.CategoryContentTitle) ? item.CategoryContentTitle : item.CategoryName
                    };
                    model.CategoryModel.TopCategoryItemModels.Add(categoryItemModel);
                }
            }

            var productCategories = _categoryService.GetSPProductCategoryForSearchProduct(searchText, categoryId);
            foreach (var item in productCategories)
            {
                var categoryNameUrl = !string.IsNullOrEmpty(item.CategoryContentTitle) ? item.CategoryContentTitle : item.CategoryName;

                string categoryUrl = Request.Url.ToString().Replace(Request.Url.Segments[1], "");
                categoryUrl = categoryUrl.Insert(categoryUrl.IndexOf("/kelime-arama"),
                    UrlBuilder.GetCategoryUrl(item.CategoryId, categoryNameUrl, null, ""));

                var categoryItemModel = new MTProductCategoryItemModel
                {
                    CategoryId = item.CategoryId,
                    CategoryName = item.CategoryName,
                    CategoryUrl = categoryUrl,
                    ProductCount = item.ProductCount
                };
                model.CategoryModel.CategoryItemModels.Add(categoryItemModel);
            }

        }

        private void PrepareSearchProductCategoryModel(MTProductCategoryModel model, string searchText)
        {
            int categoryId = GetCategoryIdQueryString();
            string customFilter = GetCustomFilterQueryString();
            int customFilterId = 0;
            switch (customFilter)
            {
                case "sifir": customFilterId = 1; break;
                case "ikinciel": customFilterId = 2; break;
                default: customFilterId = 0; break;
            }

            if (categoryId > 0)
            {
                var topCategories = _categoryService.GetSPTopCategories(categoryId);
                if (topCategories.Count > 0)
                {
                    var lastCategory = topCategories.LastOrDefault();
                    model.SelectedCategoryId = categoryId;
                    model.SelectedCategoryType = lastCategory.CategoryType;
                    model.SelectedCategoryName = lastCategory.CategoryName;
                }

                foreach (var item in topCategories)
                {
                    var categoryNameUrl = !string.IsNullOrEmpty(item.CategoryContentTitle) ? item.CategoryContentTitle : item.CategoryName;

                    string categoryUrl = Request.Url.ToString().Replace(Request.Url.Segments[1], "");
                    categoryUrl = categoryUrl.Insert(categoryUrl.IndexOf("?"),
                        UrlBuilder.GetCategoryUrl(item.CategoryId, categoryNameUrl, null, "").Replace("/", ""));

                    var categoryItemModel = new MTCategoryItemModel
                    {
                        CategoryId = item.CategoryId,
                        CategoryName = item.CategoryName,
                        CategoryParentId = item.CategoryParentId,
                        CategoryType = item.CategoryType,
                        CategoryUrl = categoryUrl,
                        DefaultCategoryName = item.CategoryName,
                        ProductCount = item.ProductCount ?? 0,
                        CategoryContentTitle = !string.IsNullOrEmpty(item.CategoryContentTitle) ? item.CategoryContentTitle : item.CategoryName
                    };
                    model.TopCategoryItemModels.Add(categoryItemModel);
                }
            }

            var productCategories = _categoryService.GetSPProductCategoryForSearchProduct(searchText, categoryId);
            foreach (var item in productCategories)
            {
                var categoryNameUrl = !string.IsNullOrEmpty(item.CategoryContentTitle) ? item.CategoryContentTitle : item.CategoryName;

                string categoryUrl = Request.Url.ToString().Replace(Request.Url.Segments[1], "");
                categoryUrl = categoryUrl.Insert(categoryUrl.IndexOf("?"),
                    UrlBuilder.GetCategoryUrl(item.CategoryId, categoryNameUrl, null, "").Replace("/", ""));

                var categoryItemModel = new MTProductCategoryItemModel
                {
                    CategoryId = item.CategoryId,
                    CategoryName = item.CategoryName,
                    CategoryUrl = categoryUrl,
                    ProductCount = item.ProductCount
                };
                model.CategoryItemModels.Add(categoryItemModel);
            }
        }

        private void PrepareSearchProductModel(IPagedList<WebSearchProductResult> products, MTSearchProductViewModel model)
        {
            if (products.Count > 0)
            {

                IList<MTSearchProductModel> searchProductModels = new List<MTSearchProductModel>();
                foreach (var item in products)
                {

                    var searchProductModel = new MTSearchProductModel
                    {
                        BrandId = item.BrandId,
                        BrandName = item.BrandName,
                        BriefDetailText = item.BriefDetailText,
                        CategoryId = item.CategoryId,
                        CategoryName = item.CategoryName,
                        CategoryProductGroupName = item.CategoryProductGroupName,
                        CategoryTreeName = item.CategoryTreeName,
                        CityName = item.CityName,
                        CurrencyName = item.CurrencyName,
                        FullSearxhName = item.FullSearxhName,
                        LocalityName = item.LocalityName,
                        MainPicture = item.MainPicture,
                        ModelId = item.ModelId,
                        ModelName = item.ModelName,
                        ModelYear = item.ModelYear,
                        ProductActive = item.ProductActive,
                        ProductActiveType = item.ProductActiveType,
                        ProductDescription = item.ProductDescription,
                        ProductId = item.ProductId,
                        ProductName = item.ProductName,
                        ProductNo = item.ProductNo,
                        ProductPrice = item.ProductPrice,
                        productrate = item.productrate,
                        ProductSalesTypeText = item.ProductSalesTypeText,
                        ProductStatuText = item.ProductStatuText,
                        ProductTypeText = item.ProductTypeText,
                        SeriesId = item.SeriesId,
                        StoreName = item.StoreName,
                    };

                    model.SearchProductModels.Add(searchProductModel);


                }

            }
        }

        private void PrepareSearchNavigation(MTSearchProductViewModel model)
        {
            string navigationUrl = QueryStringBuilder.RemoveQueryString(this.Request.Url.ToString(), PAGE_INDEX_QUERY_STRING_KEY);
            navigationUrl = QueryStringBuilder.RemoveQueryString(navigationUrl, SEARCH_TEXT_QUERY_STRING_KEY);
            navigationUrl = QueryStringBuilder.RemoveQueryString(navigationUrl, CATEGORY_ID_QUERY_STRING_KEY);
            navigationUrl = QueryStringBuilder.ModifyQueryString(navigationUrl, SEARCH_TEXT_QUERY_STRING_KEY + "=" + model.SearchText, null);

            IList<Navigation> alMenu = new List<Navigation>();
            alMenu.Add(new Navigation(model.SearchText, navigationUrl, Navigation.TargetType._self));

            navigationUrl = QueryStringBuilder.RemoveQueryString(this.Request.Url.ToString(), SEARCH_TEXT_QUERY_STRING_KEY);

            var topCategories = model.CategoryModel.TopCategoryItemModels;
            foreach (var item in topCategories)
            {
                if (item.CategoryType != (byte)CategoryTypeEnum.ProductGroup)
                {
                    navigationUrl = QueryStringBuilder.ModifyQueryString(navigationUrl, CATEGORY_ID_QUERY_STRING_KEY + "=" + item.CategoryId, null);
                    navigationUrl = QueryStringBuilder.ModifyQueryString(navigationUrl, SEARCH_TEXT_QUERY_STRING_KEY + "=" + model.SearchText, null);
                    alMenu.Add(new Navigation(item.CategoryName, navigationUrl, Navigation.TargetType._self));
                }
            }
            model.Navigation = LoadNavigationV2(alMenu);
        }

        public void PrepareSeoParams(MTCategoryProductViewModel model, string selectedCategoryId)
        {


            var brandId = GetBrandIdRouteData();
            if (brandId > 0)
            {
                //SeoPageSpecial = (int)brandId;
                var CategoryBottom = _categoryService.GetCategoryByCategoryId(brandId);
                //CreateSeoParameter(SeoModel.SeoProductParemeters.Category, model.CategoryModel.TopCategoryItemModels.LastOrDefault().CategoryName);
                //var categoryContentTitle = model.CategoryModel.TopCategoryItemModels.LastOrDefault().CategoryContentTitle;
                //CreateSeoParameter(SeoModel.SeoProductParemeters.KategoriBaslik, model.CategoryModel.TopCategoryItemModels.LastOrDefault().CategoryContentTitle);

                string categoryName = CategoryBottom.CategoryName;

                if (model.CategoryModel.SelectedCategoryType == (byte)CategoryType.ProductGroup)
                {
                    var categoryTops = _categoryService.GetCategoriesByCategoryParentId(GetCategoryIdRouteData()).Where(x => x.CategoryId != CategoryBottom.CategoryParentId);

                    foreach (var item in categoryTops)
                    {
                        var categoryAlt = _categoryService.GetCategoriesByCategoryParentId(item.CategoryId);
                        categoryAlt = categoryAlt.Where(x => x.CategoryName == CategoryBottom.CategoryName).ToList();
                        if (categoryAlt.Count > 0)
                        {

                            model.NoIndex = true;
                            categoryName = CategoryBottom.CategoryName + "(" + CategoryBottom.CategoryId + ")";
                            model.SameCategoryH1 = CategoryBottom.CategoryId.ToString();

                            break;
                        }
                        else categoryName = CategoryBottom.CategoryName;
                    }
                }

                model.SpesificBrandName = categoryName;
                //CreateSeoParameter(SeoModel.SeoProductParemeters.Brand, categoryName);


            }
            else
            {
                //SeoPageSpecial = (int)model.CategoryModel.SelectedCategoryId;
                //if (model.CategoryModel.SelectedCategoryType == (byte)CategoryType.Category || model.CategoryModel.SelectedCategoryType == (byte)CategoryType.Sector || model.CategoryModel.SelectedCategoryType == (byte)CategoryType.ProductGroup || model.CategoryModel.SelectedCategoryType == (byte)CategoryType.Model)
                //{

                //}
                //else if (model.CategoryModel.SelectedCategoryType == (byte)CategoryType.Brand)
                //{
                //    SeoPageSpecial = (int)model.CategoryModel.SelectedCategoryId;

                //    CreateSeoParameter(SeoModel.SeoProductParemeters.Category, model.CategoryModel.TopCategoryItemModels.LastOrDefault().CategoryName);
                //    CreateSeoParameter(SeoModel.SeoProductParemeters.KategoriBaslik, model.CategoryModel.TopCategoryItemModels.LastOrDefault().CategoryContentTitle);

                //    CreateSeoParameter(SeoModel.SeoProductParemeters.Brand, model.CategoryModel.SelectedCategoryName);

                //}
            }

            //try
            //{
            //    int categoryId = model.CategoryModel.SelectedCategoryId;
            //    if (GetBrandIdRouteData() != 0) categoryId = GetBrandIdRouteData();
            //    if (GetModelIdRoutData() != 0) categoryId = GetModelIdRoutData();
            //    if (GetSeriesIdRoutData() != 0) categoryId = GetSeriesIdRoutData();

            //    var seo = SeoModel.GeneralforAll(Convert.ToByte(ViewData["SEOPAGETYPE"]), categoryId, GetPageQueryString());

            //    if (seo != null)
            //    {
            //        model.SeoModel.Description = seo.Description;

            //    }
            //}
            //catch
            //{

            //}


            int seoCategoryId = 0;
            if (GetModelIdRoutData() != 0)
            {
                seoCategoryId = GetModelIdRoutData();
            }
            else if (GetBrandIdRouteData() != 0)
            {
                seoCategoryId = GetBrandIdRouteData();
            }
            else
            {
                seoCategoryId = model.CategoryModel.SelectedCategoryId;
            }

            if (GetPageQueryString() <= 1 && string.IsNullOrEmpty(GetSearchTextQueryString()))
            {
                var seoDefinition = _seoDefinitionService.GetSeoDefinitionByEntityIdWithEntityType(seoCategoryId, EntityTypeEnum.Category);
                if (seoDefinition != null)
                {
                    model.SeoModel.SeoContent = seoDefinition.SeoContent;
                    model.ContentSide = seoDefinition.ContentSide;
                    model.ContentBottomCenter = seoDefinition.ContentBottomCenter;
                }
            }
        }

        public void PrepareSearchAdressFilter(MTAdvancedSearchModel model, int selectedCategoryId, int selectedBrandId, int selectedModelId, int selectedSeriesId, int selectedCountryId, int selectedCityId, int selectedLocalityId)
        {
            IList<int> filterableCountryIds = new List<int>();
            IList<int> filterableCityIds = new List<int>();
            IList<int> filterableLocalityIds = new List<int>();
            IList<int> filterableBrandIds = new List<int>();
            IList<int> filterableModelIds = new List<int>();
            IList<int> filterableSeriesIds = new List<int>();
            int mainPartyId = AuthenticationUser.CurrentUser.Membership.MainPartyId;
            var productResult = _productService.GetCategoryProducts(selectedCategoryId, selectedBrandId, selectedModelId, selectedSeriesId, 0, mainPartyId, selectedCountryId,
                                                                                      selectedCityId, selectedLocalityId, 0, 0, 20, "");


            filterableCityIds = productResult.FilterableCityIds;
            filterableCountryIds = productResult.FilterableCountryIds;
            filterableLocalityIds = productResult.FilterableLocalityIds;

            if (filterableCountryIds != null && filterableCountryIds.Count > 0)
            {
                var filterableCountries = _addressService.GetCountriesByCountryIds(filterableCountryIds.Distinct().ToList());
                if (filterableCountries.Count > 0)
                {
                    foreach (var item in filterableCountries)
                    {
                        model.FilterItems.Add(new AdvancedSearchFilterItem
                        {
                            Value = item.CountryId,
                            Name = item.CountryName,
                            Type = 0


                        });
                    }


                }
            }
            if (filterableCityIds != null && filterableCityIds.Count > 0)
            {
                var filterableCities = _addressService.GetCitiesByCityIds(filterableCityIds.Distinct().ToList());
                if (filterableCities.Count > 0)
                {
                    foreach (var item in filterableCities)
                    {
                        model.FilterItems.Add(new AdvancedSearchFilterItem
                        {
                            Value = item.CityId,
                            Name = item.CityName,
                            Type = 1
                        });
                    }
                }
            }
            if (selectedCityId > 0)
            {
                if (filterableLocalityIds != null && filterableLocalityIds.Count > 0)
                {
                    var filterableLocalities = _addressService.GetLocalitiesByLocalityIds(filterableLocalityIds.Distinct().ToList());
                    if (filterableLocalities.Count > 0)
                    {
                        foreach (var item in filterableLocalities)
                        {
                            model.FilterItems.Add(new AdvancedSearchFilterItem
                            {
                                Value = item.LocalityId,
                                Name = item.LocalityName,
                                Type = 2
                            });
                        }
                    }
                }
            }
        }

        #endregion

        #region Methods

        #region AdvancedSearchAjax

        [HttpPost]
        public JsonResult GetSubCategory(int categoryId)
        {
            var categorySub = _categoryService.GetCategoriesByCategoryParentId(categoryId);
            List<AdvancedSearchFilterItem> list = new List<AdvancedSearchFilterItem>();
            foreach (var item in categorySub)
            {
                list.Add(new AdvancedSearchFilterItem { Value = item.CategoryId, Name = item.CategoryName });
            }
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult GetCities(int categoryId, int? brandId, int? modelId, int? serieId, int? cityId)
        {

            MTAdvancedSearchModel model = new MTAdvancedSearchModel();
            int selectedCategoryId = 0, selectedBrandId = 0, selectedModelId = 0, selectedSeriesId = 0, selectedCountryId = 0, selectedCityId = 0, selectedLocalityId = 0;
            selectedCategoryId = categoryId;
            if (brandId != null)
                selectedBrandId = Convert.ToInt32(brandId);
            if (modelId != null)
                selectedModelId = Convert.ToInt32(modelId);
            if (serieId != null)
                selectedSeriesId = Convert.ToInt32(serieId);
            if (cityId != null) selectedCityId = Convert.ToInt32(cityId);

            PrepareSearchAdressFilter(model, selectedCategoryId, selectedBrandId, selectedModelId, selectedSeriesId, selectedCountryId, selectedCityId, selectedLocalityId);
            return Json(new { Cities = model.FilterItems.Where(x => x.Type == 1), Localities = model.FilterItems.Where(x => x.Type == 2), Countries = model.FilterItems.Where(x => x.Type == 0) }, JsonRequestBehavior.AllowGet);

        }

        [HttpPost]
        public JsonResult GetBrands(int selectedCategoryId)
        {
            int mainPartyId = AuthenticationUser.CurrentUser.Membership.MainPartyId;
            CategoryProductsResult productResult = _productService.GetCategoryProducts(selectedCategoryId, 0, 0, 0, 0, mainPartyId, 0,
                                                                               0, 0, 0, 0, 20, "");
            MTAdvancedSearchModel model = new MTAdvancedSearchModel();
            IList<int> filterableBrandIds = new List<int>();
            IList<int> filterableModelIds = new List<int>();
            IList<int> filterableSeriesIds = new List<int>();
            filterableBrandIds = productResult.FilterableBrandIds;
            filterableModelIds = productResult.FilterableModelIds;
            filterableSeriesIds = productResult.FilterableSeriesIds;


            if (filterableBrandIds != null && filterableBrandIds.Count > 0)
            {
                var filterableBrands = _categoryService.GetCategoriesByCategoryIds(filterableBrandIds.Distinct().ToList());
                if (filterableBrands.Count > 0)
                {
                    var distinctfilterableBrands = filterableBrands.Select(b => b.CategoryName).Distinct();
                    if (distinctfilterableBrands.Count() == filterableBrands.Count)
                    {
                        foreach (var item in filterableBrands)
                        {
                            model.FilterItems.Add(new AdvancedSearchFilterItem
                            {
                                Value = item.CategoryId,
                                Name = item.CategoryName,
                                Type = (byte)AdvancedSearchFilterType.Brand

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
                                model.FilterItems.Add(new AdvancedSearchFilterItem
                                {
                                    Value = brands.First().CategoryId,
                                    Name = brands.First().CategoryName

                                });
                            }
                            else
                            {
                                model.FilterItems.Add(new AdvancedSearchFilterItem
                                {
                                    Value = brands.First().CategoryId,
                                    Name = brands.First().CategoryName,

                                });
                            }
                        }
                    }
                }
            }

            return Json(model, JsonRequestBehavior.AllowGet);

        }

        [HttpPost]
        public JsonResult GetModels(int selectedCategoryId, int selectedBrandId)
        {
            CategoryProductsResult productResult = _productService.GetCategoryProducts(selectedCategoryId, selectedBrandId, 0, 0, 0, 0, 0,
                                                                               0, 0, 0, 0, 20, "");
            MTAdvancedSearchModel model = new MTAdvancedSearchModel();

            IList<int> filterableBrandIds = new List<int>();
            IList<int> filterableModelIds = new List<int>();
            IList<int> filterableSeriesIds = new List<int>();

            filterableModelIds = productResult.FilterableModelIds;


            if (filterableModelIds != null && filterableModelIds.Count > 0)
            {
                var filterableModels = _categoryService.GetCategoriesByCategoryIds(filterableModelIds.Distinct().ToList());
                if (filterableModels.Count > 0)
                {

                    foreach (var item in filterableModels)
                    {

                        model.FilterItems.Add(new AdvancedSearchFilterItem
                        {
                            Value = item.CategoryId,
                            Name = item.CategoryName
                        });
                    }

                }

            }

            return Json(model, JsonRequestBehavior.AllowGet);

        }

        [HttpPost]
        public JsonResult GetSeries(int selectedCategoryId, int selectedBrandId, int selectedModelId)
        {
            CategoryProductsResult productResult = _productService.GetCategoryProducts(selectedCategoryId, selectedBrandId, selectedModelId, 0, 0, 0, 0,
                                                                               0, 0, 0, 0, 20, "");
            MTAdvancedSearchModel model = new MTAdvancedSearchModel();

            IList<int> filterableSeriesIds = new List<int>();

            filterableSeriesIds = productResult.FilterableSeriesIds;


            if (filterableSeriesIds != null && filterableSeriesIds.Count > 0)
            {
                var filterableSeries = _categoryService.GetCategoriesByCategoryIds(filterableSeriesIds.Distinct().ToList());
                if (filterableSeries.Count > 0)
                {
                    foreach (var item in filterableSeries)
                    {
                        model.FilterItems.Add(new AdvancedSearchFilterItem
                        {
                            Value = item.CategoryId,
                            Name = item.CategoryName
                        });
                    }
                }
            }

            return Json(model, JsonRequestBehavior.AllowGet);

        }

        #endregion

        [RemoveDuplicateContent]
        [Compress]
        public async Task<ActionResult> Index2(string selectedCategoryId, string SearchText)
        {
            var request = HttpContext.Request;
            if (request.Url.ToString() == "https://www.makinaturkiye.com/urun-kategori-c-0")
            {
                return RedirectPermanent("https://urun.makinaturkiye.com");
            }

            var yenilink = PrepareForLink(selectedCategoryId);
            if (!string.IsNullOrEmpty(yenilink) || request.Url.AbsoluteUri.StartsWith("https://video.") == true)
            {
                //ExceptionHandler.HandleException(Server.GetLastError());
                if (request.Url.AbsoluteUri.StartsWith("https://video.") == true)
                {
                    return RedirectPermanent(request.Url.ToString().Replace("https://video.", "https://www."));
                }

                return RedirectPermanent(yenilink);
            }

            if (!Request.IsLocal)
            {



            }




            #region redirect

            if (Server.UrlDecode(request.Url.AbsolutePath).Any(char.IsUpper))
            {
                string url = AppSettings.SiteUrlWithoutLastSlash + request.Url.AbsolutePath;
                return RedirectPermanent(url.ToLower().Replace("ı", "i"));
            }
            int countryId = GetCountryIdRouteData();
            if (countryId != 0)
            {
                if (request.Url.AbsolutePath.Substring(request.Url.AbsolutePath.Length - 1, 1) != "/")
                {
                    return RedirectPermanent(AppSettings.SiteUrlWithoutLastSlash + request.Url.AbsolutePath + "/");
                }
            }
            int index = request.Url.AbsolutePath.IndexOf("-c-");
            if (index > 0)
            {
                string idCategories = request.Url.AbsolutePath.Substring((index + 3), request.Url.AbsolutePath.Length - (index + 3));
                string categoryUrlName = request.Url.AbsolutePath.Substring(0, index - 1);
                if (idCategories.Contains("-"))
                {
                    string[] idCategorie = idCategories.Split('-');
                    if (idCategorie[0] == idCategorie[1])
                    {
                        var category1 = _categoryService.GetCategoryByCategoryId(Convert.ToInt32(idCategorie[0]));
                        var categoryParent = _categoryService.GetCategoryByCategoryId(Convert.ToInt32(category1.CategoryParentId));
                        string categoryNameUrl = (!string.IsNullOrEmpty(categoryParent.CategoryContentTitle)) ? categoryParent.CategoryContentTitle : categoryParent.CategoryName;

                        string urlNew = UrlBuilder.GetCategoryUrl(categoryParent.CategoryId, categoryNameUrl, Convert.ToInt32(idCategorie[0]), category1.CategoryName);
                        return RedirectPermanent(urlNew);

                    }
                }
            }
            var brandId = GetBrandIdRouteData();
            if (brandId > 0)
            {
                var categoryBrand = _categoryService.GetCategoryByCategoryId(brandId);
                if (categoryBrand == null)
                {
                    var category = _categoryService.GetCategoryByCategoryId(GetCategoryIdRouteData());
                    string categoryNameUrl = (!string.IsNullOrEmpty(category.CategoryContentTitle)) ? category.CategoryContentTitle : category.CategoryName;

                    var urlBuilder = UrlBuilder.GetCategoryUrl(category.CategoryId, categoryNameUrl, null, string.Empty);
                    return RedirectPermanent(urlBuilder);
                }
            }
            object routeName;
            if (RouteData.DataTokens.TryGetValue("RouteName", out routeName))
            {
                if (routeName != null && RouteData.Values.ContainsKey("productGroupName"))
                {
                    var countryIdRoute = RouteData.Values.ContainsKey("CountryId") ? Convert.ToInt32(RouteData.Values["CountryId"]) : 0;
                    var cityIdRoute = RouteData.Values.ContainsKey("CityId") ? Convert.ToInt32(RouteData.Values["CityId"]) : 0;
                    var localityIdRoute = RouteData.Values.ContainsKey("LocalityId") ? Convert.ToInt32(RouteData.Values["LocalityId"]) : 0;
                    var filterParams = GetCategoryFilterParameters("", "", countryIdRoute, cityIdRoute, localityIdRoute);
                    var redirectUrl = "";

                    var selectedCategory = _categoryService.GetCategoryByCategoryId(Convert.ToInt32(RouteData.Values["categoryId"]));

                    if (selectedCategory.CategoryType == (byte)CategoryType.Category || selectedCategory.CategoryType == (byte)CategoryType.ProductGroup || selectedCategory.CategoryType == (byte)CategoryType.Sector)
                    {
                        var categoryNameUrl = !string.IsNullOrEmpty(selectedCategory.CategoryContentTitle) ? selectedCategory.CategoryContentTitle : selectedCategory.CategoryName;

                        redirectUrl = UrlBuilder.GetCategoryUrl(selectedCategory.CategoryId, categoryNameUrl,
                            null, null);

                        if (RouteData.Values.ContainsKey("categoryIddown"))
                        {
                            var brandCategory =
                                _categoryService.GetCategoryByCategoryId(Convert.ToInt32(RouteData.Values["categoryIddown"]));

                            redirectUrl = UrlBuilder.GetCategoryUrl(selectedCategory.CategoryId, categoryNameUrl, brandCategory.CategoryId, brandCategory.CategoryName);
                        }
                    }
                    else if (selectedCategory.CategoryType == (byte)CategoryType.Model)
                    {
                        if (RouteData.Values.ContainsKey("categoryIddown"))
                        {
                            var modelUpperCategory =
                                _categoryService.GetCategoryByCategoryId(selectedCategory.CategoryParentId.Value);
                            var selectedCategoryTopCategory = _categoryService.GetCategoryByCategoryId(Convert.ToInt32(RouteData.Values["categoryIddown"]));

                            if (modelUpperCategory.CategoryType == (byte)CategoryType.Brand)
                            {
                                string categoryNameUrl = (!string.IsNullOrEmpty(selectedCategory.CategoryContentTitle)) ? selectedCategory.CategoryContentTitle : selectedCategory.CategoryName;

                                redirectUrl = UrlBuilder.GetModelUrl(selectedCategory.CategoryId, categoryNameUrl,
                                    modelUpperCategory.CategoryName, selectedCategoryTopCategory.CategoryName,
                                    selectedCategoryTopCategory.CategoryId);
                            }
                            else if (modelUpperCategory.CategoryType == (byte)CategoryType.Series)
                            {
                                var brandCategory =
                                    _categoryService.GetCategoryByCategoryId(modelUpperCategory.CategoryParentId.Value);
                                string categoryNameUrl = (!string.IsNullOrEmpty(selectedCategory.CategoryContentTitle)) ? selectedCategory.CategoryContentTitle : selectedCategory.CategoryName;

                                redirectUrl = UrlBuilder.GetModelUrl(selectedCategory.CategoryId,
                                    categoryNameUrl, brandCategory.CategoryName,
                                    selectedCategoryTopCategory.CategoryName, selectedCategoryTopCategory.CategoryId);
                            }
                        }
                        else
                        {
                            var category = _categoryService.GetCategoryByCategoryId(GetCategoryIdRouteData());
                            string categoryNameUrl = (!string.IsNullOrEmpty(category.CategoryContentTitle)) ? category.CategoryContentTitle : category.CategoryName;

                            var modelUpperCategory = _categoryService.GetCategoryByCategoryId(selectedCategory.CategoryParentId.Value);

                            var brandCategory = _categoryService.GetCategoryByCategoryId(modelUpperCategory.CategoryParentId.Value);

                            redirectUrl = UrlBuilder.GetCountryUrl(countryId, "türkiye", category.CategoryId, category.CategoryName, brandId, "");
                            return RedirectPermanent(redirectUrl);

                        }
                    }
                    else if (selectedCategory.CategoryType == (byte)CategoryType.Series)
                    {
                        //var serieCategory = _categoryService.GetCategoryByCategoryId(selectedCategory.CategoryParentId.Value);
                        var brandCategory = _categoryService.GetCategoryByCategoryId(selectedCategory.CategoryParentId.Value);
                        var categoryCategory =
                            _categoryService.GetCategoryByCategoryId(brandCategory.CategoryParentId.Value);
                        string categoryNameUrl = (!string.IsNullOrEmpty(selectedCategory.CategoryContentTitle)) ? selectedCategory.CategoryContentTitle : selectedCategory.CategoryName;

                        redirectUrl = UrlBuilder.GetSerieUrl(selectedCategory.CategoryId, categoryNameUrl,
                            brandCategory.CategoryName, categoryCategory.CategoryName);
                    }
                    else if (selectedCategory.CategoryType == (byte)CategoryType.Brand)
                    {
                        var brandUpperCategory = new Category();

                        if (selectedCategory.CategoryParentId != null)
                        {
                            brandUpperCategory =
                                _categoryService.GetCategoryByCategoryId(selectedCategory.CategoryParentId.Value);
                        }

                        if (brandUpperCategory != null)
                        {
                            string categoryNameUrl = (!string.IsNullOrEmpty(brandUpperCategory.CategoryContentTitle)) ? brandUpperCategory.CategoryContentTitle : brandUpperCategory.CategoryName;

                            redirectUrl = UrlBuilder.GetCategoryUrl(brandUpperCategory.CategoryId,
                                categoryNameUrl, selectedCategory.CategoryId,
                                selectedCategory.CategoryName);
                        }
                        else
                        {
                            string categoryNameUrl = (!string.IsNullOrEmpty(selectedCategory.CategoryContentTitle)) ? selectedCategory.CategoryContentTitle : selectedCategory.CategoryName;

                            redirectUrl = UrlBuilder.GetCategoryUrl(selectedCategory.CategoryId,
                                categoryNameUrl, null, null);
                        }
                    }

                    redirectUrl = UrlBuilder.GetFilterUrl(redirectUrl, filterParams);
                    return RedirectPermanent(redirectUrl);

                }
            }

            #endregion

            MTCategoryProductViewModel model = new MTCategoryProductViewModel { SearchText = SearchText };

            IList<FilterableCategoriesResult> filterableCategoryIds;
            IList<int> filterableCountryIds;
            IList<int> filterableCityIds;
            IList<int> filterableLocalityIds;
            IList<int> filterableBrandIds;
            IList<int> filterableModelIds;
            IList<int> filterableSeriesIds;

            int serviceProductCount;
            int newProductCount;
            int usedProductCount;

            PrepareProductCategoryTopCategoryModel(model);
            PrepareCategoryProductModel(model, out serviceProductCount, out newProductCount, out usedProductCount, out filterableCountryIds, out filterableCityIds, out filterableLocalityIds, out filterableCategoryIds, out filterableBrandIds, out filterableModelIds, out filterableSeriesIds, SearchText);
            PrepareProductCategoryModel(model, filterableCategoryIds);
            PrepareCategoryProductNavigation(model.CategoryModel);
            PrepareDataFilterModel(model, filterableCountryIds, filterableCityIds, filterableLocalityIds, filterableBrandIds, filterableModelIds, filterableSeriesIds);

            PrepareCustomFilterModel(model.FilteringContext, newProductCount, usedProductCount, serviceProductCount);

            PrepareSortingModel(model.FilteringContext);
            PreparePagingModel(model.PagingModel, model.TotalItemCount);
            PrepareCategoryStoreModel(model);

            PrepareSectorCategories(model);
            PrepareBannerModel(model);
            PrepareMostViewProductModel(model);

            PrepareSeoParams(model, selectedCategoryId);

            PrepareSeoLinks(model);
            PrepareJsonLd(model);
            PrePareSliderImages(model);
            int selectedBrandId = GetBrandIdRouteData();
            int selectCategoryId = GetCategoryIdRouteData();


            int selectedCountryId = GetCountryIdRouteData();
            int selectedCityId = GetCityIdRouteData();
            int selectedLocalityId = GetLocalityIdRouteData();
            model.CategoryId = selectCategoryId;
            model.BrandId = selectedBrandId;
            model.CountryId = selectedCountryId;
            model.CityId = selectedCityId;
            model.LocalityId = selectedLocalityId;

            return await Task.FromResult(View(model));
        }

        private void PrePareSliderImages(MTCategoryProductViewModel model)
        {

            var banners = _bannerService.GetBannersByCategoryId(model.CategoryModel.SelectedCategoryId).Where(x => x.BannerType == (byte)BannerType.CategorySliderBanner);

            var bannersList = banners.OrderBy(x => x.BannerOrder).GroupBy(x => x.BannerOrder).ToList();
            foreach (var item in bannersList)
            {
                var bannersSpecial = banners.Where(x => x.BannerOrder == item.Key);
                var bannerMobile = bannersSpecial.FirstOrDefault(x => x.BannerImageType == (byte)BannerImageType.Mobile);
                var bannerTablet = bannersSpecial.FirstOrDefault(x => x.BannerImageType == (byte)BannerImageType.Tablet);
                var bannerPc = bannersSpecial.FirstOrDefault(x => x.BannerImageType == (byte)BannerImageType.Pc);

                var bannerItemModel = new MTCategorySliderItem();

                bannerItemModel.SliderImagePathPc = ImageHelper.GetCategoryBannerImagePath(bannerPc.BannerResource);
                if (bannerTablet != null)
                    bannerItemModel.SliderImagePathTablet = ImageHelper.GetCategoryBannerImagePath(bannerTablet.BannerResource);
                if (bannerMobile != null)
                    bannerItemModel.SliderImagePathMobile = ImageHelper.GetCategoryBannerImagePath(bannerMobile.BannerResource);

                bannerItemModel.AltTag = !string.IsNullOrEmpty(bannerPc.BannerAltTag) ? bannerPc.BannerAltTag : model.CategoryModel.SelectedCategoryContentTitle;

                model.MTCategoSliderItems.Add(bannerItemModel);
            }
        }

        [HttpPost]
        public PartialViewResult GetStoresByCategoryId(string selectedCategoryId, string selectedBrandId, string selectedCountryId, string selectedCityId, string selectedLocalityId)
        {
            MTCategoryStoreModel model = new MTCategoryStoreModel();
            try
            {
                Category categoryAndBrand = null;
                int countryId = 0;
                int brandId = 0;
                int cityId = 0;
                int categoryId = 0;
                int localityId = 0;

                if (!string.IsNullOrEmpty(selectedCountryId))
                    countryId = Convert.ToInt32(selectedCountryId);
                if (!string.IsNullOrEmpty(selectedBrandId)) brandId = Convert.ToInt32(selectedBrandId);
                if (!string.IsNullOrEmpty(selectedCityId)) cityId = Convert.ToInt32(selectedCityId);
                if (!string.IsNullOrEmpty(selectedLocalityId)) localityId = Convert.ToInt32(selectedLocalityId);
                if (!string.IsNullOrEmpty(selectedCategoryId)) categoryId = Convert.ToInt32(selectedCategoryId);

                if (brandId > 0)
                    categoryAndBrand = _categoryService.GetCategoryByCategoryId(brandId);
                else
                    categoryAndBrand = _categoryService.GetCategoryByCategoryId(categoryId);
                IList<StoreForCategoryResult> categoryStores = _storeService.GetSPStoreForCategorySearch(categoryId, brandId, countryId, cityId, localityId);

                model.SelectedCategoryId = categoryAndBrand.CategoryId;
                model.SelectedCategoryType = categoryAndBrand.CategoryType.Value;
                model.SelectedCategoryName = categoryAndBrand.CategoryName;
                if (!string.IsNullOrEmpty(categoryAndBrand.CategoryContentTitle))
                    model.SelectedCategoryName = categoryAndBrand.CategoryContentTitle;
                model.StoreCategoryUrl = string.Empty;//Yapılacak.

                foreach (var item in categoryStores)
                {
                    string storeUrlName = _storeService.GetStoreByMainPartyId(item.MainPartyId).StoreUrlName;
                    model.StoreItemModes.Add(new MTCategoryStoreItemModel
                    {
                        MainPartyId = item.MainPartyId,
                        StoreName = item.StoreName,
                        StoreRate = item.StoreRate,
                        StoreProductCategoryUrl = UrlBuilder.GetStoreProfileProductCategoryUrl(categoryAndBrand.CategoryId, categoryAndBrand.CategoryName, storeUrlName),
                        StoreUrl = UrlBuilder.GetStoreProfileUrl(item.MainPartyId, item.StoreName, storeUrlName),
                        PictureLogoPath = ImageHelpers.GetStoreImage(item.MainPartyId, item.StoreLogo, "120")
                    });
                }
                string storePageTitle = "";
                if (!string.IsNullOrEmpty(categoryAndBrand.StorePageTitle))
                {
                    if (categoryAndBrand.StorePageTitle.Contains("Firma"))
                    {
                        storePageTitle = FormatHelper.GetCategoryNameWithSynTax(categoryAndBrand.StorePageTitle, CategorySyntaxType.CategoryNameOnyl);
                    }
                    else
                    {
                        storePageTitle = FormatHelper.GetCategoryNameWithSynTax(categoryAndBrand.StorePageTitle, CategorySyntaxType.Store);

                    }
                }
                else if (!string.IsNullOrEmpty(categoryAndBrand.CategoryContentTitle))
                    storePageTitle = FormatHelper.GetCategoryNameWithSynTax(categoryAndBrand.CategoryContentTitle, CategorySyntaxType.Store);
                else
                    storePageTitle = FormatHelper.GetCategoryNameWithSynTax(categoryAndBrand.CategoryName, CategorySyntaxType.Store);

                model.StoreCategoryUrl = UrlBuilder.GetStoreCategoryUrl(model.SelectedCategoryId, storePageTitle);

            }
            catch
            {

            }

            return PartialView("_CategoryStores", model);
        }
        [HttpPost]
        public PartialViewResult MViewedProductGet(string SelectedCategoryId, string selectedCategoryName)
        {

            int selectedCategoryId = Convert.ToInt32(SelectedCategoryId);

            var mostViewedProductModel = new MTMostViewedProductModel();
            mostViewedProductModel.SelectedCategoryName = selectedCategoryName;
            var products = _productService.GetSPMostViewProductsByCategoryId(selectedCategoryId);
            int mostViewProductIndex = 1;
            foreach (var item in products)
            {
                mostViewedProductModel.ProductItemModels.Add(new MTMostViewedProductItemModel
                {
                    ProductId = item.ProductId,
                    BrandName = string.IsNullOrEmpty(item.BrandName) ? string.Empty : item.BrandName,
                    Index = mostViewProductIndex,
                    ModelName = string.IsNullOrEmpty(item.ModelName) ? string.Empty : item.ModelName,
                    ProductName = item.ProductName,
                    ProductUrl = UrlBuilder.GetProductUrl(item.ProductId, item.ProductName),
                    SmallPictureName = StringHelper.Truncate(item.ProductName, 80),
                    SmallPicturePah = ImageHelper.GetProductImagePath(item.ProductId, item.MainPicture, ProductImageSize.px200x150),
                    CurrencyCss = item.GetCurrencyCssName(),
                    Price = item.GetFormattedPrice(),
                    ProductContactUrl = UrlBuilder.GetProductContactUrl(item.ProductId, item.StoreName),
                    StoreName = item.StoreName

                });
                mostViewProductIndex++;
            }
            int remindsProductNumber = 13 - mostViewProductIndex;
            if (remindsProductNumber != 0)
            {
                GetRemindMostViewProducts(mostViewedProductModel, remindsProductNumber, mostViewProductIndex);
            }
            return PartialView("_MostViewProduct", mostViewedProductModel);

        }
        public ActionResult AdvancedSearch()
        {
            var categoryService = _categoryService.GetMainCategories();
            MTAdvancedSearchModel model = new MTAdvancedSearchModel();
            foreach (var item in categoryService)
            {
                model.SectorList.Add(new MTCategoryItemModel
                {
                    CategoryId = item.CategoryId,
                    CategoryName = item.CategoryName
                });
            }
            return View(model);
        }

        [HttpPost]
        public ActionResult AdvancedSearch(string category, string brand, string productGroup, string model, string sector, string serie, string city, string locality, string country)
        {
            string url;
            int categoryId = 0;
            int categoryIdTemp;
            int modelId = 0;
            int serieId = 0;
            int brandId = 0;
            int cityId = 0;
            int localityId = 0;
            int countryId = 0;
            var brandCategory = new global::MakinaTurkiye.Entities.Tables.Catalog.Category();
            var modelCategory = new global::MakinaTurkiye.Entities.Tables.Catalog.Category();


            if (!string.IsNullOrEmpty(sector))
                if (sector != "0")
                    categoryId = Convert.ToInt32(sector);
            if (!string.IsNullOrEmpty(productGroup))
                if (productGroup != "0")
                    categoryId = Convert.ToInt32(productGroup);
            if (!string.IsNullOrEmpty(category))
                if (category != "0")
                    categoryId = Convert.ToInt32(category);
            if (!string.IsNullOrEmpty(brand))
                if (category != "0")
                    brandId = Convert.ToInt32(brand);
            if (!string.IsNullOrEmpty(model))
                if (model != "0")
                    modelId = Convert.ToInt32(model);
            if (!string.IsNullOrEmpty(serie))
                if (serie != "0")
                    serieId = Convert.ToInt32(serie);
            if (!string.IsNullOrEmpty(country))
                if (country != "0")
                    countryId = Convert.ToInt32(country);
            if (!string.IsNullOrEmpty(city))
                if (city != "0")
                    cityId = Convert.ToInt32(city);
            if (!string.IsNullOrEmpty(locality))
                if (locality != "0")
                    localityId = Convert.ToInt32(locality);

            if (serieId != 0)
            {

                serieId = Convert.ToInt32(serie);
                categoryIdTemp = serieId;
                var categoryItem = _categoryService.GetCategoryByCategoryId(categoryId);
                var serieItem = _categoryService.GetCategoryByCategoryId(serieId);
                var brandCat = _categoryService.GetCategoryByCategoryId(brandId);
                url = UrlBuilder.GetSerieUrl(serieItem.CategoryId, serieItem.CategoryName, brandCat.CategoryName, categoryItem.CategoryName);
            }
            else if (modelId != 0)
            {

                modelId = Convert.ToInt32(model);
                categoryIdTemp = modelId;
                var categoryItem = _categoryService.GetCategoryByCategoryId(modelId);
                var categoryParent = _categoryService.GetCategoryByCategoryId(categoryId);
                brandCategory = _categoryService.GetCategoryByCategoryId(brandId);
                url = UrlBuilder.GetModelUrl(modelId, categoryItem.CategoryName, brandCategory.CategoryName, categoryParent.CategoryName, categoryParent.CategoryId);
            }
            else if (brandId != 0)
            {
                brandId = Convert.ToInt32(brand);
                categoryIdTemp = brandId;
                var categoryItem = _categoryService.GetCategoryByCategoryId(brandId);
                brandCategory = categoryItem;
                var categoryParentItem = _categoryService.GetCategoryByCategoryId(Convert.ToInt32(categoryItem.CategoryParentId));
                url = UrlBuilder.GetCategoryUrl(categoryParentItem.CategoryId, categoryParentItem.CategoryName, brandId, categoryItem.CategoryName);
            }
            else
            {
                var categoryItem = _categoryService.GetCategoryByCategoryId(categoryId);
                categoryIdTemp = categoryId;
                url = UrlBuilder.GetCategoryUrl(categoryItem.CategoryId, categoryItem.CategoryName, null, string.Empty);
            }

            if (countryId != 0)
            {
                List<int> countries = new List<int> { countryId };
                var countryItem = _addressService.GetCountriesByCountryIds(countries).FirstOrDefault();

                var categoryItem = _categoryService.GetCategoryByCategoryId(categoryIdTemp);
                url = UrlBuilder.GetCountryUrl(countryId, countryItem.CountryName, categoryItem.CategoryId, categoryItem.CategoryName, brandId, brandCategory.CategoryName);
                if (cityId != 0)
                {
                    List<int> cities = new List<int> { cityId };
                    var cityItem = _addressService.GetCitiesByCityIds(cities).FirstOrDefault();
                    url = UrlBuilder.GetCityUrl(cityItem.CityId, cityItem.CityName, countryItem.CountryId, categoryItem.CategoryId, categoryItem.CategoryName, brandId, brandCategory.CategoryName);
                    if (localityId != 0)
                    {
                        List<int> localities = new List<int> { localityId };
                        var localityItem = _addressService.GetLocalitiesByLocalityIds(localities).First();
                        url = UrlBuilder.GetLocalityUrl(localityItem.LocalityId, localityItem.LocalityName, cityItem.CityId, countryId, categoryItem.CategoryId, categoryItem.CategoryName, brandId, brandCategory.CategoryName);
                    }
                }
            }

            return Redirect(url);
        }
        public ActionResult ProductContact(string productId)
        {
            return RedirectPermanent("/Product/ProductContact?productId=" + productId);
        }

        public ActionResult AllSectorProducts()
        {
            AllSectorProductsModel model = new AllSectorProductsModel();
            var sectors = _categoryService.GetMainCategories();
            foreach (var item in sectors)
            {
                model.Sectors.Add(new MTCategoryItemModel { CategoryName = item.CategoryName, CategoryId = item.CategoryId, CategoryIcon = item.CategoryIcon });
                var productResult = _productService.GetSectorRandomProductsByCategoryId(item.CategoryId);
                foreach (var productItem in productResult.ToList())
                {
                    MTAllSectorProductItemModel itemP = new MTAllSectorProductItemModel
                    {
                        ProductName = productItem.ProductName,
                        Price = productItem.ProductPrice != null ? Convert.ToString(productItem.ProductPrice) : "",
                        SectorId = item.CategoryId,
                        ProductUrl = UrlBuilder.GetProductUrl(productItem.ProductId, productItem.ProductName),
                        PicturePaths =
                      new string[5]{
                     ImageHelper.GetProductImagePath(productItem.ProductId, productItem.MainPicture,
                        ProductImageSize.px100),
                       ImageHelper.GetProductImagePath(productItem.ProductId, productItem.MainPicture,
                        ProductImageSize.px100x75),
                       ImageHelper.GetProductImagePath(productItem.ProductId, productItem.MainPicture,
                        ProductImageSize.px180x135),
                      ImageHelper.GetProductImagePath(productItem.ProductId, productItem.MainPicture,
                        ProductImageSize.x160x120),
                      ImageHelper.GetProductImagePath(productItem.ProductId, productItem.MainPicture,
                        ProductImageSize.px400x300)
                    },
                        BrandName = productItem.BrandName,
                        ModelName = productItem.ModelName
                    };
                    model.Products.Add(itemP);
                }
            }
            return View(model);
        }

        #endregion
    }
}

