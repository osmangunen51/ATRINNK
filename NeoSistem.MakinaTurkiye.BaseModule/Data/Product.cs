using NeoSistem.EnterpriseEntity.Business;
using NeoSistem.EnterpriseEntity.Extensions;
using NeoSistem.EnterpriseEntity.Extensions.Data;
using System.Collections.Generic;
using System.Data;

namespace NeoSistem.MakinaTurkiye.Data
{
    public class Product : BusinessDataEntity
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

            DataTable dt = ExecuteDataSet("spProductSearch", prms).Tables[0];
            TotalRecord = prms[0].Value.ToInt32();
            return dt;
        }
        public void ProductRateCalculate()
        {
            //ExecuteNonQuery("ProductRateCalculate");
            ExecuteNonQuery("SP_ProductRateCalculate1");
        }
        public DataTable GetProductListBySearchTypeId(byte SearchTypeId)
        {
            var prms = new List<IDataParameter>
      {
        SearchTypeId.InSqlParameter("SearchTypeId")
      };
            return ExecuteDataSet("spGetProductListBySearchTypeId", prms).Tables[0];
        }

        public DataTable GetProductTop4ByCategoryId(int CategoryId)
        {
            var prms = new List<IDataParameter>
      {
        CategoryId.InSqlParameter("CategoryId")
      };
            return ExecuteDataSet("spProductTop4ByCategoryId", prms).Tables[0];
        }

        public DataTable GetProductShowcase(int ProductCount)
        {
            var prms = new List<IDataParameter>
      {
        ProductCount.InSqlParameter("ProductCount")
      };
            return ExecuteDataSet("spProductGetShowcaseByTopId", prms).Tables[0];
        }

        public DataTable GetProductNewAdded(int ProductCount)
        {
            var prms = new List<IDataParameter>
      {
        ProductCount.InSqlParameter("ProductCount")
      };
            return ExecuteDataSet("spProductGetNewAddedByTopId", prms).Tables[0];
        }

        public DataTable GetSearch(ref int TotalRecord, int PageDimension, int Page, int recordCount, int SearchType, int ProductStatu, int CategoryId)
        {
            var prms = new List<IDataParameter>
      {
        TotalRecord.OutSqlParameter("TotalRecord"),
        PageDimension.InSqlParameter("PageDimension"),
        Page.InSqlParameter("Page"),
        recordCount.InSqlParameter("RecordCount"),
        SearchType.InSqlParameter("SearchType"),
        ProductStatu.InSqlParameter("ProductStatu"),
        CategoryId.InSqlParameter("CategoryId")
      };

            DataTable dt = ExecuteDataSet("spProductWebSearch", prms).Tables[0];
            TotalRecord = prms[0].Value.ToInt32();

            return dt;
        }

        public DataTable GetSearchByLastViewDate(ref int TotalRecord, int PageDimension, int Page, int recordCount, int SearchType, int CategoryId)
        {
            var prms = new List<IDataParameter>
      {
        TotalRecord.OutSqlParameter("TotalRecord"),
        PageDimension.InSqlParameter("PageDimension"),
        Page.InSqlParameter("Page"),
        recordCount.InSqlParameter("RecordCount"),
        SearchType.InSqlParameter("SearchType"),
        CategoryId.InSqlParameter("CategoryId")
      };

            DataTable dt = ExecuteDataSet("spProductWebSearchLastViewDate", prms).Tables[0];
            TotalRecord = prms[0].Value.ToInt32();

            return dt;
        }


        public DataTable GetSearchByMostViewDate(ref int TotalRecord, int PageDimension, int Page, int recordCount, int CategoryId)
        {
            var prms = new List<IDataParameter>
      {
        TotalRecord.OutSqlParameter("TotalRecord"),
        PageDimension.InSqlParameter("PageDimension"),
        Page.InSqlParameter("Page"),
        recordCount.InSqlParameter("RecordCount"),
        CategoryId.InSqlParameter("CategoryId")
      };

            DataTable dt = ExecuteDataSet("spProductWebSearchMostViewDate", prms).Tables[0];
            TotalRecord = prms[0].Value.ToInt32();

            return dt;
        }

        public DataTable GetSearchWebByProductActiveType(ref int TotalRecord, int PageDimension, int Page, byte advertType, int mainPartyId)
        {
            var prms = new List<IDataParameter>
      {
          TotalRecord.OutSqlParameter("TotalRecord"),
          PageDimension.InSqlParameter("PageDimension"),
          Page.InSqlParameter("Page"),
          advertType.InSqlParameter("AdvertType"),
          mainPartyId.InSqlParameter("MainPartyId")
      };
            DataTable dt = ExecuteDataSet("spProductSearchByProductActiveType", prms).Tables[0];
            TotalRecord = prms[0].Value.ToInt32();

            return dt;
        }
        public DataTable GetSearchWebByProductActiveTypeNew(ref int TotalRecord, int PageDimension, int Page, byte advertType, int mainPartyId)
        {
            var prms = new List<IDataParameter>
      {
          TotalRecord.OutSqlParameter("TotalRecord"),
          PageDimension.InSqlParameter("PageDimension"),
          Page.InSqlParameter("Page"),
          advertType.InSqlParameter("AdvertType"),
          mainPartyId.InSqlParameter("MainPartyId")
      };
            DataTable dt = ExecuteDataSet("SP_ProductSearchByProductActiveTypeNew", prms).Tables[0];
            TotalRecord = prms[0].Value.ToInt32();

            return dt;
        }
        public DataTable GetSearchWebByProductActiveTypeByProductNameNew(ref int TotalRecord, int PageDimension, int Page, byte advertType, int mainPartyId, string productName)
        {
            var prms = new List<IDataParameter>
      {
          TotalRecord.OutSqlParameter("TotalRecord"),
          PageDimension.InSqlParameter("PageDimension"),
          Page.InSqlParameter("Page"),
          advertType.InSqlParameter("AdvertType"),
          mainPartyId.InSqlParameter("MainPartyId"),
          productName.InSqlParameter("ProductName")
      };
            DataTable dt = ExecuteDataSet("SP_ProductSearchByProductActiveTypeByProductName", prms).Tables[0];
            TotalRecord = prms[0].Value.ToInt32();

            return dt;

        }
        public DataTable GetProductSearchByCategoryId(ref int TotalRecord, int PageDimension, int Page, int CategoryId)
        {
            var prms = new List<IDataParameter>
      {
          TotalRecord.OutSqlParameter("TotalRecord"),
          PageDimension.InSqlParameter("PageDimension"),
          Page.InSqlParameter("Page"),
          CategoryId.InSqlParameter("CategoryId")
      };
            DataTable dt = ExecuteDataSet("spProductSearchByCategoryId", prms).Tables[0];
            TotalRecord = prms[0].Value.ToInt32();

            return dt;
        }

        public DataTable GetProductSearchByCategoryIdAndMainPartyId(ref int TotalRecord, int PageDimension, int Page, int CategoryId, int MainPartyId)
        {
            var prms = new List<IDataParameter>
      {
          TotalRecord.OutSqlParameter("TotalRecord"),
          PageDimension.InSqlParameter("PageDimension"),
          Page.InSqlParameter("Page"),
          CategoryId.InSqlParameter("CategoryId"),
          MainPartyId.InSqlParameter("MainPartyId")
      };
            DataTable dt = ExecuteDataSet("spProductSearchByCategoryIdAndMainPartyId", prms).Tables[0];
            TotalRecord = prms[0].Value.ToInt32();

            return dt;
        }

        public DataTable GetProductSearchByMainPartyId(ref int TotalRecord, int PageDimension, int Page, int MainPartyId)
        {
            var prms = new List<IDataParameter>
      {
          TotalRecord.OutSqlParameter("TotalRecord"),
          PageDimension.InSqlParameter("PageDimension"),
          Page.InSqlParameter("Page"),
          MainPartyId.InSqlParameter("MainPartyId")
      };
            DataTable dt = ExecuteDataSet("spProductSearchByMainPartyId", prms).Tables[0];
            TotalRecord = prms[0].Value.ToInt32();

            return dt;
        }
        //public static string arama(string text)
        //{
        //  if (!string.IsNullOrEmpty(text))
        //  {
        //    text = text.Replace("I", "ı");
        //    text = text.Replace("İ", "i");
        //    return text;
        //  }
        //  return "";
        //}
        public DataTable MainPageSearch(ref int TotalRecord, int PageDimension, int Page, string SearchText, int CategoryId)
        {
            //SearchText = arama(SearchText);
            var prms = new List<IDataParameter>
      {
        TotalRecord.OutSqlParameter("TotalRecord"),
        PageDimension.InSqlParameter("PageDimension"),
        Page.InSqlParameter("Page"),
        SearchText.InSqlParameter("SearchText"),
        CategoryId.InSqlParameter("CategoryId")
      };

            DataTable dt = ExecuteDataSet("spProductWebSearchOrderMainPage", prms).Tables[0];
            TotalRecord = prms[0].Value.ToInt32();
            return dt;
        }
        public DataTable MainPageSearcha(ref int TotalRecord, int PageDimension, int Page, string SearchText, int CategoryId)
        {
            //SearchText = arama(SearchText);
            var prms = new List<IDataParameter>
      {
        TotalRecord.OutSqlParameter("TotalRecord"),
        PageDimension.InSqlParameter("PageDimension"),
        Page.InSqlParameter("Page"),
        SearchText.InSqlParameter("SearchText"),
        CategoryId.InSqlParameter("CategoryId")
      };

            DataTable dt = ExecuteDataSet("spProductWebSearchOrderMainPage", prms).Tables[0];
            TotalRecord = prms[0].Value.ToInt32();
            return dt;
        }

        public DataTable GetPopulerAds(int ProductCount)
        {
            var prms = new List<IDataParameter>
      {
        ProductCount.InSqlParameter("productCount")
      };

            return ExecuteDataSet("spPopulerAds", prms).Tables[0];
        }

    }
}