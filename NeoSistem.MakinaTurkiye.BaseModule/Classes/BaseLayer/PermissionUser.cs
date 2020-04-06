namespace NeoSistem.MakinaTurkiye.Classes
{
    using EnterpriseEntity.Business;
    using EnterpriseEntity.Business.Attributes;
    using System.Data;

    [Table("PermissionUser")]
	public partial class PermissionUser: EntityObject
	{
		[Column("PermissionUserId", SqlDbType.Int, PrimaryKey=true, Identity=true)]
		public int PermissionUserId{ get; set; }

		[Column("UserId", SqlDbType.TinyInt)]
		public byte UserId{ get; set; }

		[Column("UserGroupId", SqlDbType.Int)]
		public int UserGroupId{ get; set; }

	}


}
