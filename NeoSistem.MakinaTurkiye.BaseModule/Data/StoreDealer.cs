namespace NeoSistem.MakinaTurkiye.Data
{
    using EnterpriseEntity.Business;
    using EnterpriseEntity.Extensions.Data;
    using System.Collections.Generic;
    using System.Data;

    public class StoreDealer : BusinessDataEntity
  {
    public DataTable StoreDealerItemsByMainPartyId(int MainPartyId,byte DealerType)
    {
      var prm = new HashSet<IDataParameter> 
      { 
        MainPartyId.InSqlParameter("MainPartyId"),
        DealerType.InSqlParameter("DealerType")
      };
      return ExecuteDataSet("spStoreDealerItemsByMainPartyId", prm).Tables[0];
    }

  }
}