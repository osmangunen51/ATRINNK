using MakinaTurkiye.Entities.Tables.Catalog;
using System.Collections.Generic;

namespace MakinaTurkiye.Services.Catalog
{
    public interface IProductHomePageService
    {
        IList<ProductHomePage> GetProductHomePages();

        IList<ProductHomePage> GetProductHomePagesByCategoryId(int categoryId, bool showHidden = false);

        void InsertProductHomePage(ProductHomePage productHomePage);
        void DeleteProductHomePage(ProductHomePage productHomePage);
        void UpdateProductHomePage(ProductHomePage productHomePage);

        ProductHomePage GetProductHomePageByProductId(int productId);

    }
}
