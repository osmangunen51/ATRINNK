namespace NeoSistem.Trinnk.Data
{
    using EnterpriseEntity.Business;
    using EnterpriseEntity.Extensions;
    using EnterpriseEntity.Extensions.Data;
    using System.Collections.Generic;
    using System.Data;

    public class Order : BusinessDataEntity
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
            DataSet dtresult = ExecuteDataSet("spOrderSearch", prms);
            DataTable dt = new DataTable();
            if (dtresult!=null)
            {
                if (dtresult.Tables.Count>0)
                {
                    dt = dtresult.Tables[0];
                }
                
            }
            TotalRecord = prms[0].Value.ToInt32();
            return dt;
        }

    }
}