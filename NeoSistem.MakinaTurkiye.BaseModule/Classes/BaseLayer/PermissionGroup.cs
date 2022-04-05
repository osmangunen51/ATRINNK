namespace NeoSistem.MakinaTurkiye.Classes
{
    using EnterpriseEntity.Business;
    using EnterpriseEntity.Business.Attributes;
    using System.Data;

    [Table("PermissionGroup")]
    public partial class PermissionGroup : EntityObject
    {
        [Column("PermissionGroupId", SqlDbType.Int, PrimaryKey = true, Identity = true)]
        public int PermissionGroupId { get; set; }

        [Column("GroupId", SqlDbType.Int)]
        public int GroupId { get; set; }

        [Column("PermissionId", SqlDbType.Int)]
        public int PermissionId { get; set; }

    }


}
