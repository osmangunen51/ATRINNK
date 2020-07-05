using MakinaTurkiye.Entities.Tables.Stores;
using System.Data.Entity.ModelConfiguration;

namespace MakinaTurkiye.Data.Mapping.Tables.Stores
{
    public class FavoriteStoreMap: EntityTypeConfiguration<FavoriteStore>
    {
        public FavoriteStoreMap()
        {
            this.ToTable("FavoriteStore");
            this.Ignore(f => f.Id);
            this.HasKey(f => f.FavoriteMainPartyId);
        }
    }
}
