using NeoSistem.MakinaTurkiye.Web.Models.Catalog;

namespace NeoSistem.MakinaTurkiye.Web.Factories
{
    public interface IProductModelFactory
    {
        MTCategoryProductModel PrepareProductAreaItemModel(global::MakinaTurkiye.Entities.Tables.Catalog.Product product);
    }
}