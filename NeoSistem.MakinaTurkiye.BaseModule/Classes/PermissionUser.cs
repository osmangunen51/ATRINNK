


namespace NeoSistem.MakinaTurkiye.Classes
{
    using EnterpriseEntity.Extensions.Data;

    public partial class PermissionUser
    {
        public void DeleteByUserId(int userId)
        {
            var prms = new[] {
        userId.InSqlParameter("UserId")
      };

            ExecuteNonQuery("spPermissionUserDeleteByUserId", prms);
        }
    }
}
