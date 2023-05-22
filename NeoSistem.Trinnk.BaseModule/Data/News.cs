namespace NeoSistem.Trinnk.Data
{
    using EnterpriseEntity.Business;
    using EnterpriseEntity.Extensions;
    using EnterpriseEntity.Extensions.Data;
    using System.Collections.Generic;
    using System.Data;

    public class News : BusinessDataEntity
    {

        public DataTable GetNewsTopByTopCount(byte NewsCount)
        {
            var prm = new HashSet<IDataParameter>
      {
        NewsCount.InSqlParameter("NewsCount")
      };

            DataTable dt = ExecuteDataSet("spNewsGetItemsByNewsTopCount", prm).Tables[0];
            return dt;
        }

        public DataTable Search(ref int TotalRecord, int PageDimension, int Page, string Where, string OrderName, string Order)
        {
            var prms = new List<IDataParameter>
      {
        TotalRecord.InOutSqlParameter("TotalRecord"),
        PageDimension.InSqlParameter("PageDimension"),
        Page.InSqlParameter("Page"),
        Where.InSqlParameter("Where", SqlDbType.NVarChar),
        OrderName.InSqlParameter("OrderName", SqlDbType.NVarChar),
        Order.InSqlParameter("Order", SqlDbType.NVarChar)
      };

            DataTable dt = ExecuteDataSet("spNewsSearch", prms).Tables[0];
            TotalRecord = prms[0].Value.ToInt32();
            return dt;
        }

    }
}