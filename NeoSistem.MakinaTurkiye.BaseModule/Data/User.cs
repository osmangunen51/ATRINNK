using NeoSistem.EnterpriseEntity.Business;
using NeoSistem.EnterpriseEntity.Extensions;
using NeoSistem.EnterpriseEntity.Extensions.Data;
using System.Collections.Generic;
using System.Data;

namespace NeoSistem.MakinaTurkiye.Data
{

    public class User : BusinessDataEntity
    {
        public Classes.User Login(string UserName, string UserPass)
        {
            Classes.User curUser = null;

            var prms = new[] {
        UserName.InSqlParameter("UserName"),
        UserPass.InSqlParameter("UserPass")
      };

            var dr = ExecuteReader("spUserLogin", prms);

            if (dr.Read())
            {
                curUser = new Classes.User
                {
                    UserName = dr["UserName"].ToString(),
                    UserPass = dr["UserPass"].ToString(),
                    UserId = dr["UserId"].ToByte(),
                    CallCenterUrl= dr["CallCenterUrl"].ToString()
                };
            }

            return curUser;
        }

        public DataTable GetPermissions(int userId)
        {
            var prms = new[] {
        userId.InSqlParameter("UserId")
      };

            var ds = ExecuteDataSet("spPermissionGetItemsByUserId", prms);
            return ds.Tables[0];
        }

        public DataTable GetSearch(ref int TotalRecord, string SearchText, short PageDimension, short Page, int OrderField, bool Desc)
        {
            var prms = new List<IDataParameter>
      {
          TotalRecord.OutSqlParameter("TotalRecord"),
          SearchText.InSqlParameter("SearchText", SqlDbType.NVarChar, 50),
          PageDimension.InSqlParameter("PageDimension"),
          Page.InSqlParameter("Page"),
          OrderField.InSqlParameter("OrderField"),
          Desc.InSqlParameter("Desc"),
      };
            DataSet ds = ExecuteDataSet("spUserSearch", prms);
            TotalRecord = prms[0].Value.ToInt32();

            return ds.Tables[0];
        }

        public DataTable Search(ref int TotalRecord, int PageDimension, int Page, string Where, string OrderName, string Order)
        {
            var prms = new List<IDataParameter> {
        TotalRecord.InOutSqlParameter("TotalRecord"),
        PageDimension.InSqlParameter("PageDimension"),
        Page.InSqlParameter("Page"),
        Where.InSqlParameter("Where", SqlDbType.NVarChar),
        OrderName.InSqlParameter("OrderName", SqlDbType.NVarChar),
        Order.InSqlParameter("Order", SqlDbType.NVarChar)
      };

            DataSet ds = ExecuteDataSet("spUserSearch", prms);
            TotalRecord = prms[0].Value.ToInt32();
            return ds.Tables[0];
        }
    }

}
