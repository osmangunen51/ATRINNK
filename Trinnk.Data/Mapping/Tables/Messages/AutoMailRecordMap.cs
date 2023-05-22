using Trinnk.Entities.Tables.Messages;
using System.Data.Entity.ModelConfiguration;

namespace Trinnk.Data.Mapping.Tables.Messages
{
    public class AutoMailRecordMap : EntityTypeConfiguration<AutoMailRecord>
    {
        public AutoMailRecordMap()
        {
            this.ToTable("AutoMailRecord");
            this.Ignore(x => x.Id);
            this.HasKey(x => x.AutoMailRecordId);

            //this.HasRequired(x => x.Store).WithMany(s => s.AutoMailRecords).HasForeignKey(x => x.StoreMainPartyId);
        }
    }
}
