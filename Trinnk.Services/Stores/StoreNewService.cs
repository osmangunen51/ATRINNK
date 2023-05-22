using Trinnk.Caching;
using Trinnk.Core;
using Trinnk.Core.Data;
using Trinnk.Entities.Tables.Stores;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace Trinnk.Services.Stores
{
    public class StoreNewService : BaseService, IStoreNewService
    {

        #region Constants

        private const string STORENEWS_BY_STORE_MAIN_PARTY_ID_KEY = "storenew.bystoremainpartyid-{0}-{1}";

        #endregion

        #region Fields

        private readonly IRepository<StoreNew> _storeNewRepository;
        private readonly ICacheManager _cacheManager;

        #endregion

        #region Ctor

        public StoreNewService(IRepository<StoreNew> storeNewRepository, ICacheManager cacheManager) : base(cacheManager)
        {
            this._storeNewRepository = storeNewRepository;
            this._cacheManager = cacheManager;
        }

        #endregion

        #region Methods

        public void DeleteStoreNew(StoreNew storeNew)
        {
            if (storeNew == null)
                throw new ArgumentException("storeNew");
            _storeNewRepository.Delete(storeNew);
        }

        public IList<StoreNew> GetAllStoreNews(byte newType)
        {
            var query = _storeNewRepository.Table;

            return query.Where(x => x.NewType == newType).ToList();

        }

        public StoreNew GetStoreNewByStoreNewId(int storeNewId)
        {
            if (storeNewId == 0)
                throw new ArgumentNullException("storeNewId");

            var query = _storeNewRepository.Table;

            //query = query.Include(sn => sn.Store);

            return query.FirstOrDefault(x => x.StoreNewId == storeNewId);
        }

        public IPagedList<StoreNew> GetAllStoreNews(int pageDimension, int page, byte newType)
        {
            var query = _storeNewRepository.Table;


            int totalRecord = query.Where(x => x.Active == true).Count();

            query = query.Where(x => x.Active == true && x.NewType == newType)
                .OrderByDescending(x => x.UpdateDate).Skip(page * pageDimension - pageDimension).Take(pageDimension);

            var storeNews = query.ToList();
            return new PagedList<StoreNew>(storeNews, page, pageDimension, totalRecord);
        }

        public IList<StoreNew> GetStoreNewsByStoreMainPartyId(int storeMainPartyId, StoreNewTypeEnum newType)
        {
            if (storeMainPartyId == 0)
                throw new ArgumentNullException("storeMainPartyId");

            string key = string.Format(STORENEWS_BY_STORE_MAIN_PARTY_ID_KEY, storeMainPartyId, newType);
            return _cacheManager.Get(key, () =>
            {
                var query = _storeNewRepository.Table;

                query = query.Where(x => x.StoreMainPartyId == storeMainPartyId && x.NewType == (byte)newType);

                var storeNews = query.ToList();
                return storeNews.ToList();
            });
        }

        public IList<StoreNew> GetStoreNewsTop(int top, byte newType)
        {
            var query = _storeNewRepository.Table;

            query = query.Where(x => x.Active == true && x.NewType == newType).OrderByDescending(x => x.UpdateDate).Skip(0).Take(top);
            return query.ToList();
        }

        public void InsertStoreNew(StoreNew storeNew)
        {
            if (storeNew == null)
                throw new ArgumentException("storeNew");

            _storeNewRepository.Insert(storeNew);
        }

        public void UpdateStoreNew(StoreNew storeNew)
        {
            if (storeNew == null)
                throw new ArgumentException("storeNew");

            _storeNewRepository.Update(storeNew);
        }

        #endregion

    }
}
