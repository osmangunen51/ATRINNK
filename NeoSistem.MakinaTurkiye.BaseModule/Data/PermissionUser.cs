using NeoSistem.EnterpriseEntity.Business;
using NeoSistem.EnterpriseEntity.Extensions.Data;
using System.Data;

namespace NeoSistem.MakinaTurkiye.Data
{
    public class PermissionUser : BusinessDataEntity
    {

        public DataTable GetItemsByUserId(int userId)
        {
            var prm = new[] {
        userId.InSqlParameter("UserId")
      };

            var ds = ExecuteDataSet("spPermissionUserGetItemsByUserId", prm);

            return ds.Tables[0];
        }
    }
}