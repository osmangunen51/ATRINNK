namespace NeoSistem.MakinaTurkiye.Classes
{
    using EnterpriseEntity.Business;
    using EnterpriseEntity.Business.Attributes;
    using System.Data;

    [Table("StoreBrand")]
	public partial class StoreBrand: EntityObject
	{
		[Column("StoreBrandId", SqlDbType.Int, PrimaryKey=true, Identity=true)]
		public int StoreBrandId{ get; set; }

		[Column("MainPartyId", SqlDbType.Int)]
		public int MainPartyId{ get; set; }

		[Column("BrandName", SqlDbType.VarChar)]
		public string BrandName{ get; set; }

		[Column("BrandDescription", SqlDbType.VarChar)]
		public string BrandDescription{ get; set; }

		[Column("BrandPicture", SqlDbType.VarChar)]
		public string BrandPicture{ get; set; }

	}


}
