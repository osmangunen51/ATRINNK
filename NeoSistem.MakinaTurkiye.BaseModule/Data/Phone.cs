namespace NeoSistem.MakinaTurkiye.Data
{
    using EnterpriseEntity.Business;
    using EnterpriseEntity.Extensions.Data;
    using System.Collections.Generic;
    using System.Data;

    public class Phone : BusinessDataEntity
    {
        public DataTable GetPhoneItemsByMainPartyId(int mainPartyId)
        {
            var prm = new HashSet<IDataParameter>
      {
        mainPartyId.InSqlParameter("MainPartyId")
      };

            return ExecuteDataSet("spPhoneGetItemsByMainPartyId", prm).Tables[0];
        }

        public DataTable GetPhoneItemsByAddressId(int AddressId)
        {
            var prm = new HashSet<IDataParameter>
      {
        AddressId.InSqlParameter("AddressId")
      };
            return ExecuteDataSet("spPhoneGetItemsByAddressId", prm).Tables[0];
        }
    }
}