using Trinnk.Entities.Tables.Messages;
using System.Data.Entity.ModelConfiguration;

namespace Trinnk.Data.Mapping.Tables.Messages
{
    public class MobileMessageMap : EntityTypeConfiguration<MobileMessage>
    {
        public MobileMessageMap()
        {
            this.ToTable("MobileMessage");
            this.HasKey(x => x.ID);
            this.Ignore(x => x.Id);

        }
    }
}
