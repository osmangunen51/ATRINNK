﻿using MakinaTurkiye.Caching;
using MakinaTurkiye.Core.Data;
using MakinaTurkiye.Entities.Tables.Stores;
using System;
using System.Linq;

namespace MakinaTurkiye.Services.Stores
{
    public class FavoriteStoreService : IFavoriteStoreService
    {
        #region Constants

        private const string FAVORITESTORES_BY_MEMBERMAINPARTY_ID_WITH_STOREMAINPARTY_ID_KEY = "makinaturkiye.favoritestore.bymembermainpartyId-storemainpartyId-{0}-{1}";

        #endregion

        #region Fields

        private readonly IRepository<FavoriteStore> _favoriteStoreRepository;
        private readonly ICacheManager _cacheManager;

        #endregion

        #region Ctor

        public FavoriteStoreService(IRepository<FavoriteStore> favoriteStoreRepository, ICacheManager cacheManager)
        {
            _favoriteStoreRepository = favoriteStoreRepository;
            _cacheManager = cacheManager;
        }

        #endregion

        #region Methods

        public FavoriteStore GetFavoriteStoreByMemberMainPartyIdWithStoreMainPartyId(int memberMainPartyId, int storeMainPartyId)
        {
            if (memberMainPartyId == 0)
                return null;

            if (storeMainPartyId == 0)
                return null;

            string key = string.Format(FAVORITESTORES_BY_MEMBERMAINPARTY_ID_WITH_STOREMAINPARTY_ID_KEY, memberMainPartyId, storeMainPartyId);
            return _cacheManager.Get(key, () =>
            {
                var query = _favoriteStoreRepository.Table;
                return query.FirstOrDefault(fp => fp.MemberMainPartyId == memberMainPartyId && fp.StoreMainPartyId == storeMainPartyId);
            });
        }

        public void InsertFavoriteStore(FavoriteStore favoriteStore)
        {
            if (favoriteStore == null)
                throw new ArgumentNullException("favoriteStore");
            _favoriteStoreRepository.Insert(favoriteStore);
        }

        public void DeleteFavoriteStore(FavoriteStore favoriteStore)
        {
            if (favoriteStore == null)
                throw new ArgumentNullException("favoriteStore");
            _favoriteStoreRepository.Delete(favoriteStore);
        }

        #endregion
    }
}
