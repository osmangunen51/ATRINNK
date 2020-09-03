using MakinaTurkiye.Core;
using MakinaTurkiye.Entities.Tables.Stores;
using System.Collections.Generic;

namespace MakinaTurkiye.Services.Stores
{
    public interface IFavoriteStoreService
    {
        List<FavoriteStore> GetFavoriteStores();

        FavoriteStore GetFavoriteStoreByMemberMainPartyIdWithStoreMainPartyId(int memberMainPartyId, int storeMainPartyId);
        IList<FavoriteStore> GetAllFavoriteStore(int pageSize, int page);

        void InsertFavoriteStore(FavoriteStore favoriteStore);

        void DeleteFavoriteStore(FavoriteStore favoriteStore);

    }
}
