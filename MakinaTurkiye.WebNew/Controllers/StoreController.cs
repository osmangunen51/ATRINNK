using MakinaTurkiye.Core;
using MakinaTurkiye.Entities.StoredProcedures.Stores;
using MakinaTurkiye.Services.Catalog;
using MakinaTurkiye.Services.Common;
using MakinaTurkiye.Services.Stores;
using MakinaTurkiye.Utilities.FormatHelpers;
using MakinaTurkiye.Utilities.HttpHelpers;
using MakinaTurkiye.Utilities.ImageHelpers;

using NeoSistem.MakinaTurkiye.Web.Models;
using NeoSistem.MakinaTurkiye.Web.Models.Stores;
using NeoSistem.MakinaTurkiye.Web.Models.UtilityModel;
using MakinaTurkiye.Utilities.Mvc;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using MakinaTurkiye.Services.Seos;
using MakinaTurkiye.Entities.Tables.Catalog;
using System.Threading.Tasks;

namespace NeoSistem.MakinaTurkiye.Web.Controllers
{
    [AllowAnonymous]
    public class StoreController : BaseController
    {
        #region Constants

        private const int SHOWED_PAGE_LENGTH = 12;
        private const string PAGE_INDEX_QUERY_STRING_KEY = "page";
        private const string ORDER_BY_QUERY_STRING_KEY = "orderby";
        private const string CITY_ID_QUERY_STRING_KEY = "cityId";
        private const string LOCALITY_ID_QUERY_STRING_KEY = "localityId";

        #endregion

        #region Fields

        private readonly IStoreService _storeService;
        private readonly ICategoryService _categoryService;
        private readonly IProductService _productService;
        private readonly IAddressService _addressService;
        private readonly ISeoDefinitionService _seoDefinitionService;
        private readonly IActivityTypeService _activityTypeService;

        #endregion

        public StoreController(IStoreService storeService, ISeoDefinitionService seoDefinitionService, 
            ICategoryService categoryService, IProductService productService,
            IAddressService addressService, IActivityTypeService activityTypeService)
        {
            _storeService = storeService;
            _categoryService = categoryService;
            _productService = productService;
            _addressService = addressService;
            _seoDefinitionService = seoDefinitionService;
            _activityTypeService = activityTypeService;
        }


        #region Utilities

        public void PrepareActivityTypeFilterModelNew(IList<int> ActivityIds, MTStoreViewModel model)
        {
            var acitivtyIdsDistinct = ActivityIds.Distinct();
            var activitiyTypes = _activityTypeService.GetAllActivityTypes();
            foreach (var activityTypeId in acitivtyIdsDistinct)
            {
                var activityType = activitiyTypes.FirstOrDefault(at => at.ActivityTypeId == activityTypeId);
                if (activityType != null)
                {
                    var tempActivityName = activityType.ActivityName.TrimEnd(' ').Replace(" ", "-");
                    var filtered = GetActivityTypeFilterQueryString().Split(',');
                    model.FilteringContext.MtStoreActivityTypeFilterModel.ActivityTypeFilterItemModels.Add(new MTStoreActivityTypeFilterItemModel()
                    {
                        Filtered = filtered.Contains(tempActivityName),
                        FilterItemId = activityType.ActivityTypeId.ToString(),
                        FilterItemName = activityType.ActivityName,
                        StoreCount = ActivityIds.Count(x => x == activityTypeId),
                        FilterUrl = filtered.Contains(tempActivityName) ? QueryStringBuilder.RemoveActivityTypeFilterQueryString(Request.Url.ToString(), tempActivityName) : QueryStringBuilder.ModifyActivityTypeFilterQueryString(Request.Url.ToString(), tempActivityName)
                    });
                }
            }
        }

        protected virtual string GenerateFilteredLocalityQueryParam(IList<int> localityIds)
        {
            string result = "";

            if (localityIds == null || localityIds.Count == 0)
                return result;

            for (int i = 0; i < localityIds.Count; i++)
            {
                result += localityIds[i];
                if (i != localityIds.Count - 1)
                    result += ",";
            }
            return result;
        }

        protected virtual string GenerateFilteredLocalityQueryParamNew(IList<string> localityNames)
        {
            var result = "";
            if (localityNames == null || localityNames.Count == 0)
                return result;
            for (int i = 0; i < localityNames.Count; i++)
            {
                result += localityNames[i];
                if (i != localityNames.Count - 1)
                    result += ",";
            }
            return result;
        }
        private string GenerateSelectedCityLocalityString()
        {
            var selectedCityName = GetCityNameQueryString();
            var sb = new StringBuilder();
            int cityId;
            if (int.TryParse(selectedCityName, out cityId)) return string.Empty;
            sb.Append(selectedCityName);
            var selectedLocalityNames = GetLocalityNameQueryString();
            if (selectedLocalityNames != "0")
            {
                sb.Append(" (" + selectedLocalityNames + ")");
            }
            sb.Append(" firmaları");
            return sb.ToString();
        }
        private void PrepareSortOptionModels(IList<WebSearchStoreResult> stores, MTStoreViewModel model)
        {
            var selectedSortOptionId = GetOrderByQueryString();
            model.FilteringContext.SortOptionModels.Add(new SortOptionModel
            {
                SortId = 1,
                SortOptionName = "Popüler Firmalar",
                SortOptionUrl = QueryStringBuilder.ModifyQueryString(Request.Url.ToString(), ORDER_BY_QUERY_STRING_KEY + "=1", null),
                Selected = (selectedSortOptionId == 1)

            });
            model.FilteringContext.SortOptionModels.Add(new SortOptionModel
            {
                SortId = 2,
                SortOptionName = "Son Eklenen",
                SortOptionUrl = QueryStringBuilder.ModifyQueryString(Request.Url.ToString(), ORDER_BY_QUERY_STRING_KEY + "=2", null),
                Selected = (selectedSortOptionId == 2)
            });
            model.FilteringContext.SortOptionModels.Add(new SortOptionModel
            {
                SortId = 3,
                SortOptionName = "a-Z Sıralama",
                SortOptionUrl = QueryStringBuilder.ModifyQueryString(Request.Url.ToString(), ORDER_BY_QUERY_STRING_KEY + "=3", null),
                Selected = (selectedSortOptionId == 3)
            });
            model.FilteringContext.SortOptionModels.Add(new SortOptionModel
            {
                SortId = 4,
                SortOptionName = "z-A Sıralama",
                SortOptionUrl = QueryStringBuilder.ModifyQueryString(Request.Url.ToString(), ORDER_BY_QUERY_STRING_KEY + "=4", null),
                Selected = (selectedSortOptionId == 4)
            });
        }


