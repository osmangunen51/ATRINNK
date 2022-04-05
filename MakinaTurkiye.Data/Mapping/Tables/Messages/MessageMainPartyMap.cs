using MakinaTurkiye.Entities.Tables.Messages;
using System.Data.Entity.ModelConfiguration;

namespace MakinaTurkiye.Data.Mapping.Tables.Messages
{
    public class MessageMainPartyMap : EntityTypeConfiguration<MessageMainParty>
    {
        public MessageMainPartyMap()
        {
            this.ToTable("MessageMainParty");
            this.Ignore(x => x.Id);
            this.HasKey(x => x.MessageMainPartyId);
        }
    }
}
