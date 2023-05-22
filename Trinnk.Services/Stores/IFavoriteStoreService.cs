using Trinnk.Entities.Tables.Stores;
using System.Collections.Generic;

namespace Trinnk.Services.Stores
{
    public interface IFavoriteStoreService
    {
        List<FavoriteStore> GetFavoriteStores();
        List<FavoriteStore> GetFavoriteStoresByMemberMainPartyId(int memberMainPartyId);

        FavoriteStore GetFavoriteStoreByMemberMainPartyIdWithStoreMainPartyId(int memberMainPartyId, int storeMainPartyId);
        IList<FavoriteStore> GetAllFavoriteStore(int pageSize, int page);

        void InsertFavoriteStore(FavoriteStore favoriteStore);

        void DeleteFavoriteStore(FavoriteStore favoriteStore);

    }
}
