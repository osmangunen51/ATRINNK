namespace NeoSistem.MakinaTurkiye.Classes
{
    using EnterpriseEntity.Business;
    using EnterpriseEntity.Business.Attributes;
    using System.Data;

    [Table("ActivityType")]
	public partial class ActivityType: EntityObject
	{
		[Column("ActivityTypeId", SqlDbType.TinyInt, PrimaryKey=true, Identity=true)]
		public byte ActivityTypeId{ get; set; }

		[Column("ActivityName", SqlDbType.VarChar)]
		public string ActivityName{ get; set; }

        [Column("Order", SqlDbType.TinyInt)]
        public byte? Order { get; set; }
	}


}
