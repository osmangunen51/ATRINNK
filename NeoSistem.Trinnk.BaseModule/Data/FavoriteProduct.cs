namespace NeoSistem.Trinnk.Data
{
    using EnterpriseEntity.Business;
    using EnterpriseEntity.Extensions;
    using EnterpriseEntity.Extensions.Data;
    using System.Collections.Generic;
    using System.Data;

    public class FavoriteProduct : BusinessDataEntity
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
            DataTable dt = ExecuteDataSet("spFavoriteProductSearchByMainPartyId", prms).Tables[0];
            TotalRecord = prms[0].Value.ToInt32();

            return dt;
        }

    }
}