        private void PrepareActivityTypeFilterModelNew(MTStoreViewModel model)
        {
            var activitiyTypes = _activityTypeService.GetAllActivityTypes();
            foreach (var activityType in activitiyTypes)
            {
                var tempActivityName = activityType.ActivityName.TrimEnd(' ').Replace(" ", "-");
                var filtered = GetActivityTypeFilterQueryString().Split(',');
                model.FilteringContext.MtStoreActivityTypeFilterModel.ActivityTypeFilterItemModels.Add(new MTStoreActivityTypeFilterItemModel()
                {
                    Filtered = filtered.Contains(tempActivityName),
                    FilterItemId = activityType.ActivityTypeId.ToString(),
                    FilterItemName = activityType.ActivityName,
                    FilterUrl = filtered.Contains(tempActivityName) ? QueryStringBuilder.RemoveActivityTypeFilterQueryString(Request.Url.ToString(), tempActivityName) : QueryStringBuilder.ModifyActivityTypeFilterQueryString(Request.Url.ToString(), tempActivityName)
                });
            }

        }


        private void PrepareAdressFilterModel(MTStoreViewModel model, IList<int> filterableCityIds, IList<int> filterableLocalityIds)
        {
            if (filterableCityIds != null && filterableCityIds.Count > 0)
            {
                int selectedCityId = GetCityIdByCityName();
                var filterAbleCities = _addressService.GetCitiesByCityIds(filterableCityIds.Distinct().ToList()); 
                string filterUrl = QueryStringBuilder.RemoveQueryString(this.Request.Url.ToString(), CITY_ID_QUERY_STRING_KEY);
                filterUrl = QueryStringBuilder.RemoveQueryString(filterUrl, PAGE_INDEX_QUERY_STRING_KEY);
                filterUrl = QueryStringBuilder.RemoveQueryString(filterUrl, LOCALITY_ID_QUERY_STRING_KEY);
                model.FilteringContext.StoreAddressFilterModel.CityFilterItemModels.Add(new MTStoreAddressFilterItemModel
                {
                    Filtered = false,
                    FilterItemId = "0",
                    FilterItemName = "Tüm Şehirler",
                    FilterItemStoreCount = filterableCityIds.Count,
                    FilterUrl = filterUrl
                });
                foreach (var item in filterAbleCities)
                {
                    filterUrl = QueryStringBuilder.ModifyQueryString(filterUrl, CITY_ID_QUERY_STRING_KEY + "=" + item.CityName, null);
                    model.FilteringContext.StoreAddressFilterModel.CityFilterItemModels.Add(new MTStoreAddressFilterItemModel
                    {
                        Filtered = (item.CityId == selectedCityId),
                        FilterItemId = item.CityId.ToString(),
                        FilterItemName = item.CityName,
                        FilterItemStoreCount = filterableCityIds.Count(ct => ct == item.CityId),
                        FilterUrl = filterUrl
                    });
                }
                model.FilteringContext.StoreAddressFilterModel.CityFilterItemModels = model.FilteringContext
                  .StoreAddressFilterModel.CityFilterItemModels.OrderByDescending(p => p.FilterItemId == "0")
                  .ThenByDescending(p => p.Filtered)
                  .ThenBy(p => p.FilterItemName).ToList();
            }

            if (filterableLocalityIds != null && filterableLocalityIds.Count > 0)
            {
                int selectedCityId = GetCityIdByCityName();
                if (selectedCityId > 0)
                {
                    var localities = _addressService.GetLocalitiesByCityId(selectedCityId);
                    if (localities.Count > 0)
                    {
                        int[] arrayToLocalityIds = filterableLocalityIds.Distinct().ToArray();
                        var filterableLocalities = (from l in localities
                                                    where arrayToLocalityIds.Contains(l.LocalityId)
                                                    orderby l.LocalityName
                                                    select l).ToList();
                        string filterUrl = QueryStringBuilder.RemoveQueryString(this.Request.Url.ToString(), PAGE_INDEX_QUERY_STRING_KEY);
                        foreach (var item in filterableLocalities)
                        {
                            var selectedLocalityNames = GetLocalityNamesQueryString();
                            var localityfilterItemModel = new MTStoreAddressFilterItemModel
                            {
                                Filtered = (selectedLocalityNames.Contains(item.LocalityName)),
                                FilterItemId = item.LocalityId.ToString(),
                                FilterItemName = item.LocalityName,
                                FilterItemStoreCount = filterableLocalityIds.Count(l => l == item.LocalityId)
                            };

                            if (localityfilterItemModel.Filtered)
                            {
                                selectedLocalityNames.Remove(item.LocalityName);
                            }
                            else
                            {
                                if (!selectedLocalityNames.Contains(item.LocalityName))
                                    selectedLocalityNames.Add(item.LocalityName);
                            }

                            if (selectedLocalityNames.Count > 0)
                            {
                                string newQueryParam = GenerateFilteredLocalityQueryParamNew(selectedLocalityNames);
                                filterUrl = QueryStringBuilder.ModifyQueryString(filterUrl, LOCALITY_ID_QUERY_STRING_KEY + "=" + newQueryParam, null);

                            }
                            else
                            {
                                filterUrl = QueryStringBuilder.RemoveQueryString(filterUrl, LOCALITY_ID_QUERY_STRING_KEY);
                            }
                            localityfilterItemModel.FilterUrl = filterUrl;

                            model.FilteringContext.StoreAddressFilterModel.LocalityFilterItemModels.Add(localityfilterItemModel);
                        }

                    }
                }
            }
        }

