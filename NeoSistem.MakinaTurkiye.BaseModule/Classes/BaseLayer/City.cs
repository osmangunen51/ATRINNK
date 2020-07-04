namespace NeoSistem.MakinaTurkiye.Classes
{
    using EnterpriseEntity.Business;
    using EnterpriseEntity.Business.Attributes;
    using System.Data;

    [Table("City")]
	public partial class City: EntityObject
	{
		[Column("CityId", SqlDbType.Int, PrimaryKey=true)]
		public int CityId{ get; set; }

		[Column("CountryId", SqlDbType.Int)]
		public int CountryId{ get; set; }

		[Column("Plate", SqlDbType.NVarChar)]
		public string Plate{ get; set; }

		[Column("CityName_Big", SqlDbType.NVarChar)]
		public string CityName_Big{ get; set; }

		[Column("CityName", SqlDbType.NVarChar)]
		public string CityName{ get; set; }

		[Column("CityName_Small", SqlDbType.NVarChar)]
		public string CityName_Small{ get; set; }

		[Column("AreaCode", SqlDbType.VarChar)]
		public string AreaCode{ get; set; }

    [Column("CityOrder", SqlDbType.TinyInt)]
    public byte CityOrder { get; set; }

	}


}
