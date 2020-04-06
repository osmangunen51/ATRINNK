using MakinaTurkiye.Caching;
using MakinaTurkiye.Core;
using MakinaTurkiye.Core.Data;
using MakinaTurkiye.Data;
using MakinaTurkiye.Entities.StoredProcedures.Stores;
using MakinaTurkiye.Entities.Tables.Stores;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;

namespace MakinaTurkiye.Services.Stores
{
    public class StoreService : BaseService, IStoreService
    {
        #region Constants

        private const string STORES_BY_MAINPARTY_ID_KEY = "makinaturkiye.store.bymainpartyId-{0}";
        private const string STORES_BY_CATEGORY_ID_KEY = "makinaturkiye.store.bycategoryId-{0}";
        private const string STORES_BY_CATEGORY_ID_AND_BRAND_ID_KEY = "makinaturkiye.store.bycategoryId-brandId-{0}-{1}";
        private const string STORES_BY_SEARCH_KEY = "makinaturkiye.store.bysearchparameters-{0}-{1}-{2}-{3}";
        private const string STORES_HOME_KEY = "makinaturkiye.store.home";
        private const string STORES_BY_STORE_URL_NAME_KEY = "makinaturkiye.store.bystoreurlname-{0}";

        private const string STORECERTIFICATES_BY_MAIN_PARTY_ID_KEY = "makinaturkiye.storecertificates.bymainpartyid-{0}";

        private const string STORES_SP_CATEGORYSTORES_BY_PARAMETER_KEY = "makinaturkiye.store.sp.categorystore.byparameter={0}-{1}-{2}-{3}-{4}-{5}-{6}-{7}-{8}-{9}";

        private const string STORES_PATTERN_KEY = "makinaturkiye.store.";
        #endregion

        #region Fields

        private readonly IDbContext _dbContext;
        private readonly IDataProvider _dataProvider;
        private readonly IRepository<Store> _storeRepository;
        private readonly ICacheManager _cacheManager;
        private readonly IRepository<StoreActivityType> _storeActivityTypeRepository;
        private readonly IRepository<DealerBrand> _dealarBrandRepository;
        private readonly IRepository<StoreCertificate> _storeCertificateRepository;

        #endregion

        #region Ctor

        public StoreService(IDbContext dbContext, IDataProvider dataProvider, IRepository<Store> storeRepository, ICacheManager cacheManager, IRepository<StoreActivityType> storeActivityTypeRepository, IRepository<DealerBrand> dealarBrandRepository
            ,IRepository<StoreCertificate> storeCertificateRepository) : base(cacheManager)
        {
            this._dbContext = dbContext;
            this._dataProvider = dataProvider;
            this._storeRepository = storeRepository;
            this._cacheManager = cacheManager;
            this._storeActivityTypeRepository = storeActivityTypeRepository;
            this._dealarBrandRepository = dealarBrandRepository;
            this._storeCertificateRepository = storeCertificateRepository;
        }

        #endregion

        #region Methods

        public IList<Store> GetAllStores(StoreActiveTypeEnum? storeActiveType = null)
        {
            var query = _storeRepository.Table;
            
            if(storeActiveType!=null)
            {
                query = query.Where(s => s.StoreActiveType == (byte)storeActiveType);
            }

            query = query.OrderBy(s => s.MainPartyId);
            return query.ToList();
        }

