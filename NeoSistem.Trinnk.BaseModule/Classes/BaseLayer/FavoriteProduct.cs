namespace NeoSistem.Trinnk.Classes
{
    using EnterpriseEntity.Business;
    using EnterpriseEntity.Business.Attributes;
    using System.Data;

    [Table("FavoriteProduct")]
    public partial class FavoriteProduct : EntityObject
    {
        [Column("FavoriteProductId", SqlDbType.Int, PrimaryKey = true, Identity = true)]
        public int FavoriteProductId { get; set; }

        [Column("MainPartyId", SqlDbType.Int)]
        public int MainPartyId { get; set; }

        [Column("ProductId", SqlDbType.Int)]
        public int ProductId { get; set; }

    }


}