        private void PreparePagingModel(MTStoreViewModel model)
        {
            if (model.FilteringContext.TotalItemCount > SHOWED_PAGE_LENGTH)
            {
                int pageIndex = GetPageQueryString();
                model.StorePagingModel.CurrentPageIndex = pageIndex;
                if (model.FilteringContext.TotalItemCount % SHOWED_PAGE_LENGTH == 0)
                {
                    model.StorePagingModel.TotalPageCount = model.FilteringContext.TotalItemCount / SHOWED_PAGE_LENGTH;
                }
                else
                {
                    model.StorePagingModel.TotalPageCount = (model.FilteringContext.TotalItemCount / SHOWED_PAGE_LENGTH) + 1;
                }

                int firstPage = model.StorePagingModel.CurrentPageIndex >= 5 ? model.StorePagingModel.CurrentPageIndex - 4 : 1;
                int lastPage = firstPage + 8;
                if (lastPage >= model.StorePagingModel.TotalPageCount)
                {
                    lastPage = model.StorePagingModel.TotalPageCount;
                }
                model.StorePagingModel.FirstPage = firstPage;
                model.StorePagingModel.LastPage = lastPage;

                model.StorePagingModel.FirstPageUrl = QueryStringBuilder.ModifyQueryString(this.Request.Url.ToString(), PAGE_INDEX_QUERY_STRING_KEY + "=1", null);
                model.StorePagingModel.LastPageUrl = QueryStringBuilder.ModifyQueryString(this.Request.Url.ToString(), PAGE_INDEX_QUERY_STRING_KEY + "=" + model.StorePagingModel.TotalPageCount, null);

                for (int i = model.StorePagingModel.FirstPage; i <= model.StorePagingModel.LastPage; i++)
                {
                    model.StorePagingModel.PageUrls.Add(i, QueryStringBuilder.ModifyQueryString(this.Request.Url.ToString(), PAGE_INDEX_QUERY_STRING_KEY + "=" + i, null));
                }
            }
        }

