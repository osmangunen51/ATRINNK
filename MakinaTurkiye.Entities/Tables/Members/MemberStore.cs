namespace MakinaTurkiye.Entities.Tables.Members
{
    public class MemberStore : BaseEntity
    {
        public int MemberStoreId { get; set; }
        public int? MemberMainPartyId { get; set; }
        public int? StoreMainPartyId { get; set; }
        public byte MemberStoreType { get; set; }
    }
}
