using MakinaTurkiye.Entities.Tables.Members;
using System.Collections.Generic;

namespace MakinaTurkiye.Services.Members
{
    public interface IMemberStoreService : ICachingSupported
    {
        MemberStore GetMemberStoreByMemberMainPartyId(int memberMainPartyId);
        MemberStore GetMemberStoreByStoreMainPartyId(int storeMainPartyId);
        IList<MemberStore> GetMemberStoresByStoreMainPartyId(int storeMainPartyId);
        IList<MemberStore> GetMemberStores();
        void InsertMemberStore(MemberStore memberStore);
    }
}
