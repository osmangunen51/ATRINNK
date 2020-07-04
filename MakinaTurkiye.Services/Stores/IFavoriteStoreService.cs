using MakinaTurkiye.Entities.Tables.Stores;

namespace MakinaTurkiye.Services.Stores
{
    public interface IFavoriteStoreService
    {
        FavoriteStore GetFavoriteStoreByMemberMainPartyIdWithStoreMainPartyId(int memberMainPartyId, int storeMainPartyId);

        void InsertFavoriteStore(FavoriteStore favoriteStore);

        void DeleteFavoriteStore(FavoriteStore favoriteStore);
    }
}
