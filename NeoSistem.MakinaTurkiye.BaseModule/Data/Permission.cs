using NeoSistem.EnterpriseEntity.Business;
using NeoSistem.EnterpriseEntity.Extensions.Data;
using System.Data;

namespace NeoSistem.MakinaTurkiye.Data
{
    public class Permission : BusinessDataEntity
    {

        public DataTable GetItemsByGroupId(int groupId)
        {
            var prm = new[] {
        groupId.InSqlParameter("GroupId")
      };

            var ds = ExecuteDataSet("spPermissionGetItemsByGroupId", prm);

            return ds.Tables[0];
        }
    }
}