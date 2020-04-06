namespace NeoSistem.MakinaTurkiye.Data
{
    using EnterpriseEntity.Business;
    using EnterpriseEntity.Extensions;
    using EnterpriseEntity.Extensions.Data;
    using System.Collections.Generic;
    using System.Data;

    public class Message : BusinessDataEntity
  {
    public DataTable GetItemsByMainPartyIds(string fromMainPartyIds, byte messagetype)
    {
      var prms = new HashSet<IDataParameter> {
        fromMainPartyIds.InSqlParameter("MainPartyId"),
        messagetype.InSqlParameter("MessageType") 

      };

      DataSet ds = ExecuteDataSet("SP_GetMessageByMainPartyIdByMessageType_New", prms);

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

      DataTable dt = ExecuteDataSet("spMessageSearch", prms).Tables[0];
      TotalRecord = prms[0].Value.ToInt32();
      return dt;
    }

  }
}