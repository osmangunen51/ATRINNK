using MakinaTurkiye.Caching;
using MakinaTurkiye.Core;
using MakinaTurkiye.Core.Data;
using MakinaTurkiye.Data;
using MakinaTurkiye.Entities.StoredProcedures.Catalog;
using MakinaTurkiye.Entities.StoredProcedures.Stores;
using MakinaTurkiye.Entities.Tables.Catalog;
using MakinaTurkiye.Services.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;

namespace MakinaTurkiye.Services.Catalog
{
    public class ProductService : BaseService, IProductService
    {

        #region Constants
        private const string PRODUCTS_BY_NEWS_PRODUCTS_KEY = "makinaturkiye.product.newsproduct.bynoneparameter";
        private const string PRODUCTS_BY_PORULAR_PRODUCTS_KEY = "makinaturkiye.product.popularproduct.bynoneparameter";
        private const string PRODUCTS_BY_PRODUCT_ID_KEY = "makinaturkiye.product.byproductId-{0}";
        private const string PRODUCTS_BY_MAINPARTY_ID_AND_NON_PRODUCT_ID_KEY = "makinaturkiye.product.bymainpartyId-nonproductId-{0}-{1}-{2}";
        private const string PRODUCTS_BY_CATEGORY_ID_AND_NON_PRODUCT_ID_KEY = "makinaturkiye.product.bycategoryId-nonproductId-{0}-{1}-{2}";
        private const string PRODUCTS_MOSTVIEWPRODUCTS_BY_CATEGORY_ID_KEY = "makinaturkiye.product.mostviewproduct.bycategoryId-{0}-{1}";
        private const string PRODUCTS_BY_STORE_MAIN_PARTY_ID_KEY = "makinaturkiye.product.bystoremainpartyId-byparameter-{0}-{1}-{2}-{3}-{4}";
        private const string PRODUCTS_BY_CATEGORY_ID_KEY = "makinaturkiye.product.bycategoryid={0}";
        private const string PRODUCTS_BY_MAIN_PARTY_ID_KEY = "makinaturkiye.product.bymainpartyid={0}-{1}";

        private const string PRODUCTS_SP_CATEGORYPRODUCTS_BY_PARAMETER_KEY = "makinaturkiye.product.sp.categoryproduct.byparameter={0}-{1}-{2}-{3}-{4}-{5}-{6}-{7}-{8}-{9}-{10}-{11}";
        private const string PRODUCTS_SP_PRODUCTFORSTORE_BY_BRAND_ID_KEY = "makinaturkiye.product.sp.productforstore.bybrandid-{0}-{1}-{2}";
        private const string PRODUCTS_SP_PRODUCTFORSTORE_BY_MODEL_ID_KEY = "makinaturkiye.product.sp.productforstore.bymodelid-{0}-{1}-{2}";
        private const string PRODUCTS_SP_PRODUCTFORSTORE_BY_CATEGORY_ID_KEY = "makinaturkiye.product.sp.productforstore.bycategoryid-{0}-{1}-{2}";
        private const string PRODUCTS_SHOWCASE_KEY = "makinaturkiye.product.showcase";

        private const string CATEGORY_PATTERN_KEY = "makinaturkiye.category";
        private const string PRODUCTS_PATTERN_KEY = "makinaturkiye.product.";

        #endregion

        #region Fields

        private readonly IDbContext _dbContext;
        private readonly IDataProvider _dataProvider;
        private readonly IRepository<Product> _productRepository;
        private readonly ICacheManager _cacheManager;
        private readonly IFavoriteProductService _favoriteProductService;
        private readonly ICategoryService _categoryService;
        private readonly IConstantService _constantService;


        #endregion

        #region Ctor

        public ProductService(IDbContext dbContext, IDataProvider dataProvider, IRepository<Product> productRepository,
            ICacheManager cacheManager, IFavoriteProductService favoriteProductService, ICategoryService categoryService, IConstantService constantService) : base(cacheManager)
        {
            this._dbContext = dbContext;
            this._dataProvider = dataProvider;
            this._productRepository = productRepository;
            this._cacheManager = cacheManager;
            this._favoriteProductService = favoriteProductService;
            this._categoryService = categoryService;
            this._constantService = constantService;
        }

        #endregion

        #region Methods

        public IList<ProductForStoreResult> GetSPProductForStoreByCategoryId(int categoryId = 0, int memberMainPartyId = 0, int topCount = 5)
        {

            //if (categoryId <= 0)
            //    throw new ArgumentNullException("categoryId");

            //if (memberMainPartyId <= 0)
            //    throw new ArgumentNullException("memberMainPartyId");

            //if (topCount <= 0)
            //    throw new ArgumentNullException("topCount");


            string key = string.Format(PRODUCTS_SP_PRODUCTFORSTORE_BY_CATEGORY_ID_KEY, categoryId, memberMainPartyId, topCount);
            return _cacheManager.Get(key, () =>
            {
                var pCategoryId = _dataProvider.GetParameter();
                pCategoryId.ParameterName = "CategoryId";
                pCategoryId.Value = categoryId;
                pCategoryId.DbType = DbType.Int32;

                var pMemberMainPartyId = _dataProvider.GetParameter();
                pMemberMainPartyId.ParameterName = "MemberMainPartyId";
                pMemberMainPartyId.Value = memberMainPartyId;
                pMemberMainPartyId.DbType = DbType.Int32;

                var pTopCount = _dataProvider.GetParameter();
                pTopCount.ParameterName = "TopCount";
                pTopCount.Value = topCount;
                pTopCount.DbType = DbType.Int32;

                const string sql = "SP_GetProductForStoreByCategoryId @CategoryId, @MemberMainPartyId, @TopCount";
                var productsForStore = _dbContext.SqlQuery<ProductForStoreResult>(sql, pCategoryId, pMemberMainPartyId,
                    pTopCount);
                return productsForStore.ToList();
            });
        }

        public IList<ProductForStoreResult> GetSPProductForStoreByBrandId(int brandId = 0, int memberMainPartyId = 0,
            int topCount = 10)
        {

            if (brandId <= 0)
                throw new ArgumentNullException("brandId");

            if (memberMainPartyId <= 0)
                throw new ArgumentNullException("memberMainPartyId");

            if (topCount <= 0)
                throw new ArgumentNullException("topCount");

            string key = string.Format(PRODUCTS_SP_PRODUCTFORSTORE_BY_BRAND_ID_KEY, brandId, memberMainPartyId, topCount);
            return _cacheManager.Get(key, () =>
            {
                var pBrandId = _dataProvider.GetParameter();
                pBrandId.ParameterName = "BrandId";
                pBrandId.Value = brandId;
                pBrandId.DbType = DbType.Int32;

                var pMemberMainPartyId = _dataProvider.GetParameter();
                pMemberMainPartyId.ParameterName = "MemberMainPartyId";
                pMemberMainPartyId.Value = memberMainPartyId;
                pMemberMainPartyId.DbType = DbType.Int32;

                var pTopCount = _dataProvider.GetParameter();
                pTopCount.ParameterName = "TopCount";
                pTopCount.Value = topCount;
                pTopCount.DbType = DbType.Int32;

                const string sql = "SP_GetProductForStoreByBrandId @BrandId, @MemberMainPartyId, @TopCount";
                var productsForStore = _dbContext.SqlQuery<ProductForStoreResult>(sql, pBrandId, pMemberMainPartyId,
                    pTopCount);

                return productsForStore.ToList();
            });
        }

