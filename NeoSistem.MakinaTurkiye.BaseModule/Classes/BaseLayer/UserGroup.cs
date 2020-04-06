namespace NeoSistem.MakinaTurkiye.Classes
{
    using EnterpriseEntity.Business;
    using EnterpriseEntity.Business.Attributes;
    using System.Data;

    [Table("UserGroup")]
  public partial class UserGroup : EntityObject
  {
    [Column("UserGroupId", SqlDbType.Int, PrimaryKey = true, Identity = true)]
    public int UserGroupId { get; set; }

    [Column("GroupName", SqlDbType.VarChar)]
    public string GroupName { get; set; }

  }


}
