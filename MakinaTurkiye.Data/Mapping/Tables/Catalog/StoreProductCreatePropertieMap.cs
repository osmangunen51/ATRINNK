using MakinaTurkiye.Entities.Tables.Catalog;
using System.Data.Entity.ModelConfiguration;

namespace MakinaTurkiye.Data.Mapping.Tables.Catalog
{
    public class StoreProductCreatePropertieMap : EntityTypeConfiguration<StoreProductCreatePropertie>
    {
        public StoreProductCreatePropertieMap()
        {
            this.ToTable("StoreProductCreatePropertie");
            this.Ignore(x => x.Id);
            this.HasKey(x => x.StoreProductCreatePropertieId);
        }
    }
}
