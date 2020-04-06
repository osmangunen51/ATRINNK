namespace NeoSistem.MakinaTurkiye.Classes
{
    using EnterpriseEntity.Business;
    using EnterpriseEntity.Business.Attributes;
    using System.Data;

    [Table("StoreDealer")]
	public partial class StoreDealer: EntityObject
	{
    [Column("StoreDealerId", SqlDbType.Int, PrimaryKey = true)]
    public int StoreDealerId { get; set; }

		[Column("MainPartyId", SqlDbType.Int)]
    public int MainPartyId { get; set; }

		[Column("DealerName", SqlDbType.VarChar)]
		public string DealerName{ get; set; }

		[Column("DealerType", SqlDbType.TinyInt)]
		public byte DealerType{ get; set; }

	}


}
