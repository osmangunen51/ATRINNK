namespace NeoSistem.Trinnk.Data
{
    using EnterpriseEntity.Business;
    using EnterpriseEntity.Extensions.Data;
    using System.Collections.Generic;
    using System.Data;

    public class Constant : BusinessDataEntity
    {
        public DataTable ConstantGetByConstantType(byte ConstantType)
        {
            var prm = new List<IDataParameter>
      {
        ConstantType.InSqlParameter("ConstantType")
      };
            return ExecuteDataSet("spConstantGetItemByConstantType", prm).Tables[0];
        }

    }
}