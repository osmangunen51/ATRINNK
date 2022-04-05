using System;

namespace MakinaTurkiye.Entities.Tables.Members
{
    public class MemberDescriptionLog : BaseEntity
    {
        public int MemberDescription_logId { get; set; }
        public int descId { get; set; }
        public int? MainPartyId { get; set; }
        public string Title { get; set; }
        public DateTime? Date { get; set; }
        public DateTime? UpdateDate { get; set; }
        public Int16? DescriptionDegree { get; set; }
        public int? BaseID { get; set; }
        public int? Status { get; set; }
        public int? UserId { get; set; }
        public int? FromUserId { get; set; }
        public int? ConstantId { get; set; }
        public bool IsFirst { get; set; }
        public int? PreRegistrationStoreId { get; set; }
        public DateTime RecordDate { get; set; }

        public string UserIdName { get; set; }
        public string FromUserIdName { get; set; }
        public string TransactionType { get; set; }
    }
}
