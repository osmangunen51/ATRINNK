using NeoSistem.Trinnk.Web.Models.Catalog;

namespace NeoSistem.Trinnk.Web.Factories
{
    public interface IProductModelFactory
    {
        MTCategoryProductModel PrepareProductAreaItemModel(global::Trinnk.Entities.Tables.Catalog.Product product);
    }
}