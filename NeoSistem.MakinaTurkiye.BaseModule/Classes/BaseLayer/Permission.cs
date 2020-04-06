namespace NeoSistem.MakinaTurkiye.Classes
{
    using EnterpriseEntity.Business;
    using EnterpriseEntity.Business.Attributes;
    using System.Data;

    [Table("Permission")]
	public partial class Permission: EntityObject
	{
		[Column("PermissionId", SqlDbType.Int, PrimaryKey=true, Identity=true)]
		public int PermissionId{ get; set; }

		[Column("PermissionName", SqlDbType.VarChar)]
		public string PermissionName{ get; set; }

		[Column("PermissionGroupName", SqlDbType.VarChar)]
		public string PermissionGroupName{ get; set; }

	}


}
