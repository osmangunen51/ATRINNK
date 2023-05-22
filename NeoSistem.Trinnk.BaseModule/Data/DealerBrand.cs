namespace NeoSistem.Trinnk.Data
{
    using EnterpriseEntity.Business;
    using EnterpriseEntity.Extensions.Data;
    using System.Collections.Generic;
    using System.Data;

    public class DealerBrand : BusinessDataEntity
    {
        public DataTable GetDealerBrandByMainPartyId(int MainPartyId)
        {
            var prm = new List<IDataParameter>
      {
        MainPartyId.InSqlParameter("MainPartyId")
      };
            return ExecuteDataSet("spDealerBrandGetItemsByMainPartyId", prm).Tables[0];
        }


    }
}