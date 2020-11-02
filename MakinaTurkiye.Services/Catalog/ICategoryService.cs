using MakinaTurkiye.Entities.StoredProcedures.Catalog;
using MakinaTurkiye.Entities.StoredProcedures.Stores;
using MakinaTurkiye.Entities.StoredProcedures.Videos;
using MakinaTurkiye.Entities.Tables.Catalog;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MakinaTurkiye.Services.Catalog
{
    public interface ICategoryService : ICachingSupported
    {
        IList<Category> GetCategoriesByName(string Name);
        IList<Category> GetAllCategories();

        IList<TopCategoryResult> GetSPTopCategories(int categoryId);
        IList<BottomCategoryResult> GetSPBottomCategories(int categoryId);

        IList<TopCategoryResult> GetTopCategoriesForSearchProduct(string searchText, int categoryId = 0, int customFilterId = 0);

        IList<ProductCategoryForSearchProductResult> GetSPProductCategoryForSearchProduct(string searchText,
                     int categoryId = 0, int brandId = 0, int modelId = 0,
                     int seriesId = 0, int searchType = 0, int countryId = 0, int cityId = 0, int localityId = 0,
                     int customFilterId = 0);

        IList<Category> GetCategoriesByCategoryParentId(int categoryParentId, bool showHidden = false , bool fromCache = true, bool isProductCount=true);

        IList<int> GetCategoryIdsByCategoryParentId(int categoryParentId, bool showHidden = false);

        Category GetCategoryByCategoryId(int categoryId);

        IList<ProductCategoryForSearchProductResult> GetSPProductCategoryForSearchProductOneStep(string searchText);

        IList<VideoCategoryResult> GetSPVideoCategoryForSearchVideo(string searchText, int categoryId);

        IList<VideoCategoryResult> GetSPVideoCategoryByMainPartyId(int mainPartyId, int categoryId);

        IList<StoreCategoryResult> GetSPStoreCategoryByCategoryParentId(int categoryParentId = 0);

        IList<Category> GetBreadCrumbCategoriesByCategoryId(int categoryId);

        IList<Category> GetMainCategories();

        IList<Category> GetCategoriesByMainCategoryType(MainCategoryTypeEnum mainCategoryType);
        IList<Category> GetCategoriesByCategoryType(CategoryTypeEnum categoryType, bool showHidden = false);

        IList<Category> GetCategoriesLessThanCategoryType(CategoryTypeEnum categoryType, bool showHidden = false, int categoryParentId = 0);

        IList<Category> GetCategoriesByCategoryIds(List<int> categoryIds);

        IList<Category> GetCategoriesByCategoryParentIds(List<int> categoryParentIds, bool showHidden=false);

        Task<List<Category>> GetCategoriesByCategoryParentIdAsync(int categoryParentId);

        IList<Category> GetCategoriesByCategoryParentIdWithCategoryType(int categoryParentId, CategoryTypeEnum categoryType, bool showHidden = false);

        void InsertCategory(Category category);
        void UpdateCategory(Category category);
        void DeleteCategory(Category category);

        IList<CategoryForStoreProfileResult> GetCategoriesByStoreMainPartyId(int mainPartyId);

        bool IsCategoryHaveProduct(int categoryId, int storeId);

        //IList<Category> GetCategoriesByCategoryParentId(int categoryParentId);

        IList<AllSubCategoryItemResult> GetSPAllSubCategoriesByCategoryId(int categoryId);

        void ClearAllCache();

        IList<Category> GetSPCategoryGetCategoryByCategoryName(string categoryName);


    }
}
