using Trinnk.Caching;
using Trinnk.Core.Data;
using Trinnk.Entities.Tables.Stores;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Trinnk.Services.Stores
{
    public class StoreInfoNumberShowService : BaseService, IStoreInfoNumberShowService
    {
        #region Constants

        private const string STOREINFONUMBERSHOWS_BY_STORE_MAIN_PARTY_ID_KEY = "storeinfonumbershow.bystoremainpartid-{0}";

        #endregion

        #region Fields

        private readonly IRepository<StoreInfoNumberShow> _storeInfoNumberRepository;
        private readonly ICacheManager _cacheManager;

        #endregion

        #region Ctor

        public StoreInfoNumberShowService(IRepository<StoreInfoNumberShow> storeInfoNumberRepository, ICacheManager cacheManager) : base(cacheManager)
        {
            this._storeInfoNumberRepository = storeInfoNumberRepository;
            this._cacheManager = cacheManager;

        }

        #endregion

        #region Methods

        public IList<StoreInfoNumberShow> GetAllStoreInfoNumberShow()
        {
            var query = _storeInfoNumberRepository.Table;
            return query.ToList();
        }

        public StoreInfoNumberShow GetStoreInfoNumberShowByStoreInfoNumberShowId(int storeInfoNumberShowId)
        {
            if (storeInfoNumberShowId == 0)
                throw new ArgumentException("storeInfoNumberShowId");

            var query = _storeInfoNumberRepository.Table;
            return query.FirstOrDefault(x => x.StoreInfoNumberShowId == storeInfoNumberShowId);
        }

        public StoreInfoNumberShow GetStoreInfoNumberShowByStoreMainPartyId(int storeMainPartyId)
        {
            if (storeMainPartyId == 0)
                throw new ArgumentException("storeMainPartyId");

            string key = string.Format(STOREINFONUMBERSHOWS_BY_STORE_MAIN_PARTY_ID_KEY, storeMainPartyId);
            return _cacheManager.Get(key, () =>
            {
                var query = _storeInfoNumberRepository.Table;
                return query.FirstOrDefault(x => x.StoreMainpartyId == storeMainPartyId);
            });
        }



        public void InsertStoreInfoNumberShow(StoreInfoNumberShow storeInfoNumberShow)
        {
            if (storeInfoNumberShow == null)
                throw new ArgumentNullException("storeInfoNumberShow");

            _storeInfoNumberRepository.Insert(storeInfoNumberShow);

        }

        public void UpdateStoreInfoNumberShow(StoreInfoNumberShow storeInfoNumberShow)
        {
            if (storeInfoNumberShow == null)
                throw new ArgumentNullException("storeInfoNumberShow");

            _storeInfoNumberRepository.Update(storeInfoNumberShow);

            string key = string.Format(STOREINFONUMBERSHOWS_BY_STORE_MAIN_PARTY_ID_KEY, storeInfoNumberShow.StoreMainpartyId);
            _cacheManager.Remove(key);
        }

        public void DeleteStoreInfoNumberShow(StoreInfoNumberShow storeInfoNumberShow)
        {
            if (storeInfoNumberShow == null)
                throw new ArgumentNullException("storeInfoNumberShow");

            _storeInfoNumberRepository.Delete(storeInfoNumberShow);

            string key = string.Format(STOREINFONUMBERSHOWS_BY_STORE_MAIN_PARTY_ID_KEY, storeInfoNumberShow.StoreMainpartyId);
            _cacheManager.Remove(key);
        }

        #endregion

    }
}
