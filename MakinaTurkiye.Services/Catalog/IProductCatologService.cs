using MakinaTurkiye.Entities.Tables.Catalog;
using System.Collections.Generic;

namespace MakinaTurkiye.Services.Catalog
{
    public interface IProductCatologService : ICachingSupported
    {
        ProductCatolog GetProductCatologByProductCatologId(int productCatologId);

        IList<ProductCatolog> GetProductCatologsByProductId(int productId);

        void InsertProductCatolog(ProductCatolog productCatolog);

        void DeleteProductCatolog(ProductCatolog productCatolog);

        void UpdateProductCatolog(ProductCatolog productCatolog);
    }
}
