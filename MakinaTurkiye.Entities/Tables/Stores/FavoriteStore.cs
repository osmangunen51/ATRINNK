namespace MakinaTurkiye.Entities.Tables.Stores
{
    public class FavoriteStore: BaseEntity
    {
        public int FavoriteMainPartyId { get; set; }
        public int? MemberMainPartyId { get; set; }
        public int? StoreMainPartyId { get; set; }
    }
}
