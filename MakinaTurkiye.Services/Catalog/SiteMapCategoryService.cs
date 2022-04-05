using MakinaTurkiye.Core.Data;
using MakinaTurkiye.Data;
using MakinaTurkiye.Entities.StoredProcedures.Catalog;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace MakinaTurkiye.Services.Catalog
{
    public class SiteMapCategoryService : ISiteMapCategoryService
    {
        #region  Fields

        private readonly IDbContext _dbContext;
        private readonly IDataProvider _dataProvider;

        #endregion

        #region Ctor
        public SiteMapCategoryService(IDbContext dbContext, IDataProvider dataProvider)
        {
            _dbContext = dbContext;
            _dataProvider = dataProvider;
        }

        #endregion

        #region Methods

        public IList<SiteMapStoreCategoryResult> GetSiteMapStoreCategories()
        {
            const string sql = "SP_GetSiteMapStoreCategory";
            var categories = _dbContext.SqlQuery<SiteMapStoreCategoryResult>(sql).ToList();
            return categories;
        }

        public IList<SiteMapVideoCategoryResult> GetSiteMapVideoCategories()
        {
            const string sql = "SP_GetSiteMapVideoCategory";
            var categories = _dbContext.SqlQuery<SiteMapVideoCategoryResult>(sql).ToList();
            return categories;
        }

        public IList<SiteMapCategoryResult> GetSiteMapCategories(SiteMapCategoryTypeEnum categoryType)
        {
            const string sql = "SP_GetSiteMapCategory @CategoryType";

            var pCategoryType = _dataProvider.GetParameter();
            pCategoryType.ParameterName = "CategoryType";
            pCategoryType.Value = categoryType;
            pCategoryType.DbType = DbType.Int32;

            var categories = _dbContext.SqlQuery<SiteMapCategoryResult>(sql, pCategoryType).ToList();
            return categories;
        }

        public IList<SiteMapCategoryPlaceResult> GetSiteMapCategoriesPlace(CategoryPlaceTypeEnum categoryPlaceType)
        {
            const string sql = "SP_CategoryPlace @Chosen";

            var pCategoryType = _dataProvider.GetParameter();
            pCategoryType.ParameterName = "Chosen";
            pCategoryType.Value = categoryPlaceType;
            pCategoryType.DbType = DbType.Int32;

            var categories = _dbContext.SqlQuery<SiteMapCategoryPlaceResult>(sql, pCategoryType).ToList();
            return categories;
        }

        #endregion
    }
}
