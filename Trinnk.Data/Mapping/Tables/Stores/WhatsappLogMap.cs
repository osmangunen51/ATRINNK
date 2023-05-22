using Trinnk.Entities.Tables.Stores;
using System.Data.Entity.ModelConfiguration;

namespace Trinnk.Data.Mapping.Tables.Stores
{
    public class WhatsappLogMap : EntityTypeConfiguration<WhatsappLog>
    {
        public WhatsappLogMap()
        {
            this.ToTable("WhatsappLog");
            this.Ignore(x => x.Id);
            this.HasKey(x => x.WhatsappLogId);

            //this.HasRequired(w => w.Store).WithMany(x => x.WhatsappLogs).HasForeignKey(x => x.MainPartyId);
        }
    }
}