        public IPagedList<WebSearchStoreResult> SPWebSearch(out IList<int> filterableCityIds, out IList<int> filterableLocalityIds, 
            out IList<int> filterableActivityIds, int categoryId = 0, int modelId = 0, int brandId = 0, int cityId = 0, 
            IList<int> localityIds = null,
            string searchText = "", int orderBy = 0, int pageIndex = 0, int pageSize = 0, string activityType = "")
        {

            filterableCityIds = new List<int>();
            filterableLocalityIds = new List<int>();
            filterableActivityIds = new List<int>();

            string localityIdsStr = "";
            if (localityIds != null && localityIds.Count>0)
            {
                for (int i = 0; i < localityIds.Count; i++)
                {
                    localityIdsStr += localityIds[i].ToString();
                    if (i != localityIds.Count - 1)
                    {
                        localityIdsStr += ",";
                    }
                }
            }

            var pCategoryId = _dataProvider.GetParameter();
            pCategoryId.ParameterName = "CategoryId";
            pCategoryId.Value = categoryId;
            pCategoryId.DbType = DbType.Int32;

            var pModelId = _dataProvider.GetParameter();
            pModelId.ParameterName = "ModelId";
            pModelId.Value = modelId;
            pModelId.DbType = DbType.Int32;

            var pBrandId = _dataProvider.GetParameter();
            pBrandId.ParameterName = "BrandId";
            pBrandId.Value = brandId;
            pBrandId.DbType = DbType.Int32;

            var pCityId = _dataProvider.GetParameter();
            pCityId.ParameterName = "CityId";
            pCityId.Value = cityId;
            pCityId.DbType = DbType.Int32;

            var pLocalityId = _dataProvider.GetParameter();
            pLocalityId.ParameterName = "LocalityIds";
            pLocalityId.Value = localityIdsStr;
            pLocalityId.DbType = DbType.String;

            var pSearchText = _dataProvider.GetParameter();
            pSearchText.ParameterName = "SearchText";
            pSearchText.Value = searchText;
            pSearchText.DbType = DbType.String;

            var pOrderBy = _dataProvider.GetParameter();
            pOrderBy.ParameterName = "OrderBy";
            pOrderBy.Value = orderBy;
            pOrderBy.DbType = DbType.Int32;

            var pActivityType = _dataProvider.GetParameter();
            pActivityType.ParameterName = "ActivityType";
            pActivityType.Value = activityType;
            pActivityType.DbType = DbType.String;

            var pPageIndex = _dataProvider.GetParameter();
            pPageIndex.ParameterName = "PageIndex";
            pPageIndex.Value = pageIndex;
            pPageIndex.DbType = DbType.Int32;

            var pPageSize = _dataProvider.GetParameter();
            pPageSize.ParameterName = "PageSize";
            pPageSize.Value = pageSize;
            pPageSize.DbType = DbType.Int32;


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

            var pFilterableAcitivtyIds = _dataProvider.GetParameter();
            pFilterableAcitivtyIds.ParameterName = "FilterableActivityIds";
            pFilterableAcitivtyIds.Direction = ParameterDirection.Output;
            pFilterableAcitivtyIds.Size = int.MaxValue - 1;
            pFilterableAcitivtyIds.DbType = DbType.String;


            var pTotalRecords = _dataProvider.GetParameter();
            pTotalRecords.ParameterName = "TotalRecords";
            pTotalRecords.DbType = DbType.Int32;
            pTotalRecords.Direction = ParameterDirection.Output;

            string sql = "SP_WebSearchStore_new1 @CategoryId, @ModelId, @BrandId, @CityId, @LocalityIds, @SearchText, @OrderBy, @PageIndex, @PageSize, @ActivityType," +
                         " @TotalRecords output, @FilterableCityIds output, @FilterableLocalityIds output, @FilterableActivityIds output";

            var stores = _dbContext.SqlQuery<WebSearchStoreResult>(sql,pCategoryId, pModelId, pBrandId, pCityId, pLocalityId, pSearchText, pOrderBy,
                pPageIndex, pPageSize, pActivityType, pTotalRecords, pFilterableCityIds, pFilterableLocalityIds, pFilterableAcitivtyIds).ToList();

            int totalRecords = (pTotalRecords.Value != DBNull.Value) ? Convert.ToInt32(pTotalRecords.Value) : 0;

           
            //get filterable 
            string filterableCityIdsStr = (pFilterableCityIds.Value != DBNull.Value) ? (string)pFilterableCityIds.Value : "";
            if (!string.IsNullOrEmpty(filterableCityIdsStr))
            {
                filterableCityIds = filterableCityIdsStr
                                  .Split(new[] { ',' })
                                  .Select(x => Convert.ToInt32(x.Trim()))
                                  .ToList();
            }

            string filterableLocalityIdStr = (pFilterableLocalityIds.Value != DBNull.Value) ? (string)pFilterableLocalityIds.Value : "";
            if (!string.IsNullOrEmpty(filterableLocalityIdStr))
            {
                filterableLocalityIds = filterableLocalityIdStr
                    .Split(new[] { ',' })
                    .Select(x => Convert.ToInt32(x.Trim()))
                    .ToList();
            }

            string filterableActivityIdStr = (pFilterableAcitivtyIds.Value != DBNull.Value) ? (string)pFilterableAcitivtyIds.Value : "";
            if (!string.IsNullOrEmpty(filterableActivityIdStr))
            {
                filterableActivityIds = filterableActivityIdStr
                    .Split(new[] { ',' })
                    .Select(x => Convert.ToInt32(x.Trim()))
                    .ToList();
            }

            return new PagedList<WebSearchStoreResult>(stores, pageIndex, pageSize, totalRecords);

        }


