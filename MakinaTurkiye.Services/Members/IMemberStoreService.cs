using MakinaTurkiye.Entities.Tables.Members;
using System.Collections.Generic;

namespace MakinaTurkiye.Services.Members
{
    public interface IMemberStoreService: ICachingSupported
    {
        MemberStore GetMemberStoreByMemberMainPartyId(int memberMainPartyId);
        MemberStore GetMemberStoreByStoreMainPartyId(int storeMainPartyId);
        IList<MemberStore> GetMemberStoresByStoreMainPartyId(int storeMainPartyId);
        void InsertMemberStore(MemberStore memberStore);
    }
}