        public IList<ProductForStoreResult> GetSPProductForStoreByModelId(int modelId = 0, int memberMainPartyId = 0,
            int topCount = 10)
        {
            if (modelId <= 0)
                throw new ArgumentNullException("modelId");

            if (memberMainPartyId <= 0)
                throw new ArgumentNullException("memberMainPartyId");

            if (topCount <= 0)
                throw new ArgumentNullException("topCount");

            string key = string.Format(PRODUCTS_SP_PRODUCTFORSTORE_BY_MODEL_ID_KEY, modelId, memberMainPartyId, topCount);
            return _cacheManager.Get(key, () =>
            {
                var pModelId = _dataProvider.GetParameter();
                pModelId.ParameterName = "ModelId";
                pModelId.Value = modelId;
                pModelId.DbType = DbType.Int32;

                var pMemberMainPartyId = _dataProvider.GetParameter();
                pMemberMainPartyId.ParameterName = "MemberMainPartyId";
                pMemberMainPartyId.Value = memberMainPartyId;
                pMemberMainPartyId.DbType = DbType.Int32;

                var pTopCount = _dataProvider.GetParameter();
                pTopCount.ParameterName = "TopCount";
                pTopCount.Value = topCount;
                pTopCount.DbType = DbType.Int32;

                const string sql = "SP_GetProductForStoreByModelId @ModelId, @MemberMainPartyId, @TopCount";
                var productsForStore = _dbContext.SqlQuery<ProductForStoreResult>(sql, pModelId, pMemberMainPartyId,
                    pTopCount);
                return productsForStore.ToList();
            });
        }

        public IPagedList<WebSearchProductResult> SPWebSearch(string searchText, int categoryId = 0,
            int customFilterId = 0, int pageIndex = 0, int pageSize = 0)
        {
            var pCategoryId = _dataProvider.GetParameter();
            pCategoryId.ParameterName = "CategoryId";
            pCategoryId.Value = categoryId;
            pCategoryId.DbType = DbType.Int32;

            var pPageIndex = _dataProvider.GetParameter();
            pPageIndex.ParameterName = "PageIndex";
            pPageIndex.Value = pageIndex;
            pPageIndex.DbType = DbType.Int32;

            var pPageSize = _dataProvider.GetParameter();
            pPageSize.ParameterName = "PageSize";
            pPageSize.Value = pageSize;
            pPageSize.DbType = DbType.Int32;

            var pSearchText = _dataProvider.GetParameter();
            pSearchText.ParameterName = "SearchText";
            pSearchText.Value = searchText;
            pSearchText.DbType = DbType.String;

            var pCustomFilterId = _dataProvider.GetParameter();
            pCustomFilterId.ParameterName = "CustomFilterId";
            pCustomFilterId.Value = customFilterId;
            pCustomFilterId.DbType = DbType.Int32;

            var pTotalRecords = _dataProvider.GetParameter();
            pTotalRecords.ParameterName = "@TotalRecords";
            pTotalRecords.DbType = DbType.Int32;
            pTotalRecords.Direction = ParameterDirection.Output;

            var datatable = _dbContext.ExecuteDataTable("SP_WebSearchProduct", CommandType.StoredProcedure, pSearchText,
                pCategoryId, pCustomFilterId, pPageSize, pPageIndex, pTotalRecords);
            var products = datatable.DataTableToObjectList<WebSearchProductResult>();

            int totalRecords = (pTotalRecords.Value != DBNull.Value) ? Convert.ToInt32(pTotalRecords.Value) : 0;

            return new PagedList<WebSearchProductResult>(products, pageIndex, pageSize, totalRecords);
        }

