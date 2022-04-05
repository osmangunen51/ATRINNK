using System;

namespace MakinaTurkiye.Entities.StoredProcedures.Members
{
    public class MemberDescriptionTaskItem
    {

        public int ID { get; set; }
        public int? UserId { get; set; }
        public DateTime Date { get; set; }
        public DateTime? UpdateDate { get; set; }
        public string StoreName { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int? MainPartyId { get; set; }
        public string UserName { get; set; }
        public string UserColor { get; set; }
        public byte? AuthorizedId { get; set; }
        public int? StoreMainPartyId { get; set; }
        public string MemberName { get; set; }
        public string MemberSurname { get; set; }
        public int? MemberMainPartyId { get; set; }
        public byte? MemberType { get; set; }
        public int? FromUserId { get; set; }
        public string FromUserName { get; set; }
        public string PreStoreName { get; set; }
        public string PreMemberName { get; set; }
        public string PreMemberSurname { get; set; }

    }
}