        private void PrepareStoreCategoryModel(int categoryId, MTStoreViewModel model)
        {
            int selectedOrderby = GetOrderByQueryString();
            if (categoryId > 0)
            {
                var topCategories = _categoryService.GetSPTopCategories(categoryId).Where(x => x.CategoryType != (byte)CategoryType.Model).ToList();
                if (topCategories.Count > 0)
                {
                    var lastCategory = topCategories.LastOrDefault();
                    model.StoreCategoryModel.SelectedCategoryId = categoryId;
                }

                foreach (var item in topCategories)
                {
                    string storePageTitle = "";
                    var categoryItemModel = new MTStoreCategoryItemModel
                    {
                        CategoryId = item.CategoryId,
                        CategoryType = item.CategoryType,
                        DefaultCategoryName = item.CategoryName
                    };
                    if (!string.IsNullOrEmpty(item.StorePageTitle))
                    {
                        if (item.StorePageTitle.Contains("Firma"))
                        {
                            storePageTitle = FormatHelper.GetCategoryNameWithSynTax(item.StorePageTitle, CategorySyntaxType.CategoryNameOnyl);
                        }
                        else
                        {
                            storePageTitle = FormatHelper.GetCategoryNameWithSynTax(item.StorePageTitle, CategorySyntaxType.Store);

                        }
                    }
                    else if (!string.IsNullOrEmpty(item.CategoryContentTitle))
                        storePageTitle = FormatHelper.GetCategoryNameWithSynTax(item.CategoryContentTitle, CategorySyntaxType.Store);
                    else
                        storePageTitle = FormatHelper.GetCategoryNameWithSynTax(item.CategoryName, CategorySyntaxType.Store);

                    if (item.CategoryType == (byte)CategoryType.Model)
                    {
                        int topCategoryCount = topCategories.Count;
                        var elementCategory = topCategories.ElementAt(topCategoryCount - 2);
                        string modelCategoryName = string.Format("{0}", item.CategoryName);
                        categoryItemModel.CategoryName = FormatHelper.GetCategoryNameWithSynTax(item.CategoryName, CategorySyntaxType.CategoryNameOnyl);
                        categoryItemModel.CategoryUrl = UrlBuilder.GetStoreCategoryUrl(item.CategoryId, storePageTitle, selectedOrderby);
                    }
                    else if (item.CategoryType == (byte)CategoryTypeEnum.ProductGroup)
                    {
                        var fistCategory = topCategories.FirstOrDefault();
                        string productGrupCategoryName = string.Format("{0}", item.CategoryName);
                        categoryItemModel.CategoryName = FormatHelper.GetCategoryNameWithSynTax(item.CategoryName, CategorySyntaxType.CategoryNameOnyl);
                        categoryItemModel.CategoryUrl = UrlBuilder.GetStoreCategoryUrl(item.CategoryId, storePageTitle, selectedOrderby);

                    }
                    else
                    {
                        categoryItemModel.CategoryName = FormatHelper.GetCategoryNameWithSynTax(item.CategoryName, CategorySyntaxType.CategoryNameOnyl);
                        categoryItemModel.CategoryUrl = UrlBuilder.GetStoreCategoryUrl(item.CategoryId, storePageTitle, selectedOrderby);
                    }
                    categoryItemModel.CategoryContentTitle = storePageTitle;
                    model.StoreCategoryModel.SelectedCategoryName = storePageTitle;
                    
                    model.StoreCategoryModel.StoreTopCategoryItemModels.Add(categoryItemModel);
                }
            }

            var storeCategories = _categoryService.GetSPStoreCategoryByCategoryParentId(categoryId).Where(x => x.CategoryType != (byte)CategoryType.Brand).ToList();
            IList<MTStoreCategoryItemModel> videoCategoryItemModels = new List<MTStoreCategoryItemModel>();
            foreach (var item in storeCategories)
            {
                string storePageTitle = "";
                if (!string.IsNullOrEmpty(item.StorePageTitle))
                {
                    if (item.StorePageTitle.Contains("Firma"))
                    {
                        storePageTitle = FormatHelper.GetCategoryNameWithSynTax(item.StorePageTitle, CategorySyntaxType.CategoryNameOnyl);
                    }
                    else
                    {
                        storePageTitle = FormatHelper.GetCategoryNameWithSynTax(item.StorePageTitle, CategorySyntaxType.Store);

                    }
                }
                else if (!string.IsNullOrEmpty(item.CategoryContentTitle))
                    storePageTitle = FormatHelper.GetCategoryNameWithSynTax(item.CategoryContentTitle, CategorySyntaxType.Store);
                else
                    storePageTitle = FormatHelper.GetCategoryNameWithSynTax(item.CategoryName, CategorySyntaxType.Store);
                var itemModel = new MTStoreCategoryItemModel
                {
                    CategoryUrl = UrlBuilder.GetStoreCategoryUrl(item.CategoryId, storePageTitle, selectedOrderby),
                    CategoryType = item.CategoryType
                };


                if (item.CategoryType == (byte)CategoryTypeEnum.ProductGroup)
                {
                    var lastCategory = model.StoreCategoryModel.StoreTopCategoryItemModels.LastOrDefault();
                    string productGrupCategoryName = string.Format("{0}", item.CategoryName);
                    itemModel.CategoryName = productGrupCategoryName;
                }
                else if (item.CategoryType == (byte)CategoryTypeEnum.Model)
                {
                    int topCategoryCount = model.StoreCategoryModel.StoreTopCategoryItemModels.Count;
                    var elementCategory = model.StoreCategoryModel.StoreTopCategoryItemModels.ElementAt(topCategoryCount - 1);
                    string modelCategoryName = string.Format("{0}", item.CategoryName);
                    itemModel.CategoryName = modelCategoryName;
                    itemModel.CategoryUrl = UrlBuilder.GetStoreCategoryUrl(item.CategoryId, storePageTitle, selectedOrderby);
                }
                else
                {
                    itemModel.CategoryName = item.CategoryName;
                    itemModel.CategoryUrl = UrlBuilder.GetStoreCategoryUrl(item.CategoryId, storePageTitle, selectedOrderby);
                }
                if (itemModel.CategoryType != 5)
                {
                    videoCategoryItemModels.Add(itemModel);
                }
            }

            model.StoreCategoryModel.StoreCategoryItemModels = videoCategoryItemModels;
        }

        private string PrepareForLink(Category category)
        {
            int selectedOrderBy = GetOrderByQueryString();
            string url = "";
            string storePageTitle = "";
            if (!string.IsNullOrEmpty(category.StorePageTitle))
            {
                if (category.StorePageTitle.Contains("Firma"))
                {
                    storePageTitle = FormatHelper.GetCategoryNameWithSynTax(category.StorePageTitle, CategorySyntaxType.CategoryNameOnyl);
                }
                else
                {
                    storePageTitle = FormatHelper.GetCategoryNameWithSynTax(category.StorePageTitle, CategorySyntaxType.Store);

                }
            }
            else if (!string.IsNullOrEmpty(category.CategoryContentTitle))
                storePageTitle = FormatHelper.GetCategoryNameWithSynTax(category.CategoryContentTitle, CategorySyntaxType.Store);
            else
                storePageTitle = FormatHelper.GetCategoryNameWithSynTax(category.CategoryName, CategorySyntaxType.Store);

            if (category.CategoryType == (byte)CategoryType.Model)
            {

                string modelCategoryName = string.Format("{0}", category.CategoryName);

                url = UrlBuilder.GetStoreCategoryUrl(category.CategoryId, storePageTitle);

            }
            else if (category.CategoryType == (byte)CategoryTypeEnum.ProductGroup)
            {
                string productGrupCategoryName = string.Format("{0}", category.CategoryName);

                url = UrlBuilder.GetStoreCategoryUrl(category.CategoryId, storePageTitle);
            }
            else
            {
                url = UrlBuilder.GetStoreCategoryUrl(category.CategoryId, storePageTitle);
            }
            return url;

        }

        private void PrepareNavigation(MTStoreViewModel model)
        {
            IList<Navigation> alMenu = new List<Navigation>();
            alMenu.Add(new Navigation("Firmalar", AppSettings.StoreAllUrl, Navigation.TargetType._self));
            var topCategories = model.StoreCategoryModel.StoreTopCategoryItemModels;
            foreach (var item in topCategories)
            {
                

                var url =item.CategoryUrl;


                alMenu.Add(new Navigation(item.CategoryContentTitle, url, Navigation.TargetType._self));
            }
            model.Navigation = LoadNavigationV3(alMenu);
        }

