namespace NeoSistem.MakinaTurkiye.Data
{
    using EnterpriseEntity.Business;
    using EnterpriseEntity.Extensions;
    using EnterpriseEntity.Extensions.Data;
    using System.Collections.Generic;
    using System.Data;
    using System.Text;

    public class Category : BusinessDataEntity
    {
        public int Delete(int categoryId)
        {
            var prm = new[] {
        categoryId.InSqlParameter("CategoryId")
      };

            return ExecuteScalar("spCategoryDelete", prm).ToInt32();
        }

        public DataTable CategoryTopCategoriesByCategoryId(int CategoryId)
        {
            var prm = new[] { CategoryId.InSqlParameter("CategoryId") };
            return ExecuteDataSet("spCategoryTopCategoriesByCategoryId", prm).Tables[0];
        }
        public void UpdateProductCountOnCategorys(string catTree)
        {
            var prm = new[] { catTree.InSqlParameter("catTree") };
            ExecuteNonQuery("UpdateCountCategory", prm);

        }
        //public int CategoryProductCountByCategoryId(int CategoryId, byte CategoryType)
        //{
        //  var prm = new[] { CategoryId.InSqlParameter("CategoryId"), CategoryType.InSqlParameter("CategoryType") };
        //  var value = ExecuteScalar("spCategoryProductCountByCategoryId", prm);
        //  return value.ToInt32();
        //}
        //sağ taraf kategori seçimi.
        public DataTable CategoryMainPartyLeft(int CategoryId)
        {
            var prm = new List<IDataParameter> { CategoryId.InSqlParameter("CategoryId") };
            DataTable dt = ExecuteDataSet("CategoryMainParty", prm).Tables[0];
            return dt;
        }
        public DataTable GetCategoryBySectorIdAndCategoryGroupId(int SectorId, byte CategoryRoute)
        {
            var prm = new HashSet<IDataParameter>
      {
        SectorId.InSqlParameter("SectorId"),
        CategoryRoute.InSqlParameter("CategoryRoute")
      };
            return ExecuteDataSet("spCategoryBySectorIdAndCategoryGroupId", prm).Tables[0];
        }

        public DataTable ParentCategoryByCategoryProductGroup()
        {
            return ExecuteDataSet("spParentCategoryByCategoryProductGroup").Tables[0];
        }

        public DataTable GetCategoryItemsByGroupType(byte GroupType)
        {
            var prm = new HashSet<IDataParameter>
      {
        GroupType.InSqlParameter("CategoryGroupType")
      };
            return ExecuteDataSet("spCategoryItemsByGroupType", prm).Tables[0];
        }

        public DataTable GetCategoryByCategoryParent(int CategoryId, byte CategoryRoute)
        {
            var prms = new HashSet<IDataParameter>
      {
        CategoryId.InSqlParameter("CategoryId"),
        CategoryRoute.InSqlParameter("CategoryRoute")
      };

            return ExecuteDataSet("spCategoryByCategoryParent", prms).Tables[0];
        }

        public DataTable CategoryGetSectorItemsByCategoryParent(int CategoryParentId)
        {
            var prms = new HashSet<IDataParameter>
      {
        CategoryParentId.InSqlParameter("CategoryParentId")
      };

            return ExecuteDataSet("spCategoryGetSectorItemsByCategoryParentId", prms).Tables[0];
        }

        public DataTable GetProductByLastAddedTop10()
        {
            return ExecuteDataSet("spProductGetDataByLastAddedTop10").Tables[0];
        }

        public Classes.Category GetTopCategoryByCategoryParentId(int CategoryParentId)
        {
            IDataReader dr = null;
            try
            {
                var prm = new HashSet<IDataParameter>
        {
           CategoryParentId.InSqlParameter("CategoryParentId")
        };

                dr = ExecuteReader("spGetTopCategoryByCategoryParentId", prm);
                if (dr.Read())
                {
                    var curCategory = new Classes.Category
                    {
                        Active = dr["Active"].ToBoolean(),
                        CategoryId = dr["CategoryId"].ToInt32(),
                        CategoryName = dr["CategoryName"].ToString(),
                        CategoryOrder = dr["CategoryOrder"].ToByte(),
                        CategoryParentId = dr["CategoryParentId"].ToInt32(),
                        CategoryType = dr["CategoryType"].ToByte(),
                        LastUpdateDate = dr["LastUpdateDate"].ToDateTime(),
                        LastUpdaterId = dr["LastUpdaterId"].ToInt32(),
                        RecordCreatorId = dr["RecordCreatorId"].ToInt32(),
                        RecordDate = dr["RecordDate"].ToDateTime(),
                        Title = dr["Title"].ToString(),
                        Description = dr["Description"].ToString(),
                        Keywords = dr["Keywords"].ToString(),
                    };
                }
                return new Classes.Category();
            }
            finally
            {
                if (dr != null)
                    dr.Close();
            }
        }

        public DataTable GetCategorySectorAndProductGroup()
        {
            return ExecuteDataSet("spCategorySectorAndProductGroup").Tables[0];
        }

