namespace NeoSistem.MakinaTurkiye.Data
{
    using EnterpriseEntity.Business;
    using EnterpriseEntity.Extensions;
    using EnterpriseEntity.Extensions.Data;
    using System.Collections.Generic;
    using System.Data;

    public class Video : BusinessDataEntity
  {
    public DataTable GetItemsByProductId(int productId)
    {
      var prm = new[] { 
        productId.InSqlParameter("ProductId")
      };

      var ds = ExecuteDataSet("spVideoGetItemsByProductId", prm);

      return ds.Tables[0];
    }

    public DataTable VideoSearch(ref int TotalRecord, int PageDimension, int Page, int CategoryId)
    {
      var prms = new List<IDataParameter> 
      { 
        TotalRecord.OutSqlParameter("TotalRecord"),
        PageDimension.InSqlParameter("PageDimension"),
        Page.InSqlParameter("Page"),
        CategoryId.InSqlParameter("CategoryId")
      };

      DataTable dt = ExecuteDataSet("spVideoSearch", prms).Tables[0];
      TotalRecord = prms[0].Value.ToInt32();

      return dt;
    }

    public DataTable GetMostWatchedFiveVideo()
    {
        var ds = ExecuteDataSet("spMostWatchedFiveVideo");

        return ds.Tables[0];
    }
  }
}