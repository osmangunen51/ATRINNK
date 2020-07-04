namespace NeoSistem.MakinaTurkiye.Classes
{
    using EnterpriseEntity.Business;
    using EnterpriseEntity.Business.Attributes;
    using System.Data;

    [Table("DealerBrand")]
	public partial class DealerBrand: EntityObject
	{
		[Column("DealerBrandId", SqlDbType.Int, PrimaryKey=true, Identity=true)]
		public int DealerBrandId{ get; set; }

		[Column("MainPartyId", SqlDbType.Int)]
		public int MainPartyId{ get; set; }

		[Column("DealerBrandName", SqlDbType.VarChar)]
		public string DealerBrandName{ get; set; }

		[Column("DealerBrandPicture", SqlDbType.VarChar)]
		public string DealerBrandPicture{ get; set; }

	}


}