        public DataTable GetCategoryParentByCategoryId(int CategoryId, byte MainCategoryType)
        {
            var prms = new HashSet<IDataParameter>
      {
        CategoryId.InSqlParameter("CategoryId"),
        MainCategoryType.InSqlParameter("mainCategoryType")
      };

            return ExecuteDataSet("spCategoryParentItemsByCategoryId", prms).Tables[0];
        }

        public DataTable GetCategoryParentByCategoryIdAndCategoryGroupType(int CategoryId, byte CategoryType)
        {
            var prms = new HashSet<IDataParameter>
      {
        CategoryId.InSqlParameter("CategoryId"),
        CategoryType.InSqlParameter("CategoryType")
      };

            return ExecuteDataSet("spCategoryParentItemsByCategoryIdAndCategoryGroupType", prms).Tables[0];
        }

        public DataTable GetCategoryParentBySectorId(int CategoryId)
        {
            var prms = new HashSet<IDataParameter>
      {
        CategoryId.InSqlParameter("CategoryId")
      };

            return ExecuteDataSet("spCategoryParentItemsByCategoryId", prms).Tables[0];
        }

        public DataSet GetCategoryParentAndSectorByCategoryId(int CategoryId, int SectorId)
        {
            var prms = new HashSet<IDataParameter>
      {
        CategoryId.InSqlParameter("CategoryId"),
        SectorId.InSqlParameter("SectorId")
      };
            return ExecuteDataSet("spCategoryParentAndSectorItemsByCategoryId", prms);
        }

        public DataTable GetCategoryParentByCategoryIdAndCategoryType(int CategoryId, byte CategoryType)
        {
            var prms = new HashSet<IDataParameter>
      {
        CategoryId.InSqlParameter("CategoryId"),
        CategoryType.InSqlParameter("CategoryType")
      };

            return ExecuteDataSet("spCategoryParentItemsByCategoryIdAndCategoryType", prms).Tables[0];
        }

        public DataTable GetCategoryParentByCategoryIdAndCategoryTypeById(int CategoryId, byte CategoryType)
        {
            var prms = new HashSet<IDataParameter>
      {
        CategoryId.InSqlParameter("CategoryId"),
        CategoryType.InSqlParameter("CategoryType")
      };

            return ExecuteDataSet("spCategoryParentItemsByCategoryIdAndCategoryTypeById", prms).Tables[0];
        }

        public DataTable GetCategoryItemByCategoryId(int CategoryId)
        {
            var prms = new HashSet<IDataParameter>
      {
        CategoryId.InSqlParameter("CategoryId")
      };

            return ExecuteDataSet("spCategoryItemByCategoryId", prms).Tables[0];
        }

        public DataTable CategoryItemsByCategoryIdAndCategoryGroupType(int CategoryId, byte CategoryGroupType)
        {
            var prms = new HashSet<IDataParameter>
      {
        CategoryId.InSqlParameter("CategoryId"),
        CategoryGroupType.InSqlParameter("CategoryGroupType")
      };

            return ExecuteDataSet("spCategoryItemsByCategoryIdAndCategoryGroupType", prms).Tables[0];
        }

        public DataTable CategoryGroupParentItemsByCategoryId(int CategoryId)
        {
            var prm = new HashSet<IDataParameter>
      {
        CategoryId.InSqlParameter("CategoryId")
      };
            return ExecuteDataSet("spCategoryGroupParentItemsByCategoryId", prm).Tables[0];
        }

        public DataTable HavingId(params int[] categoryIds)
        {
            var builder = new StringBuilder();

            foreach (int item in categoryIds) builder.AppendFormat("{0},", item);

            builder.Remove(builder.Length - 1, 1);

            var prms = new[] {
        builder.ToString().InSqlParameter("InClause")
      };

            var ds = ExecuteDataSet("spCategoryHavingId", prms);

            return ds.Tables[0];
        }

        public DataTable HavingIdAndMainPartyId(int MainPartyId, params int[] categoryIds)
        {
            var builder = new StringBuilder();

            foreach (int item in categoryIds) builder.AppendFormat("{0},", item);

            builder.Remove(builder.Length - 1, 1);

            var prms = new[] {
        builder.ToString().InSqlParameter("InClause"),
        MainPartyId.InSqlParameter("MainPartyId")
      };

            var ds = ExecuteDataSet("spCategoryHavingIdByMainPartyId", prms);

            return ds.Tables[0];
        }

        public DataSet CategoryParentAndSubCategoriesByCategoryId(int CategoryId)
        {
            var prm = new[] { CategoryId.InSqlParameter("CategoryId") };
            return ExecuteDataSet("spCategoryParentAndSubCategoriesByCategoryId", prm);
        }

        public DataTable ProductCategoryItems(int MainPartyId)
        {
            var prm = new HashSet<IDataParameter>
      {
        MainPartyId.InSqlParameter("MainPartyId")
      };
            return ExecuteDataSet("spCategoryGetProductCategoryItemsByMainPartyId", prm).Tables[0];
        }

    }
}