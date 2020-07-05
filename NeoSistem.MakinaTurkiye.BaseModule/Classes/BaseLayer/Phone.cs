namespace NeoSistem.MakinaTurkiye.Classes
{
    using EnterpriseEntity.Business;
    using EnterpriseEntity.Business.Attributes;
    using System.Data;

    [Table("Phone")]
	public partial class Phone: EntityObject
	{
		[Column("PhoneId", SqlDbType.Int, PrimaryKey=true, Identity=true)]
		public int PhoneId{ get; set; }

		[Column("MainPartyId", SqlDbType.Int)]
		public int? MainPartyId{ get; set; }

		[Column("AddressId", SqlDbType.Int)]
		public int? AddressId{ get; set; }

		[Column("PhoneCulture", SqlDbType.VarChar)]
		public string PhoneCulture{ get; set; }

		[Column("PhoneAreaCode", SqlDbType.VarChar)]
		public string PhoneAreaCode{ get; set; }

		[Column("PhoneNumber", SqlDbType.VarChar)]
		public string PhoneNumber{ get; set; }

		[Column("PhoneType", SqlDbType.TinyInt)]
		public byte PhoneType{ get; set; }

		[Column("GsmType", SqlDbType.TinyInt)]
		public byte? GsmType{ get; set; }

	}


}