        private void PrepareStoreModel(IList<WebSearchStoreResult> stores, MTStoreViewModel model)
        {
            //int categoryId = 0;
            var lastCategory = model.StoreCategoryModel.StoreTopCategoryItemModels.LastOrDefault();
            //if (lastCategory != null)
            //{
            //    categoryId = lastCategory.CategoryId;
            //}

            IList<MTStoreModel> storeModels = new List<MTStoreModel>();
            foreach (var item in stores)
            {
                var storeModel = new MTStoreModel();
                storeModel.WebSiteUrl = string.Format("http://{0}", item.StoreWeb);
                storeModel.StoreProfileUrl = UrlBuilder.GetStoreProfileUrl(item.MainPartyId, item.StoreName, item.StoreUrlName);
                storeModel.BrandUrlForStoreProfile = UrlBuilder.GetBrandUrlForStoreProfile(item.MainPartyId, item.StoreName, item.StoreUrlName);
                storeModel.ProductUrlForStoreProfile = UrlBuilder.GetProductUrlForStoreProfile(item.MainPartyId, item.StoreName, item.StoreUrlName);

                if (lastCategory != null)
                {
                    storeModel.SelectedCategoryContentTitle = lastCategory.CategoryContentTitle;
                    storeModel.SelectedCategoryName = lastCategory.DefaultCategoryName;
                    if(lastCategory.CategoryType!=(byte)CategoryType.Sector)
                    storeModel.SelectedCategoryProductUrlForStoreProfile = UrlBuilder.GetStoreProfileProductCategoryUrl(lastCategory.CategoryId, lastCategory.CategoryName.Replace(" Firmaları", ""), item.StoreUrlName);
                }

                storeModel.StoreShortName = item.StoreShortName;
                storeModel.StoreName = item.StoreName;
                storeModel.TruncateStoreName = StringHelper.Truncate(item.StoreName, 100);
                storeModel.StoreLogoPath = ImageHelper.GetStoreLogoParh(item.MainPartyId, item.StoreLogo, 300);
                storeModel.FullActivityTypeName = StringHelper.Truncate(item.FullActivityTypeName, 145);
                storeModel.StoreShowName = item.StoreName;
                storeModel.StoreAbout = item.StoreAbout;

                if (!string.IsNullOrEmpty(item.StoreShortName))
                    storeModel.StoreShowName = item.StoreShortName;
                IList<ProductForStoreResult> productsForStore = null;

                if (lastCategory != null)
                {
                    switch (lastCategory.CategoryType)
                    {
                        case (byte)CategoryTypeEnum.Brand:
                            productsForStore = _productService.GetSPProductForStoreByBrandId(lastCategory.CategoryId, item.MemberMainPartyId, 6);
                            break;
                        case (byte)CategoryTypeEnum.Model:
                            productsForStore = _productService.GetSPProductForStoreByModelId(lastCategory.CategoryId, item.MemberMainPartyId, 6);
                            break;
                        default:
                            productsForStore = _productService.GetSPProductForStoreByCategoryId(lastCategory.CategoryId, item.MemberMainPartyId, 6);
                            break;
                    }
                }
                else
                {
                    productsForStore = _productService.GetSPProductForStoreByCategoryId(0, item.MemberMainPartyId, 6);

                }

                if (productsForStore!=null)
                {
                    foreach (var productItem in productsForStore)
                    {
                        if (!string.IsNullOrEmpty(productItem.MainPicturePath))
                        {
                            var categoryContentTitle = _categoryService.GetCategoryByCategoryId(productItem.CategoryId).CategoryContentTitle;
                            string categoryUrlName = !string.IsNullOrEmpty(categoryContentTitle) ? categoryContentTitle : productItem.CategoryName;
                            var productModel = new MTStoreModel.ProductModel();
                            productModel.BrandName = productItem.BrandName;
                            productModel.ModelName = productItem.ModelName;
                            productModel.ProductName = productItem.ProductName.Replace("\"", "");
                            productModel.ProductUrl = UrlBuilder.GetProductUrl(productItem.ProductId, productItem.ProductName);
                            productModel.LargePicturePath = ImageHelper.GetProductImagePath(productItem.ProductId, productItem.MainPicturePath, ProductImageSize.px400x300);
                            productModel.SmallPicturePath = ImageHelper.GetProductImagePath(productItem.ProductId, productItem.MainPicturePath, ProductImageSize.px100x75);
                            productModel.SimilarUrl = UrlBuilder.GetCategoryUrl(productItem.CategoryId, categoryUrlName, null, string.Empty);
                            storeModel.ProductModels.Add(productModel);
                        }
                    }
                }

                storeModels.Add(storeModel);
            }
            model.StoreModels = storeModels;
        }

        //private void PrepareSeo(int categoryId)
        //{
        //    if (categoryId > 0)
        //    {
        //        //SeoPageType = (byte)PageType.StoreCategory;
        //        var category = _categoryService.GetCategoryByCategoryId(categoryId);
        //        string storePageTitle = "";
        //        if (!string.IsNullOrEmpty(category.StorePageTitle))
        //            storePageTitle = category.StorePageTitle;
        //        else if (!string.IsNullOrEmpty(category.CategoryContentTitle))
        //            storePageTitle = category.CategoryContentTitle;
        //        else
        //            storePageTitle = category.CategoryName;

        //        CreateSeoParameter(SeoModel.SeoProductParemeters.Category, storePageTitle);

        //    }
        //    else
        //    {
        //        //SeoPageType = (byte)PageType.StoreCategoryHome;
        //    }
        //}

        private int GetCityIdByCityName()
        {
            var city = GetCityNameQueryString();
            int id = 0;
            var tryParse = int.TryParse(city, out id);
            return tryParse ? id : _addressService.GetSingleCityIdByCityName(city);
        }

