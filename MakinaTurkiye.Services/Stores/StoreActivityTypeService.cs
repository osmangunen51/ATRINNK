using MakinaTurkiye.Caching;
using MakinaTurkiye.Core.Data;
using MakinaTurkiye.Entities.Tables.Stores;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace MakinaTurkiye.Services.Stores
{
    public class StoreActivityTypeService : BaseService, IStoreActivityTypeService
    {

        #region Constants

        private const string STOREACTIVITYTYPES_BY_STORE_ID_KEY = "makinaturkiye.storeactivitytype.bystoreid-{0}";

        #endregion

        #region Fields

        private readonly IRepository<StoreActivityType> _storeActivityTypeRepository;
        private readonly ICacheManager _cacheManager;

        #endregion

        #region Utilities

        private void RemoveStoreActivityTypeCache(StoreActivityType storeActivityType)
        {
            string key = string.Format(STOREACTIVITYTYPES_BY_STORE_ID_KEY, storeActivityType.StoreId);
            _cacheManager.Remove(key);
        }

        #endregion


        #region Ctor

        public StoreActivityTypeService(IRepository<StoreActivityType> storeActivityTypeRepository, ICacheManager cacheManager): base(cacheManager)
        {
            _storeActivityTypeRepository = storeActivityTypeRepository;
            _cacheManager = cacheManager;
        }

        #endregion


        public IList<StoreActivityType> GetStoreActivityTypesByStoreId(int storeId)
        {
            if (storeId == 0)
                throw new ArgumentNullException("storeId");

            string key = string.Format(STOREACTIVITYTYPES_BY_STORE_ID_KEY, storeId);
            return _cacheManager.Get(key, () => 
            {
                var query = _storeActivityTypeRepository.Table;

                query = query.Include(x => x.ActivityType);

                query = query.Where(x => x.StoreId == storeId);

                var storeActivityTypes = query.ToList();
                return storeActivityTypes;
            });
        }

        public void InsertStoreActivityType(StoreActivityType storeActivityType)
        {
            if (storeActivityType == null)
                throw new ArgumentNullException("storeActivityType");

            _storeActivityTypeRepository.Insert(storeActivityType);

            RemoveStoreActivityTypeCache(storeActivityType);

        }

        public void DeleteStoreActivityType(StoreActivityType storeActivityType)
        {
            if (storeActivityType == null)
                throw new ArgumentNullException("storeActivityType");

            _storeActivityTypeRepository.Delete(storeActivityType);

            RemoveStoreActivityTypeCache(storeActivityType);
        }
    }
}
