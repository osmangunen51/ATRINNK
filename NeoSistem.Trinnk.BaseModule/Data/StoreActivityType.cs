namespace NeoSistem.Trinnk.Data
{
    using EnterpriseEntity.Business;
    using EnterpriseEntity.Extensions.Data;
    using System.Collections.Generic;
    using System.Data;

    public class StoreActivityType : BusinessDataEntity
    {

        public DataTable GetStoreActivityTypeItemsByMainPartyId(int MainPartyId)
        {
            var prm = new HashSet<IDataParameter>
      {
        MainPartyId.InSqlParameter("MainPartyId")
      };
            return ExecuteDataSet("spStoreActivityTypeItemsByMainPartyId", prm).Tables[0];
        }

        public void ActivityTypeDeleteByMainPartyId(int MainPartyId)
        {
            var prm = new HashSet<IDataParameter>
      {
        MainPartyId.InSqlParameter("MainPartyId")
      };
            ExecuteNonQuery("spActivityTypeDeleteByMainPartyId", prm);
        }

    }
}