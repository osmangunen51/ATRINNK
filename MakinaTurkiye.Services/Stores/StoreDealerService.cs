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
        private const string STOREDEALERS_BY_STTORE_DEALER_ID_KEY = "makinaturkiye.storedealer.bystoredealerid-{0}";

        #endregion Constants

        #region Fields

        private readonly IRepository<StoreDealer> _storeDealerRepository;
        private readonly ICacheManager _cacheManager;

        #endregion Fields

        #region Ctor

        public StoreDealerService(IRepository<StoreDealer> storeDealerRepository, ICacheManager cacheManager) : base(cacheManager)
        {
            this._storeDealerRepository = storeDealerRepository;
            this._cacheManager = cacheManager;
        }

        #endregion Ctor

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

                if (dealerType != DealerTypeEnum.All)
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

        public void DeleteStoreDealer(StoreDealer storeDealer)
        {
            if (storeDealer == null)
                throw new ArgumentNullException("storeDealer");

            _storeDealerRepository.Delete(storeDealer);
            string key = string.Format(STOREDEALERS_BY_MAIN_PARTY_ID_KEY, storeDealer.MainPartyId, storeDealer.DealerType);
            _cacheManager.Remove(key);
        }

        public void UpdateStoreDealer(StoreDealer storeDealer)
        {
            if (storeDealer == null) throw new ArgumentNullException("storeDealer");
            _storeDealerRepository.Delete(storeDealer);
            string key = string.Format(STOREDEALERS_BY_MAIN_PARTY_ID_KEY, storeDealer.MainPartyId, storeDealer.DealerType);
            _cacheManager.Remove(key);
        }

        public StoreDealer GetStoreDealersByStoreDealerId(int StoreDealerId)
        {
            string key = string.Format(STOREDEALERS_BY_STTORE_DEALER_ID_KEY, StoreDealerId);
            return _cacheManager.Get(key, () =>
            {
                var query = _storeDealerRepository.Table;
                return query.FirstOrDefault(sd => sd.StoreDealerId == StoreDealerId);
            });
        }

        #endregion Methods
    }
}