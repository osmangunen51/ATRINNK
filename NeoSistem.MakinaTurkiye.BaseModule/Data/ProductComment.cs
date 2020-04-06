namespace NeoSistem.MakinaTurkiye.Data
{
    using EnterpriseEntity.Business;
    using EnterpriseEntity.Extensions.Data;
    using System.Collections.Generic;
    using System.Data;

    public class ProductComment : BusinessDataEntity
  {
    public DataTable ProductCommentItemsByMainPartyId(int MainPartyId)
    {
      var prm = new List<IDataParameter> 
      {
        MainPartyId.InSqlParameter("MainPartyId")
      };
      return ExecuteDataSet("spProductCommentItemsByMainPartyId", prm).Tables[0];
    }

    public DataTable ProductCommentItemsByProductId(int ProductId)
    {
      var prm = new List<IDataParameter> 
      {
        ProductId.InSqlParameter("ProductId")
      };
      return ExecuteDataSet("spProductCommentItemsByProductId", prm).Tables[0];
    }

  }
}