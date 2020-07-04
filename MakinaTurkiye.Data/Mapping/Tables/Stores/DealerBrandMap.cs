using MakinaTurkiye.Entities.Tables.Stores;
using System.Data.Entity.ModelConfiguration;

namespace MakinaTurkiye.Data.Mapping.Tables.Stores
{
    public class DealerBrandMap:EntityTypeConfiguration<DealerBrand>
    {
        public DealerBrandMap()
        {
            this.ToTable("DealerBrand");
            this.Ignore(d=>d.Id);
            this.HasKey(d=>d.DealerBrandId);

            //this.HasRequired(d => d.Store).WithMany(s => s.DealarBrands).HasForeignKey(s=>s.MainPartyId);

        }
    }
}
