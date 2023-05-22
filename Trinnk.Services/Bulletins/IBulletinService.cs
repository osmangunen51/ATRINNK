using Trinnk.Entities.Tables.Bullettins;
using System.Collections.Generic;

namespace Trinnk.Services.Bulletins
{
    public interface IBulletinService
    {
        List<BulletinMember> GetBulletinMembers();
        List<BulletinMember> GetBulletinMembersByCategoryId(int categoryId);

        void InsertBulletinMember(BulletinMember bulletinMember);
        void UpdateBulletinMember(BulletinMember bulletinMember);
        void DeleteBulletinMember(BulletinMember bulletinMember);
        BulletinMember GetBulletinMemberByBulletinMemberId(int bulletinMemberId);

        void InsertBulletinMemberCategory(BulletinMemberCategory bulletinMemberCategory);



    }
}
