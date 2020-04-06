namespace NeoSistem.MakinaTurkiye.Data
{
    using EnterpriseEntity.Business;
    using EnterpriseEntity.Extensions;
    using EnterpriseEntity.Extensions.Data;
    using System.Collections.Generic;
    using System.Data;

    public class FavoriteStore : BusinessDataEntity
  {
    public DataTable GetSearchWebByMainPartyId(ref int TotalRecord, int PageDimension, int Page, int MainPartyId)
    {
      var prms = new List<IDataParameter> 
      { 
        TotalRecord.OutSqlParameter("TotalRecord"), 
        PageDimension.InSqlParameter("PageDimension"),
        Page.InSqlParameter("Page"), 
        MainPartyId.InSqlParameter("MainPartyId")
      };
      DataTable dt = ExecuteDataSet("spFavoriteStoreSearchByMainPartyId", prms).Tables[0];
      TotalRecord = prms[0].Value.ToInt32();

      return dt;
    }

  }
}
