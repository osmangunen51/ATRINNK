using Trinnk.Entities.Tables.Catalog;

namespace Trinnk.Services.Catalog
{
    public interface IDeletedProductRedirectService
    {
        DeletedProductRedirect GetDeletedProductRedirectByProductId(int productId);
        void InsertDeletedProductRedirect(DeletedProductRedirect deletedProductRedirect);
    }
}
