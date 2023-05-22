using Trinnk.Entities.Tables.Stores;
using System.Data.Entity.ModelConfiguration;

namespace Trinnk.Data.Mapping.Tables.Stores
{
    public class StoreActivityTypeMap : EntityTypeConfiguration<StoreActivityType>
    {
        public StoreActivityTypeMap()
        {
            this.ToTable("StoreActivityType");
            this.Ignore(sa => sa.Id);
            this.HasKey(sa => sa.StoreActivityTypeId);

            //this.HasRequired(s => s.ActivityType).WithMany(sa=>sa.StoreActivityTypes).HasForeignKey(x => x.ActivityTypeId);
        }
    }
}
