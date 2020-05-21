using MakinaTurkiye.Entities.StoredProcedures.Catalog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MakinaTurkiye.Services.Catalog
{
    public interface ISiteMapCategoryService
    {
        IList<SiteMapStoreCategoryResult> GetSiteMapStoreCategories();

        IList<SiteMapVideoCategoryResult> GetSiteMapVideoCategories();

        IList<SiteMapCategoryResult> GetSiteMapCategories(SiteMapCategoryTypeEnum categoryType);

        IList<SiteMapCategoryPlaceResult> GetSiteMapCategoriesPlace(CategoryPlaceTypeEnum categoryPlaceType);

    }
}