        private List<int> GetLocalityIdByLocalityName()
        {
            var locality = GetLocalityNamesQueryString();
            var ifLocalityIds = new List<int>();
            foreach (var item in locality)
            {
                int id;
                if (int.TryParse(item, out id))
                {
                    ifLocalityIds.Add(id);
                }
            }
            return ifLocalityIds.Count > 0 ? ifLocalityIds : _addressService.GetSingleLocalityIdByLocalityName(locality);
        }

        #endregion

        #region Methods

        [HttpGet]
        [Compress]
        public ActionResult Index()
        {
            string searchText = GetSearchTextQueryString();
            if (searchText.Length > 100)
            {
                return RedirectToActionPermanent("Index");
            }

            int categoryId = GetCategoryIdRouteData();
            int orderby = GetOrderByQueryString();
         
            //var cityID = Request.QueryString["cityID"] != null ? Request.QueryString["cityID"] : "0";
            //var pageId = Request.QueryString["page"] != null ? Request.QueryString["page"] : "0";
            //int pageID = 0;
            //if (pageId.Contains(","))
            //{
            //    pageID = Convert.ToInt32(pageId.Split(',')[0]);
            //    var url = Request.Url.AbsolutePath.ToString();

            //    var newPageUrl = "&page=" + pageID;
            //    if (Request.QueryString["cityId"] != null)
            //        url += "?cityID=" + cityID;
            //    if (Request.QueryString["filtre"] != null)
            //        url += "&filtre=" + Request.QueryString["filtre"].ToString() + "," + pageId.Split(',')[1];
            //    else
            //        url += "&filtre=" + pageId.Split(',')[1];
            //    if (Request.QueryString["localityId"] != null)
            //        url += "&localityId=" + Request.QueryString["localityId"].ToString();
            //    if (Request.QueryString["orderby"] != null)
            //        url += "&orderby=" + Request.QueryString["orderby"].ToString();
            //    url += newPageUrl;
            //    return RedirectPermanent(url);
            //}

            //if (ViewData["SEOPAGETYPE"] == null)
            //    ViewData["SEOPAGETYPE"] = 29;

            Category category = null;
     
            if (!Request.IsLocal && categoryId==0)
            {
                if (Request.Url.ToString().ToLower().Contains("sirketler")){
                    return RedirectPermanent(AppSettings.StoreAllUrl);
                }
            }
            if (categoryId > 0)
            {
                category = _categoryService.GetCategoryByCategoryId(categoryId);
                if (category == null)
                {
                    return RedirectPermanent(AppSettings.StoreAllUrl);
                }
                else
                {
    

                    if (string.IsNullOrEmpty(searchText))
                    {
                        string url = PrepareForLink(category);
                        string urlWithPath = Request.Url.AbsoluteUri;
                        if (Request.Url.AbsoluteUri.ToString().IndexOf("?") > 0)
                        {
                            urlWithPath = Request.Url.AbsoluteUri.Substring(0, Request.Url.AbsoluteUri.ToString().IndexOf("?"));
                        }
   
                        string urlCheck = Request.IsLocal ? Request.Url.AbsolutePath : urlWithPath;
                        if (urlCheck != url)
                        {
                            return RedirectPermanent(url);
                        }
                    }
                }
            }


            var model = new MTStoreViewModel();
            //store category
            PrepareStoreCategoryModel(categoryId, model);
            //navigation
            PrepareNavigation(model);
            //seo
            //PrepareSeo(categoryId);
            var request = HttpContext.Request;



            int modelId = 0;
            int brandId = 0;

            if (category != null)
            {
                if (category.CategoryType == (byte)CategoryTypeEnum.Brand)
                {
                    brandId = category.CategoryId;
                    int topCategoryCount = model.StoreCategoryModel.StoreTopCategoryItemModels.Count;
                    var elementCategory =
                        model.StoreCategoryModel.StoreTopCategoryItemModels.ElementAt(topCategoryCount - 2);
                    categoryId = elementCategory.CategoryId;
                }

                if (category.CategoryType == (byte)CategoryTypeEnum.Model)
                {
                    modelId = category.CategoryId;
                    int topCategoryCount = model.StoreCategoryModel.StoreTopCategoryItemModels.Count;
                    var elementCategory =
                        model.StoreCategoryModel.StoreTopCategoryItemModels.ElementAt(topCategoryCount - 3);
                    categoryId = elementCategory.CategoryId;
                    var brand = model.StoreCategoryModel.StoreTopCategoryItemModels.ElementAt(topCategoryCount - 2);
                    brandId = brand.CategoryId;
                }


            }

            int selectedCityId = GetCityIdByCityName();

            ViewBag.SelectedCityName = GenerateSelectedCityLocalityString();
            var selectedLocalityIds = GetLocalityIdByLocalityName();
            int pageIndex = GetPageQueryString();
            int selectedSortOptionId = GetOrderByQueryString();
            var activityType = QueryStringBuilder.GetCorrectlyActivityTypeName(GetActivityTypeFilterQueryString());

            model.FilteringContext.MtStoreActivityTypeFilterModel.SelectedActivityTypeFilterName = string.IsNullOrEmpty(activityType) ? string.Empty : "( " + activityType + " )";

            IList<int> searchBasedOnFilterableCityIds = null;
            IList<int> searchBasedOnFilterableLocalityIds = null;
            IList<int> filterableActivityIds = null;

            var storeResult = _storeService.GetCategoryStores(categoryId, modelId, brandId, selectedCityId,
                selectedLocalityIds, searchText, selectedSortOptionId, pageIndex, SHOWED_PAGE_LENGTH, activityType);

            searchBasedOnFilterableCityIds = storeResult.FilterableCityIds;
            searchBasedOnFilterableLocalityIds = storeResult.FilterableLocalityIds;
            filterableActivityIds = storeResult.FilterableActivityIds;

            model.FilteringContext.TotalItemCount = storeResult.TotalCount;

            //store
            PrepareStoreModel(storeResult.Stores, model);
            //filter sort options
            PrepareSortOptionModels(storeResult.Stores, model);
            //paging
            PreparePagingModel(model);
            //filer adddress
            PrepareAdressFilterModel(model, searchBasedOnFilterableCityIds, searchBasedOnFilterableLocalityIds);

            PrepareActivityTypeFilterModelNew(filterableActivityIds,model);
            //PrepareActivityTypeFilterModel(model);
            if (model.StoreModels.Count == 0 && category!=null)
            {
                string redirectUrl = "https:://magaza.makinaturkiye.com";
                if (category.CategoryParentId.HasValue)
                {
                    var categoryParent = _categoryService.GetCategoryByCategoryId(category.CategoryParentId.Value);
                    if (category != null)
                    {
                        string urlName = "";
                        if (!string.IsNullOrEmpty(category.StorePageTitle))
                        {
                            if (category.StorePageTitle.Contains("Firma"))
                            {
                                urlName = FormatHelper.GetCategoryNameWithSynTax(category.StorePageTitle, CategorySyntaxType.CategoryNameOnyl);
                            }
                            else
                            {
                                urlName = FormatHelper.GetCategoryNameWithSynTax(category.StorePageTitle, CategorySyntaxType.Store);

                            }
                        }
                        else if (!string.IsNullOrEmpty(category.CategoryContentTitle))
                            urlName = FormatHelper.GetCategoryNameWithSynTax(category.CategoryContentTitle, CategorySyntaxType.Store);
                        else
                            urlName = FormatHelper.GetCategoryNameWithSynTax(category.CategoryName, CategorySyntaxType.Store);

                        redirectUrl = UrlBuilder.GetStoreCategoryUrl(categoryParent.CategoryId, urlName);

                    }
                }
                model.RedirectUrl = redirectUrl;
            }

            #region canonicals

            if (model.StorePagingModel.TotalPageCount > model.StorePagingModel.CurrentPageIndex + 1)
            {
                int nextP = model.StorePagingModel.CurrentPageIndex + 1;
                model.NextPage = QueryStringBuilder.ModifyQueryString(AppSettings.SiteUrl.Substring(0, AppSettings.SiteUrl.Length - 1) + request.Url.PathAndQuery, "page" + "=" + nextP, null);

            }
            if (Request.QueryString["page"] != null)
            {
                if (Convert.ToInt32(Request.QueryString["page"]) > 1 && Convert.ToInt32(Request.QueryString["page"]) <= model.StorePagingModel.TotalPageCount)
                {
                    int prevP = model.StorePagingModel.CurrentPageIndex - 1;
                    model.PrevPage = QueryStringBuilder.ModifyQueryString(AppSettings.SiteUrl.Substring(0, AppSettings.SiteUrl.Length - 1) + request.Url.PathAndQuery, "page" + "=" + prevP, null); ;
                }
            }

            if (string.IsNullOrEmpty(searchText))
            {
         
                    model.CanonicalUrl = "https://magaza.makinaturkiye.com" + request.Url.AbsolutePath;
            }

            #endregion

            var seoDefinition = _seoDefinitionService.GetSeoDefinitionByEntityIdWithEntityType(categoryId, EntityTypeEnum.StoreCategory);
            if (seoDefinition != null)
            { 
              model.SeoContent = seoDefinition.SeoContent;
            }

            return View(model);
        }