        public IPagedList<WebCategoryProductResult> SPWebCategoryProduct(out List<FilterableCategoriesResult> filterableCategoryIds,
            out List<int> filterableCountryIds,
            out List<int> filterableCityIds, out List<int> filterableLocalityIds, out List<int> filterableBrandIds,
            out List<int> filterableModelIds, out List<int> filterableSeriesIds,
            out int newProductCount, out int usedProductCount, out int serviceProductCount,
            int categoryId, int brandId,
            int modelId, int seriresId, int searchTypeId, int mainPartyId, int countryId = 0, int cityId = 0,
            int localityId = 0, int orderById = 0, int pageIndex = 0, int pageSize = 0, string searchText = "")
        {

            filterableCountryIds = new List<int>();
            filterableCityIds = new List<int>();
            filterableLocalityIds = new List<int>();

            filterableCategoryIds = new List<FilterableCategoriesResult>();
            filterableBrandIds = new List<int>();
            filterableModelIds = new List<int>();
            filterableSeriesIds = new List<int>();


            var pCategoryId = _dataProvider.GetParameter();
            pCategoryId.ParameterName = "CategoryId";
            pCategoryId.Value = categoryId;
            pCategoryId.DbType = DbType.Int32;

            var pBrandId = _dataProvider.GetParameter();
            pBrandId.ParameterName = "BrandId";
            pBrandId.Value = brandId;
            pBrandId.DbType = DbType.Int32;

            var pModelId = _dataProvider.GetParameter();
            pModelId.ParameterName = "ModelId";
            pModelId.Value = modelId;
            pModelId.DbType = DbType.Int32;

            var pSeriesId = _dataProvider.GetParameter();
            pSeriesId.ParameterName = "SeriresId";
            pSeriesId.Value = seriresId;
            pSeriesId.DbType = DbType.Int32;

            var pSearchType = _dataProvider.GetParameter();
            pSearchType.ParameterName = "SearchType";
            pSearchType.Value = searchTypeId;
            pSearchType.DbType = DbType.Int16;

            var pMainPartyId = _dataProvider.GetParameter();
            pMainPartyId.ParameterName = "MainPartyId";
            pMainPartyId.Value = mainPartyId;
            pMainPartyId.DbType = DbType.Int32;

            var pOrderBy = _dataProvider.GetParameter();
            pOrderBy.ParameterName = "OrderBy";
            pOrderBy.Value = orderById;
            pOrderBy.DbType = DbType.Int16;

            var pCountryId = _dataProvider.GetParameter();
            pCountryId.ParameterName = "CountryId";
            pCountryId.Value = countryId;
            pCountryId.DbType = DbType.Int32;

            var pCityId = _dataProvider.GetParameter();
            pCityId.ParameterName = "CityId";
            pCityId.Value = cityId;
            pCityId.DbType = DbType.Int32;

            var pLocalityId = _dataProvider.GetParameter();
            pLocalityId.ParameterName = "LocalityId";
            pLocalityId.Value = localityId;
            pLocalityId.DbType = DbType.Int32;

            var pPageIndex = _dataProvider.GetParameter();
            pPageIndex.ParameterName = "PageIndex";
            pPageIndex.Value = pageIndex;
            pPageIndex.DbType = DbType.Int32;

            var pPageSize = _dataProvider.GetParameter();
            pPageSize.ParameterName = "PageSize";
            pPageSize.Value = pageSize;
            pPageSize.DbType = DbType.Int32;

            var pFilterableCategoryIds = _dataProvider.GetParameter();
            pFilterableCategoryIds.ParameterName = "FilterableCategoryIds";
            pFilterableCategoryIds.Direction = ParameterDirection.Output;
            pFilterableCategoryIds.Size = int.MaxValue - 1;
            pFilterableCategoryIds.DbType = DbType.String;

            var pFilterableBrandIds = _dataProvider.GetParameter();
            pFilterableBrandIds.ParameterName = "FilterableBrandIds";
            pFilterableBrandIds.Direction = ParameterDirection.Output;
            pFilterableBrandIds.Size = int.MaxValue - 1;
            pFilterableBrandIds.DbType = DbType.String;

            var pFilterableModelIds = _dataProvider.GetParameter();
            pFilterableModelIds.ParameterName = "FilterableModelIds";
            pFilterableModelIds.Direction = ParameterDirection.Output;
            pFilterableModelIds.Size = int.MaxValue - 1;
            pFilterableModelIds.DbType = DbType.String;

            var pFilterableSeriesIds = _dataProvider.GetParameter();
            pFilterableSeriesIds.ParameterName = "FilterableSeriesIds";
            pFilterableSeriesIds.Direction = ParameterDirection.Output;
            pFilterableSeriesIds.Size = int.MaxValue - 1;
            pFilterableSeriesIds.DbType = DbType.String;

            var pFilterableCountryIds = _dataProvider.GetParameter();
            pFilterableCountryIds.ParameterName = "FilterableCountryIds";
            pFilterableCountryIds.Direction = ParameterDirection.Output;
            pFilterableCountryIds.Size = int.MaxValue - 1;
            pFilterableCountryIds.DbType = DbType.String;

            var pFilterableCityIds = _dataProvider.GetParameter();
            pFilterableCityIds.ParameterName = "FilterableCityIds";
            pFilterableCityIds.Direction = ParameterDirection.Output;
            pFilterableCityIds.Size = int.MaxValue - 1;
            pFilterableCityIds.DbType = DbType.String;

            var pFilterableLocalityIds = _dataProvider.GetParameter();
            pFilterableLocalityIds.ParameterName = "FilterableLocalityIds";
            pFilterableLocalityIds.Direction = ParameterDirection.Output;
            pFilterableLocalityIds.Size = int.MaxValue - 1;
            pFilterableLocalityIds.DbType = DbType.String;

            var pSearchText = _dataProvider.GetParameter();
            pSearchText.ParameterName = "SearchText";
            pSearchText.Value = searchText;
            pSearchText.DbType = DbType.String;

            var pNewProductCount = _dataProvider.GetParameter();
            pNewProductCount.ParameterName = "NewProductCount";
            pNewProductCount.DbType = DbType.Int32;
            pNewProductCount.Direction = ParameterDirection.Output;

            var pUsedProductCount = _dataProvider.GetParameter();
            pUsedProductCount.ParameterName = "UsedProductCount";
            pUsedProductCount.DbType = DbType.Int32;
            pUsedProductCount.Direction = ParameterDirection.Output;

            var pServiceProductCount = _dataProvider.GetParameter();
            pServiceProductCount.ParameterName = "ServiceProductCount";
            pServiceProductCount.DbType = DbType.Int32;
            pServiceProductCount.Direction = ParameterDirection.Output;

            var pTotalRecords = _dataProvider.GetParameter();
            pTotalRecords.ParameterName = "TotalRecord";
            pTotalRecords.DbType = DbType.Int32;
            pTotalRecords.Direction = ParameterDirection.Output;


            var datatable = _dbContext.ExecuteDataTable("SP_ProductWebSearchLast", CommandType.StoredProcedure,
            pCategoryId, pBrandId, pModelId, pSeriesId, pSearchType, pOrderBy,
            pCountryId, pCityId, pLocalityId, pPageSize, pPageIndex, pSearchText, pMainPartyId,
            pFilterableCategoryIds, pFilterableBrandIds, pFilterableModelIds,
            pFilterableSeriesIds, pFilterableCountryIds,
            pFilterableCityIds, pFilterableLocalityIds, pTotalRecords, pNewProductCount, pUsedProductCount, pServiceProductCount);
            var products = datatable.DataTableToObjectList<WebCategoryProductResult>();

            int totalRecords = (pTotalRecords.Value != DBNull.Value) ? Convert.ToInt32(pTotalRecords.Value) : 0;
            newProductCount = (pNewProductCount.Value != DBNull.Value) ? Convert.ToInt32(pNewProductCount.Value) : 0;
            usedProductCount = (pUsedProductCount.Value != DBNull.Value) ? Convert.ToInt32(pUsedProductCount.Value) : 0;
            serviceProductCount = (pServiceProductCount.Value != DBNull.Value) ? Convert.ToInt32(pServiceProductCount.Value) : 0;


            string filterableCategoryIdStr = (pFilterableCategoryIds.Value != DBNull.Value) ? (string)pFilterableCategoryIds.Value : string.Empty;
            if (!string.IsNullOrEmpty(filterableCategoryIdStr))
            {
                var filterableCategoriesStr = filterableCategoryIdStr.Split(',');
                foreach (var item in filterableCategoriesStr)
                {
                    var filterableCategoryResult = new FilterableCategoriesResult
                    {
                        CategoryId = Convert.ToInt32(item.Split('~')[0]),
                        ProductCount = Convert.ToInt32(item.Split('~')[1])
                    };
                    filterableCategoryIds.Add(filterableCategoryResult);
                }
            }

            string filterableBrandIdStr = (pFilterableBrandIds.Value != DBNull.Value) ? (string)pFilterableBrandIds.Value : string.Empty;
            if (!string.IsNullOrEmpty(filterableBrandIdStr))
            {
                filterableBrandIds = filterableBrandIdStr
                    .Split(new[] { ',' })
                    .Select(x => Convert.ToInt32(x.Trim()))
                    .ToList();
            }

            string filterableModelIdStr = (pFilterableModelIds.Value != DBNull.Value) ? (string)pFilterableModelIds.Value : string.Empty;
            if (!string.IsNullOrEmpty(filterableModelIdStr))
            {
                filterableModelIds = filterableModelIdStr
                    .Split(new[] { ',' })
                    .Select(x => Convert.ToInt32(x.Trim()))
                    .ToList();
            }

            string filterableSeriesIdStr = (pFilterableSeriesIds.Value != DBNull.Value) ? (string)pFilterableSeriesIds.Value : string.Empty;
            if (!string.IsNullOrEmpty(filterableSeriesIdStr))
            {
                filterableSeriesIds = filterableSeriesIdStr
                    .Split(new[] { ',' })
                    .Select(x => Convert.ToInt32(x.Trim()))
                    .ToList();
            }

            string filterableCountryIdsStr = (pFilterableCountryIds.Value != DBNull.Value) ? (string)pFilterableCountryIds.Value : string.Empty;
            if (!string.IsNullOrEmpty(filterableCountryIdsStr))
            {
                filterableCountryIds = filterableCountryIdsStr
                    .Split(new[] { ',' })
                    .Select(x => Convert.ToInt32(x.Trim()))
                    .ToList();
            }

            string filterableCityIdsStr = (pFilterableCityIds.Value != DBNull.Value) ? (string)pFilterableCityIds.Value : string.Empty;
            if (!string.IsNullOrEmpty(filterableCityIdsStr))
            {
                filterableCityIds = filterableCityIdsStr
                    .Split(new[] { ',' })
                    .Select(x => Convert.ToInt32(x.Trim()))
                    .ToList();
            }

            string filterableLocalityIdStr = (pFilterableLocalityIds.Value != DBNull.Value) ? (string)pFilterableLocalityIds.Value : string.Empty;
            if (!string.IsNullOrEmpty(filterableLocalityIdStr))
            {
                filterableLocalityIds = filterableLocalityIdStr
                    .Split(new[] { ',' })
                    .Select(x => Convert.ToInt32(x.Trim()))
                    .ToList();
            }

            return new PagedList<WebCategoryProductResult>(products, pageIndex, pageSize, totalRecords);
        }

