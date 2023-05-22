using Trinnk.Entities.Tables.Stores;
using System.Data.Entity.ModelConfiguration;

namespace Trinnk.Data.Mapping.Tables.Stores
{
    public class StoreUpdatedMap : EntityTypeConfiguration<StoreUpdated>
    {
        public StoreUpdatedMap()
        {
            this.ToTable("StoreUpdated");
            this.Ignore(x => x.Id);
            this.HasKey(x => x.StoreUpdatedId);
        }
    }
}
