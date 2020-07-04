using MakinaTurkiye.Entities.Tables.Stores;
using System.Data.Entity.ModelConfiguration;

namespace MakinaTurkiye.Data.Mapping.Tables.Stores
{
    public class StoreSectorMap:EntityTypeConfiguration<StoreSector>
    {
        public StoreSectorMap()
        {
            this.ToTable("StoreSector");
            this.Ignore(x => x.Id);
            this.HasKey(x => x.StoreSectorId);

            //this.HasRequired(x => x.Store).WithMany(x => x.StoreSectors).HasForeignKey(x => x.StoreMainPartyId);
        }
    }
}
