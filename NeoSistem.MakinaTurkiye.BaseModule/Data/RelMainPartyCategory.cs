namespace NeoSistem.MakinaTurkiye.Data
{
    using EnterpriseEntity.Business;
    using EnterpriseEntity.Extensions.Data;
    using System.Collections.Generic;
    using System.Data;

    public class RelMainPartyCategory : BusinessDataEntity
    {
        public DataTable GetMainPartyRelatedCategoryItemsByMainPartyId(int MainPartyId)
        {
            var prm = new HashSet<IDataParameter>
      {
        MainPartyId.InSqlParameter("MainPartyId")
      };
            return ExecuteDataSet("spMainPartyRelatedCategoryItemsByMainPartyId", prm).Tables[0];
        }

        public void MainPartyRelatedCategoryDeleteByMainPartyId(int MainPartyId)
        {
            var prm = new HashSet<IDataParameter>
      {
        MainPartyId.InSqlParameter("MainPartyId")
      };
            ExecuteNonQuery("spMainPartyRelatedCategoryDeleteByMainPartyId", prm);
        }


        public DataTable GetMainCategoryByAuthenticationUserID(int mainPartyId)
        {
            var prm = new List<IDataParameter>
      {
        mainPartyId.InSqlParameter("mainPartyId")
      };

            return ExecuteDataSet("spMainCategoryByAuthenticationUserID", prm).Tables[0];

        }
    }
}