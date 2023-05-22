using Trinnk.Entities.StoredProcedures.Members;
using Trinnk.Entities.Tables.Members;
using System;
using System.Collections.Generic;

namespace Trinnk.Services.Members
{
    public partial interface IMemberDescriptionService
    {
        void UpdateMemberDescription(MemberDescription memberDescription);
        void DeleteMemberDescription(MemberDescription memberDescription);
        void InsertMemberDescription(MemberDescription memberDescription);
        List<MemberDescription> GetMemberDescriptionsByMainPartyId(int mainPartyId);
        List<MemberDescription> GetMemberDescriptions();
        List<MemberDescription> GetMemberDescriptionsByDate(DateTime date);
        IList<MemberDescriptionForStore> GetByMainPartyIDOrderByColumn(string orderDesc, int mainPartyId);
        IList<MemberDescriptionForStore> GetMemberDescByOnDate(int userId, int userGroupId, int pageDimension, int pageIndex, int orderBy, int consttandtId, out int totalRecord, string city = "", string inputdate = "", string updateDate = "", int fromUserId = 0);
        IList<MemberDescriptionForStore> GetMemberDescNoUpdateDate(int page, int pageSize);
        IList<BaseMemberDescriptionForStore> GetMemberDescCloseDate(int Page, int PageSize, string Type, int UserId, out int TotalRecord);
        IList<BaseMemberDescriptionForStore> GetMemberDateDesc(int Page, int PageSize, string Type, int UserId, out int TotalRecord);
        IList<MemberDescriptionForStore> GetMemberDescDate(int Page, int PageSize, string Type);
        IList<MemberDescriptionForStore> GetMemberDescSearchText(string Text);
        List<MemberDescriptionTaskItem> SP_GetMemberDescriptionSearch(int Page, int PageSize, int UserId, System.DateTime? Date, string OrderColumn, byte OrderType, int ConstantId, string addedDate, out int TotalRecord);
        List<MemberDescriptionCount> SP_GetMemberDescriptionCount();
        List<MemberDescription> GetMemberDescriptionByPreRegistrationStoreId(int preRegistrationStoreId);
        void SP_MemberDescriptionsUpdateDateForRest();
        void SP_UpdateMemberDescriptions();
        void InsertMemberDescriptionLog(MemberDescriptionLog memberDescriptionLog);
        MemberDescription GetMemberDescriptionsByMemberDescriptionId(int memberDescriptionId);
    }
}
