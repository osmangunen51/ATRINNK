using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MakinaTurkiye.Services.Catalog
{
    public enum SiteMapCategoryTypeEnum:byte
    {
        Brand = CategoryTypeEnum.Brand,
        Series = CategoryTypeEnum.Series,
        Model = CategoryTypeEnum.Model,
        ProductGroup = CategoryTypeEnum.ProductGroup,
        Sector = CategoryTypeEnum.Sector,
        Category = CategoryTypeEnum.Category,
        ProductGroupBrand = 7
    }
}