        public CategoryStoresResult GetCategoryStores(int categoryId = 0, int modelId = 0, int brandId = 0,
            int cityId = 0, IList<int> localityIds = null, string searchText = "",
            int orderBy = 0, int pageIndex = 0, int pageSize = 0, string activityType = "")
        {
            string localityIdsText = string.Empty;
            if(localityIds!=null && localityIds.Count>0)
            {
                foreach (var item in localityIds)
                {
                    localityIdsText += string.Format("{0},", item.ToString());
                }
                localityIdsText = localityIdsText.Substring(0, localityIdsText.Length - 1);
            }
            string key = string.Format(STORES_SP_CATEGORYSTORES_BY_PARAMETER_KEY, categoryId, modelId, brandId, 
                         cityId, localityIdsText, searchText, orderBy, pageIndex, pageSize, activityType);

            return _cacheManager.Get(key, () => 
            {
                var stores = this.SPWebSearch(out IList<int> filterableCityIds, out IList<int> filterableLocalityIds, out IList<int> filterableActivityIds,
                            categoryId, modelId, brandId, cityId, localityIds, searchText, orderBy, pageIndex, pageSize, activityType);

                var result = new CategoryStoresResult
                {
                    Stores = stores,
                    FilterableCityIds=filterableActivityIds,
                    FilterableLocalityIds=filterableLocalityIds,
                    FilterableActivityIds=filterableActivityIds,
                    PageIndex = stores.PageIndex,
                    PageSize = stores.PageSize,
                    TotalCount = stores.TotalCount,
                    TotalPages = stores.TotalPages,
                };
                return result;
            });
        }

        public IList<StoreForCategoryResult> GetSPStoresForCategoryByCategoryId(int categoryId)
        {
            string key = string.Format(STORES_BY_CATEGORY_ID_KEY, categoryId);
            return _cacheManager.Get(key, () =>
                 {
                     var pCategoryId = _dataProvider.GetParameter();
                     pCategoryId.ParameterName = "CategoryId";
                     pCategoryId.Value = categoryId;
                     pCategoryId.DbType = DbType.Int32;

                     var stores = _dbContext.SqlQuery<StoreForCategoryResult>("SP_GetStoreForCategoryByCategoryId @CategoryId", pCategoryId).ToList();
                     return stores;
                 });
        }

        public IList<StoreForCategoryResult> GetSPGetStoreForCategoryByCategoryIdAndBrandId(int categoryId = 0, int brandId = 0)
        {
            string key = string.Format(STORES_BY_CATEGORY_ID_AND_BRAND_ID_KEY, categoryId, brandId);
            return _cacheManager.Get(key, () =>
            {
                var pCategoryId = _dataProvider.GetParameter();
                pCategoryId.ParameterName = "CategoryId";
                pCategoryId.Value = categoryId;
                pCategoryId.DbType = DbType.Int32;

                var pBrandId = _dataProvider.GetParameter();
                pBrandId.ParameterName = "BrandId";
                pBrandId.Value = brandId;
                pBrandId.DbType = DbType.Int32;

                var stores = _dbContext.SqlQuery<StoreForCategoryResult>("SP_GetStoreForCategoryByCategoryIdAndBrandId @CategoryId, @BrandId", pCategoryId, pBrandId).ToList();
                return stores;
            });

        }