        public IList<PopularProductResult> GetSPPopularProducts()
        {

            string key = PRODUCTS_BY_PORULAR_PRODUCTS_KEY;

            return _cacheManager.Get(key, () =>
            {
                const int topCount = 36;
                var pTopCount = _dataProvider.GetParameter();
                pTopCount.ParameterName = "TopCount";
                pTopCount.Value = topCount;
                pTopCount.DbType = DbType.Int32;

                var popularProducts = _dbContext.SqlQuery<PopularProductResult>("SP_GetPopularProductTopCount @TopCount", pTopCount);
                return popularProducts.ToList();
            });
        }


        public IList<PopularProductResult> GetSPNewProducts()
        {

            string key = PRODUCTS_BY_NEWS_PRODUCTS_KEY;

            return _cacheManager.Get(key, () =>
            {
                const int topCount = 36;
                var pTopCount = _dataProvider.GetParameter();
                pTopCount.ParameterName = "TopCount";
                pTopCount.Value = topCount;
                pTopCount.DbType = DbType.Int32;

                var popularProducts = _dbContext.SqlQuery<PopularProductResult>("SP_GetNewsProductTopCount @TopCount", pTopCount);
                return popularProducts.ToList();
            });
        }



        public Product GetProductByProductId(int productId)
        {
            if (productId == 0)
                return null;

            string key = string.Format(PRODUCTS_BY_PRODUCT_ID_KEY, productId);
            return _cacheManager.Get(key, () =>
            {
                var query = _productRepository.Table;
                query = query.Include(p => p.Category);

                query = query.Include(p => p.Country);
                query = query.Include(p => p.City);
                query = query.Include(p => p.Locality);
                query = query.Include(p => p.Town);
                var product = query.FirstOrDefault(p => p.ProductId == productId);
                if (product != null)
                {
                    if (product.BrandId.HasValue)
                        product.Brand = _categoryService.GetCategoryByCategoryId(product.BrandId.Value);
                    if (product.ModelId.HasValue)
                        product.Model = _categoryService.GetCategoryByCategoryId(product.ModelId.Value);
                }
                return product;
            });
        }

        public Product GetProductByProductNo(string productNo)
        {
            if (string.IsNullOrEmpty(productNo))
                throw new ArgumentNullException("productNo");

            var query = _productRepository.Table;

            var product = query.FirstOrDefault(p => p.ProductNo == productNo);
            return product;
        }

        public IList<Product> GetProductsByProductName(string productName)
        {
            if (string.IsNullOrEmpty(productName))
                throw new ArgumentNullException("productName");

            var query = _productRepository.Table;
            query = query.Where(p => p.ProductName == productName);

            var products = query.ToList();
            return products;
        }

        public IList<Product> GetProductsByMainPartIdAndNonProductId(int mainPartId, int nonProductId, int topCount = 12)
        {
            if (mainPartId == 0)
                return new List<Product>();

            if (nonProductId == 0)
                return new List<Product>();

            string key = string.Format(PRODUCTS_BY_MAINPARTY_ID_AND_NON_PRODUCT_ID_KEY, mainPartId, nonProductId, topCount);
            return _cacheManager.Get(key, () =>
            {
                var query = _productRepository.Table;
                query =
                    query.Where(
                        p =>
                            p.MainPartyId == mainPartId && p.ProductId != nonProductId && p.ProductActive == true &&
                            p.ProductActiveType == 1);
                query = query.OrderBy(x => x.Sort).ThenBy(p => p.productrate);
                query = query.Take(topCount);
                return query.ToList();
            });
        }

        public IList<Product> GetProductsByMainPartyId(int mainPartyId, bool showHidden = false)
        {
            if (mainPartyId <= 0)
                throw new ArgumentNullException("mainPartyId");

            string key = string.Format(PRODUCTS_BY_MAIN_PARTY_ID_KEY, mainPartyId, showHidden);
            return _cacheManager.Get(key, () =>
            {
                var query = _productRepository.Table;

                if (!showHidden)
                    query = query.Where(p => p.ProductActiveType == 1 && p.ProductActive == true);

                query = query.Where(p => p.MainPartyId == mainPartyId);

                var products = query.ToList();
                return products;
            });
        }

        public IList<Product> GetAllProductsByMainPartyIds(List<int?> mainPartyIds, bool includeBrand = false)
        {
            if (mainPartyIds.Count == 0)
                throw new ArgumentNullException("mainPartyId");
            var query = _productRepository.Table;
            query = query.Include(x => x.Category);
            if (includeBrand == true)
            {
                query = query.Include(x => x.Brand);
                query = query.Include(x => x.Model);
            }
            query = query.Include(x => x.Country);
            query = query.Include(x => x.City);
            query = query.Include(x => x.Locality);

            query = query.Where(p => mainPartyIds.Contains(p.MainPartyId));
            return query.ToList();
        }

