namespace NeoSistem.MakinaTurkiye.Classes
{
    using EnterpriseEntity.Business;
    using EnterpriseEntity.Business.Attributes;
    using System;
    using System.Data;

    [Table("Currency")]
	public partial class Currency: EntityObject
	{
		[Column("CurrencyId", SqlDbType.TinyInt, PrimaryKey=true, Identity=true)]
		public byte CurrencyId{ get; set; }

		[Column("CurrencyName", SqlDbType.VarChar)]
		public string CurrencyName{ get; set; }

		[Column("CurrencyFullName", SqlDbType.VarChar)]
		public string CurrencyFullName{ get; set; }

		[Column("CurrencyCodeName", SqlDbType.VarChar)]
		public string CurrencyCodeName{ get; set; }

		[Column("Active", SqlDbType.Bit)]
		public bool Active{ get; set; }

		[Column("RecordDate", SqlDbType.DateTime)]
		public DateTime RecordDate{ get; set; }

		[Column("RecordCreatorId", SqlDbType.Int)]
		public int RecordCreatorId{ get; set; }

	}


}
