namespace NeoSistem.MakinaTurkiye.Classes
{
    using EnterpriseEntity.Business;
    using EnterpriseEntity.Business.Attributes;
    using System;
    using System.Data;

    [Table("Fair")]
	public partial class Fair: EntityObject
	{
		[Column("FairId", SqlDbType.Int, PrimaryKey=true, Identity=true)]
		public int FairId{ get; set; }

		[Column("MainPartyId", SqlDbType.Int)]
		public int MainPartyId{ get; set; }

		[Column("FairTitle", SqlDbType.NVarChar)]
		public string FairTitle{ get; set; }

		[Column("FairContent", SqlDbType.NVarChar)]
		public string FairContent{ get; set; }

		[Column("FairBeginDate", SqlDbType.Date)]
		public DateTime FairBeginDate{ get; set; }

		[Column("FairEndDate", SqlDbType.Date)]
		public DateTime FairEndDate{ get; set; }

		[Column("Active", SqlDbType.Bit)]
		public bool Active{ get; set; }

	}


}
