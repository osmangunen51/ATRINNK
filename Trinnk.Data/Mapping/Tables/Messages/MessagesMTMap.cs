using Trinnk.Entities.Tables.Messages;
using System.Data.Entity.ModelConfiguration;

namespace Trinnk.Data.Mapping.Tables.Messages
{
    public class MessagesMTMap : EntityTypeConfiguration<MessagesMT>
    {
        public MessagesMTMap()
        {
            this.ToTable("MessagesMT");
            this.Ignore(x => x.Id);
            this.HasKey(x => x.MessagesMTId);
        }

    }
}