        public IList<Product> GetProductsByCategoryIdAndNonProductId(int categoryId, int nonProductId, int topCount = 12)
        {
            if (categoryId == 0)
                return new List<Product>();

            if (nonProductId == 0)
                return new List<Product>();

            string key = string.Format(PRODUCTS_BY_CATEGORY_ID_AND_NON_PRODUCT_ID_KEY, categoryId, nonProductId,
                topCount);
            return _cacheManager.Get(key, () =>
            {
                var query = _productRepository.Table;
                query =
                    query.Where(
                        p =>
                            p.CategoryId == categoryId && p.ProductId != nonProductId && p.ProductActive == true &&
                            p.ProductActiveType == 1);
                query = query.Take(topCount);
                query = query.OrderBy(p => p.productrate);
                return query.ToList();
            });
        }

        public IList<Product> GetProductsByCategoryId(int categoryId)
        {
            if (categoryId == 0)
                throw new ArgumentNullException("categoryId");

            string key = string.Format(PRODUCTS_BY_CATEGORY_ID_KEY, categoryId);
            return _cacheManager.Get(key, () =>
            {
                var query = _productRepository.Table;
                query = query.Where(p => p.CategoryId == categoryId && p.ProductActive == true && p.ProductActiveType == 1);

                var products = query.ToList();
                return products;
            });
        }

        public IList<Product> GetProductsWithPageNo(int PageNo=0, int PageSize=50)
        {
            var query = _productRepository.Table.OrderBy(x=>x.ProductId).Skip(PageNo * PageSize).Take(PageSize);
            return query.ToList();
        }


        public IList<Product> GetProductsAll()
        {
            var query = _productRepository.Table.OrderBy(x=>x.ProductId);
            return query.ToList();
        }

        public IList<Product> Search(out int TotolRowCount,string name="",string companyName="",string country = "",string town="",bool isnew=true,bool isold=true, bool sortByViews= true, bool sortByDate= true,decimal minPrice=0,decimal maxPrice=0,int PageNo = 0, int PageSize = 50)
        {
            List<int> CategoryIdList = _categoryService.GetCategoriesByName(companyName).Select(x=>x.CategoryId).ToList();
            var query = _productRepository.Table.OrderBy(x =>
            ( x.ProductName.Contains(name) | name == "") && (CategoryIdList.Contains((int)x.CategoryId) | companyName == "")
            &&
            (
                (x.ProductPrice >= minPrice| minPrice==0 ) && (x.ProductPrice <= maxPrice | maxPrice==0
                )
            )
            &&
            (x.City.CityName.Contains(town) | town=="")
            &&
            (x.Country.CountryName.Contains(country) | country == "")
            );

            query = query.OrderBy(x => x.ProductName);

            if (sortByViews)
            {
                query = query.OrderByDescending(x => x.ViewCount);
            }
            if (sortByDate)
            {
                query = query.OrderByDescending(x => x.ProductRecordDate);
            }
            TotolRowCount = query.Count();
            return query.Skip(PageNo * PageSize).Take(PageSize).ToList();
        }




        public IList<Product> GetProductsByCategoryIds(List<int> categoryIds)
        {
            if (categoryIds == null)
                throw new ArgumentNullException("categoryId");

            var query = _productRepository.Table;
            query = query.Where(p => TreeKontrolSonuc(categoryIds, p));
            return query.ToList();
        }

        public IList<MostViewProductResult> GetSPMostViewProductsByCategoryId(int categoryId, int topCount = 12)
        {
            if (categoryId == 0)
                return new List<MostViewProductResult>();

            string key = string.Format(PRODUCTS_MOSTVIEWPRODUCTS_BY_CATEGORY_ID_KEY, categoryId, topCount);
            return _cacheManager.Get(key, () =>
            {
                var pCategoryId = _dataProvider.GetParameter();
                pCategoryId.ParameterName = "CategoryId";
                pCategoryId.Value = categoryId;
                pCategoryId.DbType = DbType.Int32;

                var pTopCount = _dataProvider.GetParameter();
                pTopCount.ParameterName = "TopCount";
                pTopCount.Value = topCount;
                pTopCount.DbType = DbType.Int32;

                const string sql = "SP_GetMostViewProductByCategoryId @CategoryId,  @TopCount";
                var products = _dbContext.SqlQuery<MostViewProductResult>(sql, pCategoryId, pTopCount);
                return products.ToList();
            });
        }

        public IList<MostViewProductResult> GetSPMostViewProductsByCategoryIdRemind(int categoryId, int topCount,
            int selectedCategoryId)
        {
            var pCategoryId = _dataProvider.GetParameter();
            pCategoryId.ParameterName = "CategoryId";
            pCategoryId.Value = categoryId;
            pCategoryId.DbType = DbType.Int32;

            var pTopCount = _dataProvider.GetParameter();
            pTopCount.ParameterName = "TopCount";
            pTopCount.Value = topCount;
            pTopCount.DbType = DbType.Int32;

            var pSelectedCategoryId = _dataProvider.GetParameter();
            pSelectedCategoryId.ParameterName = "SelectedCategoryId";
            pSelectedCategoryId.Value = selectedCategoryId;
            pSelectedCategoryId.DbType = DbType.Int32;

            const string sql = "SP_GetMostViewProductByCategoryIdRemind @CategoryId,  @TopCount, @SelectedCategoryId";
            var products = _dbContext.SqlQuery<MostViewProductResult>(sql, pCategoryId, pTopCount, pSelectedCategoryId);
            return products.ToList();
        }

