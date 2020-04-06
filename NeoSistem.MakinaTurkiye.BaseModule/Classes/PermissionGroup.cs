
namespace NeoSistem.MakinaTurkiye.Classes
{
    using EnterpriseEntity.Extensions.Data;

    public partial class PermissionGroup
  {
    public void DeleteByGroupId(int groupId)
    {
      var prms = new[] { 
        groupId.InSqlParameter("GroupId")
      };

      ExecuteNonQuery("spPermissionGroupDeleteByGroupId", prms);
    }
  }
}
