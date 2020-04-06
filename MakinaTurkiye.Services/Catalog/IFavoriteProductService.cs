using MakinaTurkiye.Core;
using MakinaTurkiye.Entities.Tables.Catalog;
using System.Collections.Generic;

namespace MakinaTurkiye.Services.Catalog
{
    public interface IFavoriteProductService
    {
        List<FavoriteProduct> GetFavoriteProducts();
        FavoriteProduct GetFavoriteProductByFavoriteProductId(int favoriteProductId);
        FavoriteProduct GetFavoriteProductByMainPartyIdWithProductId(int mainPartyId, int productId);
        IPagedList<FavoriteProduct> GetAllFavoriteProduct(int pageSize, int page);

        void InsertFavoriteProduct(FavoriteProduct favoriteProduct);

        void DeleteFavoriteProduct(FavoriteProduct favoriteProduct);
    }
}
