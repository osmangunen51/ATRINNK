namespace NeoSistem.MakinaTurkiye.Classes
{
    using EnterpriseEntity.Business;
    using EnterpriseEntity.Business.Attributes;
    using System.Data;

    [Table("StoreActivityType")]
	public partial class StoreActivityType: EntityObject
	{
		[Column("ActivityTypeId", SqlDbType.TinyInt)]
		public byte ActivityTypeId{ get; set; }

		[Column("StoreId", SqlDbType.Int)]
		public int StoreId{ get; set; }

	}


}
