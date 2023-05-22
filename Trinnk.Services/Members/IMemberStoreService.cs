using Trinnk.Entities.Tables.Members;
using System.Collections.Generic;

namespace Trinnk.Services.Members
{
    public interface IMemberStoreService : ICachingSupported
    {
        MemberStore GetMemberStoreByMemberMainPartyId(int memberMainPartyId);
        MemberStore GetMemberStoreByStoreMainPartyId(int storeMainPartyId);
        IList<MemberStore> GetMemberStoresByStoreMainPartyId(int storeMainPartyId);
        IList<MemberStore> GetMemberStoreListByMemberMainPartyId(int memberMainPartyId);
        IList<MemberStore> GetMemberStores();
        void InsertMemberStore(MemberStore memberStore);
    }
}