        public CategoryProductsResult GetCategoryProducts(int categoryId, int brandId, int modelId, int seriresId,
            int searchTypeId, int mainPartyId, int countryId = 0, int cityId = 0,
            int localityId = 0, int orderById = 0, int pageIndex = 0, int pageSize = 0, string searchText = "")
        {
            if (categoryId == 0)
                return new CategoryProductsResult();


            if (string.IsNullOrEmpty(searchText))
            {
                string key = string.Format(PRODUCTS_SP_CATEGORYPRODUCTS_BY_PARAMETER_KEY, categoryId, brandId, modelId, seriresId,
searchTypeId, mainPartyId, countryId, cityId, localityId, orderById, pageIndex, pageSize);
                return _cacheManager.Get(key, () =>
                {


                    var products = this.SPWebCategoryProduct(out List<FilterableCategoriesResult> filterableCategoryIds,
        out List<int> filterableCountryIds,
        out List<int> filterableCityIds, out List<int> filterableLocalityIds, out List<int> filterableBrandIds,
        out List<int> filterableModelIds, out List<int> filterableSeriesIds,
        out int newProductCount, out int usedProductCount, out int serviceProductCount,
        categoryId, brandId, modelId, seriresId,
        searchTypeId, mainPartyId, countryId, cityId, localityId, orderById, pageIndex, pageSize, searchText);

                    var result = new CategoryProductsResult
                    {
                        Products = products,
                        FilterableCategoryIds = filterableCategoryIds,
                        FilterableBrandIds = filterableBrandIds,
                        FilterableCityIds = filterableCityIds,
                        FilterableCountryIds = filterableCountryIds,
                        FilterableLocalityIds = filterableLocalityIds,
                        FilterableModelIds = filterableModelIds,
                        FilterableSeriesIds = filterableSeriesIds,
                        PageIndex = products.PageIndex,
                        PageSize = products.PageSize,
                        TotalCount = products.TotalCount,
                        TotalPages = products.TotalPages,
                        NewProductCount = newProductCount,
                        UsedProductCount = usedProductCount,
                        ServicesProductCount = serviceProductCount

                    };
                    return result;
                });

            }
            else
            {
                var products = this.SPWebCategoryProduct(out List<FilterableCategoriesResult> filterableCategoryIds,
          out List<int> filterableCountryIds,
          out List<int> filterableCityIds, out List<int> filterableLocalityIds, out List<int> filterableBrandIds,
          out List<int> filterableModelIds, out List<int> filterableSeriesIds,
          out int newProductCount, out int usedProductCount, out int serviceProductCount,
          categoryId, brandId, modelId, seriresId,
          searchTypeId, mainPartyId, countryId, cityId, localityId, orderById, pageIndex, pageSize, searchText);

                var result = new CategoryProductsResult
                {
                    Products = products,
                    FilterableCategoryIds = filterableCategoryIds,
                    FilterableBrandIds = filterableBrandIds,
                    FilterableCityIds = filterableCityIds,
                    FilterableCountryIds = filterableCountryIds,
                    FilterableLocalityIds = filterableLocalityIds,
                    FilterableModelIds = filterableModelIds,
                    FilterableSeriesIds = filterableSeriesIds,
                    PageIndex = products.PageIndex,
                    PageSize = products.PageSize,
                    TotalCount = products.TotalCount,
                    TotalPages = products.TotalPages,
                    NewProductCount = newProductCount,
                    UsedProductCount = usedProductCount,
                    ServicesProductCount = serviceProductCount

                };
                return result;
            }

        }


        public IList<StoreProfileProductsResult> GetSPProductsByStoreMainPartyId(int pageDimension,
            int page, int storeMainPartyId, int mainPartyId = 0, byte searchType = 0)
        {

            string key = string.Format(PRODUCTS_BY_STORE_MAIN_PARTY_ID_KEY, pageDimension, page, storeMainPartyId, mainPartyId, searchType);
            return _cacheManager.Get(key, () =>
            {

                var pTotalRecord = _dataProvider.GetParameter();
                pTotalRecord.ParameterName = "TotalRecord";
                pTotalRecord.DbType = DbType.Int32;
                pTotalRecord.Direction = ParameterDirection.Output;

                var pPageDimension = _dataProvider.GetParameter();
                pPageDimension.ParameterName = "PageDimension";
                pPageDimension.Value = pageDimension;
                pPageDimension.DbType = DbType.Int32;

                var pPage = _dataProvider.GetParameter();
                pPage.ParameterName = "Page";
                pPage.Value = page;
                pPage.DbType = DbType.Int32;

                var pMainPartyId = _dataProvider.GetParameter();
                pMainPartyId.ParameterName = "MainPartyId";
                pMainPartyId.Value = storeMainPartyId;
                pMainPartyId.DbType = DbType.Int32;

                var pUserMainPartyId = _dataProvider.GetParameter();
                pUserMainPartyId.ParameterName = "UserMainPartyId";
                pUserMainPartyId.Value = mainPartyId;
                pUserMainPartyId.DbType = DbType.Int32;


                var pSearchType = _dataProvider.GetParameter();
                pSearchType.ParameterName = "SearchType";
                pSearchType.Value = searchType;
                pSearchType.DbType = DbType.Int32;

                var products = _dbContext.SqlQuery<StoreProfileProductsResult>(
                        "SP_GetProductsByMainPartyId_new1 @TotalRecord output,@PageDimension,@Page,@MainPartyId,@SearchType, @UserMainPartyId",
                         pTotalRecord, pPageDimension, pPage, pMainPartyId, pSearchType, pUserMainPartyId).ToList();
                return products;
            });

            //totalRecord = (pTotalRecord.Value != DBNull.Value) ? Convert.ToInt32(pTotalRecord.Value) : 0;

        }

        public IList<StoreProfileProductsResult> GetSPProductsByStoreMainPartyIdAndCategoryId(out int totalRecord,
            int pageDimension, int page, int storeMainPartyId, int categoryId, int userMainPartyId = 0)
        {

            var pTotalRecord = _dataProvider.GetParameter();
            pTotalRecord.ParameterName = "TotalRecord";
            pTotalRecord.DbType = DbType.Int32;
            pTotalRecord.Direction = ParameterDirection.Output;

            var pPageDimension = _dataProvider.GetParameter();
            pPageDimension.ParameterName = "PageDimension";
            pPageDimension.Value = pageDimension;
            pPageDimension.DbType = DbType.Int32;

            var pPage = _dataProvider.GetParameter();
            pPage.ParameterName = "Page";
            pPage.Value = page;
            pPage.DbType = DbType.Int32;

            var pMainPartyId = _dataProvider.GetParameter();
            pMainPartyId.ParameterName = "MainPartyId";
            pMainPartyId.Value = storeMainPartyId;
            pMainPartyId.DbType = DbType.Int32;

            var pCategoryId = _dataProvider.GetParameter();
            pCategoryId.ParameterName = "CategoryId";
            pCategoryId.Value = categoryId;
            pCategoryId.DbType = DbType.Int32;

            var pUserMainPartyId = _dataProvider.GetParameter();
            pUserMainPartyId.ParameterName = "UserMainPartyId";
            pUserMainPartyId.Value = userMainPartyId;
            pUserMainPartyId.DbType = DbType.Int32;

            var products =
                _dbContext.SqlQuery<StoreProfileProductsResult>(
                    "SP_GetProductsByMainPartyIdAndCategoryIdNew @TotalRecord output,@PageDimension,@Page,@CategoryId,@MainPartyId, @UserMainPartyId",
                    pTotalRecord, pPageDimension, pPage, pCategoryId, pMainPartyId, pUserMainPartyId).ToList();

            totalRecord = (pTotalRecord.Value != DBNull.Value) ? Convert.ToInt32(pTotalRecord.Value) : 0;

            return products;
        }

