using MakinaTurkiye.Entities.Tables.Catalog;

namespace MakinaTurkiye.Services.Catalog
{
    public interface IDeletedProductRedirectService
    {
        DeletedProductRedirect GetDeletedProductRedirectByProductId(int productId);
        void InsertDeletedProductRedirect(DeletedProductRedirect deletedProductRedirect);
    }
}
