using Trinnk.Entities.Tables.Messages;
using System.Data.Entity.ModelConfiguration;

namespace Trinnk.Data.Mapping.Tables.Messages
{
    public class MessageMap : EntityTypeConfiguration<Message>

    {
        public MessageMap()
        {
            this.ToTable("Message");
            this.Ignore(x => x.Id);
            this.HasKey(x => x.MessageId);
        }
    }
}