        [ChildActionOnly]
        public ActionResult _StoreCategoryGeneral()
        {

            MTStoreCategoryModel model = new MTStoreCategoryModel();
            var storeCategories = _categoryService.GetSPStoreCategoryByCategoryParentId(0).Where(x => x.CategoryType != (byte)CategoryType.Brand).ToList();
            IList<MTStoreCategoryItemModel> videoCategoryItemModels = new List<MTStoreCategoryItemModel>();
            foreach (var item in storeCategories)
            {

                var itemModel = new MTStoreCategoryItemModel
                {
                    CategoryUrl = UrlBuilder.GetStoreCategoryUrl(item.CategoryId, FormatHelper.GetCategoryNameWithSynTax(item.CategoryName, CategorySyntaxType.Store)),
                    CategoryType = item.CategoryType
                };

                if (item.CategoryType == (byte)CategoryTypeEnum.ProductGroup)
                {
                    var lastCategory = model.StoreTopCategoryItemModels.LastOrDefault();
                    string productGrupCategoryName = string.Format("{0}", item.CategoryName);
                    itemModel.CategoryName = FormatHelper.GetCategoryNameWithSynTax(productGrupCategoryName, CategorySyntaxType.Store);

                    //var subCategories = _storeService.GetSPStoreCategoryByCategoryParentId(item.CategoryId);
                    //foreach (var subItem in subCategories)
                    //{
                    //    itemModel.SubStoreCategoryItemModes.Add(new MTStoreCategoryItemModel
                    //    {
                    //        CategoryName = FormatHelper.GetCategoryNameWithSynTax(subItem.CategoryName, CategorySyntaxType.Store),
                    //        CategoryUrl = UrlBuilder.GetStoreCategoryUrl(subItem.CategoryId, FormatHelper.GetCategoryNameWithSynTax(subItem.CategoryName, CategorySyntaxType.Store), selectedOrderby),
                    //        CategoryType = subItem.CategoryType,
                    //        CategoryId = subItem.CategoryId
                    //    });
                    //}
                }
                //else if (item.CategoryType == (byte)CategoryTypeEnum.Brand)
                //{
                //    int topCategoryCount = model.StoreCategoryModel.StoreTopCategoryItemModels.Count;
                //    var elementCategory = model.StoreCategoryModel.StoreTopCategoryItemModels.ElementAt(topCategoryCount - 1);
                //    string brandCategoryName = string.Format("{0}", item.CategoryName);
                //    itemModel.CategoryName = FormatHelper.GetCategoryNameWithSynTax(brandCategoryName, CategorySyntaxType.Store);
                //    itemModel.CategoryUrl = UrlBuilder.GetStoreCategoryUrl(item.CategoryId, FormatHelper.GetCategoryNameWithSynTax(brandCategoryName, CategorySyntaxType.Store), selectedOrderby);
                //}

                else if (item.CategoryType == (byte)CategoryTypeEnum.Model)
                {
                    int topCategoryCount = model.StoreTopCategoryItemModels.Count;
                    var elementCategory = model.StoreTopCategoryItemModels.ElementAt(topCategoryCount - 1);
                    string modelCategoryName = string.Format("{0}", item.CategoryName);
                    itemModel.CategoryName = FormatHelper.GetCategoryNameWithSynTax(modelCategoryName, CategorySyntaxType.Store);
                    itemModel.CategoryUrl = UrlBuilder.GetStoreCategoryUrl(item.CategoryId, FormatHelper.GetCategoryNameWithSynTax(modelCategoryName, CategorySyntaxType.Store));
                }
                else
                {
                    itemModel.CategoryName = FormatHelper.GetCategoryNameWithSynTax(item.CategoryName, CategorySyntaxType.Store);
                    itemModel.CategoryUrl = UrlBuilder.GetStoreCategoryUrl(item.CategoryId, FormatHelper.GetCategoryNameWithSynTax(item.CategoryName, CategorySyntaxType.Store));
                }
                if (itemModel.CategoryType != 5)
                {
                    videoCategoryItemModels.Add(itemModel);
                }
            }

            model.StoreCategoryItemModels = videoCategoryItemModels;
            return PartialView(model);
        }


