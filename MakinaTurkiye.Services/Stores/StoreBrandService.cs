using MakinaTurkiye.Caching;
using MakinaTurkiye.Core.Data;
using MakinaTurkiye.Entities.Tables.Stores;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MakinaTurkiye.Services.Stores
{
    public class StoreBrandService : BaseService, IStoreBrandService
    {

        #region Constants

        private const string STOREBRANS_BY_MAIN_PARTY_ID_KEY = "storebrand.bymainpartyid-{0}";
        private const string STOREBRANS_BY_BY_ID_KEY = "storebrand.byid-{0}";

        #endregion

        #region Fields

        private readonly IRepository<StoreBrand> _storeBrandRepository;
        private readonly ICacheManager _cacheManager;

        #endregion

        #region Ctor

        public StoreBrandService(IRepository<StoreBrand> storeBrandRepository, ICacheManager cacheManager) : base(cacheManager)
        {
            this._storeBrandRepository = storeBrandRepository;
            this._cacheManager = cacheManager;
        }

        public void DeleteStoreBrand(StoreBrand storeBrand)
        {
            this._storeBrandRepository.Delete(storeBrand);
            this._cacheManager.RemoveByPattern("makinaturkiye.storebrand");
        }

        #endregion

        #region Methods

        public IList<StoreBrand> GetStoreBrandByMainPartyId(int mainPartyId)
        {
            if (mainPartyId == 0)
                throw new ArgumentNullException("mainPartyId");

            string key = string.Format(STOREBRANS_BY_MAIN_PARTY_ID_KEY, mainPartyId);
            return _cacheManager.Get(key, () =>
            {
                var query = _storeBrandRepository.Table;
                query = query.Where(sb => sb.MainPartyId == mainPartyId);

                var storeBrands = query.ToList();
                return storeBrands;
            });
        }

        public StoreBrand GetStoreBrandByStoreBrand(int storeBrandId)
        {
            string key = string.Format(STOREBRANS_BY_BY_ID_KEY, storeBrandId);
            return _cacheManager.Get(key, () =>
            {
                StoreBrand result = new StoreBrand();
                var query = _storeBrandRepository.Table;
                query = query.Where(sb => sb.StoreBrandId==storeBrandId);
                result = query.FirstOrDefault();
                return result;
            });
        }

        public void InsertStoreBrand(StoreBrand storeBrand)
        {
            if (storeBrand == null)
                throw new ArgumentNullException("storeBrand");

            _storeBrandRepository.Insert(storeBrand);
            RemoveStoreBrandCache(storeBrand);
        }

        private void RemoveStoreBrandCache(StoreBrand StoreBrand)
        {
            if (StoreBrand.MainPartyId > 0)
            {
                string key = string.Format(STOREBRANS_BY_MAIN_PARTY_ID_KEY, StoreBrand.MainPartyId);
                _cacheManager.Remove(key);
            }
            if (StoreBrand.StoreBrandId > 0)
            {
                string key = string.Format(STOREBRANS_BY_BY_ID_KEY, StoreBrand.StoreBrandId);
                _cacheManager.Remove(key);
            }
        }
        #endregion
    }
}
