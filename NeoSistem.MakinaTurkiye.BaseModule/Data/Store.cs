namespace NeoSistem.MakinaTurkiye.Data
{
    using EnterpriseEntity.Business;
    using EnterpriseEntity.Extensions;
    using EnterpriseEntity.Extensions.Data;
    using System.Collections.Generic;
    using System.Data;

    public class Store : BusinessDataEntity
    {
        public DataTable Search(ref int TotalRecord, int PageDimension, int Page, string Where, string OrderName, string Order)
        {
            var prms = new List<IDataParameter>
          {
            TotalRecord.InOutSqlParameter("TotalRecord"),
            PageDimension.InSqlParameter("PageDimension"),
            Page.InSqlParameter("Page"),
            Where.InSqlParameter("Where", SqlDbType.NVarChar),
            OrderName.InSqlParameter("OrderName", SqlDbType.NVarChar),
            Order.InSqlParameter("Order", SqlDbType.NVarChar)
          };

            DataTable dt = ExecuteDataSet("spStoreSearch", prms).Tables[0];
            TotalRecord = prms[0].Value.ToInt32();
            return dt;
        }

        public DataTable GetSearchWeb(string sort, ref int TotalRecord, string SearchText, int PageDimension, int Page, int OrderField, byte SearchType, int CategoryId, int CityId, int LocalityId)
        {
            var prms = new List<IDataParameter>
      {
          TotalRecord.OutSqlParameter("TotalRecord"),
          SearchText.InSqlParameter("SearchText", SqlDbType.NVarChar, 50),
          PageDimension.InSqlParameter("PageDimension"),
          Page.InSqlParameter("Page"),
          OrderField.InSqlParameter("OrderField"),
          SearchType.InSqlParameter("SearchType"),
          CategoryId.InSqlParameter("CategoryId"),
          CityId.InSqlParameter("CityId"),
          LocalityId.InSqlParameter("LocalityId"),
          sort.InSqlParameter("Sort"),
      };
            DataTable dt = ExecuteDataSet("spStoreSearchWeb", prms).Tables[0];
            TotalRecord = prms[0].Value.ToInt32();

            return dt;
        }

        public void HavingStoreHead(ref bool ProductExist, ref bool AboutUs, ref bool Services, ref bool Branch, ref bool Brand, ref bool Dealer, ref bool Dealership, int MainPartyId)
        {
            var prms = new List<IDataParameter>
      {
        AboutUs.OutSqlParameter("AboutUs"),
        Services.OutSqlParameter("Services"),
        Branch.OutSqlParameter("Branch"),
        Brand.OutSqlParameter("Brand"),
        Dealer.OutSqlParameter("Dealer"),
        Dealership.OutSqlParameter("Dealership"),
        ProductExist.OutSqlParameter("ProductExist"),
        MainPartyId.InSqlParameter("MainPartyId")

      };

            ExecuteNonQuery("spStoreHeaderHavingByMainPartyId", prms);

            AboutUs = prms[0].Value.ToBoolean();
            Services = prms[1].Value.ToBoolean();
            Branch = prms[2].Value.ToBoolean();
            Brand = prms[3].Value.ToBoolean();
            Dealer = prms[4].Value.ToBoolean();
            Dealership = prms[5].Value.ToBoolean();
            ProductExist = prms[6].Value.ToBoolean();
        }

        public DataTable GetStoreTopByTopCount(byte StoreCount)
        {
            var prm = new HashSet<IDataParameter>
      {
        StoreCount.InSqlParameter("StoreCount")
      };

            return ExecuteDataSet("spStoreGetItemsByStoreTopCount", prm).Tables[0];
        }

        public DataTable AddressTypeGetItemsByMainPartyId(int MainPartyId)
        {
            var prm = new HashSet<IDataParameter>
      {
        MainPartyId.InSqlParameter("MainPartyId")
      };
            return ExecuteDataSet("spAddressTypeGetItemsByMainPartyId", prm).Tables[0];
        }

        public void StoreUpdateAbout(int MainPartyId, string FounderText, string GeneralText, string HistoryText, string PhilosophyText)
        {
            var prm = new HashSet<IDataParameter>
      {
        MainPartyId.InSqlParameter("MainPartyId"),
        FounderText.InSqlParameter("FounderText"),
        GeneralText.InSqlParameter("GeneralText"),
        HistoryText.InSqlParameter("HistoryText"),
        PhilosophyText.InSqlParameter("PhilosophyText")
      };
            ExecuteNonQuery("spStoreUpdateAbout", prm);
        }

        public DataTable MainPageSearch(ref int TotalRecord, int PageDimension, int Page, string SearchText, int CategoryId)
        {
            var prms = new List<IDataParameter>
      {
        TotalRecord.OutSqlParameter("TotalRecord"),
        PageDimension.InSqlParameter("PageDimension"),
        Page.InSqlParameter("Page"),
        SearchText.InSqlParameter("SearchText"),
        CategoryId.InSqlParameter("CategoryId")
      };

            DataTable dt = ExecuteDataSet("spStoreWebSearchOrderMainPage", prms).Tables[0];
            TotalRecord = prms[0].Value.ToInt32();

            return dt;
        }

        public DataTable StoreGetBySearchText(string SearchText)
        {
            var prms = new List<IDataParameter>
      {
        SearchText.InSqlParameter("SearchText")
      };

            DataTable dt = ExecuteDataSet("spStoreGetBySearchText", prms).Tables[0];
            return dt;
        }

        public DataTable GetWatchedFiveVideo()
        {
            DataTable dt = ExecuteDataSet("spStoreWatchedFiveVideo").Tables[0];
            return dt;
        }

    }
}