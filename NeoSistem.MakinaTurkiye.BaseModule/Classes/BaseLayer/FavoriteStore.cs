namespace NeoSistem.MakinaTurkiye.Classes
{
    using EnterpriseEntity.Business;
    using EnterpriseEntity.Business.Attributes;
    using System.Data;

    [Table("FavoriteStore")]
    public partial class FavoriteStore : EntityObject
    {
        [Column("FavoriteMainPartyId", SqlDbType.Int, PrimaryKey = true, Identity = true)]
        public int FavoriteMainPartyId { get; set; }

        [Column("MemberMainPartyId", SqlDbType.Int)]
        public int MemberMainPartyId { get; set; }

        [Column("StoreMainPartyId", SqlDbType.Int)]
        public int StoreMainPartyId { get; set; }

    }


}
