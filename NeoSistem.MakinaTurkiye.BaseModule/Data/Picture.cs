namespace NeoSistem.MakinaTurkiye.Data
{
    using EnterpriseEntity.Business;
    using EnterpriseEntity.Extensions.Data;
    using System.Data;

    public class Picture : BusinessDataEntity
  {
    public DataTable GetItemsByProductId(int productId)
    {
      var prm = new[] { 
        productId.InSqlParameter("ProductId")
      };

      var ds = ExecuteDataSet("spPictureGetItemsByProductId", prm);

      return ds.Tables[0];
    }

    //public DataTable GetItemsByStoreDealerId(int StoreDealerId)
    //{
    //  var prm = new[] { 
    //    StoreDealerId.InSqlParameter("StoreDealerId")
    //  };

    //  var ds = ExecuteDataSet("spPictureGetItemsByStoreDealerId", prm);

    //  return ds.Tables[0];
    //}

  }
}