using MakinaTurkiye.Entities.Tables.Stores;
using System.Data.Entity.ModelConfiguration;

namespace MakinaTurkiye.Data.Mapping.Tables.Stores
{
    public class StorePackagePurchaseRequestMap : EntityTypeConfiguration<StorePackagePurchaseRequest>
    {
        public StorePackagePurchaseRequestMap()
        {
            this.ToTable("StorePackagePurchaseRequest");
            this.HasKey(x => x.Id);
        }
    }
}