        [HttpGet]
        public  ActionResult _HeaderStoreCategoryGeneral()
        {

            MTStoreCategoryModel model = new MTStoreCategoryModel();
            var storeCategories = _categoryService.GetSPStoreCategoryByCategoryParentId(0).Where(x => x.CategoryType != (byte)CategoryType.Brand).ToList();
            IList<MTStoreCategoryItemModel> videoCategoryItemModels = new List<MTStoreCategoryItemModel>();
            foreach (var item in storeCategories)
            {
                string storePageTitle = "";
                if (!string.IsNullOrEmpty(item.StorePageTitle))
                {
                    if (item.StorePageTitle.Contains("Firma"))
                    {
                        storePageTitle = FormatHelper.GetCategoryNameWithSynTax(item.StorePageTitle, CategorySyntaxType.CategoryNameOnyl);
                    }
                    else
                    {
                        storePageTitle = FormatHelper.GetCategoryNameWithSynTax(item.StorePageTitle, CategorySyntaxType.Store);

                    }
                }
                else if (!string.IsNullOrEmpty(item.CategoryContentTitle))
                    storePageTitle = FormatHelper.GetCategoryNameWithSynTax(item.CategoryContentTitle, CategorySyntaxType.Store);
                else
                    storePageTitle = FormatHelper.GetCategoryNameWithSynTax(item.CategoryName, CategorySyntaxType.Store);

                var itemModel = new MTStoreCategoryItemModel
                {
                    CategoryUrl = UrlBuilder.GetStoreCategoryUrl(item.CategoryId, storePageTitle),
                    CategoryType = item.CategoryType
                };

                if (item.CategoryType == (byte)CategoryTypeEnum.ProductGroup)
                {
                    var lastCategory = model.StoreTopCategoryItemModels.LastOrDefault();
                    string productGrupCategoryName = string.Format("{0}", item.CategoryName);
                    itemModel.CategoryName = FormatHelper.GetCategoryNameWithSynTax(productGrupCategoryName, CategorySyntaxType.CategoryNameOnyl);
                }
                else if (item.CategoryType == (byte)CategoryTypeEnum.Model)
                {
                    int topCategoryCount = model.StoreTopCategoryItemModels.Count;
                    var elementCategory = model.StoreTopCategoryItemModels.ElementAt(topCategoryCount - 1);
                    string modelCategoryName = string.Format("{0}", item.CategoryName);
                    itemModel.CategoryName = FormatHelper.GetCategoryNameWithSynTax(modelCategoryName, CategorySyntaxType.CategoryNameOnyl);
                    itemModel.CategoryUrl = UrlBuilder.GetStoreCategoryUrl(item.CategoryId, FormatHelper.GetCategoryNameWithSynTax(modelCategoryName, CategorySyntaxType.Store));
                }
                else
                {
                    itemModel.CategoryName = FormatHelper.GetCategoryNameWithSynTax(item.CategoryName, CategorySyntaxType.CategoryNameOnyl);
                    itemModel.CategoryUrl = UrlBuilder.GetStoreCategoryUrl(item.CategoryId, storePageTitle);
                }
                if (itemModel.CategoryType != 5)
                {
                    videoCategoryItemModels.Add(itemModel);
                }
            }

            model.StoreCategoryItemModels = videoCategoryItemModels;
            return PartialView(model);
        }

        public ActionResult StoreWrongUrl(int? categoryId)
        {
            if (categoryId.HasValue)
            {
                var category = _categoryService.GetCategoryByCategoryId(categoryId.Value);
                if (category == null)
                {
                    return RedirectPermanent(AppSettings.StoreAllUrl);
                }
                var storeUrl = UrlBuilder.GetStoreCategoryUrl(category.CategoryId, FormatHelper.GetCategoryNameWithSynTax(category.CategoryName, CategorySyntaxType.Store));
                return RedirectPermanent(storeUrl);
            }
            else
            {
                return RedirectPermanent(AppSettings.StoreAllUrl);
            }


        }



        #endregion

    }

}