        public Store GetStoreByMainPartyId(int mainPartyId)
        {
            if (mainPartyId == 0)
                return null;

            string key = string.Format(STORES_BY_MAINPARTY_ID_KEY, mainPartyId);
            return _cacheManager.Get(key, () =>
            {
                var query = _storeRepository.Table;

                query = query.Include(s => s.StoreActivityCategories);

                return query.FirstOrDefault(s => s.MainPartyId == mainPartyId);
            });
        }

        public Store GetStoreByStoreEmail(string storeEmail)
        {
            if (string.IsNullOrEmpty(storeEmail))
                throw new ArgumentNullException("storeEmail");

            var query = _storeRepository.Table;
            var store = query.FirstOrDefault(s => s.StoreEMail == storeEmail);
            return store;
        }

        public List<Store> GetStoresByMainPartyIds(List<int?> mainPartyIds)
        {
            var query = _storeRepository.Table;

            query = query.Where(x => mainPartyIds.Contains(x.MainPartyId));
            return query.ToList();
        }




        public Store GetStoreByStoreUrlName(string storeUrlName)
        {
            if (string.IsNullOrEmpty(storeUrlName))
                throw new ArgumentNullException("storeUrlName");

            string key = string.Format(STORES_BY_STORE_URL_NAME_KEY, storeUrlName);
            return _cacheManager.Get(key, () => 
            {
                var query = _storeRepository.Table;
                return query.FirstOrDefault(s => s.StoreUrlName == storeUrlName);
            });
        }

        public void InsertStore(Store store)
        {
            if (store == null)
                throw new ArgumentNullException("store");

            _storeRepository.Insert(store);

        }

        public void UpdateStore(Store store)
        {
            if (store == null)
                throw new ArgumentNullException("store");

            _storeRepository.Update(store);

            string key = string.Format(STORES_BY_MAINPARTY_ID_KEY, store.MainPartyId);
            _cacheManager.Remove(key);

        }

        public IList<StoreForCategoryResult> GetSPStoreForCategorySearch(int categoryId, int brandId = 0, int countryId = 0, int cityId = 0, int localityId = 0)
        {
            string key = string.Format(STORES_BY_SEARCH_KEY, categoryId, brandId, countryId, cityId, localityId);
            return _cacheManager.Get(key, () =>
            {
                var pCategoryId = _dataProvider.GetParameter();
                pCategoryId.ParameterName = "CategoryId";
                pCategoryId.Value = categoryId;
                pCategoryId.DbType = DbType.Int32;

                var pBrandId = _dataProvider.GetParameter();
                pBrandId.ParameterName = "BrandId";
                pBrandId.Value = brandId;
                pBrandId.DbType = DbType.Int32;

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

                var stores = _dbContext.SqlQuery<StoreForCategoryResult>("SP_GetStoreForCategorySearch @CategoryId, @BrandId, @CountryId, @CityId, @LocalityId",
                                                                        pCategoryId, pBrandId, pCountryId, pCityId, pLocalityId).ToList();
                return stores;
            });
        }

        public Store GetStoreForVideoSearch(string searchText)
        {
            if (searchText == "")
                throw new ArgumentNullException("searchText");
            var query = _storeRepository.Table;
            query = from m in query
                    where m.StoreName.ToLower().Contains(searchText.ToLower())
                    select m;
            return query.FirstOrDefault();

        }

        public IList<Store> GetHomeStores(int pageSize = int.MaxValue)
        {
            return _cacheManager.Get(STORES_HOME_KEY, () =>
            {

                var query = _storeRepository.Table;
                query = query.Where(s => s.StoreShowcase == true);
                return query.Take(pageSize).ToList();
            });

        }

