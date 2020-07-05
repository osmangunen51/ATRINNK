using MakinaTurkiye.Caching;
using MakinaTurkiye.Core.Data;
using MakinaTurkiye.Entities.Tables.Members;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MakinaTurkiye.Services.Members
{
    public class MemberStoreService : BaseService, IMemberStoreService
    {
        #region Constants

        private const string MEMBERSTORES_BY_MEMBERMAINPARTY_ID_KEY = "makinaturkiye.memberstore.bymembermainpartyId={0}";
        private const string MEMBERSTORES_BY_STORE_MAIN_PARTY_ID_KEY = "makinaturkiye.memberstore.bystoremainpartyId={0}";

        #endregion

        #region Fields

        private readonly IRepository<MemberStore> _memberStoreRepository;
        private readonly ICacheManager _cacheManager;

        #endregion

        #region Ctor

        public MemberStoreService(IRepository<MemberStore> memberStoreRepository, ICacheManager cacheManager) : base(cacheManager)
        {
            _memberStoreRepository = memberStoreRepository;
            _cacheManager = cacheManager;
        }

        #endregion

        #region Methods

        public MemberStore GetMemberStoreByMemberMainPartyId(int memberMainPartyId)
        {
            if (memberMainPartyId == 0)
                return null;

            string key = string.Format(MEMBERSTORES_BY_MEMBERMAINPARTY_ID_KEY, memberMainPartyId);
            return _cacheManager.Get(key, () =>
            {
                var query = _memberStoreRepository.Table;
                return query.FirstOrDefault(ms => ms.MemberMainPartyId == memberMainPartyId);
            });
        }

        public MemberStore GetMemberStoreByStoreMainPartyId(int storeMainPartyId)
        {
            if (storeMainPartyId <= 0)
                throw new ArgumentNullException("");

            string key = string.Format(MEMBERSTORES_BY_STORE_MAIN_PARTY_ID_KEY, storeMainPartyId);
            return _cacheManager.Get(key, () => 
            {
                var query = _memberStoreRepository.Table;
                return query.FirstOrDefault(ms => ms.StoreMainPartyId == storeMainPartyId);
            });
        }

        public IList<MemberStore> GetMemberStoresByStoreMainPartyId(int storeMainPartyId)
        {
            var query = _memberStoreRepository.Table;
            query = query.Where(x => x.StoreMainPartyId == storeMainPartyId);
            return query.ToList();
        }

        public void InsertMemberStore(MemberStore memberStore)
        {
            if (memberStore == null)
                throw new ArgumentNullException("memberStore");
            _memberStoreRepository.Insert(memberStore);
        }

        #endregion

    }
}
