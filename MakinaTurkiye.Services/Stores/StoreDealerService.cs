using MakinaTurkiye.Caching;
using MakinaTurkiye.Core.Data;
using MakinaTurkiye.Entities.Tables.Stores;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MakinaTurkiye.Services.Stores
{
    public class StoreDealerService : BaseService, IStoreDealerService
    {
        #region Constants

        private const string STOREDEALERS_BY_MAIN_PARTY_ID_KEY = "makinaturkiye.storedealer.bymainpartyid-{0}-{1}";

        #endregion

        #region Fields

        private readonly IRepository<StoreDealer> _storeDealerRepository;
        private readonly ICacheManager _cacheManager;

        #endregion

        #region Ctor

        public StoreDealerService(IRepository<StoreDealer> storeDealerRepository, ICacheManager cacheManager) : base(cacheManager)
        {
            this._storeDealerRepository = storeDealerRepository;
            this._cacheManager = cacheManager;
        }

        #endregion

        #region Methods

        public IList<StoreDealer> GetStoreDealersByMainPartyId(int mainPartyId, DealerTypeEnum dealerType)
        {
            if (mainPartyId == 0)
                throw new ArgumentNullException("mainPartyId");

            string key = string.Format(STOREDEALERS_BY_MAIN_PARTY_ID_KEY, mainPartyId, dealerType);
            return _cacheManager.Get(key, () => 
            {
                var query = _storeDealerRepository.Table;
                query = query.Where(sd => sd.MainPartyId == mainPartyId);

                if(dealerType!= DealerTypeEnum.All)
                    query = query.Where(x => x.DealerType == (byte)dealerType);

                var storeDealers = query.ToList();
                return storeDealers;
            });
        }

        public void InsertStoreDealer(StoreDealer storeDealer)
        {
            if (storeDealer == null)
                throw new ArgumentNullException("storeDealer");


            _storeDealerRepository.Insert(storeDealer);
            string key = string.Format(STOREDEALERS_BY_MAIN_PARTY_ID_KEY, storeDealer.MainPartyId, storeDealer.DealerType);
            _cacheManager.Remove(key);
        }

        #endregion
    }
}
