using MakinaTurkiye.Entities.Tables.Stores;
using System.Data.Entity.ModelConfiguration;

namespace MakinaTurkiye.Data.Mapping.Tables.Stores
{
    public class StoreChangeHistoryMap : EntityTypeConfiguration<StoreChangeHistory>
    {
        public StoreChangeHistoryMap()
        {
            this.ToTable("StoreChangeHistory");
            this.Ignore(s=>s.Id);
            this.HasKey(s=>s.StoreChangeHistoryId);

            //this.HasRequired(s => s.Store).WithMany(st => st.StoreChangeHistories).HasForeignKey(s => s.MainPartyId);
        }
    }
}
