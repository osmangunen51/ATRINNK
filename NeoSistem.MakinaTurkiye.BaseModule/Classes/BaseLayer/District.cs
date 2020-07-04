namespace NeoSistem.MakinaTurkiye.Classes
{
    using EnterpriseEntity.Business;
    using EnterpriseEntity.Business.Attributes;
    using System.Data;

    [Table("District")]
	public partial class District: EntityObject
	{
		[Column("DistrictId", SqlDbType.Int, PrimaryKey=true, Identity=true)]
		public int DistrictId{ get; set; }

		[Column("CityId", SqlDbType.Int)]
		public int CityId{ get; set; }

		[Column("LocalityId", SqlDbType.Int)]
		public int LocalityId{ get; set; }

		[Column("DistrictName_Big", SqlDbType.NVarChar)]
		public string DistrictName_Big{ get; set; }

		[Column("DistrictName", SqlDbType.NVarChar)]
		public string DistrictName{ get; set; }

		[Column("DistrictName_Small", SqlDbType.NVarChar)]
		public string DistrictName_Small{ get; set; }

		[Column("ZipCode", SqlDbType.NVarChar)]
		public string ZipCode{ get; set; }

	}


}
