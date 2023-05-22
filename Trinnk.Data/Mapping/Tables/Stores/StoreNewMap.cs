using Trinnk.Entities.Tables.Stores;
using System.Data.Entity.ModelConfiguration;

namespace Trinnk.Data.Mapping.Tables.Stores
{
    public class StoreNewMap : EntityTypeConfiguration<StoreNew>
    {
        public StoreNewMap()
        {
            this.ToTable("StoreNew");
            this.Ignore(x => x.Id);
            this.HasKey(x => x.StoreNewId);

            //this.HasRequired(x => x.Store).WithMany(x => x.StoreNews).HasForeignKey(x => x.StoreMainPartyId);
        }
    }
}