        public IList<string> GetProductsByTerm(string term, int pageSize)
        {
            //TODO AdilD bu servis auto complate icin yazildi
            if (string.IsNullOrWhiteSpace(term))
                return new List<String>();

            var pSearchText = _dataProvider.GetParameter();
            pSearchText.ParameterName = "Term";
            pSearchText.Value = term;
            pSearchText.DbType = DbType.String;

            var pPageSize = _dataProvider.GetParameter();
            pPageSize.ParameterName = "PageSize";
            pPageSize.Value = pageSize;
            pPageSize.DbType = DbType.Int32;


            var result =
                _dbContext.SqlQuery<String>("SP_ProductSearchForAutoComplate @Term,@PageSize", pSearchText, pPageSize)
                    .ToList();

            return result;
        }

        public IList<Product> GetRandomProductsByBill(int take)
        {
            if (take == 0)
                throw new ArgumentNullException();
            var query = _productRepository.Table;
            query = query.Where(x => x.ProductActive == true).OrderBy(x => Guid.NewGuid()).Take(take);
            return query.ToList();
        }

        public int GetProductCountBySearchType(int categoryId, int brandId, int modelId, int seriresId, int searchTypeId,
           string searchText, int countryId = 0, int cityId = 0, int localityId = 0)
        {




            var pCategoryId = _dataProvider.GetParameter();
            pCategoryId.ParameterName = "CategoryId";
            pCategoryId.Value = categoryId;
            pCategoryId.DbType = DbType.Int32;

            var pBrandId = _dataProvider.GetParameter();
            pBrandId.ParameterName = "BrandId";
            pBrandId.Value = brandId;
            pBrandId.DbType = DbType.Int32;

            var pModelId = _dataProvider.GetParameter();
            pModelId.ParameterName = "ModelId";
            pModelId.Value = modelId;
            pModelId.DbType = DbType.Int32;

            var pSeriesId = _dataProvider.GetParameter();
            pSeriesId.ParameterName = "SeriresId";
            pSeriesId.Value = seriresId;
            pSeriesId.DbType = DbType.Int32;

            var pSearchType = _dataProvider.GetParameter();
            pSearchType.ParameterName = "SearchType";
            pSearchType.Value = searchTypeId;
            pSearchType.DbType = DbType.Int16;

            var pCountryId = _dataProvider.GetParameter();
            pCountryId.ParameterName = "CountryId";
            pCountryId.Value = countryId;
            pCountryId.DbType = DbType.Int32;
            var pCityId = _dataProvider.GetParameter();
            pCityId.ParameterName = "CityId";
            pCityId.Value = cityId;
            pCityId.DbType = DbType.Int32;

            var pLocalityId = _dataProvider.GetParameter();
            pLocalityId.ParameterName = "LocalityId";
            pLocalityId.Value = localityId;
            pLocalityId.DbType = DbType.Int32;

            var pSearchText = _dataProvider.GetParameter();
            pSearchText.ParameterName = "SearchText";
            pSearchText.Value = "\"" + searchText + "\"";
            pSearchText.DbType = DbType.String;


            var pTotalRecords = _dataProvider.GetParameter();
            pTotalRecords.ParameterName = "TotalRecord";
            pTotalRecords.DbType = DbType.Int32;
            pTotalRecords.Direction = ParameterDirection.Output;


            var datatable = _dbContext.ExecuteDataTable("SP_GetProductCountBySearchType_new", CommandType.StoredProcedure, pCategoryId, pBrandId, pModelId, pSeriesId, pSearchType,
                                                pCountryId, pCityId, pLocalityId, pSearchText, pTotalRecords);
            var products = datatable.DataTableToObjectList<WebCategoryProductResult>();
            int totalRecords = (pTotalRecords.Value != DBNull.Value) ? Convert.ToInt32(pTotalRecords.Value) : 0;
            return totalRecords;



        }

        public IList<Product> GetProductsForChoiced()
        {
            var query = _productRepository.Table;
            query = query.Where(x => x.ChoicedForCategoryIndex == true && x.ProductActive == true);
            return query.ToList();
        }

        public IList<AllSectorRandomProductResultModel> GetSectorRandomProductsByCategoryId(int categoryId)
        {
            var pCategoryId = _dataProvider.GetParameter();
            pCategoryId.ParameterName = "CategoryId";
            pCategoryId.Value = categoryId;
            pCategoryId.DbType = DbType.Int32;

            return _dbContext.SqlQuery<AllSectorRandomProductResultModel>("SP_GetAllSectorProductsRandomByCategoryId @CategoryId", pCategoryId).ToList();

        }

        public IList<ProductRecomandationResult> GetSPProductRecomandation(string categoryId, string modelId, string brandId)
        {
            var pCategoryId = _dataProvider.GetParameter();
            pCategoryId.ParameterName = "CategoryId";
            pCategoryId.Value = categoryId;
            pCategoryId.DbType = DbType.String;

            var pModelId = _dataProvider.GetParameter();
            pModelId.ParameterName = "ModelId";
            pModelId.Value = modelId;
            pModelId.DbType = DbType.String;

            var pBrandId = _dataProvider.GetParameter();
            pBrandId.ParameterName = "BrandId";
            pBrandId.Value = brandId;
            pBrandId.DbType = DbType.String;


            return _dbContext.SqlQuery<ProductRecomandationResult>("SP_GetProductRecomandation @CategoryId,@ModelId,@BrandId", pCategoryId, pModelId, pBrandId).ToList();
        }

        public IList<Product> GetProductsByProductIds(List<int> ProductIds, int take = 0)
        {
            if (ProductIds.Count == 0)
                throw new ArgumentNullException("productIds");
            var query = _productRepository.Table;
            if (take == 0)
            {
                query = query.Where(x => ProductIds.Contains(x.ProductId)).OrderBy(x => x.ProductHomePageOrder.Value);
                return query.ToList();
            }

            else
                return query.Where(x => ProductIds.Contains(x.ProductId)).OrderBy(x => x.ProductId).Skip(0).Take(take).ToList();
        }

        public IList<StoreProfileProductsResult> GetSPProductsCountByStoreMainPartyIdAndSearchType(out int totalRecord, int mainPartyId, byte searchType, int categoryId)
        {
            var pMainPartyId = _dataProvider.GetParameter();
            pMainPartyId.ParameterName = "MainPartyId";
            pMainPartyId.Value = mainPartyId;
            pMainPartyId.DbType = DbType.Int32;


            var pSearchType = _dataProvider.GetParameter();
            pSearchType.ParameterName = "SearchType";
            pSearchType.Value = searchType;
            pSearchType.DbType = DbType.Int32;

            var pCategoryId = _dataProvider.GetParameter();
            pCategoryId.ParameterName = "CategoryId";
            pCategoryId.Value = categoryId;
            pCategoryId.DbType = DbType.Int32;

            var pTotalRecord = _dataProvider.GetParameter();
            pTotalRecord.ParameterName = "TotalRecord";
            pTotalRecord.DbType = DbType.Int32;
            pTotalRecord.Direction = ParameterDirection.Output;

            var products =
                _dbContext.SqlQuery<StoreProfileProductsResult>(
                    "SP_GetProductsCountByMainPartyIdAndSearchType @TotalRecord output,@MainPartyId,@SearchType,@CategoryId", pTotalRecord,
                   pMainPartyId, pSearchType, pCategoryId).ToList();

            totalRecord = (pTotalRecord.Value != DBNull.Value) ? Convert.ToInt32(pTotalRecord.Value) : 0;

            return products;
        }

