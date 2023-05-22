using Trinnk.Entities.StoredProcedures.Catalog;
using System.Collections.Generic;

namespace Trinnk.Services.Catalog
{
    public interface ISiteMapCategoryService
    {
        IList<SiteMapStoreCategoryResult> GetSiteMapStoreCategories();

        IList<SiteMapVideoCategoryResult> GetSiteMapVideoCategories();

        IList<SiteMapCategoryResult> GetSiteMapCategories(SiteMapCategoryTypeEnum categoryType);

        IList<SiteMapCategoryPlaceResult> GetSiteMapCategoriesPlace(CategoryPlaceTypeEnum categoryPlaceType);

    }
}
