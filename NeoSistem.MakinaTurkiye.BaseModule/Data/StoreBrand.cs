namespace NeoSistem.MakinaTurkiye.Data
{
    using EnterpriseEntity.Business;
    using EnterpriseEntity.Extensions.Data;
    using System.Collections.Generic;
    using System.Data;

    public class StoreBrand : BusinessDataEntity
  {
    public DataTable GetStoreBrandByMainPartyId(int StoreId)
    {
      var prm = new List<IDataParameter> 
      {
        StoreId.InSqlParameter("StoreId")
      };
      return ExecuteDataSet("spStoreBrandByMainPartyId", prm).Tables[0];
    }


  }
}