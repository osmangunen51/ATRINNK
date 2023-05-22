using Trinnk.Core;
using Trinnk.Entities.Tables.Catalog;
using System.Collections.Generic;

namespace Trinnk.Services.Catalog
{
    public interface IFavoriteProductService
    {
        List<FavoriteProduct> GetFavoriteProducts();

        List<FavoriteProduct> GetFavoriteProductsByMainPartyId(int mainPartyId);

        FavoriteProduct GetFavoriteProductByFavoriteProductId(int favoriteProductId);
        FavoriteProduct GetFavoriteProductByMainPartyIdWithProductId(int mainPartyId, int productId);
        IPagedList<FavoriteProduct> GetAllFavoriteProduct(int pageSize, int page);

        void InsertFavoriteProduct(FavoriteProduct favoriteProduct);

        void DeleteFavoriteProduct(FavoriteProduct favoriteProduct);
    }
}