        public IList<Product> GetSPFavoriteProductsByMainPartyId(int mainPartyId, int page, int pageSize, out int totalRecord)
        {
            var pMainPartyId = _dataProvider.GetParameter();
            pMainPartyId.ParameterName = "MainPartyId";
            pMainPartyId.Value = mainPartyId;
            pMainPartyId.DbType = DbType.Int32;

            var pPageSize = _dataProvider.GetParameter();
            pPageSize.ParameterName = "PageDimension";
            pPageSize.Value = pageSize;
            pPageSize.DbType = DbType.Int32;

            var pPageIndex = _dataProvider.GetParameter();
            pPageIndex.ParameterName = "PageIndex";
            pPageIndex.Value = page;
            pPageIndex.DbType = DbType.Int32;

            var pTotalRecord = _dataProvider.GetParameter();
            pTotalRecord.ParameterName = "TotalRecord";
            pTotalRecord.DbType = DbType.Int32;
            pTotalRecord.Direction = ParameterDirection.Output;



            var favoriteProducts = _dbContext.SqlQuery<Product>("SP_GetFavoriteProductsByMainPartyId @MainPartyId,  @PageIndex, @PageDimension, @TotalRecord out", pMainPartyId, pPageIndex, pPageSize, pTotalRecord).ToList();
            totalRecord = (pTotalRecord.Value != DBNull.Value) ? Convert.ToInt32(pTotalRecord.Value) : 0;
            return favoriteProducts;

        }


        public IList<SiteMapProductResult> GetSiteMapProducts()
        {
            const string sql = "SP_GetSiteMapProduct";
            var products = _dbContext.SqlQuery<SiteMapProductResult>(sql).ToList();
            return products;
        }

        public int GetNumberOfProductsByMainPartyId(int mainPartyId)
        {
            if (mainPartyId < 0)
                throw new ArgumentNullException("mainPartyId");

            var query = _productRepository.Table;
            query = query.Where(p => p.MainPartyId == mainPartyId);

            int numberOfProducts = query.Count();
            return numberOfProducts;
        }

        public long GetViewOfProductsByMainPartyId(int mainPartyId)
        {
            if (mainPartyId < 0)
                throw new ArgumentNullException("mainPartyId");

            var query = _productRepository.Table;
            query = query.Where(p => p.MainPartyId == mainPartyId);

            long viewOfProducts = query.Sum(p => p.ViewCount.Value);
            return viewOfProducts;
        }

        public bool TreeKontrolSonuc(List<int> Liste, Product product)
        {
            bool Sonuc = false;
            Sonuc = (product.ProductActive == true && product.ProductActiveType == 1);
            if (!Sonuc)
            {
                foreach (var item in Liste)
                {
                    Sonuc = product.CategoryTreeName.Contains(item.ToString());
                    if (Sonuc)
                    {
                        break;
                    }
                }
            }
            return Sonuc;
        }


        public void CalculateSPProductRate()
        {
            _dbContext.ExecuteSqlCommand("exec ProductRateCalculate");
        }

        public void CheckSPProductSearch(int productId)
        {
            if (productId == 0)
                throw new ArgumentNullException("productId");

            string sqlCommand = String.Format("exec CheckProductSearch {0}", productId);
            _dbContext.ExecuteSqlCommand(sqlCommand);
        }

        public void InsertProduct(Product product)
        {
            if (product == null)
                throw new ArgumentNullException("product");

            product.ProductRecordDate = DateTime.Now;

            if (product.ProductPriceWithDiscount > 0)
                product.ProductPriceForOrder = product.ProductPriceWithDiscount;
            else if (product.ProductPriceType == 239 && product.ProductPriceBegin != 0)
                product.ProductPriceForOrder = product.ProductPriceBegin;
            else if (product.ProductPriceType == 238 && product.ProductPrice != 0)
                product.ProductPriceForOrder = product.ProductPrice;
            else
                product.ProductPriceForOrder = 99999999999;

            var constant = _constantService.GetConstantByConstantId(471);
            if (constant != null)
            {
                product.productrate = Convert.ToDecimal(constant.ConstantTitle);
            }

            _productRepository.Insert(product);
        }

        public void UpdateProduct(Product product, bool removeCache = false)
        {
            if (product == null)
                throw new ArgumentNullException("product");

            if (product.ProductPriceWithDiscount > 0)
                product.ProductPriceForOrder = product.ProductPriceWithDiscount;

            else if (product.ProductPriceType == 239 && product.ProductPriceBegin != 0)
                product.ProductPriceForOrder = product.ProductPriceBegin;
            else if (product.ProductPriceType == 238 && product.ProductPrice!=0)
                product.ProductPriceForOrder = product.ProductPrice;
            else
                product.ProductPriceForOrder = 99999999999;


            _productRepository.Update(product);


            if (removeCache)
            {
                _cacheManager.RemoveByPattern(PRODUCTS_PATTERN_KEY);
                _cacheManager.RemoveByPattern(CATEGORY_PATTERN_KEY);
            }

        }

        public IList<Product> GetProductByProductActiveType(ProductActiveTypeEnum productActiveTypeEnum)
        {
            var query = _productRepository.Table;
            query = query.Where(x => x.ProductActiveType == (byte)productActiveTypeEnum);
            return query.ToList();
        }

        public void DeleteProduct(Product product)
        {
            if (product == null)
                throw new ArgumentNullException("product");
            _productRepository.Delete(product);

            string key = string.Format(PRODUCTS_BY_PRODUCT_ID_KEY, product.ProductId);
            _cacheManager.Remove(key);
        }

        public void SPUpdateProductSearchCategoriesByCategoryId(int categoryId)
        {

            if (categoryId == 0)
                throw new ArgumentNullException("productId");

            string sqlCommand = String.Format("exec SP_UpdateProductSearchCategoriesByCategoryId {0}", categoryId);
            _dbContext.ExecuteSqlCommand(sqlCommand);
        }

        public IList<Product> GetProductsByShowCase()
        {
            string key = PRODUCTS_SHOWCASE_KEY;
            return _cacheManager.Get(key, () =>
            {
                var query = _productRepository.Table;
                return query.Where(x => x.ProductShowcase == true).ToList();
            });

        }
        #endregion

    }
}
