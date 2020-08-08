using MakinaTurkiye.Caching;
using MakinaTurkiye.Core.Data;
using MakinaTurkiye.Data;
using MakinaTurkiye.Entities.StoredProcedures.Catalog;
using MakinaTurkiye.Entities.StoredProcedures.Stores;
using MakinaTurkiye.Entities.StoredProcedures.Videos;
using MakinaTurkiye.Entities.Tables.Catalog;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace MakinaTurkiye.Services.Catalog
{
    public class CategoryService : BaseService, ICategoryService
    {
        #region Constants

        private const string CATEGORIES_BY_MAIN_CATEGORIES_KEY = "makinaturkiye.category.main-category";
        private const string CATEGORIES_TOP_CATEGORIES_BY_CATEGORY_ID_KEY ="makinaturkiye.category.top-category.bycategoryId-{0}";
        private const string CATEGORIES_BOTTOM_CATEGORIES_BY_CATEGORY_ID_KEY = "makinaturkiye.category.bottom-category.bycategoryId-{0}";
        private const string CATEGORIES_BY_CATEGORYPARENT_ID_KEY = "makinaturkiye.category.bycategoryparentId-{0}";
        private const string CATEGORIES_BY_CATEGORY_IDS_KEY = "makinaturkiye.category.bycategoryIds-{0}";
        private const string CATEGORIES_BY_CATEGORY_NAME_KEY = "makinaturkiye.category.bycategoryname-{0}";
        private const string CATEGORIES_BY_CATEGORY_ID_KEY = "makinaturkiye.category.bycategoryId-{0}";
        private const string CATEGORIES_BY_MAIN_CATEGORY_TYPE_KEY = "makinaturkiye.category.bymaincategorytype-{0}";
        private const string CATEGORIES_BY_CATEGORYPARENT_ID_KEY_ASYNC ="makinaturkiye.category.bycategoryparentId-{0}-async";
        private const string CATEGORIES_SP_PRODUCTCATEGORY_FOR_SEARCH_TEXT_KEY = "makinaturkiye.category.sp.productcategory.forseachtext-{0}-{1}-{2}-{3}-{4}-{5}-{6}-{7}-{8}-{9}";
        private const string CATEGORIES_SP_PRODUCTCATEGORY_ONE_STEP_FOR_SEARCH_TEXT_KEY = "makinaturkiye.category.sp.productcategory.onestep.forseachtext-{0}";
        private const string CATEGORIES_SP_VIDEOCATEGORY_FOR_SEARCH_TEXT_KEY = "makinaturkiye.category.sp.videocategory.forseachtext-{0}-{1}";
        private const string CATEGORIES_SP_VIDEOCATEGORY_BY_MAINPARTY_ID_KEY = "makinaturkiye.category.sp.videocategory.bymainpartyid-{0}-{1}";
        private const string CATEGORIES_SP_BREADCRUMB_BY_CATEGORY_ID_KEY = "makinaturkiye.category.sp.breadcrub.bycategoryid-{0}";
        private const string CATEGORIES_BY_CATEGORYPARENT_IDS_KEY = "makinaturkiye.category.bycategoryparentIds-{0}-{1}";
        private const string CATEGORIES_BY_CATEGORYPARENT_ID_WITH_CATEGORY_TYPE_KEY = "makinaturkiye.category.bycategoryparentId-categorytype-{0}-{1}-{2}";
        private const string CATEGORIES_SP_STOREPROFILE_BY_MAINPARTY_ID_KEY = "makinaturkiye.category.sp.storeprofile.bymainpartyid-{0}";
        private const string CATEGORIES_BY_CATEGORY_TYPE_KEY = "makinaturkiye.category.bycategorytype-{0}-{1}";
        private const string CATEGORIES_BY_LESS_THAN_CATEGORY_TYPE_KEY = "makinaturkiye.category.lessthancategorytype-{0}-{1}-{2}";
        private const string CATEGORIES_SP_STORECATEGORY_BY_CATEGORYPARENT_ID_KEY = "makinaturkiye.category.sp.storecategory.bycategoryparentid-{0}";

        #endregion

        #region  Fields

        private readonly IDbContext _dbContext;
        private readonly IDataProvider _dataProvider;
        private readonly IRepository<Category> _categoryRepository;
        private readonly ICacheManager _cacheManager;
        private readonly IRepository<CategoryPlaceChoice> _categoryPlaceChoiceRepository;


        #endregion

        #region Ctor 

        public CategoryService(IDbContext dbContext, IDataProvider dataProvider,
            IRepository<Category> categoryRepository, ICacheManager cacheManager,
            IRepository<CategoryPlaceChoice> categoryPlaceChoiceRepository): base(cacheManager)
        {
            _dbContext = dbContext;
            _dataProvider = dataProvider;
            _categoryRepository = categoryRepository;
            _cacheManager = cacheManager;
            _categoryPlaceChoiceRepository = categoryPlaceChoiceRepository;

        }

        #endregion 

        #region Methods


        public IList<TopCategoryResult> GetSPTopCategories(int categoryId)
        {
            if (categoryId == 0)
                return new List<TopCategoryResult>();

            string key = string.Format(CATEGORIES_TOP_CATEGORIES_BY_CATEGORY_ID_KEY, categoryId);
            return _cacheManager.Get(key, () =>
            {
                var pCategoryId = _dataProvider.GetParameter();
                pCategoryId.ParameterName = "CategoryId";
                pCategoryId.Value = categoryId;
                pCategoryId.DbType = DbType.Int32;
                var topCategories =
                    _dbContext.SqlQuery<TopCategoryResult>("spCategoryTopCategoriesByCategoryId @CategoryId",
                        pCategoryId).ToList();
                return topCategories;
            });
        }

        public IList<BottomCategoryResult> GetSPBottomCategories(int categoryId)
        {
            if (categoryId == 0)
                return new List<BottomCategoryResult>();

            string key = string.Format(CATEGORIES_BOTTOM_CATEGORIES_BY_CATEGORY_ID_KEY, categoryId);
            return _cacheManager.Get(key, () =>
            {
                var pCategoryId = _dataProvider.GetParameter();
                pCategoryId.ParameterName = "CategoryId";
                pCategoryId.Value = categoryId;
                pCategoryId.DbType = DbType.Int32;
                var topCategories =
                    _dbContext.SqlQuery<BottomCategoryResult>("sp_BottomProductCategoryByCategoryId @CategoryId",
                        pCategoryId).ToList();
                return topCategories;
            });
        }

        public IList<TopCategoryResult> GetTopCategoriesForSearchProduct(string searchText, int categoryId = 0,
            int customFilterId = 0)
        {
            if (categoryId == 0)
                return new List<TopCategoryResult>();
            var pCategoryId = _dataProvider.GetParameter();
            pCategoryId.ParameterName = "CategoryId";
            pCategoryId.Value = categoryId;
            pCategoryId.DbType = DbType.Int32;

            var pSearchText = _dataProvider.GetParameter();
            pSearchText.ParameterName = "SearchText";
            pSearchText.Value = searchText;
            pSearchText.DbType = DbType.String;

            var pCustomFilterId = _dataProvider.GetParameter();
            pCustomFilterId.ParameterName = "CustomFilterId";
            pCustomFilterId.Value = customFilterId;
            pCustomFilterId.DbType = DbType.Int32;

            IDataReader reader = _dbContext.ExecuteDataReader("SP_GetProductCategoryForSearchProduct",
                CommandType.StoredProcedure, CommandBehavior.CloseConnection, pSearchText, pCategoryId, pCustomFilterId);
            var productCategory = reader.DataReaderToObjectList<TopCategoryResult>();
            return productCategory;
        }

        public IList<Category> GetCategoriesByCategoryParentId(int categoryParentId, bool showHidden=false, bool fromCache = true, bool isProductCount = true)
        {
            if (categoryParentId == 0)
                return new List<Category>();

            if (fromCache)
            {
                string key = string.Format(CATEGORIES_BY_CATEGORYPARENT_ID_KEY, categoryParentId);
                return _cacheManager.Get(key, () =>
                {
                    var query = _categoryRepository.Table;

                    if (!showHidden)
                        query = query.Where(c => c.Active == true);

                    query = query.Where(c => c.CategoryParentId == categoryParentId);
                    if (isProductCount)
                        query = query.Where(x => x.ProductCount > 0);

                    query = query.OrderBy(c => c.CategoryOrder).ThenBy(k => k.CategoryName);

                    var categories = query.ToList();
                    return categories;
                });
            }
            else
            {
                var query = _categoryRepository.Table;

                if (!showHidden)
                    query = query.Where(c => c.Active == true);

                query=query.Where( c => c.CategoryParentId == categoryParentId && c.ProductCount > 0);
                query=query.OrderBy(c => c.CategoryOrder).ThenBy(k => k.CategoryName);

                var categories = query.ToList();
                return categories;
            }

        }

        public IList<int> GetCategoryIdsByCategoryParentId(int categoryParentId, bool showHidden = false)
        {

            var categories = this.GetCategoriesByCategoryParentId(categoryParentId, showHidden);

            var categoryIds = categories.Select(c => c.CategoryId).ToList();
            return categoryIds;
        }

        public Category GetCategoryByCategoryId(int categoryId)
        {
            if (categoryId == 0)
                throw new ArgumentNullException("categoryId");

            string key = string.Format(CATEGORIES_BY_CATEGORY_ID_KEY, categoryId);
            //return _cacheManager.Get(key, () =>
            //{
            //    var category = _categoryRepository.Table.FirstOrDefault(c => c.CategoryId == categoryId);
            //    return category;
            //});

            var category = _categoryRepository.Table.FirstOrDefault(c => c.CategoryId == categoryId);
            return category;
        }

        public IList<ProductCategoryForSearchProductResult> GetSPProductCategoryForSearchProduct(string searchText,
            int categoryId = 0, int brandId = 0, int modelId = 0,
            int seriesId = 0, int searchType = 0, int countryId = 0, int cityId = 0, int localityId = 0,
            int customFilterId = 0)
        {
            string key = string.Format(CATEGORIES_SP_PRODUCTCATEGORY_FOR_SEARCH_TEXT_KEY, searchText, categoryId, 
                                brandId, modelId, seriesId, searchType, countryId, cityId, localityId, customFilterId);
            return _cacheManager.Get(key, () => 
            {
                var pSearchText = _dataProvider.GetParameter();
                pSearchText.ParameterName = "SearchText";
                pSearchText.Value = searchText ?? string.Empty;
                pSearchText.DbType = DbType.String;

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
                pSeriesId.Value = seriesId;
                pSeriesId.DbType = DbType.Int32;

                var pSearchType = _dataProvider.GetParameter();
                pSearchType.ParameterName = "SearchType";
                pSearchType.Value = searchType;
                pSearchType.DbType = DbType.Int32;

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

                var pCustomFilterId = _dataProvider.GetParameter();
                pCustomFilterId.ParameterName = "CustomFilterId";
                pCustomFilterId.Value = customFilterId;
                pCustomFilterId.DbType = DbType.Int32;

                IDataReader reader = _dbContext.ExecuteDataReader("SP_GetProductCategoryForSearchProduct_new2",
                    CommandType.StoredProcedure, CommandBehavior.CloseConnection, pSearchText, pCategoryId, pBrandId,
                    pModelId, pSeriesId, pSearchType, pCountryId, pCityId, pLocalityId, pCustomFilterId);
                var productCategory = reader.DataReaderToObjectList<ProductCategoryForSearchProductResult>();
                return productCategory;
            });
        }

        public IList<ProductCategoryForSearchProductResult> GetSPProductCategoryForSearchProductOneStep(
            string searchText)
        {
            //string key = string.Format(CATEGORIES_SP_PRODUCTCATEGORY_ONE_STEP_FOR_SEARCH_TEXT_KEY, searchText);
            //return _cacheManager.Get(key, () => 
            //{

            //});


            var pSearchText = _dataProvider.GetParameter();
            pSearchText.ParameterName = "SearchText";
            pSearchText.Value = searchText;
            pSearchText.DbType = DbType.String;

            IDataReader reader = _dbContext.ExecuteDataReader("SP_GetProductCategoryAndProductGroupsBySearch",
                CommandType.StoredProcedure, CommandBehavior.CloseConnection, pSearchText);
            var productCategory = reader.DataReaderToObjectList<ProductCategoryForSearchProductResult>();
            return productCategory;
        }

        public IList<CategoryForStoreProfileResult> GetCategoriesByStoreMainPartyId(int mainPartyId)
        {
            if (mainPartyId == 0)
                throw new ArgumentNullException("mainPartyId");

            string key = string.Format(CATEGORIES_SP_STOREPROFILE_BY_MAINPARTY_ID_KEY, mainPartyId);
            return _cacheManager.Get(key, () => 
            {
                var pMainPartyId = _dataProvider.GetParameter();
                pMainPartyId.ParameterName = "MainPartyId";
                pMainPartyId.Value = mainPartyId;
                pMainPartyId.DbType = DbType.Int32;
                const string sql = "SP_GetCategoryForStoreProfileByMainPartyId @MainPartyId ";
                return _dbContext.SqlQuery<CategoryForStoreProfileResult>(sql, pMainPartyId).ToList();
            });
        }

        public IList<VideoCategoryResult> GetSPVideoCategoryForSearchVideo(string searchText, int categoryId)
        {
            string key = string.Format(CATEGORIES_SP_VIDEOCATEGORY_FOR_SEARCH_TEXT_KEY, searchText, categoryId);
            var pCategoryId = _dataProvider.GetParameter();
            pCategoryId.ParameterName = "CategoryId";
            pCategoryId.Value = categoryId;
            pCategoryId.DbType = DbType.Int32;

            var pSearchText = _dataProvider.GetParameter();
            pSearchText.ParameterName = "SearchText";
            pSearchText.Value = searchText;
            pSearchText.DbType = DbType.String;

            IDataReader reader = _dbContext.ExecuteDataReader("SP_GetVideoCategoryForVideoSearch",
                CommandType.StoredProcedure, CommandBehavior.CloseConnection, pSearchText, pCategoryId);
            var videoCategory = reader.DataReaderToObjectList<VideoCategoryResult>();
            return videoCategory;
        }

        public IList<VideoCategoryResult> GetSPVideoCategoryByMainPartyId(int mainPartyId, int categoryId)
        {
            if (mainPartyId == 0)
                throw new ArgumentNullException("mainPartyId");


            string key = string.Format(CATEGORIES_SP_VIDEOCATEGORY_BY_MAINPARTY_ID_KEY, mainPartyId, categoryId);
            return _cacheManager.Get(key, () => 
            {


                var pMainPartyId = _dataProvider.GetParameter();
                pMainPartyId.ParameterName = "MainPartyId";
                pMainPartyId.Value = mainPartyId;
                pMainPartyId.DbType = DbType.Int32;

                var pCategoryId = _dataProvider.GetParameter();
                pCategoryId.ParameterName = "CategoryId";
                pCategoryId.Value = categoryId;
                pCategoryId.DbType = DbType.Int32;

                IDataReader reader = _dbContext.ExecuteDataReader("SP_GetVideoCategoryByMainPartyId",
                    CommandType.StoredProcedure, CommandBehavior.CloseConnection, pMainPartyId, pCategoryId);
                var videoCategory = reader.DataReaderToObjectList<VideoCategoryResult>();
                return videoCategory;
            });

        }

        public IList<StoreCategoryResult> GetSPStoreCategoryByCategoryParentId(int categoryParentId = 0)
        {
            //if (categoryParentId <= 0)
            //    throw new ArgumentNullException("categoryParentId");

            string key = string.Format(CATEGORIES_SP_STORECATEGORY_BY_CATEGORYPARENT_ID_KEY, categoryParentId);
            return _cacheManager.Get(key, () => 
            {
                var pCategoryParentId = _dataProvider.GetParameter();
                pCategoryParentId.ParameterName = "CategoryParentId";
                pCategoryParentId.Value = categoryParentId;
                pCategoryParentId.DbType = DbType.Int32;

                string sql = "SP_GetStoreCategoryByCategoryParentId @CategoryParentId";
                var storeCategories = _dbContext.SqlQuery<StoreCategoryResult>(sql, pCategoryParentId).ToList();
                return storeCategories;
            });
        }

        public IList<Category> GetBreadCrumbCategoriesByCategoryId(int categoryId)
        {
            if (categoryId < 0)
                return new List<Category>();

            string key = string.Format(CATEGORIES_SP_BREADCRUMB_BY_CATEGORY_ID_KEY, categoryId);
            return _cacheManager.Get(key, () => 
            {
                var pCategoryId = _dataProvider.GetParameter();
                pCategoryId.ParameterName = "CategoryId";
                pCategoryId.Value = categoryId;
                pCategoryId.DbType = DbType.Int32;

                IDataReader reader = _dbContext.ExecuteDataReader("SP_GetBreadCrumbCategoryByCategoryId",
                    CommandType.StoredProcedure, CommandBehavior.CloseConnection, pCategoryId);
                var categories = reader.DataReaderToObjectList<Category>();
                return categories;
            });
        }

        public IList<Category> GetMainCategories()
        {
            string key = CATEGORIES_BY_MAIN_CATEGORIES_KEY;
            return _cacheManager.Get(key, () =>
            {

                var query = _categoryRepository.Table;
                query = query.Where(c => c.CategoryParentId == null && c.CategoryType == (byte)CategoryTypeEnum.Sector);
                query = query.Where(c => c.Active == true && c.ProductCount > 0);

                query = query.OrderBy(c => c.CategoryName).ThenBy(c => c.CategoryName);

                var categories = query.ToList();
                return categories;

                //var result =
                //    _categoryRepository.Table.Where(
                //        c =>
                //            c.CategoryParentId == null && c.CategoryType == (byte)CategoryTypeEnum.Sector &&
                //            c.Active == true && c.ProductCount > 0).ToList();

                //result = result.OrderBy(c => c.CategoryName).ThenBy(c => c.CategoryName).ToList();
                //return result;
            });
        }


        public IList<Category> GetCategoriesByName(string Name)
        {
            string key = string.Format(CATEGORIES_BY_CATEGORY_NAME_KEY,Name);
            return _cacheManager.Get(key, () =>
            {
                var query = _categoryRepository.Table;
                query = query.Where(c =>(c.CategoryType== (byte)CategoryTypeEnum.Sector | c.CategoryType == (byte)CategoryTypeEnum.Category) && c.CategoryName.Contains(Name));
                query = query.OrderBy(c => c.CategoryName);
                return query.ToList();
            });
        }

        public IList<Category> GetAllCategories()
        {
            string key = string.Format(CATEGORIES_BY_CATEGORY_NAME_KEY, "GetAllCategories");
            return _cacheManager.Get(key, () =>
            {
                var query = _categoryRepository.Table;
                query = query.Where(c => (c.CategoryType == (byte)CategoryTypeEnum.Sector | c.CategoryType == (byte)CategoryTypeEnum.Category));
                query = query.OrderBy(c => c.CategoryName);
                return query.ToList();
            });
        }



        public IList<Category> GetCategoriesByCategoryIds(List<int> categoryIds)
        {
            if (categoryIds == null || categoryIds.Count == 0)
                return new List<Category>();

            string categoryIdsText = string.Empty;
            foreach (var item in categoryIds)
            {
                categoryIdsText += string.Format("{0},", item.ToString());
            }
            categoryIdsText = categoryIdsText.Substring(0, categoryIdsText.Length - 1);

            string key = string.Format(CATEGORIES_BY_CATEGORY_IDS_KEY, categoryIdsText);
            return _cacheManager.Get(key, () =>
            {
                var query = _categoryRepository.Table;
                query = query.Where(c => categoryIds.Contains(c.CategoryId));
                query = query.OrderBy(c => c.CategoryName);
                return query.ToList();
            });
        }

        public IList<Category> GetCategoriesByCategoryParentIds(List<int> categoryParentIds, bool showHidden = false)
        {
            if (categoryParentIds == null || categoryParentIds.Count == 0)
                return new List<Category>();

            string categoryParentIdsText = string.Empty;
            foreach (var item in categoryParentIds)
            {
                categoryParentIdsText += string.Format("{0},", item.ToString());
            }
            categoryParentIdsText = categoryParentIdsText.Substring(0, categoryParentIdsText.Length - 1);

            string key = string.Format(CATEGORIES_BY_CATEGORY_IDS_KEY, categoryParentIdsText,showHidden);
            return _cacheManager.Get(key, () => 
            {
                var query = _categoryRepository.Table;

                if (!showHidden)
                    query = query.Where(c => c.Active == true);

                query = query.Where(c => categoryParentIds.Contains(c.CategoryParentId.Value));

                query = query.OrderBy(c => c.CategoryName);

                var categories = query.ToList();
                return categories;
            });
        }

        public Task<List<Category>> GetCategoriesByCategoryParentIdAsync(int categoryParentId)
        {
            string key = string.Format(CATEGORIES_BY_CATEGORYPARENT_ID_KEY_ASYNC, categoryParentId);
            return _cacheManager.Get(key, () =>
            {
                var result =
                    _categoryRepository.Table.Where(
                        c => c.CategoryParentId == categoryParentId && c.ProductCount > 0 && c.Active == true);
                result = result.OrderBy(c => c.CategoryOrder).ThenBy(k => k.CategoryName);
                return result.ToListAsync();
            });
        }

        public IList<Category> GetCategoriesByCategoryParentIdWithCategoryType(int categoryParentId, CategoryTypeEnum categoryType, bool showHidden=false)
        {
            if (categoryParentId == 0)
                throw new ArgumentException("categoryParentId");

            string key = string.Format(CATEGORIES_BY_CATEGORYPARENT_ID_WITH_CATEGORY_TYPE_KEY, categoryParentId, (byte)categoryType,showHidden);
            return _cacheManager.Get(key, () => 
            {
                var query = _categoryRepository.Table;

                if (!showHidden)
                    query = query.Where(c => c.Active == true);

                query = query.Where(c => c.CategoryParentId == categoryParentId && c.CategoryType == (byte)categoryType);
                query = query.OrderBy(c => c.CategoryName);

                var categories = query.ToList();
                return categories;
            });
        }

        public IList<Category> GetCategoriesByMainCategoryType(MainCategoryTypeEnum mainCategoryType)
        {
            string key = string.Format(CATEGORIES_BY_MAIN_CATEGORY_TYPE_KEY, mainCategoryType);
            return _cacheManager.Get(key, () =>
            {
                var query = _categoryRepository.Table;
                query = query.Where(c => c.MainCategoryType == (byte)mainCategoryType && c.Active == true);

                var categories = query.OrderBy(c => c.CategoryOrder);
                return categories.ToList();
            });
        }

        public IList<Category> GetCategoriesByCategoryType(CategoryTypeEnum categoryType, bool showHidden = false)
        {
            string key = string.Format(CATEGORIES_BY_CATEGORY_TYPE_KEY, categoryType, showHidden);
            return _cacheManager.Get(key, () => 
            {
                var query = _categoryRepository.Table;

                if (!showHidden)
                    query = query.Where(c => c.Active == true);

                query = query.Where(c => c.CategoryType == (byte)categoryType);
                var categories = query.ToList();

                return categories;
            });
        }

        public IList<Category> GetCategoriesLessThanCategoryType(CategoryTypeEnum categoryType, bool showHidden = false, int categoryParentId = 0)
        {
            string key = string.Format(CATEGORIES_BY_LESS_THAN_CATEGORY_TYPE_KEY, categoryType, showHidden, categoryParentId);
            return _cacheManager.Get(key, () => 
            {
                var query = _categoryRepository.Table;

                if (!showHidden)
                    query = query.Where(c => c.Active == true);

                query = query.Where(c => c.CategoryType < (byte)categoryType);
                if (categoryParentId != 0)
                    query = query.Where(c => c.CategoryParentId == categoryParentId);
                query = query.OrderBy(x => x.CategoryOrder).ThenBy(x => x.CategoryName);
                var categories = query.ToList();

                return categories;
            });
        }

        public IList<AllSubCategoryItemResult> GetSPAllSubCategoriesByCategoryId(int categoryId)
        {
            var pCategoryId = _dataProvider.GetParameter();
            pCategoryId.ParameterName = "CategoryId";
            pCategoryId.Value = categoryId;
            pCategoryId.DbType = DbType.Int32;

            var categories = _dbContext.SqlQuery<AllSubCategoryItemResult>("SP_GetAllSubCategoriesByCategoryId @CategoryId", pCategoryId);
            return categories.ToList();
        }

        public bool IsCategoryHaveProduct(int categoryId, int storeId)
        {
            var pStoreId = _dataProvider.GetParameter();
            pStoreId.ParameterName = "StoreId";
            pStoreId.Value = storeId;
            pStoreId.DbType = DbType.Int32;

            var pCategoryId = _dataProvider.GetParameter();
            pCategoryId.ParameterName = "CategoryId";
            pCategoryId.Value = categoryId;
            pCategoryId.DbType = DbType.Int32;
            var count = _dbContext.SqlQuery<int>("SP_GetCategoryProductsCount @StoreId , @CategoryId", pStoreId, pCategoryId).First();
            return count > 0;
        }

        public void UpdateCategory(Category category)
        {
            if (category == null)
                throw new ArgumentNullException("category");

            _categoryRepository.Update(category);
        }

        public void InsertCategory(Category category)
        {
            if (category == null)
                throw new ArgumentNullException("category");

            _categoryRepository.Insert(category);
        }

        public void DeleteCategory(Category category)
        {
            if (category == null)
                throw new ArgumentNullException("category");

            _categoryRepository.Delete(category);
        }

        public void ClearAllCache()
        {
            _cacheManager.Clear();
        }




        #endregion

    }
}