using NeoSistem.EnterpriseEntity.Business;
using NeoSistem.EnterpriseEntity.Extensions;
using NeoSistem.EnterpriseEntity.Extensions.Data;
using System.Collections.Generic;
using System.Data;

namespace NeoSistem.MakinaTurkiye.Data
{

    public class CategoryProductGroup : BusinessDataEntity
    {
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

            DataSet ds = ExecuteDataSet("spCategoryProductGroupSearch", prms);
            TotalRecord = prms[0].Value.ToInt32();
            return ds.Tables[0];
        }
    }

}