        public IList<Store> GetStoreSearchByStoreName(string storeName)
        {
            var query = _storeRepository.Table;
            query = query.Where(x => x.StoreName.ToUpper().StartsWith(storeName.ToUpper())
            || x.StoreShortName.ToUpper().StartsWith(storeName.ToUpper()));
            return query.ToList();

        }

        public Store GetStoreByStoreNo(string storeNo)
        {
            var query = _storeRepository.Table;
            return query.FirstOrDefault(x => x.StoreNo == storeNo);
        }

        public List<Store> SP_GetStoresForAutoMail(int packetId, int constandId, int pageDimension, int pageIndex)
        {

            var pPacketId = _dataProvider.GetParameter();
            pPacketId.ParameterName = "PacketId";
            pPacketId.Value = packetId;
            pPacketId.DbType = DbType.Int32;

            var pConstantId = _dataProvider.GetParameter();
            pConstantId.ParameterName = "ConstantId";
            pConstantId.Value = constandId;
            pConstantId.DbType = DbType.Int32;

            var pPageIndex = _dataProvider.GetParameter();
            pPageIndex.ParameterName = "PageIndex";
            pPageIndex.Value = pageIndex;
            pPageIndex.DbType = DbType.Int32;

            var pPageDimension = _dataProvider.GetParameter();
            pPageDimension.ParameterName = "PageDimension";
            pPageDimension.Value = pageDimension;
            pPageDimension.DbType = DbType.Int32;

            var stores = _dbContext.SqlQuery<Store>("SP_GetStoresByPacketIdForAutoMail @PacketId, @ConstantId, @PageDimension, @PageIndex",
                                                                    pPacketId, pConstantId, pPageDimension, pPageIndex).ToList();
            return stores;
        }

     
        public IList<StoreCertificate> GetStoreCertificatesByMainPartyId(int mainPartyId)
        {
            if (mainPartyId == 0)
                throw new ArgumentNullException("mainPartyId");

            string key = string.Format(STORECERTIFICATES_BY_MAIN_PARTY_ID_KEY, mainPartyId);
            return _cacheManager.Get(key, () => 
            {
                var query = _storeCertificateRepository.Table;
                query = query.Where(x => x.MainPartyId == mainPartyId).OrderBy(x => x.Order).ThenByDescending(x => x.StoreCertificateId);

                var storeCertificates = query.ToList();
                return storeCertificates;
            });
        }

        public void UpdateStoreCertificate(StoreCertificate storeCertificate)
        {
            if (storeCertificate == null)
                throw new ArgumentNullException("storeCertificate");

            _storeCertificateRepository.Update(storeCertificate);

            string key = string.Format(STORECERTIFICATES_BY_MAIN_PARTY_ID_KEY, storeCertificate.MainPartyId);
            _cacheManager.Remove(key);
        }

        public void DeleteStoreCertificate(StoreCertificate storeCertificate)
        {
            if (storeCertificate == null)
                throw new ArgumentNullException("storeCertificate");

            _storeCertificateRepository.Delete(storeCertificate);

            string key = string.Format(STORECERTIFICATES_BY_MAIN_PARTY_ID_KEY, storeCertificate.MainPartyId);
            _cacheManager.Remove(key);
        }

        public void InsertStoreCertificate(StoreCertificate storeCertificate)
        {
            if (storeCertificate == null)
                throw new ArgumentNullException("storeCertificate");

            _storeCertificateRepository.Insert(storeCertificate);

            string key = string.Format(STORECERTIFICATES_BY_MAIN_PARTY_ID_KEY, storeCertificate.MainPartyId);
            _cacheManager.Remove(key);
        }


        public StoreCertificate GetStoreCertificateByStoreCertificateId(int storeCertificateId)
        {
            if (storeCertificateId <=0)
                throw new ArgumentNullException("storeCertificateId");

            var query = _storeCertificateRepository.Table;
            return query.FirstOrDefault(x => x.StoreCertificateId == storeCertificateId);
        }

        #endregion


    }
}
