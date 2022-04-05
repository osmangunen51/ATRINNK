namespace NeoSistem.MakinaTurkiye.Classes
{
    using EnterpriseEntity.Business;
    using EnterpriseEntity.Business.Attributes;
    using System.Data;

    [Table("MemberStore")]
    public partial class MemberStore : EntityObject
    {
        [Column("MemberStoreId", SqlDbType.Int, PrimaryKey = true, Identity = true)]
        public int MemberStoreId { get; set; }

        [Column("MemberMainPartyId", SqlDbType.Int)]
        public int MemberMainPartyId { get; set; }

        [Column("StoreMainPartyId", SqlDbType.Int)]
        public int StoreMainPartyId { get; set; }

    }


}
