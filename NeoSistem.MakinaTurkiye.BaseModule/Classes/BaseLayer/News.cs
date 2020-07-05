namespace NeoSistem.MakinaTurkiye.Classes
{
    using EnterpriseEntity.Business;
    using EnterpriseEntity.Business.Attributes;
    using System;
    using System.Data;

    [Table("News")]
	public partial class News: EntityObject
	{
		[Column("NewsId", SqlDbType.Int, PrimaryKey=true, Identity=true)]
		public int NewsId{ get; set; }

		[Column("MainPartyId", SqlDbType.Int)]
		public int MainPartyId{ get; set; }

		[Column("NewsTitle", SqlDbType.NVarChar)]
		public string NewsTitle{ get; set; }

		[Column("NewsText", SqlDbType.NText)]
		public string NewsText{ get; set; }

		[Column("NewsPicturePath", SqlDbType.NVarChar)]
		public string NewsPicturePath{ get; set; }

		[Column("NewsDate", SqlDbType.Date)]
		public DateTime NewsDate{ get; set; }

		[Column("Active", SqlDbType.Bit)]
		public bool Active{ get; set; }

	}


}
